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
        public clsTest(int TestID, int TestAppointmentID, bool TestResult,
                       string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
        }
        public static clsTest FindByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsTestDB.GetTestInfoByTestAppointmentID(
                TestAppointmentID,
                ref TestID,
                ref TestResult,
                ref Notes,
                ref CreatedByUserID))
            {
                return new clsTest(
                    TestID,
                    TestAppointmentID,
                    TestResult,
                    Notes,
                    CreatedByUserID);
            }

            return null;
        }
    }
    
}
