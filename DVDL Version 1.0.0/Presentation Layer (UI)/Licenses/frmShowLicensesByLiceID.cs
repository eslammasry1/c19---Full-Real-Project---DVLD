using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmShowLicensesByLiceID : Form
    {
        int _LicenseID = -1;
        public frmShowLicensesByLiceID(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
        }

        private void frmShowLicensesByLiceID_Load(object sender, EventArgs e)
        {
            cntrlLicenses1.LoadByLicenseID(_LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
