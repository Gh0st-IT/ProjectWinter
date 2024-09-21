using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinter.Models
{
    internal class Machines
    {
        private string _pc_name;
        private string _windows_version;
        private string _registration_time;
        public Machines(string pc_name, string windows_version) 
        {
            this._pc_name = pc_name;
            this._windows_version = windows_version;
            this._registration_time = DateTime.Now.ToString();
        }
        
    }
}
