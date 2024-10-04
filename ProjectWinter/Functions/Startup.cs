using IWshRuntimeLibrary;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;

namespace ProjectWinter.Functions
{
    internal class Startup
    {
        public static void AddToStartup()
        {
            string appName = "Winter"; // Change this to your application's name
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location; // Path to your .exe

            // Open the registry key for all users
            RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rk.SetValue(appName, appPath);
        }
        public void RemoveFromStartup()
        {
            string appName = "Winter"; // Change this to your application's name

            // Open the registry key for all users
            RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rk.DeleteValue(appName, false);
        }

        public void AddToStartupAllUsers()
        {
            try
            {
                string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
                string shortcutPath = Path.Combine(startupFolder, "Winter.lnk");
                string appPath = Application.ExecutablePath;

                WshShell wsh = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)wsh.CreateShortcut(shortcutPath);
                shortcut.Description = "Winter";
                shortcut.TargetPath = appPath;
                shortcut.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating shortcut: {ex.Message}");
            }
        }

        public void RemoveFromStartupAllUsers()
        {
            string startupFolder = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp";
            string shortcutPath = System.IO.Path.Combine(startupFolder, "Winter.lnk");

            if (System.IO.File.Exists(shortcutPath))
            {
                System.IO.File.Delete(shortcutPath);
            }
        }

        public void AddToStartupViaTaskScheduler()
        {
            string taskName = "Winter";
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Runs my program at login for all users";

                // Trigger the task at login for all users
                td.Triggers.Add(new LogonTrigger());

                // Set the path to the executable
                td.Actions.Add(new ExecAction(exePath, null, null));

                // Register the task with TaskLogonType.Group to run for all users
                ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.Group);
            }
        }

        public void RemoveStartupTaskViaTaskScheduler()
        {
            string taskName = "Winter";

            // Command to delete the scheduled task
            string command = $"/delete /tn \"{taskName}\" /f";

            // Run the schtasks command to delete the task
            Process.Start(new ProcessStartInfo("schtasks", command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }

        public void AddToStartupSpecificUser()
        {
            // Get the path to the Startup folder for the current user
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Define the shortcut path and the target executable (your .NET app)
            string shortcutPath = Path.Combine(startupFolderPath, "Winter.lnk");
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // Create a new WshShell instance
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

            // Set shortcut properties
            shortcut.Description = "Windows Updater";
            shortcut.TargetPath = exePath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(exePath);
            shortcut.Save(); // Save the shortcut to the Startup folder
        }

        public static void CreateStartupShortcut()
        {
            try
            {
                // For All Users Startup (Requires Admin)
                string startupFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup), "Winter.lnk");

                // For Current User Startup (No Admin needed)
                // string startupFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Winter.lnk");

                string appPath = @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\Installer\Winter\setup.exe";
                string iconPath = @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\Installer\Winter\snowflake.ico";

                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(startupFolderPath);

                shortcut.TargetPath = appPath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(appPath);
                shortcut.Description = "Winter";

                // Set the icon for the shortcut
                shortcut.IconLocation = iconPath; // or specify an exe or dll with index, e.g., "C:\Path\MyApp.exe,0"

                shortcut.Save();

                Console.WriteLine("Startup shortcut created successfully.");
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Failed to create startup shortcut: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show($"WINTER Started (Non-Administrative)", "Winter", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static bool AddToStartupForAllUsers()
        {
            string appName = "Winter"; // Change this to your application's name
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location; // Path to your .exe

            const string StartupKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            try
            {
                // Check for administrative privileges
                if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                {
                    throw new UnauthorizedAccessException("Administrative privileges are required.");
                }

                // Open the registry key
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(StartupKeyPath, true))
                {
                    if (key == null)
                    {
                        throw new Exception($"Unable to open registry key: {StartupKeyPath}");
                    }

                    // Set the value in the registry
                    key.SetValue(appName, appPath, RegistryValueKind.String);

                    // Verify the value was set correctly
                    if (key.GetValue(appName).ToString() != appPath)
                    {
                        throw new Exception("Failed to set registry value correctly.");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding to startup: {ex.Message}");
                return false;
            }
        }

    }
}
