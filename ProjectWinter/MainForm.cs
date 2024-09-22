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
using System.Threading;

namespace ProjectWinter
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
            WindowsDetails windows = WindowsVersionChecker.Checker();
            DisplayCurrentVersion(windows.OSVersion, windows.OSBuild);
            user.Text = GetUser.GetCurrentLoggedInUser();

            // Now you can access the details of the windows object
            //Console.WriteLine($"Display Version: {windows.DisplayVersion}");
            //Console.WriteLine($"OS Build: {windows.OSBuild}");
            //Console.WriteLine($"OS Version: {windows.OSVersion}");

            Thread newThread = new Thread(WindowsUpdater.UpdateChecker);
            newThread.Start();
            
        }
        private void DisplayCurrentVersion(string OsVersion, string OsBuild) 
        { 
            osVersion.Text = OsVersion;
            osBuild.Text = OsBuild;
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
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void checkWindowsUpdate_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(WindowsUpdater.UpdateChecker);
            newThread.Start();
        }
    }
}
