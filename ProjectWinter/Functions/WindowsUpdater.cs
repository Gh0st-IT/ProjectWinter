using ProjectWinter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WUApiLib;

namespace ProjectWinter.Functions
{
    class WindowsUpdater
    {
        public static void UpdateChecker()
        {
            InstallUpdates();
        }

        public static void UpdateCheckerClickable()
        {
            try
            {
                MessageBox.Show("Checking Windows Update");

                UpdateSession updateSession = new UpdateSession();
                IUpdateSearcher updateSearcher = updateSession.CreateUpdateSearcher();
                ISearchResult searchResult = updateSearcher.Search("IsInstalled=0");

                if (searchResult.Updates.Count == 0)
                {
                    MessageBox.Show("No updates available.");
                    return;
                }
                MessageBox.Show("Windows Updates Detected");

                IUpdateInstaller installer = updateSession.CreateUpdateInstaller();
                UpdateCollection updateCollection = new UpdateCollection();

                for (int i = 0; i < searchResult.Updates.Count; i++)
                {
                    updateCollection.Add(searchResult.Updates[i]);
                }

                installer.Updates = updateCollection;
                IInstallationResult result = installer.Install();

                //Update database after installation success
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
                
                Machines machines = new Machines(pcName, OSBuild, username, OperatingSystemVersion, AppVersion, ip);
                machines.UpdateMachine();

                Console.WriteLine("Installation Result: " + result.ResultCode);
                if (result.RebootRequired)
                {
                    MessageBox.Show("Windows Updates Installed. Restart your computer");
                }

                //Console.WriteLine("Reboot Required: " + result.RebootRequired);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking for updates: {ex.Message}");
            }
        }

        private static void InstallUpdates()
        {
            //Update database after installation success
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

            Machines machines = new Machines(pcName, OSBuild, username, OperatingSystemVersion, AppVersion, ip);
            machines.UpdateMachine();

            //Initiate Windows Update
            UpdateSession updateSession = new UpdateSession();
            IUpdateSearcher updateSearcher = updateSession.CreateUpdateSearcher();
            ISearchResult searchResult = updateSearcher.Search("IsInstalled=0");

            if (searchResult.Updates.Count == 0)
            {
                Console.WriteLine("No updates available.");
                return;
            }
            MessageBox.Show("Windows Updates Detected");

            IUpdateInstaller installer = updateSession.CreateUpdateInstaller();
            UpdateCollection updateCollection = new UpdateCollection();

            for (int i = 0; i < searchResult.Updates.Count; i++)
            {
                updateCollection.Add(searchResult.Updates[i]);
            }

            installer.Updates = updateCollection;
            IInstallationResult result = installer.Install();

            

            Console.WriteLine("Installation Result: " + result.ResultCode);
            if (result.RebootRequired)
            {
                MessageBox.Show("Windows Updates Installed. Restart your computer");
            }
            
            //Console.WriteLine("Reboot Required: " + result.RebootRequired);
        }
    }
}
