using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Souper: Hrac
    {
        public Souper(float x, float y, int xMax, int yMax, Color barva, float rychlost, int hp)
            : base(x, y, xMax, yMax, barva)
        {
            this.rychlost = rychlost;
            Hp = hp;
            maxHp = Hp;
        }

        public void Vykresli(Graphics gr)
        {
            gr.FillEllipse(new SolidBrush(barva), X, Y, velikost, velikost);
            //Health bar
            float podilHp = 360 - ((float)Hp / maxHp) * 360;
            gr.FillPie(Brushes.Red, X + velikost / 8, Y + velikost / 8, velikost * 3 / 4, velikost * 3 / 4, 270, podilHp);
        }

        public void Pohyb(float hracX, float hracY)
        {
            float v = rychlost;
            float Dx = 0;
            float Dy = 0;
            if (hracX - X > v)
                Dx = v;
            else if (hracX - X < -v)
                Dx = -v;
            if (hracY - Y > v)
                Dy = v;
            else if (hracY - Y < -v)
                Dy = -v;
            if (Dx != 0 && Dy != 0) //stejná diagonální rychlost jako horizontální/vertikální
            {
                Dx /= (float)Math.Sqrt(2);
                Dy /= (float)Math.Sqrt(2);
            }
            if (X + Dx >= 0 && X + Dx + velikost <= xMax) //okraje okna
                X += Dx;
            if (Y + Dy >= 0 && Y + Dy + velikost <= yMax)
                Y += Dy;
        }

        public bool Zasah(float strelaX, float strelaY)
        {
            float stredX = strelaX + Strela.velikost / 2;
            float stredY = strelaY + Strela.velikost / 2;
            float vzdalenost = (float)Math.Sqrt(Math.Pow(X + velikost / 2 - stredX, 2) + Math.Pow(Y + velikost / 2 - stredY, 2));
            if (vzdalenost < velikost / 2 + Strela.velikost / 2)
            {
                Hp--;
                return true;
            }
            return false;
        }
    }
}
