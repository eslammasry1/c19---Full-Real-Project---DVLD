using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsTestType
    {
        public int ID {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Fees { get; set; }
        clsTestType(int id,string title,string description,decimal fees)
        {
            this.ID = id;
            this.Title = title;
            this.Description = description;
            this.Fees = fees;
        }
        public bool UpdateTestType()
        {
           return clsTestTypesDB.EditTestType(this.ID, this.Title,this.Description, this.Fees);
        }
        public static DataTable GetAllTestType()
        {
            return clsTestTypesDB.GetAllTestType();
        }
        public static clsTestType Find(int id)
        {
            string Title = "";
            string Description = "";
            decimal Fees = 0;
            if (clsTestTypesDB.GetAllTestTypeById(id,ref Title,ref Description,ref Fees))
            {
                return new clsTestType(id,Title,Description,Fees);
            }
            else
            {
                return null;
            }
        }
        public static int GetRowsNumber()
        {
            return clsTestTypesDB.GetNumberRowsOfTestType();
        }
    }
}
