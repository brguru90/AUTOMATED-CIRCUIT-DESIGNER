using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;

namespace test_LD
{

    public partial class Form2 : Form
    {

        System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["Form1"];
        public Form2()
        {
            InitializeComponent();
            this.MouseWheel += Form2_MouseWheel;
            this.panel1.MouseWheel += Form2_MouseWheel;
            // this.DoubleBuffered = true;

            this.SetStyle(
             ControlStyles.UserPaint |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.OptimizedDoubleBuffer, true);
            if (object_property.test_mode)
            {
                label3.Visible = true;
                richTextBox1.Visible = true;
                richTextBox1.Enabled = true;
            }
            else
            {
                label3.Visible = false;
                richTextBox1.Visible = false;
                richTextBox1.Enabled = false;
            }
        }

        public String logical_exp=null;
        private System.Windows.Forms.TextBox[] minterm;
        int n_ip, n_op;
        bool bt_flag = false;
        IDictionary<String, String> q = new Dictionary<String, String>();
        String[] minimized;
        IList<String> remaining = new List<String>();
        public char[] letter = new characters().letter;
        public int[] sort_asc(int[] Array)
        {
            int temp;

            for (int i = 0; i < Array.Length - 1; i++)
            {

                for (int j = i + 1; j < Array.Length; j++)
                {
                    if (Array[i] > Array[j])
                    {

                        temp = Array[i];
                        Array[i] = Array[j];
                        Array[j] = temp;

                    }

                }

            }
            return Array;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            n_ip = Form1.n_ip;
            n_op = Form1.n_op;
            minterm = Form1.minterm;
            Log.write_log("No of ip:" + n_ip + "\nno of op" + n_op + "\n");
            String[] op_row;
            String[,] min_order = new String[n_op, n_ip + 1];
            String[] min_order2 = new String[n_op];
            char[] res_arr;
            int sum = 0;
            for (int j = 0; j < n_op; j++)
            {
                if (minterm[j].Text != "")
                {
                    Log.write_log("Minterm: " + minterm[j].Text + "\n");
                    //preparations for QM method
                    String[] minterm_asc = minterm[j].Text.Split(',');
                    int[] bc = new int[minterm_asc.Length];
                    int ind=0;
                    foreach (String sval in minterm_asc)
                        bc[ind++] = Int32.Parse(sval);
                    bc=sort_asc(bc);
                    ind=0;
                    foreach (int ival in bc)
                        minterm_asc[ind++] = ival.ToString();
                    op_row = minterm_asc;
                    foreach (String val in op_row)
                    {
                        sum = 0;
                        //decimal to binary convertion
                        String result = Convert.ToString(Int32.Parse(val), 2);
                        //adding zeroes to empty fields
                        result = String.Format("{0:D" + n_ip + "}", Int32.Parse(result));
                        res_arr = result.ToCharArray();
                        //detecting number of 1's in binary
                        for (int n = 0; n < res_arr.Length; n++)
                        {
                            sum += Int32.Parse(res_arr[n].ToString());
                        }
                        if (min_order[j, sum] == null)
                            min_order[j, sum] = result;
                        else
                            min_order[j, sum] += "," + result;
                        Log.write_log("j="+j+" sum="+sum+"=="+min_order[j, sum]);
                    }
                    //ordering binary accoundings to sum of 1's in it
                    for (int i = 0; i <= min_order.Length; i++)
                    {
                        String[] temps = null;
                        try
                        {
                            temps = min_order[j, i].Split(',');
                            foreach (String val2 in temps)
                            {
                                if (min_order2[j] == null)
                                    min_order2[j] = val2;
                                else
                                    min_order2[j] += "," + val2;
                            }
                        }
                        catch (Exception) { }

                    }
                }

            }



//----------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------CALCULATING PRIME IMPLICANTS BY QM METHOD-----------------------------------------------------------
            Log.write_log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            for (int j = 0; j < n_op; j++)
            {
                Log.write_log("------------" + j + "-------------");
                String[] temps = min_order2[j].Split(',');
                for (int i = 0; i < temps.Length; i++)
                    try
                    {
                        Log.write_log(temps[i]);
                    }
                    catch (Exception) { }
                Log.write_log("---------------------------calculation data---------------------------------------");
                //--------Start of actual QM Method----------
                compare_str(temps, false);
                //--------End of actual QM Method----------
                remaining=remaining.Distinct().ToList();
                foreach (KeyValuePair<string, string> val in q)
                {
                    Log.write_log(val.Key + "==>" + val.Value);
                }
                Log.write_log("---------------------------finally---------------------------------------");
                try
                {
                    for (int m = 0; m < remaining.Count; m++)
                        Log.write_log("Remaining " + remaining[m]);
                }
                 
                catch (Exception) { }

                string temp_exp = generate_exp(minterm[j].Text);
                if (label1.Text == "")
                    label1.Text = temp_exp;
                else
                    label1.Text += " , " + temp_exp;

                temp_exp = generate_exp(minterm[j].Text);
                if (logical_exp == "" || logical_exp == null)
                    logical_exp = temp_exp;
                else
                    logical_exp += "," + temp_exp;
                remaining = new List<String>();
            }
            Log.write_log("---------------------------Expressions---------------------------------------");
            Log.write_log(logical_exp);
            //--------------------------------------------------------------------------------------------------------
            //------------------------------------------END-----------------------------------------------------------

        }
//--------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------FINCTION END-------------------------------------------------------




