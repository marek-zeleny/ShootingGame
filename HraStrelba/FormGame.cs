using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShootingGame
{
    public partial class FormGame : Form
    {
        private GameManager gameManager;
        public int finalScore;
        public int finalLevel;

        public FormGame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameManager = new GameManager(ClientSize.Width, ClientSize.Height, this);
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            gameManager.SetClientSize(ClientSize.Width, ClientSize.Height);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gameManager.PaintAll(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            gameManager.KeyPressRelease((ConsoleKey)e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            gameManager.KeyPressRelease((ConsoleKey)e.KeyCode, false);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            float x = e.X - ClientRectangle.X;
            float y = e.Y - ClientRectangle.Y;
            gameManager.SetMousePosition(x, y);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                gameManager.shootingEnabled = true;
                gameManager.PlayerShoot();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                gameManager.shootingEnabled = false;
        }

        private void TimerMovement_Tick(object sender, EventArgs e)
        {
            gameManager.MoveAll();
            LabelGameStats.Text = gameManager.GetScoreAmmoHpInfo();
            Refresh();
        }

        private void TimerShoot_Tick(object sender, EventArgs e)
        {
            gameManager.PlayerShoot();
        }

        private void TimerLevel_Tick(object sender, EventArgs e)
        {
            gameManager.NextLevel();
        }

        private void TimerBonus_Tick(object sender, EventArgs e)
        {
            LabelInfo.Text = "";
            TimerInfo.Enabled = false;
        }
        /// <summary>
        /// Informs about a new active bonus.
        /// </summary>
        /// <param name="bonusType">Type of the bonus</param>
        public void WriteInfo(string text)
        {
            LabelInfo.Text = text;
            LabelInfo.Left = ClientSize.Width / 2 - LabelInfo.Width / 2;
            TimerInfo.Enabled = true;
        }

        public void GameOver(int finalScore, int finalLevel)
        {
            this.finalScore = finalScore;
            this.finalLevel = finalLevel;
            Close();
        }
    }
}
