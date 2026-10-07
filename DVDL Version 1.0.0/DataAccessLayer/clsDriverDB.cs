using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsDriverDB
    {
        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string Query = @"insert into Drivers
                            (   PersonID,
                            	CreatedByUserID,
                            	CreatedDate)
                            values
                            (@PersonID,
                             @CreatedByUserID,
                             GETDATE())
                              Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
        public static bool GetDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID,ref DateTime CreatedDate)
        {
            bool isfound;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select * from Drivers
                             where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                else
                {
                    isfound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;
            }
            finally
            {
                connection.Close();
            }
            return isfound;
        }
        public static bool GetDriverByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID,ref DateTime CreatedDate)
        {
            bool isfound;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select * from Drivers
                             where DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                else
                {
                    isfound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;
            }
            finally
            {
                connection.Close();
            }
            return isfound;
        }
        public static DataTable GetDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select D.DriverID,P.PersonID,P.NationalNo,    P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,D.CreatedDate ,
                            (Select count (*)  from Licenses L  where L.DriverID = D.DriverID and L.IsActive = 1) as ActiveLicenses
                            from Drivers D
                            join People P on P.PersonID = D.PersonID";
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
        public static DataTable GetDriversFilterByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select D.DriverID,P.PersonID,P.NationalNo,    P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,D.CreatedDate ,
                             (Select count (*)  from Licenses L  where L.DriverID = D.DriverID and L.IsActive = 1) as ActiveLicenses
                             from Drivers D
                             join People P on P.PersonID = D.PersonID
                             where D.DriverID = @DriverID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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

        public static DataTable GetDriversFilterByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select D.DriverID,P.PersonID,P.NationalNo,    P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,D.CreatedDate ,
                             (Select count (*)  from Licenses L  where L.DriverID = D.DriverID and L.IsActive = 1) as ActiveLicenses
                             from Drivers D
                             join People P on P.PersonID = D.PersonID
                             where p.PersonID = @PersonID ";
            SqlCommand command = new SqlCommand(quiry, connection);
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
        public static DataTable GetAllUsersFilterByFullName(string FullName)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select D.DriverID,P.PersonID,P.NationalNo,    P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,D.CreatedDate ,
                            (Select count (*)  from Licenses L  where L.DriverID = D.DriverID and L.IsActive = 1) as ActiveLicenses
                            from Drivers D
                            join People P on P.PersonID = D.PersonID
                            where  P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName like @FullName";
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

        public static DataTable GetAllDriversFilterByNationalNo(string NationalNo)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = @"select D.DriverID,P.PersonID,P.NationalNo,    P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS FullName,D.CreatedDate ,
                             (Select count (*)  from Licenses L  where L.DriverID = D.DriverID and L.IsActive = 1) as ActiveLicenses
                             from Drivers D
                             join People P on P.PersonID = D.PersonID
                             where P.NationalNo like @NationalNo";
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
            catch (System.Exception ex)
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
