using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace test_LD
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.TextBox[] txtbox=null;
        private System.Windows.Forms.TextBox[,] txtbox_op=null;
        public static System.Windows.Forms.TextBox[] minterm = new System.Windows.Forms.TextBox[20];
        public static int n_ip;
        public static double n_ip2;
        public static int n_op,i,j,pad, tab_index;
        public Form1()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            checkBox1.BackColor = Color.Transparent;
        }
        private void truth_table(int pad)
        {
            if (checkBox1.Checked && Form1.minterm[0]!=null)
            {
                try
                {
                    foreach (Control val in txtbox)
                    {
                        this.Controls.Remove(val);
                        if (val != null)
                            val.Dispose();
                    }
                }
                catch (Exception) { }
                try
                {
                    foreach (Control val in txtbox_op)
                    {
                        this.Controls.Remove(val);
                        if (val != null)
                            val.Dispose();
                    }
                }
                catch (Exception) { }

                Form1.n_ip2 = Math.Pow(2, Int32.Parse(textBox1.Text));
                txtbox = new System.Windows.Forms.TextBox[Convert.ToInt32(Form1.n_ip2)];
                txtbox_op = new System.Windows.Forms.TextBox[Form1.n_op, Convert.ToInt32(Form1.n_ip2)];
                //generated binary inputs
                for (i = 0; i < Form1.n_ip2; i++)
                {
                    txtbox[i] = new System.Windows.Forms.TextBox();
                    this.Controls.Add(txtbox[i]);
                    txtbox[i].Location = new System.Drawing.Point(50, pad + (i * 25));
                    txtbox[i].Name = "textBox" + (i + 2);
                    String result = Convert.ToString(i, 2);
                    txtbox[i].Size = new System.Drawing.Size(100, 22);
                    txtbox[i].TabIndex = tab_index;
                    txtbox[i].Text = String.Format("{0:D" + textBox1.Text + "}", Int32.Parse(result));
                    txtbox[i].ReadOnly = true;
                    tab_index++;
                }
                int end;
                int left = 200;
                int k = 0;

                //displaying inputs to respected binary
                for (j = 0; j < Form1.n_op; j++)
                {
                    end = i;
                    for (; i < (end + Form1.n_ip2); i++)
                    {
                        k = (i - end);
                        txtbox_op[j, k] = new System.Windows.Forms.TextBox();
                        this.Controls.Add(txtbox_op[j, k]);
                        txtbox_op[j, k].Location = new System.Drawing.Point(left, pad + (k * 25));
                        txtbox_op[j, k].Name = "textBox" + (k + 2);
                        txtbox_op[j, k].Size = new System.Drawing.Size(40, 22);
                        txtbox_op[j, k].TabIndex = tab_index;
                        txtbox_op[j, k].Text = "0";
                        txtbox_op[j, k].ReadOnly = true;
                        tab_index++;
                    }
                    left += 50;
                }
            }
            else
            {
                try
                {
                    foreach (Control val in txtbox)
                    {
                        this.Controls.Remove(val);
                        if (val != null)
                            val.Dispose();
                    }
                }
                catch (Exception) { }
                try
                {
                    foreach (Control val in txtbox_op)
                    {
                        this.Controls.Remove(val);
                        if (val != null)
                            val.Dispose();
                    }
                }
                catch (Exception) { }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != null && textBox1.Text != "0")
            {
                Form1.n_ip = Int32.Parse(textBox1.Text);
                Form1.n_ip2 = Int32.Parse(Math.Pow(2, Int32.Parse(textBox1.Text)).ToString());
            }
            else
            {
                textBox1.Focus();
                textBox1.Select();
                MessageBox.Show("Enter number of input");
                button3.Enabled = false;
                return;
            }
            if (textBox2.Text != "" && textBox2.Text != null && textBox2.Text != "0")
                Form1.n_op = Int32.Parse(textBox2.Text);
            else
            {
                textBox2.Focus();
                textBox2.Select();
                MessageBox.Show("Enter number of output");
                button3.Enabled = false;
                return;
            }
            try
            {
                foreach (Control val in Form1.minterm)
                {
                    this.Controls.Remove(val);
                    if (val != null)
                        val.Dispose();
                }
            }
            catch (Exception) { }
            i = 0; j=0;
            pad = 100;
            tab_index = 10;
            Form1.minterm = new System.Windows.Forms.TextBox[Form1.n_op];
           
            //label as Form1.minterm
            System.Windows.Forms.Label minterm_h = new System.Windows.Forms.Label();
            this.Controls.Add(minterm_h);
            minterm_h.Location = new System.Drawing.Point(50, pad);
            minterm_h.Name = "Minterm_h";
            minterm_h.Size = new System.Drawing.Size(400, 22);
            minterm_h.BackColor = Color.Transparent;
            minterm_h.ForeColor=Color.White;
            minterm_h.TabIndex = tab_index;
            minterm_h.Text = "Form1.minterm(seperator-\"comma\")";
            minterm_h.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tab_index++;
            pad += 25;
            //Form1.minterm inputs
            for (j = 0; j < Form1.n_op; j++)
            {
                Form1.minterm[j] = new System.Windows.Forms.TextBox();
                this.Controls.Add(Form1.minterm[j]);
                Form1.minterm[j].Location = new System.Drawing.Point(50, pad + (j * 25));
                Form1.minterm[j].Name = "Form1.minterm" + (j + 2);
                Form1.minterm[j].Size = new System.Drawing.Size(400, 22);
                Form1.minterm[j].TabIndex = tab_index;
                Form1.minterm[j] .TextChanged+= new EventHandler(textBox_TextChanged);
                tab_index++;
                //Form1.minterm[j].Text = "4,5,12,13,9,2";
            }
            pad = pad + (j * 25);
            checkBox1.Enabled = true;
            truth_table(pad);

            Form1.minterm[0].Focus();
            Form1.minterm[0].Select();
            button2.Visible = true;
            button2.Enabled = true;
            button3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.n_op = Int32.Parse(textBox2.Text);
            String[] op_row=new String[64];
            //validation of inputs
            for (int j = 0; j < Form1.n_op; j++)
            {
                if (!Regex.IsMatch(Form1.minterm[j].Text, "^[0-9]+(,[0-9]+)*$"))
                {
                    Form1.minterm[j].Focus();
                    Form1.minterm[j].Select();
                    MessageBox.Show("Incorrect Format");
                    button3.Enabled = false;
                    return;
                }
                foreach(String val in Form1.minterm[j].Text.Split(','))
                    if(Int32.Parse(val)>=Math.Pow(2, Int32.Parse(textBox1.Text)))
                    {
                        Form1.minterm[j].Focus();
                        Form1.minterm[j].Select();
                        MessageBox.Show("Not in range");
                        button3.Enabled = false;
                        return;
                    }
                //fell output in binary
                if (Form1.minterm[j].Text != "" && (checkBox1.Checked==true))
                {
                    op_row = Form1.minterm[j].Text.Split(',');
                    foreach (String val in op_row)
                        txtbox_op[j, Int32.Parse(val)].Text = "1";
                }
            }
            button3.Enabled = true;
            button3.Focus();
            button3.Select();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.Show();
            this.Hide();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String file=Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\guru\\logs.txt";
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }
            /*
             * label3.Font = new Font(new font_loader().private_fonts.Families[1], 40);
            label3.UseCompatibleTextRendering = true;
            label5.Font = new Font(new font_loader().private_fonts.Families[0], 40);
            label5.UseCompatibleTextRendering = true;
             * */
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            truth_table(pad);
        }
        protected void textBox_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }
    }
}
