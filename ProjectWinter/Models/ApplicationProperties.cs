using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProjectWinter.Models
{
    internal class ApplicationProperties : Model
    {
        public string updated_version;
        public string current_version;

        public ApplicationProperties()
        {
            this.current_version = Application.ProductVersion;
            var result = this.PostgresqlSelect("application_update", "updated_version");
            this.updated_version = result[0]["updated_version"].ToString();
            this.CheckUpdate();
        }

        private void CheckUpdate()
        {
            if (current_version != updated_version)
            {
                //DialogResult dialogResult = MessageBox.Show("Winter Update Available. Do you want to update now?", "Update Available", MessageBoxButtons.YesNo);
                //if (dialogResult == DialogResult.Yes)
                //{
                    try
                    {
                        string installerPath = @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\Installer\Winter\setup.exe";
                        
                        // Inform the user about the update
                        //MessageBox.Show("The application will close and the update process will begin.", "Update Process", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Start the installer in a new process
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = installerPath,
                            UseShellExecute = true  // This allows the installer to run with elevated privileges if needed
                        });

                        // Exit the application
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to start the update process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                //}
            }
        }
    }
}