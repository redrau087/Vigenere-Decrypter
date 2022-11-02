# C++

### To Compile  
1. Using either clang++ or g++ (I will use clang++ for showing)
2. Compile each program
> clang++ -o callingFileName fileToCompile 
> Ex: clang++ -o decrypt Decrypt.cpp  

3. Run the files using ./callingFileName


### To Solve 
1. Use the split string to separate the ciphertext and use the frequency analyzer on ONE of the split string files to determine whether that key length is likely 
2. Once the key length is determined, use the frequency analysis on EACH split file to determine the key for each one (the distance between 'E' and the observed highest frequency letter) 
3. You can test the solved key by using the decrypt script 