using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsScheduleTest
    {
        public enum enMode { AddNewAppointment = 0, UpdateAppointment = 1 }
        enMode Mode = enMode.AddNewAppointment;

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLooked { get; set; }

        clsScheduleTest(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, decimal paidFees, int createdByUserID, bool isLooked)
        {
            this.TestAppointmentID = testAppointmentID;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.AppointmentDate = appointmentDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.IsLooked = isLooked;
            Mode = enMode.UpdateAppointment;
        }
        public clsScheduleTest()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = new DateTime();
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLooked = false;
            Mode = enMode.AddNewAppointment;
        }
        public static clsScheduleTest Find(int ID)
        {
            int testTypeID = -1;
            int localDrivingLicenseApplicationID = -1;
            DateTime appointmentDate = new DateTime();
            decimal paidFees = -1;
            int createdByUserID = -1;
            bool isLooked = false;


            if (clsScheduleTestDB.GetAppointmentByID(ID, ref testTypeID, ref localDrivingLicenseApplicationID, ref appointmentDate
                                                     , ref paidFees, ref createdByUserID, ref isLooked))
                return new clsScheduleTest(ID, testTypeID, localDrivingLicenseApplicationID, appointmentDate, paidFees, createdByUserID, isLooked);
            else
                return null;
        }
        private bool _AddNewAppointment()
        {
            this.TestAppointmentID = clsScheduleTestDB.AddAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees,
                                                     this.CreatedByUserID);
            return this.TestAppointmentID != -1;
        }
        private bool _UpdateAppointment()
        {
            return clsScheduleTestDB.UpdateAppointment(this.TestAppointmentID, this.AppointmentDate);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNewAppointment:
                    if (_AddNewAppointment())
                    {
                        Mode = enMode.UpdateAppointment;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateAppointment:
                    return _UpdateAppointment();
            }
            return false;
        }

        public static DataTable AppointmentInfo(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsScheduleTestDB.GetAppointmentInfo(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public static bool TestIsPassed(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsScheduleTestDB.GeTTestIsPassed(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public static decimal FeesTest(int TestTypeID)
        {
            return clsScheduleTestDB.GetFeesTest(TestTypeID);
        }
        public static int CountTestPassed(int LocalDrivingLicenseApplicationID)
        {
            return clsScheduleTestDB.GetCountTestPassed(LocalDrivingLicenseApplicationID) ;
        }
        public static bool ISExistAppointments(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            return clsScheduleTestDB.ISExistAppointments(TestTypeID, LocalDrivingLicenseApplicationID);
        }
        public static bool ISExistFinshedAppointments(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            return clsScheduleTestDB.ISExistFinshedAppointments(TestTypeID, LocalDrivingLicenseApplicationID);
        }
        public static bool IsPassThisTest(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            return clsScheduleTestDB.IsPassThisTest(TestTypeID, LocalDrivingLicenseApplicationID);
        }
        public static bool ISLocked(int TestAppointmentID)
        {
            return clsScheduleTestDB.GetIsLoked(TestAppointmentID);
        }
        public static int TrialAppointments(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return clsScheduleTestDB.GeTContAppointment(LocalDrivingLicenseApplicationID,TestTypeID);
        }


}

}
