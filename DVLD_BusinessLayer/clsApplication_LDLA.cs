using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsApplication_LDLA
    {
        public enum enMode { AddNewApplication = 0, UpdateApplication = 1 }
        enMode Mode = enMode.AddNewApplication;

        public class clsLDLA
        {
            public int LDLAid {  get; set; }
            public int AppId {  get; set; }
            public int LicenseClassID {  get; set; }
            
            clsLDLA(int LDLAid, int AppId, int LicenseClassID)
            {
                this.LDLAid = LDLAid;
                this.AppId = AppId;
                this.LicenseClassID = LicenseClassID;
            }
            public static clsLDLA Find(int LDLAid)
            {
                int appid = -1;
                int licenseclassid = -1;
                if (clsApp_LDLADB.GetLDLAByLDLAid(LDLAid,ref appid,ref licenseclassid))
                {
                    return new clsLDLA(LDLAid, appid, licenseclassid);
                }
                else
                {
                    return null;
                }
            }
        }
        public class clsApplication
        {
            public int AppId { get; set; }
            public int PersonID { get; set; }
            public DateTime ApplicationDate { get; set; }
            public int ApplicationTypeID { get; set; }
            public int ApplicationStatus { get; set; }
            public DateTime LastStatusDate { get; set; }
            public decimal PaidFees { get; set; }
            public int CreatedByUserID { get; set; }

            clsApplication(int appid,int personid,DateTime applicationDate,int applicationTypeID,int applicationStatus,DateTime lastStatusDate,decimal paidFees,int createdByUserID)
            {
                this.AppId = appid;
                this.PersonID = personid;
                this.ApplicationDate = applicationDate;
                this.ApplicationTypeID = applicationTypeID;
                this.ApplicationStatus = applicationStatus;
                this.LastStatusDate = lastStatusDate;
                this.PaidFees = paidFees;
                this.CreatedByUserID = createdByUserID;
            }
            public static clsApplication Find(int AppId)
            {
                int personid = -1;
                DateTime applicationDate = new DateTime();
                int applicationTypeID = -1;
                int applicationStatus = -1;
                DateTime lastStatusDate = new DateTime();
                decimal paidFees = 0;
                int createdByUserID = -1;
                if (clsApp_LDLADB.GetApplicationByApID(AppId,ref personid,ref applicationDate,ref applicationTypeID,ref applicationStatus,ref lastStatusDate,ref paidFees,ref createdByUserID))
                {
                    return new clsApplication(AppId,personid,applicationDate,applicationTypeID,applicationStatus,lastStatusDate,paidFees,createdByUserID);
                }
                else
                {
                    return null;
                }
            }
        }
        public int LDLAid { get; set; }
        public int LicenseClassID { get; set; }
        public int AppId { get; set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsApplication_LDLA(int LDLAid, int AppId, int LicenseClassID, int personid, DateTime applicationDate, 
                            int applicationTypeID, int applicationStatus, DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
            this.LDLAid = LDLAid;
            this.AppId = AppId;
            this.LicenseClassID = LicenseClassID;
            this.PersonID = personid;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            Mode = enMode.UpdateApplication;
        }
        public clsApplication_LDLA()
        {
            this.LDLAid = -1;
            this.AppId = -1;
            this.LicenseClassID = -1;
            this.PersonID = -1;
            this.ApplicationDate = new DateTime();
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = -1;
            this.LastStatusDate = new DateTime();
            this.PaidFees = 15;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNewApplication;
        }
        private bool _AddNewApplication()
        {
            this.LDLAid = clsApp_LDLADB.AddNewApplication(this.PersonID, this.CreatedByUserID, this.LicenseClassID);
            return this.LDLAid != -1;
        }
        private bool _UpdateApplication()
        {
            return clsApp_LDLADB.UpdateApplication(this.LDLAid, this.LicenseClassID);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNewApplication:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.UpdateApplication;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateApplication:
                    return _UpdateApplication();
            }
            return false;
        }

        public static clsApplication_LDLA Find(int LDLA)
        {
            clsLDLA ldla = clsLDLA.Find(LDLA);

            if (ldla == null)
                return null;

            clsApplication application = clsApplication.Find(ldla.AppId);

            if (application == null)
                return null;

            return new clsApplication_LDLA(
                LDLA,
                ldla.AppId,
                ldla.LicenseClassID,
                application.PersonID,
                application.ApplicationDate,
                application.ApplicationTypeID,
                application.ApplicationStatus,
                application.LastStatusDate,
                application.PaidFees,
                application.CreatedByUserID
            );
        }
        
        public static DataTable GetLinceseClass()
        {
            return clsApp_LDLADB.GetClassName();
        }
        public static DataTable GetAllDLApplication()
        {
            return clsApp_LDLADB.GetAllDLApplication();
        }
        public static bool ExistApplicationStatus(int PersonID,int LicenseClassID)
        {
            return clsApp_LDLADB.ExistApplicationStatus(PersonID, LicenseClassID);
        }
        public static bool ApplicationIsCompleted(int PersonID,int LicenseClassID)
        {
            return clsApp_LDLADB.ApplicationIsCompleted(PersonID, LicenseClassID);
        }

        public static bool DeleteLdlaApp(int LdlaId)
        {
            clsApplication_LDLA cls = clsApplication_LDLA.Find(LdlaId);
            return clsApp_LDLADB.DeleteApplication(cls.AppId);
        }
        public static bool CancelleApp(int LdlaId)
        {
            clsApplication_LDLA cls = clsApplication_LDLA.Find(LdlaId);
            return clsApp_LDLADB.CancelleApp(cls.AppId);
        }        
        public static bool HaveLicense(int LdlaId)
        {
            clsApplication_LDLA cls = clsApplication_LDLA.Find(LdlaId);
            return clsApp_LDLADB.HaveLicense(cls.AppId);
        }        
        public static bool AppIsCancelled(int LdlaId)
        {
            clsApplication_LDLA cls = clsApplication_LDLA.Find(LdlaId);
            return clsApp_LDLADB.AppIsCancelled(cls.AppId);
        }
        public static int GetApplicationStatus(int LdlaId)
        {
            clsApplication_LDLA cls = clsApplication_LDLA.Find(LdlaId);
            return clsApp_LDLADB.GetApplicationStatus(cls.AppId);
        }

        public static bool AppIsCompleted(int LdlaId)
        {
            clsApplication_LDLA cls = clsApplication_LDLA.Find(LdlaId);
            return clsApp_LDLADB.AppIsCompleted(cls.AppId);
        }
        public static DataTable FilterByLdlaID(int LdlaID)
        {
            return clsApp_LDLADB.GetAllAppFilterByLdlaID(LdlaID);
        }
        public static DataTable FilterByNationalNo(string NationalNo)
        {
            return clsApp_LDLADB.GetAllAppFilterByNationalNo(NationalNo);
        }
        public static DataTable FilterByFullName(string FullName)
        {
            return clsApp_LDLADB.GetAllAppFilterByFullName(FullName);
        }
        public static DataTable FilterByApplicationStatus(int ApplicationStatus)
        {
            return clsApp_LDLADB.GetAllAppFilterByApplicationStatus(ApplicationStatus);
        }

        public static bool CompleteApplication(int ApplicationID)
        {
            return clsApp_LDLADB.CompleteApplication(ApplicationID);
        }
    }
}
