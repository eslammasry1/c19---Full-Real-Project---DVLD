using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace _19___Full_Real_Project___DVLD2
{
    public partial class cntrlInternationalInfo : UserControl
    {
        public cntrlInternationalInfo()
        {
            InitializeComponent();
        }
        public void Load(int ID)
        {
            clsInternationalLicenseInfo internationalLicenseInfo;
            internationalLicenseInfo = clsInternationalLicenseInfo.Find(ID);
            lblName.Text = internationalLicenseInfo.FullName;
            lblInternationalID.Text = internationalLicenseInfo.InternationalLicenseID.ToString();
            lblLicensesID.Text = internationalLicenseInfo.LocalLicenseID.ToString();
            lblNationalNo.Text = internationalLicenseInfo.NationalNo;
            lblGendor.Text = internationalLicenseInfo.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text = internationalLicenseInfo.IssueDate.ToString("dd/MMM/yyyy");
            lblAppID.Text = internationalLicenseInfo.ApplicationID.ToString();
            lblActive.Text = internationalLicenseInfo.IsActive ? "Yes" : "No";
            lblDateBirth.Text = internationalLicenseInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblDriverID.Text = internationalLicenseInfo?.DriverID.ToString();
            lblExpirationDate.Text = internationalLicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy");

            if (!string.IsNullOrEmpty(internationalLicenseInfo.ImagePath) && File.Exists(internationalLicenseInfo.ImagePath))
            {
                pbImage.ImageLocation = internationalLicenseInfo.ImagePath;
            }
            else
            {
                if (internationalLicenseInfo.Gender == 0)
                    pbImage.Image = Properties.Resources.icons8_person_94;
                else
                    pbImage.Image = Properties.Resources.icons8_person_96;
            }



        }
        private void cntrlInternationalInfo_Load(object sender, EventArgs e)
        {

        }

        private void cntrlInternationalInfo_Load_1(object sender, EventArgs e)
        {

        }
    }
}
