using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestTypesDB
    {
        public static DataTable GetAllTestType()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = "SELECT * from TestTypes";
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
        public static bool EditTestType(int ID,string Title,string Description,decimal Fees)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"update TestTypes set
                             TestTypeTitle = @Title,
                             TestTypeDescription = @Description,
                             TestTypeFees = @Fees
                             where TestTypeID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@Title", Title);
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
        public static bool GetAllTestTypeById(int ID,ref string Title,ref string Description,ref decimal Fees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = "select * from TestTypes where TestTypeID = @ID";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Title = (string)reader["TestTypeTitle"];
                    Description = (string)reader["TestTypeDescription"];
                    Fees = Convert.ToDecimal(reader["TestTypeFees"]);
                    IsFound = true;
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
        public static int GetNumberRowsOfTestType()
        {
            int RowsNo = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = "select COUNT(*) from TestTypes";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                RowsNo = Convert.ToInt32(command.ExecuteScalar());
            }
            catch(Exception ex) 
            {
                RowsNo = 0;
            }
            finally
            {
                connection.Close();
            }
            return RowsNo;
        }
    }
}