        //------------------------COMPARE STRING--------------------------------------------
        private void compare_str(String[] str, bool flag)
        {
            IList<String> temp_arr = new List<String>();
            String[] test = new String[2];
            int c = 0;
            if (flag == false)
                for (int i = 0; i < str.Length; i++)
                {
                    try
                    {
                        q.Add(str[i], Convert.ToInt32(str[i], 2).ToString());
                    }
                    catch (Exception) { }
                }
            int ss = 0;
            bool flag2 = false;
            for (int s = 0; s < str.Length; s++)
                ss += s;
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                flag2 = false;
                for (int j = i + 1; j < str.Length; j++)
                {
                    if (i == j)
                        continue;
                    try
                    {
                        test = compare_bits(str[i], str[j]);
                        temp_arr.Add(test[1]);
                        c++;
                        flag2 = true;
                        //Console.WriteLine(test[0] + "-" + test[1]);
                    }
                    catch (Exception) { count++; }
                }
                if (flag2 == false)
                {
                    foreach (KeyValuePair<string, string> val in q)
                    {
                        String[] temp_str;
                        if (val.Value.ToString().Split(',').Length > 2)
                            temp_str = val.Value.Split('-');
                        else
                            temp_str = val.Value.Split(',');
                        if (temp_str.Length == 2)
                        {
                            if (q[str[i]].ToString().Equals(temp_str[1].ToString()))
                            {
                                flag2 = true;
                                break;
                            }
                        }
                    }
                    if (flag2 == false)
                        remaining.Add(str[i] + " " + q[str[i]]);
                }
            }
            if (ss == count)
                return;
            String[] temp_arr2 = new String[c];
            temp_arr2 = temp_arr.ToArray();
            minimized = temp_arr.ToArray();
            compare_str(temp_arr2, true);
        }


