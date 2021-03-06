﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services.Data;
using MinesweeperWebApp.Services.Utility;

namespace MinesweeperWebApp.Services.Business
{
    /*
     * This class is a business service for the GameStorageModel object model
    */
    public class GameService
    {
        /*
         * this method saves a game 
         */
        public bool Save(GameStorageModel storageModel)
        {
            // assume the game will not be saved
            bool success = false;

            // databse connection string
            String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // connect to databse in business service to support ACID transactions
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // instantiate DAO
                GameDAO dao = new GameDAO(connection);

                // open databse connection
               connection.Open();

                // pass control to DAO to delete user previous save
               dao.Delete(storageModel.User);

                // pass control to DAO to save the current game and catch return value
               success = dao.Create(storageModel);

                // close database connection
               connection.Close();
            }

            // return result of save attempt 
            return success;
        }

        /*
         * this method loads a user's saved game
         */
        public GameStorageModel Load(string user)
        {
            // database conection string
            String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // prepare list of storage models to hold DAO method return value
            List<GameStorageModel> results = new List<GameStorageModel>();

            // connect to database in business service
            // this design supports ACID transactions and is a best practice
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // instantiate DAO
                GameDAO dao = new GameDAO(connection);

                // open connection to database
                connection.Open();

                // pass control to DAO to read the user's saved game and catch return value with list
                results = dao.Read(user);

                // close databse connection
                connection.Close();
                
                // if the save wasn't found
                if (results.Count != 1)
                {
                    return null;
                }

                // othwerwise return the loaded game
                else
                {
                    // transform list to array to acces the game 
                    return results.ToArray()[0];
                }
                
            }
        }

       


        // lay the mines
        public GameBundle SetupLiveNeighbors(GameBundle bundle)
        {
            int rand = -1;
            // create random object
            Random random = new Random();
            // randomely determine if each Cell is live
            for (int i = 0; i < bundle.Board.Size; i++)
            {
                for (int j = 0; j < bundle.Board.Size; j++)
                {
                    // generate random number between 1 and 100
                    rand = random.Next(1, 100);
                    // if the ranodm number is less than or equal to the difficulty the Cell is live
                    if (rand <= bundle.Difficulty * 5)
                    {
                        bundle.Board.Grid[i, j].Live = true;
                    }
                }
            }

            return bundle;
        }

        // determine how many of a Cell's neighbors are live
        public void CalculateLiveNeighbors(GameBundle bundle)
        {
            int liveNeighborCount;

            // iterate once for every Cell use i and j to keep track of which cell is being examined
            for (int i = 0; i < bundle.Board.Size; i++)
            {
                for (int j = 0; j < bundle.Board.Size; j++)
                {
                    // reset liveNeighborCount to zero for each Cell
                    liveNeighborCount = 0;

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row + 1, bundle.Board.Grid[i, j].Column + 1, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row + 1, bundle.Board.Grid[i, j].Column + 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row + 1, bundle.Board.Grid[i, j].Column, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row + 1, bundle.Board.Grid[i, j].Column].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row + 1, bundle.Board.Grid[i, j].Column - 1, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row + 1, bundle.Board.Grid[i, j].Column - 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row, bundle.Board.Grid[i, j].Column + 1, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row, bundle.Board.Grid[i, j].Column + 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row, bundle.Board.Grid[i, j].Column, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row, bundle.Board.Grid[i, j].Column].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row, bundle.Board.Grid[i, j].Column - 1, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row, bundle.Board.Grid[i, j].Column - 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row - 1, bundle.Board.Grid[i, j].Column + 1, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row - 1, bundle.Board.Grid[i, j].Column + 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row - 1, bundle.Board.Grid[i, j].Column, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row - 1, bundle.Board.Grid[i, j].Column].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(bundle.Board.Grid[i, j].Row - 1, bundle.Board.Grid[i, j].Column - 1, bundle))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (bundle.Board.Grid[bundle.Board.Grid[i, j].Row - 1, bundle.Board.Grid[i, j].Column - 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }
                    bundle.Board.Grid[i, j].liveNeighbors = liveNeighborCount;
                }
            }

        }

        // determines what happens when a cell is selected by the user
        // a return value of -1 indicates invalid arguments
        // a return value of 0 indicates the given cell is not a bomb and the game may continue
        // a return value of 1 indicates a bomb
        // a return value of 2 indicates the player has won
        public int Update(GameBundle bundle)
        {
            if (bundle.Board.gameOver == false)
            {
                if (isValid(bundle.Row, bundle.Column, bundle))
                {
                    //  mark appropriate cells as visited
                    floodFill(bundle.Row, bundle.Column, bundle);

                    // the cell is a bomb
                    if (bundle.Board.Grid[bundle.Row, bundle.Column].Live == true)
                    {
                        // game over
                        bundle.Board.gameOver = true;
                        // return update result
                        return 1;
                    }

                    else
                    {
                        // the cell is not a bomb the game may continue
                        if (hasUnvistedSafeCells(bundle))
                            // return update result
                            return 0;

                        // the player wins
                        else
                        {
                            bundle.Board.gameOver = true;
                            // return update result
                            return 2;
                        }
                    }
                }
            }

            // an error has occured either invalid Grid coordinates or the game is already over
            return -1;
        }

        // determines if there are unvisted safe cells in the grid
        public bool hasUnvistedSafeCells(GameBundle bundle)
        {
            for (int i = 0; i < bundle.Board.Size; i++)
            {
                for (int j = 0; j < bundle.Board.Size; j++)
                {
                    // any cell that is neither live or visited means the game goes on
                    if (bundle.Board.Grid[i, j].Visited == false && bundle.Board.Grid[i, j].Live == false)
                    {
                        return true;
                    }
                }
            }
            // there are no unvisited safe cells the game is over
            return false;
        }

        // safety method to prevent crashes
        private bool isValid(int row, int col, GameBundle bundle)
        {
            bool validRow = false;
            bool validColumn = false;

            // determine if given row is valid
            if (row >= 0 && (row < bundle.Board.Size))
                validRow = true;
            // determine if given column is valid
            if (col >= 0 && col < bundle.Board.Size)
                validColumn = true;

            // if both the given row and the given column are valid return true otherwise return false
            return validRow && validColumn;
        }

        // recursive method used to mark cells as visited
        public void floodFill(int row, int col, GameBundle bundle)
        {
            // mark the current cell as visited
            bundle.Board.Grid[row, col].Visited = true;

            // unflag visited cells
            bundle.Board.Grid[row, col].isFlagged = false;

            // if there are no live neighbors call floodFill on all eight neighbors
            if (bundle.Board.Grid[row, col].liveNeighbors == 0)
            {
                if (isValid(row + 1, col, bundle))
                {
                    if (bundle.Board.Grid[row + 1, col].Visited == false)
                        floodFill(row + 1, col, bundle);
                }

                if (isValid(row - 1, col, bundle))
                {
                    if (bundle.Board.Grid[row - 1, col].Visited == false)
                        floodFill(row - 1, col, bundle);
                }

                if (isValid(row, col + 1, bundle))
                {
                    if (bundle.Board.Grid[row, col + 1].Visited == false)
                        floodFill(row, col + 1, bundle);
                }

                if (isValid(row, col - 1, bundle))
                {
                    if (bundle.Board.Grid[row, col - 1].Visited == false)
                        floodFill(row, col - 1, bundle);
                }

                if (isValid(row + 1, col + 1, bundle))
                {
                    if (bundle.Board.Grid[row + 1, col + 1].Visited == false)
                        floodFill(row + 1, col + 1, bundle);
                }

                if (isValid(row + 1, col - 1, bundle))
                {
                    if (bundle.Board.Grid[row + 1, col - 1].Visited == false)
                        floodFill(row + 1, col - 1, bundle);
                }

                if (isValid(row - 1, col + 1, bundle))
                {
                    if (bundle.Board.Grid[row - 1, col + 1].Visited == false)
                        floodFill(row - 1, col + 1, bundle);
                }

                if (isValid(row - 1, col - 1, bundle))
                {
                    if (bundle.Board.Grid[row - 1, col - 1].Visited == false)
                        floodFill(row - 1, col - 1, bundle);
                }
            }
        }
    }
}