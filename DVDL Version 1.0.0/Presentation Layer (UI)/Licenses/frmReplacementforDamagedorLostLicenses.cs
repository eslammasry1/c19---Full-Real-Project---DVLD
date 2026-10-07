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
    public partial class frmReplacementforDamagedorLostLicenses : Form
    {
        private int _UserID = -1;
        private clsUser _user;
        private clsLicenses _OldLicenses;
        private clsLicenses _NewLicenses;
        private clsApplication _application;
        private clsLdla _Ldla;

        public frmReplacementforDamagedorLostLicenses(int UserID)
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
            lblOldL.Text = _OldLicenses.LicenseID.ToString();
            llSLHistory.Enabled = true;
            btnReplace.Enabled = true;
        }
        private void _AddNewApplication()
        {
            _application = new clsApplication();
            _application.ApplicantPersonID = clsDriver.FindByDriverID(_OldLicenses.DriverID).PersonID;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationTypeID = rbDamagedL.Checked ? 4 : 3;
            _application.ApplicationStatus = 3;
            _application.LastStatusDate = DateTime.Now;
            _application.PaidFees = clsApplicationType.GetFeesType(rbDamagedL.Checked ? 4 : 3);
            _application.CreatedByUserID = _UserID;
            _application.AddNewApplication();
        }
        private void _AddNewLdla()
        {
            _AddNewApplication();
            _Ldla = new clsLdla();
            _Ldla.ApplicationID = _application.ApplicationID;
            _Ldla.LicenseClassID = _OldLicenses.LicensesClass;
            _Ldla._AddNewLocalDrivingLicenseApplication();
        }
        private void _ReplaceLicense()
        {
            _AddNewLdla();
            _NewLicenses = new clsLicenses();
            _NewLicenses.ApplicationID = _application.ApplicationID;
            _NewLicenses.DriverID = _OldLicenses.DriverID;
            _NewLicenses.LicensesClass = _OldLicenses.LicensesClass;
            _NewLicenses.IssueDate = DateTime.Now;
            _NewLicenses.ExpirationDate = _OldLicenses.ExpirationDate;
            _NewLicenses.Note = "";
            _NewLicenses.PaidFees = 0;
            _NewLicenses.IsActive = true;
            _NewLicenses.IssueReason = rbDamagedL.Checked ? (int)clsLicenses.enIssueReason.ReplacementforDamaged : (int)clsLicenses.enIssueReason.ReplacementforLost;
            _NewLicenses.CreatedByUserID = _UserID;
            if (MessageBox.Show("Are you sure you want to Replace this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_NewLicenses.AddNewLicense())
                {
                    MessageBox.Show($"The License has been successfully replaced With ID ={_NewLicenses.LicenseID}");
                }
                else
                {
                    MessageBox.Show("Operation failed; please try again.");
                    return;
                }
            }
            clsLicenses.InActiveLicense(_OldLicenses.LicenseID);
            lblAppID.Text = _application.ApplicationID.ToString();
            lblReplaceL.Text = _NewLicenses.LicenseID.ToString();
        }

        private void frmReplacementforDamagedorLostLicenses_Load(object sender, EventArgs e)
        {
            cntrlFindLicense1.FindLicense += _LoadDelegat;
            _user = clsUser.Find(_UserID);
            lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblUserName.Text = _user.UserName;
            rbDamagedL.Checked = true;
            btnReplace.Enabled = false;
            llShowNewLInfo.Enabled = false;
            llSLHistory.Enabled = false;

        }

        private void rbDamagedL_CheckedChanged(object sender, EventArgs e)
        {
            lblAppFees.Text = ((int)clsApplicationType.GetFeesType(4)).ToString();
            lblTitle.Text = "Replacement For Damaged License";
        }

        private void rbLostL_CheckedChanged(object sender, EventArgs e)
        {
            lblAppFees.Text = ((int)clsApplicationType.GetFeesType(3)).ToString();
            lblTitle.Text = "Replacement For Lost License";
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            _ReplaceLicense();
            llShowNewLInfo.Enabled = true;
            btnReplace.Enabled = false;

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
    }
}
