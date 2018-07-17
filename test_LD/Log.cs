using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace test_LD
{
    public class Log
    {
        public static String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\guru\\";    
        public Log()
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                String file_name="ckt_0";
                int i = 1;
                while (File.Exists(path + file_name + ".png"))
                {
                    file_name = "ckt_" + (i++);
                }
                object_property.file_count = i-1;
                path += file_name+".txt";
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+" Unable create file");
            }
        }
        public static void write_log(String log)
        {
            if (object_property.logs)
            {
                try
                {
                    FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(fs);
                    writer.Write(log + "\n");
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " Unable to log");
                }
            }
        }
    }
}
