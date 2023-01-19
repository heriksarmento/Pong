using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PongCss
{
    public class Game
    {
        public Pong player1 = new Pong(1, 600 / 2 -40);
        public Pong player2 = new Pong(2, 600 / 2 -40);
        public Ball ball = new Ball(new Point(800 / 2, 600 / 2));
        public int[] score = new int[2];
        public bool rightDir = false;
        public double []angle = new double[2];
        public bool scored = false;
        public float speed = 7;
    

        public bool checkColision()
        {
            if(ball.posX < 400)
            {
                if(((player1.posX + 10) >= (ball.posX - ball.size)) && ((((ball.posY + ball.size)>=player1.posY)&&((ball.posY + ball.size) <= player1.posY + 2*player1.size)) || (((ball.posY - ball.size) >= player1.posY) && ((ball.posY - ball.size) <= player1.posY + 2 * player1.size))))
                {
                    rightDir = true;
                    double c = Math.Atan((ball.posY - (player1.posY + player1.size))/ (ball.posX - (player1.posX + 10)));
                    angle[0] = 1*Math.Cos(c);
                    angle[1] = 1*Math.Sin(c);
                    speed += 0.5f;
                    if (speed >= 20)
                    {
                        speed = 20;
                    }
                }
                else if(((ball.posX - ball.size)<=player1.posX+10) && (((ball.posY+ball.size >= player1.posY)&&(ball.posY + ball.size <= player1.posY + 2 * player1.size)) || ((ball.posY - ball.size <= player1.posY) && (ball.posY + ball.size >= player1.posY + 2 * player1.size))))
                {
                    angle[1] *= -1;
                }



                if(ball.posX - ball.size <= 0)
                {
                    speed = 7;
                    scored= true;
                    score[1]++;
                    rightDir = true;
                }
            }
            else
            {
                    if (((player2.posX) <= (ball.posX + ball.size)) && ((((ball.posY + ball.size) >= player2.posY) && ((ball.posY + ball.size) <= player2.posY + 2 * player2.size)) || (((ball.posY - ball.size) >= player2.posY) && ((ball.posY - ball.size) <= player2.posY + 2 * player2.size))))
                {
                    rightDir = false;
                    double c = Math.Atan((ball.posY - (player2.posY + player2.size)) / ((player2.posX) - ball.posX));
                    angle[0] = -1 * Math.Cos(c);
                    angle[1] = 1 * Math.Sin(c);
                    speed += 0.5f;
                    if (speed >= 20)
                    {
                        speed = 20;
                    }
                } 
                else if (((ball.posX - ball.size) >= player2.posX) && (((ball.posY + ball.size >= player2.posY) && (ball.posY + ball.size <= player2.posY + 2 * player2.size)) || ((ball.posY - ball.size <= player2.posY) && (ball.posY + ball.size >= player2.posY + 2 * player2.size))))
                {
                    angle[1] *= -1;
                }

                if (ball.posX + ball.size >= 800)
                {
                    speed= 7;
                    scored = true;
                    score[0]++;
                    rightDir = false;
                }

            }
            return rightDir;
        }
    }
}
