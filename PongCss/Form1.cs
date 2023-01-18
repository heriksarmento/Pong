using SimuladorGravitacional;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;


namespace PongCss
{
    public partial class Form1 : Form
    {
        Timer timer;
        Game game = new Game();
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += GameLoop_Tick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.DrawLine(Pens.White, 400, 0, 400, 600);
            Font a = new Font("04b_19", 30, FontStyle.Bold);

            g.DrawString(game.score[0].ToString(), a, Brushes.White, 360, 20);
            g.DrawString(game.score[1].ToString(), a, Brushes.White, 406, 20);

            g.FillRectangle(Brushes.White, (float)game.player1.posX , (float)game.player1.posY, 10, game.player1.size*2);
            g.FillRectangle(Brushes.White, (float)game.player2.posX , (float)game.player2.posY, 10, game.player2.size*2);
            g.FillRectangle(Brushes.White, (float)game.ball.posX - game.ball.size, (float)game.ball.posY - game.ball.size, game.ball.size * 2, game.ball.size * 2);
        
        
        
        }

        private enum PlayerMotionState { NotMoving, MovingUp, MovingDown };
        private PlayerMotionState PlayerMotion = PlayerMotionState.NotMoving;
        private PlayerMotionState PlayerMotion2 = PlayerMotionState.NotMoving;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.W)
            {
                PlayerMotion = PlayerMotionState.MovingUp;
                if (i == 0)
                {
                    i++;
                    game.angle[0] = 1;
                }
            }
            if (e.KeyData == Keys.S)
            {
                PlayerMotion = PlayerMotionState.MovingDown;
                if (i == 0)
                {
                    i++;
                    game.angle[0] = -1;
                }
            }


            if (e.KeyData == Keys.Up)
            {
                PlayerMotion2 = PlayerMotionState.MovingUp;
                if (i == 0)
                {
                    i++;
                    game.angle[0] = -1;
                }
            }
            if (e.KeyData == Keys.Down)
            {
                PlayerMotion2 = PlayerMotionState.MovingDown;
                if (i == 0)
                {
                    i++;
                    game.angle[0] = 1;
                }
            }

            if (!timer.Enabled)
            {
                timer.Start();
                if(game.ball.posX == game.player1.posX + 10 + game.ball.size) {
                    game.dir = true;
                    switch (e.KeyData)
                    {
                        case Keys.W:
                            game.angle[0] = 1;
                            game.angle[1] = 1;
                            break;
                        case Keys.D:
                            game.angle[0] = 1;
                            game.angle[1] = 0;
                            break;
                        case Keys.S:
                            game.angle[0] = 1;
                            game.angle[1] = -1;
                            break;
                        default:
                            timer.Stop();
                            break;
                    }
                }
                else
                {
                    game.dir = false;
                    switch (e.KeyData)
                    {
                        case Keys.Up:
                            game.angle[0] = -1;
                            game.angle[1] = 1;
                            break;
                        case Keys.Left:
                            game.angle[0] = -1;
                            game.angle[1] = 0;
                            break;
                        case Keys.Down:
                            game.angle[0] = -1;
                            game.angle[1] = -1;
                            break;
                        default:
                            timer.Stop();
                            break;
                    }
                }
                
            }

            base.OnKeyDown(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if ((e.KeyData == Keys.W && PlayerMotion == PlayerMotionState.MovingUp) ||
                (e.KeyData == Keys.S && PlayerMotion == PlayerMotionState.MovingDown)){
                PlayerMotion = PlayerMotionState.NotMoving;
            }
            if ((e.KeyData == Keys.Up && PlayerMotion2 == PlayerMotionState.MovingUp) ||
                (e.KeyData == Keys.Down && PlayerMotion2 == PlayerMotionState.MovingDown))
            {
                PlayerMotion2 = PlayerMotionState.NotMoving;
            }
            base.OnKeyUp(e);
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            if (PlayerMotion == PlayerMotionState.MovingUp)
            {
                game.player1.posY -= 5;
                if (game.player1.posY <= 0)
                {
                    game.player1.posY = 0;
                }
            }
            else if (PlayerMotion == PlayerMotionState.MovingDown)
            {
                game.player1.posY += 5;

                if (game.player1.posY + 2*game.player1.size >= 600)
                {
                    game.player1.posY = 600 - 2*game.player1.size;
                }
            }

            if (PlayerMotion2 == PlayerMotionState.MovingUp)
            {
                game.player2.posY -= 5;
                if (game.player2.posY <= 0)
                {
                    game.player2.posY = 0;
                }
            }
            else if (PlayerMotion2 == PlayerMotionState.MovingDown)
            {
                game.player2.posY += 5;
                if (game.player2.posY + 2 * game.player2.size >= 600)
                {
                    game.player2.posY = 600 - 2 * game.player2.size;
                }
            }

            if (game.checkColision())
            {
                game.ball.posX += game.speed * game.angle[0];
                game.ball.posY += game.speed *game.angle[1];
                if (game.ball.posX >= 800)
                {
                    game.ball.posX = 800;
                }
            }
            else
            {
                game.ball.posX += game.speed* game.angle[0];
                game.ball.posY += game.speed* game.angle[1];
                if (game.ball.posX <= 0)
                {
                    game.ball.posX = 0;
                }
                
            }

            if (game.ball.posY <= 0)
            {
                game.angle[1] *= -1;
            }else if (game.ball.posY >= 600)
            {
                game.angle[1] *= -1;
            }

            if (game.scored)
            {
                game.scored = false;
                timer.Stop();
                game.player1.posY = game.player2.posY = 300 - 20;
                if (game.dir)
                {
                    game.ball.posX = game.player1.posX + 10 + game.ball.size;
                    game.ball.posY = game.player1.posY + game.player1.size;
                }
                else
                {
                    game.ball.posX = game.player2.posX - game.ball.size;
                    game.ball.posY = game.player2.posY + game.player2.size;
                }
                Invalidate();
            }
            Invalidate();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
