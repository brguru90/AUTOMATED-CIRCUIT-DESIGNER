using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace test_LD
{
     
    class font_loader
    {
        public PrivateFontCollection private_fonts = new PrivateFontCollection();
        public font_loader()
        {
            LoadFont("Tribal Dragon.ttf");
            LoadFont("RosewoodStd-Regular.ttf");
        }
        private void LoadFont(String font)
        {
            // specify embedded resource name
            string resource = "test_LD." + font;
            // receive resource stream
            Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
            // create a buffer to read in to
            byte[] fontdata = new byte[fontStream.Length];
            // read the font data from the resource
            fontStream.Read(fontdata, 0, (int)fontStream.Length);
            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);
            // pass the font to the font collection
            private_fonts.AddMemoryFont(data, (int)fontStream.Length);
            // close the resource stream
            fontStream.Close();
            // free the unsafe memory
            Marshal.FreeCoTaskMem(data);
        }
    }
}
