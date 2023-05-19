using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _8192
{
    public class Ball
    {
        public int id, value, rotationSpeed, speed, size, timer;
        public double x, y, xSpeed, ySpeed, direction;
        public SolidBrush ballBrush = new SolidBrush(Color.White);
        Random random = new Random();

        public Ball(int _id, int _value, int _x, int _y)
        {
            id = _id;
            value = _value;
            x = _x;
            y = _y;
            direction = random.Next(0, 17999);
            rotationSpeed = random.Next(-2, 3);
            speed = random.Next(3, 9);
            size = 30;
            if (value <= GameScreen.ballColours.Count)
            {
                ballBrush.Color = GameScreen.ballColours[value - 1];
            }
        }

        public void Move()
        {
            SetSpeed(direction / 50);
            direction += random.Next(-5, 6);
            timer++;

            if (direction >= 10000)
            {
                direction = 0;
                rotationSpeed = random.Next(-5, 6);
            }

            x += xSpeed;
            y += ySpeed;

            // Side walls collision

            if (x < 0 - size)
            {
                x = Form1.formWidth + size;
            }
            else if (x > Form1.formWidth + size)
            {
                x = 0 - size;
            }

            // Top and bottom walls collision

            if (y < 0 - size)
            {
                y = Form1.formHeight + size;
            }
            else if (y > Form1.formHeight + size)
            {
                y = 0 - size;
            }

            CheckCollision();
        }

        public static double GetDirection(double xSpeed, double ySpeed)
        {
            double direction;

            if (ySpeed == 0)
            {
                if (xSpeed < 0)
                {
                    return -90;
                }
                else
                {
                    return 90;
                }
            }
            else
            {
                direction = Math.Atan2(xSpeed, ySpeed * -1) * 180 / Math.PI;

                if (xSpeed >= 0)
                {
                    return direction;
                }
                else
                {
                    return direction + 360;
                }
            }
        }

        public void SetSpeed()
        {
            double direction = GetDirection(xSpeed, ySpeed);
            xSpeed = speed * Math.Sin(direction);
            ySpeed = speed * Math.Cos(direction);
        }

        public void SetSpeed(double direction)
        {
            xSpeed = speed * Math.Sin(direction);
            ySpeed = speed * Math.Cos(direction);
        }

        public void CheckCollision()
        {
            foreach (Ball b in GameScreen.balls)
            {
                Rectangle self = new Rectangle(Convert.ToInt32(Math.Round(x - size / 2)), Convert.ToInt32(Math.Round(y - size / 2)), size, size);
                Rectangle other = new Rectangle(Convert.ToInt32(Math.Round(b.x - b.size / 2)), Convert.ToInt32(Math.Round(b.y - b.size / 2)), b.size, b.size);

                if (self.IntersectsWith(other) && id != b.id)
                {
                    if (value == b.value)
                    {
                        GameScreen.idCounter++;
                        GameScreen.balls.Add(new Ball(GameScreen.idCounter, value + 1, Convert.ToInt32(Math.Round((x + b.x) / 2)), Convert.ToInt32(Math.Round((y + b.y) / 2))));
                        id = 0;
                        b.id = 0;
                        break;
                    }
                    else if (value - b.value < 4)
                    {
                        x -= 2 * xSpeed;
                        y -= 2 * ySpeed;
                        direction += 9000;
                        // Make number balls not go inside other ones
                    }
                }
            }
        }
    }
}