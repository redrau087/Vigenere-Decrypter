# Decrypter
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