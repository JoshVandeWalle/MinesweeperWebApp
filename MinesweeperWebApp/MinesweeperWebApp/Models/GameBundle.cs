using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperWebApp.Models
{
    public class GameBundle
    {
        public BoardModel Board { get; set; }
        public int Difficulty { get; set; }
        public TimeSpan Timer { get; set; }
        public DateTime StartTime { get; set; }
        public int Row;
        public int Column;

        public GameBundle(int difficulty)
        {
            Difficulty = difficulty;
            if (Difficulty == 0)
                Board = new BoardModel(10);
            else if (Difficulty == 2)
                Board = new BoardModel(20);
            else
                Board = new BoardModel(15);
            Timer  = new TimeSpan();
            StartTime = DateTime.Now;
        }
    }
}