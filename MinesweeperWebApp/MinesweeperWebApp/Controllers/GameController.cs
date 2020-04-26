using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services;
using MinesweeperWebApp.Services.Business;
using MinesweeperWebApp.Services.Utility;
using Newtonsoft.Json;

namespace MinesweeperWebApp.Controllers
{
    /*
     * GameController manages behvaior related to the game 
     */
    public class GameController : Controller
    {
        public static GameBundle Bundle { get; set; }
        public ILogger Logger;

        public GameController(ILogger logger)
        {
            Logger = logger;
        }



        // start the game
        // authorization required
        [HttpPost]
        [MinesweeperAuthorization]
        public ActionResult StartGame(string difficulty)
        {
            // use try/catch block to handle exceptions
            try
            {
                // the difficulty the player selected on the previous form
                int chosenDifficulty = Int32.Parse(difficulty);

                // instaniate game bundle. The bundle model includes all models and primitives related to the game.
                Bundle = new GameBundle(chosenDifficulty);

                Bundle.User = Cache.AccessCache().Get("activeAccount");

                // instantiate game service
                // the game service is a Business Logic Layer class that enforces rules & logic
                GameService gameService = new GameService();

                // deploy mines
                gameService.SetupLiveNeighbors(Bundle);

                // calculate neighbor counts for each cell
                gameService.CalculateLiveNeighbors(Bundle);

                // return the game board view
                return View("gameBoard", Bundle);
            }

            // handle exceptions
            catch (Exception e)
            {
                // log error
                Logger.Error("Failed to start game: " + e.Message);
                // return generic error page
                return View("Exception");
            }
        }

        /*
         * this method handles a cell click
         */
        [HttpPost]
        [MinesweeperAuthorization]
        public ActionResult HandleCellClick(string cell)
        {
            try
            {
                Logger.Info("Entering GameController.HandleCellClick()");
                GameService gameService = new GameService();

                string[] coords = cell.Split(',');

                // determine which cell was clicked
                Bundle.Row = Convert.ToInt32(coords[0]);
                Bundle.Column = Convert.ToInt32(coords[1]);

                Logger.Info("Values parsed " + Bundle.Row + "," + Bundle.Column);

                if (Bundle.Board.Grid[Bundle.Row, Bundle.Column].isFlagged == false)
                {
                    // update board
                    int result = gameService.Update(Bundle);

                    // if the player won
                    if (result == 2)
                    {
                        // instantiate service
                        GameRecordService archiveService = new GameRecordService();

                        // pass control to service
                        archiveService.Archive(new GameRecordModel(-1, Bundle.User, Bundle.Difficulty, Bundle.Timer.ToString()));

                        Logger.Info("Game Won!");
                        return View("gameWon", Bundle);
                    }

                    // if the player lost
                    else if (result == 1)
                    {
                        Logger.Info("Game Lost!");
                        return View("gameLost", Bundle);
                    }

                    // otherwise
                    else
                    {
                        Bundle.Timer = DateTime.Now - Bundle.StartTime;
                        Logger.Info("Game Continues");
                        //return PartialView("_board", Bundle);
                        return View("gameBoard", Bundle);
                    }
                }

                else
                {
                    Bundle.Timer = DateTime.Now - Bundle.StartTime;
                    Logger.Info("Can't reveal flagged cell");
                    return View("gameBoard", Bundle);
                    //return PartialView("_board", Bundle);
                }
            }

            // handle exceptions
            catch (Exception e)
            {
                // log error
                Logger.Error("Exception in GameController.HandleCellClick(): " + e.Message);
                // return generic error page
                return View("Exception");
            }
        }

        // authorization required
        [HttpPost]
        [MinesweeperAuthorization]
        public ActionResult HandleCellRightClick(string cell)
        {
            try
            {
                Logger.Info("Entering GameController.HandleCellRightClick()");
                string[] coords = cell.Split(',');

                // determine which cell was clicked
                int row = Convert.ToInt32(coords[0]);
                int column = Convert.ToInt32(coords[1]);

                Logger.Info("Values parsed " + row + "," + column);

                if (Bundle.Board.Grid[row, column].isFlagged == false)
                {
                    Logger.Info("setting flag property to true");
                    Bundle.Board.Grid[row, column].isFlagged = true;
                }

                else
                {
                    Logger.Info("setting flag property to false");
                    Bundle.Board.Grid[row, column].isFlagged = false;
                }

                Bundle.Timer = DateTime.Now - Bundle.StartTime;
                //return PartialView("_board", Bundle);
                return View("gameBoard", Bundle);
            }

            // handle exceptions
            catch (Exception e)
            {
                // log error
                Logger.Error("Exception in GameController.HandleCellRightClick(): " + e.Message);
                // return generic error page
                return View("Exception");
            }
        }

        // authorization required
        [HttpPost]
        [MinesweeperAuthorization]
        public ActionResult HandleSave()
        {
            try
            {
                // instantiate business service
                GameService service = new GameService();

                // instantiate model
                GameStorageModel storageModel = new GameStorageModel(-1, JsonConvert.SerializeObject(Bundle), Bundle.User);

                // pass control to service and catch return value
                bool success = service.Save(storageModel);

                Logger.Info("Game saved successfully!");

                // return view and boolean flag
                return View("save", success);
            }

            catch (Exception e)
            {
                Logger.Error("Failed to save game: " + e.Message);
                return View("exception");
            }
        }

        // authorization required
        [HttpPost]
        [MinesweeperAuthorization]
        public ActionResult HandleLoad()
        {
            try
            {
                // instantiate business service
                GameService service = new GameService();

                // pass control to service and catch return value
                GameStorageModel businessLayerResponseModel = service.Load(Bundle.User);

                // deserialize game state and save to bundle
                Bundle = JsonConvert.DeserializeObject<GameBundle>(businessLayerResponseModel.GameState);

                // configure timer
                Bundle.StartTime = DateTime.Now - Bundle.Timer;

                // log successful load
                Logger.Info("Game loaded successfully!");

                // return board view with bundle
                return View("gameBoard", Bundle);
            }

            catch (Exception e)
            {
                Logger.Error("Failed to load game: " + e.Message);
                return View("exception");
            }
            
        }
    }
}