using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ProjectWinter.Functions
{
    internal class WindowsDetails
    {
        public string DisplayVersion { get; set; }
        public string OSBuild { get; set; }
        public string OSVersion { get; set; }
        public string OperatingSystemVersion { get; set; }

        public WindowsDetails(string displayVersion, string osBuild, string osVersion, string operatingSystemVersion)
        {
            this.DisplayVersion = displayVersion;
            this.OSBuild = osBuild;
            this.OSVersion = osVersion;
            this.OperatingSystemVersion = operatingSystemVersion;
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
            string operatingSystemVersion = GetOperatingSystemVersion();

            // Get the Windows Version Information as an object
            WindowsDetails versionInfo =  new WindowsDetails
                (
                    osVersion,
                    osFullBuild,
                    displayVersion,
                    operatingSystemVersion
                );

            // Now you can access the details of the windows object
            //Console.WriteLine($"Display Version: {windows.DisplayVersion}");
            //Console.WriteLine($"OS Build: {windows.OSBuild}");
            //Console.WriteLine($"OS Version: {windows.OSVersion}");

            return versionInfo;
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

        static string GetOperatingSystemVersion()
        {
            //string productName = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "");
            string osBuild = GetFullOSBuild();

            if (osBuild.Contains("226"))
            {
                return "Windows 11";
            }
            else if (osBuild.Contains("190"))
            {
                return "Windows 10";
            }
            else
            {
                return "Other version of Windows";
            }
        }
    }
}
