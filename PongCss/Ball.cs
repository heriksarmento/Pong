using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongCss
{
    public class Ball
    {
        public double posX;
        public double posY;
        public int size;
        public Ball(Point a) {
            posX = a.X;
            posY = a.Y;
            size = 9;
        }
    }
}
