using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongCss
{
    public class Pong
    {
        public int size;
        public double posX;
        public double posY;

        public Pong(int a,int posy)
        {
            if(a == 1)
            {
                posX = 10;
            }
            else if(a == 2)
            {
                posX = 800-20;
            }
            size = 40;
            posY= posy;
        }

    }
}
