using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsApplicationDB
    {
        public static int AddNewApplication(int ApplicantPersonID,DateTime ApplicationDate, int ApplicationTypeID,
                                            int ApplicationStatus, DateTime LastStatusDate,decimal PaidFees, int UserID)
        {
            int ID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSitting.ConnectionString);

            string query = @"INSERT INTO Applications
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
                                 @ApplicantPersonID,
                                 @ApplicationDate,
                                 @ApplicationTypeID,
                                 @ApplicationStatus,
                                 @LastStatusDate,
                                 @PaidFees,
                                 @UserID
                             );
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", UserID);

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

    }
}
