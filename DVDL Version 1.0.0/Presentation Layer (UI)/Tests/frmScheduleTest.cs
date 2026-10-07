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
    public partial class frmScheduleTest : Form
    {
        private clsAppAndDLInfo _AppAndDLInfo;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestTypeID = -1;
        private int _TestAppointmentID = -1;
        private int _CreatedByUserID = -1;
        private decimal _TotalFees = -1;
        private clsApplication _application;

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
        private void _Loked()
        {
            lblLoked.Visible = _TestAppointmentID != -1 && _ScheduleTest.IsLooked;
            btnSave.Enabled = !_ScheduleTest.IsLooked;
        }

        private void _TestTitle()
        {
            if (_ScheduleTest == null)
            {
                if (clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID) == 0)
                {
                    lblTestTitle.Text = "Schedule Test";
                }
                else
                {
                    lblTestTitle.Text = "Schedule Retake Test";
                }
            }
            else
            {
                if ( _ScheduleTest.RetakeTestApplicationID != 0)
                {
                    lblTestTitle.Text = "Schedule Retake Test";
                }
                else
                {
                    lblTestTitle.Text = "Schedule Test";
                }
            }
        }
        private void _Fees()
        {
            lblFees.Text = clsScheduleTest.FeesTest(_TestTypeID).ToString();
            if (clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID) == 0&& _TestAppointmentID == -1)
            {
                gbRetake.Enabled = false;
                lblTotalFees.Text = clsScheduleTest.FeesTest(_TestTypeID).ToString();
                lblRAppID.Text = "N/A";
                lblRAppFees.Text = 0.ToString();
            }
            else if (clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID) != 0 && _TestAppointmentID == -1)
            {
                lblTotalFees.Text = (clsScheduleTest.FeesTest(_TestTypeID) + clsApplicationType.GetFeesType(7)).ToString();
                lblRAppID.Text = "N/A";
                lblRAppFees.Text = clsApplicationType.GetFeesType(7).ToString();
            }
            else if (_TestAppointmentID != -1)
            {
                if ( _ScheduleTest.RetakeTestApplicationID !=0)
                {
                    lblTotalFees.Text = (clsScheduleTest.FeesTest(_TestTypeID) + clsApplicationType.GetFeesType(7)).ToString();
                    lblRAppID.Text = _ScheduleTest.RetakeTestApplicationID.ToString();
                    lblRAppFees.Text = clsApplicationType.GetFeesType(7).ToString();
                }
                else
                {
                    gbRetake.Enabled = false;
                    lblTotalFees.Text = clsScheduleTest.FeesTest(_TestTypeID).ToString();
                    lblRAppID.Text = "N/A";
                    lblRAppFees.Text = 0.ToString();
                }
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
                lblRAppID.Text = _ScheduleTest.RetakeTestApplicationID == 0 ? "N/A" : _ScheduleTest.RetakeTestApplicationID.ToString();
                lblLoked.Visible = _ScheduleTest.IsLooked;
                dtpDate.Enabled = !_ScheduleTest.IsLooked;
            }
            else if (_TestAppointmentID == -1)
            {
                dtpDate.MinDate = DateTime.Today;
                _ScheduleTest = new clsScheduleTest();
                
            }
        }
        private void _AddNewApplication()
        {
            
            _application = new clsApplication();
            _application.ApplicantPersonID =clsApplication_LDLA.Find(_LocalDrivingLicenseApplicationID).PersonID;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationTypeID = 7;
            _application.ApplicationStatus = 3;
            _application.LastStatusDate = DateTime.Now;
            _application.PaidFees = clsApplicationType.GetFeesType(7);
            _application.CreatedByUserID = _CreatedByUserID;
            _application.AddNewApplication();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            _UpdateAddSchedule();
            _appAndDLInfo();
            lblFees.Text = ((int)clsScheduleTest.FeesTest(_TestTypeID)).ToString();
            lblTrial.Text = clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID).ToString();
            _TestTitle();
            _GBName();
            _Fees();
            _Loked();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ScheduleTest.TestTypeID = _TestTypeID;
            _ScheduleTest.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _ScheduleTest.AppointmentDate = dtpDate.Value;
            _ScheduleTest.PaidFees =clsScheduleTest.FeesTest(_TestTypeID);
            _ScheduleTest.CreatedByUserID = _CreatedByUserID;
            if (clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID) != 0 && _TestAppointmentID == -1)
            {
                _AddNewApplication();
                _ScheduleTest.RetakeTestApplicationID = _application.ApplicationID;
            }
            else if (_TestAppointmentID  == -1) 
            {
                _ScheduleTest.RetakeTestApplicationID = 0;
            }

            if (_ScheduleTest.Save())
            {
                MessageBox.Show("Data Saved Successfully");
            }
            else
            {
                MessageBox.Show("ERORR : Data  IS NOT Saved Successfully");
                return;
            }
            if (clsScheduleTest.TrialAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID) != 0)
            {
                lblRAppID.Text = _ScheduleTest.RetakeTestApplicationID.ToString();
            }
            _TestAppointmentID = _ScheduleTest.TestAppointmentID;
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
