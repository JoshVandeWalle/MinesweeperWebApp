using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperWebApp.Models
{
    // game Cell
    public class CellModel
    {
        // Cell row number
        public int Row { get; set; }
        // Cell column number
        public int Column { get; set; }
        // used to determine if the Cell has been visted yet
        public bool Visited { get; set; }
        // the number of neighbor Cells that are live
        public int liveNeighbors { get; set; }
        // used to determine if the Cell is live
        public bool Live { get; set; }
        // used to determine if the cell is flagged
        public bool isFlagged { get; set; }

        // default constructor initializes Cell fields 
        public CellModel()
        {
            Row = -1;
            Column = -1;
            Visited = false;
            liveNeighbors = 0;
            Live = false;
            isFlagged = false;
        }


    }
}