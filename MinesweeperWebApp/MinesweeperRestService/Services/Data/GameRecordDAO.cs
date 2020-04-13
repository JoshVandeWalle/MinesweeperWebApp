using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MinesweeperRestService.Models;

namespace MinesweeperRestService.Services.Data
{
    public class GameRecordDAO
    {
        private SqlConnection Connection { get; set; }

        public GameRecordDAO(SqlConnection connection)
        {
            Connection = connection;
        }

        public List<GameRecordModel> ReadByID(int id)
        {
            String query = "SELECT * FROM dbo.GAME_RECORD WHERE ID = @id";

            List<GameRecordModel> modelSet = new List<GameRecordModel>();

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    modelSet.Add(new GameRecordModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }

                return modelSet;
            }
        }

        public List<GameRecordModel> ReadByUser(string username)
        {
            String query = "SELECT * FROM dbo.GAME_RECORD WHERE Account = @account";

            List<GameRecordModel> modelSet = new List<GameRecordModel>();

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@account", SqlDbType.NVarChar).Value = username;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    modelSet.Add(new GameRecordModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }

                return modelSet;
            }
        }
    }
}