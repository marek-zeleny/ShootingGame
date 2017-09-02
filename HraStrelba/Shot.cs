using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    class Shot: Object
    {
        private float vX, vY;

        /// <summary>
        /// Creates a new shot.
        /// </summary>
        /// <param name="x">Starting x coordinate</param>
        /// <param name="y">Starting y coordinate</param>
        /// <param name="playerX">X coordinate of the center of the player</param>
        /// <param name="playerY">Y coordinate of the center of the player</param>
        public Shot(float x, float y, float playerX, float playerY)
            : base(x, y)
        {
            Size = 6;
            Velocity = 6;
            Damage = 1;
            Colour = Color.Black;
            vX = (X - playerX) * (Velocity / Player.gunSize);
            vY = (Y - playerY) * (Velocity / Player.gunSize);
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
