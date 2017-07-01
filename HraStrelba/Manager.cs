using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HraStrelba
{
    class Manager
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private Player player;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Shot> shots = new List<Shot>();

        private List<float> historyX = new List<float>();
        private List<float> historyY = new List<float>();

        private float mouseX;
        private float mouseY;

        private bool right = false;
        private bool left = false;
        private bool up = false;
        private bool down = false;

        private Random r = new Random();

        public Manager(int width, int height)
        {
            Width = width;
            Height = height;
        }

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
            }
        }

        public void Aim(float x, float y)
        {
            mouseX = x;
            mouseY = y;
        }

        public void Shoot()
        {
            Shot s = new Shot(player.GunX, player.GunY, player.X + (int)(Player.size / 2), player.Y + (int)(Player.size / 2), (int)Player.size);
            shots.Add(s);
        }

        public void Movement()
        {
            player.Move(right, left, up, down);
            historyX.RemoveAt(historyX.Count - 1);
            historyY.RemoveAt(historyY.Count - 1);
            historyX.Insert(0, player.X);
            historyY.Insert(0, player.Y);
            foreach (Enemy s in enemies)
            {
                int vzdalenost = (int)Math.Sqrt((player.X - s.X) * (player.X - s.X) + (player.Y - s.Y) * (player.Y - s.Y));
                int i = vzdalenost / 10 - 1;
                if (i > 100)
                    i = 100;
                else if (i < 0)
                    i = 0;
                s.Move(historyX[i], historyY[i]);
            }

            List<Shot> smazatStrely = new List<Shot>();
            foreach (Shot s in shots)
            {
                s.Move();
                if (s.X < -Shot.size || s.Y < -Shot.size || s.X > Width || s.Y > Height)
                    smazatStrely.Add(s);
            }

            List<Enemy> smazatSoupere = new List<Enemy>();
            foreach (Enemy souper in enemies)
            {
                foreach (Shot strela in shots)
                    if (souper.Hit(strela.X, strela.Y))
                        smazatStrely.Add(strela);
                if (souper.Hp <= 0)
                    smazatSoupere.Add(souper);
            }

            foreach (Shot s in smazatStrely)
                shots.Remove(s);
            foreach (Enemy s in smazatSoupere)
                enemies.Remove(s);
        }

        public void Paint(Graphics g)
        {
            foreach (Enemy s in enemies)
                s.Draw(g);
            foreach (Shot s in shots)
                s.Draw(g);
            player.Draw(g, mouseX, mouseY);
        }

        public void Load()
        {
            int x = r.Next(Width - (int)Player.size);
            int y = r.Next(Height - (int)Player.size);
            player = new Player(x, y, Width, Height, Color.Blue);
            for (int i = 0; i < 100; i++)
            {
                historyX.Insert(0, x);
                historyY.Insert(0, y);
            }

            float v = 2; //rychlost
            for (int i = 0; i < 5; i++)
            {
                x = r.Next(Width - (int)Player.size);
                y = r.Next(Height - (int)Player.size);
                enemies.Add(new Enemy(x, y, Width, Height, Color.Yellow, v, 5));
                v += 0.6F;
            }
        }
    }
}
