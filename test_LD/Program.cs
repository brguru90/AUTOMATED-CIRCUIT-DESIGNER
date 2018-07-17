using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace test_LD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Log();
            Log.write_log("********AUTOMATIC CIRCUTE GENERATOR********\n");
            new embeded_file_extract("RosewoodStd-Regular.ttf", "RosewoodStd-Regular.ttf");
            new embeded_file_extract("Tribal Dragon.ttf", "Tribal Dragon.ttf");
            new object_property();
            Application.Run(new Form1());
        }
    }
}
