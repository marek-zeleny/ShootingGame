using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    /// <summary>
    /// Mother class for player, enemies, shots etc.
    /// </summary>
    class Object
    {
        /// <summary>
        /// X coordinate of the center of the object
        /// </summary>
        public float X { get; protected set; }
        /// <summary>
        /// X coordinate of the center of the object
        /// </summary>
        public float Y { get; protected set; }
        protected float CornerX { get { return X - Size / 2; } }
        protected float CornerY { get { return Y - Size / 2; } }
        public float Size { get; protected set; }
        public int Damage { get; protected set; }
        protected int hp;
        public int Hp { get { return hp; } protected set { hp = value; if (value > MaxHp) hp = MaxHp; } }
        protected int maxHp;
        public int MaxHp { get { return maxHp; } set { maxHp = value; Hp = maxHp; } }
        protected float Velocity { get; set; }
        protected Color Colour { get; set; }

        /// <summary>
        /// Creates a new object.
        /// </summary>
        /// <param name="x">Starting X coordinate</param>
        /// <param name="y">Starting Y coordinate</param>
        public Object(float x, float y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Draws the object.
        /// </summary>
        /// <param name="gr"></param>
        public virtual void Draw(Graphics gr)
        {
            gr.FillEllipse(new SolidBrush(Colour), CornerX, CornerY, Size, Size);
        }
        /// <summary>
        /// Moves the object in a given direction.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        public virtual void Move(bool right, bool left, bool up, bool down)
        {
            float v = Velocity;
            float Dx = 0;
            float Dy = 0;
            if ((right || left) && (up || down)) //same diagonal speed as horizontal/vertical
                v /= (float)Math.Sqrt(2);
            if (right)
                Dx += v;
            if (left)
                Dx -= v;
            if (up)
                Dy -= v;
            if (down)
                Dy += v;
            X += Dx;
            Y += Dy;
        }
        /// <summary>
        /// Checkes whether the object touches another object.
        /// </summary>
        /// <param name="obj">The other object being checked</param>
        /// <returns>True if the objects dies consequently</returns>
        public virtual bool TouchesAnotherObject(Object obj)
        {
            float distance = Methods.Distance(X, Y, obj.X, obj.Y);
            if (distance <= (Size + obj.Size) / 2)
                Hp -= obj.Damage;
            if (Hp <= 0)
                return true;
            return false;
        }
    }
}
