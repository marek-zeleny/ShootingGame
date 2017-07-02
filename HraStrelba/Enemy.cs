using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Enemy: Subject
    {
        public const float size = 30;
        public const int hp = 5;

        public Enemy(float x, float y, float velocity)
            : base(x, y)
        {
            this.Velocity = velocity;
            Size = size;
            Hp = hp;
            maxHp = Hp;
            colour = Color.Yellow;
        }
        /// <summary>
        /// Moves the enemy.
        /// </summary>
        /// <param name="playerX">X coordinate of player's current position</param>
        /// <param name="playerY">Y coordinate of player's current position</param>
        public void Move(float playerX, float playerY)
        {
            bool right = (playerX - CenterX > Velocity);
            bool left = (playerX - CenterX < -Velocity);
            bool down = (playerY - CenterY > Velocity);
            bool up = (playerY - CenterY < -Velocity);
            base.Move(right, left, up, down);
        }
        /// <summary>
        /// Checks whether the enemy got hit by a player's shot.
        /// </summary>
        /// <param name="shotCenterX">X coordinate of the center of the shot</param>
        /// <param name="shotCenterY">Y coordinate of the center of the shot</param>
        /// <returns>True if the enemy got hit</returns>
        public bool Hit(float shotCenterX, float shotCenterY)
        {
            float distance = Methods.Distance(CenterX, CenterY, shotCenterX, shotCenterY);
            if (distance < Size / 2 + Shot.size / 2)
            {
                Hp--;
                return true;
            }
            return false;
        }
    }
}
