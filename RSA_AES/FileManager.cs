using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_AES
{
    class FileManager
    {
        public static string CreateArchive(string directory)
        {
            string oldpath = "";
            string newdir = "";
            if (File.Exists(directory))
            {
                newdir = Path.GetDirectoryName(directory) + "\\" + Hash.encHash.ToString();
                Directory.CreateDirectory(newdir);
                File.Copy(directory, newdir + "\\" + Path.GetFileName(directory));
                Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(Encoding.UTF8);
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                    oldpath = directory;
                    zip.AddDirectory(newdir);
                    directory = Path.GetDirectoryName(directory) + "\\" + Hash.encHash;
                    zip.Save(newdir + ".zip");
                }
            }
            File.Delete(oldpath);
            Directory.Delete(newdir, true);
            return directory;
        }
    }
}
