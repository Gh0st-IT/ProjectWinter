using ProjectWinter.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectWinter.Modals
{
    public partial class OtherInformation : Form
    {
        public OtherInformation()
        {
            InitializeComponent();
        }

        private void OtherInformation_Load(object sender, EventArgs e)
        {
            label2.Text = MachineFunctions.GetIPAddress();
        }
    }
}
