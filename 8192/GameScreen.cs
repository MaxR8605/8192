using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8192
{
    public partial class GameScreen : UserControl
    {
        public static int ballTimer = 0;
        public static int idCounter = 0;
        Random random = new Random();

        Font font = new Font("Consolas", 22);
        SolidBrush black = new SolidBrush(Color.Black);
        public static List<Color> ballColours = new List<Color> { Color.FromArgb(180, 180, 180), Color.FromArgb(200, 180, 160), Color.FromArgb(250, 120, 100), Color.FromArgb(230, 70, 0), 
            Color.FromArgb(235, 60, 50), Color.FromArgb(255, 0, 0), Color.FromArgb(250, 230, 150), Color.FromArgb(250, 200, 100), Color.FromArgb(255, 180, 50), Color.FromArgb(255, 160, 0), Color.FromArgb(255, 100, 0),
            Color.FromArgb(100, 50, 255), Color.FromArgb(175, 60, 255), Color.FromArgb(200, 130, 255), Color.FromArgb(230, 100, 255), Color.FromArgb(150, 200, 150), Color.FromArgb(0, 155, 50), Color.FromArgb(0, 100, 120),
            Color.FromArgb(20, 25, 200) };

    public static List<Ball> balls = new List<Ball>();

        public GameScreen()
        {
            InitializeComponent();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (ballTimer == 0)
            {
                ballTimer = 2;
                CreateBall(1, random.Next(0, Width - 30), random.Next(0, Height - 30));
            }
            else
            {
                ballTimer--;
            }
            foreach (Ball b in balls)
            {
                b.Move();
                
                if (b.id == 0)
                {
                    balls.Remove(b);
                    break;
                }
            }

            Refresh();
        }
        public void CreateBall(int value, int x, int y)
        {
            idCounter++;
            balls.Add(new Ball(idCounter, value, x, y));
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ball b in balls)
            {
                float x = Convert.ToInt32(Math.Round(b.x - b.size / 2));
                float y = Convert.ToInt32(Math.Round(b.y - b.size / 2));
                e.Graphics.FillEllipse(b.ballBrush, x, y, b.size, b.size);

                int number = 1;
                int fontSize = 0;
                int fontX = 0;
                int fontY = 0;
                for (int i = 0; i < b.value; i++)
                {
                    number *= 2;
                }

                switch (Convert.ToString(number).Length)
                {
                    case 1:
                        fontSize = 25;
                        fontY = -5;
                        break;
                    case 2:
                        fontSize = 18;
                        fontX = -1;
                        fontY = 2;
                        b.size = 33;
                        break;
                    case 3:
                        fontSize = 15;
                        fontX = -1;
                        fontY = 6;
                        b.size = 36;
                        break;
                    case 4:
                        fontSize = 12;
                        fontX = -1;
                        fontY = 11;
                        b.size = 40;
                        break;
                    case 5:
                        fontSize = 11;
                        fontY = 15;
                        b.size = 45;
                        break;
                    case 6:
                        fontSize = 10;
                        fontY = 18;
                        b.size = 50;
                        break;
                    case 7:
                        fontSize = 10;
                        fontY = 21;
                        b.size = 56;
                        break;
                    case 8:
                        fontSize = 12;
                        fontX = -1;
                        fontY = 24;
                        b.size = 65;
                        break;
                }
                font = new Font("Consolas", fontSize);
                e.Graphics.DrawString($"{number}", font, black, x + fontX, y + fontY);
            }
        }
    }
}
