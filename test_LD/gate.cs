using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace test_LD
{
    class gate
    {
        Form1 f;
        public int img_height, img_width;
        public object_property px_gate = new object_property();
        int top_side = 0;
        public int gate_id = 0;
        public static int n_or=0;
        public int level;
        public int or_gate_pos=-1;
        String label = "";
        public gate(Form2 f, System.Drawing.Graphics g, int zoom, object_property std_img_size, System.Reflection.Assembly myAssembly, int nor_of_rows, int n_ip, String gate,String cur ,int x, int y, int n_gate_ip, String gate_name, IList<String> from,int level,int belongs, int is_line = -1)
        {
            px_gate.width=img_width = std_img_size.width;
            px_gate.height=img_height = std_img_size.height;
            px_gate.px_ip = new int[n_gate_ip];
            px_gate.gate_name = gate_name;
            this.level = level;
            top_side += new gate_property(img_height, n_gate_ip).single_side_height;
            int ip_gap = new gate_property(img_height, n_gate_ip).ip_gap;
            px_gate.ip_label = new String[n_gate_ip];
            for (int i = 0; i < n_gate_ip; i++)
            {
                px_gate.px_ip[i] = top_side + y + ip_gap * i + 1;
               // draw_point d = new draw_point(f, g, zoom, x, px_gate.px_ip[i], System.Drawing.Color.Blue);
            }
            label=px_gate.label1 = gate;
            px_gate.label2 = cur;
            px_gate.belongs = belongs;
            px_gate.x = x;
            px_gate.y = y;
            px_gate.px_op[0] = x + img_width;
            px_gate.px_op[1] = y + img_height / 2;
            if (is_line==-1)
            {
                //drawing gate image
                px_gate.ip_label = from.ToArray();
                draw_image di = new draw_image(g, myAssembly, gate_name + ".png", px_gate.x, px_gate.y, img_width, img_height, 0, true);
            }
            else
            {
                //drawing bypass line
                px_gate.ip_label[2] = from[0];
                new draw_elbow(f, g, px_gate.x, px_gate.px_ip[is_line], px_gate.px_op[0], px_gate.px_op[1]);
            }
            label=Regex.Replace(label,"_[0-9]+","_");
            new draw_string(n_gate_ip +"-"+gate_name.ToUpper() + " " + label, g, x + 10, y + img_height, System.Drawing.Color.Red);
            Log.write_log("\n"+n_gate_ip + gate_name + " " + px_gate.label1 + "," + (x + 10) + "," + (y + img_height + 5) + "," + "Green\n");
        }
    }
}
