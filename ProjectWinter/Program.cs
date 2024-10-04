using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectWinter
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Mutex mutex;

        [STAThread]
        static void Main()
        {
            const string MutexName = "Winter";

            // Attempt to acquire the mutex
            bool createdNew;
            mutex = new Mutex(true, MutexName, out createdNew);

            if (createdNew)
            {
                // Mutex acquired, no other instance is running
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
                mutex.ReleaseMutex(); // Release the mutex when application exits
            }
            else
            {
                // Mutex already exists, another instance is running
                MessageBox.Show("Another instance of the application is already running.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
