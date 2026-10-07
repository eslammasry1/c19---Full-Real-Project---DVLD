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
using static System.Net.Mime.MediaTypeNames;

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmRenewLicense : Form
    {
        private int _UserID = -1;
        private clsUser _user;
        private clsLicenses _OldLicenses;
        private clsLicenses _NewLicenses;
        private clsApplication _application;
        public frmRenewLicense(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }
        private void _LoadDelegat(object sender, int licenseID)
        {
            _OldLicenses = clsLicenses.Find(licenseID);
            if (_OldLicenses.IsActive == false)
            {
                MessageBox.Show("This License is not activated.");
                return;

            }
            if (_OldLicenses.ExpirationDate>DateTime.Now)
            {
                MessageBox.Show($@"The specified visa has not yet expired.
                                it Will expire On:{_OldLicenses.ExpirationDate.ToString("dd/MMM/yyyy")}");
                return;
            }
            lblLicenseFees.Text = clsLicenses.GetLicensesClassFees(_OldLicenses.LicensesClass).ToString();
            lblExDate.Text = DateTime.Now.AddYears(clsLicenses.GeTDefaultValidityLength(_OldLicenses.LicensesClass)).ToString("dd/MMM/yyyy");
            lblTotalFees.Text = (clsApplicationType.GetFeesType(2)+ clsLicenses.GetLicensesClassFees(_OldLicenses.LicensesClass)).ToString();
            lblOldL.Text= _OldLicenses.LicenseID.ToString();
            btnRenew.Enabled = true;
            llSLHistory.Enabled = true;
        }
        private void _AddNewApplication()
        {
            _application = new clsApplication();
            _application.ApplicantPersonID = clsDriver.FindByDriverID(_OldLicenses.DriverID).PersonID;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationTypeID = 2;
            _application.ApplicationStatus = 3;
            _application.LastStatusDate = DateTime.Now;
            _application.PaidFees = clsApplicationType.GetFeesType(2);
            _application.CreatedByUserID = _UserID;
            _application.AddNewApplication();
        }
        private void _RenewLicense()
        {
            _AddNewApplication();
            _NewLicenses = new clsLicenses();
            _NewLicenses.ApplicationID = _application.ApplicationID;
            _NewLicenses.DriverID = _OldLicenses.DriverID;
            _NewLicenses.LicensesClass = _OldLicenses.LicensesClass;
            _NewLicenses.IssueDate = DateTime.Now;
            _NewLicenses.ExpirationDate = DateTime.Now.AddYears(clsLicenses.GeTDefaultValidityLength(_OldLicenses.LicensesClass));
            _NewLicenses.Note = tbNotes.Text;
            _NewLicenses.PaidFees = clsLicenses.GetLicensesClassFees(_OldLicenses.LicensesClass);
            _NewLicenses.IsActive = true;
            _NewLicenses.IssueReason = (int)clsLicenses.enIssueReason.Renew;
            _NewLicenses.CreatedByUserID = _UserID;
            if (MessageBox.Show("Are you sure you want to Renew this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_NewLicenses.AddNewLicense())
                {
                    MessageBox.Show($"License Renewes Successfuly With ID ={_NewLicenses.LicenseID}");
                }
                else
                {
                    MessageBox.Show("Operation failed; please try again.");
                    return;
                }
            }
            clsLicenses.InActiveLicense(_OldLicenses.LicenseID);
            lblAppID.Text = _application.ApplicationID.ToString();
            lblRenewL.Text = _NewLicenses.LicenseID.ToString();
        }
        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            cntrlFindLicense1.FindLicense += _LoadDelegat;
            _user = clsUser.Find(_UserID);
            lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblAppFees.Text = clsApplicationType.GetFeesType(2).ToString();
            lblUserName.Text = _user.UserName;
            btnRenew.Enabled = false;
            llShowNewLInfo.Enabled = false;
            llSLHistory.Enabled = false;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            _RenewLicense();
            llShowNewLInfo.Enabled = true;
            btnRenew.Enabled = false;
        }

        private void llShowNewLInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicensesByLiceID(_NewLicenses.LicenseID);
            frm.ShowDialog();

        }

        private void llSLHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseHistory(clsDriver.FindByDriverID(_OldLicenses.DriverID).PersonID);
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
