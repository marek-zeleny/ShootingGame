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
    class GameManager
    {
        /// <summary>
        /// Width of the client (form)
        /// </summary>
        private int FormWidth { get; set; }
        /// <summary>
        /// Height of the client (form)
        /// </summary>
        private int FormHeight { get; set; }

        private FormGame form;

        private int level = 1;
        private Player player;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Shot> shots = new List<Shot>();
        private List<Bonus> bonuses = new List<Bonus>();
        //previous positions of the player
        private List<float> playerHistoryX = new List<float>();
        private List<float> playerHistoryY = new List<float>();
        //current mouse position
        private float mouseX;
        private float mouseY;
        //player movement
        private bool moveRight = false;
        private bool moveLeft = false;
        private bool moveUp = false;
        private bool moveDown = false;

        private float velocityModifier = 1;
        public bool shootingEnabled = false;

        private Random random = new Random();

        /// <summary>
        /// Creates an instance of the manager and prepares the game.
        /// </summary>
        /// <param name="width">Width of the client</param>
        /// <param name="height">Height of the client</param>
        /// <param name="form">Instance of the active form</param>
        public GameManager(int width, int height, FormGame form)
        {
            FormWidth = width;
            FormHeight = height;
            this.form = form;
            LoadGame();
        }
        /// <summary>
        /// Applies a change in the client size.
        /// </summary>
        /// <param name="width">New width of the client</param>
        /// <param name="height">New height of the client</param>
        public void SetClientSize(int width, int height)
        {
            FormWidth = width;
            FormHeight = height;
            player.XMax = width;
            player.YMax = height;
        }
        /// <summary>
        /// Prepares everything necessary to start the game.
        /// </summary>
        private void LoadGame()
        {
            int x = FormWidth / 2;
            int y = FormHeight / 2;
            player = new Player(x, y, FormWidth, FormHeight);
            for (int i = 0; i < 100; i++)
            {
                playerHistoryX.Insert(0, x);
                playerHistoryY.Insert(0, y);
            }
            NextLevel();
        }
        /// <summary>
        /// Ends the game and returns to the menu.
        /// </summary>
        public void GameOver()
        {
            form.GameOver(player.Score, level);
        }
        /// <summary>
        /// Refreshes information about the currently played game.
        /// </summary>
        /// <returns>Player's score and ammo as string.</returns>
        public string GetScoreAmmoHpInfo()
        {
            string s = String.Format("Score: {0}\nAmmo: {1}\nHp: {2}", player.Score, player.Ammo, player.Hp);
            return s;
        }
        /// <summary>
        /// Sets up next level of the game.
        /// </summary>
        public void NextLevel()
        {
            int[] xy;
            // add bonus
            int x = random.Next(FormWidth);
            int y = random.Next(FormHeight);
            bonuses.Add(new Bonus(x, y));

            int enemyCount, damage, hp, scoreValue;
            float size, velocity;
            Color colour;
            // set up enemies for each level
            switch (level)
            {
                default:
                    enemyCount = 8;
                    size = 30;
                    velocity = 3.6F;
                    damage = 2;
                    hp = 3;
                    scoreValue = 8;
                    colour = Color.Orange;
                    break;
                case 1:
                    enemyCount = 4;
                    size = 30;
                    velocity = 3;
                    damage = 1;
                    hp = 5;
                    scoreValue = 5;
                    colour = Color.Yellow;
                    break;
                case 2:
                    enemyCount = 3;
                    size = 25;
                    velocity = 2.5F;
                    damage = 3;
                    hp = 7;
                    scoreValue = 7;
                    colour = Color.Violet;
                    break;
            }
            // generating the enemies
            for (int i = 0; i < enemyCount; i++)
            {
                xy = Methods.GetBorderPosition(FormWidth, FormHeight, random);
                x = xy[0];
                y = xy[1];
                enemies.Add(new EnemyNormal(x, y, size, velocity, damage, hp, scoreValue, colour));
            }

            form.WriteInfo("Level " + level);
            level++;
        }
        /// <summary>
        /// Processes a key press/release.
        /// </summary>
        /// <param name="key">Key value</param>
        /// <param name="pressOrRelease">True for press, false for release</param>
        public void KeyPressRelease(ConsoleKey key, bool pressOrRelease)
        {
            switch (key)
            {
                case ConsoleKey.D:
                    moveRight = pressOrRelease;
                    break;
                case ConsoleKey.A:
                    moveLeft = pressOrRelease;
                    break;
                case ConsoleKey.W:
                    moveUp = pressOrRelease;
                    break;
                case ConsoleKey.S:
                    moveDown = pressOrRelease;
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
        public void SetMousePosition(float x, float y)
        {
            mouseX = x;
            mouseY = y;
        }
        /// <summary>
        /// Fires a new shot from the player's gun.
        /// </summary>
        public void PlayerShoot()
        {
            if (player.Shoot(shootingEnabled))
            {
                Shot s = new Shot(player.GunX, player.GunY, player.X, player.Y);
                shots.Add(s);
            }
        }

        public void NewBonus()
        {
            string effect = player.ActiveBonus.Effect;
            string temporaryBonuses = "Rapid Fire Shotgun Slow Motion";
            form.WriteInfo("Bonus: " + effect);

            if (effect == "Slow Motion")
                velocityModifier = 0.5F;
            if (temporaryBonuses.Contains(effect))
                form.StartTimer("Bonus");
        }

        public void BonusExpire()
        {
            velocityModifier = 1;
            player.BonusEffectExpire();
        }
        /// <summary>
        /// Draws all objects on the form.
        /// </summary>
        /// <param name="g"></param>
        public void PaintAll(Graphics g)
        {
            foreach (Enemy e in enemies)
                e.Draw(g);
            foreach (Shot s in shots)
                s.Draw(g);
            foreach (Bonus b in bonuses)
                b.Draw(g);
            player.Draw(g, mouseX, mouseY);
        }
        /// <summary>
        /// Processes the movement of all objects in the game.
        /// </summary>
        public void MoveAll()
        {
            player.Move(moveRight, moveLeft, moveUp, moveDown, velocityModifier);
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
                e.Move(playerHistoryX[i], playerHistoryY[i], true, velocityModifier);
            }

            List<Shot> deleteShots = new List<Shot>();
            foreach (Shot shot in shots)
            {
                shot.Move(velocityModifier);
                if (shot.X < -shot.Size || shot.Y < -shot.Size || shot.X > FormWidth + shot.Size || shot.Y > FormHeight + shot.Size)
                    deleteShots.Add(shot);
            }
            foreach (Shot shot in deleteShots)
                shots.Remove(shot);

            CheckContactBetweenObjects();
        }
        /// <summary>
        /// Checks whether objects touch each other and if so, starts required operations.
        /// </summary>
        private void CheckContactBetweenObjects()
        {
            List<Enemy> deleteEnemies = new List<Enemy>();
            List<Shot> deleteShots = new List<Shot>();
            List<Bonus> deleteBonuses = new List<Bonus>();

            for (int i = enemies.Count - 1; i > 0; i--)
                for (int y = 0; y < i; y++)
                    enemies[i].TouchesAnotherObject(enemies[y]);


            foreach (Enemy enemy in enemies)
            {
                foreach (Shot shot in shots)
                {
                    if (shot.TouchesAnotherObject(enemy))
                        deleteShots.Add(shot);
                    if (enemy.TouchesAnotherObject(shot))
                    {
                        deleteEnemies.Add(enemy);
                        player.EnemyKilled(enemy);
                    }
                }
            }

            foreach (Enemy enemy in enemies)
            {
                if (player.TouchesAnotherObject(enemy))
                    GameOver();
                if (enemy.TouchesAnotherObject(player))
                    deleteEnemies.Add(enemy);
            }

            foreach (Bonus bonus in bonuses)
            {
                player.TouchesAnotherObject(bonus);
                if (bonus.TouchesAnotherObject(player))
                {
                    deleteBonuses.Add(bonus);
                    NewBonus();
                }
            }

            foreach (Enemy enemy in deleteEnemies)
                enemies.Remove(enemy);
            foreach (Shot shot in deleteShots)
                shots.Remove(shot);
            foreach (Bonus bonus in deleteBonuses)
                bonuses.Remove(bonus);

            if (enemies.Count <= 0)
            {
                NextLevel();
                form.StartTimer("Level");
            }
        }
    }
}
