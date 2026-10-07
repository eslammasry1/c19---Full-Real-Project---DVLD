using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsLicensesDB
    {
        public static int AddLicenses(int ApplicationID, int DriverID, int LicensesClass, DateTime IssueDate, DateTime ExpirationDate,
                                      string Note, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"insert into Licenses
                            (   ApplicationID,
                            	DriverID,
                            	LicenseClass,
                            	IssueDate,
                            	ExpirationDate,
                            	Notes,
                            	PaidFees,
                            	IsActive,
                            	IssueReason,
                            	CreatedByUserID)
                            values
                            (@ApplicationID,
                             @DriverID,
                             @LicensesClass,
                             @IssueDate,
                             @ExpirationDate,  
                             @Note,
                             @PaidFees,
                             @IsActive,
                             @IssueReason,
                             @CreatedByUserID);
                             Select SCOPE_IDENTITY();
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicensesClass", LicensesClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if (Note == "")
                command.Parameters.AddWithValue("@Note", System.DBNull.Value);
            else
                command.Parameters.AddWithValue("@Note", Note);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    ID = id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return ID;
        }
        public static DataTable GetLicensesOfPerson(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select L.LicenseID,L.ApplicationID,LC.ClassName,L.IssueDate,L.ExpirationDate,L.IsActive from Licenses L
                             join LicenseClasses LC on LC.LicenseClassID=L.LicenseClass
                             join Drivers D on D.DriverID = L.DriverID
                             join LocalDrivingLicenseApplications ldla on l.ApplicationID = ldla.ApplicationID 
                             where D.PersonID =@PersonID";
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
        public static decimal GetLicensesClassFees(int LicensesClassID)
        {
            decimal ClassFess = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select ClassFees from LicenseClasses
                            where LicenseClassID = @LicensesClassID ";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@LicensesClassID", LicensesClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && decimal.TryParse(result.ToString(), out decimal fees))
                {
                    ClassFess = fees;
                }
            }
            catch (Exception ex)
            {
                ClassFess = 0;
            }
            finally
            {
                connection.Close();
            }

            return ClassFess;
        }
        public static bool GetLicensesInfoByLicenseID(int LicenseID,ref int ApplicationID, ref int DriverID, ref int LicenseClass,
                                                          ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,ref decimal PaidFees,
                                                           ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select * from Licenses
                                where LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "";
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = Convert.ToInt32(reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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

        public static bool GetLicensesInfoByApplicationID(int ApplicationID, ref string LicenseClass, ref string FullName, ref int LicenseID, ref string NationalNo, ref int Gendor
                                           , ref DateTime IssueDate, ref DateTime ExpirationDate, ref int IssueReason, ref string Notes, ref bool IsActive, ref DateTime DateOfBirth,
                                            ref int DriverID, ref string ImagePath, ref int IsDetained)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"SELECT
                             LC.ClassName,
                             P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                             L.LicenseID,
                             P.NationalNo,
                             P.Gendor,
                             L.IssueDate,
                             L.ExpirationDate,
                             L.IssueReason,
                             L.Notes,
                             L.IsActive,
                             P.DateOfBirth,
                             D.DriverID,
                             P.ImagePath,
                             CASE 
                                 WHEN EXISTS (
                                     SELECT 1
                                     FROM DetainedLicenses DL
                                     WHERE DL.LicenseID = L.LicenseID
                                       AND DL.IsReleased = 0)
                                 THEN 1
                                 ELSE 0
                             END AS IsDetained
                         FROM Licenses L
                         JOIN Drivers D ON D.DriverID = L.DriverID
                         JOIN People P ON P.PersonID = D.PersonID
						 join LicenseClasses LC ON LC.LicenseClassID = L.LicenseClass
                         where L.ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LicenseClass = (string)reader["ClassName"];
                    FullName = (string)reader["FullName"];
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    NationalNo = (string)reader["NationalNo"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IssueReason = Convert.ToInt32(reader["IssueReason"]);
                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "";
                    IsActive = (bool)reader["IsActive"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";
                    IsDetained = (int)reader["IsDetained"];

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
        public static bool GetLicensesByLicenseID(int LicenseID, ref string LicenseClass, ref string FullName, ref string NationalNo, ref int Gendor
                                           , ref DateTime IssueDate, ref DateTime ExpirationDate, ref int IssueReason, ref string Notes, ref bool IsActive, ref DateTime DateOfBirth,
                                            ref int DriverID, ref string ImagePath, ref int IsDetained)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"SELECT
                             LC.ClassName,
                             P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,
                             P.NationalNo,
                             P.Gendor,
                             L.IssueDate,
                             L.ExpirationDate,
                             L.IssueReason,
                             L.Notes,
                             L.IsActive,
                             P.DateOfBirth,
                             D.DriverID,
                             P.ImagePath,
                             CASE 
                                 WHEN EXISTS (
                                     SELECT 1
                                     FROM DetainedLicenses DL
                                     WHERE DL.LicenseID = L.LicenseID
                                       AND DL.IsReleased = 0)
                                 THEN 1
                                 ELSE 0
                             END AS IsDetained
                         FROM Licenses L
                         JOIN Drivers D ON D.DriverID = L.DriverID
                         JOIN People P ON P.PersonID = D.PersonID
						 join LicenseClasses LC ON LC.LicenseClassID = L.LicenseClass
                         where L.LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LicenseClass = (string)reader["ClassName"];
                    FullName = (string)reader["FullName"];
                    NationalNo = (string)reader["NationalNo"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IssueReason = Convert.ToInt32(reader["IssueReason"]);
                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "";
                    IsActive = (bool)reader["IsActive"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";
                    IsDetained = (int)reader["IsDetained"];

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
        public static bool ExistPerson(int ApplicationID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found=1 from Licenses L where L.ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        public static int GeTDefaultValidityLength(int LicenseClassID)
        {
            int DefaultValidityLength = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select DefaultValidityLength from LicenseClasses where LicenseClassID = @LicenseClassID
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    DefaultValidityLength = count;
                }
            }
            catch (Exception ex)
            {
                DefaultValidityLength = 0;
            }
            finally
            {
                connection.Close();
            }

            return DefaultValidityLength;
        }



        public static bool ExistLicensesByApplicationID(int ApplicationID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found = 1 from Licenses where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        public static bool InActiveLicense(int LicenseId)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"update Licenses set 
                             IsActive = 0
                             where LicenseID = @LicenseId";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@LicenseId", LicenseId);
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
    }

    }
