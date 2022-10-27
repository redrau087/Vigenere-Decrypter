# Vigenere-Decryptor
A program to find the key for a given ciphertext which was encrypted with a Vigenere cipher using frequency analysis

>Developed for Computer Security Project 1



## StringAnalyzer
Uses an unsigned integer array of length 26 to hold the total number of occurrences for each letter

### TotalCharacters Property
>Return type: uint

Returns the total number of characters analyzed

### HighestProbabilityCharacter
>Return type: char

Returns the character that has appeared the most in the analysis

### HighestProbability
>Return type: double

Returns the frequency of the most common character as a percentage

### LowestProbability
>Return type: double

Returns the frequency of the least common character as a percentage

### StringAnalyzer Constructor
>Return type: StringAnalyzer
>Accessibility: public
>Parameters: None

### Analyze
>Return type: void
>Accessibility: public
>Parameters: string or IEnumerable<char> textIn

Analyzes the input and tallies the total of each character

### Clear
>Return type: void
>Accessibility: public
>Parameters: None

Clears the counters for each character to reset the analyzer


## Decrypter
Holds the cipher text input as a private string

### Decrypter Constructor
>Return type: Decrypter
>Accessibility: public
>Parameters: string cipherTextIn

Takes the whole cipher text string as input

### DecryptSingleKey
>Return type: IEnumerable<char>
>Accessibility: private
>Parameters: string cipherTextIn, char key

Iterates through each character in the provided cipher text and decrypts it using the given key

### DecryptSingleChar
>Return type: char
>Accessibility: private
>Parameters: char cipherCharIn, char key

Decrypts one character with the given key using a caesar shift cipher

### SeparateStrings
>Return type: IEnumerable<string>
>Accessibility: private
>Parameters: int keyLength

Separates strings for encryption by each character of the key

### FindKeyLength
>Return type: ushort
>Accessibility: public
>Parameters: None

Performs a frequency analysis testing multiple different key lengths to determine the most probable key length

### FindKey
>Return type: IEnumerable<char>
>Accessibility: private
>Parameters: short keyLength

Finds the key by determining the distance between 'E' and the most frequent letter analyzed to discover each character of the key at a time

### FindEncryptionKey
>Return type: string
>Accessibility: public
>Parameters: None

Finds and returns the key used for encrypting the ciphertext as a string

### Decrypt
>Return type: string
Accessibility: private
Parameters: void or string keyIn

Decrypts the ciphertext using the given key or finds the key and decrypts the ciphertext