        //------------------------COMPARE BITS--------------------------------------------
        private String[] compare_bits(String bits1, String bits2)//EX 0000 11111
        {
            String[] match = new String[2]; ;
            int count = 0, i, j, place = 0;
            char[] bin1 = bits1.ToCharArray();
            char[] bin2 = bits2.ToCharArray();
            for (i = 0, j = 0; i < bin1.Length && j < bin2.Length; i++, j++)
            {
                if (!bin1[i].Equals(bin2[j]))
                {
                    count++;
                    place = i;
                }
            }
            if (count == 1)
            {
                if (q[bits1].ToString().Split(',').Length >= 2 || q[bits2].ToString().Split(',').Length >= 2)
                    match[0] = q[bits1] + "-" + q[bits2];
                else
                    match[0] = q[bits1] + "," + q[bits2];
                string somestring = bits1;
                StringBuilder sb = new StringBuilder(somestring);
                try
                {
                    sb[place] = '_';
                }
                catch (Exception) { }
                match[1] = sb.ToString();
                String word_2 = "";
                try
                {
                    String word = match[1].ToString();
                    if (word.Length > 1)
                        foreach (KeyValuePair<string, string> val in q)
                        {
                            if (Regex.IsMatch(val.Key, @"^" + word))
                            {
                                word_2 = val.Key;
                            }
                        }
                }
                catch (Exception) { }
                if(word_2!="")
                {
                    int rep = 0;
                    try
                    {
                        rep = Int32.Parse(word_2.Split('@')[1]);
                    }
                    catch (Exception) { }
                    rep++;
                    q.Add(word_2.Split('@')[0]+"@"+rep, match[0]);
                }
                else
                    q.Add(match[1], match[0]);
            }
            else
                match = null;
            return match;
        }
        struct myExp
        {
            public int count;
            public String str;
            public int[] index;
        };
        private String generate_exp(String minterm)
        {
            String logical_expression = null;
            n_op = Int32.Parse(Form1.n_op.ToString());
            int[] rep = new int[Int32.Parse(Math.Pow(2, n_ip).ToString())];
            String[] op_row = minterm.Split(',');
            for (int i = 0; i < op_row.Length; i++)
                rep[Int32.Parse(op_row[i])] = 0;
            for (int m = 0; m < remaining.Count; m++)
            {
                String temp = remaining[m];
                String[] num = temp.Split(' ');
                temp =num[1].Replace('-',',');
                num = temp.Split(',');
                for (int i = 0; i < num.Length; i++)
                    rep[Int32.Parse(num[i])]++;                
            }
            for (int i = 0; i < rep.Length; i++)
            {
                Log.write_log(i + "-" + rep[i] + " ");
            }
            String[] temp_impl = new String[Int32.Parse(Math.Pow(2, n_ip).ToString())];
            String ttemp = "";
            List<String> reguler = new List<String>();
            for (int m = 0; m < remaining.Count; m++)
            {
                String temp = remaining[m];
                String[] num = temp.Split(' ');
                temp = num[1].Replace('-', ',');
                num = temp.Split(',');
                for (int i = 0; i < num.Length; i++)
                {
                    if (rep[Int32.Parse(num[i])] == 1)
                    {
                        Log.write_log("\n" + temp + "-" + remaining[m]);
                        if (logical_expression == null)
                            logical_expression = conv_alpa_exp(remaining[m].Split(' ')[0]);
                        else
                            logical_expression += "+" + conv_alpa_exp(remaining[m].Split(' ')[0]);
                        //add single column marked number
                        if (ttemp == "")
                            ttemp = temp;
                        else
                            ttemp += "," + temp;
                        break;
                    }
                    else
                        reguler.Add(remaining[m]);
                }
               
            }
            for (int i = 0; i < rep.Length; i++)
            {
                if(rep[i]==0)
                    if (ttemp == "")
                        ttemp = i+"";
                    else
                        ttemp += "," + i;
            }
            Log.write_log("\n" + ttemp);
            String[] vis = ttemp.Split(',');   
            List<String> unvis = new List<String>();  
                for (int j = 0; j < Int32.Parse(Math.Pow(2, n_ip).ToString()); j++)
                {
                    Match match1=null;
                    for (int n = 0; n < vis.Length; n++)
                    {
                        match1 = Regex.Match(vis[n], @"" + j, RegexOptions.IgnoreCase);
                        if (match1.Success) goto next;
                    }
                    unvis.Add(j + "");
                    next: { }
                }
            Log.write_log("\nunvisited:" + string.Join(",", unvis));
            reguler=reguler.Distinct().ToList();
            Log.write_log("\nreguler:" + string.Join(" ", reguler));

            myExp[] myexp = new myExp[vis.Length];
            List<int> count = new  List<
                int>();
            int ii = 0;
            var dict = new Dictionary<string, int>();
            var done = new Dictionary<string, int>();
            foreach (String rexp in reguler)
            {
                dict[rexp] = 0;
                foreach (String unv in unvis)
                {
                    String[] rexp_ar = rexp.Split(' ')[1].Split(',');
                    foreach(String myval in rexp_ar)
                    {
                        if(myval.Equals(unv))
                        {
                            dict[rexp]++;
                            Log.write_log("match--" + rexp + " count=" + dict[rexp]);
                        }
                    }
                }
            }
            var dctTemp = new Dictionary<String, int>();

            foreach (KeyValuePair<String, int> pair in dict.OrderByDescending(key => key.Value))
            {

                int c1 = unvis.Capacity;
                int c2 = 0;
                foreach (String vvv in pair.Key.ToString().Split(' ')[1].Replace("-", ",").Split(','))
                {
                    foreach (String vv in unvis)
                        if (vvv.Equals(vv))
                            c2++;
                }
                if (c2 < 1)
                    continue;
                dctTemp.Add(pair.Key, pair.Value);
                Log.write_log(pair.Key + "-" + pair.Value);
            }
            foreach (KeyValuePair<String, int> pair in dctTemp)
            {
                if (!done.ContainsKey(pair.Key.ToString().Split(' ')[1].Replace("-",",")))
                    done[pair.Key.ToString().Split(' ')[1].Replace("-", ",")] = 0;
                if (done[pair.Key.ToString().Split(' ')[1].Replace("-", ",")] == 1)
                    continue;
                String[] my_val = pair.Key.ToString().Split(' ')[1].Replace("-", ",").Split(',');
                    done[pair.Key.ToString().Split(' ')[1]] = 1;
                    foreach (String vvv in my_val)
                    {
                        if (!done.ContainsKey(vvv))
                            done[vvv] = 0;
                        if (done[vvv] == 1)
                            goto next2;
                        done[vvv] = 1;
                    }
                    if (logical_expression == null)
                        logical_expression = conv_alpa_exp(pair.Key.Split(' ')[0]);
                    else
                        logical_expression += "+" + conv_alpa_exp(pair.Key.Split(' ')[0]);
                    Log.write_log("\n\ncount--" + pair.Key + "--" + pair.Value);
                next2: { }
            }
            return logical_expression;
        }
        //converting binary to letters/character expression
        private String conv_alpa_exp(String exp)
        {
            char[] ch = exp.ToCharArray();
            String alph_exp="";
            for (int i = 0; i < ch.Length; i++)
            {
                String alpha="";
                switch (ch[i])
                {
                    case '0': alpha = letter[i]+"\'";
                        break;
                    case '1': alpha = letter[i] + "";
                        break;
                    default: continue;
                }
                if(alph_exp=="")
                    alph_exp = alpha;
                else
                    alph_exp +="."+ alpha;
            }
            return alph_exp;
        }

