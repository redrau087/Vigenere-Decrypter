# StringAnalyzer
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