using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services.Utility;

namespace MinesweeperWebApp.Services.Data
{
    /*
     * This class is a DAO for the GameStorageModel object model
     */
    public class GameDAO
    {
        // the database connection
        private SqlConnection Connection;

        /* non-default constructor initializes object state from parameters
         *@param connection the databse connection 
         */
        public GameDAO(SqlConnection connection)
        {
            Connection = connection;
        }

        /*
         * this method creates a new game save 
         * @param storageModel the game to be saved
         * @return bool true if the save operation was successful false otherwise
         */
        public bool Create(GameStorageModel storageModel)
        {
            // the SQL query with prepared statement parameters
            String query = "INSERT INTO dbo.GAME (STATE, ACCOUNT) VALUES (@state, @account)";

            // set a SqlCommand object using the SQL query and database connection
            // this object supposts prepared statements
            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                // configure prepared statement parameters
                command.Parameters.Add("@state", SqlDbType.Text).Value = storageModel.GameState;
                command.Parameters.Add("@account", SqlDbType.NVarChar).Value = storageModel.User;

                // execute query and catch row count
                int rowsAffected = command.ExecuteNonQuery();

                // if rows were affected
                if (rowsAffected > 0)
                {
                    // save operation is successful
                    return true;
                }

                // otherwise
                else
                {
                    // save operation fails
                    return false;
                }
            }
        }

        /*
         * this method reads a game save
         * @param user the user whose save is to be loaded
         * return List<GameStorageModel> the matching save(s) NOTE: there should only be one
         */
        public List<GameStorageModel> Read(string user)
        {
            // the SQL query with prepared statement parameters
            String query = "SELECT * FROM dbo.GAME WHERE ACCOUNT = @account";

            // instantiate list of models to be returned
             List<GameStorageModel> modelSet = new List<GameStorageModel>();

            // set a SqlCommand object using the SQL query and database connection
            // this object supposts prepared statements
            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                // configure prepared statement parameters
                command.Parameters.Add("@account", SqlDbType.NVarChar).Value = user;

                // instantiate reader
                SqlDataReader reader = command.ExecuteReader();

                // loop over result set
                while (reader.Read())
                {
                    // store results in list of matching saves
                    modelSet.Add(new GameStorageModel(reader.GetInt32(0), reader.GetString(1)));
                }

                // return list of matching saves
                return modelSet;
            }
        }

        /*
         * this method deletes a save and is used to enforce the one user one save rule
         * @param user the user whose save is to be deleted
         * @return bool flag that indicates if the deletion operation was successful
         */
        public bool Delete(string user)
        {
            // the SQL query with prepared statement parameters
            String query = "DELETE FROM dbo.GAME WHERE ACCOUNT = @account";

            // set a SqlCommand object using the SQL query and database connection
            // this object supposts prepared statements
            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                // configure prepared statement parameters
                command.Parameters.Add("@account", SqlDbType.NVarChar).Value = user;

                // execute query and catch row count
                int rowsAffected =  command.ExecuteNonQuery();

                // if rows were affected
                if (rowsAffected > 0)
                {
                    // delete operation is successful
                    return true;
                }

                // otherwise
                else
                {
                    // delete operation fails
                    return false;
                }
            }
        }
    }
}
