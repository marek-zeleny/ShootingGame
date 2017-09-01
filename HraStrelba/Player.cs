using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    class Player: Enemy
    {
        public float GunX { get; private set; }
        public float GunY { get; private set; }
        public const float gunSize = 30;
        private float shootingSpeed = 1F;
        private float reload = 0;
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
            this.xMax = xMax;
            this.yMax = yMax;
            MaxHp = 10;
            Hp = MaxHp;
            Size = 30;
            Velocity = 4;
            Colour = Color.Blue;
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
            float Dx = mouseX - X; //distance between mouse and player (center)
            float Dy = mouseY - Y;
            GunY = (Dy * gunSize) / (float)Math.Sqrt(Dx * Dx + Dy * Dy); //equation
            GunX = (float)Math.Sqrt(gunSize * gunSize - GunY * GunY);
            GunX += X; //position relative to the player (center)
            GunY += Y;
            if (Dx < 0) //correction of the consequence of squaring in the equation
                GunX = 2 * X - GunX;
            gr.DrawLine(Pens.Black, X, Y, GunX, GunY);
        }
        /// <summary>
        /// Moves the player in a given direction.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        public override void Move(bool right, bool left, bool up, bool down)
        {
            base.Move(right, left, up, down);
            if (X < Size / 2) //window border
                X = Size / 2;
            if (X > xMax - Size / 2)
                X = xMax - Size / 2;
            if (Y < Size / 2)
                Y = Size / 2;
            if (Y > yMax - Size / 2)
                Y = yMax - Size / 2;
        }
        /// <summary>
        /// Reloads the gun and eventually fires a shot.
        /// </summary>
        /// <param name="fire">Decides whether to fire or just keep the gun loaded</param>
        /// <returns>True to fire a shot.</returns>
        public bool Shoot(bool fire)
        {
            reload += shootingSpeed;
            if (reload >= 1 && fire)
            {
                reload = 0;
                return true;
            }
            return false;
        }
    }
}
