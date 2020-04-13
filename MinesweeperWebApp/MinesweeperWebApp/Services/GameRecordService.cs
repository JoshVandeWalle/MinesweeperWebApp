using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services.Data;

namespace MinesweeperWebApp.Services
{
    public class GameRecordService
    {
        public bool Archive(GameRecordModel game)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            bool success = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                GameRecordDAO dao = new GameRecordDAO(connection);

                connection.Open();
                success = dao.Create(game);
                connection.Close();

                
            }
            return success;
        }
    }
}