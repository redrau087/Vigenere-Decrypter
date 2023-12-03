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
            		Console.WriteLine("Enter the CORRECT path/name of the ciphertext file");
            		ciphertextFile = Console.ReadLine();
            	}

                Decrypter cipher = new Decrypter(File.ReadAllText(ciphertextFile));
                string plaintext = "";
                string key = "";

                byte maxLength = 0;
                while (maxLength == 0)
                {
                    Console.WriteLine("Enter the max length key to be checked");
                    if (byte.TryParse(Console.ReadLine(), out maxLength))
                        break;
                    else
                        Console.WriteLine("Invalid value. Length must be between 1 and 255 inclusive");
                }

                char explain = ' ';
                while (char.ToLower(explain) != 'y' && char.ToLower(explain) != 'n')
                {
                    Console.WriteLine("Run decryption tool with explanations? y or n");
                    explain = Console.ReadKey().KeyChar;
                }

                Console.Clear();

                if (explain == 'y')
                    (key, plaintext) = cipher.DecryptAndExplain(maxLength);
                else
                    (key, plaintext) = cipher.Decrypt(maxLength);

                Console.Clear();
                File.WriteAllText(plaintextFile, plaintext);
                Console.WriteLine($"Key found: {key}\n\nPlaintext written to {plaintextFile}\nPress any key to close the window");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}