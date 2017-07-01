using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    /// <summary>
    /// Class managing objects and their processes and ensuring communication with the form
    /// </summary>
    class Manager
    {
        /// <summary>
        /// Width of the client
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of the client
        /// </summary>
        public int Height { get; set; }

        private Player player;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Shot> shots = new List<Shot>();
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

        private Random r = new Random();
        /// <summary>
        /// Creates an instance of the manager.
        /// </summary>
        /// <param name="width">Width of the client</param>
        /// <param name="height">Height of the client</param>
        public Manager(int width, int height)
        {
            Width = width;
            Height = height;
        }
        /// <summary>
        /// Prepares everything necessary to start the game.
        /// </summary>
        public void Load()
        {
            int x = r.Next(Width - (int)Player.size);
            int y = r.Next(Height - (int)Player.size);
            player = new Player(x, y, Width, Height);
            for (int i = 0; i < 100; i++)
            {
                playerHistoryX.Insert(0, x);
                playerHistoryY.Insert(0, y);
            }

            float v = 2; //velocity
            for (int i = 0; i < 4; i++)
            {
                x = r.Next(Width - (int)Player.size);
                y = r.Next(Height - (int)Player.size);
                enemies.Add(new Enemy(x, y, v));
                v += 0.6F;
            }
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
            Shot s = new Shot(player.GunX, player.GunY, player.CenterX, player.CenterY);
            shots.Add(s);
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
            playerHistoryX.Insert(0, player.CenterX);
            playerHistoryY.Insert(0, player.CenterY);
            foreach (Enemy e in enemies)
            {
                int i = (int)(Methods.Distance(player.CenterX, player.CenterY, e.CenterX, e.CenterY) / 10) - 1;
                if (i > 100)
                    i = 100;
                else if (i < 0)
                    i = 0;
                e.Move(playerHistoryX[i], playerHistoryY[i]);
            }

            List<Shot> deleteShots = new List<Shot>();
            foreach (Shot s in shots)
            {
                s.Move();
                if (s.X < -Shot.size || s.Y < -Shot.size || s.X > Width || s.Y > Height)
                    deleteShots.Add(s);
            }

            List<Enemy> deleteEnemies = new List<Enemy>();
            foreach (Enemy e in enemies)
            {
                foreach (Shot s in shots)
                    if (e.Hit(s.CenterX, s.CenterY))
                        deleteShots.Add(s);
                if (e.Hp <= 0)
                    deleteEnemies.Add(e);
            }

            foreach (Shot s in deleteShots)
                shots.Remove(s);
            foreach (Enemy e in deleteEnemies)
                enemies.Remove(e);
        }
    }
}
