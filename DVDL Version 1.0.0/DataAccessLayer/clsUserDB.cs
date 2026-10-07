using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsUserDB
    {
        public static bool UserIsExistByUserNameAndPassword(string UserName, string Password)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select found = 1  from Users
                             where UserName = @UserName and Password = @Password ";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
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
        public static bool UserIsActive(string UserName)
        {
            bool IsActive = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select isActive from Users
                             where UserName = @UserName ";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsActive = false;
            }
            finally
            {
                connection.Close();
            }
            return  IsActive;
        }
        public static bool GetUserByUserID(int UserID,ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select * from Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(quiry,connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                else
                {
                    IsFound = false;
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
        public static bool GetUserByUserName(string UserName, ref int PersonID, ref int UserID, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select * from Users where UserName = @UserName";
            SqlCommand command = new SqlCommand(quiry,connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                else
                {
                    IsFound = false;
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
        public static DataTable GetAllUsers()
        {
            DataTable DT = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select users.UserID,users.PersonID,CONCAT(FirstName,' ' ,SecondName,' ',ThirdName,' ',LastName)
                             as FullName,users.UserName,users.IsActive from Users join People 
                             on People.PersonID = users.PersonID";
            SqlCommand command = new SqlCommand(quiry,connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    DT.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return DT;
        }
        public static int AddNewUser(int PersonID ,string UserName,string Password,bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"insert into Users( PersonID , UserName, Password, IsActive)
                           values      (@PersonID , @UserName, @Password, @IsActive);
                                                Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }

            }
            catch (Exception ex)
            {
                UserID = -1;
            }
            finally
            {
                connection.Close();
            }
            return UserID;
        }
        public static bool EditUser(int UserID, string UserName, string Password, bool IsActive)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection( clsDataAccessSitting.ConnectionString);
            string quiry = @"Update Users set
                             UserName = @UserName,
                             Password = @Password,
                             IsActive = @IsActive
                             where UserID = @UserID";
            SqlCommand command = new SqlCommand(quiry,connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
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
        public static bool DeleteUser(int UserID)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "Delete Users where UserId = @UserID";
            SqlCommand command = new SqlCommand(quiry,connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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

        public static DataTable GetAllUsersFilterByUserID(int UserID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select users.UserID,users.PersonID,CONCAT(FirstName,' ' ,SecondName,' ',ThirdName,' ',LastName)
                             as FullName,users.UserName,users.IsActive from Users join People 
                             on People.PersonID = users.PersonID
                             where UserID like @UserID";
            SqlCommand command = new SqlCommand(quiry,connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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
        public static DataTable GetAllUsersFilterByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select users.UserID,users.PersonID,CONCAT(FirstName,' ' ,SecondName,' ',ThirdName,' ',LastName)
                             as FullName,users.UserName,users.IsActive from Users join People 
                             on People.PersonID = users.PersonID
                             where users.PersonID like @PersonID";
            SqlCommand command = new SqlCommand(quiry,connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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
        public static DataTable GetAllUsersFilterByUserName(string UserName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select users.UserID,users.PersonID,CONCAT(FirstName,' ' ,SecondName,' ',ThirdName,' ',LastName)
                             as FullName,users.UserName,users.IsActive from Users join People 
                             on People.PersonID = users.PersonID
                             where UserName like @UserName";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@UserName", UserName + "%");
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
        public static DataTable GetAllUsersFilterByFullName(string FullName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select users.UserID,users.PersonID,CONCAT(FirstName,' ' ,SecondName,' ',ThirdName,' ',LastName)
                             as FullName,users.UserName,users.IsActive from Users join People 
                             on People.PersonID = users.PersonID
                             where CONCAT(FirstName,' ' ,SecondName,' ',ThirdName,' ',LastName) like @FullName";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@FullName",  FullName + "%");
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
        public static DataTable GetAllUsersFilterByIsActive(bool isActive)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select users.UserID,users.PersonID,CONCAT(FirstName,' ' ,SecondName,' ',ThirdName,' ',LastName)
                             as FullName,users.UserName,users.IsActive from Users join People 
                             on People.PersonID = users.PersonID
                             where isActive like @isActive";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@isActive", isActive);
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
        public static bool ExistUserByPersonID(int PersonID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found = 1 from Users where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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
        public static bool ExistUserByUserID(int UserID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found = 1 from Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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

        public static bool ExistUserByUserName(string UserName)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select found = 1 from Users where UserName = @UserName";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
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

        public static bool EditPassword(int UserID, string Password)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"Update Users set
                             Password = @Password
                             where UserID = @UserID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);
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
