using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsApplicationType
    {
        public int ApplicationTypeID {  get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        clsApplicationType(int applicationTypeID, string applicationTypeTitle, decimal applicationFees)
        {
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeTitle = applicationTypeTitle;
            this.ApplicationFees = applicationFees;
        }
        public bool UpdateApplicationType()
        {
            return clsApplicationTypesDB.UpdateApplicationType(this.ApplicationTypeID,this.ApplicationTypeTitle,this.ApplicationFees);
        }
        public static clsApplicationType Find(int Id)
        {
            string ApplicationType = "";
            decimal Applicationfees = 0;
            if (clsApplicationTypesDB.GetApplicationTypeById(Id,ref ApplicationType,ref Applicationfees))
                return new clsApplicationType(Id,ApplicationType,Applicationfees);
            else
                return null;
        }
        public static DataTable GetAllApplicationType()
        {
            return clsApplicationTypesDB.GetAllApplicationTypes();
        }
        public static int RowsNumber()
        {
            return clsApplicationTypesDB.GetApplicationTypesCount();
        }
        public static decimal GetFeesType(int ApplicationTypeID)
        {
            return clsApplicationTypesDB.GetFeesType(ApplicationTypeID);
        }

    }
}
