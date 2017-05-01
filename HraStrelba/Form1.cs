using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HraStrelba
{
    public partial class Form1 : Form
    {
        private Hrac hrac;
        private List<Souper> souperi = new List<Souper>();
        private List<Strela> strely = new List<Strela>();
        private List<float> historieX = new List<float>();
        private List<float> historieY = new List<float>();
        private float mysX;
        private float mysY;
        private Random r = new Random();
        private bool doprava = false;
        private bool doleva = false;
        private bool nahoru = false;
        private bool dolu = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int x = r.Next(ClientSize.Width - (int)Hrac.velikost);
            int y = r.Next(ClientSize.Height - (int)Hrac.velikost);
            hrac = new Hrac(x, y, ClientSize.Width, ClientSize.Height, Color.Blue);
            for (int i = 0; i < 100; i++)
            {
                historieX.Insert(0, x);
                historieY.Insert(0, y);
            }

            float v = 2; //rychlost
            for (int i = 0; i < 5; i++)
            {
                x = r.Next(ClientSize.Width - (int)Hrac.velikost);
                y = r.Next(ClientSize.Height - (int)Hrac.velikost);
                souperi.Add(new Souper(x, y, ClientSize.Width, ClientSize.Height, Color.Yellow, v, 5));
                v += 0.6F;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Souper s in souperi)
                s.Vykresli(e.Graphics);
            foreach (Strela s in strely)
                s.Vykresli(e.Graphics);
            hrac.Vykresli(e.Graphics, mysX, mysY);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    doprava = true;
                    break;
                case Keys.A:
                    doleva = true;
                    break;
                case Keys.W:
                    nahoru = true;
                    break;
                case Keys.S:
                    dolu = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    doprava = false;
                    break;
                case Keys.A:
                    doleva = false;
                    break;
                case Keys.W:
                    nahoru = false;
                    break;
                case Keys.S:
                    dolu = false;
                    break;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mysX = e.X - ClientRectangle.X;
            mysY = e.Y - ClientRectangle.Y;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TimerShoot.Enabled = true;
                Strela s = new Strela(hrac.DeloX, hrac.DeloY, hrac.X + (int)(Hrac.velikost / 2), hrac.Y + (int)(Hrac.velikost / 2), (int)Hrac.velikost);
                strely.Add(s);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                TimerShoot.Enabled = false;
        }

        private void TimerMovement_Tick(object sender, EventArgs e)
        {
            hrac.Pohyb(doprava, doleva, nahoru, dolu);
            historieX.RemoveAt(historieX.Count - 1);
            historieY.RemoveAt(historieY.Count - 1);
            historieX.Insert(0, hrac.X);
            historieY.Insert(0, hrac.Y);
            foreach (Souper s in souperi)
            {
                int vzdalenost = (int)Math.Sqrt((hrac.X - s.X) * (hrac.X - s.X) + (hrac.Y - s.Y) * (hrac.Y - s.Y));
                int i = vzdalenost / 10 - 1;
                if (i > 100)
                    i = 100;
                else if (i < 0)
                    i = 0;
                s.Pohyb(historieX[i], historieY[i]);
            }

            List<Strela> smazatStrely = new List<Strela>();
            foreach (Strela s in strely)
            {
                s.Pohyb();
                if (s.X < -Strela.velikost || s.Y < -Strela.velikost || s.X > ClientSize.Width || s.Y > ClientSize.Height)
                    smazatStrely.Add(s);
            }

            List<Souper> smazatSoupere = new List<Souper>();
            foreach (Souper souper in souperi)
            {
                foreach (Strela strela in strely)
                    if (souper.Zasah(strela.X, strela.Y))
                        smazatStrely.Add(strela);
                if (souper.Hp <= 0)
                    smazatSoupere.Add(souper);
            }

            foreach (Strela s in smazatStrely)
                strely.Remove(s);
            foreach (Souper s in smazatSoupere)
                souperi.Remove(s);
            Refresh();
        }

        private void TimerShoot_Tick(object sender, EventArgs e)
        {
            Strela s = new Strela(hrac.DeloX, hrac.DeloY, hrac.X + (int)(Hrac.velikost / 2), hrac.Y + (int)(Hrac.velikost / 2), (int)Hrac.velikost);
            strely.Add(s);
        }
    }
}
