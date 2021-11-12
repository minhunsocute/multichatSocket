﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Client
{
    public static class Utils
    {
        public static int GetTextHeight(Label lbl) { 
            using(Graphics g = lbl.CreateGraphics()) {
                SizeF size = g.MeasureString(lbl.Text, lbl.Font, 495);
                return (int)Math.Ceiling(size.Height);
            } 
        }
    }
}
