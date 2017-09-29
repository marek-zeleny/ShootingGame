using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    class Enemy: Object
    {
        public int ScoreValue { get; protected set; }

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
        /// Moves the enemy towards or away from a given point.
        /// </summary>
        /// <param name="playerX">X coordinate of the guiding point</param>
        /// <param name="playerY">Y coordinate of the guiding point</param>
        /// <param name="towards">True for moving towards the point, fals for moving away</param>
        public void Move(float playerX, float playerY, bool towards, float velocityModifier)
        {
            bool right = (playerX - X > Velocity);
            bool left = (playerX - X < -Velocity);
            bool down = (playerY - Y > Velocity);
            bool up = (playerY - Y < -Velocity);
            if (towards)
                base.Move(right, left, up, down, velocityModifier);
            else
                base.Move(!right, !left, !up, !down, velocityModifier);
        }

        public override bool TouchesAnotherObject(Object obj)
        {
            float distance = Methods.Distance(X, Y, obj.X, obj.Y);
            if (obj is Enemy && !(obj is Player))
            {
                if (distance < (Size + obj.Size) / 2)
                    Move(obj.X, obj.Y, false, 1);
                return false;
            }
            return base.TouchesAnotherObject(obj);
        }
    }
}
