using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Hrac
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        public float DeloX { get; private set; }
        public float DeloY { get; private set; }
        public int Hp { get; protected set; }
        protected int maxHp;
        public const float velikost = 30;
        protected float rychlost = 4;
        protected int xMax, yMax;
        protected Color barva;

        public Hrac(float x, float y, int xMax, int yMax, Color barva)
        {
            X = x;
            Y = y;
            this.xMax = xMax;
            this.yMax = yMax;
            this.barva = barva;
            Hp = 30;
            maxHp = Hp;
        }

        public void Vykresli(Graphics gr, float mX, float mY)
        {
            gr.FillEllipse(new SolidBrush(barva), X, Y, velikost, velikost);
            //Health bar
            float podilHp = 360 - ((float)Hp / maxHp) * 360;
            gr.FillPie(Brushes.Red, X + velikost / 8, Y + velikost / 8, velikost * 3 / 4, velikost * 3 / 4, 270, podilHp);
            //zaměřovač
            float sX = X + velikost / 2; //střed hráče
            float sY = Y + velikost / 2;
            float dX = mX - sX; //vzdálenost myši od středu hráče
            float dY = mY - sY;
            float r = velikost; //poloměr děla
            DeloY = (dY * r) / (float)Math.Sqrt(dX * dX + dY * dY); //rovnice
            DeloX = (float)Math.Sqrt(r * r - DeloY * DeloY);
            DeloX += sX; //pozice vůči středu hráče
            DeloY += sY;
            if (dX < 0) //důsledek umocnění při úpravě rovnice
                DeloX = 2 * sX - DeloX;
            gr.DrawLine(Pens.Black, sX, sY, DeloX, DeloY);
        }

        public void Pohyb(bool doprava, bool doleva, bool nahoru, bool dolu)
        {
            float v = rychlost;
            float Dx = 0;
            float Dy = 0;
            if ((doprava || doleva) && (nahoru || dolu)) //stejná diagonální rychlost jako horizontální/vertikální
                v /= (float)Math.Sqrt(2);
            if (doprava)
                Dx += v;
            if (doleva)
                Dx -= v;
            if (nahoru)
                Dy -= v;
            if (dolu)
                Dy += v;
            if (X + Dx >= 0 && X + Dx + velikost <= xMax) //okraje okna
                X += Dx;
            if (Y + Dy >= 0 && Y + Dy + velikost <= yMax)
                Y += Dy;
        }
    }
}
