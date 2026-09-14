using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmTestType : Form
    {
        private clsAppAndDLInfo _AppAndDLInfo;
        private clsScheduleTest _ScheduleTest;
        private clsTest _Test;

        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestTypeID = -1;
        private int _TestAppointmentID = -1;
        private int _CreatedByUserID = -1;

        public frmTestType(int CreatedByUserID, int TestAppointmentID, int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _TestAppointmentID = TestAppointmentID;
            _CreatedByUserID = CreatedByUserID;

        }
        private void _GBName()
        {
            if (_TestTypeID == 1)
            {
                gbType.Text = "vision Test";
                pbType.Image = Properties.Resources.eye_icon;

            }
            else if (_TestTypeID == 2)
            {
                gbType.Text = "Written Test";
                pbType.Image = Properties.Resources.Writing_exam_icon;
            }
            else
            {
                gbType.Text = "Street Test";
                pbType.Image = Properties.Resources.street_exam_icon_2;

            }
        }
        private void _appAndDLInfo()
        {
            _AppAndDLInfo = clsAppAndDLInfo.Find(_LocalDrivingLicenseApplicationID);
            _ScheduleTest = clsScheduleTest.Find(_TestAppointmentID);
            if (_AppAndDLInfo == null || _ScheduleTest == null)
            {
                return;
            }
            lblLDLVId.Text = _AppAndDLInfo.DLid.ToString();
            lblClass.Text = _AppAndDLInfo.ClassName.ToString();
            lblName.Text = _AppAndDLInfo.FullName.ToString();
            lblDate.Text = _ScheduleTest.AppointmentDate.ToString();
            lblFees.Text = _ScheduleTest.PaidFees.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = tbNotes.Text;
            _Test.CreatedByUserID = _CreatedByUserID;
            if(MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.","Confirm"
               ,MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)== DialogResult.Yes)
            {
                if(_Test.TakingTest())
                {
                    MessageBox.Show("Data Saved Successfully");
                }
                else
                {
                    MessageBox.Show("ERORR : Data  IS NOT Saved Successfully");
                    return;
                }

            }
            btnSave.Enabled = false;
            lblTestID.Text = _Test.TestID.ToString();
        }

        private void frmTestType_Load(object sender, EventArgs e)
        {
            _GBName();
            _appAndDLInfo();
            rbPass.Checked = true;
            _Test = new clsTest();
            lblTrial.Text = clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID).ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
