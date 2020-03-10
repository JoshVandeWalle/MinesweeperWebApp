using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperWebApp.Models
{
    public class BoardModel
    {
        // the Minesweeper board
        public CellModel[,] Grid;

        // dimensions for square Grid
        public int Size;

        // game difficulty is the percentage liklihood that a given Cell will be live
        private int MINE_LIKLIHOOD = 10;

        public bool gameOver { get; set; }

        public BoardModel(int size)
        {
            // set Size and initialize Grid
            Size = size;
            Grid = new CellModel[size, size];

            // game is not over
            gameOver = false;

            // initalize the Cells
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new CellModel();
                    Grid[i, j].Row = i;
                    Grid[i, j].Column = j;

                }
            }
        }

        // lay the mines
        public void setupLiveNeighbors()
        {
            int rand = -1;
            // create random object
            Random random = new Random();
            // randomely determine if each Cell is live
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // generate random number between 1 and 100
                    rand = random.Next(1, 100);
                    // if the ranodm number is less than or equal to the difficulty the Cell is live
                    if (rand <= MINE_LIKLIHOOD)
                    {
                        Grid[i, j].Live = true;
                    }
                }
            }
        }

        // determine how many of a Cell's neighbors are live
        public void calculateLiveNeighbors()
        {
            int liveNeighborCount;

            // iterate once for every Cell use i and j to keep track of which cell is being examined
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // reset liveNeighborCount to zero for each Cell
                    liveNeighborCount = 0;

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row + 1, Grid[i, j].Column + 1))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row + 1, Grid[i, j].Column + 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row + 1, Grid[i, j].Column))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row + 1, Grid[i, j].Column].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row + 1, Grid[i, j].Column - 1))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row + 1, Grid[i, j].Column - 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row, Grid[i, j].Column + 1))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row, Grid[i, j].Column + 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row, Grid[i, j].Column))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row, Grid[i, j].Column].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row, Grid[i, j].Column - 1))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row, Grid[i, j].Column - 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row - 1, Grid[i, j].Column + 1))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row - 1, Grid[i, j].Column + 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row - 1, Grid[i, j].Column))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row - 1, Grid[i, j].Column].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }

                    // check if neighbor is a valid cell location
                    if (isValid(Grid[i, j].Row - 1, Grid[i, j].Column - 1))
                    {
                        // increment liveNeiborCount if the neighbor is live
                        if (Grid[Grid[i, j].Row - 1, Grid[i, j].Column - 1].Live == true)
                        {
                            liveNeighborCount++;
                        }
                    }
                    Grid[i, j].liveNeighbors = liveNeighborCount;
                }
            }

        }

        // safety method to prevent crashes
        public bool isValid(int row, int col)
        {
            bool validRow = false;
            bool validColumn = false;

            // determine if given row is valid
            if (row >= 0 && row < Size)
                validRow = true;
            // determine if given column is valid
            if (col >= 0 && col < Size)
                validColumn = true;

            // if both the given row and the given column are valid return true otherwise return false
            return validRow && validColumn;
        }

        // determines what happens when a cell is selected by the user
        // a return value of -1 indicates invalid arguments
        // a return value of 0 indicates the given cell is not a bomb and the game may continue
        // a return value of 1 indicates a bomb
        // a return value of 2 indicates the player has won
        public int Update(int rowNumber, int columnNumber)
        {
            if (gameOver == false)
            {
                if (isValid(rowNumber, columnNumber))
                {
                    //  mark appropriate cells as visited
                    floodFill(rowNumber, columnNumber);

                    // the cell is a bomb
                    if (Grid[rowNumber, columnNumber].Live == true)
                    {
                        // game over
                        gameOver = true;
                        // return update result
                        return 1;
                    }

                    else
                    {
                        // the cell is not a bomb the game may continue
                        if (hasUnvistedSafeCells())
                            // return update result
                            return 0;

                        // the player wins
                        else
                        {
                            gameOver = true;
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
        public bool hasUnvistedSafeCells()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // any cell that is neither live or visited means the game goes on
                    if (Grid[i, j].Visited == false && Grid[i, j].Live == false)
                    {
                        return true;
                    }
                }
            }
            // there are no unvisited safe cells the game is over
            return false;
        }

        // recursive method used to mark cells as visited
        public void floodFill(int row, int col)
        {
            // mark the current cell as visited
            Grid[row, col].Visited = true;

            // unflag visited cells
            Grid[row, col].isFlagged = false;

            // if there are no live neighbors call floodFill on all eight neighbors
            if (Grid[row, col].liveNeighbors == 0)
            {
                if (isValid(row + 1, col) && Grid[row + 1, col].Visited == false)
                {
                    floodFill(row + 1, col);
                }

                if (isValid(row - 1, col) && Grid[row - 1, col].Visited == false)
                {
                    floodFill(row - 1, col);
                }

                if (isValid(row, col + 1) && Grid[row, col + 1].Visited == false)
                {
                    floodFill(row, col + 1);
                }

                if (isValid(row, col - 1) && Grid[row, col - 1].Visited == false)
                {
                    floodFill(row, col - 1);
                }

                if (isValid(row + 1, col + 1) && Grid[row + 1, col + 1].Visited == false)
                {
                    floodFill(row + 1, col + 1);
                }

                if (isValid(row + 1, col - 1) && Grid[row + 1, col - 1].Visited == false)
                {
                    floodFill(row + 1, col - 1);
                }

                if (isValid(row - 1, col + 1) && Grid[row - 1, col + 1].Visited == false)
                {
                    floodFill(row - 1, col + 1);
                }

                if (isValid(row - 1, col - 1) && Grid[row - 1, col - 1].Visited == false)
                {
                    floodFill(row - 1, col - 1);
                }
            }
        }

        // set a Cell's flag status
        public void Flag(int row, int col, bool flagStatus)
        {
            if (isValid(row, col))
                Grid[row, col].isFlagged = flagStatus;
        }

    }
}