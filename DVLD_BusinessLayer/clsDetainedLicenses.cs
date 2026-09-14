using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsDetainedLicenses
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        public int? ReleaseApplicationID { get; set; }

        public clsDetainedLicenses()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleaseDate = null;
            ReleasedByUserID = null;
            ReleaseApplicationID = null;
        }

        private clsDetainedLicenses(
            int DetainID,
            int LicenseID,
            DateTime DetainDate,
            decimal FineFees,
            int CreatedByUserID,
            bool IsReleased,
            DateTime? ReleaseDate,
            int? ReleasedByUserID,
            int? ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
        }


        // Add New Detained License
        public bool AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesDB.DetainedLicenses(
                this.LicenseID,
                this.FineFees,
                this.CreatedByUserID);

            return this.DetainID != -1;
        }


        // Release Detained License
        public bool ReleaseLicense(int DetainID,int ReleasedByUserID, int ReleaseApplicationID)
        {
            bool Result = clsDetainedLicensesDB.ReleaseLicense(
                DetainID,
                ReleasedByUserID,
                ReleaseApplicationID);

            if (Result)
            {
                this.IsReleased = true;
                this.ReleaseDate = DateTime.Now;
                this.ReleasedByUserID = ReleasedByUserID;
                this.ReleaseApplicationID = ReleaseApplicationID;
            }

            return Result;
        }


        // Find By Detain ID
        public static clsDetainedLicenses FindByDetainID(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int? ReleasedByUserID = null;
            int? ReleaseApplicationID = null;

            bool IsFound =
                clsDetainedLicensesDB.GetDetailedLicenseInfoByDetainID(
                    DetainID,
                    ref LicenseID,
                    ref DetainDate,
                    ref FineFees,
                    ref CreatedByUserID,
                    ref IsReleased,
                    ref ReleaseDate,
                    ref ReleasedByUserID,
                    ref ReleaseApplicationID);

            if (IsFound)
            {
                return new clsDetainedLicenses(
                    DetainID,
                    LicenseID,
                    DetainDate,
                    FineFees,
                    CreatedByUserID,
                    IsReleased,
                    ReleaseDate,
                    ReleasedByUserID,
                    ReleaseApplicationID);
            }

            return null;
        }
        public static clsDetainedLicenses FindByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int? ReleasedByUserID = null;
            int? ReleaseApplicationID = null;

            bool IsFound =
                clsDetainedLicensesDB.GetDetailedLicenseInfoByLicenseID(
                    LicenseID,
                    ref DetainID,
                    ref DetainDate,
                    ref FineFees,
                    ref CreatedByUserID,
                    ref IsReleased,
                    ref ReleaseDate,
                    ref ReleasedByUserID,
                    ref ReleaseApplicationID);

            if (IsFound)
            {
                return new clsDetainedLicenses(
                    DetainID,
                    LicenseID,
                    DetainDate,
                    FineFees,
                    CreatedByUserID,
                    IsReleased,
                    ReleaseDate,
                    ReleasedByUserID,
                    ReleaseApplicationID);
            }

            return null;
        }


        // Check If Detain Exists And Not Released
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicensesDB.LicensesIsDetain(LicenseID);
        }

        public static DataTable GetDetainedLicenses()
        {
            return clsDetainedLicensesDB.GetDetainedLicenses();
        }
        // Get All By Detain ID
        public static DataTable GetDetainedLicensesByDetainID(int DetainID)
        {
            return clsDetainedLicensesDB.GetDetainedLicensesByDetainID(DetainID);
        }


        // Get All By License ID
        public static DataTable GetDetainedLicensesByLicenseID(int LicenseID)
        {
            return clsDetainedLicensesDB.GetDetainedLicensesByLicenseID(LicenseID);
        }


        // Get All By Is Released
        public static DataTable GetDetainedLicensesByIsReleased(bool IsReleased)
        {
            return clsDetainedLicensesDB.GetDetainedLicensesByIsReleased(IsReleased);
        }


        // Get All By National Number
        public static DataTable GetDetainedLicensesByNationalNo(string NationalNo)
        {
            return clsDetainedLicensesDB.GetDetainedLicensesByNationalNo(NationalNo);
        }


        // Get All By Full Name
        public static DataTable GetDetainedLicensesByFullName(string FullName)
        {
            return clsDetainedLicensesDB.GetDetainedLicensesByFullName(FullName);
        }


        // Get All By Release Application ID
        public static DataTable GetDetainedLicensesByReleaseApplicationID(
            int ReleaseApplicationID)
        {
            return clsDetainedLicensesDB.GetDetainedLicensesByReleaseApplicationID(ReleaseApplicationID);
        }
    }
}