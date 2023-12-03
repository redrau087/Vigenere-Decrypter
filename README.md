# Vigenere-Decryptor
A program to find the key for a given ciphertext which was encrypted with a Vigenere cipher using frequency analysis

>Developed for Computer Security Project 1 by Travis Rau

This decryption tool decrypts the Vigenere Cipher using frequency analysis

When attempting to determine the key length, frequency analysis is extremely helpful. This is because we know a very specific letter distribution for English that can be applied to sufficiently large text. To determine the key length, you only need an algorithm to split up the cipher text into n cosets and analyze only one 

Example:

> "ABCDEFGHIJKLMNOPQRSTUVWXYZ" with a key of "K1K2" should be split into  
> "ACEGIKMOQSUWY" which would be encrypted with K1 and "BDFHJLNPRTVXZ" which would be encrypted with K2 
 
The first coset would be encrypted with K1 and the second would be encrypted with K2, meaning each coset can be treated as its own cipher text. This allows you to run frequency analysis on just one of them

To find the key length:  
1. Perform a frequency analysis on ONE of the split strings (split into n strings)
2. Determine which n provides a distribution that looks most like English (one letter should be 12-13%, and 4-5 should be less than 1%)
3. n is the determined key length


Once you know the key length, you can adjust the single character key to best align with the English frequency


To see what the code does, check the source code
[C++](https://github.com/redrau087/Vigenere-Decrypter/tree/main/C%2B%2B%20Source%20Code) and [.NET](https://github.com/redrau087/Vigenere-Decrypter/tree/main/Dotnet%20Source%20Code)  

## Additional resources:
[The Vigenere Cipher: Frequency Analysis](https://pages.mtu.edu/~shene/NSF-4/Tutorial/VIG/Vig-Frequency-Analysis.html)  
[The Vigenere Cipher -- A Polyalphabetic Cipher](http://www.cs.trincoll.edu/cryptography/vigenere.html)  
[Letter Frequency](https://en.wikipedia.org/wiki/Letter_frequency)  
[Cracking Caesar Cipher (Many graphs to visualize data)](https://jrinconada.medium.com/cracking-caesar-cipher-8fe79226aabd) 