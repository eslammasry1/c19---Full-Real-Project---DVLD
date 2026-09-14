using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsGetCountry
    {
        int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsGetCountry(int ID, string CountryName)
        {
            this.CountryID = ID;
            this.CountryName = CountryName;
            
        }

        public static clsGetCountry FindCountry(int ID)
        {
            string CountryName = "";
            if (clsCountry.GetCountryName(ID,ref CountryName))
            {
                return new clsGetCountry(ID,CountryName);
            }
            else
            {
                return null;
            }
        }
        public static DataTable CountryList()
        {
            return clsCountry.GetCountryList();
        }
    }
}
