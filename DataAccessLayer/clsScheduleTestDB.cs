using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsScheduleTestDB
    {
        public static DataTable GetAppointmentInfo(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string Query = @"select TA.TestAppointmentID,TA.AppointmentDate,TA.PaidFees,TA.IsLocked from TestAppointments TA
                             Where TA.TestTypeID = @TestTypeID and TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        //public static DataTable GetAppointmentScheduleInfo( int TestAppointmentID)
        //{
        //    DataTable dt = new DataTable();
        //    SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
        //    string Query = @"select TA1.LocalDrivingLicenseApplicationID,LC.ClassName,
        //                     P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
        //               	   (select COUNT(*) from TestAppointments TA2 where TA2.TestTypeID = 1 and TA2.LocalDrivingLicenseApplicationID = TA1.LocalDrivingLicenseApplicationID and TA2.IsLocked = 1) as trial,
        //               	   TA1.AppointmentDate,TA1.PaidFees
        //               	   from TestAppointments TA1
        //               	   join LocalDrivingLicenseApplications LDLA on LDLA.LocalDrivingLicenseApplicationID = TA1.LocalDrivingLicenseApplicationID
        //               	   join LicenseClasses LC on LC.LicenseClassID = LDLA.LicenseClassID
        //               	   join Applications A on A.ApplicationID = LDLA.ApplicationID
        //               	   join People P on P.PersonID = A.ApplicantPersonID
        //                where TA1.TestTypeID = 1 and TA1.TestAppointmentID =@TestAppointmentID";
        //    SqlCommand command = new SqlCommand(Query, connection);
        //    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        if (reader.HasRows)
        //            dt.Load(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return dt;
        //}

        public static bool GeTTestIsPassed(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int PassedTests = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"SELECT COUNT(*) 
                             FROM TestAppointments TA
					         join Tests T on T.TestAppointmentID = TA.TestAppointmentID
                             WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                             AND TA.TestTypeID = @TestTypeID
                             AND TA.IsLocked = 1
					         and T.TestResult = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseApplicationID);

            command.Parameters.AddWithValue("@TestTypeID",TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    PassedTests = count;
                }
            }
            catch (Exception ex)
            {
                PassedTests = 0;
            }
            finally
            {
                connection.Close();
            }

            return PassedTests>0;
        }
        public static int GeTContAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int PassedTests = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select  count(*) from TestAppointments where TestTypeID =@TestTypeID and IsLocked =1 
                            and LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseApplicationID);

            command.Parameters.AddWithValue("@TestTypeID",TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    PassedTests = count;
                }
            }
            catch (Exception ex)
            {
                PassedTests = 0;
            }
            finally
            {
                connection.Close();
            }

            return PassedTests;
        }
        public static decimal GetFeesTest(int TestTypeID)
        {
            decimal TestFees = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select TestTypeFees from TestTypes where TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestTypeID",TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && decimal.TryParse(result.ToString(), out decimal count))
                {
                    TestFees = count;
                }
            }
            catch (Exception ex)
            {
                TestFees = 0;
            }
            finally
            {
                connection.Close();
            }

            return TestFees;
        }
        public static bool GetIsLoked(int TestAppointmentID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select IsLocked from TestAppointments
                              where TestAppointmentID =@TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && bool.TryParse(Result.ToString(), out bool IsLocked))
                {
                    result = IsLocked;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public static int GetCountTestPassed(int LocalDrivingLicenseApplicationID)
        {
            int FinshTests = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select count (*) from TestAppointments TA
                             join tests T on TA.TestAppointmentID=T.TestAppointmentID 
                             Where Ta.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and t.TestResult = 1";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    FinshTests = count;
                }
            }
            catch (Exception ex)
            {
                FinshTests = 0;
            }
            finally
            {
                connection.Close();
            }

            return FinshTests;
        }

        public static bool ISExistAppointments(int TestTypeID,int LocalDrivingLicenseApplicationID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select Found = 1 from TestAppointments where TestTypeID = @TestTypeID
                             and LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and IsLocked = 0";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsExist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                IsExist = false;
            }
            finally
            {
                connection.Close();
            }
            return IsExist;
        }
        public static bool IsPassThisTest(int TestTypeID,int LocalDrivingLicenseApplicationID)
        {
            bool IsPass = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select Found = 1 from TestAppointments TA join tests T on TA.TestAppointmentID = T.TestAppointmentID
                              where TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                              and T.TestResult =1 and TA.TestTypeID =@TestTypeID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsPass = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                IsPass = false;
            }
            finally
            {
                connection.Close();
            }
            return IsPass;
        }
        public static bool ISExistFinshedAppointments(int TestTypeID,int LocalDrivingLicenseApplicationID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select Found = 1 from TestAppointments where TestTypeID = @TestTypeID
                             and LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and IsLocked = 1";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsExist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                IsExist = false;
            }
            finally
            {
                connection.Close();
            }
            return IsExist;
        }
        //public static bool ISFinshTest(int TestTypeID, int LocalDrivingLicenseApplicationID)
        //{
        //    bool IsFinsh = false;
        //    SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
        //    string quiry = @"select Found = 1 from TestAppointments 
        //                    join Tests on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
        //                    where TestAppointments.TestTypeID = @TestTypeID and 
        //                    TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestAppointments.IsLocked = 1
        //                    and Tests.TestResult =1";
        //    SqlCommand command = new SqlCommand(quiry, connection);
        //    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
        //    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        IsFinsh = reader.HasRows;
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        IsFinsh = false;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return IsFinsh;
        //}

        public static int AddAppointment(int TestTypeID,int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
                                                                         decimal PaidFees,int CreatedByUserID)
        {
            int AppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string Query = @"insert into TestAppointments
                            (TestTypeID,
                             LocalDrivingLicenseApplicationID,
                             AppointmentDate,
                             PaidFees,
                             CreatedByUserID,
                             IsLocked)
                            Values
                            (
                            @TestTypeID,
                            @LocalDrivingLicenseApplicationID,
                            @AppointmentDate,
                            @PaidFees,
                            @CreatedByUserID,
                            0)
                             Select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    AppointmentID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return AppointmentID;
        }

        public static bool UpdateAppointment(int TestAppointmentID, DateTime AppointmentDate)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string Query = @" Update TestAppointments set 
                              AppointmentDate = @AppointmentDate
                              where TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = 0;
            }
            finally
            {
                connection.Close(); 
            }
            return result > 0;

        }

        public static bool GetAppointmentByID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate,
                                                                        ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLooked)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @" select * from TestAppointments where TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsLooked = Convert.ToBoolean(reader["IsLocked"]);

                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;

        }
           
    }
}
