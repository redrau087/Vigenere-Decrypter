# C++

> The c++ code uses a slightly modified version of the provided code along with SplitString.cpp which I wrote

### Install the compiler  
1. Open cmd as an admin  
2. Run the command  
> choco install mingw  
3. Agree and proceed to install GCC

### To Compile

1. Using g++ (in gcc)
2. Compile each program
> g++ -o callingFileName fileToCompile   
> Ex: g++ -o decrypt Decrypt.cpp  

3. Run the files using callingFileName
> Ex: g++ -o decrypt Decrypt.cpp  
> decrypt ciphertext.txt


### To Solve 
1. Use the split string to separate the ciphertext and use the frequency analyzer on ONE of the split string files to determine whether that key length is likely 
2. Once the key length is determined, use the frequency analysis on EACH split file to determine the key for each one (the distance between 'E' and the observed highest frequency letter) 
3. You can test the solved key by using the decrypt script 