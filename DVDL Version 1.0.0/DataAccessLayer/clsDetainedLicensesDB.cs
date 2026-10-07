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
    public class clsDetainedLicensesDB
    {
        public static int DetainedLicenses(int LicenseID,decimal FineFees,int CreatedByUserID )
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"insert into DetainedLicenses
                             (
                              LicenseID,
                              DetainDate,
                              FineFees,
                              CreatedByUserID,
                              IsReleased,
                              ReleaseDate,
                              ReleasedByUserID,
                              ReleaseApplicationID
                             )
                             values
                             (@LicenseID,
                              GETDATE(),
                              @FineFees,
                              @CreatedByUserID,
                              0,
                              null,
                              null,
                              null);
                             select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null &&int.TryParse(result.ToString(),out int OutID))
                {
                    ID = OutID;
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
        public static bool ReleaseLicense(int DetainID,int ReleasedByUserID,int ReleaseApplicationID)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"update DetainedLicenses set
                            IsReleased = 1,
                            ReleaseDate = GETDATE(),
                            ReleasedByUserID = @ReleasedByUserID,
                            ReleaseApplicationID = @ReleaseApplicationID
                            where DetainID = @DetainID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
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

        public static bool GetDetailedLicenseInfoByDetainID(int DetainID, ref int LicenseID, ref DateTime DetainDate,ref decimal FineFees,ref int CreatedByUserID,
                                                              ref bool IsReleased,ref DateTime? ReleaseDate,ref int? ReleasedByUserID,ref int? ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT *
                     FROM DetainedLicenses
                     WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = reader["ReleaseDate"] == DBNull.Value? null: (DateTime?)reader["ReleaseDate"];

                    ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value? null: (int?)reader["ReleasedByUserID"];

                    ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value? null: (int?)reader["ReleaseApplicationID"];
                }

                reader.Close();
            }
            catch(Exception ex) 
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        public static bool GetDetailedLicenseInfoByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate,ref decimal FineFees,ref int CreatedByUserID,
                                                              ref bool IsReleased,ref DateTime? ReleaseDate,ref int? ReleasedByUserID,ref int? ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT *
                     FROM DetainedLicenses
                     WHERE LicenseID = @LicenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = reader["ReleaseDate"] == DBNull.Value? null: (DateTime?)reader["ReleaseDate"];

                    ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value? null: (int?)reader["ReleasedByUserID"];

                    ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value? null: (int?)reader["ReleaseApplicationID"];
                }

                reader.Close();
            }
            catch(Exception ex) 
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool LicensesIsDetain(int LicenseID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select Found =1 from DetainedLicenses where IsReleased = 0 and LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
        public static DataTable GetDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT
                        DL.DetainID AS DID,
                        DL.LicenseID AS LID,
                        DL.DetainDate AS DDate,
                        DL.IsReleased,
                        DL.FineFees,
                        DL.ReleaseDate,
                        P.NationalNo AS NNo,
                        P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                        DL.ReleaseApplicationID AS ReleaseAppID
                    FROM DetainedLicenses DL
                    INNER JOIN Licenses L ON DL.LicenseID = L.LicenseID
                    INNER JOIN Drivers D ON L.DriverID = D.DriverID
                    INNER JOIN People P ON D.PersonID = P.PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetDetainedLicensesByDetainID(int DetainID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT
                        DL.DetainID AS DID,
                        DL.LicenseID AS LID,
                        DL.DetainDate AS DDate,
                        DL.IsReleased,
                        DL.FineFees,
                        DL.ReleaseDate,
                        P.NationalNo AS NNo,
                        P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                        DL.ReleaseApplicationID AS ReleaseAppID
                    FROM DetainedLicenses DL
                    INNER JOIN Licenses L ON DL.LicenseID = L.LicenseID
                    INNER JOIN Drivers D ON L.DriverID = D.DriverID
                    INNER JOIN People P ON D.PersonID = P.PersonID
                    WHERE DL.DetainID = @DetainID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static DataTable GetDetainedLicensesByLicenseID(int LicenseID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT
                        DL.DetainID AS DID,
                        DL.LicenseID AS LID,
                        DL.DetainDate AS DDate,
                        DL.IsReleased,
                        DL.FineFees,
                        DL.ReleaseDate,
                        P.NationalNo AS NNo,
                        P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                        DL.ReleaseApplicationID AS ReleaseAppID
                    FROM DetainedLicenses DL
                    INNER JOIN Licenses L ON DL.LicenseID = L.LicenseID
                    INNER JOIN Drivers D ON L.DriverID = D.DriverID
                    INNER JOIN People P ON D.PersonID = P.PersonID
                    WHERE DL.LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static DataTable GetDetainedLicensesByIsReleased(bool IsReleased)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT
                        DL.DetainID AS DID,
                        DL.LicenseID AS LID,
                        DL.DetainDate AS DDate,
                        DL.IsReleased,
                        DL.FineFees,
                        DL.ReleaseDate,
                        P.NationalNo AS NNo,
                        P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                        DL.ReleaseApplicationID AS ReleaseAppID
                    FROM DetainedLicenses DL
                    INNER JOIN Licenses L ON DL.LicenseID = L.LicenseID
                    INNER JOIN Drivers D ON L.DriverID = D.DriverID
                    INNER JOIN People P ON D.PersonID = P.PersonID
                    WHERE DL.IsReleased = @IsReleased";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static DataTable GetDetainedLicensesByNationalNo(string NationalNo)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT
                        DL.DetainID AS DID,
                        DL.LicenseID AS LID,
                        DL.DetainDate AS DDate,
                        DL.IsReleased,
                        DL.FineFees,
                        DL.ReleaseDate,
                        P.NationalNo AS NNo,
                        P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                        DL.ReleaseApplicationID AS ReleaseAppID
                    FROM DetainedLicenses DL
                    INNER JOIN Licenses L ON DL.LicenseID = L.LicenseID
                    INNER JOIN Drivers D ON L.DriverID = D.DriverID
                    INNER JOIN People P ON D.PersonID = P.PersonID
                    WHERE P.NationalNo Like @NationalNo";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo + "%");

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static DataTable GetDetainedLicensesByFullName(string FullName)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT
                        DL.DetainID AS DID,
                        DL.LicenseID AS LID,
                        DL.DetainDate AS DDate,
                        DL.IsReleased,
                        DL.FineFees,
                        DL.ReleaseDate,
                        P.NationalNo AS NNo,
                        P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                        DL.ReleaseApplicationID AS ReleaseAppID
                    FROM DetainedLicenses DL
                    INNER JOIN Licenses L ON DL.LicenseID = L.LicenseID
                    INNER JOIN Drivers D ON L.DriverID = D.DriverID
                    INNER JOIN People P ON D.PersonID = P.PersonID
                    WHERE P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName LIKE @FullName ";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FullName", FullName + "%");

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static DataTable GetDetainedLicensesByReleaseApplicationID(int ReleaseApplicationID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string Query = @"SELECT
                        DL.DetainID AS DID,
                        DL.LicenseID AS LID,
                        DL.DetainDate AS DDate,
                        DL.IsReleased,
                        DL.FineFees,
                        DL.ReleaseDate,
                        P.NationalNo AS NNo,
                        P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                        DL.ReleaseApplicationID AS ReleaseAppID
                    FROM DetainedLicenses DL
                    INNER JOIN Licenses L ON DL.LicenseID = L.LicenseID
                    INNER JOIN Drivers D ON L.DriverID = D.DriverID
                    INNER JOIN People P ON D.PersonID = P.PersonID
                    WHERE DL.ReleaseApplicationID = @ReleaseApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}
