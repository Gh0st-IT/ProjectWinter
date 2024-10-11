using ProjectWinter.Models;
using System;
using System.Windows.Forms;
using WUApiLib;

namespace ProjectWinter.Functions
{
    class WindowsUpdater
    {
        public static void UpdateChecker()
        {
            CheckForUpdates(false);
        }

        public static void UpdateCheckerClickable()
        {
            try
            {
                // Create an instance of the UpdateSession class
                var updateSession = new UpdateSession();

                // Create an UpdateSearcher instance
                var updateSearcher = updateSession.CreateUpdateSearcher();

                // Search for installed updates
                var searchResult = updateSearcher.Search("IsInstalled=1");

                Console.WriteLine($"Found {searchResult.Updates.Count} installed updates:");

                foreach (IUpdate update in searchResult.Updates)
                {
                    Console.WriteLine($"- {update.Title} (Installed on: {update.InstallationDate})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        private static void CheckForUpdates(bool isClickable)
        {
            try
            {
                // Create an instance of the UpdateSession class
                UpdateSession updateSession = new UpdateSession();

                // Create an UpdateSearcher instance
                IUpdateSearcher updateSearcher = updateSession.CreateUpdateSearcher();
                

                // Search for available updates
                ISearchResult searchResult = updateSearcher.Search("IsInstalled=1 OR IsHidden=1");

                Console.WriteLine($"Found {searchResult.Updates.Count} updates.");

                // You can process the updates here
                foreach (IUpdate update in searchResult.Updates)
                {
                    Console.WriteLine($"Update Title: {update.Title}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }



        //private static void CheckForUpdates(bool isClickable)
        //{
        //    UpdateSession updateSession = new UpdateSession();
        //    IUpdateSearcher updateSearcher = updateSession.CreateUpdateSearcher();

        //    updateSearcher.Online = true;
        //    // Expanded search criteria to include all types of updates
        //    string searchCriteria = "IsInstalled=0 AND Type='Software' OR Type='Driver' ";
        //    Console.WriteLine($"Searching for updates with criteria: {searchCriteria}");
        //    ISearchResult searchResult = updateSearcher.Search(searchCriteria);

        //    if (searchResult.Updates.Count == 0)
        //    {
        //        if (isClickable) MessageBox.Show("No updates available.");
        //        Console.WriteLine("No updates found.");
        //        return;
        //    }

        //    if (isClickable) MessageBox.Show($"{searchResult.Updates.Count} Windows Updates Detected");
        //    Console.WriteLine($"Found {searchResult.Updates.Count} updates.");

        //    // Log available updates
        //    foreach (IUpdate update in searchResult.Updates)
        //    {
        //        Console.WriteLine($"Update Title: {update.Title}");
        //        Console.WriteLine($"Description: {update.Description}");
        //        Console.WriteLine($"KB Article IDs: {string.Join(", ", update.KBArticleIDs)}");
        //        Console.WriteLine($"Is Downloaded: {update.IsDownloaded}");
        //        Console.WriteLine($"Type: {update.Type}");
        //        Console.WriteLine("---");
        //    }

        //    // If it's clickable, ask user if they want to install the updates
        //    if (isClickable)
        //    {
        //        if (MessageBox.Show("Do you want to install these updates?", "Install Updates", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            InstallUpdates(updateSession, searchResult.Updates);
        //        }
        //    }
        //    else
        //    {
        //        // For non-clickable version, proceed with installation without asking
        //        InstallUpdates(updateSession, searchResult.Updates);
        //    }
        //}

        private static void InstallUpdates(UpdateSession updateSession, IUpdateCollection updates)
        {
            IUpdateInstaller installer = updateSession.CreateUpdateInstaller();
            installer.Updates = (UpdateCollection)updates;

            IInstallationResult result = installer.Install();

            LogInstallationResult(result, updates);

            if (result.RebootRequired)
            {
                MessageBox.Show("Windows Updates Installed. Restart your computer");
            }
            else
            {
                MessageBox.Show("Windows Updates Installed successfully.");
            }

            // Update database after installation success
            UpdateSystemInfo();
        }

        private static void LogInstallationResult(IInstallationResult result, IUpdateCollection updates)
        {
            Console.WriteLine("Installation Result: " + result.ResultCode);

            for (int i = 0; i < updates.Count; i++)
            {
                IUpdate update = updates[i];
                try
                {
                    IUpdateInstallationResult updateResult = result.GetUpdateResult(i);
                    Console.WriteLine($"Update: {update.Title}, Result Code: {updateResult.ResultCode}");

                    if (updateResult.HResult != 0)
                    {
                        Console.WriteLine($"Error Code for {update.Title}: {updateResult.HResult} - {new System.ComponentModel.Win32Exception(updateResult.HResult).Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting result for update {update.Title}: {ex.Message}");
                }
            }
        }

        private static void UpdateSystemInfo()
        {
            // Initial Components
            WindowsDetails windows = WindowsVersionChecker.Checker();
            ApplicationProperties application = new ApplicationProperties();

            // Variables
            string OSVersion = windows.OSVersion;
            string OSBuild = windows.OSBuild;
            string OperatingSystemVersion = windows.OperatingSystemVersion;
            string AppVersion = application.current_version;
            string username = MachineFunctions.GetCurrentLoggedInUser();
            string pcName = MachineFunctions.GetMachineName();
            string ip = MachineFunctions.GetIPAddress();

            Machines machines = new Machines(pcName, OSBuild, username, OperatingSystemVersion, AppVersion, ip);
            machines.UpdateMachine();
        }
    }
}