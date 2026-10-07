using DVLD_BusinessLayer;
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
    public partial class cntrlFindLicense : UserControl
    {
        public delegate void cntrlFindLicenseEventHandler(object sender, int LicenseID);
        public event cntrlFindLicenseEventHandler FindLicense;


        public cntrlFindLicense()
        {
            InitializeComponent();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            clsLicenses _licenses = clsLicenses.Find(int.Parse(tbFilter.Text));
            if (_licenses == null)
            {
                MessageBox.Show("This visa does not exist.");
                return;
            }
            cntrlLicenses1.LoadByLicenseID(_licenses.LicenseID);
            FindLicense.Invoke(this, _licenses.LicenseID);

        }
        public void DisableGbFilter()
        {
            gbFilter.Enabled = false;
        }
        public void SelectedLicense(int LicenseID)
        {
            clsLicenses _licenses = clsLicenses.Find(LicenseID);
            if (_licenses == null)
            {
                return;
            }

            tbFilter.Text = LicenseID.ToString();
            cntrlLicenses1.LoadByLicenseID(LicenseID);
            gbFilter.Enabled = false;
        }

        private void cntrlFindLicense_Load(object sender, EventArgs e)
        {

        }
    }
}
