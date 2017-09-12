using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    class EnemyNormal: Enemy
    {
        public EnemyNormal(float x, float y, float size, float velocity, int damage, int hp, int scoreValue, Color colour)
            : base(x, y)
        {
            Size = size;
            Velocity = velocity;
            Damage = damage;
            MaxHp = hp;
            ScoreValue = scoreValue;
            Colour = colour;
        }
    }
}
