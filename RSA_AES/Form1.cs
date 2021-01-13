using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA_AES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RSA.ReadRSA();
        }

        private void Encrypt(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog.FileName != "")
                    {
                        EncryptAES.Encrypt(FileManager.CreateArchive(openFileDialog.FileName));
                    }
                }
            }
        }

        private void RSAform(object sender, EventArgs e)
        {
            RSA rsa = new RSA();
            rsa.Show();
        }

        private void Decrypt(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog.FileName != "")
                    {
                        DecryptAES.Decrypt(Path.Combine(Path.GetDirectoryName(openFileDialog.FileName),Path.GetFileNameWithoutExtension(openFileDialog.FileName)));
                    }
                }
            }
        }
    }
}
