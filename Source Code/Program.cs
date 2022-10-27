using System;
using System.IO;
namespace VigenereDecryptionTools
{
    static class Program
    {
        //this will be the plaintext output file
        private const string plaintextFile = @"plaintext.txt";

        /// <summary>
        /// Automatically runs the decryption program, prints the key, and writes the data to an output file
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
            	string ciphertextFile = "";
            	while (!File.Exists(ciphertextFile))
            	{
            		Console.WriteLine("Input the CORRECT path/name of the ciphertext file");
            		ciphertextFile = Console.ReadLine();
            	}
            	
                Decrypter cipher = new Decrypter(File.ReadAllText(ciphertextFile));

                string key = cipher.FindEncryptionKey();

                Console.WriteLine($"Encryption Key Found: \"{key}\"");

                string plaintext = cipher.Decrypt(key);
                File.WriteAllText(plaintextFile, plaintext);


                Console.WriteLine($"\nPlaintext file has been written to {plaintextFile}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}