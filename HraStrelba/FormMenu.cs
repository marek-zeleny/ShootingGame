using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

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
            LinkLabelInfo.LinkArea = new LinkArea(0, 0);
            LoadHighscores();
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            Hide();
            formGame = new FormGame();
            formGame.ShowDialog();
            Show();
            LinkLabelInfo.Text = String.Format("Game over!\nYour score was {0} at level {1}.", formGame.finalScore, formGame.finalLevel);
            LinkLabelInfo.Left = (ClientSize.Width - LinkLabelInfo.Width) / 2;
            SaveScore(formGame.finalScore, formGame.finalLevel);
        }

        private void ButtonHighscores_Click(object sender, EventArgs e)
        {
            ButtonClick();
            highscores = Methods.SortTwo_dimensionalArray(highscores, 0);
            string text = "\n\n\n\nHIGHSCORES\n\nScore" + "Level".PadLeft(8) + "Date  \n".PadLeft(20);
            int count = highscores.Count;
            if (count > maxHighscoresCount)
                count = maxHighscoresCount;

            for (int i = 0; i < count; i++)
            {
                text += Environment.NewLine + (i + 1).ToString().PadLeft(2) + ") " + highscores[i][0].PadRight(8) + highscores[i][1].PadLeft(2).PadRight(8) + highscores[i][2].PadLeft(20);
            }
            LinkLabelInfo.Text = text;
            LinkLabelInfo.Left = (ClientSize.Width - LinkLabelInfo.Width) / 2;
        }

        private void ButtonCredits_Click(object sender, EventArgs e)
        {
            ButtonClick();
            string linkGitHub = "https://github.com/marek-zeleny/ShootingGame.git";
            string linkITnetwork = "https://www.itnetwork.cz/";
            string text = String.Format("\n\n\nThis game was created as a competing project for ITnetwork summer 2017.\n\nAll code was written by Marek Zelený.\n\nThe license is freeware, you can see the whole code (in the latest version) at {0}.\n\n\nThank you for playing my game. I hope you enjoyed it, even though it was only a fun project. Go ahead and visit {1}, where I learned everything I know about programming.", linkGitHub, linkITnetwork);
            LinkLabelInfo.Text = text;
            LinkLabelInfo.Links.Add(new LinkLabel.Link(text.IndexOf(linkITnetwork), linkITnetwork.Length, linkITnetwork));
            LinkLabelInfo.Links.Add(new LinkLabel.Link(text.IndexOf(linkGitHub), linkGitHub.Length, linkGitHub));
            LinkLabelInfo.Left = (ClientSize.Width - LinkLabelInfo.Width) / 2;
        }

        private void ButtonControls_Click(object sender, EventArgs e)
        {
            ButtonClick();
            string text = "Movement:\nUp - W\nDown - S\nLeft - A\nRight - D\n\nAiming - mouse\nShooting - left mouse click\n\nObjective:\nKill every enemy in each level, but don't get hit by them!\n\nThere are several types of enemies. Each has unique properties (speed, size, damage, health, etc.) and gains you a different amount of points if killed.\n\nThe static green dots are bonuses that will give you some advantage.\n\nThe game cannot be paused!";
            LinkLabelInfo.Text = text;
            LinkLabelInfo.Left = (ClientSize.Width - LinkLabelInfo.Width) / 2;
        }

        private void ButtonEndGame_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to end the game?", "End game", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                Close();
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            ButtonPlay.Show();
            ButtonHighscores.Show();
            ButtonControls.Show();
            ButtonCredits.Show();
            ButtonEndGame.Show();
            ButtonBack.Hide();

            LinkLabelInfo.Text = "";
            LinkLabelInfo.LinkArea = new LinkArea(0, 0);
        }

        private void LinkLabelInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.Link.LinkData as String);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("An error occured when trying to open the hypertext link:\n{0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveHighscores();
        }
        /// <summary>
        /// Hides main menu buttons and shows "BACK" button.
        /// </summary>
        private void ButtonClick()
        {
            ButtonPlay.Hide();
            ButtonHighscores.Hide();
            ButtonControls.Hide();
            ButtonCredits.Hide();
            ButtonEndGame.Hide();
            ButtonBack.Show();
        }
        /// <summary>
        /// Saves a new score into highscores, if it's high enough.
        /// </summary>
        /// <param name="score">Score to save</param>
        /// <param name="level">Level, in which the score was achived</param>
        private void SaveScore(int score, int level)
        {
            DateTime dateTime = DateTime.Now;
            string[] newScore = new string[] { score.ToString(), level.ToString(), dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString() };

            Methods.SortTwo_dimensionalArray(highscores, 0);

            if (highscores.Count < maxHighscoresCount * 2)
                highscores.Add(newScore);
            else if (score > int.Parse(highscores[highscores.Count - 1][0])) //comparing with the worst score
            {
                highscores.RemoveAt(highscores.Count - 1);
                highscores.Add(newScore);
            }
        }
        /// <summary>
        /// Saves current highscores onto the hard drive (%appdata% folder).
        /// </summary>
        private void SaveHighscores()
        {
            Methods.SortTwo_dimensionalArray(highscores, 0);
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
        /// <summary>
        /// Loads highscores saved on the hard drive (%appdata% folder).
        /// </summary>
        private void LoadHighscores()
        {
            if (File.Exists(Path.Combine(savePath, "highscores.txt")))
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
