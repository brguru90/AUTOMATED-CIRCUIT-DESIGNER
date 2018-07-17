using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace test_LD
{
    class object_property
    {
        public static bool test_mode = false;
        public static bool logs = false;
        public int x, y, width, height, width_adj = 0, height_adj = 0, x_adj = 0, y_adj = 0;
        public String label2 = "", label1="";
        public String[] ip_label = null;
        public int[] px_ip;
        public int[] px_op=new int[2];
        public static int panel_width=0;
        public int belongs = 0;
        public String gate_name = "";
        Object obj;
        public static int file_count = 0;
        public object_property()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\guru\\"+ "test.txt"))
            {
                Console.WriteLine("\n---------------TEST MODE---------------\n");
                object_property.test_mode = true;
            }
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\guru\\" + "do_log.txt"))
            {
                Console.WriteLine("\n---------------TEST MODE---------------\n");
                object_property.logs = true;
            }
        }
    }
}
