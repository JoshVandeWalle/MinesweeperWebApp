using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperWebApp.Models
{
    public class GameStorageModel
    {
        public int ID { get; set; }
        public string GameState { get; set; }
        public string User { get; set; }

        public GameStorageModel(int iD, string gameState)
        {
            ID = iD;
            GameState = gameState;
        }

        public GameStorageModel(int iD, string gameState, string user) : this(iD, gameState)
        {
            User = user;
        }
    }
}