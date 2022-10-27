using System;
using System.IO;
namespace VigenereDecryptionTools
{
    static class Program
    {
        //this will be the ciphertext file
        private const string ciphertextFile = @"ciphertext.txt";

        /// <summary>
        /// Automatically runs the decryption program and prints the key along with part of the decrypted data to the console
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                Decrypter cipher = new Decrypter(File.ReadAllText(ciphertextFile));

                string key = cipher.FindEncryptionKey();

                Console.WriteLine($"Encryption Key Found: \"{key}\"");

                string plaintext = cipher.Decrypt(key);

                Console.WriteLine("Printing the first 5000 characters of the decrypted text in 10 seconds\n");
                System.Threading.Thread.Sleep(10000);
                Console.Write(plaintext.Substring(0, 5000));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}