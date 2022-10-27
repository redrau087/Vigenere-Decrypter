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
>Parameters: string or IEnumerable<char>

Increments the counter for the specific character that was input to the analyzer

### Clear
>Return type: void
>Accessibility: public
>Parameters: None

Clears the counters for each character to reset the analyzer




##Decrypter
Holds the cipher text input as a private string

###Decrypter Constructor
>Return type: Decrypter
>Accessibility: public
>Parameters: string

Takes the whole cipher text string as input

###DecryptSingleKey
>Return type: IEnumerable<char>
>Accessibility: private
>Parameters: string, char

Iterates through each char in the provided cipher text and decrypts it using the given key