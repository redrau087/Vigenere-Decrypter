using System.IO;
namespace VigenerDecryptionTools
{
    static class Program
    {
        //You must set this to the ciphertext file
        private const string ciphertextFile = @"ciphertext.txt";
        //this will be the plaintext output file
        private const string plaintextFile = @"plaintext.txt";

        /// <summary>
        /// Automatically runs the decryption program and writes the data to an output file
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                Decrypter cipher = new Decrypter(File.ReadAllText(ciphertextFile));


                Console.WriteLine($"Encryption Key Found: \"{cipher.FindEncryptionKey()}\"");

                string plaintext = cipher.Decrypt();
                File.WriteAllText(plaintextFile, plaintext);


                Console.WriteLine("\nPlaintext file has been written");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}