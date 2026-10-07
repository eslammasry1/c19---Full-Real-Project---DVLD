using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsInternationalLicenseInfo
    {
        public int InternationalLicenseID { get; set; }
        public string FullName { get; set; }
        public int LocalLicenseID { get; set; }
        public string NationalNo { get; set; }
        public int Gender { get; set; }
        public DateTime IssueDate { get; set; }
        public int ApplicationID { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DriverID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ImagePath { get; set; }


        public clsInternationalLicenseInfo(int InternationalLicenseID, string FullName, int LocalLicenseID, string NationalNo, int Gender, DateTime IssueDate,
                                            int ApplicationID, bool IsActive, DateTime DateOfBirth, int DriverID, DateTime ExpirationDate, string imagePath)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.FullName = FullName;
            this.LocalLicenseID = LocalLicenseID;
            this.NationalNo = NationalNo;
            this.Gender = Gender;
            this.IssueDate = IssueDate;
            this.ApplicationID = ApplicationID;
            this.IsActive = IsActive;
            this.DateOfBirth = DateOfBirth;
            this.DriverID = DriverID;
            this.ExpirationDate = ExpirationDate;
            this.ImagePath = imagePath;
        }


        public static clsInternationalLicenseInfo Find(int InternationalLicenseID)
        {
            string FullName = "";
            int LocalLicenseID = -1;
            string NationalNo = "";
            int Gender = -1;
            DateTime IssueDate = DateTime.Now;
            int ApplicationID = -1;
            bool IsActive = false;
            DateTime DateOfBirth = DateTime.Now;
            int DriverID = -1;
            DateTime ExpirationDate = DateTime.Now;
            string ImagePath = "";

            bool IsFound = clsInternationalDB.GetInternationalLicenseInfoById(InternationalLicenseID, ref FullName, ref LocalLicenseID, ref NationalNo, ref Gender,
                                                                               ref IssueDate, ref ApplicationID, ref IsActive, ref DateOfBirth, ref DriverID,
                                                                               ref ExpirationDate,ref ImagePath);

            if (IsFound)
            {
                return new clsInternationalLicenseInfo(InternationalLicenseID, FullName, LocalLicenseID, NationalNo, Gender, IssueDate, ApplicationID, IsActive,
                                                       DateOfBirth, DriverID, ExpirationDate, ImagePath);
            }

            return null;
        }
    }
}
