using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Shot: Subject
    {
        public const float size = 6;
        public const float velocity = 6;
        private float vX, vY;
        /// <summary>
        /// Creates a new shot.
        /// </summary>
        /// <param name="x">Starting x coordinate</param>
        /// <param name="y">Starting y coordinate</param>
        /// <param name="playerCenterX">X coordinate of the center of the player</param>
        /// <param name="playerCenterY">Y coordinate of the center of the player</param>
        public Shot(float x, float y, float playerCenterX, float playerCenterY)
            : base(x, y)
        {
            Size = size;
            Velocity = velocity;
            colour = Color.Aqua;
            X = x - size / 2;
            Y = y - size / 2;
            vX = (x - playerCenterX) * (Velocity / Player.gunSize);
            vY = (y - playerCenterY) * (Velocity / Player.gunSize);
        }
        /// <summary>
        /// Draws the shot.
        /// </summary>
        /// <param name="gr"></param>
        public override void Draw(Graphics gr)
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
