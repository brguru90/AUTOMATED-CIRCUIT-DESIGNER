using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test_LD
{
    public class characters
    {
        public char[] letter = new char[52];
        public characters()
        {
            int ch = 0;
            int i = 65;
            for(i=65;i<=122;i++)
            {
                letter[ch] = Convert.ToChar(i);
                ch++;
                if (i == 90)
                    i = 96;
            }
            
        }
    }
}
