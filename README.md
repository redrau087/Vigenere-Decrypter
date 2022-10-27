# Vigenere-Decryptor
A program to find the key for a given ciphertext which was encrypted with a Vigenere cipher using frequency analysis

>Developed for Computer Security Project 1

This decryption tool decrypts the Vigenere Cipher using frequency analysis

When attempting to determine the key length, frequency analysis is extremely helpful. This is because we know a very specific letter distribution for English that can be applied to sufficiently large text. To determine the key length, you only need an algorithm to split up the cipher text into n parts and an analyzer to analyze ONLY ONE part. 

Example:

> "ABCDEFGHIJKLMNOPQRSTUVWXYZ" with a key length of 2 should be split into  
> "ACEGIKMOQSUWY" and "BDFHJLNPRTVXZ"  
>               K1                                          K2  
The first split text would be encrypted with K1 and the second would be encrypted with K2, meaning you can each piece of split text can be treated as its own cipher text. This allows you to run frequency analysis on just one of the pieces of split text to determine the key length.


Doing this split with an incorrect key length would typically result in distributions that don't make sense such as all letters being approximately 4% of the ciphertext.

When a piece of split text has at least 1 letter with a frequency greater than 10% and some less than 1% it is highly likely that the key length is correct. There should be 4-5 letters with a frequency of less than 1% but typically the key length finds all of these traits at once.

Once you know the key length, you can adjust the position of the highest frequency to match 'E'. This can be done by
1. Decrypt the split cipher text using the key 'A'
2. Analyze the decrypted cipher text
3. Calculate the highest probability character - 'E' + 'A'
```C#
(char)(analyzer.HighestProbabilityCharacter - 'E' + 'A');
```
4. This result is the single character key for the split portion of cipher text. This is repeated for each character of the key and with each split cipher text string until the entire key is discovered


To see what the code does, read 
[Analyzer.md](https://github.com/redrau087/Vigenere-Decryptor/blob/main/Markdown%20and%20XML/Analyzer.md) and [Decrypter.md](https://github.com/redrau087/Vigenere-Decryptor/blob/main/Markdown%20and%20XML/Decrypter.md)  

To run the code
[Download the exe for Windows](https://github.com/redrau087/Vigenere-Decryptor/program.exe) OR
[Dowload the docker image](https://hub.docker.com/repository/docker/redrau087/vigenerecipherdecrypter/general)



## Additional resources:
[The Vigenere Cipher: Frequency Analysis](https://pages.mtu.edu/~shene/NSF-4/Tutorial/VIG/Vig-Frequency-Analysis.html)  
[The Vigenere Cipher -- A Polyalphabetic Cipher](http://www.cs.trincoll.edu/cryptography/vigenere.html)  
[Letter Frequency](https://en.wikipedia.org/wiki/Letter_frequency)