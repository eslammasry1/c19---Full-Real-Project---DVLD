using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsInternationalDB
    {
        public static int AddNewInternationalLicense(
             int ApplicationID,
             int DriverID,
             int IssuedUsingLocalLicenseID,
             DateTime IssueDate,
             DateTime ExpirationDate,
             bool IsActive,
             int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"INSERT INTO InternationalLicenses
                    (
                        ApplicationID,
                        DriverID,
                        IssuedUsingLocalLicenseID,
                        IssueDate,
                        ExpirationDate,
                        IsActive,
                        CreatedByUserID
                    )
                    VALUES
                    (
                        @ApplicationID,
                        @DriverID,
                        @IssuedUsingLocalLicenseID,
                        @IssueDate,
                        @ExpirationDate,
                        @IsActive,
                        @CreatedByUserID
                    );

                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID",
                                             IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
            }
            catch (Exception ex)
            {
                InternationalLicenseID = -1;
            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenseID;
        }

        public static bool HaveActiveInternationalLicenses(int DriverID)
        {
            int Have = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select * from InternationalLicenses where DriverID = @DriverID and IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    Have = count;
                }
            }
            catch (Exception ex)
            {
                Have = 0;
            }
            finally
            {
                connection.Close();
            }

            return Have > 0;
        }
        public static DataTable GetInternationalLicensesOfPerson(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select IL.InternationalLicenseID , IL.ApplicationID,IL.IssuedUsingLocalLicenseID as LocalLicenseID ,IL.IssueDate,IL.ExpirationDate,IL.IsActive from InternationalLicenses IL
                             join Drivers D on D.DriverID = IL.DriverID
                             where D.PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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
        public static DataTable GetInternationalInfo()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select InternationalLicenseID as intLicensesID,ApplicationID,DriverID,IssuedUsingLocalLicenseID as LLicenseID,
                             IssueDate,ExpirationDate,IsActive from InternationalLicenses";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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
        public static DataTable GetInternationalInfoFilterByInterID(int InternationalLicenseID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select InternationalLicenseID as intLicensesID,ApplicationID,DriverID,IssuedUsingLocalLicenseID as LLicenseID,
                             IssueDate,ExpirationDate,IsActive from InternationalLicenses
                             where InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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
        public static DataTable GetInternationalInfoFilterByApplicationID(int ApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select InternationalLicenseID as intLicensesID,ApplicationID,DriverID,IssuedUsingLocalLicenseID as LLicenseID,
                             IssueDate,ExpirationDate,IsActive from InternationalLicenses
                             where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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
        public static DataTable GetInternationalInfoFilterByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select InternationalLicenseID as intLicensesID,ApplicationID,DriverID,IssuedUsingLocalLicenseID as LLicenseID,
                             IssueDate,ExpirationDate,IsActive from InternationalLicenses
                             where DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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
        public static DataTable GetInternationalInfoFilterByLLicenseID(int LLicenseID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select InternationalLicenseID as intLicensesID,ApplicationID,DriverID,IssuedUsingLocalLicenseID as LLicenseID,
                             IssueDate,ExpirationDate,IsActive from InternationalLicenses
                             where IssuedUsingLocalLicenseID = @LLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LLicenseID", LLicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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
        public static DataTable GetInternationalLicenses(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select * from InternationalLicenses";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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
        public static bool GetInternationalLicenseInfoById(int InternationalLicenseID,
                                                           ref string FullName, ref int LocalLicenseID, ref string NationalNo,
                                                           ref int Gender, ref DateTime IssueDate, ref int ApplicationID,
                                                           ref bool IsActive, ref DateTime DateOfBirth, ref int DriverID,
                                                           ref DateTime ExpirationDate ,ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"SELECT
                             P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                             IL.InternationalLicenseID,
                             IL.IssuedUsingLocalLicenseID AS LocalLicenseID,
                             P.NationalNo,
                             P.Gendor,
                             IL.IssueDate,
                             IL.ApplicationID,
                             IL.IsActive,
                             P.DateOfBirth,
                             D.DriverID,
                             IL.ExpirationDate,
                             P.ImagePath
                         FROM InternationalLicenses IL
                         JOIN Drivers D ON D.DriverID = IL.DriverID
                         JOIN People P ON P.PersonID = D.PersonID
                         WHERE IL.InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    FullName = (string)reader["FullName"];
                    LocalLicenseID = Convert.ToInt32(reader["LocalLicenseID"]);
                    NationalNo = (string)reader["NationalNo"];
                    Gender = Convert.ToInt32(reader["Gendor"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    IsActive = (bool)reader["IsActive"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting International License information.", ex);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

    }
}
