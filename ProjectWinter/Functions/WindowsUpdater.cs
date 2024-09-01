using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUApiLib;

namespace ProjectWinter.Functions
{
    class WindowsUpdater
    {
        public static void UpdateChecker()
        {
            try
            {
                // Create an UpdateSession object
                UpdateSession updateSession = new UpdateSession();

                // Create an UpdateSearcher object
                IUpdateSearcher updateSearcher = updateSession.CreateUpdateSearcher();

                // Search for available updates (IsInstalled=0 means updates that are not installed)
                ISearchResult searchResult = updateSearcher.Search("IsInstalled=0");

                Console.WriteLine($"Number of updates available: {searchResult.Updates.Count}");

                // Loop through each update found
                for (int i = 0; i < searchResult.Updates.Count; i++)
                {
                    IUpdate update = searchResult.Updates[i];

                    // Check if the update is a Windows Update
                    bool isWindowsUpdate = false;

                    foreach (ICategory category in update.Categories)
                    {
                        if (category.Name.Contains("Security Update") ||
                            category.Name.Contains("Critical Update") ||
                            category.Name.Contains("Update Rollup") ||
                            category.Name.Contains("Feature Pack"))
                        {
                            isWindowsUpdate = true;
                            break;
                        }
                    }

                    if (isWindowsUpdate)
                    {
                        Console.WriteLine($"Windows Update {i + 1}: {update.Title}");
                        Console.WriteLine($"  Description: {update.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"Non-Windows Update {i + 1}: {update.Title}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking for updates: {ex.Message}");
            }
        }
    }
    
}
