using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    class Enemy: Subject
    {
        public int Hp { get; protected set; }
        protected int MaxHp { get { return maxHp; } set { maxHp = value; Hp = maxHp; } }
        protected int maxHp;

        public Enemy(float x, float y)
            : base(x, y)
        {
        }
        /// <summary>
        /// Draws the enemy.
        /// </summary>
        /// <param name="gr"></param>
        public override void Draw(Graphics gr)
        {
            base.Draw(gr);
            //Health bar
            float hpRate = 360 - ((float)Hp / MaxHp) * 360;
            gr.FillPie(Brushes.Red, CornerX + Size / 8, CornerY + Size / 8, Size * 3 / 4, Size * 3 / 4, 270, hpRate);
        }
        /// <summary>
        /// Moves the enemy.
        /// </summary>
        /// <param name="playerX">X coordinate of player's current position</param>
        /// <param name="playerY">Y coordinate of player's current position</param>
        public void Move(float playerX, float playerY)
        {
            bool right = (playerX - X > Velocity);
            bool left = (playerX - X < -Velocity);
            bool down = (playerY - Y > Velocity);
            bool up = (playerY - Y < -Velocity);
            base.Move(right, left, up, down);
        }
        /// <summary>
        /// Checks whether the enemy got hit by a player's shot.
        /// </summary>
        /// <param name="shotCenterX">X coordinate of the center of the shot</param>
        /// <param name="shotCenterY">Y coordinate of the center of the shot</param>
        /// <returns>True if the enemy got hit</returns>
        public virtual bool Hit(Subject subject)
        {
            float distance = Methods.Distance(X, Y, subject.X, subject.Y);
            if (distance <= Size / 2 + subject.Size / 2)
            {
                Hp -= subject.Damage;
                return true;
            }
            return false;
        }
    }
}
