using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA_AES
{
    class EncryptAES
    {
        private static readonly byte[] saltBytes = new byte[] { 255, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };
        private static readonly byte[] cryptKey = Hash.hash.ToByteArray();

        static public string Encrypt(string path)
        {
            FileStream fsCrypt = new FileStream(path + ".aes", FileMode.Create);
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.Padding = PaddingMode.PKCS7;
                AES.KeySize = 256;
                AES.BlockSize = 128;
                var key = new Rfc2898DeriveBytes(cryptKey, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;
                using (var cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (FileStream fsIn = new FileStream(path + ".zip", FileMode.Open))
                    {
                        byte[] buffer = new byte[10485760];
                        int read;
                        try
                        {
                            while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                cs.Write(buffer, 0, read);
                            }
                            fsIn.Close();
                        }
                        catch { }
                        finally
                        {
                            cs.Close();
                            fsCrypt.Close();
                        }
                    }
                }
            }
            File.Delete(path + ".zip");
            return path + ".aes";
        }
    }
}
