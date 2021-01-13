using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;
using System.IO;

namespace RSA_AES
{
    public partial class RSA : Form
    {
        private static BigInteger p;
        private static BigInteger q;
        public static BigInteger n;
        private static BigInteger f;
        public static BigInteger priv_key;
        public static BigInteger pub_key;
        private static List<BigInteger> pL = new List<BigInteger>();
        private static List<BigInteger> qL = new List<BigInteger>();
        private static string path = "rsa.txt";
        private static int seed;
        private static BigInteger N;

        public RSA()
        {
            InitializeComponent();
            ReadRSA();
            ValuesToForm();
        }

        private void GenerateButton(object sender, EventArgs e)
        {
            Reset();
            GenerateRSA();
            ValuesToForm();
            WriteValues();
        }

        public static void ReadRSA()
        {
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
                GenerateRSA();
                WriteValues();
            }
            else
            if (new FileInfo(path).Length == 0)
            {
                GenerateRSA();
                WriteValues();
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    p = BigInteger.Parse(streamReader.ReadLine());
                    q = BigInteger.Parse(streamReader.ReadLine());
                    n = BigInteger.Parse(streamReader.ReadLine());
                    f = BigInteger.Parse(streamReader.ReadLine());
                    priv_key = BigInteger.Parse(streamReader.ReadLine());
                    pub_key = BigInteger.Parse(streamReader.ReadLine());
                }
            }
        }

        private static void GenerateRSA()
        {

            Random rand = new Random();
            seed = rand.Next(1000000, 10000000);
            N = BigInteger.Multiply(seed, seed);
        gt:
            priv_key = 0;

            while (!IsPrime(p) || pL.Contains(p))
            {
                p = N;
                N++;
            }
            pL.Add(p);
            while (!IsPrime(q) || q == p || qL.Contains(q))
            {
                q = N;
                N++;
            }
            qL.Add(q);
            n = BigInteger.Multiply(p, q);
            f = BigInteger.Multiply(p - 1, q - 1);

            for (int i = 7; i < int.MaxValue; i += 2)
            {
                if (!IsPrime(i))
                    continue;
                if (IsDoublePrime(f, i))
                {
                    pub_key = i;
                    break;
                }
            }

            BigInteger w = BigInteger.Divide(f, pub_key) - 3;
            BigInteger count = w + 6;
            while (w < count)
            {
                if (BigInteger.Remainder(BigInteger.Multiply(w, pub_key), f) == 1)
                {
                    if (w == pub_key)
                        continue;
                    else
                    {
                        priv_key = w;
                        break;
                    }
                }
                w += 1;
            }

            if (priv_key == 0)
                goto gt;
        }

        private void ValuesToForm()
        {
            textBox1.Text = p.ToString();
            textBox2.Text = q.ToString();
            textBox3.Text = n.ToString();
            textBox4.Text = f.ToString();
            textBox5.Text = pub_key.ToString();
            textBox6.Text = priv_key.ToString();
        }

        private void Reset()
        {
            p = 0;
            q = 0;
            n = 0;
            f = 0;
            priv_key = 0;
            pub_key = 0;
            N = 0;
        }

        private static void WriteValues()
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine(p);
                streamWriter.WriteLine(q);
                streamWriter.WriteLine(n);
                streamWriter.WriteLine(f);
                streamWriter.WriteLine(priv_key);
                streamWriter.WriteLine(pub_key);
            }
        }

        public static BigInteger DectRSA(string encr)
        {
            BigInteger bi = BigInteger.Parse(encr);
            BigInteger decrypted = BigInteger.ModPow(bi, RSA.priv_key, RSA.n);
            return decrypted;
        }

        private static bool IsPrime(BigInteger dig)
        {
            if (BigInteger.Remainder(dig, 2) == 0 ||
                BigInteger.Remainder(dig, 3) == 0 ||
                BigInteger.Remainder(dig, 5) == 0 ||
                BigInteger.Remainder(dig, 7) == 0 ||
                BigInteger.Remainder(dig, 11) == 0)
            {
                //Console.WriteLine("Not prime " + dig);
                return false;
            }
            BigInteger sqrt = Sqrt(dig);
            if (BigInteger.Remainder(sqrt, 2) == 0)
                sqrt++;
            else sqrt += 2;
            while (sqrt > 2)
            {
                if (BigInteger.Remainder(dig, sqrt) == 0)
                {
                    //Console.WriteLine("Not prime " + dig);
                    return false;
                }
                sqrt -= 2;
            }
            //Console.WriteLine("Prime " + dig);
            return true;
        }

        private static bool IsDoublePrime(BigInteger fi, int dig)
        {
            if (BigInteger.Remainder(fi, dig) == 0)
                return false;
            else return true;
        }

        private static BigInteger Sqrt(BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }

    }
}
