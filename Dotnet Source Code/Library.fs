namespace VigenereTools

open System.Text

///<summary>A library for a caesar shift cipher</summary>
module Caesar =
    ///<summary>Encrypts a given plaintext with the provided key</summary>
    ///<param name="plaintext">The plaintext to encrypt</param>
    ///<param name="key">The key to use for encryption</param>
    ///<returns>The ciphertext</returns>
    let Encrypt(plaintext:string, key:char) =
        let mutable sb = new StringBuilder()
        let mutable calculationValue:byte = (byte)0
        for x in 0..(plaintext.Length - 1) do
            calculationValue <- (byte)plaintext.[x]
            calculationValue <- calculationValue - (byte)65
            calculationValue <- calculationValue + (byte)key
            calculationValue <- calculationValue + (byte)1
            calculationValue <- calculationValue % (byte)26;
            calculationValue <- calculationValue + (byte)65
            sb.Append((char)calculationValue) |> ignore

        sb.ToString()

    ///<summary>Decrypts a given ciphertext with the provided key</summary>
    ///<param name="ciphertext">The ciphertext to decrypt</param>
    ///<param name="key">The key to use for decryption</param>
    ///<returns>The plaintext</returns>
    let Decrypt(ciphertext:string, key:char) =
        let mutable sb = new StringBuilder()
        let mutable calculationValue:byte = (byte)0
        for x in 0..(ciphertext.Length - 1) do
            calculationValue <- (byte)ciphertext.[x] - (byte)39
            calculationValue <- calculationValue - ((byte)key - (byte)65)
            calculationValue <- calculationValue % (byte)26;
            calculationValue <- calculationValue + (byte)65
            sb.Append((char)calculationValue) |> ignore

        sb.ToString()



///<summary>A library for a vigenere cipher</summary>
module Vigenere =
    /// <summary>
    /// Separates the given string into n strings for a vigenere cipher
    /// </summary>
    /// <param name="text">The text to be split</param>
    /// <param name="keyLength">The number of strings to split into</param>
    let SeparateStrings(text:string, keyLength:byte) =
        let mutable stringBuilders = [|for x in 0..((int)keyLength - 1) -> new StringBuilder()|]
        for x in 0..(text.Length - 1) do
            stringBuilders.[x % (int)keyLength].Append(text.[x]) |> ignore
        seq { for sb in stringBuilders do yield sb.ToString()}

    ///<summary>Encrypts the given plaintext with the key</summary>
    ///<param name="plaintext">The plaintext to encrypt</param>
    ///<param name="key">The key to use for encryption</param>
    ///<returns>The ciphertext</returns>
    let Encrypt(plaintext:string, key:string) =
        let mutable sb = new StringBuilder()
        let length = key.Length
        let mutable calculationValue:byte = (byte)0
        for x in 0..(plaintext.Length - 1) do
            calculationValue <- (byte)plaintext.[x] - (byte)65
            calculationValue <- calculationValue + (byte)key.[x % length] - (byte)65
            calculationValue <- calculationValue % (byte)26
            calculationValue <- calculationValue + (byte)65
            sb.Append((char)calculationValue) |> ignore

        sb.ToString()

    ///<summary>Decrypts the given ciphertext with the key</summary>
    ///<param name="ciphertext">The ciphertext to be decrypted</param>
    ///<param name="key">The key to use for decryption</param>
    ///<returns>The plaintext</returns>
    let Decrypt(ciphertext:string, key:string) =
        let mutable sb = new StringBuilder()
        for x in 0..(ciphertext.Length - 1) do
            sb.Append(Caesar.Decrypt(ciphertext.[x].ToString(), key.[x % key.Length])) |> ignore

        sb.ToString()