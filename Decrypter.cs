using System;
using System.Text;

namespace VigenerDecryptionTools
{
    public class Decrypter
    {
        #region Private Variables
        private string cipherText;
        #endregion

        #region Constructors
        public Decrypter(string cipherTextIn)
        {
            cipherText = cipherTextIn;
        }
        #endregion

        #region Instance Methods

        /// <summary>
        /// Iterates through each character in the provided cipher text and decrypts it using the given key
        /// </summary>
        /// <param name="cipherTextIn">The cipher text to decrypt</param>
        /// <param name="key">The key to be used for decryption</param>
        /// <returns>The decrypted characters</returns>
        private IEnumerable<char> DecryptSingleKey(string cipherTextIn, char key)
        {
            foreach (char currentChar in cipherTextIn)
                yield return DecryptSingleChar(currentChar, key);
        }

        /// <summary>
        /// Decrypts one character with the given key using a caesar shift cipher
        /// </summary>
        /// <param name="cipherCharIn">The character to be decrypted</param>
        /// <param name="key">The decryption key</param>
        /// <returns>The decrypted character</returns>
        private char DecryptSingleChar(char cipherCharIn, char key)
        {
            byte temp = (byte)(cipherCharIn - 'A' + 26);
            temp -= (byte)(key - 'A');
            temp %= 26;
            temp += (byte)'A';
            return (char)temp;
        }

        /// <summary>
        /// Separates strings for encryption by each character of the key
        /// </summary>
        /// <param name="keyLength">The length of the key</param>
        /// <returns>The separated strings to decrypt using a single character key</returns>
        private IEnumerable<string>SeparateStrings(int keyLength)
        {
            StringBuilder[] stringBuilders = new StringBuilder[keyLength];
            for (int x = 0; x < keyLength; x++)
                stringBuilders[x] = new();

            for (int x = 0; x < cipherText.Length; x++)
                stringBuilders[x % keyLength].Append(cipherText[x]);

            foreach (StringBuilder sb in stringBuilders)
                yield return sb.ToString();
        }

        /// <summary>
        /// Performs a frequency analysis testing multiple different key lengths to determine the most probable key length
        /// </summary>
        /// <returns>The key length or 0 if not found</returns>
        public ushort FindKeyLength()
        {
            StringAnalyzer analyzer = new();
            string cipherText;
            for (byte x = 1; x < byte.MaxValue; x++)
            {
                analyzer.Clear(); //clear analysis from previous test

                cipherText = SeparateStrings(x).ToArray()[0];
                //we only need to check one of the new strings for frequency analysis so we can select only the first
                analyzer.Analyze(cipherText);

                if (analyzer.HighestProbability > 10 && analyzer.LowestProbability < 1)
                    //one letter should be ~13% and multiple should be less than 1% so we can return if this is found
                    return x;
            }

            return 0;
        }

        /// <summary>
        /// Finds the key by determining the distance between 'E' and the most frequent letter analyzed to discover each character of the key at a time
        /// </summary>
        /// <param name="keyLength"></param>
        /// <returns>Each character of the key as it's discovered</returns>
        private IEnumerable<char> FindKey(ushort keyLength)
        {
            StringAnalyzer analyzer = new();

            foreach (string cipher in SeparateStrings(keyLength))
            {
                analyzer.Clear();

                analyzer.Analyze(DecryptSingleKey(cipher, 'A'));

                yield return (char)(analyzer.HighestProbabilityCharacter - 'E' + 'A');
            }
        }

        /// <summary>
        /// Finds and returns the key used for encrypting the ciphertext as a string
        /// </summary>
        /// <param name="maxKeyLength">The max possible key length to check</param>
        /// <returns>The key string</returns>
        public string FindEncryptionKey() => new string(FindKey(FindKeyLength()).ToArray());

        /// <summary>
        /// Decrypts the ciphertext using the given key
        /// </summary>
        /// <param name="keyIn">The encryption/decryption key</param>
        /// <returns>The decrypted plaintext</returns>
        public string Decrypt(string keyIn)
        {
            StringBuilder sb = new();
            for (int x = 0; x < cipherText.Length; x++)
                sb.Append(DecryptSingleChar(cipherText[x], keyIn[x % keyIn.Length]));

            return sb.ToString();
        }

        /// <summary>
        /// A wrapper for Decrypt(string) that does not require you to input the key
        /// </summary>
        /// <returns>The decrypted plaintext</returns>
        public string Decrypt() => Decrypt(FindEncryptionKey());
        #endregion
    }
}

