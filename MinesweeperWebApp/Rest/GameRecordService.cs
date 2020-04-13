using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Rest
{
    /*
     * this is a business service class for the GameRecordModel object model
     */
    internal class GameRecordService
    {
        /*
         * this method retrieves game records based on Game ID
         * @param id the Game ID
         * @retrun GameRecordModel | NULL the desired game record or null if nothing was found
         */
        public GameRecordModel RetrieveByID(int id)
        {
            // database connection string
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // connect to database in business service
            // NOTE this is a best practice that allows ACID transactions
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // instantiate DAO
                GameRecordDAO dao = new GameRecordDAO(connection);
                
                // conect to database
                connection.Open();

                // pass control to DAO to get records
                List<GameRecordModel> records = dao.ReadByID(id);
               
                // close database connection
                connection.Close();

                // if the game record wasn't found
                if (records.Count != 1)
                {
                    return null;
                }

                // otherwise the game was found
                else
                {
                    // return the game record
                    foreach (GameRecordModel model in records)
                     {
                         return model;
                     } 

                     return null;
                }
            }
           
        }

        /*
         * this method retrieves game records based on the user who played them
         * @param username the requested user
         * @return List<GameRecordModel> list of games the user has won
         */
        public List<GameRecordModel> RetrieveByUser(string username)
        {
            // database connection string
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            // initalize list to return
            List<GameRecordModel> records = new List<GameRecordModel>();

            // connect to database in business service
            // NOTE this is a best practice that allows ACID transactions
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // instantiate DAO
                GameRecordDAO dao = new GameRecordDAO(connection);

                // open database connection
                connection.Open();

                // pass control to DAO to get records
                records = dao.ReadByUser(username);

                // close database connection
                connection.Close();
            }

            // return results
            return records;
        }
    }
}