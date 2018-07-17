using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace test_LD
{
    class draw_image
    {
        public draw_image(System.Drawing.Graphics g, System.Reflection.Assembly myAssembly,String embeded_img_name, int x, int y, int width, int height,int rotation=0,bool back_transperency=false)
        {
            Stream myStream = myAssembly.GetManifestResourceStream("test_LD." + embeded_img_name);
            Bitmap img = new Bitmap(myStream);
            if (rotation != 0)
            {
                int moveX = img.Width / 2 + x;
                int moveY = img.Height / 2 + y;
                g.TranslateTransform(moveX, moveY);
                g.RotateTransform(rotation);
                g.TranslateTransform(-moveX, -moveY);
                if(back_transperency==false)
                    g.FillRectangle(Brushes.White, x, y, width, height);
                g.DrawImage(img, x, y, width, height);
                g.ResetTransform();
            }
            else
            {
                if (back_transperency == false)
                g.FillRectangle(Brushes.White, x, y, width, height);
                g.DrawImage(img, x, y, width, height);
            }
        }
    }
}
