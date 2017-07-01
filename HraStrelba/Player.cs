using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Player: Subject
    {
        public float GunX { get; private set; }
        public float GunY { get; private set; }
        public const float gunSize = 30;
        protected int xMax, yMax; //borders of the client
        /// <summary>
        /// Creates a new player.
        /// </summary>
        /// <param name="x">Starting X coordinate</param>
        /// <param name="y">Starting Y coordinate</param>
        /// <param name="xMax">Width of the client</param>
        /// <param name="yMax">Height of the client</param>
        public Player(float x, float y, int xMax, int yMax)
            : base(x, y)
        {
            velocity = 4;
            Hp = 30;
            maxHp = Hp;
            colour = Color.Blue;
            this.xMax = xMax;
            this.yMax = yMax;
        }
        /// <summary>
        /// Draws the player.
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="mouseX">X coordinate of the current cursor position (relative to the client)</param>
        /// <param name="mouseY">Y coordinate of the current cursor position (relative to the client)</param>
        public void Draw(Graphics gr, float mouseX, float mouseY)
        {
            base.Draw(gr);
            //aimer
            float Dx = mouseX - CenterX; //distance between mouse and player (center)
            float Dy = mouseY - CenterY;
            GunY = (Dy * gunSize) / (float)Math.Sqrt(Dx * Dx + Dy * Dy); //equation
            GunX = (float)Math.Sqrt(gunSize * gunSize - GunY * GunY);
            GunX += CenterX; //position relative to the player (center)
            GunY += CenterY;
            if (Dx < 0) //correction of the consequence of squaring in the equation
                GunX = 2 * CenterX - GunX;
            gr.DrawLine(Pens.Black, CenterX, CenterY, GunX, GunY);
        }
        /// <summary>
        /// Moves the player in a given direction.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        public void Move(bool right, bool left, bool up, bool down)
        {
            base.Move(right, left, up, down, velocity);
            if (X < 0) //window border
                X = 0;
            if (X + size > xMax)
                X = xMax - size;
            if (Y < 0)
                Y = 0;
            if (Y + size > yMax)
                Y = yMax - size;
        }
    }
}
