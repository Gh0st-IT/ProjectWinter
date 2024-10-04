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
using ProjectWinter.Models;
using System.Timers;
using ProjectWinter.Modals;


namespace ProjectWinter
{
    public partial class Main : Form
    {
        private bool _isChecking = false;
        private static readonly object _lock = new object(); // Lock object for thread safety
        public Main()
        {
            InitializeComponent();
            try
            {
                Startup.CreateStartupShortcut();

                //Initial Components
                WindowsDetails windows = WindowsVersionChecker.Checker();
                ApplicationProperties application = new ApplicationProperties();

                //Variables
                string OSVersion = windows.OSVersion;
                string OSBuild = windows.OSBuild;
                string OperatingSystemVersion = windows.OperatingSystemVersion;
                string AppVersion = application.current_version;

                string username = MachineFunctions.GetCurrentLoggedInUser();
                string pcName = MachineFunctions.GetMachineName();
                string ip = MachineFunctions.GetIPAddress();

                //UI Functions
                DisplayInformation(OSVersion, OSBuild, pcName, OperatingSystemVersion, AppVersion);
                user.Text = username;

                //PC Functions
                Machines pc = new Machines(pcName, OSBuild, username, OperatingSystemVersion, AppVersion, ip);
                pc.Machine();

                //Initiate continuous checking of windows update
                System.Timers.Timer clock = new System.Timers.Timer();
                clock.Elapsed += new ElapsedEventHandler(WindowsUpdateChecker);
                clock.Interval = 60000; // ~ 60 seconds
                clock.Enabled = true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void DisplayInformation(string OsVersion, string OsBuild, string PCName, string OperatingSystemVersion, string AppVersion) 
        {
            osVersion.Text = OsVersion;
            osBuild.Text = OsBuild;
            pcNameLabel.Text = PCName;
            operatingSystemVersion.Text = OperatingSystemVersion;
            appVersion.Text = AppVersion;
        }

        private void checkWindowsUpdate_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(WindowsUpdater.UpdateCheckerClickable);
            newThread.Start();
        }
        private void WindowsUpdateChecker(object source, ElapsedEventArgs e)
        {
            // Check if another check is already running
            lock (_lock)
            {
                if (_isChecking) return; // Exit if another check is in progress
                _isChecking = true; // Set flag to indicate that checking has started
            }

            // Start the update checker on a new thread
            Thread newThread = new Thread(() =>
            {
                try
                {
                    WindowsUpdater.UpdateChecker(); // Perform the actual update check
                    ApplicationProperties application = new ApplicationProperties();
                }
                finally
                {
                    // Ensure _isChecking is reset even if an exception occurs
                    lock (_lock)
                    {
                        _isChecking = false;
                    }
                }
            });

            newThread.Start();
        }










        //System tray functions
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
            //this.Visible = false;
            //Application.Exit();
            //Environment.Exit(0);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnOtherInformation_Click(object sender, EventArgs e)
        {
            OtherInformation otherInformation = new OtherInformation();
            otherInformation.Show();
        }
        //~~~~//
    }
}
