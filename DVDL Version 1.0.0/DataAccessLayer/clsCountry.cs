using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsCountry
    {
        public static bool GetCountryName(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string quiry = "select * from Countries where CountryID = @CountryID";
            SqlCommand command = new SqlCommand(quiry, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CountryName = (string)reader["CountryName"];
                }
                isFound = true;

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
        public static DataTable GetCountryList()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection( clsDataAccessSitting.ConnectionString);
            string quiry = "select * from Countries";
            SqlCommand commandm = new SqlCommand(quiry, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = commandm.ExecuteReader();
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

    }
}
