using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace test_LD
{
    class draw_elbow
    {
        public draw_elbow(Form2 f, System.Drawing.Graphics g, int x1,int y1,int x2,int y2)
        {
            if (y1 == y2)
                g.DrawLine(new Pen(Color.Blue, 1), new Point(x1, y1), new Point(x2, y2));
            else
            {
                int x;
                if (x1 > x2)
                    x = x1 - x2;
                else
                    x = x2 - x1;
                x = x / 3;
                g.DrawLine(new Pen(Color.Blue, 1), new Point(x1, y1), new Point(x1 + x, y1));
                g.DrawLine(new Pen(Color.Blue, 1), new Point(x1 + x, y1), new Point(x2 - (x/3), y2));
                g.DrawLine(new Pen(Color.Blue, 1), new Point(x2 - x/3, y2), new Point(x2, y2));
            }
           // g.DrawLine(new Pen(Color.Blue, 1), new Point(x1, y1), new Point(x2, y2));
        }
    }
}
