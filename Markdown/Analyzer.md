# StringAnalyzer
Uses an unsigned integer array of length 26 to hold the total number of occurrences for each letter. This can be used for frequency analysis

### TotalCharacters Property
>Return type: uint

Returns the total number of characters analyzed

###FormattedLetterFrequencies Property
>Return type: string 

Returns a string that shows the frequency of each letter analyzed

###LetterFrequencies
>Return type: IEnumerable<double> 

Returns the frequency of each character in order

###MeanAbsoluteDeviation Property
>Return type: double  

Scores the text by how many separate letters there are. Lower values are typically less correct

###DeviationFromStandardFrequency property
>Return type: double  

Scores the frequency by how close it is to a standard english frequency

### Analyze  
Analyzes the input and tallies the total of each character

### Clear  
Clears the counters for each character to reset the analyzer