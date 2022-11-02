#include <iostream>
#include <fstream>
#include <string>
#include <cstdlib>

int main(int argc, char** argv) {
    if (argc != 3){
        std::cout << "Incorrect call. Run as \"programName ciphertextFile keyLength\"";
	return 0;
    }

    int keyLength = atoi(argv[2]);

    std::ifstream cipherTextFile;
    cipherTextFile.open(argv[1]);
    std::string cipherText((std::istreambuf_iterator<char>(cipherTextFile)),
                    std::istreambuf_iterator<char>());

    std::ofstream outputFiles[keyLength];
    std::string fileName = "splitCipherText";
    std::string currentName;
    for (int x = 0; x < keyLength; x++){
        currentName = fileName + std::to_string(x) + ".txt";
        outputFiles[x].open(currentName);
    }

    for (int x = 0; x < cipherText.length(); x++)
        outputFiles[x % keyLength] << cipherText[x];

    for (int x = 0; x < keyLength; x++)
        outputFiles[x].close();

    std::cout << "Split files have been written";
    return 0;
}
