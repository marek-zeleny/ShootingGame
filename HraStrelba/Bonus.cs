using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    class Bonus: Object
    {
        public const int size = 25;
        public string Effect { get; private set; }
        /// <summary>
        /// Creates a new bonus.
        /// </summary>
        /// <param name="x">Starting X coordinate</param>
        /// <param name="y">Starting Y coordinate</param>
        public Bonus(float x, float y)
            : base(x, y)
        {
            Size = size;
            Damage = 0;
            MaxHp = 1;
            Colour = Color.Green;
            SelectEffect();
        }
        /// <summary>
        /// Randomly selects an effect granted by the bonus.
        /// </summary>
        private void SelectEffect()
        {
            Random r = new Random();
            int i = r.Next(5);

            switch (i)
            {
                case 0:
                    Effect = "Extra Ammo";
                    break;
                case 1:
                    Effect = "Rapid Fire";
                    break;
                case 2:
                    Effect = "Shotgun";
                    break;
                case 3:
                    Effect = "Heal";
                    break;
                case 4:
                    Effect = "Slow Motion";
                    break;
            }
        }
    }
}
