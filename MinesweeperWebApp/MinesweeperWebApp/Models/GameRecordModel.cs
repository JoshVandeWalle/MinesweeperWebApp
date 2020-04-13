using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperWebApp.Models
{
    public class GameRecordModel
    {
        public int ID { get; set; }
        public string User { get; set; }
        public int Difficulty { get; set; }
        public string Time { get; set; }

        public GameRecordModel(int iD, string user, int difficulty, string time)
        {
            ID = iD;
            User = user;
            Difficulty = difficulty;
            Time = time;
        }
    }
}