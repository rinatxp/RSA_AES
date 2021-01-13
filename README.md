# RSA_AES
This program uses RSA and AES encryption
This program is designed to encrypt and decrypt files.
During encryption, the key for AES encryption is generated randomly, then this key is encrypted on the RSA and added to the file name.
Decryption occurs as follows: using the RSA algorithm, a key is calculated from the file name, after which the file is decrypted on this key using AES.
