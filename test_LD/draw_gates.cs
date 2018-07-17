using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test_LD
{
    class draw_gates
    {
        Form2 f;
        public String str="";
        public draw_gates(Form2 f, System.Drawing.Graphics g, int zoom, object_property std_img_size, System.Reflection.Assembly myAssembly, int nor_of_rows, int n_ip)
        {
            this.f = f;
            int cur_row = 0;
            int max_right_pad = 0;
            gate.n_or = 0;
            simplify_expression se = new simplify_expression(f.logical_exp);
            foreach (String val in se.str.Split(','))
            {
                draw_gate_row.list.Clear();
                draw_gate_row dgr = new draw_gate_row(f, g, zoom, std_img_size, myAssembly, nor_of_rows, n_ip, val, cur_row);
                if (object_property.panel_width > max_right_pad)
                    max_right_pad = object_property.panel_width;
                cur_row= dgr.nor_of_row_taken;
            }
            
            if (f.panel1.Width != max_right_pad + 400)
            {
                f.panel1.Width = max_right_pad + 400;
                f.ckt_refresh();
            }
        }
        
        
    }
}
