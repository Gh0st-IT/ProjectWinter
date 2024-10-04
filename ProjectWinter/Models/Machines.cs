using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinter.Models
{
    internal class Machines : Model
    {
        private string _pc_name;
        private string _windows_version;
        private DateTime _registration_time;
        private string _os_version;
        private string _app_version;
        private string _ip;

        private string _user;
        private SQLServer _server;

        public Machines(string pc_name = "", string windows_version = "", string user = "", string os_version = "", string app_version = "", string ip = "")
        {
            this._pc_name = pc_name;
            this._windows_version = windows_version;
            this._registration_time = DateTime.Now;
            this._os_version = os_version;
            this._app_version = app_version;
            this._ip = ip;

            this._user = user;
            this._server = new SQLServer();
        }

        public void Machine()
        {
            //Check if machine is already registered
            bool isRegistered = CheckMachine();
            if (!isRegistered)
            {
                InitialRegistration();
            }
            else
            {
                //Update User
                UpdateMachineUser();
                UpdateMachine();
            }

        }
        private bool CheckMachine()
        {
            string conditions = $"pc_name = '{_pc_name}'";
            return this.PostgresqlCheckExists("machines", conditions);
        }
        private void InitialRegistration()
        {
            //Register Machine
            RegisterMachine();

            //Get Machine Id and User ID
            int machineID = GetMachineID();
            int userID = GetUserID();


            if (machineID != 0 && userID != 0) // Ensure valid IDs before inserting
            {
                var currentUser = new Dictionary<string, object>() {
                    { "machine_id", machineID },
                    { "user_id", userID },
                    { "login_time", _registration_time }
                };
                //Create machine_user entry
                this.PostgresqlInsert("machine_user", currentUser);
                //Insert in history of initial registration
                this.PostgresqlInsert("machine_user_history", currentUser);
            }
            else
            {
                Console.WriteLine("Machine ID or User ID not found.");
            }
        }
        private void RegisterMachine()
        {
            // Machine initial registration
            var machineDetails = new Dictionary<string, object>() {
                { "pc_name", _pc_name },
                { "windows_version", _windows_version },
                { "registration_time", _registration_time },
                { "os_version", _os_version },
                { "app_version", _app_version },
                { "ip", _ip }
            };

            // Insert machine details
            this.PostgresqlInsert("machines", machineDetails);

        }
        private int GetUserID()
        {
            // Retrieve user ID
            var userList = _server.ReadData("T_Employee_List", $"adid = '{_user}'");
            int userID = 0;
            if (userList.Rows.Count > 0)
            {
                return userID = Convert.ToInt32(userList.Rows[0]["id"]);
            }
            return userID;
        }
        private int GetMachineID()
        {
            // Retrieve the machine ID
            var machineList = PostgresqlSelect("machines", "id", $"pc_name = '{_pc_name}'");
            int machineID = machineList.Count > 0 ? Convert.ToInt32(machineList[0]["id"]) : 0;
            return machineID;
        }

        private void UpdateMachineUser()
        {
            int userID = GetUserID();
            int machineID = GetMachineID();

            if (machineID != 0 && userID != 0)
            {
                var currentUser = new Dictionary<string, object>() {
                    { "machine_id", machineID },
                    { "user_id", userID },
                    { "login_time", _registration_time }
                };

                this.PostgresqlUpdate("machine_user", currentUser, $"machine_id = {machineID}");
                this.PostgresqlInsert("machine_user_history", currentUser);
            }
        }

        public void UpdateMachine()
        {
            int machineID = GetMachineID();
            var machineDetails = new Dictionary<string, object>() {
                { "pc_name", _pc_name },
                { "windows_version", _windows_version },
                { "last_update_time", DateTime.Now },
                { "os_version", _os_version },
                { "app_version", _app_version },
                { "ip", _ip }
            };
            this.PostgresqlUpdate("machines", machineDetails, $"id = {machineID}");
        }
    }
}