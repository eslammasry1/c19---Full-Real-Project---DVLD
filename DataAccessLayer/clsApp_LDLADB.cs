using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsApp_LDLADB
    {
        public static int AddNewApplication(int PersonID, int UserID, int LicenseClassID)
        {
            int ID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"
        INSERT INTO Applications
        (
            ApplicantPersonID,
            ApplicationDate,
            ApplicationTypeID,
            ApplicationStatus,
            LastStatusDate,
            PaidFees,
            CreatedByUserID
        )
        VALUES
        (
            @PersonID,
            GETDATE(),
            1,
            1,
            GETDATE(),
            15,
            @UserID
        );

        INSERT INTO LocalDrivingLicenseApplications
        (
            ApplicationID,
            LicenseClassID
        )
        VALUES
        (
            SCOPE_IDENTITY(),
            @LicenseClassID
        );

        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ID = insertedID;
                }
            }
            catch (Exception ex)
            {
                ID = -1;
            }
            finally
            {
                connection.Close();
            }

            return ID;
        }
        public static bool UpdateApplication(int ID,int LicenseClassID)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"Update LocalDrivingLicenseApplications set
                             LicenseClassID = @LicenseClassID
                             where LocalDrivingLicenseApplicationID = @ID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        public static bool CompleteApplication(int ApplicationID)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"update Applications set 
                             ApplicationStatus = 3
                             where ApplicationID = @ApplicationID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        public static bool DeleteApplication(int ApplicationID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"DELETE FROM LocalDrivingLicenseApplications
                             WHERE ApplicationID = @ApplicationID;
                             DELETE FROM Applications
                             WHERE ApplicationID = @ApplicationID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }

        public static bool GetApplicationByApID(int ApplicationID, ref int PersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref int ApplicationStatus,ref DateTime LastStatusDate,ref decimal Fees,ref int UserId)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select * from Applications 
                             where ApplicationID = @ApplicationID";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    PersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    Fees = Convert.ToDecimal(reader["PaidFees"]);
                    UserId = Convert.ToInt32(reader["CreatedByUserID"]);
                }
                else
                {
                    IsFound = false;
                }
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
        public static bool GetLDLAByLDLAid(int LDLAid,ref int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select * from LocalDrivingLicenseApplications 
                            where LocalDrivingLicenseApplicationID = @LDLAid";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAid", LDLAid);
            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
                else
                {
                    IsFound = false;
                }
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
        public static bool GetAllApp_LDLA_Info(int DLid, ref int AppID, ref int AppStatus,ref int fees, ref string AppTitle,
                                               ref string FullName,ref DateTime AppDate,ref DateTime StatusDate,
                                               ref string UserName,ref string ClassName ,ref int PersonID ,ref int PassTests)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select A.ApplicationID,A.ApplicationStatus,A.PaidFees,AT.ApplicationTypeTitle,
                                P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                         	   A.ApplicationDate,A.LastStatusDate,U.UserName,LC.ClassName,A.ApplicantPersonID,
                         	           (
                                     SELECT COUNT(*)
                                     FROM TestAppointments TA
                                     INNER JOIN Tests T 
                                         ON TA.TestAppointmentID = T.TestAppointmentID
                                     WHERE TA.LocalDrivingLicenseApplicationID = LDLA.LocalDrivingLicenseApplicationID
                                       AND T.TestResult = 1
                                 ) AS PassedTests
                         from Applications A 
                         join ApplicationTypes AT on at.ApplicationTypeID = A.ApplicationTypeID
                         join People P on P.PersonID = A.ApplicantPersonID
                         join Users U on U.UserID = A.CreatedByUserID
                         join LocalDrivingLicenseApplications LDLA on LDLA.ApplicationID = A.ApplicationID
                         join LicenseClasses LC on LC.LicenseClassID = LDLA.LicenseClassID
                         where LDLA.LocalDrivingLicenseApplicationID = @DLid";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@DLid", DLid);
            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    AppID = Convert.ToInt32(reader["ApplicationID"]);
                    AppStatus = Convert.ToInt32(reader["ApplicationStatus"]);
                    fees = Convert.ToInt32(reader["PaidFees"]);
                    AppTitle = (string)reader["ApplicationTypeTitle"];
                    FullName = (string)reader["FullName"];
                    AppDate = (DateTime)reader["ApplicationDate"];
                    StatusDate = (DateTime)reader["LastStatusDate"];
                    UserName = (string)reader["UserName"];
                    ClassName = (string)reader["ClassName"];
                    PersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    PassTests = Convert.ToInt32(reader["PassedTests"]);

                }
                else
                {
                    IsFound = false;
                }
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

        public static DataTable GetAllDLApplication()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"	SELECT 
                                LDLA.LocalDrivingLicenseApplicationID,
                                LC.ClassName,NationalNo,p.FirstName+' ' +p.SecondName + ' ' +p.ThirdName + ' ' +p.LastName as FullName , a.ApplicationDate as ApplicationDate ,
                            	(select count (*) from TestAppointments TA inner join tests T on TA.TestAppointmentID=T.TestAppointmentID Where Ta.LocalDrivingLicenseApplicationID
                            	=LDLA.LocalDrivingLicenseApplicationID and t.TestResult = 1 )as PassedTests, 
                            	case A.ApplicationStatus
                            	     when 1 then 'new'
                            	     when 2 then 'Cancelled'
                            	     when 3 then 'Completed'
                                End as status
                            FROM LocalDrivingLicenseApplications LDLA
                            
                            INNER JOIN Applications A
                                ON LDLA.ApplicationID = A.ApplicationID
                            
                            INNER JOIN People P
                                ON A.ApplicantPersonID = P.PersonID
                            
                            INNER JOIN LicenseClasses LC
                                ON LDLA.LicenseClassID = LC.LicenseClassID";
            SqlCommand command = new SqlCommand(quiry, connection);
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
        public static bool ExistApplicationStatus(int PersonId, int LicenseClassID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select ldla.LicenseClassID,A.ApplicantPersonID,A.ApplicationStatus from LocalDrivingLicenseApplications ldla
                             join Applications A on A.ApplicationID = ldla.ApplicationID
                             where ldla.LicenseClassID = @LicenseClassID and A.ApplicantPersonID = @PersonId and A.ApplicationStatus = 1;";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@PersonId", PersonId);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        public static bool ApplicationIsCompleted(int PersonId, int LicenseClassID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select ldla.LicenseClassID,A.ApplicantPersonID,A.ApplicationStatus from LocalDrivingLicenseApplications ldla
                             join Applications A on A.ApplicationID = ldla.ApplicationID
                             where ldla.LicenseClassID = @LicenseClassID and A.ApplicantPersonID = @PersonId and A.ApplicationStatus = 3;";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@PersonId", PersonId);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        public static DataTable GetClassName()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select ClassName from LicenseClasses";
            SqlCommand command = new SqlCommand(quiry, connection);
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
        public static bool CancelleApp(int AppID)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"Update Applications set
                             ApplicationStatus = 2,
                             LastStatusDate = GETDATE()
                             where ApplicationID = @AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
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

        public static bool AppIsCancelled(int AppId)
        {
            bool IsCancelled = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found = 1 from Applications where ApplicationID = @AppId and ApplicationStatus = 2";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@AppId", AppId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsCancelled = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                IsCancelled = false;
            }
            finally
            {
                connection.Close();
            }
            return IsCancelled;
        }
        public static bool AppIsCompleted(int AppId)
        {
            bool IsCompleted = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found = 1 from Applications where ApplicationID = AppId and ApplicationStatus = 3";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@AppId", AppId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsCompleted = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                IsCompleted = false;
            }
            finally
            {
                connection.Close();
            }
            return IsCompleted;
        }
        public static bool HaveLicense(int AppId)
        {
            bool HaveLicense = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select Found = 1 from Licenses where ApplicationID = @AppId";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@AppId", AppId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                HaveLicense = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                HaveLicense = false;
            }
            finally
            {
                connection.Close();
            }
            return HaveLicense;
        }

        public static DataTable GetAllAppFilterByLdlaID(int LdlaID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT LDLA.LocalDrivingLicenseApplicationID,
                                LC.ClassName,NationalNo,p.FirstName+' ' +p.SecondName + ' ' +p.ThirdName + ' ' +p.LastName as FullName , a.ApplicationDate as ApplicationDate ,
                            	(select count (*) from TestAppointments TA inner join tests T on TA.TestAppointmentID=T.TestAppointmentID Where Ta.LocalDrivingLicenseApplicationID
                            	=LDLA.LocalDrivingLicenseApplicationID and t.TestResult = 1 )as PassedTests, 
                            	case A.ApplicationStatus
                            	     when 1 then 'new'
                            	     when 2 then 'Cancelled'
                            	     when 3 then 'Completed'
                                End as status
                            FROM LocalDrivingLicenseApplications LDLA
                            INNER JOIN Applications A
                                ON LDLA.ApplicationID = A.ApplicationID
                            INNER JOIN People P
                                ON A.ApplicantPersonID = P.PersonID
                            INNER JOIN LicenseClasses LC
                                ON LDLA.LicenseClassID = LC.LicenseClassID
                            	where ldla.LocalDrivingLicenseApplicationID = @LdlaID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@LdlaID", LdlaID );
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

        public static DataTable GetAllAppFilterByNationalNo(string NationalNo)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT LDLA.LocalDrivingLicenseApplicationID,
                                LC.ClassName,NationalNo,p.FirstName+' ' +p.SecondName + ' ' +p.ThirdName + ' ' +p.LastName as FullName , a.ApplicationDate as ApplicationDate ,
                            	(select count (*) from TestAppointments TA inner join tests T on TA.TestAppointmentID=T.TestAppointmentID Where Ta.LocalDrivingLicenseApplicationID
                            	=LDLA.LocalDrivingLicenseApplicationID and t.TestResult = 1 )as PassedTests, 
                            	case A.ApplicationStatus
                            	     when 1 then 'new'
                            	     when 2 then 'Cancelled'
                            	     when 3 then 'Completed'
                                End as status
                            FROM LocalDrivingLicenseApplications LDLA
                            INNER JOIN Applications A
                                ON LDLA.ApplicationID = A.ApplicationID
                            INNER JOIN People P
                                ON A.ApplicantPersonID = P.PersonID
                            INNER JOIN LicenseClasses LC
                                ON LDLA.LicenseClassID = LC.LicenseClassID
                            	where p.NationalNo like  @NationalNo";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo + "%");
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
        public static DataTable GetAllAppFilterByFullName(string FullName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT LDLA.LocalDrivingLicenseApplicationID,
                                LC.ClassName,NationalNo,p.FirstName+' ' +p.SecondName + ' ' +p.ThirdName + ' ' +p.LastName as FullName , a.ApplicationDate as ApplicationDate ,
                            	(select count (*) from TestAppointments TA inner join tests T on TA.TestAppointmentID=T.TestAppointmentID Where Ta.LocalDrivingLicenseApplicationID
                            	=LDLA.LocalDrivingLicenseApplicationID and t.TestResult = 1 )as PassedTests, 
                            	case A.ApplicationStatus
                            	     when 1 then 'new'
                            	     when 2 then 'Cancelled'
                            	     when 3 then 'Completed'
                                End as status
                            FROM LocalDrivingLicenseApplications LDLA
                            INNER JOIN Applications A
                                ON LDLA.ApplicationID = A.ApplicationID
                            INNER JOIN People P
                                ON A.ApplicantPersonID = P.PersonID
                            INNER JOIN LicenseClasses LC
                                ON LDLA.LicenseClassID = LC.LicenseClassID
                            	where p.FirstName+' ' +p.SecondName + ' ' +p.ThirdName + ' ' +p.LastName  like @FullName";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@FullName", FullName + "%");
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;


        }
        public static DataTable GetAllAppFilterByApplicationStatus(int ApplicationStatus)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"	SELECT LDLA.LocalDrivingLicenseApplicationID,
                                LC.ClassName,NationalNo,p.FirstName+' ' +p.SecondName + ' ' +p.ThirdName + ' ' +p.LastName as FullName , a.ApplicationDate as ApplicationDate ,
                            	(select count (*) from TestAppointments TA inner join tests T on TA.TestAppointmentID=T.TestAppointmentID Where Ta.LocalDrivingLicenseApplicationID
                            	=LDLA.LocalDrivingLicenseApplicationID and t.TestResult = 1 )as PassedTests, 
                            	case A.ApplicationStatus
                            	     when 1 then 'new'
                            	     when 2 then 'Cancelled'
                            	     when 3 then 'Completed'
                                End as status
                            FROM LocalDrivingLicenseApplications LDLA
                            
                            INNER JOIN Applications A
                                ON LDLA.ApplicationID = A.ApplicationID
                            
                            INNER JOIN People P
                                ON A.ApplicantPersonID = P.PersonID
                            
                            INNER JOIN LicenseClasses LC
                                ON LDLA.LicenseClassID = LC.LicenseClassID
                            	where A.ApplicationStatus  = @ApplicationStatus";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
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
        public static int GetApplicationStatus(int AppId)
        {
            int Status = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select ApplicationStatus from Applications 
                               where ApplicationID = @AppId";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@AppId", AppId);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    Status = count;
                }
            }
            catch (Exception ex)
            {
                Status = 0;
            }
            finally
            {
                connection.Close();
            }

            return Status;
        }
        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"INSERT INTO LocalDrivingLicenseApplications
                            (
                                ApplicationID,
                                LicenseClassID
                            )
                            VALUES
                            (
                                @ApplicationID,
                                @LicenseClassID
                            );
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    LocalDrivingLicenseApplicationID = ID;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenseApplicationID;
        }

    }
}
