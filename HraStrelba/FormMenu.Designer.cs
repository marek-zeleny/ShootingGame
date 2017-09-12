namespace ShootingGame
{
    partial class FormMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonPlay = new System.Windows.Forms.Button();
            this.ButtonHighscores = new System.Windows.Forms.Button();
            this.ButtonCredits = new System.Windows.Forms.Button();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.ButtonEndGame = new System.Windows.Forms.Button();
            this.ButtonBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonPlay
            // 
            this.ButtonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ButtonPlay.Location = new System.Drawing.Point(240, 150);
            this.ButtonPlay.Name = "ButtonPlay";
            this.ButtonPlay.Size = new System.Drawing.Size(220, 50);
            this.ButtonPlay.TabIndex = 0;
            this.ButtonPlay.Text = "PLAY";
            this.ButtonPlay.UseVisualStyleBackColor = true;
            this.ButtonPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
            // 
            // ButtonHighscores
            // 
            this.ButtonHighscores.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ButtonHighscores.Location = new System.Drawing.Point(240, 206);
            this.ButtonHighscores.Name = "ButtonHighscores";
            this.ButtonHighscores.Size = new System.Drawing.Size(220, 50);
            this.ButtonHighscores.TabIndex = 1;
            this.ButtonHighscores.Text = "HIGHSCORES";
            this.ButtonHighscores.UseVisualStyleBackColor = true;
            this.ButtonHighscores.Click += new System.EventHandler(this.ButtonHighscores_Click);
            // 
            // ButtonCredits
            // 
            this.ButtonCredits.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ButtonCredits.Location = new System.Drawing.Point(240, 262);
            this.ButtonCredits.Name = "ButtonCredits";
            this.ButtonCredits.Size = new System.Drawing.Size(220, 50);
            this.ButtonCredits.TabIndex = 2;
            this.ButtonCredits.Text = "CREDITS";
            this.ButtonCredits.UseVisualStyleBackColor = true;
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabelInfo.Location = new System.Drawing.Point(350, 50);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(0, 21);
            this.LabelInfo.TabIndex = 3;
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ButtonEndGame
            // 
            this.ButtonEndGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ButtonEndGame.Location = new System.Drawing.Point(240, 318);
            this.ButtonEndGame.Name = "ButtonEndGame";
            this.ButtonEndGame.Size = new System.Drawing.Size(220, 50);
            this.ButtonEndGame.TabIndex = 4;
            this.ButtonEndGame.Text = "END GAME";
            this.ButtonEndGame.UseVisualStyleBackColor = true;
            // 
            // ButtonBack
            // 
            this.ButtonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ButtonBack.Location = new System.Drawing.Point(240, 599);
            this.ButtonBack.Name = "ButtonBack";
            this.ButtonBack.Size = new System.Drawing.Size(220, 50);
            this.ButtonBack.TabIndex = 5;
            this.ButtonBack.Text = "BACK";
            this.ButtonBack.UseVisualStyleBackColor = true;
            this.ButtonBack.Visible = false;
            this.ButtonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.ButtonBack);
            this.Controls.Add(this.ButtonEndGame);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.ButtonCredits);
            this.Controls.Add(this.ButtonHighscores);
            this.Controls.Add(this.ButtonPlay);
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMenu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMenu_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonPlay;
        private System.Windows.Forms.Button ButtonHighscores;
        private System.Windows.Forms.Button ButtonCredits;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.Button ButtonEndGame;
        private System.Windows.Forms.Button ButtonBack;
    }
}