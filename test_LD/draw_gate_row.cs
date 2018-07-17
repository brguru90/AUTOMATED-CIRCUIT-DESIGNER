using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace test_LD
{
    class draw_gate_row
    {
        Form2 f;
        String gates = "";
        int inner_gate_max_x_axis;
        object_property[] px_row = draw_ip_lines.px_row;
        public int nor_of_row_taken = 0;
        public static List<gate> list = new List<gate>();
        public int right_pad=0;
        int max_gate;
        public draw_gate_row(Form2 f, System.Drawing.Graphics g, int zoom, object_property std_img_size, System.Reflection.Assembly myAssembly, int nor_of_rows, int n_ip, String exp, int cur_row)
        {
            this.f = f;
            //getting grouped gates seperated by @ symbol
            divide_gates_for_row dgfr = new divide_gates_for_row(exp);
            gates = dgfr.gates;
            int max_row = 0;
            int i, j;
            i = j = 0;
            //inner_gate_max_x_axis = igad.inner_gate_max_x_axis;
            int cur_row2 = 0;
            cur_row2 += cur_row;

            int max_level = 0;
            gate_mat[,] gm_or = null;
            int nor_of_row_taken_or = gates.Split('+').Length;
            int col_or = 0, row_or = nor_of_row_taken_or;
            while (row_or != 2 && row_or != 3 && nor_of_row_taken_or != 1)
            {
                row_or = (row_or / 3) + (row_or % 3);
                col_or++;
            }
            col_or += 2;
            gm_or = new gate_mat[col_or, nor_of_row_taken_or];
            String temp_or_str = "";
            String[] temp_or = new String[col_or + 1];

            //drawing gates
            gate_mat[,] gm = null;
            int belongs = 0;
            //spliting each gate
            foreach (String value in gates.Split('+'))
            {
                String temp_str = "", temp_str2="",val = "";
                int completed = 0;
                val = value;
                if(gates.Split('+').Length!=1)
                if (value[value.Length - 1] == '@')
                    val = value.Remove(value.Length - 1, 1);
                //simlifing gate that has more than 3 inputs
                int nor_of_row_taken2 = val.Split('@').Length;
                int col = 0, row = nor_of_row_taken2;
                while (row != 2 && row != 3 && nor_of_row_taken2!=1)
                {
                    row = (row / 3) + (row % 3);
                    col++;
                }
                col+=2;
                gm = new gate_mat[col,nor_of_row_taken2];
                String[] temp = new String[col+1];
                foreach (String v in val.Split('@'))
                {
                    if (v == null)
                        continue;
                    temp[0] = add_str.add(temp[0],v,"_1");
                }
                String[] str = val.Split('@');
                int len = str.Length;
                j = 0;
                for (i = 0; i < len; i++)
                {
                    if (Regex.IsMatch(str[i], "[(].+[)]"))
                        str[i] = str[i].Substring(1, str[i].Length - 2);
                    gm[j, i] = new gate_mat();
                    gm[j, i].cur = str[i];
                    gm[j, i].gate =str[i];
                    gm[j, i].level = 0;
                    foreach(String s in str[i].Split('.'))
                        gm[j, i].from.Add(s);
                    foreach (String s in str[i].Split('ʘ'))
                        gm[j, i].from.Add(s);
                }
                max_gate = 1;
                str = null;
                int k = 0;
                if(len!=1)
                for (j = 1; j <col ; j++)
                {
                    k = 0;
                    gm[j, k] = new gate_mat();
                    str = Regex.Split(temp[j - 1], "_" + j);
                    len = str.Length;
                    temp[j] = temp_str = temp_str2 = "";
                    if(len!=1)
                    for (i = 0; i <len ; i++)
                    {
                        if ((i+1) % 3 == 0 || (len % 3 != 0 && i == len - 1))
                        {
                            temp_str = add_str.add(temp_str, str[i], "_" + j);
                            temp_str2 += "(" + str[i] + ")";
                            temp[j] = add_str.add(temp[j], temp_str, "_" + (j+1));
                            gm[j, k].from.Add(str[i]);
                            //foreach (String ss in gm[j, k].from)
                               // f.richTextBox1.AppendText("\n--from" + ss + "\n");
                            gm[j, k].cur = temp_str;
                            gm[j, k].gate = temp_str2;
                            temp_str=temp_str2="";
                            gm[j, k].level = j;
                            k++;
                            gm[j, k] = new gate_mat();
                            max_gate++;
                        }
                        else
                        {
                            gm[j, k].from.Add(str[i]);
                            temp_str = add_str.add(temp_str, str[i], "_" + j);
                            temp_str2 += "(" + str[i] + ")";
                        }

                        max_row++;
                    }
                    if (len == 1)
                        break;
                }
                
                f.richTextBox1.AppendText("\n");
                String last_gate = "";
                for (i = 0; i <col; i++)
                {
                    for (j = 0; j < nor_of_row_taken2; j++)
                    {
                        if (gm[i, j] == null || gm[i, j].cur=="")
                            continue;
                        Log.write_log("-inner gates-" + "[" + j + "][" + i + "]" + gm[i, j].cur);
                        new identify_gate_and_draw(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gm[i, j].gate, j, i, cur_row2, (max_row+gates.Split('+').Length), ++completed, gm[i, j].from, gm[i, j].cur, gm[i, j].level, belongs);
                        if (gm[i, j].level > max_level)
                            max_level = gm[i, j].level;
                        last_gate = gm[i, j].cur;
                        f.richTextBox1.AppendText("\n");
                    }
                }
                cur_row2 += nor_of_row_taken2;
                belongs++;

                temp_or_str = add_str.add(temp_or_str, last_gate, '+');
            }

            nor_of_row_taken = cur_row2;
            //simlifing OR gate that has more than 3 inputs
            max_gate = 0;
            string replace = "__1";
            temp_or_str = Regex.Replace(temp_or_str, "[+]", replace);
            temp_or[0] = temp_or_str;
            String[] str_or = Regex.Split(temp_or_str, replace);
            int len2 = str_or.Length;
            str_or = null;
            String temp_str_or ="", temp_str2_or = "";
            int kk = 0;
            if (len2 != 1)
                for (j = 1; j < col_or; j++)
                {
                    kk = 0;
                    gm_or[j, kk] = new gate_mat();
                    str_or = Regex.Split(temp_or[j - 1], "__" + j);
                    len2 = str_or.Length;
                    temp_or[j] = temp_str_or = temp_str2_or = "";
                    for (i = 0; i < len2; i++)
                    {
                        if ((i + 1) % 3 == 0 || (len2%3!=0 && i == len2 - 1))
                        {
                            temp_str_or = add_str.add(temp_str_or, str_or[i], "__" + j);
                            temp_str2_or = add_str.add(temp_str2_or, "(" + str_or[i] + ")", '+');
                            temp_or[j] = add_str.add(temp_or[j], temp_str_or, "__" + (j + 1));
                            gm_or[j, kk].from.Add(str_or[i]);
                            gm_or[j, kk].cur = temp_str_or;
                            gm_or[j, kk].gate = temp_str2_or;
                            temp_str_or = temp_str2_or = "";
                            gm_or[j, kk].level = j;
                            kk++;
                            gm_or[j, kk] = new gate_mat();
                            max_gate++;
                        }
                        else
                        {
                            gm_or[j, kk].from.Add(str_or[i]);
                            temp_str_or = add_str.add(temp_str_or, str_or[i], "__" + j);
                            temp_str2_or = add_str.add(temp_str2_or, "(" + str_or[i] + ")", '+');
                        }
                        
                    }
                    if (len2 == 1)
                        break;
                }

            //drawing OR gate
            int completed2 = 0;
            for (i = 0; i < col_or; i++)
            {
                for (j = 0; j < nor_of_row_taken_or; j++)
                {
                    if (gm_or[i, j] == null || gm_or[i, j].cur == "")
                        continue;

                    Log.write_log("-outer gates-" + "[" + j + "][" + i + "]" + gm_or[i, j].cur);
                    new identify_gate_and_draw(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gm_or[i, j].gate, j, i + max_level, cur_row, max_gate, ++completed2, gm_or[i, j].from, gm_or[i, j].cur, gm_or[i, j].level + max_level, belongs, max_level,true);
                }
            }
            //connecting respective gates
            foreach (gate gt1 in draw_gate_row.list)
            {
                foreach (gate gt2 in draw_gate_row.list)
                {


                    if (gt1.px_gate.label1 == gt2.px_gate.label1 || gt1.level > gt2.level)
                        continue;
                    if (gt2.px_gate.gate_name == "or" && (gt1.or_gate_pos == -1 && gt2.or_gate_pos > 1))
                        continue;
                    if (gt2.px_gate.gate_name != "or" && (Math.Abs(gt2.level - gt1.level) > 1))
                        continue;
                    if (gt2.px_gate.gate_name != "or")
                        if ((Math.Abs(gt2.level - gt1.level) > 1 && (gt2.px_gate.gate_name != "direct" ^ gt1.px_gate.gate_name != "direct")) || (gt2.px_gate.gate_name != "direct" && gt1.px_gate.belongs != gt2.px_gate.belongs))
                            continue;
                    if (gt1.px_gate.gate_name == "direct" && gt2.px_gate.gate_name == "direct" && gt1.px_gate.belongs != gt2.px_gate.belongs)
                        continue;
                    try
                    {
                        if (Regex.IsMatch(gt1.px_gate.label2, "^" + gt2.px_gate.ip_label[0] + "$"))
                            new draw_elbow(f, g, gt1.px_gate.px_op[0], gt1.px_gate.px_op[1], gt2.px_gate.x, gt2.px_gate.px_ip[0]);
                    }
                    catch (Exception) { }
                    try{
                        if (Regex.IsMatch(gt1.px_gate.label2, "^" + gt2.px_gate.ip_label[1] + "$"))
                            new draw_elbow(f, g, gt1.px_gate.px_op[0], gt1.px_gate.px_op[1], gt2.px_gate.x, gt2.px_gate.px_ip[1]);
                    }
                    catch (Exception) { }
                    try
                    {
                        if (Regex.IsMatch(gt1.px_gate.label2, "^" + gt2.px_gate.ip_label[2] + "$"))
                            new draw_elbow(f, g, gt1.px_gate.px_op[0], gt1.px_gate.px_op[1], gt2.px_gate.x, gt2.px_gate.px_ip[2]);                        
                    }
                    catch (Exception) {}
                }
            }
        }
    }
}
