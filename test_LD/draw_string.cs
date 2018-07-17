using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace test_LD
{
    class draw_string
    {
        public draw_string(String str,System.Drawing.Graphics g, int x, int y, System.Drawing.Color color,int size=7,int width=120,int height=150)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(color);
            System.Drawing.PointF pt = new System.Drawing.PointF(x, y);
            System.Drawing.Size sz = new System.Drawing.Size(width, height);
            System.Drawing.Font ft = new Font("Times New Roman", size);
            RectangleF rf = new RectangleF(pt, sz);
            StringFormat sf = new StringFormat();
            g.DrawString(str, ft, myBrush, rf, sf);
        }
    }
}
