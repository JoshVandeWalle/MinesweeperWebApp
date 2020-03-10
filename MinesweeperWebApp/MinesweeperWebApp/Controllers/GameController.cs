using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services.Business;

namespace MinesweeperWebApp.Controllers
{
    /*
     * GameController manages behvaior related to the game 
     */
    public class GameController : Controller
    {

        public static BoardModel Board { get; set; } = new BoardModel(10);
        public static GameBundle Bundle { get; set; }

        // start the game
        public ActionResult StartGame(string difficulty)
        {
            int chosenDifficulty = Int32.Parse(difficulty);

            Bundle = new GameBundle(chosenDifficulty);

            GameService gameService = new GameService();
            // deploy mines
            //Board.setupLiveNeighbors();
            gameService.SetupLiveNeighbors(Bundle);

            // calculate neighbor counts for each cell
            //Board.calculateLiveNeighbors();

            gameService.CalculateLiveNeighbors(Bundle);

            // return the game board view
            //return View("gameBoard", Board);
            return View("gameBoard", Bundle);
        }

        /*
         * this method handles a cell click
         */
        public ActionResult HandleCellClick(string cell)
        {
            GameService gameService = new GameService();

            string[] coords = cell.Split(',');

            // determine which cell was clicked
            Bundle.Row = Convert.ToInt32(coords[0]);
            Bundle.Column = Convert.ToInt32(coords[1]);

            // update board
            //int result = Board.Update(row, col);
            int result = gameService.Update(Bundle);

            // if the player won
            if (result == 2)
            {
                return View("gameWon");
            }

            // if the player lost
            else if (result == 1)
            {
                return View("gameLost", Bundle);
            }

            // otherwise
            else
            {
                Bundle.Timer = DateTime.Now - Bundle.StartTime;
                return PartialView("_board", Bundle);
            }
        }
    }
}