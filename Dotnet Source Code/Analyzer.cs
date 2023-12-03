using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VigenereDecryptionTools
{
    /// <summary>
    /// Uses an unsigned integer array of length 26 to hold the total number of occurrences for each letter. This can be used for frequency analysis
    /// </summary>
    public class StringAnalyzer
    {
        #region Private Variables
        private protected uint[] characterCount = new uint[26];
        private protected const double standardMean = 100.0 / 26.0;
        private protected readonly double[] standardFrequency = new double[26]
        {
            8.2, 1.5, 2.8, 4.3, 13, 2.2, 2, 6.1, 7, 0.15, 0.77, 4, 2.4,
            6.7, 7.5, 1.9, 0.095, 6, 6.3, 9.1, 2.8, 0.98, 2.4, 0.15, 2, 0.074
        };
        #endregion

        #region Properties
        /// <summary>
        /// Returns the total number of characters analyzed
        /// </summary>
        public uint TotalCharacters
        {
            get
            {
                uint temp = 0;
                foreach (uint value in characterCount)
                    temp += value;

                return temp;
            }
        }

        /// <summary>
        /// Returns a formatted string with the percentage frequency of a letter
        /// </summary>
        public string FormattedLetterFrequencies
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                double totalCharacters = Convert.ToDouble(TotalCharacters) / 100.0; //save the value so it's not recalculated each time
                for (byte x = 0; x < 26; x++)
                    sb.Append((char)(x + 'A')).Append(": ").Append(characterCount[x] / totalCharacters).Append("%\n");

                return sb.ToString();
            }
        }

        /// <summary>
        /// Returns the frequency of each letter as a percentage
        /// </summary>
        public IEnumerable<double> LetterFrequencies
        {
            get
            {
                double totalCharacters = Convert.ToDouble(TotalCharacters) / 100.0; //save the value so it's not recalculated each time
                foreach (uint value in characterCount)
                    yield return value / totalCharacters;
            }
        }

        /// <summary>
        /// Scores the frequency by how close it is to a standard english frequency
        /// </summary>
        public double DeviationFromStandardFrequency
        {
            get
            {
                double totalDifference = 0;
                double[] frequencies = LetterFrequencies.ToArray();
                for (int x = 0; x < 26; x++)
                    totalDifference += Math.Abs(standardFrequency[x] - frequencies[x]);

                return totalDifference / 26;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public StringAnalyzer() { }
        #endregion

        #region Instance Methods
        /// <summary>
        /// Analyzes the input and tallies the total of each character
        /// </summary>
        /// <param name="textIn">The input text to be analyzed</param>
        public void Analyze(IEnumerable<char> textIn)
        {
            foreach (char c in textIn)
            {
                if (Char.IsLetter(c))
                    characterCount[(byte)Char.ToUpper(c) - (byte)'A']++;
            }
        }

        /// <summary>
        /// Wrapper for Analyze(IEnumerable)
        /// </summary>
        /// <param name="textIn">The input text to be analyzed</param>
        public void Analyze(string textIn) => Analyze(textIn.AsEnumerable());

        /// <summary>
        /// Clears the counters for each character to reset the analyzer
        /// </summary>
        public void Clear()
        {
            characterCount = new uint[26];
        }
        #endregion
    }
}