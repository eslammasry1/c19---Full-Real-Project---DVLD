using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsLicenses
    {
        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplacementforDamaged = 3, ReplacementforLost = 4 }


        public int LicenseID {  get; set; }
        public int ApplicationID {  get; set; }
        public int DriverID {  get; set; }
        public int LicensesClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Note { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public int IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public clsLicenses(int LicenseID,int ApplicationID, int DriverID, int LicensesClass, DateTime IssueDate, DateTime ExpirationDate,
                                      string Note, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicensesClass = LicensesClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Note = Note;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
        }
        public clsLicenses()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicensesClass = -1;
            this.IssueDate = new DateTime();
            this.ExpirationDate = new DateTime();
            this.Note = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = -1;
            this.CreatedByUserID = -1;
        }
        public bool AddNewLicense()
        {
            this.LicenseID = clsLicensesDB.AddLicenses(this.ApplicationID,this.DriverID, this.LicensesClass, this.IssueDate, this.ExpirationDate,
                                                       this.Note, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
            return this.LicenseID != -1;
        }
        public static clsLicenses Find(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicensesClass = -1;
            DateTime IssueDate = new DateTime();
            DateTime ExpirationDate = new DateTime();
            string Note = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            int IssueReason = -1;
            int CreatedByUserID = -1;
            if (clsLicensesDB.GetLicensesInfoByLicenseID(LicenseID, ref ApplicationID, ref DriverID, ref LicensesClass, ref IssueDate, ref ExpirationDate, ref Note, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicensesClass, IssueDate, ExpirationDate, Note, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else 
                return null;
        }
        public static decimal GetLicensesClassFees(int LicensesClassID)
        {
            return clsLicensesDB.GetLicensesClassFees(LicensesClassID);
        }
        public static DataTable GetLicensesOfPerson(int PersonID)
        {
            return clsLicensesDB.GetLicensesOfPerson(PersonID);
        }
        public static int GeTDefaultValidityLength(int LicenseClassID)
        {
            return clsLicensesDB.GeTDefaultValidityLength(LicenseClassID);
        }

        public static bool ExistLicensesByApplicationID(int LdlaId)
        {
            clsApplication_LDLA cls = clsApplication_LDLA.Find(LdlaId);
            return clsLicensesDB.ExistLicensesByApplicationID(cls.AppId);
        }

        public static bool InActiveLicense(int LicenseId)
        {
            return clsLicensesDB.InActiveLicense(LicenseId);
        }
    }
}
