using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Timers;

namespace ShootingGame
{
    class Player: Enemy
    {
        public float GunX { get; private set; }
        public float GunY { get; private set; }
        public const float gunSize = 30;

        private float shootingSpeed = 0.35F;
        private float reload = 0;
        public int Ammo { get; private set; }
        public int Score { get; private set; }

        public List<string> ActiveBonuses { get; private set; }
        public List<int> ActiveBonusesExpireTime { get; private set; }
        public string LastActivatedBonus { get; private set; }

        private Timer TimerBonus;
        public int BonusDuration { get; private set; }

        public float VelocityModifier { get; private set; }

        private int xMax; //borders of the client
        private int yMax;

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

            ActiveBonuses = new List<string>();
            ActiveBonusesExpireTime = new List<int>();
            LastActivatedBonus = "";

            VelocityModifier = 1;
            BonusDuration = 8000; //time in milliseconds
            TimerBonus = new Timer(100);
            TimerBonus.Elapsed += new ElapsedEventHandler(TimerBonus_Tick);
            TimerBonus.Enabled = true;

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
        /// Adds the effect of a picked up bonus.
        /// </summary>
        /// <param name="bonus">Instance of the bonus</param>
        private void NewBonusEffect(Bonus bonus)
        {
            LastActivatedBonus = bonus.Effect;
            bool requiresTimer = false;
            switch (bonus.Effect)
            {
                case "Extra Ammo":
                    Ammo += 10;
                    break;
                case "Rapid Fire":
                    shootingSpeed = 1;
                    requiresTimer = true;
                    break;
                case "Shotgun":
                    requiresTimer = true;
                    break;
                case "Heal":
                    Hp += 1;
                    break;
                case "Slow Motion":
                    VelocityModifier = 0.6F;
                    requiresTimer = true;
                    break;
                default:
                    break;
            }
            if (requiresTimer)
            {
                if (ActiveBonuses.Contains(LastActivatedBonus))
                    ActiveBonusesExpireTime[ActiveBonuses.IndexOf(LastActivatedBonus)] = BonusDuration;
                else
                {
                    ActiveBonuses.Insert(0, bonus.Effect);
                    ActiveBonusesExpireTime.Insert(0, BonusDuration);
                }
            }
        }
        /// <summary>
        /// Cancles the effect of an expired bonus.
        /// </summary>
        private void BonusEffectExpire(string effect)
        {
            switch (effect)
            {
                case "Rapid Fire":
                    shootingSpeed = 0.35F;
                    break;
                case "Slow Motion":
                    VelocityModifier = 1;
                    break;
                default:
                    break;
            }
            int i = ActiveBonuses.IndexOf(effect);
            ActiveBonuses.RemoveAt(i);
            ActiveBonusesExpireTime.RemoveAt(i);
        }

        private void TimerBonus_Tick(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < ActiveBonusesExpireTime.Count; i++)
                ActiveBonusesExpireTime[i] -= 100;
            for (int i = 0; i < ActiveBonusesExpireTime.Count; i++)
                if (ActiveBonusesExpireTime[i] <= 0)
                    BonusEffectExpire(ActiveBonuses[i]);
        }
        /// <summary>
        /// Applies a change in the client size.
        /// </summary>
        /// <param name="width">New width of the client</param>
        /// <param name="height">New height of the client</param>
        public void SetClientSize(int width, int height)
        {
            xMax = width;
            yMax = height;
        }
    }
}
