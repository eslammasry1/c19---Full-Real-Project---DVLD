using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsInternationalLicense
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }


        public clsInternationalLicense(
            int ApplicationID,
            int DriverID,
            int IssuedUsingLocalLicenseID,
            DateTime IssueDate,
            DateTime ExpirationDate,
            bool IsActive,
            int CreatedByUserID)
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
        }


        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;
        }


        public bool AddNewInternationalLicense()
        {
            this.InternationalLicenseID =
                clsInternationalDB.AddNewInternationalLicense(
                    this.ApplicationID,
                    this.DriverID,
                    this.IssuedUsingLocalLicenseID,
                    this.IssueDate,
                    this.ExpirationDate,
                    this.IsActive,
                    this.CreatedByUserID
                );

            return this.InternationalLicenseID != -1;
        }
        public static DataTable GetInternationalLicensesOfPerson(int PersonID)
        {
            return clsInternationalDB.GetInternationalLicensesOfPerson(PersonID);
        }
        public static bool HaveActiveInternationalLicenses(int DriverID)
        {
            return clsInternationalDB.HaveActiveInternationalLicenses(DriverID);
        }
        public static DataTable GetInternationalInfo()
        {
            return clsInternationalDB.GetInternationalInfo();
        }
        public static DataTable GetInternationalInfoFilterByInterID(int InternationalLicenseID)
        {
            return clsInternationalDB.GetInternationalInfoFilterByInterID(InternationalLicenseID);
        }
        public static DataTable GetInternationalInfoFilterByApplicationID(int ApplicationID)
        {
            return clsInternationalDB.GetInternationalInfoFilterByApplicationID(ApplicationID);
        }
        public static DataTable GetInternationalInfoFilterByDriverID(int DriverID)
        {
            return clsInternationalDB.GetInternationalInfoFilterByDriverID(DriverID);
        }
        public static DataTable GetInternationalInfoFilterByLLicenseID(int LLicenseID)
        {
            return clsInternationalDB.GetInternationalInfoFilterByLLicenseID(LLicenseID);
        }
    }
}
