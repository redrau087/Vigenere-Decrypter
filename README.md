# Vigenere-Decryptor
A program to find the key for a given ciphertext which was encrypted with a Vigenere cipher using frequency analysis

>Developed for Computer Security Project 1

This decryption tool decrypts the Vigenere Cipher using frequency analysis

When attempting to determine the key length, frequency analysis is extremely helpful. This is because we know a very specific letter distribution for English that can be applied to sufficiently large text. To determine the key length, you only need an algorithm to split up the cipher text into n parts and an analyzer to analyze ONLY ONE part. Doing this with an incorrect key length would typically result in distributions that don't make sense such as all letters being approximately 4% of the ciphertext.

When the key length checker's ciphertext has at least 1 letter with a frequency greater than 10% and some less than 1% it is highly likely that the key length is correct. There should be 4-5 letters with a frequency of less than 1% but typically the key length finds all of these traits at once.

Once you know the key length, you can adjust the position of the highest frequency to match 'E'. This can be done by
1. Decrypt the separated cipher text string using the key 'A'
2. Analyze the decrypted cipher text
3. Calculate the highest probability character - 'E' + 'A'
```C#
(char)(analyzer.HighestProbabilityCharacter - 'E' + 'A');
```
4. This result is the single character key for the portion of cipher text

This is repeated for each character of the key and with each separated cipher text string until the entire key is discovered


To see what the code does, read the markdown files to get information

##Additional resources:
[The Vigenere Cipher: Frequency Analysis](https://pages.mtu.edu/~shene/NSF-4/Tutorial/VIG/Vig-Frequency-Analysis.html)
[The Vigenere Cipher -- A Polyalphabetic Cipher](http://www.cs.trincoll.edu/cryptography/vigenere.html)