using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Shot
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public const float size = 6;
        private float vX, vY;
        private float velocity = 6;
        private Color colour = Color.Aqua;

        public Shot(float x, float y, float centerX, float centerY, int gunSize)
        {
            X = x - size / 2;
            Y = y - size / 2;
            vX = (x - centerX) * (velocity / gunSize);
            vY = (y - centerY) * (velocity / gunSize);
        }

        public void Draw(Graphics gr)
        {
            gr.FillEllipse(Brushes.Black, X, Y, size, size);
        }

        public void Move()
        {
            X += vX;
            Y += vY;
        }
    }
}
