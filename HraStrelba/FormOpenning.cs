using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ShootingGame
{
    public partial class FormOpenning : Form
    {
        public FormOpenning()
        {
            InitializeComponent();
        }

        private void FormOpenning_Shown(object sender, EventArgs e)
        {
            Thread.Sleep(4000);
            Close();
        }
    }
}
