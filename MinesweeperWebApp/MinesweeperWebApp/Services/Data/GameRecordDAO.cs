using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MinesweeperWebApp.Models;

namespace MinesweeperWebApp.Services.Data
{
    public class GameRecordDAO
    {
        public SqlConnection Connection { get; set; }

        public GameRecordDAO(SqlConnection connection)
        {
            Connection = connection;
        }

        public bool Create(GameRecordModel game)
        {
            String query = "INSERT INTO dbo.GAME_RECORD (ACCOUNT, TIME, DIFFICULTY) VALUES (@user, @time, @difficulty)";

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@user", SqlDbType.NVarChar).Value = game.User;
                command.Parameters.Add("@time", SqlDbType.NVarChar).Value = game.Time;
                command.Parameters.Add("@difficulty", SqlDbType.Int).Value = game.Difficulty;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }


        public List<GameRecordModel> ReadByID(int id)
        {
            string query = "SELECT * FROM dbo.GAME_RECORD WHERE ID = @id";

            List<GameRecordModel> modelSet = new List<GameRecordModel>();

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    modelSet.Add(new GameRecordModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), reader.GetString(2)));
                }

                return modelSet;
            }
        }
    }
}