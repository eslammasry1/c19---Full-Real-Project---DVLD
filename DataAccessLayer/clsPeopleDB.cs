using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{
    public class clsPeopleDB
    {
        public static bool GetPersonInfoByID(int ID ,ref string NationalNo,ref string FirstName,ref string SecondName,ref string ThirdName,
                                            ref string LastName,ref DateTime DateOfBirth,ref int Gendor,ref string address,ref string Phone
                                            ,ref string Email,ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            String quiry = @"select * from People
                             where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                connection.Open();
                //MessageBox.Show("Not null");
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    address = (string)reader["address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    ImagePath = reader["ImagePath"] == DBNull.Value ? "" : reader["ImagePath"].ToString();
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int ID, ref string FirstName, ref string SecondName, ref string ThirdName,
                                    ref string LastName, ref DateTime DateOfBirth, ref int Gendor, ref string address, ref string Phone
                                    , ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            String quiry = @"select * from People
                             where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    address = (string)reader["address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    ImagePath = reader["ImagePath"] == DBNull.Value ? "" : reader["ImagePath"].ToString();

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static DataTable GetAllPersons()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID";
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
        public static DataTable GetAllPersonsFilterByID(int ID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                            where PersonID like @ID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@ID", ID);
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


        public static DataTable GetAllPersonsFilterByNationalNo(string NationalNo)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where NationalNo like @NationalNo";
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
        public static DataTable GetAllPersonsFilterByFirstName(string FirstName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where FirstName like @FirstName";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName +"%");
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
        public static DataTable GetAllPersonsFilterBySecondName(string SecondName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where SecondName like @SecondName";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@SecondName", SecondName + "%");
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

        public static DataTable GetAllPersonsFilterByThirdName(string ThirdName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where ThirdName like @ThirdName";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@ThirdName", ThirdName + "%");
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
        public static DataTable GetAllPersonsFilterByLastName(string LastName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where LastName like @LastName";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@LastName", LastName + "%");
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
        public static DataTable GetAllPersonsFilterByGendor(int Gendor)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where Gendor like @Gendor";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@Gendor", Gendor);
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
        public static DataTable GetAllPersonsFilterByNationality(string Nationality)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where CountryName like @Nationality";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@Nationality", Nationality + "%");
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
        public static DataTable GetAllPersonsFilterByPhone(string Phone)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where Phone like @Phone";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@Phone", Phone + "%");
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
        public static DataTable GetAllPersonsFilterByEmail(string Email)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"SELECT People.PersonID, People.NationalNo,People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                                            CASE
                                            WHEN  People.Gendor = 0 THEN 'Male'
                                            WHEN  People.Gendor = 1 THEN 'Female'
                                            ELSE 'Unknown'
                                            END as Gendor
							 , People.DateOfBirth, Countries.CountryName as Nationality, People.Phone, People.Email
                             FROM People INNER JOIN
                             Countries ON People.NationalityCountryID = Countries.CountryID
                             where Email like @Email";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@Email", Email + "%");
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
        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,
                                       string LastName,DateTime DateOfBirth,int Gendor, string Address, string Phone,
                                       string Email,int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"insert into People(NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth
                                                ,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath)
                           values      (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth
                                               ,@Gendor,@Address , @Phone,@Email,@NationalityCountryID,@ImagePath);
                                                Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(quiry,connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != null)
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int insertedID))
                {
                    PersonID = insertedID;
                }
            }
            catch (Exception ex)
            {
                PersonID = -1;
            }
            finally
            {
                connection.Close();
            }
            return PersonID;
        }
        public static bool UpdatePerson(int ID,string NationalNo, string FirstName, string SecondName, string ThirdName,
                                       string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone,
                                       string Email, int NationalityCountryID, string ImagePath)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"Update People set
                             NationalNo = @NationalNo,
                             FirstName=@FirstName,
                             SecondName = @SecondName,
                             ThirdName = @ThirdName,
                             LastName=@LastName,
                             DateOfBirth=@DateOfBirth,
                             Gendor = @Gendor,
                             Address=@Address, 
                             Phone=@Phone,
                             Email=@Email,
                             NationalityCountryID=@NationalityCountryID,
                             ImagePath=@ImagePath
                             Where PersonID=@ID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != null)
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }
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
        public static bool DeletePersons(int ID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "Delete people where PersonID = @ID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@ID", ID);
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
        public static bool ExistPerson(int ID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found = 1 from People where PersonID = @ID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@ID", ID);
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

        public static bool ExistPerson(string NationalNo)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found = 1 from People where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
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

    }
}
