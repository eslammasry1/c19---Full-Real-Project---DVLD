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
    public partial class frmAddNewInternationalLicense : Form
    {
        int _UserID = -1;
        clsUser _user;
        clsLicenses Licenses;
        clsApplication _application;
        clsInternationalLicense _internationalLicense;

        public frmAddNewInternationalLicense(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
            clsApplication _application;
        }
        private void _LoadDelegat(object sender, int licenseID)
        {
            Licenses = clsLicenses.Find(licenseID);
            if (Licenses == null)
            {
                MessageBox.Show("This visa does not exist.");
                return;
            }
            if (Licenses.IsActive == false)
            {
                MessageBox.Show("This License is not activated.");
                return;
            }
            if (Licenses.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("This License has expired.");
                return;
            }
            if (Licenses.LicensesClass != 3)
            {
                MessageBox.Show("The License must be of the third Class.");
                return;
            }
            if (clsInternationalLicense.HaveActiveInternationalLicenses(Licenses.DriverID))
            {
                MessageBox.Show("He already has an international License.");
                return;
            }
            lblLocalLicense.Text = Licenses.LicenseID.ToString();
            llSLHistory.Enabled = true;
            btnIssue.Enabled = true;

        }
        private void frmAddNewInternationalLicense_Load(object sender, EventArgs e)
        {
            btnIssue.Enabled = false;
            cntrlFindLicense1.FindLicense += _LoadDelegat;
            _user = clsUser.Find(_UserID);
            lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblExDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblUserName.Text = _user.UserName;
            lblFees.Text = clsApplicationType.GetFeesType(6).ToString();
            llSLInfo.Enabled = false;
            llSLHistory.Enabled = false;
        }

        private void _AddNewApplication ()
        {
            _application = new clsApplication();
            _application.ApplicantPersonID = clsDriver.FindByDriverID(Licenses.DriverID).PersonID;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationTypeID = 6;
            _application.ApplicationStatus = 3;
            _application.LastStatusDate = DateTime.Now;
            _application.PaidFees = clsApplicationType.GetFeesType(6);
            _application.CreatedByUserID = _UserID;
            _application.AddNewApplication();
        }
        private void _AddNewinternational()
        {
            _AddNewApplication();
            _internationalLicense = new clsInternationalLicense();
            _internationalLicense.ApplicationID = _application.ApplicationID;
            _internationalLicense.DriverID = Licenses.DriverID;
            _internationalLicense.IssuedUsingLocalLicenseID = Licenses.LicenseID;
            _internationalLicense.IssueDate = DateTime.Now;
            _internationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _internationalLicense.IsActive = true;
            _internationalLicense.CreatedByUserID = _UserID;
            if (MessageBox.Show("Are you sure you want to issue this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_internationalLicense.AddNewInternationalLicense())
                {
                    MessageBox.Show($"international License Issued Successfuly With ID ={_internationalLicense.InternationalLicenseID}");
                }
                else
                {
                    MessageBox.Show("Operation failed; please try again.");
                    return;
                }
            }
            lblAppID.Text = _application.ApplicationID.ToString();
            lblInternationalLicenses.Text = _internationalLicense.InternationalLicenseID.ToString();
            llSLInfo.Enabled = true;
            btnIssue.Enabled = false;
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (Licenses ==  null)
            {
                MessageBox.Show("Enter your License details first.");
                return;
            }
            _AddNewinternational();
            cntrlFindLicense1.DisableGbFilter();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void llSLInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowInternationlInfo(_internationalLicense.InternationalLicenseID);
            frm.ShowDialog();

        }

        private void llSLHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseHistory(clsDriver.FindByDriverID(Licenses.DriverID).PersonID);
            frm.ShowDialog();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cntrlFindLicense1_Load(object sender, EventArgs e)
        {

        }
    }
}
