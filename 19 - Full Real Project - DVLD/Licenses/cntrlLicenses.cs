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
using System.IO;


namespace _19___Full_Real_Project___DVLD2
{
    public partial class cntrlLicenses : UserControl
    {
        public cntrlLicenses()
        {
            InitializeComponent();
        }
        public void LoadByLdla(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            ApplicationID = clsApplication_LDLA.Find(LocalDrivingLicenseApplicationID).AppId;
            clsLicensesInfo licensesInfo = clsLicensesInfo.FindByApplicationID(ApplicationID);
            lblClass.Text = licensesInfo.LicenseClass;
            lblName.Text = licensesInfo.FullName.ToString();
            lblLicensesID.Text = licensesInfo.LicenseID.ToString();
            lblNationalNo.Text = licensesInfo.NationalNo.ToString();
            lblGendor.Text = licensesInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = licensesInfo.IssueDate.ToString();
            lblReason.Text = licensesInfo.IssueReason == 1 ? "First Time" :
                             licensesInfo.IssueReason == 2 ? "Renew" :
                             licensesInfo.IssueReason == 3 ? "Replacement for Damaged" :
                             licensesInfo.IssueReason == 4 ? "Replacement for Lost" : "";
            lblNotes.Text = licensesInfo.Notes == "" ? "No Notes" : licensesInfo.Notes.ToString();
            lblActive.Text = licensesInfo.IsActive ? "Yes" : "No";
            lblDateBirth.Text = licensesInfo.DateOfBirth.ToString();
            lblDriverID.Text = licensesInfo.DriverID.ToString();
            lblExpirationDate.Text = licensesInfo.ExpirationDate.ToString();
            lblIsDetained.Text = licensesInfo.IsDetained == 1 ? "yes" : "No";
            if (!string.IsNullOrEmpty(licensesInfo.ImagePath) && File.Exists(licensesInfo.ImagePath))
            {
                pbImage.ImageLocation = licensesInfo.ImagePath;
            }
            else
            {
                if (licensesInfo.Gendor == 0)
                    pbImage.Image = Properties.Resources.icons8_person_94;
                else
                    pbImage.Image = Properties.Resources.icons8_person_96;
            }

        }
        public void LoadByLicenseID(int LicenseID)
        {
            clsLicensesInfo licensesInfo = clsLicensesInfo.FindByLicenseID(LicenseID);
            lblClass.Text = licensesInfo.LicenseClass;
            lblName.Text = licensesInfo.FullName.ToString();
            lblLicensesID.Text = licensesInfo.LicenseID.ToString();
            lblNationalNo.Text = licensesInfo.NationalNo.ToString();
            lblGendor.Text = licensesInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = licensesInfo.IssueDate.ToString();
            lblReason.Text = licensesInfo.IssueReason == 1 ? "First Time" :
                             licensesInfo.IssueReason == 2 ? "Renew" :
                             licensesInfo.IssueReason == 3 ? "Replacement for Damaged" :
                             licensesInfo.IssueReason == 4 ? "Replacement for Lost" : "";
            lblNotes.Text = licensesInfo.Notes == "" ? "No Notes" : licensesInfo.Notes.ToString();
            lblActive.Text = licensesInfo.IsActive ? "Yes" : "No";
            lblDateBirth.Text = licensesInfo.DateOfBirth.ToString();
            lblDriverID.Text = licensesInfo.DriverID.ToString();
            lblExpirationDate.Text = licensesInfo.ExpirationDate.ToString();
            lblIsDetained.Text = licensesInfo.IsDetained == 1 ? "yes" : "No";
            if (!string.IsNullOrEmpty(licensesInfo.ImagePath) && File.Exists(licensesInfo.ImagePath))
            {
                pbImage.ImageLocation = licensesInfo.ImagePath;
            }
            else
            {
                if (licensesInfo.Gendor == 0)
                    pbImage.Image = Properties.Resources.icons8_person_94;
                else
                    pbImage.Image = Properties.Resources.icons8_person_96;
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cntrlLicenses_Load(object sender, EventArgs e)
        {

        }
    }
}
