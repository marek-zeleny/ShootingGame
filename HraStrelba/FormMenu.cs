using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ShootingGame
{
    public partial class FormMenu : Form
    {
        FormGame formGame;
        private string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShootingGame");
        private List<string[]> highscores = new List<string[]>();
        private const int maxHighscoresCount = 10;

        public FormMenu()
        {
            InitializeComponent();
            LoadHighscores();
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            Hide();
            formGame = new FormGame();
            formGame.ShowDialog();
            Show();
            LabelInfo.Text = String.Format("Game over!\nYour score was {0} at level {1}.", formGame.finalScore, formGame.finalLevel);
            LabelInfo.Left = (ClientSize.Width - LabelInfo.Width) / 2;
            SaveScore(formGame.finalScore, formGame.finalLevel);
        }

        private void ButtonHighscores_Click(object sender, EventArgs e)
        {
            ButtonPlay.Hide();
            ButtonHighscores.Hide();
            ButtonCredits.Hide();
            ButtonEndGame.Hide();
            ButtonBack.Show();

            string text = "Date".PadLeft(13).PadRight(20) + "Score".PadLeft(12).PadRight(14) + "Level\n".PadLeft(6).PadRight(7);
            int count = highscores.Count;
            if (count > maxHighscoresCount)
                count = maxHighscoresCount;

            for (int i = 0; i < count; i++)
            {
                text += Environment.NewLine + (i + 1).ToString().PadLeft(2) + ") " + highscores[i][2].PadRight(20) + highscores[i][0].PadLeft(8).PadRight(10) + highscores[i][1].PadLeft(4);
            }
            LabelInfo.Text = text;
            LabelInfo.Left = (ClientSize.Width - LabelInfo.Width) / 2;
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            ButtonPlay.Show();
            ButtonHighscores.Show();
            ButtonCredits.Show();
            ButtonEndGame.Show();
            ButtonBack.Hide();

            LabelInfo.Text = "";
        }

        private void FormMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveHighscores();
        }

        private void SaveScore(int score, int level)
        {
            DateTime dateTime = DateTime.Now;
            string[] newScore = new string[] { score.ToString(), level.ToString(), dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString() };
            int i = maxHighscoresCount - 1;

            if (highscores.Count < maxHighscoresCount)
                highscores.Add(newScore);
            else if (score > int.Parse(highscores[i][0]))
            {
                while (i > 0 && score > int.Parse(highscores[i - 1][0]))
                    i--;
                highscores.Insert(i, newScore);
                highscores.RemoveAt(maxHighscoresCount);
            }
            highscores = Methods.SortTwo_dimentionalArray(highscores);
        }

        private void SaveHighscores()
        {
            try
            {
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
            }
            catch
            {
                MessageBox.Show("Didn't manage to create a folder {0}, check your authorisation", savePath);
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(savePath, "highscores.txt"), false))
                {
                    for (int i = 0; i < highscores.Count; i++)
                        sw.WriteLine("{0};{1};{2}", highscores[i][0], highscores[i][1], highscores[i][2]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("An error occured when trying to save the highscores:\n{0}", e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHighscores()
        {
            try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(savePath, "highscores.txt")))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] devided = line.Split(';');
                        highscores.Add(devided);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("An error occured when trying to load the highscores:\n{0}", e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
