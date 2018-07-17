using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace test_LD
{
    class divide_gates_for_row
    {

        public String gates = "";
        public divide_gates_for_row(String exp)
        {
            //adding @ symbol to AND gate to group gate with less than 3 input
            int n_row = exp.Split('+').Length;
            String val;
            foreach (String value in exp.Split('+'))
            {
                val = value;
                val = Regex.Replace(value, "[.][(]", "@(");
                if ((val.Split('.').Length) <= 3)
                    gates = add_str.add(gates, val, '+');
                else
                {
                    int n = val.Split('.').Length;
                    String[] temp_str = val.Split('.');
                    String temp = "", gates_temp = "";
                    int i = 0;
                    int m = n;
                    if (n % 3 != 0)
                        m -= n % 3;
                    while (i < m)
                    {
                        temp = "";
                        for (int j = i; j < i + 3; j++)
                        {
                            temp = add_str.add(temp, temp_str[j].ToString(), '.');
                        }
                        gates_temp = add_str.add(gates_temp, temp.ToString(), '@');
                        i += 3;
                    }
                    temp = "";
                    for (int j = m; j < n; j++)
                        temp = add_str.add(temp, temp_str[j].ToString(), '.');
                    gates_temp = add_str.add(gates_temp, temp.ToString(), '@');
                    gates = add_str.add(gates, gates_temp, '+');
                }
            }
         
        }
    }
}
