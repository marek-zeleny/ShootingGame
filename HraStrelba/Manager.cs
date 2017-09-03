using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShootingGame
{
    /// <summary>
    /// Class managing objects and ensuring communication with the form
    /// </summary>
    class Manager
    {
        /// <summary>
        /// Width of the client
        /// </summary>
        private int Width { get; set; }
        /// <summary>
        /// Height of the client
        /// </summary>
        private int Height { get; set; }

        private Form1 form;

        private int score = 0;
        private int level = 1;
        private Player player;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Shot> shots = new List<Shot>();
        private List<Bonus> bonus = new List<Bonus>();
        //previous positions of the player
        private List<float> playerHistoryX = new List<float>();
        private List<float> playerHistoryY = new List<float>();
        //current mouse position
        private float mouseX;
        private float mouseY;
        //player movement
        private bool right = false;
        private bool left = false;
        private bool up = false;
        private bool down = false;

        public bool shootingEnabled = false;

        private Random r = new Random();

        /// <summary>
        /// Creates an instance of the manager and prepares the game.
        /// </summary>
        /// <param name="width">Width of the client</param>
        /// <param name="height">Height of the client</param>
        /// <param name="form">Instance of the active form</param>
        public Manager(int width, int height, Form1 form)
        {
            Width = width;
            Height = height;
            this.form = form;
            Load();
        }
        /// <summary>
        /// Applies a change in the client size.
        /// </summary>
        /// <param name="width">New width of the client</param>
        /// <param name="height">New height of the client</param>
        public void ClientSize(int width, int height)
        {
            Width = width;
            Height = height;
            player.XMax = width;
            player.YMax = height;
        }
        /// <summary>
        /// Prepares everything necessary to start the game.
        /// </summary>
        private void Load()
        {
            int x = Width / 2;
            int y = Height / 2;
            player = new Player(x, y, Width, Height);
            for (int i = 0; i < 100; i++)
            {
                playerHistoryX.Insert(0, x);
                playerHistoryY.Insert(0, y);
            }
            NextLevel();
        }
        /// <summary>
        /// Sets up next level of the game.
        /// </summary>
        public void NextLevel()
        {
            int x = r.Next(Width);
            int y = r.Next(Height);

            bonus.Add(new Bonus(x, y));

            int count = 0;
            float size = 0;
            float velocity = 0;
            int damage = 0;
            int hp = 0;
            int scoreValue = 0;
            Color colour = Color.White;

            if (level == 1)
            {
                count = 4;
                size = 30;
                velocity = 3;
                damage = 1;
                hp = 5;
                scoreValue = 7;
                colour = Color.Yellow;
            }

            if (level == 2)
            {
                count = 3;
                size = 25;
                velocity = 2.5F;
                damage = 3;
                hp = 8;
                scoreValue = 10;
                colour = Color.Violet;
            }

            for (int i = 0; i < count; i++)
            {
                x = r.Next(Width);
                y = r.Next(Height);
                enemies.Add(new Enemy1(x, y, size, velocity, damage, hp, scoreValue, colour));
            }

            level++;
        }
        /// <summary>
        /// Processes a key press/release.
        /// </summary>
        /// <param name="key">Key value</param>
        /// <param name="type">True for press, false for release</param>
        public void Key(ConsoleKey key, bool type)
        {
            switch (key)
            {
                case ConsoleKey.D:
                    right = type;
                    break;
                case ConsoleKey.A:
                    left = type;
                    break;
                case ConsoleKey.W:
                    up = type;
                    break;
                case ConsoleKey.S:
                    down = type;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Reacts to the current possition of the cursor.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void Aim(float x, float y)
        {
            mouseX = x;
            mouseY = y;
        }
        /// <summary>
        /// Fires a new shot from the players gun.
        /// </summary>
        public void Shoot()
        {
            if (player.Shoot(shootingEnabled))
            {
                Shot s = new Shot(player.GunX, player.GunY, player.X, player.Y);
                shots.Add(s);
            }
        }
        /// <summary>
        /// Refreshes information about the currently played game.
        /// </summary>
        /// <returns>Player's score and ammo as string.</returns>
        public string Info()
        {
            string s = String.Format("Score: {0}\nAmmo: {1}", score, player.Ammo);
            return s;
        }
        /// <summary>
        /// Draws all objects on the form.
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            foreach (Enemy e in enemies)
                e.Draw(g);
            foreach (Shot s in shots)
                s.Draw(g);
            foreach (Bonus b in bonus)
                b.Draw(g);
            player.Draw(g, mouseX, mouseY);
        }
        /// <summary>
        /// Processes the movement of all objects in the game.
        /// </summary>
        public void Movement()
        {
            player.Move(right, left, up, down);
            playerHistoryX.RemoveAt(playerHistoryX.Count - 1);
            playerHistoryY.RemoveAt(playerHistoryY.Count - 1);
            playerHistoryX.Insert(0, player.X);
            playerHistoryY.Insert(0, player.Y);
            foreach (Enemy e in enemies)
            {
                int i = (int)(Methods.Distance(player.X, player.Y, e.X, e.Y) / 10) - 1;
                if (i > 99)
                    i = 99;
                else if (i < 0)
                    i = 0;
                e.Move(playerHistoryX[i], playerHistoryY[i]);
            }

            List<Enemy> deleteEnemies = new List<Enemy>();
            List<Shot> deleteShots = new List<Shot>();
            List<Bonus> deleteBonus = new List<Bonus>();

            foreach (Shot s in shots)
            {
                s.Move();
                if (s.X < -s.Size || s.Y < -s.Size || s.X > Width + s.Size || s.Y > Height + s.Size)
                    deleteShots.Add(s);
            }

            foreach (Enemy e in enemies)
            {
                foreach (Shot s in shots)
                    if (e.Hit(s))
                        deleteShots.Add(s);
                if (e.Hp <= 0)
                {
                    deleteEnemies.Add(e);
                    score += e.ScoreValue;
                    player.Ammo += e.ScoreValue;
                }
            }

            foreach (Enemy e in enemies)
                if (player.Hit(e))
                    deleteEnemies.Add(e);

            foreach (Bonus b in bonus)
                if (player.Hit(b))
                {
                    player.AddBonus(b.Type);
                    form.Bonus(b.Type);
                    deleteBonus.Add(b);
                }

            foreach (Enemy e in deleteEnemies)
                enemies.Remove(e);
            foreach (Shot s in deleteShots)
                shots.Remove(s);
            foreach (Bonus b in deleteBonus)
                bonus.Remove(b);
        }
    }
}
