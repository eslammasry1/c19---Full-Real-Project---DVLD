using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsAppAndDLInfo
    {
        public int DLid {  get; set; }
        public int AppID {  get; set; }
        public int AppStatus {  get; set; }
        public int fees {  get; set; }
        public string AppTitle {  get; set; }
        public string FullName {  get; set; }
        public DateTime AppDate {  get; set; }
        public DateTime StatusDate {  get; set; }
        public string UserName {  get; set; }
        public string ClassName {  get; set; }
        public int ApplicantPersonID { get; set; }
        public int PassTests { get; set; }

        clsAppAndDLInfo(int dLid, int appID, int appStatus, int fees, string appTitle, 
                        string fullName, DateTime appDate, DateTime statusDate, string userName
                        , string ClassName,int PersonID, int passTests)
        {
            this.DLid = dLid;
            this.AppID = appID;
            this.AppStatus = appStatus;
            this.fees = fees;
            this.AppTitle = appTitle;
            this.FullName = fullName;
            this.AppDate = appDate;
            this.StatusDate = statusDate;
            this.UserName = userName;
            this.ClassName = ClassName;
            this.ApplicantPersonID = PersonID;
            this.PassTests = passTests;
        }
        public static clsAppAndDLInfo Find(int dLid)
        {
            int appID = -1;
            int appStatus = -1;
            int fees = 0;
            string appTitle = "";
            string fullName = "";
            DateTime appDate = new DateTime();
            DateTime statusDate = new DateTime(); 
            string userName = "";
            int PersonID = -1;
            string className = "";
            int passTests = -1;
            if (clsApp_LDLADB.GetAllApp_LDLA_Info(dLid,ref appID,ref appStatus,ref fees,ref appTitle,
                                                  ref fullName,ref appDate,ref statusDate,ref userName
                                                  ,ref className,ref PersonID, ref passTests))
            {

                return new clsAppAndDLInfo(dLid, appID, appStatus, fees, appTitle, fullName, appDate, 
                    statusDate, userName, className, PersonID, passTests);
            }
            else 
                return null;
        }
    }
}
