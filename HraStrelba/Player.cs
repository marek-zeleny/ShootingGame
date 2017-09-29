﻿using System;
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
        private float shootingSpeed = 0.3F;
        private float reload = 0;
        public int Ammo { get; private set; }
        public int Score { get; private set; }
        public Bonus ActiveBonus { get; private set; }
        public int XMax { get; set; } //borders of the client
        public int YMax { get; set; }

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
            Size = 30;
            Velocity = 4;
            Damage = 100;
            MaxHp = 10;
            Ammo = 10;
            Score = 0;
            Colour = Color.Blue;
            XMax = xMax;
            YMax = yMax;
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
        public override void Move(bool right, bool left, bool up, bool down, float velocityModifier)
        {
            base.Move(right, left, up, down, velocityModifier);
            if (X < Size / 2) //window border
                X = Size / 2;
            if (X > XMax - Size / 2)
                X = XMax - Size / 2;
            if (Y < Size / 2)
                Y = Size / 2;
            if (Y > YMax - Size / 2)
                Y = YMax - Size / 2;
        }
        /// <summary>
        /// Reloads the gun and eventually fires a shot.
        /// </summary>
        /// <param name="fire">Decides whether to fire or just keep the gun loaded</param>
        /// <returns>True to fire a shot.</returns>
        public bool Shoot(bool fire)
        {
            reload += shootingSpeed;
            if (reload >= 1 && Ammo > 0 && fire)
            {
                reload = 0;
                Ammo--;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks wheter the player touches another object.
        /// </summary>
        /// <param name="obj">The other object being checked</param>
        /// <returns>True if the player dies consequently</returns>
        public override bool TouchesAnotherObject(Object obj)
        {
            float distance = Methods.Distance(X, Y, obj.X, obj.Y);
            if (obj is Bonus)
                if (distance <= (Size + obj.Size) / 2)
                {
                    Bonus bonus = (Bonus)obj;
                    NewBonusEffect(bonus);
                }
            if (obj is Enemy)
                if (distance <= (Size + obj.Size) / 2)
                {
                    Hp -= obj.Damage;
                    Ammo += 1;
                }
            if (Hp <= 0)
                return true;
            return false;
        }
        /// <summary>
        /// Increases score and ammo when an enemy is killed.
        /// </summary>
        /// <param name="enemy">Killed enemy</param>
        public void EnemyKilled(Enemy enemy)
        {
            Score += enemy.ScoreValue;
            Ammo += enemy.MaxHp + 1;
        }
        /// <summary>
        /// Adds an effect of a picked up bonus.
        /// </summary>
        /// <param name="bonus">Instance of the bonus</param>
        private void NewBonusEffect(Bonus bonus)
        {
            ActiveBonus = bonus;
            switch (ActiveBonus.Effect)
            {
                case "Extra Ammo":
                    Ammo += 10;
                    break;
                case "Rapid Fire":
                    shootingSpeed = 1;
                    break;
                case "Shotgun":
                    break;
                case "Heal":
                    Hp += 1;
                    break;
                case "Slow Motion":
                    break;
                default:
                    break;
            }
        }

        public void BonusEffectExpire()
        {
            switch (ActiveBonus.Effect)
            {
                case "Rapid Fire":
                    shootingSpeed = 0.3F;
                    break;
                case "Shotgun":
                    break;
                case "Slow Motion":
                    break;
                default:
                    break;
            }

        }
    }
}
