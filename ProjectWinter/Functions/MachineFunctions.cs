using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinter.Functions
{
    internal class MachineFunctions
    {
        public static string GetMachineName()
        {
            return Environment.MachineName;
        }
        public static string GetCurrentLoggedInUser()
        {
            return Environment.UserName;
        }
        public static string GetIPAddress()
        {
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();
        }
    }
}
