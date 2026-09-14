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

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmDetainLicense : Form
    {
        clsUser _user;
        clsDetainedLicenses _DetainedL;
        int _Userid;
        clsLicenses _Licenses;
        public frmDetainLicense(int userid)
        {
            InitializeComponent();
            _Userid = userid;
        }
        private void _LoadDelegat(object sender, int licenseID)
        {
            _Licenses = clsLicenses.Find(licenseID);
            if (clsDetainedLicenses.IsLicenseDetained(licenseID))
            {
                MessageBox.Show("Selected License Is Already Detained,choose another one","Not Allowed",MessageBoxButtons.OK ,MessageBoxIcon.Error);
                return;
            }
            btnDetain.Enabled = true;
            llSLHistory.Enabled = true;
            lblLicenseID.Text = licenseID.ToString();
        }
        private void _DetainLicense()
        {
            _DetainedL = new clsDetainedLicenses();
            _DetainedL.LicenseID = _Licenses.LicenseID;
            _DetainedL.FineFees = tbDetainFees.Text == "" ? 0 : Convert.ToDecimal(tbDetainFees.Text);
            _DetainedL.CreatedByUserID = _Userid;
            if (MessageBox.Show("Are you sure you want to Detaine this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_DetainedL.AddNewDetainedLicense())
                {
                    MessageBox.Show($"License Detained Successfuly With ID ={_DetainedL.LicenseID}");
                }
                else
                {
                    MessageBox.Show("Operation failed; please try again.");
                    return;
                }
            }

            lblDetainID.Text = _DetainedL.DetainID.ToString();
            btnDetain.Enabled = false;
            llShowLafterUpdate.Enabled = true;
            cntrlFindLicense1.DisableGbFilter();
        }
        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            cntrlFindLicense1.FindLicense += _LoadDelegat;
            _user =  clsUser.Find(_Userid);
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblUserName.Text = _user.UserName;
            llShowLafterUpdate.Enabled = false;
            llSLHistory.Enabled = false;
            btnDetain.Enabled = false;

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            _DetainLicense();
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

        private void cntrlFindLicense1_Load(object sender, EventArgs e)
        {

        }
    }
}
