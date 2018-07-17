using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Drawing;

namespace test_LD
{
    class identify_gate_and_draw
    {
        object_property[] px_row;
        int x, y;
        public int inner_gate_max_x_axis=0;
        public static int right_pad = -1;
        int c = 0, cur_row = 0, max_row, completed;
        Form2 f;
        System.Drawing.Graphics g;
        public identify_gate_and_draw(Form2 f, System.Drawing.Graphics g, int zoom, object_property std_img_size, System.Reflection.Assembly myAssembly, int nor_of_rows, int n_ip, String gate, int r, int c, int cur_row, int max_row, int completed, IList<String> from, String cur,int level,int belongs,int or_gate_pos=-1,bool is_it_or=false)
        {
            this.cur_row = cur_row;
            this.max_row = max_row;
            this.completed = completed;
            r += cur_row;
            this.f = f;
            this.g = g;
            if((c - or_gate_pos )!= -1)
                or_gate_pos = c - or_gate_pos;
            px_row = draw_ip_lines.px_row;
            if(px_row.Length>n_ip-1)
                x = (c+1)*std_img_size.width_adj + px_row[n_ip-1].x;
            else
                x = (c + 1) * std_img_size.width_adj + px_row[px_row.Length - 1].x;
            if (px_row.Length >r)
                y = px_row[r].y;
            else
                y = px_row[px_row.Length-1].y;
            f.richTextBox1.AppendText("\ncur_row" + cur_row + " max_row-" + max_row + " completed" + completed );
            Log.write_log("\n"+is_it_or+" cur_row" + cur_row + " max_row-" + max_row + " completed" + completed);
            f.richTextBox1.AppendText("\nPosition: row-" + r + ",col-" + c + "=" + gate +"\n");
            Log.write_log("\nPosition: row-" + r + ",col-" + c + "=" + gate );
            Log.write_log("\nAxis: row-" + x + ",col-" + y + "=" + gate );
            Log.write_log("\nlevel:" + level + "belongs" + belongs + "\n\n");
            if(object_property.test_mode)
                new draw_point(f, g, zoom,  x, y, System.Drawing.Color.Blue);
            gate gt=null;
            String pattern0 = "[(]+[A-Z._0-9\'ʘ]+[)]+[+]";
            String pattern1 = "[(]+[A-Z._0-9\'ʘ]+[)]+";
            String pattern2 = "[(]+[A-Z._0-9\']+[)]+ʘ";
            String pattern4_1 = "[(]+[A-Z._0-9\'ʘ]+[)]+";
            //gate recognation
            if (Regex.IsMatch(gate, "^" + pattern1 + "$", RegexOptions.IgnoreCase) || Regex.IsMatch(gate, "^[A-Z]\'?$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("0single input : " + gate );
                Log.write_log("0single input : " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate,cur,x, y, 3, "direct", from,level,belongs,2);
                gt.or_gate_pos = or_gate_pos;
            }

            if (Regex.IsMatch(gate, "^(" + pattern4_1 + "){2}$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("2-AND Gate: " + gate);
                Log.write_log("2-AND Gate: " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 2, "and", from, level, belongs);
            }

            if (Regex.IsMatch(gate, "^(" + pattern4_1 + "){3}$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("3-AND Gate: " + gate);
                Log.write_log("3-AND Gate: " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 3, "and", from, level, belongs);
            }
            if (Regex.IsMatch(gate, "^" + pattern2 +pattern1+ "$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("2-XOR Gate: " + gate);
                Log.write_log("2-XOR Gate: " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 2, "xor", from, level, belongs);
            }

            if (Regex.IsMatch(gate, "^(" + pattern2 + "){2}" + pattern1 + "$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("3-XOR Gate: " + gate);
                Log.write_log("3-XOR Gate: " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 3, "xor", from, level, belongs);
            }
            if (Regex.IsMatch(gate, "^" + pattern0 + pattern1 + "$", RegexOptions.IgnoreCase))
            {
                    f.richTextBox1.AppendText("2-OR Gate: " + gate);
                    Log.write_log("2-OR Gate: " + gate);
                    gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 2, "or", from, level, belongs);
                    gt.or_gate_pos = or_gate_pos;
            }               
            if (Regex.IsMatch(gate, "^(" + pattern0 + "){2}" + pattern1 + "$", RegexOptions.IgnoreCase))
            {
                    f.richTextBox1.AppendText("3-OR Gate: " + gate);
                    Log.write_log("3-OR Gate: " + gate);
                    gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 3, "or", from, level, belongs);
                    gt.or_gate_pos = or_gate_pos;
            }
            if (Regex.IsMatch(gate, "^[A-Z]\'?[.][A-Z]\'?$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("2-AND Gate: " + gate);
                Log.write_log("2-AND Gate: " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 2, "and", from, level, belongs);
            }
            if (Regex.IsMatch(gate, "^[A-Z]\'?[.][A-Z]\'?[.][A-Z]\'?$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("3-AND Gate: " + gate);
                Log.write_log("3-AND Gate: " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, cur, gate, x, y, 3, "and", from, level, belongs);
            }
            if (Regex.IsMatch(gate, "^[A-Z]ʘ[A-Z]$", RegexOptions.IgnoreCase) || Regex.IsMatch(gate, "^[(][A-Z]ʘ[A-Z][)]$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("2-XOR Gate: " + gate);
                Log.write_log("2-XOR Gate: " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 2, "xor", from, level, belongs);
            }
            if (Regex.IsMatch(gate, "^[A-Z]ʘ[A-Z]ʘ[A-Z]$", RegexOptions.IgnoreCase) || Regex.IsMatch(gate, "^[(][A-Z]ʘ[A-Z]ʘ[A-Z][)]$", RegexOptions.IgnoreCase))
            {
                f.richTextBox1.AppendText("3-XOR Gate: " + gate);
                Log.write_log("3-XOR Gate: " + gate);
                gt = new gate(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, gate, cur, x, y, 3, "xor", from, level, belongs);
            }
            if (gt != null)
            {
                if (object_property.test_mode)
                    new draw_string(cur + " P:" + gt.or_gate_pos+" B:"+belongs+ " L:"+level, g, x+10, y - 15, System.Drawing.Color.Green);
                connect_gate(gt, c);
            }
        }
        
        private void connect_gate(gate gt, int c)
        {
            draw_gate_row.list.Add(gt);
            String gate = gt.px_gate.label1;
            int[] px_ip = gt.px_gate.px_ip;
            if (c == 0)
            {
                //conect gate from source
                gate = gate.Split('@')[gate.Split('@').Length - 1];
                object_property[] px_ip_points = draw_ip_lines.px_ip_points;
                String[] gate_arr = gate.Split('.');
                int ip = px_ip.Length - 1;
                for (int i = 0; i < gate_arr.Length; i++)
                {
                    for (int j = 0; j < px_ip_points.Length; j++)
                    {
                        if (px_ip_points[j].label1.Equals(gate_arr[i]))
                        {
                            new draw_elbow(f, g, px_ip_points[j].x, px_ip[ip - i], gt.px_gate.x, px_ip[ip - i]);
                            right_pad = gt.px_gate.x;
                            if (right_pad > object_property.panel_width)
                                object_property.panel_width = right_pad;
                        }
                    }
                }
                gate_arr = gate.Split('ʘ');
                
                ip = px_ip.Length - 1;
                for (int i = 0; i < gate_arr.Length; i++)
                {
                    for (int j = 0; j < px_ip_points.Length; j++)
                    {
                        if (px_ip_points[j].label1.Equals(gate_arr[i]))
                        {
                            new draw_elbow(f, g, px_ip_points[j].x, px_ip[ip - i], gt.px_gate.x, px_ip[ip - i]);
                            right_pad = gt.px_gate.x;
                            if (right_pad > object_property.panel_width)
                                object_property.panel_width = right_pad;
                        }
                    }
                }
            }
            //add a line to last gate
            if (max_row == completed)
            {
                new draw_elbow(f, g, gt.px_gate.px_op[0], gt.px_gate.px_op[1], gt.px_gate.px_op[0] + gt.px_gate.width, gt.px_gate.px_op[1]);
                right_pad = gt.px_gate.px_op[0] + gt.px_gate.width;
                if (right_pad > object_property.panel_width)
                    object_property.panel_width = right_pad;
            }
        }
    }
}
