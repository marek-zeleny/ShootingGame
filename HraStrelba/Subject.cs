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
        public float CenterX { get { return X + Size / 2; } }
        public float CenterY { get { return Y + Size / 2; } }
        protected float Size { get; set; }
        protected float Velocity { get; set; }
        public int Hp { get; protected set; }
        protected int maxHp;
        protected Color colour;
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
            gr.FillEllipse(new SolidBrush(colour), X, Y, Size, Size);
            //Health bar
            float hpRate = 360 - ((float)Hp / maxHp) * 360;
            gr.FillPie(Brushes.Red, X + Size / 8, Y + Size / 8, Size * 3 / 4, Size * 3 / 4, 270, hpRate);
        }
        /// <summary>
        /// Moves the subject in a given direction.
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
