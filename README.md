# Vigenere-Decryptor
A program to find the key for a given ciphertext which was encrypted with a Vigenere cipher using frequency analysis

>Developed for Computer Security Project 1

This decryption tool decrypts the Vigenere Cipher using frequency analysis

When attempting to determine the key length, frequency analysis is extremely helpful. This is because we know a very specific letter distribution for English that can be applied to sufficiently large text. To determine the key length, you only need an algorithm to split up the cipher text into n parts and an analyzer to analyze ONLY ONE part. 

Example:

> "ABCDEFGHIJKLMNOPQRSTUVWXYZ" with a key of "K1K2" should be split into  
> "ACEGIKMOQSUWY" which was would be encrypted with K1 and "BDFHJLNPRTVXZ" which would be encrypted with K2 
 
The first split text would be encrypted with K1 and the second would be encrypted with K2, meaning you can each piece of split text can be treated as its own cipher text. This allows you to run frequency analysis on just one of the pieces of split text to determine the key length.

Given a max possible key length "max"
1. Split the cipher text into n parts
2. Select only the 1st part
3. Decrypt with 'A'
4. Score the distribution
5. Repeat with the other 25 possible keys to see what provides the best score
6. Keep a record of the best score
7. Repeat for n+1 until you have the best score for all key sizes between 1 and max
8. The key size with the best score is most likely the correct key size



Once you know the key length, you can adjust the single character key to best align with the English frequency
1. Decrypt with 'A'
2. Score the distribution
3. Repeat with the other 25 possible keys to see what provides the best score
4. This result is the single character key for the split portion of cipher text. This is repeated for each character of the key and with each split cipher text string until the entire key is discovered


To see what the code does, read 
[Analyzer.md](https://github.com/redrau087/Vigenere-Decryptor/blob/main/Markdown%20and%20XML/Analyzer.md) and [Decrypter.md](https://github.com/redrau087/Vigenere-Decryptor/blob/main/Markdown%20and%20XML/Decrypter.md)  

To run the code [Download the exe for Windows](https://github.com/redrau087/Vigenere-Decrypter/raw/main/program.exe)  
The program will automatically prompt the user for the ciphertext input file, the max key length to test, and if they would like to have each step explained as it solves  

This program can take a large amount of time with large cipher texts (around a minute for the provided cipher text) due to the amount of statistical analysis being performed for accuracy


## Additional resources:
[The Vigenere Cipher: Frequency Analysis](https://pages.mtu.edu/~shene/NSF-4/Tutorial/VIG/Vig-Frequency-Analysis.html)  
[The Vigenere Cipher -- A Polyalphabetic Cipher](http://www.cs.trincoll.edu/cryptography/vigenere.html)  
[Letter Frequency](https://en.wikipedia.org/wiki/Letter_frequency)
