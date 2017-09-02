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
        public float CornerX { get { return X - Size / 2; } }
        public float CornerY { get { return Y - Size / 2; } }
        public float Size { get; protected set; }
        public int Damage { get; protected set; }
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
    }
}
