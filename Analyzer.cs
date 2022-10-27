using System;
using System.Collections.Generic;
using System.Linq;

namespace VigenereDecryptionTools
{
    /// <summary>
    /// Uses an unsigned integer array of length 26 to hold the total number of occurrences for each letter. This can be used for frequency analysis
    /// </summary>
    public class StringAnalyzer
    {
        #region Private Variables
        private protected uint[] characterCount = new uint[26];
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
        /// Returns the character that has appeared the most in the analysis
        /// </summary>
        public char HighestProbabilityCharacter
        {
            get
            {
                byte index = 0;
                for (byte x = 1; x < 26; x++)
                    if (characterCount[x] > characterCount[index])
                        index = x;
                return (char)('A' + index);
            }
        }

        /// <summary>
        /// Returns the frequency of the most common character as a percentage
        /// </summary>
        public double HighestProbability
        {
            get
            {
                return characterCount.Max() / (Convert.ToDouble(TotalCharacters) / 100.0);
            }
        }

        /// <summary>
        /// Returns the frequency of the least common character as a percentage
        /// </summary>
        public double LowestProbability
        {
            get
            {
                return characterCount.Min() / (Convert.ToDouble(TotalCharacters) / 100.0);
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