using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinesweeperWebApp.Models;

namespace MinesweeperWebApp.Controllers
{
    /*
     * GameController manages behvaior related to the game 
     */
    public class GameController : Controller
    {

        public static BoardModel Board { get; set; } = new BoardModel(10);

        // start the game
        public ActionResult StartGame()
        {
            // deploy mines
            Board.setupLiveNeighbors();

            // calculate neighbor counts for each cell
            Board.calculateLiveNeighbors();

            // return the game board view
            return View("gameBoard", Board);
        }

        /*
         * this method handles a cell click
         */
        public ActionResult HandleCellClick(string cell)
        {
            // determine which cell was clicked
            int row = Convert.ToInt32(cell.Substring(0, 1));
            int col = Convert.ToInt32(cell.Substring(2, 1));
            // update board
            int result = Board.Update(row, col);

            // if the player won
            if (result == 2)
            {
                return View("gameWon");
            }

            // if the player lost
            else if (result == 1)
            {
                return View("gameLost");
            }

            // otherwise
            else
            {
                return View("gameBoard", Board);
            }
        }
    }
}