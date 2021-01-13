using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA_AES
{
    class Hash
    {
        public static BigInteger hash = BigInteger.Parse(GenerateRandomData());
        public static string encHash = BigInteger.ModPow(hash,RSA.pub_key,RSA.n).ToString();

        public static string GenerateRandomData()
        {
            string datenow = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
            DateTime d = DateTime.Parse(datenow);
            string number = ((long)((DateTimeOffset)d).Ticks).ToString();
            string data = $"{number}";
            using (MD5 hash = MD5.Create())
            {
                string full = string.Join
                (
                    "",
                    from ba in hash.ComputeHash
                    (
                        Encoding.UTF8.GetBytes(data)
                    )
                    select ba.ToString()
                );

                return full.Substring(1, 20);
            }
        }
    }
}
