using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsLdla
    {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }


        public clsLdla()
        {
            LocalDrivingLicenseApplicationID = -1;
            ApplicationID = -1;
            LicenseClassID = -1;
        }


        private clsLdla(
            int LocalDrivingLicenseApplicationID,
            int ApplicationID,
            int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID =
                LocalDrivingLicenseApplicationID;

            this.ApplicationID = ApplicationID;

            this.LicenseClassID = LicenseClassID;
        }





        public bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID =
                clsApp_LDLADB
                .AddNewLocalDrivingLicenseApplication(
                    this.ApplicationID,
                    this.LicenseClassID);

            return this.LocalDrivingLicenseApplicationID != -1;
        }


    }
}
