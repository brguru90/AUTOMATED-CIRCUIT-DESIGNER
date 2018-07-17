using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Drawing;


namespace test_LD
{
    class draw_ip_lines
    {

        System.Drawing.Graphics g;
        Stream myStream;
        System.Reflection.Assembly myAssembly;
        int _x, _y, x1, y1, x2, y2, total_depth, width_gap,sum, starting_top_gap, n_ip, nor_of_rows, not_gate_sapce;
        public static object_property[] px_row, px_ip_points;
        int zoom = 1;
        Bitmap img;
        int imageHeight, imageWidth;
        object_property std_img_size = new object_property();
        Form2 f;
        public draw_ip_lines(Form2 f, System.Drawing.Graphics g, int zoom, object_property std_img_size, System.Reflection.Assembly myAssembly, int nor_of_rows, int n_ip, int v_gap = 60, int h_gap = 40)
        {
            this.f = f;
            this.g = g;
            this.nor_of_rows = nor_of_rows;
            this.n_ip = n_ip;
            _x = std_img_size.x;
            _y = std_img_size.y;
            this.zoom = zoom;
            this.myAssembly = myAssembly;
            this.std_img_size = std_img_size;
            imageHeight = std_img_size.height + zoom;
            //line gap
            imageWidth = std_img_size.width + zoom;
            width_gap = std_img_size.width + zoom + h_gap;
            //primary top level gap 
            starting_top_gap = imageHeight / 4 + _y;
            //generating  grids for not gates
            sum = starting_top_gap;
            not_gate_sapce = imageHeight/2 + sum;
            //generating  grids for horizontal ckt_input lines
            px_row = new object_property[nor_of_rows];
            for (int i = 0; i < nor_of_rows; i++)
            {
                sum += imageHeight + v_gap;
                px_row[i] = new object_property();
                px_row[i].x = _x;
                px_row[i].y = sum;
            }
            //normal input line depth is total depth
            total_depth = sum + imageHeight + v_gap;
            draw_inputs();
            for (int j = 0; j < n_ip*2; j++)
            {
                for (int i = 0; i < nor_of_rows; i++)
                {
                    px_row[i].x = px_ip_points[j].x;
                    if (object_property.test_mode)
                    {
                        if (j % 2 == 0)
                            draw_point(px_row[i].x, px_row[i].y);
                    }
                }
            }
        }
        public void draw_point(int x, int y)
        {
            draw_point d = new draw_point(f, g,zoom, x, y, System.Drawing.Color.Blue);
        }
        private void place_gates(int x, int y, int width, int height)
        {
            draw_image di = new draw_image(g, myAssembly, "not.png", x, y, width, height, 90);
        }
        public void draw_inputs()
        {
            //draw normal input line
            x1 = x2 = _x;
            y1 = _y;
            y2 = _y + total_depth;
            int x_gap_prev = _x;
            px_ip_points = new object_property[n_ip * 2];
            for (int i = 0; i < n_ip; i++)
            {
                x_gap_prev = x2;
                y1 = _y;
                y2 = _y + total_depth;
                g.DrawLine(new Pen(Color.Brown, 1), new Point(x1, y1), new Point(x2, y2));
                x1 += width_gap;
                x2 = x1;
                y1 = _y + starting_top_gap;
                g.DrawLine(new Pen(Color.Brown, 1), new Point(x1, y1), new Point(x2, y2));
                g.DrawLine(new Pen(Color.Brown, 1), new Point(x_gap_prev, y1), new Point(x2, y1));
                //getting row-col points
                px_ip_points[i + i] = new object_property();
                px_ip_points[i + i].x = x_gap_prev;
                px_ip_points[i + i + 1] = new object_property();
                px_ip_points[i + i + 1].x = x2;
                px_ip_points[i + i].label1 = new characters().letter[i].ToString();
                px_ip_points[i + i+1].label1 = new characters().letter[i].ToString()+"'";
                place_gates(x2 + std_img_size.x_adj + zoom - zoom / 2, not_gate_sapce, std_img_size.width + zoom, std_img_size.height + zoom);
                //Assigning input letter to column
                new draw_string(px_ip_points[i + i].label1, g, x_gap_prev, _y - 15, System.Drawing.Color.Blue, 15);
                new draw_string(px_ip_points[i + i + 1].label1, g, x2, y1-5 - 15, System.Drawing.Color.Blue, 15);
                x1 += width_gap;
                x2 = x1;
            }
            f.panel1.Size = new Size(f.panel1.Size.Width, y2+((sum - not_gate_sapce) / nor_of_rows) / 2);
        }
    }
}
