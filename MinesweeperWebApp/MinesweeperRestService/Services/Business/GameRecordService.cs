using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MinesweeperRestService.Models;
using MinesweeperRestService.Services.Data;

namespace MinesweeperRestService.Services.Business
{
    public class GameRecordService
    {
        public GameRecordModel RetrieveByID(int id)
        {
            string connectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                GameRecordDAO dao = new GameRecordDAO(connection);

                connection.Open();
                List<GameRecordModel> records = dao.ReadByID(id);
                connection.Close();

                if (records.Count != 1)
                {
                    return null;
                }

                else
                {
                   foreach (GameRecordModel model in records)
                    {
                        return model;
                    }

                    return null;
                }
            }
        }

        public List<Object> RetrieveByUser(string username)
        {
            return null;
        }
    }
}