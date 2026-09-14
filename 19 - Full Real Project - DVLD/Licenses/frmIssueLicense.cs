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
    public partial class frmIssueLicense : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _UserID;
        private clsApplication_LDLA _Application_LDLA;
        private clsLicenses _Licenses;
        public frmIssueLicense(int LocalDrivingLicenseApplicationID , int UserID)
        {                      
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _UserID = UserID;
        }

        private void _AddNewDriver()
        {

            clsDriver driver = clsDriver.FindByPersonID(_Application_LDLA.PersonID);
            if (driver == null )
            {
                clsDriver NewDriver = new clsDriver();
                NewDriver.PersonID = _Application_LDLA.PersonID;
                NewDriver.CreatedByUserID = _UserID;
                NewDriver.AddNewDriver();
            }
        }
        private bool _IssueLicense()
        {
            _Licenses = new clsLicenses();
            _Licenses.ApplicationID = _Application_LDLA.AppId;
            _Licenses.DriverID = clsDriver.FindByPersonID(_Application_LDLA.PersonID).DriverID;
            _Licenses.LicensesClass = _Application_LDLA.LicenseClassID;
            _Licenses.IssueDate = DateTime.Now;
            _Licenses.ExpirationDate = DateTime.Now.AddYears(clsLicenses.GeTDefaultValidityLength(_Application_LDLA.LicenseClassID));
            _Licenses.Note = tbNotes.Text;
            _Licenses.PaidFees = clsLicenses.GetLicensesClassFees(_Application_LDLA.LicenseClassID);
            _Licenses.IsActive = true;
            _Licenses.IssueReason = (int)clsLicenses.enIssueReason.FirstTime;
            _Licenses.CreatedByUserID = _UserID;
            return _Licenses.AddNewLicense();
        }
        private void frmIssueLicense_Load(object sender, EventArgs e)
        {
            _Application_LDLA = clsApplication_LDLA.Find(_LocalDrivingLicenseApplicationID);   
            cntrlAppAndLDLAInfo1.Load(_LocalDrivingLicenseApplicationID);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _AddNewDriver();
            if (_IssueLicense())
                MessageBox.Show($"Liscense Issued Successfully With License ={_Licenses.LicenseID}");
            else
                MessageBox.Show("ERORR : Data  IS NOT Saved Successfully");
            clsApplication_LDLA.CompleteApplication(_Application_LDLA.AppId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
