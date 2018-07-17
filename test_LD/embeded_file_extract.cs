using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

namespace test_LD
{
    class embeded_file_extract
    {
        private String from,to;
        public embeded_file_extract(String from, String to)
        {
            this.from = from;
            this.to = to;
            //extracting files embeded in exe
            WriteResourceToFile();
        }
        public void WriteResourceToFile()
        {
            try
            {
                String font_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\guru\\";
                to = font_path + to;
                if (!Directory.Exists(font_path))
                {
                    Directory.CreateDirectory(font_path);
                }
                System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream readStream = myAssembly.GetManifestResourceStream("test_LD." + from);
                FileStream writeStream = new FileStream(to, FileMode.Create, FileAccess.Write);
                readStream.CopyTo(writeStream);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load files to: "+to);
            }
        }       
    }
}
