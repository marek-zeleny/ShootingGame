using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Enemy: Subject
    {
        public Enemy(float x, float y, float velocity)
            : base(x, y)
        {
            this.velocity = velocity;
            Hp = 8;
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
            bool right = (playerX - CenterX > velocity);
            bool left = (playerX - CenterX < -velocity);
            bool down = (playerY - CenterY > velocity);
            bool up = (playerY - CenterY < -velocity);
            base.Move(right, left, up, down, velocity);
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
            if (distance < size / 2 + Shot.size / 2)
            {
                Hp--;
                return true;
            }
            return false;
        }
    }
}
