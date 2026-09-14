using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsLicensesInfo
    {
        public int ApplicationID {  get; set; }
        public string LicenseClass {  get; set; }
        public string FullName {  get; set; }
        public int LicenseID { get; set; }
        public string NationalNo { get; set; }
        public int Gendor { get; set; }
        public DateTime IssueDate {  get; set; }
        public DateTime ExpirationDate { get; set; }
        public int IssueReason { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DriverID {  get; set; }
        public string ImagePath {  get; set; }
        public int IsDetained { get; set; }


        clsLicensesInfo(int ApplicationID, string LicenseClass,string FullName,int LicenseID,string NationalNo,int Gendor
                                           ,DateTime IssueDate,DateTime ExpirationDate,int IssueReason,string Notes,bool IsActive,DateTime DateOfBirth,
                                            int DriverID,string ImagePath,int IsDetained)
        {
            this.ApplicationID = ApplicationID;
            this.LicenseClass = LicenseClass;
            this.FullName = FullName;
            this.LicenseID = LicenseID;
            this.NationalNo = NationalNo;
            this.Gendor = Gendor;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IssueReason = IssueReason;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.DateOfBirth = DateOfBirth;
            this.DriverID = DriverID;
            this.ImagePath = ImagePath;
            this.IsDetained = IsDetained;
        }
        clsLicensesInfo( string LicenseClass,string FullName,int LicenseID,string NationalNo,int Gendor
                                           ,DateTime IssueDate,DateTime ExpirationDate,int IssueReason,string Notes,bool IsActive,DateTime DateOfBirth,
                                            int DriverID,string ImagePath,int IsDetained)
        {
            this.LicenseClass = LicenseClass;
            this.FullName = FullName;
            this.LicenseID = LicenseID;
            this.NationalNo = NationalNo;
            this.Gendor = Gendor;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IssueReason = IssueReason;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.DateOfBirth = DateOfBirth;
            this.DriverID = DriverID;
            this.ImagePath = ImagePath;
            this.IsDetained = IsDetained;
        }
        public static clsLicensesInfo FindByApplicationID(int ApplicationID)
        {
            string LicenseClass = "";
            string FullName = "";
            int LicenseID = -1;
            string NationalNo = "";
            int Gendor = -1;
            DateTime IssueDate = new DateTime();
            DateTime ExpirationDate = new DateTime();
            int IssueReason = -1;
            string Notes = "";
            bool IsActive = false;
            DateTime DateOfBirth = new DateTime();
            int DriverID = -1;
            string ImagePath = "";
            int IsDetained = 0;
            if (clsLicensesDB.GetLicensesInfoByApplicationID(ApplicationID, ref LicenseClass, ref FullName, ref LicenseID, ref NationalNo, ref Gendor
                                           , ref IssueDate, ref ExpirationDate, ref IssueReason, ref Notes, ref IsActive, ref DateOfBirth,
                                            ref DriverID, ref ImagePath, ref IsDetained))
                return new clsLicensesInfo(ApplicationID, LicenseClass, FullName, LicenseID, NationalNo, Gendor
                                           , IssueDate, ExpirationDate, IssueReason, Notes, IsActive, DateOfBirth,
                                             DriverID, ImagePath, IsDetained);
            else
                return null;
        }
        public static clsLicensesInfo FindByLicenseID(int LicenseID)
        {
            string LicenseClass = "";
            string FullName = "";
            string NationalNo = "";
            int Gendor = -1;
            DateTime IssueDate = new DateTime();
            DateTime ExpirationDate = new DateTime();
            int IssueReason = -1;
            string Notes = "";
            bool IsActive = false;
            DateTime DateOfBirth = new DateTime();
            int DriverID = -1;
            string ImagePath = "";
            int IsDetained = 0;
            if (clsLicensesDB.GetLicensesByLicenseID(LicenseID, ref LicenseClass, ref FullName,  ref NationalNo, ref Gendor
                                           , ref IssueDate, ref ExpirationDate, ref IssueReason, ref Notes, ref IsActive, ref DateOfBirth,
                                            ref DriverID, ref ImagePath, ref IsDetained))
                return new clsLicensesInfo( LicenseClass, FullName, LicenseID, NationalNo, Gendor
                                           , IssueDate, ExpirationDate, IssueReason, Notes, IsActive, DateOfBirth,
                                             DriverID, ImagePath, IsDetained);
            else
                return null;
        }
    }
}
