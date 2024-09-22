using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinter.Functions
{
    internal class GetUser
    {
        public static string GetCurrentLoggedInUser()
        {
            return Environment.UserName;
        }
    }
}
