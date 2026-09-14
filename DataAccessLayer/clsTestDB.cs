using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestDB
    {
        public static int TakingTest(int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"insert into tests
                            (TestAppointmentID,
                             TestResult,
                             Notes,
                             CreatedByUserID)
                            values
                            (@TestAppointmentID,
                            @TestResult,
                            @Notes,
                            @CreatedByUserID)
                            Select SCOPE_IDENTITY();
                            update TestAppointments set
                            IsLocked = 1
                            where TestAppointmentID = @TestAppointmentID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes != null)
            {
                command.Parameters.AddWithValue("@Notes", Notes);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);
            }
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }

            }
            catch (Exception ex)
            {
                TestID = -1;
            }
            finally
            {
                connection.Close();
            }
            return TestID;
        }
    }
}
