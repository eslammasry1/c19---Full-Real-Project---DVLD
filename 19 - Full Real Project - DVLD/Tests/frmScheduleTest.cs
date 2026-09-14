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
    public partial class frmScheduleTest : Form
    {
        private clsAppAndDLInfo _AppAndDLInfo;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestTypeID = -1;
        private int _TestAppointmentID = -1;
        private int _CreatedByUserID = -1;
        private decimal _TotalFees = -1;

        private clsScheduleTest _ScheduleTest;
        public frmScheduleTest(int CreatedByUserID,int TestAppointmentID,int LocalDrivingLicenseApplicationID, int TestTypeID)
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

        private void _TestTitle()
        {
            if (clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID) == 0)
            {
                lblTestTitle.Text = "Schedule Test";
                gbRetake.Enabled = false;
                _TotalFees = clsScheduleTest.FeesTest(_TestTypeID);
            }
            else
            {
                _TotalFees = clsScheduleTest.FeesTest(_TestTypeID) + 5;
                lblTestTitle.Text = "Schedule Retake Test";
                gbRetake.Enabled = true;
                lblRAppFees.Text = 5.ToString();
            }
        }

        private void _appAndDLInfo()
        {
            _AppAndDLInfo = clsAppAndDLInfo.Find(_LocalDrivingLicenseApplicationID);
            if (_AppAndDLInfo == null)
            {
                return;
            }
            lblLDLVId.Text = _AppAndDLInfo.DLid.ToString();
            lblClass.Text = _AppAndDLInfo.ClassName.ToString();
            lblName.Text = _AppAndDLInfo.FullName.ToString();

        }
        private void _UpdateAddSchedule()
        {
            if (_TestAppointmentID != -1)
            {
                _ScheduleTest = clsScheduleTest.Find(_TestAppointmentID);
                if (_ScheduleTest == null)
                {
                    return;
                }
                dtpDate.Value = _ScheduleTest.AppointmentDate;
                if (clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID) != 0)
                {
                    lblRAppID.Text = _ScheduleTest.TestAppointmentID.ToString();
                }
            }
            else if (_TestAppointmentID == -1)
            {
                dtpDate.MinDate = DateTime.Today;
                _ScheduleTest = new clsScheduleTest();
            }
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            _appAndDLInfo();
            lblFees.Text = ((int)clsScheduleTest.FeesTest(_TestTypeID)).ToString();
            lblTrial.Text = clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID).ToString();
            _TestTitle();
            _UpdateAddSchedule();
            _GBName();
            lblTotalFees.Text = ((int)_TotalFees).ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ScheduleTest.TestTypeID = _TestTypeID;
            _ScheduleTest.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _ScheduleTest.AppointmentDate = dtpDate.Value;
            _ScheduleTest.PaidFees = _TotalFees;
            _ScheduleTest.CreatedByUserID = _CreatedByUserID;
            if (_ScheduleTest.Save())
            {
                MessageBox.Show("Data Saved Successfully");
            }
            else
            {
                MessageBox.Show("ERORR : Data  IS NOT Saved Successfully");
            }
            if (clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID) != 0)
            {
                lblRAppID.Text = _ScheduleTest.TestAppointmentID.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gbType_Enter(object sender, EventArgs e)
        {

        }
    }
}
