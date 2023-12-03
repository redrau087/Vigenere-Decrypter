/**
 * Simple letter frequency analyzer
 */

#include <iostream>
#include <fstream>
#include <string>

int main(int argc, char** argv) {
    if (argc != 2){
        std::cout << "Incorrect call. Run as \"programName fileToAnalyze\"";
        return 0;
    }
    std::string text, lineOfText, cleanText;
    std::ifstream inputFile;
    uint32_t letterFrequency[26] = {0};
    // Make sure to change the file name to the name of the file being analyzed
    inputFile.open(argv[1]);
    // Reading the file into text
    while (!inputFile.eof()) {
        getline(inputFile, lineOfText);
        text.append(lineOfText);
    }
    inputFile.close();
    char c;
    char a;
    for (int x = 0; x < text.length(); x++) {
        a = text[x];
        c = toupper(a);
        if (c >= 'A' and c <= 'Z') {
            cleanText.push_back(c);
            letterFrequency[c - 'A']++;
        }
    }
    // Print number of occurrences
    char j = 0;
    uint32_t i;
    for (int x = 0; x < 26; x++) {
        i = letterFrequency[x];
        std::cout << "Frequency of " << (char) ('A' + j) << " is " << i <<
                  std::endl; // / (double) cleanText.size()
        j++;
    }
    std::cout << "Total number of letters: " << cleanText.size() << std::endl <<
              std::endl << std::endl;
    // Print probability
    j = 0;
    for (auto i: letterFrequency) {
        std::cout << "P['" << (char) ('A' + j) << "']=" << i / (double)
                cleanText.size()
                  << std::endl; // / (double) cleanText.size()
        j++;
    }
    return 0;
}