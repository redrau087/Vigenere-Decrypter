using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VigenereDecryptionTools
{
    /// <summary>
    /// Holds the ciphertext input and can use frequency analysis to determine the encryption key and decrypt
    /// </summary>
    public class Decrypter
    {
        #region Private Variables
        private string cipherText;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor that takes the cipher text string as input and removes any non-letter characters
        /// </summary>
        /// <param name="cipherTextIn">The cipher text</param>
        public Decrypter(string cipherTextIn)
        {
            string temp = cipherTextIn;
            StringBuilder sb = new StringBuilder();
            foreach (char c in temp)
                if (Char.IsLetter(c))
                    sb.Append(c);
            cipherText = sb.ToString();
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Performs a vigenere encryption with the given plain text and key
        /// </summary>
        /// <param name="plaintext">The plain text to be encrypted</param>
        /// <param name="key">Then encryption key</param>
        /// <returns></returns>
        public static string Encrypt(string plaintext, string key)
        {
            StringBuilder sb = new StringBuilder();

            for (int x = 0; x < plaintext.Length; x++)
                sb.Append((char)(((plaintext[x] - 'A' + key[x % key.Length] + 1) % 26) + 'A'));

            return sb.ToString();
        }

        /// <summary>
        /// Iterates through each character in the provided cipher text and decrypts it using the given key
        /// </summary>
        /// <param name="cipherTextIn">The cipher text to decrypt</param>
        /// <param name="key">The key to be used for decryption</param>
        /// <returns>The decrypted characters</returns>
        private static IEnumerable<char> DecryptSingleKey(string cipherTextIn, char key)
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
        private static char DecryptSingleChar(char cipherCharIn, char key)
        {
            byte temp = (byte)(cipherCharIn - 'A' + 26);
            temp -= (byte)(key - 'A');
            temp %= 26;
            temp += (byte)'A';
            return (char)temp;
        }
        #endregion

        #region Instance Methods
        /// <summary>
        /// Separates strings for encryption by each character of the key
        /// </summary>
        /// <param name="keyLength">The length of the key</param>
        /// <returns>The separated strings to decrypt using a single character key</returns>
        private IEnumerable<string>SeparateStrings(int keyLength)
        {
            StringBuilder[] stringBuilders = new StringBuilder[keyLength];
            for (int x = 0; x < keyLength; x++)
                stringBuilders[x] = new StringBuilder();

            for (int x = 0; x < cipherText.Length; x++)
                stringBuilders[x % keyLength].Append(cipherText[x]);

            foreach (StringBuilder sb in stringBuilders)
                yield return sb.ToString();
        }

        /// <summary>
        /// Performs a frequency analysis testing multiple different key lengths to determine the most probable key length
        /// </summary>
        /// <returns>The key length or 0 if not found</returns>
        public byte FindKeyLength()
        {
            StringAnalyzer analyzer = new StringAnalyzer();
            string cipherText;
            for (byte x = 1; x < byte.MaxValue; x++)
            {
                analyzer.Clear(); //clear analysis from previous test

                cipherText = SeparateStrings(x).ToArray()[0];
                //we only need to check one of the new strings for frequency analysis so we can select only the first
                analyzer.Analyze(cipherText);

                if (analyzer.MeanAbsoluteDeviation > 2.7)
                    //incorrect key lengths will have a much lower MAD
                    return x;
            }

            return 0;
        }

        /// <summary>
        /// Finds the key by determining the closest alignment between the standard frequency and the decrypted ciphertext
        /// </summary>
        /// <param name="keyLength"></param>
        /// <returns>Each character of the key as it's discovered</returns>
        private IEnumerable<char> FindKey(byte keyLength)
        {
            StringAnalyzer analyzer = new StringAnalyzer();
            double currentScore;
            double lowestScore;
            byte lowestScoreIndex;

            foreach (string cipher in SeparateStrings(keyLength))
            {
                analyzer.Clear();

                analyzer.Analyze(DecryptSingleKey(cipher, 'A'));
                lowestScore = analyzer.DeviationFromStandardFrequency;
                lowestScoreIndex = 0;

                for (byte x = 1; x < 26; x++)
                {
                    analyzer.Clear();
                    analyzer.Analyze(DecryptSingleKey(cipher, (char)('A' + x)));
                    currentScore = analyzer.DeviationFromStandardFrequency;

                    if (currentScore < lowestScore)
                    {
                        lowestScore = currentScore;
                        lowestScoreIndex = x;
                    }
                }

                yield return (char)('A' + lowestScoreIndex);
            }
        }

        /// <summary>
        /// Finds and returns the key used for encrypting the ciphertext as a string
        /// </summary>
        /// <returns>The key string</returns>
        public string FindEncryptionKey() => new string(FindKey(FindKeyLength()).ToArray());

        /// <summary>
        /// Decrypts the ciphertext using the given key
        /// </summary>
        /// <param name="keyIn">The encryption/decryption key</param>
        /// <returns>The decrypted plaintext</returns>
        public string Decrypt(string keyIn)
        {
            StringBuilder sb = new StringBuilder();
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

        #region Explaining
        /// <summary>
        /// Decrypts the ciphertext and explains the steps to finding the key while solving
        /// </summary>
        public void DecryptAndExplain()
        {
            //To decrypt the ciphertext we must find the key but to find the key the first step is to find the length of the key
            //Incorrect key lengths will have irregular letter frequencies. We can exploit this by testing different key lengths until we find a normal-looking distribution
            StringAnalyzer analyzer = new StringAnalyzer();

            analyzer.Clear(); //clear analysis from previous test

            cipherText = SeparateStrings(5).ToArray()[0];
            //we only need to check one of the new strings for frequency analysis so we can select only the first
            analyzer.Analyze(cipherText);

            Console.WriteLine(analyzer.FormattedLetterFrequencies);
            Console.WriteLine($"\nMAD: {analyzer.MeanAbsoluteDeviation}");
        }

        /// <summary>
        /// Finds the length of the key used to encrypt the cipher text and explains the process
        /// </summary>
        /// <returns>The key length</returns>
        public byte FindKeyLengthAndExplain()
        {
            StringAnalyzer analyzer = new StringAnalyzer();
            string cipherText;
            for (byte x = 1; x < byte.MaxValue; x++)
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(); //hold so you can read previous data
                Console.Clear(); //clear the console


                analyzer.Clear(); //clear analysis from previous test

                cipherText = SeparateStrings(x).ToArray()[0];
                //we only need to check one of the new strings for frequency analysis so we can select only the first
                analyzer.Analyze(cipherText);

                double mad = analyzer.MeanAbsoluteDeviation;

                if (mad > 2.7)
                {
                    Console.WriteLine($"A mean absolute deviation of {mad} is high enough so the expected key length is {x}");
                    //incorrect key lengths will have a much lower MAD
                    return x;
                }
                else
                    Console.WriteLine($"A mean absolute deviation of {mad} is too low so a key length of {x} is unlikely");
            }

            return 0;
        }

        /// <summary>
        /// Finds the encryption key used to encrypt the cipher text and explains the process
        /// </summary>
        /// <param name="keyLength">The length of the key to find</param>
        /// <returns>The encryption key</returns>
        public string FindEncryptionKeyAndExplain(byte keyLength)
        {
            StringAnalyzer analyzer = new StringAnalyzer();

            StringBuilder keyFound = new StringBuilder();

            double currentScore;
            double lowestScore;
            byte lowestScoreIndex;

            foreach (string cipher in SeparateStrings(keyLength))
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(); //hold so you can read previous data
                Console.Clear(); //clear the console
                Console.WriteLine($"Current key discovered: {keyFound.ToString()}");
                analyzer.Clear();



                analyzer.Analyze(DecryptSingleKey(cipher, 'A'));
                lowestScore = analyzer.DeviationFromStandardFrequency;
                lowestScoreIndex = 0;

                for (byte x = 1; x < 26; x++)
                {
                    analyzer.Clear();
                    analyzer.Analyze(DecryptSingleKey(cipher, (char)('A' + x)));
                    currentScore = analyzer.DeviationFromStandardFrequency;

                    if (currentScore < lowestScore)
                    {
                        lowestScore = currentScore;
                        lowestScoreIndex = x;
                    }
                }

                Console.WriteLine($"Key found: {(char)(lowestScoreIndex + 'A')} with a deviation of {lowestScore}" +
                    $"\nFrequency analysis using this key: ");
                analyzer.Clear();
                analyzer.Analyze(DecryptSingleKey(cipher, (char)(lowestScoreIndex + 'A')));
                Console.WriteLine(analyzer.FormattedLetterFrequencies);

                keyFound.Append((char)(lowestScoreIndex + 'A'));
            }

            return keyFound.ToString();
        }
        #endregion
    }
}

