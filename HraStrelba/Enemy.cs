using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Enemy: Player
    {
        public Enemy(float x, float y, int xMax, int yMax, Color colour, float velocity, int hp)
            : base(x, y, xMax, yMax, colour)
        {
            this.velocity = velocity;
            Hp = hp;
            maxHp = Hp;
        }

        public void Draw(Graphics gr)
        {
            gr.FillEllipse(new SolidBrush(colour), X, Y, size, size);
            //Health bar
            float hpRate = 360 - ((float)Hp / maxHp) * 360;
            gr.FillPie(Brushes.Red, X + size / 8, Y + size / 8, size * 3 / 4, size * 3 / 4, 270, hpRate);
        }

        public void Move(float playerX, float playerY)
        {
            float v = velocity;
            float Dx = 0;
            float Dy = 0;
            if (playerX - X > v)
                Dx = v;
            else if (playerX - X < -v)
                Dx = -v;
            if (playerY - Y > v)
                Dy = v;
            else if (playerY - Y < -v)
                Dy = -v;
            if (Dx != 0 && Dy != 0) //same diagonal speed as horizontal/vertical
            {
                Dx /= (float)Math.Sqrt(2);
                Dy /= (float)Math.Sqrt(2);
            }
            X += Dx;
            Y += Dy;
        }

        public bool Hit(float shotX, float shotY)
        {
            float shotCenterX = shotX + Shot.size / 2;
            float shotCenterY = shotY + Shot.size / 2;
            float distance = Methods.Distance(CenterX, CenterY, shotCenterX, shotCenterY);
            if (distance < size / 2 + Shot.size / 2)
            {
                Hp--;
                return true;
            }
            return false;
        }
    }
}
