using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinter.Functions
{
    internal class WindowsDetails
    {
        public string DisplayVersion { get; set; }
        public string OSBuild { get; set; }
        public string OSVersion { get; set; }

        public WindowsDetails(string displayVersion, string osBuild, string osVersion)
        {
            this.DisplayVersion = displayVersion;
            this.OSBuild = osBuild;
            this.OSVersion = osVersion;
        }
    }
    internal class WindowsVersionChecker
    {
        public static WindowsDetails Checker()
        {
            // Get the version, full build number, and feature update
            string osVersion = GetOSVersion();
            string osFullBuild = GetFullOSBuild();
            string displayVersion = GetDisplayVersion();

            // Get the Windows Version Information as an object
            WindowsDetails versionInfo =  new WindowsDetails
                (
                    osVersion,
                    osFullBuild,
                    displayVersion
                );
            return versionInfo;

            // Print the version info (or you can use it in your program)
            //Console.WriteLine(versionInfo);

            // Print the information
            //Console.WriteLine($"Microsoft Windows");
            //Console.WriteLine($"Version {displayVersion} (OS Build {osFullBuild})");
        }


        // Function to retrieve the OS version
        static string GetOSVersion()
        {
            OperatingSystem os = Environment.OSVersion;
            return $"{os.Version.Major}.{os.Version.Minor}.{os.Version.Build}";
        }

        // Function to retrieve the OS full build number (Major.Build.Minor)
        static string GetFullOSBuild()
        {
            string buildNumber = string.Empty;
            string ubrNumber = string.Empty;

            try
            {
                // Get the major build number from WMI
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (ManagementObject obj in searcher.Get())
                {
                    buildNumber = obj["BuildNumber"].ToString();
                }

                // Get the UBR (Update Build Revision) from the registry
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    if (key != null)
                    {
                        object ubr = key.GetValue("UBR");
                        if (ubr != null)
                        {
                            ubrNumber = ubr.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving build number: " + ex.Message);
            }

            return $"{buildNumber}.{ubrNumber}";
        }

        // Function to retrieve the Display Version (like 21H2, 22H2, etc.)
        static string GetDisplayVersion()
        {
            string displayVersion = "Unknown";

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    if (key != null)
                    {
                        object version = key.GetValue("DisplayVersion");  // Get the DisplayVersion for newer Windows
                        if (version != null)
                        {
                            displayVersion = version.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving display version: " + ex.Message);
            }

            return displayVersion;
        }
    }
}
