using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Player
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        protected float CenterX
        {
            get
            {
                return X + size / 2;
            }
        }
        protected float CenterY
        {
            get
            {
                return Y + size / 2;
            }
        }
        public float GunX { get; private set; }
        public float GunY { get; private set; }
        public int Hp { get; protected set; }
        protected int maxHp = 30;
        public const float size = 30;
        protected float velocity = 4;
        protected int xMax, yMax;
        protected Color colour;

        public Player(float x, float y, int xMax, int yMax, Color colour)
        {
            X = x;
            Y = y;
            this.xMax = xMax;
            this.yMax = yMax;
            this.colour = colour;
            Hp = maxHp;
        }

        public void Draw(Graphics gr, float mouseX, float mouseY)
        {
            gr.FillEllipse(new SolidBrush(colour), X, Y, size, size);
            //Health bar
            float hpRate = 360 - ((float)Hp / maxHp) * 360;
            gr.FillPie(Brushes.Red, X + size / 8, Y + size / 8, size * 3 / 4, size * 3 / 4, 270, hpRate);
            //aimer
            float Dx = mouseX - CenterX; //distance between mouse and player (center)
            float Dy = mouseY - CenterY;
            float r = size; //gun ratio
            GunY = (Dy * r) / (float)Math.Sqrt(Dx * Dx + Dy * Dy); //equation
            GunX = (float)Math.Sqrt(r * r - GunY * GunY);
            GunX += CenterX; //position relative to the player (center)
            GunY += CenterY;
            if (Dx < 0) //consequence of squaring in the equation
                GunX = 2 * CenterX - GunX;
            gr.DrawLine(Pens.Black, CenterX, CenterY, GunX, GunY);
        }

        public void Move(bool right, bool left, bool up, bool down)
        {
            float v = velocity;
            float Dx = 0;
            float Dy = 0;
            if ((right || left) && (up || down)) //same diagonal speed as horizontal/vertical
                v /= (float)Math.Sqrt(2);
            if (right)
                Dx += v;
            if (left)
                Dx -= v;
            if (up)
                Dy -= v;
            if (down)
                Dy += v;
            if (X + Dx >= 0 && X + Dx + size <= xMax) //window border
                X += Dx;
            if (Y + Dy >= 0 && Y + Dy + size <= yMax)
                Y += Dy;
        }
    }
}
