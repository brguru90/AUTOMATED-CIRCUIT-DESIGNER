using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace test_LD
{
    class draw_point
    {

        public draw_point(Form2 f, System.Drawing.Graphics g, int zoom, int x, int y,System.Drawing.Color color , int thickness = 4)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(color);
            g.FillRectangle(myBrush, x - thickness / 2, y - thickness/2, thickness, thickness);
            new draw_string("(" + x + "," + y + ")", g, x, y + 5, System.Drawing.Color.Blue);
            myBrush.Dispose();
        }
    }
}
