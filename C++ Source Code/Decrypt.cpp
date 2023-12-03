#include <iostream>
#include <fstream>
#include <string>
// Make sure that the key is only uppercase letters
int main(int argc, char **argv) {
    if (argc != 3) {
        std::cout << "Incorrect call. Run as \"programName ciphertextFile decryptionKey\"";
	return 0;
    }
    std::string key(argv[2]);
    std::string text, lineOfText, cleanText, plainText;
    std::ifstream inputFile;
    inputFile.open(argv[1]);
    // Reading the file into text
    while (!inputFile.eof()) {
        getline(inputFile, lineOfText);
        text.append(lineOfText);
    }
    inputFile.close();
    int k = 0;
    char cc;
    for (int i = 0; i < text.size(); i++) {
        cc = 'A' + ((text.at(i) - 'A') - (key.at(k) - 'A') + 26) % 26;
        plainText.push_back(cc);
        k = (k + 1) % key.size();
    }
    std::ofstream outputFile;
    outputFile.open("decryptedText.txt");
    outputFile << plainText;
    outputFile.close();
    return 0;
}