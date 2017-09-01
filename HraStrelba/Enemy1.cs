using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    class Enemy1: Enemy
    {
        public Enemy1(float x, float y)
            : base(x, y)
        {
            Size = 30;
            Velocity = 3;
            Damage = 1;
            MaxHp = 5;
            Colour = Color.Yellow;
        }
    }
}
