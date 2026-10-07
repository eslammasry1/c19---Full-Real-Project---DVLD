using BusinessLayer;
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
    public partial class frmReleaseLicense : Form
    {
        private clsUser _user;
        private clsDetainedLicenses _DetainedL;
        private int _Userid = -1;
        private int _DetainID = -1;
        private clsLicenses _Licenses;
        clsApplication _application;
        public frmReleaseLicense(int detainID,int userId)
        {
            InitializeComponent();
            _DetainID = detainID;
            _Userid = userId;
        }
        public void _Selected()
        {
            _DetainedL = clsDetainedLicenses.FindByDetainID(_DetainID);
            _Licenses = clsLicenses.Find(_DetainedL.LicenseID);
            if (_DetainedL == null)
            {
                return;
            }

            cntrlFindLicense1.SelectedLicense(_DetainedL.LicenseID);
            lblDetainID.Text = _DetainedL.DetainID.ToString();
            lblDetainDate.Text = _DetainedL.DetainDate.ToString("dd/MMM/yyyy");
            lblAppFees.Text = ((int)clsApplicationType.GetFeesType(5)).ToString();
            lblLicenseID.Text = _DetainedL.LicenseID.ToString();
            lblFineFees.Text = ((int)_DetainedL.FineFees).ToString();
            lblTotalFees.Text = ((int)(clsApplicationType.GetFeesType(5) + _DetainedL.FineFees)).ToString();
            llSLHistory.Enabled = true;
            btnRelease.Enabled = true;
        }
        private void _LoadDelegatSearch(object sender, int licenseID)
        {
            if (!clsDetainedLicenses.IsLicenseDetained(licenseID))
            {
                MessageBox.Show("Selected license Is Not Detained,Choose Another One", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Licenses = clsLicenses.Find(licenseID);
            _DetainedL = clsDetainedLicenses.FindByLicenseID(licenseID);
            lblDetainID.Text = _DetainedL.DetainID.ToString();
            lblDetainDate.Text = _DetainedL.DetainDate.ToString("dd/MMM/yyyy");
            lblAppFees.Text = clsApplicationType.GetFeesType(5).ToString();
            lblFineFees.Text = ((int)_DetainedL.FineFees).ToString();
            lblTotalFees.Text = ((int)(clsApplicationType.GetFeesType(5) + _DetainedL.FineFees)).ToString();
            lblLicenseID.Text = licenseID.ToString();
            llSLHistory.Enabled = true;
            btnRelease.Enabled = true;
            _DetainID = _DetainedL.DetainID;

        }
        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {
            _user = clsUser.Find(_Userid);
            lblUserName.Text = _user.UserName;
            llShowLafterUpdate.Enabled = false;
            llSLHistory.Enabled = false;
            btnRelease.Enabled = false;
            if (_DetainID != -1)
            {
                _Selected();
            }
            else
            {
                cntrlFindLicense1.FindLicense += _LoadDelegatSearch;

            }
        }

        private void llShowLafterUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicensesByLiceID(_Licenses.LicenseID);
            frm.ShowDialog();
        }

        private void llSLHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseHistory(clsDriver.FindByDriverID(_Licenses.DriverID).PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _AddNewApplication()
        {
            _application = new clsApplication();
            _application.ApplicantPersonID = clsDriver.FindByDriverID(_Licenses.DriverID).PersonID;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationTypeID = 5;
            _application.ApplicationStatus = 3;
            _application.LastStatusDate = DateTime.Now;
            _application.PaidFees = clsApplicationType.GetFeesType(5);
            _application.CreatedByUserID = _Userid;
            _application.AddNewApplication();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            _AddNewApplication();
            if (MessageBox.Show("Are you sure you want to Release this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_DetainedL.ReleaseLicense(_DetainID,_Userid,_application.ApplicationID))
                {
                    MessageBox.Show($"License Release Successfuly");
                }
                else
                {
                    MessageBox.Show("Operation failed; please try again.");
                    return;
                }
            }
            btnRelease.Enabled = false;
            lblReleaseAppID.Text = _application.ApplicationID.ToString();
            llShowLafterUpdate.Enabled = true;
            cntrlFindLicense1.SelectedLicense(_DetainedL.LicenseID);

        }
    }
}
