using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Rest
{
    /*
     * this class is a DAO for the GameRecordModel object model 
     */
    internal class GameRecordDAO
    {
        // the connection variable
        public SqlConnection Connection { get; set; }

        /*
         * non-default constructor initializes object state from parameters
         * @param connection the database connection
         */
        public GameRecordDAO(SqlConnection connection)
        {
            Connection = connection;
        }

        /*
         * this method gets records that match the specified game ID
         * @param id the game ID
         * @return List<GameRecordModel> all matching records
         */
        public List<GameRecordModel> ReadByID(int id)
        {
            // the SQL query with prepared statement parameters
            string query = "SELECT * FROM dbo.GAME_RECORD WHERE ID = @id";

            // instantiate list to return
            List<GameRecordModel> modelSet = new List<GameRecordModel> ();

            // create a SqlCommand object with SQL query and database connection
            // this object is used to support prepared statements
            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                // configure parameters for prepared statment
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                // instantiate reader
                SqlDataReader reader = command.ExecuteReader();

                // loop over result set
                while (reader.Read())
                {
                    // add each result to the list as a game record
                    modelSet.Add(new GameRecordModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), reader.GetString(2)));

                    
                }

                // return the list of matching records
                return modelSet;
            }
        }

        /*
         * this method returns records of all games won by a specific user
         */
        public List<GameRecordModel> ReadByUser(string username)
        {
            // the SQL query including prepared statemnt parameters
            string query = "SELECT * FROM dbo.GAME_RECORD WHERE ACCOUNT = @account";

            // instantiate the list to return
            List<GameRecordModel> modelSet = new List<GameRecordModel>();

            // create a SqlCommand object with SQL query and database connection
            // this object is used to support prepared statements
            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                // configure prepared statement parameters
                command.Parameters.Add("@account", SqlDbType.NVarChar).Value = username;

                // instantiate reader
                SqlDataReader reader = command.ExecuteReader();

                // loop over result set
                while (reader.Read())
                {
                    // add matching records to the list of matching records
                    modelSet.Add(new GameRecordModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), reader.GetString(2)));
                }

                // return list of matching records
                return modelSet;
            }
        }
    }
}