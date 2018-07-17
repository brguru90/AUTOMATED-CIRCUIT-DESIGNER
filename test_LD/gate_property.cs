using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test_LD
{
    class gate_property
    {
        int pos, total_ip_height, total_side_height;
        public int single_side_height, ip_gap;
        public gate_property(int img_height,int ip)
        {
            total_ip_height = Convert.ToInt32(img_height * 0.6);
            ip_gap = Convert.ToInt32(total_ip_height / ip);
            total_side_height = Convert.ToInt32(img_height - total_ip_height);
            single_side_height = Convert.ToInt32(total_side_height / 2);
        }
    }
}
