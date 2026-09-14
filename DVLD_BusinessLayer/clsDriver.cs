using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DVLD_BusinessLayer
{
    public class clsDriver
    {         
        public int PersonID  { get; set; }
        public int DriverID {  get; set; }
        public int CreatedByUserID {  get; set; }
        DateTime CreatedDate { get; set; }

        public clsDriver(int PersonID, int DriverID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.PersonID = PersonID;
            this.DriverID = DriverID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
        }

        public clsDriver()
        {
            this.PersonID = -1;
            this.DriverID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = new DateTime();
        }
        public static clsDriver FindByPersonID(int PersonID)
        {
            int personID = -1;
            int driverID = -1;
            int createdByUserID = -1;
            DateTime createdDate = new DateTime();
            if (clsDriverDB.GetDriverByPersonID(PersonID, ref driverID, ref createdByUserID, ref createdDate))
                return new clsDriver(PersonID, driverID, createdByUserID, createdDate);
            else
                return null;
        }
        public static clsDriver FindByDriverID(int driverID)
        {
            int personID = -1;
            int createdByUserID = -1;
            DateTime createdDate = new DateTime();
            if (clsDriverDB.GetDriverByDriverID(driverID, ref personID, ref createdByUserID, ref createdDate))
                return new clsDriver(personID, driverID, createdByUserID, createdDate);
            else
                return null;
        }
        public bool AddNewDriver()
        {
            this.DriverID = clsDriverDB.AddNewDriver(this.PersonID, this.CreatedByUserID);
            return this.DriverID != -1;
        }
        public static DataTable GetDrivers()
        {
            return clsDriverDB.GetDrivers();
        }
        public static DataTable GetDriversFilterByDriverID(int DriverID)
        {
            return clsDriverDB.GetDriversFilterByDriverID(DriverID);
        }
        public static DataTable GetDriversFilterByPersonID(int PersonID)
        {
            return clsDriverDB.GetDriversFilterByPersonID(PersonID);
        }
        public static DataTable GetAllUsersFilterByFullName(string FullName)
        {
            return clsDriverDB.GetAllUsersFilterByFullName(FullName);
        }
        public static DataTable GetAllDriversFilterByNationalNo(string NationalNo)
        {
            return clsDriverDB.GetAllDriversFilterByNationalNo(NationalNo);
        }

    }
}
