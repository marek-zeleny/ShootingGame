using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    class Bonus: Object
    {
        public string Type { get; private set; }

        public Bonus(float x, float y)
            : base(x, y)
        {
            Size = 25;
            Damage = 0;
            Colour = Color.Green;
            SelectType();
        }

        private void SelectType()
        {
            Random r = new Random();
            int i = r.Next(5);

            switch (i)
            {
                case 0:
                    Type = "Extra Ammo";
                    break;
                case 1:
                    Type = "Rapid Fire";
                    break;
                case 2:
                    Type = "Shotgun";
                    break;
                case 3:
                    Type = "Heal";
                    break;
                case 4:
                    Type = "";
                    break;
            }
        }
    }
}
