using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsApplicationTypesDB
    {
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = "select ApplicationTypeID as ID,ApplicationTypeTitle as Title ,ApplicationFees as fees from ApplicationTypes";
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
        public static bool GetApplicationTypeById(int ApplicationTypeID, ref string ApplicationType
                                                  ,ref decimal ApplicationFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"select * from ApplicationTypes
                            where ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                IsFound = true;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationType = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToDecimal(reader["ApplicationFees"]);
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

        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationType, decimal ApplicationFees)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);
            string query = @"update ApplicationTypes set
                             ApplicationTypeTitle = @ApplicationTypeTitle,
                             ApplicationFees = @ApplicationFees
                             where ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationType);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
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
        public static int GetApplicationTypesCount()
        {
            int RowsNumber = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"SELECT COUNT(*) FROM ApplicationTypes";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                RowsNumber = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                RowsNumber = 0;
            }
            finally
            {
                connection.Close();
            }

            return RowsNumber;
        }
        public static decimal GetFeesType(int ApplicationTypeID)
        {
            decimal Fees = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"select ApplicationFees from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && decimal.TryParse(result.ToString(), out decimal fees))
                {
                    Fees = fees;
                }
            }
            catch (Exception ex)
            {
                Fees = 0;
            }
            finally
            {
                connection.Close();
            }

            return Fees;
        }

    }
}
