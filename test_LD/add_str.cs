using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test_LD
{
    public class add_str
    {
        public static String add(String to, String str, char sep = '.')
        {
            if (to == "" || to == null)
                to = str;
            else
                to += sep + str;
            return to;
        }
        public static String add(String to, String str, String sep)
        {
            if (to == "" || to == null)
                to = str;
            else
                to += sep + str;
            return to;
        }
        public static String reverse(String str)
        {
            String temp="";
            for (int i = str.Length - 1; i >= 0; i--)
                temp += str[i];
            return temp;
        }
        public static String remove_brace(String str)
        {
            String temp = "";
            for(int i=str.IndexOf("(")+1;i<str.IndexOf(")");i++)
                temp += str[i];
            return temp;
        }
    }
}
