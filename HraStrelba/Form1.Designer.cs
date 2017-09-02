namespace ShootingGame
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.TimerMovement = new System.Windows.Forms.Timer(this.components);
            this.TimerShoot = new System.Windows.Forms.Timer(this.components);
            this.LabelInfo = new System.Windows.Forms.Label();
            this.TimerLevel = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TimerMovement
            // 
            this.TimerMovement.Enabled = true;
            this.TimerMovement.Interval = 10;
            this.TimerMovement.Tick += new System.EventHandler(this.TimerMovement_Tick);
            // 
            // TimerShoot
            // 
            this.TimerShoot.Enabled = true;
            this.TimerShoot.Tick += new System.EventHandler(this.TimerShoot_Tick);
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabelInfo.Location = new System.Drawing.Point(12, 9);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(0, 20);
            this.LabelInfo.TabIndex = 0;
            // 
            // TimerLevel
            // 
            this.TimerLevel.Enabled = true;
            this.TimerLevel.Interval = 20000;
            this.TimerLevel.Tick += new System.EventHandler(this.TimerLevel_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.LabelInfo);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer TimerMovement;
        private System.Windows.Forms.Timer TimerShoot;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.Timer TimerLevel;
    }
}

