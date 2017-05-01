using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Strela
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public const float velikost = 6;
        private float vX, vY;
        private float rychlost = 6;
        private Color barva = Color.Aqua;

        public Strela(float x, float y, float sX, float sY, int velikostDela)
        {
            X = x - velikost / 2;
            Y = y - velikost / 2;
            vX = (x - sX) * (rychlost / velikostDela);
            vY = (y - sY) * (rychlost / velikostDela);
        }

        public void Vykresli(Graphics gr)
        {
            gr.FillEllipse(Brushes.Black, X, Y, velikost, velikost);
        }

        public void Pohyb()
        {
            X += vX;
            Y += vY;
        }
    }
}
