using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;
namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmAddEditLDLAapp : Form
    {
        public enum enMode { AddNewApplication = 0, UpdateApplication = 1 }
        enMode Mode = enMode.AddNewApplication;

        private int _PersonID = -1;
        private clsApplication_LDLA _App_Ldla;
        private int LDLAid = -1;
        private int _UserID = -1;
        public frmAddEditLDLAapp(int LdlaId,int userID)
        {
            InitializeComponent();
            LDLAid = LdlaId;
            _UserID = userID;
            if (LDLAid == -1)
                Mode = enMode.AddNewApplication;
            else
                Mode = enMode.UpdateApplication;
        }
        private void _FillCountriesInComboBox()
        {
            DataTable dt = clsApplication_LDLA.GetLinceseClass();

            foreach (DataRow DT in dt.Rows)
            {
                cbLicenseType.Items.Add(DT["ClassName"]);
            }
            cbLicenseType.SelectedIndex = 0;
        }

        private void frmAddLDLAapp_Load(object sender, EventArgs e)
        {
            _FillCountriesInComboBox();
            if (Mode  == enMode.UpdateApplication)
            {
                _App_Ldla = clsApplication_LDLA.Find(LDLAid);
                clsUser User = clsUser.Find(_App_Ldla.CreatedByUserID);
                lblDLID.Text = _App_Ldla.LDLAid.ToString();
                cntrlFindPerson1.LoadUserInfo(_App_Ldla.PersonID);
                lblUserName.Text = User.UserName;
                lblAppFees.Text = _App_Ldla.PaidFees.ToString();
                lblAppDate.Text = _App_Ldla.ApplicationDate.ToString();
                cbLicenseType.SelectedIndex = _App_Ldla.LicenseClassID - 1;
                lblTitle.Text = "Update Local Driving License Application";
                return;
            }
            if (Mode == enMode.AddNewApplication)
            {
                clsUser User = clsUser.Find(_UserID);
                _App_Ldla = new clsApplication_LDLA();
                lblAppDate.Text = DateTime.Now.ToString("dd/mm/yyyy");
                lblAppFees.Text = "15";
                lblUserName.Text = User .UserName;
                lblTitle.Text = "New Local Driving License Application";

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (cntrlFindPerson1.PersonId == -1)
            {
                MessageBox.Show("You must select a person to place the order.");
                return;
            }
            else
            {
                tabAddEditLicense.SelectedIndex = 1;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cntrlFindPerson1.PersonId == -1)
            {
                MessageBox.Show("You must select a person to place the order.");
                return;
            }
            if (clsApplication_LDLA.ExistApplicationStatus(cntrlFindPerson1.PersonId,cbLicenseType.SelectedIndex+1))
            {
                MessageBox.Show("This request was submitted recently and is still being processed.");
                return;
            }
            if (clsApplication_LDLA.ApplicationIsCompleted(cntrlFindPerson1.PersonId,cbLicenseType.SelectedIndex+1))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class.", "Confirm"
               , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _App_Ldla.PersonID = cntrlFindPerson1.PersonId;
            _App_Ldla.LicenseClassID = cbLicenseType.SelectedIndex + 1;
            _App_Ldla.CreatedByUserID = _UserID;
            if (_App_Ldla.Save())
            {
                MessageBox.Show("Data Saved Successfully");
            }
            else
            {
                MessageBox.Show("ERORR : Data  IS NOT Saved Successfully");
                return;
            }
            lblDLID.Text = _App_Ldla.LDLAid.ToString();
            lblTitle.Text = "Update Local Driving License Application";
            // clsApplication_LDLA.Find(_App_Ldla.LDLAid);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