        int zoom = -20;
        int nor_of_rows = 0;
        Bitmap ckt_img;
        public void ckt_refresh()
        {
           // draw_ckt(zoom);
        }
        private void draw_ckt(int zoom)
        {
            //drawing circutes
                 
            nor_of_rows = 0;
            simplify_expression se = new simplify_expression(logical_exp);
            label1.Text = se.str;
            foreach (String val in se.str.Split(','))
            {
                divide_gates_for_row dgfr = new divide_gates_for_row(val);
                foreach (String v in dgfr.gates.Split('+'))
                {
                    nor_of_rows += v.Split('@').Length;
                }
            }
            richTextBox1.AppendText("Origina exp=" + logical_exp + "\n");
            Log.write_log("Origina exp=" + logical_exp + "\n");
            richTextBox1.AppendText("Simplified exp=" + se.str + "\n");
            Log.write_log("Simplified exp=" + se.str + "\n");

            String img_file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\guru\\ckt_"+ object_property.file_count + ".png";

            richTextBox1.AppendText("nor_of_rows=" + nor_of_rows + "\n");
            Log.write_log("nor_of_rows=" + nor_of_rows + "\n");
            richTextBox1.ScrollToCaret();
            label2.Text = zoom.ToString();
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Drawing.Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            draw_line(zoom, myAssembly,g);
            draw_Gates(zoom, myAssembly, g);
            g.Save();

            ckt_img = new Bitmap(panel1.Width, panel1.Height+100);
            Graphics g2 = Graphics.FromImage(ckt_img);
            g2.Clear(Color.White);
            draw_line(zoom, myAssembly, g2);
            draw_Gates(zoom, myAssembly, g2);
            new draw_string(se.str, g2, 30, panel1.Height+20, System.Drawing.Color.Red, 11, panel1.Width, 40);
            new draw_string("Dr AIT Mini project CSE", g2, panel1.Width-290, panel1.Height+25, System.Drawing.Color.Red,14,290,100);
            ckt_img.Save(img_file, ImageFormat.Png);
            panel1.Refresh();
            //  g.Dispose();
        }
        private void draw_line(int zoom, System.Reflection.Assembly myAssembly,System.Drawing.Graphics g)
        {
            object_property std_img_size = new object_property();
            std_img_size.x = 20;
            std_img_size.y = 20;
            std_img_size.width = 50;
            std_img_size.height = 60;
            std_img_size.x_adj = -108;
            std_img_size.y_adj = 0;
            std_img_size.width_adj = 0;
            std_img_size.height_adj = 0;
            int input_space = 50+zoom;
            draw_ip_lines ip_l = new draw_ip_lines(this, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, input_space);
        }
        private void draw_Gates(int zoom, System.Reflection.Assembly myAssembly, System.Drawing.Graphics g)
        {
            object_property std_img_size2 = new object_property();
            std_img_size2.width = 50+zoom;
            std_img_size2.height = 60+zoom;
            std_img_size2.width_adj = 150+zoom;
            std_img_size2.height_adj = 63+zoom;

            draw_gates dg = new draw_gates(this, g, zoom, std_img_size2, myAssembly, nor_of_rows, n_ip);
        }

        private void button1_Click(object sender, EventArgs e)
        {    
            draw_ckt(zoom);
            button2.Visible = true;
            button3.Visible = true;
        }
        private void Form2_MouseWheel(object sender, MouseEventArgs e)
        {
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zoom += 10;
            draw_ckt(zoom);
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            zoom -= 10;
            object_property.panel_width = 0;
            draw_ckt(zoom);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                panel1.Width = ckt_img.Width + 10;
                panel1.Height = ckt_img.Height + 10;
                e.Graphics.DrawImage(ckt_img, new Point(10, 10));
                
            }
            catch (Exception) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }
    }
    
    
}
