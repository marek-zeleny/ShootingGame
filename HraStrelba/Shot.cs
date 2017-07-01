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
        public float CenterX
        {
            get
            {
                return X + size / 2;
            }
        }
        public float CenterY
        {
            get
            {
                return Y + size / 2;
            }
        }
        public const float size = 6;
        private float vX, vY;
        private float velocity = 6;
        private Color colour = Color.Aqua;
        /// <summary>
        /// Creates a new shot.
        /// </summary>
        /// <param name="x">Starting x coordinate</param>
        /// <param name="y">Starting y coordinate</param>
        /// <param name="centerX">X coordinate of the center of the player</param>
        /// <param name="centerY">Y coordinate of the center of the player</param>
        public Shot(float x, float y, float centerX, float centerY)
        {
            X = x - size / 2;
            Y = y - size / 2;
            vX = (x - centerX) * (velocity / Player.gunSize);
            vY = (y - centerY) * (velocity / Player.gunSize);
        }
        /// <summary>
        /// Draws the shot.
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr)
        {
            gr.FillEllipse(Brushes.Black, X, Y, size, size);
        }
        /// <summary>
        /// Moves the shot.
        /// </summary>
        public void Move()
        {
            X += vX;
            Y += vY;
        }
    }
}
