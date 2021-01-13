# RSA_AES
This program is designed to encrypt and decrypt files.
During encryption, the key for AES encryption is generated randomly, then this key is encrypted on the RSA and added to the file name.
Decryption occurs as follows: using the RSA algorithm, a key is calculated from the file name, after which the file is decrypted on this key using AES.

--------------------------------
Эта программа предназначена для шифрования и дешифрования файлов. Во время шифрования ключ для шифрования AES генерируется случайным образом, затем этот ключ шифруется на RSA и добавляется к имени файла. Расшифровка происходит следующим образом: с помощью алгоритма RSA по имени файла вычисляется ключ, после чего файл расшифровывается по этому ключу с помощью AES.
