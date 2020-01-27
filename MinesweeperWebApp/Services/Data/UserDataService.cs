using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MinesweeperWebApp.Models;

namespace MinesweeperWebApp.Services.Data
{
    public class UserDataService
    {
        /*
         * CRUD create method
         */
        public bool CreateAccount(UserModel user)
        {
            // by deafult registration success is false
            bool success = false;

            // string used to connect to the database
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // query string for registration
            string query = "INSERT INTO dbo.User_Table (FIRSTNAME, LASTNAME, SEX, STATE, AGE, USERNAME, EMAIL, PASSWORD) VALUES (@FIRSTNAME, @LASTNAME, @SEX, @STATE, @Age, @USERNAME, @EMAIL, @PASSWORD);";

            // setup database connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                // instantiate the SqlCommand
                SqlCommand command = new SqlCommand(query, connection);

                // bind params for prepared statement
                command.Parameters.Add("@FIRSTNAME", System.Data.SqlDbType.VarChar, 50).Value = user.FirstName;
                command.Parameters.Add("@LASTNAME", System.Data.SqlDbType.VarChar, 50).Value = user.LastName;
                command.Parameters.Add("@SEX", System.Data.SqlDbType.VarChar, 50).Value = user.Sex;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 50).Value = user.State;
                command.Parameters.Add("@AGE", System.Data.SqlDbType.VarChar, 50).Value = user.Age;
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 50).Value = user.Email;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                
            }

            // return boolean flag to indicate whether registration was successful
            return true;
        }

        /*
         * CRUD Read method
         */
        public bool ReadAccount(CredentialModel CredentialSet)
        {
            // by default database connection is invalid
            bool success = false;

            // database connection string
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // sql query used for login
            string query = "SELECT * FROM dbo.User_Table WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD;";

            // setup database connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // instantiate the SqlCommand
                SqlCommand command = new SqlCommand(query, connection);

                // bind params for prepared statement
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 50).Value = CredentialSet.Email;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = CredentialSet.Password;

                // use try to handle database exceptions
                try
                {
                    // open databse connection
                    connection.Open();
                    // instantiate SqlDataReader to count result set 
                    SqlDataReader reader = command.ExecuteReader();

                    // if the set is not empty
                    if (reader.HasRows)
                    {
                        // set login success to true
                        success = true;
                    }

                    // close reader
                    reader.Close();
                }

                // handle exceptions
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // return database flag
            return success;
        }
    }
}
