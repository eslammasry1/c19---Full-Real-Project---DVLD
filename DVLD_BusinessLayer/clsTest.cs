using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsTest
    {
        public int TestID {  get; set; }
        public int TestAppointmentID {  get; set; }
        public bool TestResult {  get; set; }
        public string Notes {  get; set; }
        public int CreatedByUserID {  get; set; }

        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;
        }
        public bool TakingTest()
        {
            this.TestID = clsTestDB.TakingTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);
            return this.TestID != -1;
        }
    }
    
}
