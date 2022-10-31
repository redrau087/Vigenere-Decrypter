using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VigenereTools;

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

        #region Instance Methods
        /// <summary>
        /// Performs a frequency analysis testing multiple different key lengths to determine the most probable key length
        /// </summary>
        /// <returns>The key length or 0 if not found</returns>
        public byte FindKeyLength(byte maxLength)
        {
            StringAnalyzer analyzer = new StringAnalyzer();
            string cipher;
            double lowestScore = 100.0;
            double currentScore;

            byte bestKeySize = 1;
            double bestKeySizeScore = 100.0;


            for (byte x = 1; x < ((maxLength == Byte.MaxValue) ? maxLength : maxLength + 1); x++) //inclusive size unless max value
            {
                analyzer.Clear();
                cipher = Vigenere.SeparateStrings(cipherText, x).FirstOrDefault();
                //we only need to check one of the new strings for frequency analysis so we can select only the first

                analyzer.Analyze(cipherText);
                lowestScore = analyzer.DeviationFromStandardFrequency;
                for (byte y = 0; y < 26; y++)
                {
                    analyzer.Clear(); //clear analysis from previous test
                    analyzer.Analyze(Caesar.Decrypt(cipher, (char)('A' + y)));
                    currentScore = analyzer.DeviationFromStandardFrequency;
                    if (currentScore < lowestScore)
                        lowestScore = currentScore;
                }
                if (lowestScore < bestKeySizeScore)
                {
                    bestKeySizeScore = lowestScore;
                    bestKeySize = x;
                }
            }

            return bestKeySize;
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

            foreach (string cipher in Vigenere.SeparateStrings(cipherText, keyLength))
            {
                analyzer.Clear();

                analyzer.Analyze(Caesar.Decrypt(cipher, 'A'));
                lowestScore = analyzer.DeviationFromStandardFrequency;
                lowestScoreIndex = 0;

                for (byte x = 1; x < 26; x++)
                {
                    analyzer.Clear();
                    analyzer.Analyze(Caesar.Decrypt(cipher, (char)('A' + x)));
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
        public string FindEncryptionKey(byte maxLength) => new string(FindKey(FindKeyLength(maxLength)).ToArray());

        /// <summary>
        /// Decrypts the ciphertext using the given key
        /// </summary>
        /// <param name="keyIn">The encryption/decryption key</param>
        /// <returns>The decrypted plaintext</returns>
        public string Decrypt(string keyIn) => Vigenere.Decrypt(cipherText, keyIn);

        /// <summary>
        /// A wrapper for Decrypt(string) that does not require you to input the key
        /// </summary>
        /// <returns>The key and decrypted plaintext</returns>
        public (string, string) Decrypt(byte maxLength)
        {
            string key = FindEncryptionKey(maxLength);
            return (key, Decrypt(key));
        }
        #endregion

        #region Explaining
        /// <summary>
        /// Decrypts the ciphertext and explains the steps to finding the key while solving
        /// </summary>
        /// <param name="maxLength">The max length key to check</param>
        /// <returns>The key and plaintext</returns>
        public (string, string) DecryptAndExplain(byte maxLength)
        {
            //To decrypt the ciphertext we must find the key but to find the key the first step is to find the length of the key
            //Incorrect key lengths will have irregular letter frequencies. We can exploit this by testing different key lengths until we find a normal-looking distribution
            string key = FindEncryptionKeyAndExplain(FindKeyLengthAndExplain(maxLength));
            return (key, Decrypt(key));
        }

        /// <summary>
        /// Finds the length of the key used to encrypt the cipher text and explains the process
        /// </summary>
        /// <returns>The key length</returns>
        public byte FindKeyLengthAndExplain(byte maxLength)
        {
            StringAnalyzer analyzer = new StringAnalyzer();
            string cipher;
            double lowestScore = 100.0;
            double currentScore;

            byte bestKeySize = 1;
            double bestKeySizeScore = 100.0;


            for (byte x = 1; x < ((maxLength == Byte.MaxValue) ? maxLength : maxLength + 1); x++)
            {
                analyzer.Clear();
                cipher = Vigenere.SeparateStrings(cipherText, x).FirstOrDefault();
                //we only need to check one of the new strings for frequency analysis so we can select only the first

                analyzer.Analyze(cipherText);
                lowestScore = analyzer.DeviationFromStandardFrequency;
                for (byte y = 0; y < 26; y++)
                {
                    analyzer.Clear(); //clear analysis from previous test
                    analyzer.Analyze(Caesar.Decrypt(cipher, (char)('A' + y)));
                    currentScore = analyzer.DeviationFromStandardFrequency;
                    if (currentScore < lowestScore)
                        lowestScore = currentScore;
                }
                if (lowestScore < bestKeySizeScore)
                {
                    bestKeySizeScore = lowestScore;
                    bestKeySize = x;
                }

                Console.WriteLine($"After testing all possibilities with key length {x}, the best score is {lowestScore}" +
    $"\nThe current best key length is {bestKeySize} with a score of {bestKeySizeScore}");
            }

            return bestKeySize;
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

            foreach (string cipher in Vigenere.SeparateStrings(cipherText, keyLength))
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(); //hold so you can read previous data
                Console.Clear(); //clear the console
                Console.WriteLine($"Current key discovered: {keyFound.ToString()}");
                analyzer.Clear();



                analyzer.Analyze(Caesar.Decrypt(cipher, 'A'));
                lowestScore = analyzer.DeviationFromStandardFrequency;
                lowestScoreIndex = 0;

                for (byte x = 1; x < 26; x++)
                {
                    analyzer.Clear();
                    analyzer.Analyze(Caesar.Decrypt(cipher, (char)('A' + x)));
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
                analyzer.Analyze(Caesar.Decrypt(cipher, (char)(lowestScoreIndex + 'A')));
                Console.WriteLine(analyzer.FormattedLetterFrequencies);

                keyFound.Append((char)(lowestScoreIndex + 'A'));
            }

            return keyFound.ToString();
        }
        #endregion
    }
}