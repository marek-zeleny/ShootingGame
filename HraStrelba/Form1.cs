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
    public partial class Form1 : Form
    {
        private Manager manager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            manager = new Manager(ClientSize.Width, ClientSize.Height);
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            manager.ClientSize(ClientSize.Width, ClientSize.Height);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            manager.Paint(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            manager.Key((ConsoleKey)e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            manager.Key((ConsoleKey)e.KeyCode, false);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            float x = e.X - ClientRectangle.X;
            float y = e.Y - ClientRectangle.Y;
            manager.Aim(x, y);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                manager.shootingEnabled = true;
                manager.Shoot();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                manager.shootingEnabled = false;
        }

        private void TimerMovement_Tick(object sender, EventArgs e)
        {
            manager.Movement();
            LabelInfo.Text = manager.Info();
            Refresh();
        }

        private void TimerShoot_Tick(object sender, EventArgs e)
        {
            manager.Shoot();
        }

        private void TimerLevel_Tick(object sender, EventArgs e)
        {
            manager.NextLevel();
        }
    }
}
