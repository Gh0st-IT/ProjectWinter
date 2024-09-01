using ProjectWinter.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectWinter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            WindowsUpdater.UpdateChecker();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                sysTray.Visible = true;
            }
        }

        private void sysTray_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.WindowState = FormWindowState.Minimized;
        }
    }
}
