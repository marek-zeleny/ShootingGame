using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    /// <summary>
    /// Mother class for player and enemies
    /// </summary>
    class Subject
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        public float CenterX
        {
            get
            {
                return X + size / 2;
            }
        }
        public float CenterY
        {
            get
            {
                return Y + size / 2;
            }
        }
        public int Hp { get; protected set; }
        protected int maxHp;
        protected float velocity;
        protected Color colour;
        public const float size = 30;
        /// <summary>
        /// Creates a new subject.
        /// </summary>
        /// <param name="x">Starting X coordinate</param>
        /// <param name="y">Starting Y coordinate</param>
        public Subject(float x, float y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Draws the subject.
        /// </summary>
        /// <param name="gr"></param>
        public virtual void Draw(Graphics gr)
        {
            gr.FillEllipse(new SolidBrush(colour), X, Y, size, size);
            //Health bar
            float hpRate = 360 - ((float)Hp / maxHp) * 360;
            gr.FillPie(Brushes.Red, X + size / 8, Y + size / 8, size * 3 / 4, size * 3 / 4, 270, hpRate);
        }
        /// <summary>
        /// Moves the subject in a given direction with a given speed.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        /// <param name="v">Velocity of the subject</param>
        protected void Move(bool right, bool left, bool up, bool down, float v)
        {
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
