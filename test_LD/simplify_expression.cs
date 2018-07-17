using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace test_LD
{
    public class simplify_expression
    {
        public String str="";
        public simplify_expression(String logical_exp)
        {
            foreach (String val in logical_exp.Split(','))
            {
                simplify(val);
            }
        }
        private void simplify(String exp)
        {
            String[] exp_arr=exp.Split('+');
            String temp_simpl_exp = "", simpl_exp = "";
            int[] visited = new int[exp_arr.Length];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = 0;
            for (int i = 0; i < exp_arr.Length; i++)
            {
                for (int j = 0; j < exp_arr.Length; j++)
                {
                    if (i == j || visited[i]==1 || visited[j]==1)
                        continue;
                    String[] term1 = exp_arr[i].Split('.');
                    String[] term2 = exp_arr[j].Split('.');
                    String same = "";
                    //i don't know how to simplify xor with more than 3 input :(
                    if (term1.Length >= 4 || term2.Length >= 4)
                        continue;
                    for (int m = 0; m < term1.Length; m++)
                    {
                        for (int n = 0; n < term2.Length; n++)
                        {
                            if (term1[m].Equals(term2[n]))
                            {
                                if (same == "")
                                    same = term1[m];
                                else
                                    same += "." + term1[m];

                            }
                        }
                    }
                    if (same == "")
                        goto no_siml;
                    String exp_arr_i = exp_arr[i];
                    String exp_arr_j = exp_arr[j];
                    foreach (String val in same.Split('.'))
                    {
                        exp_arr_i = Regex.Replace(exp_arr_i, val + ".", "");
                        exp_arr_j = Regex.Replace(exp_arr_j, val + ".", "");
                    }
                    if (!(exp_arr_i.Split('.').Length == 2 && exp_arr_j.Split('.').Length == 2))
                        goto no_siml;
                    int count = 0;
                    String exp_arr_xor1 = "", exp_arr_xor2="";
                    foreach(String val1 in exp_arr_i.Split('.'))
                    {
                        foreach (String val2 in exp_arr_j.Split('.'))
                        {
                            if (Regex.IsMatch(val1.ToString(), val2 + "'") || Regex.IsMatch(val2.ToString(), val1 + "'"))
                            {
                                exp_arr_xor1 = add_str.add(exp_arr_xor1, val1);
                                exp_arr_xor2 = add_str.add(exp_arr_xor2, val2);
                                count++;
                            }
                        }
                    }
                    //A'B'+AB
                    if (Regex.IsMatch(exp_arr_xor1, "^[A-Z]'.[A-Z]'$") && Regex.IsMatch(exp_arr_xor2, "^[A-Z].[A-Z]$"))
                    {
                        temp_simpl_exp = add_str.add(temp_simpl_exp, same + ".(" + exp_arr_xor2.Replace('.', 'ʘ') + ")'", '+');
                        visited[i] = 1;
                        visited[j] = 1;
                        i=j;
                        goto next;

                    }
                    else
                    //A'B+AB'
                    if (Regex.IsMatch(exp_arr_xor1, "^[A-Z]'.[A-Z]$") && Regex.IsMatch(exp_arr_xor2, "^[A-Z].[A-Z]'$"))
                    {
                        temp_simpl_exp = add_str.add(temp_simpl_exp, same + ".(" + exp_arr_xor1.Split('.')[1] + "ʘ" + exp_arr_xor2.Split('.')[0] + ")", '+');
                        visited[i] = 1;
                        visited[j] = 1;
                        i=j;
                        goto next;
                    }
                    else
                        goto no_siml;
                    no_siml:
                        {
                            if (j == exp_arr.Length - 1)
                                temp_simpl_exp=add_str.add(temp_simpl_exp, exp_arr[i], '+');

                            if (i == exp_arr.Length - 2)
                                temp_simpl_exp = add_str.add(temp_simpl_exp, exp_arr[j], '+');
                        }
                }
                    
                next:{}
            }
            string temp_str="";
            int ii;
            for (ii = 0; ii < visited.Length; ii++)
            {
                if (visited[ii] != 0)
                    break;
                temp_str = add_str.add(temp_str, exp_arr[ii].ToString(), '+');
            }
            if (ii == visited.Length)
            {
                str = add_str.add(str, temp_str, ',');
                goto last;
            }
            IDictionary<String, bool> v = new Dictionary<String, bool>();
            foreach (String val in temp_simpl_exp.Split('+'))
                v[val] = false;
            if (temp_simpl_exp.Split('+').Length > 1)
                foreach (String val1 in temp_simpl_exp.Split('+'))
                {
                    foreach (String val2 in temp_simpl_exp.Split('+'))
                    {
                        if (val1 == val2 || v[val1] == true || v[val2] == true)
                            continue;

                        if (Regex.IsMatch(val1.Split('.')[0].ToString() + "'", val2.Split('.')[0].ToString()) || Regex.IsMatch(val2.Split('.')[0].ToString() + "'", val1.Split('.')[0].ToString()))
                        {
                            if (Regex.IsMatch(add_str.remove_brace(val1.Split('.')[1].ToString()), add_str.remove_brace(val2.Split('.')[1].ToString())) || Regex.IsMatch(add_str.reverse(add_str.remove_brace(val2.Split('.')[1].ToString())), add_str.remove_brace(val1.Split('.')[1].ToString())))
                            {
                                v[val1] = true;
                                v[val2] = true;
                                simpl_exp = add_str.add(simpl_exp, val1.Split('.')[0].ToString()[0].ToString() + 'ʘ' + add_str.remove_brace(val2.Split('.')[1].ToString()), '+');
                            }
                        }
                    }
                }
            else
                simpl_exp = temp_simpl_exp;
            Console.WriteLine(temp_simpl_exp);
            str = add_str.add(str, simpl_exp, ',');
           
            last:
            {

                String str_xor = "";
                v = new Dictionary<String, bool>();
                foreach (String val in str.Split('+'))
                    v[val] = false;
                foreach(String val1 in str.Split('+'))
                {
                    foreach(String val2 in str.Split('+'))
                    {
                        if (val1 == val2 || v[val1] == true || v[val2] == true)
                            continue;
                        if ((Regex.IsMatch(val1, "^[A-Z]'.[A-Z]$") && Regex.IsMatch(val2, "^[A-Z].[A-Z]'$")) || (Regex.IsMatch(val2, "^[A-Z]'.[A-Z]$") && Regex.IsMatch(val1, "^[A-Z].[A-Z]'$")))
                        {
                            v[val1] = true;
                            v[val2] = true;
                            str_xor = add_str.add(str_xor, val1.Split('.')[1] + "ʘ" + val2.Split('.')[0], '+');
                        }
                    }
                }
                foreach (String val in str.Split('+'))
                {
                    if(v[val]==false)
                    {
                        str_xor = add_str.add(str_xor, val, '+');
                    }
                }
                
                str=str_xor;
            }
            Console.WriteLine(str);
        }
    }
}
