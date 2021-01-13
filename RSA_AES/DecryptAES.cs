using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA_AES
{
    class DecryptAES
    {
        private static readonly byte[] saltBytes = new byte[] { 255, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

        // Decrypt string
        public static void Decrypt(string path)
        {
            FileStream fsCrypts = new FileStream(path + ".zip", FileMode.Create);
            byte[] cryptKey = (RSA.DectRSA(Path.GetFileNameWithoutExtension(path))).ToByteArray();
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.Padding = PaddingMode.PKCS7;
                AES.KeySize = 256;
                AES.BlockSize = 128;
                var key = new Rfc2898DeriveBytes(cryptKey, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;
                using (var css = new CryptoStream(fsCrypts, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    using (FileStream fsIns = new FileStream(path + ".aes", FileMode.Open))
                    {
                        byte[] buffer = new byte[10485760];
                        int read;
                        try
                        {
                            while ((read = fsIns.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                css.Write(buffer, 0, read);
                            }
                            fsIns.Close();
                        }
                        catch { }
                        finally
                        {
                            css.Close();
                            fsCrypts.Close();
                        }
                    }
                }
            }
            File.Delete(path + ".aes");
        }
    }
}
