using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinter.Models
{
    internal class Users
    {
        private string _id_number;
        private string _adid;
        private string _section;
        private string _registration_time;
        public Users(string id_number, string adid, string section, string registration_time) 
        { 
            this._id_number = id_number;
            this._adid = adid;
            this._section = section;
            this._registration_time = registration_time;
        }

    }
}
