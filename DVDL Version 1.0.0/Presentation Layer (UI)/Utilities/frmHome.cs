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
    public partial class frmHome : Form 
    {
        private int _UserId = -1;
        private bool _Active = false;
        clsUser _user;
        public frmHome(int id)
        {
            InitializeComponent();
            _UserId = id;
        }

        private void pepoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPeople();
            frm.ShowDialog();

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageUsers();
            frm.ShowDialog();

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo(_UserId);
            frm.ShowDialog();


        }

        private void chanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(_UserId);
            frm.ShowDialog();


        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            _user = clsUser.Find(_UserId);
            Form frm = new frmLogin();
            frm.ShowDialog();

        }

        private void frmHome_Load(object sender, EventArgs e)
        {

        }

        private void manageApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageTestType();
            frm.ShowDialog();

        }

        private void newDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLDLA(_UserId);
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLDLA(_UserId);
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDrivers();
            frm.ShowDialog();  
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm =new frmAddNewInternationalLicense(_UserId);
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternational(_UserId);
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmRenewLicense(_UserId);
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReplacementforDamagedorLostLicenses(_UserId);
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicense(_UserId);
            frm.ShowDialog();

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense(-1,_UserId);
            frm.ShowDialog();

        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListDetain(_UserId);
            frm.ShowDialog();

        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense(-1, _UserId);
            frm.ShowDialog();

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLDLA(_UserId);
            frm.ShowDialog();
        }
    }
}

