﻿using System;

namespace Minesweeper_350.Models
{
    [Serializable()]
    public class BoardModel
    {

        public string BoardId { get; set; }
        public int Size { get; set; }
        public CellModel[,] Grid { get; set; }
        public int Difficulty { get; set; }

        public BoardModel()
        {
            Size = 16;
            Difficulty = 20; 
            Grid = new CellModel[Size, Size];
            SetupLiveNeighbors();
        }
        public BoardModel(int difficulty, int size = 16)
        {
            Size = size;
            Difficulty = 10 + (40 * (difficulty / 10));

            Grid = new CellModel[Size, Size];
            SetupLiveNeighbors();
        }

        public void SetupLiveNeighbors()
        {
            int squareSpace = (int)Math.Pow(Size, 2);
            int totalAllowedBombs = (int)Math.Ceiling((decimal)squareSpace * ((decimal)Difficulty / 100));

            // Create 2D array 
            Random rand = new Random();
            bool[] liveCells = new bool[squareSpace];

            // Setup and populate sorting set,
            Double[] sortOrder = new Double[squareSpace];
            for (int idx = 0; idx < sortOrder.Length; idx++)
                sortOrder[idx] = rand.NextDouble();
            for (int idx = 0; idx < squareSpace; idx++)
            {
                // guarantee that all allowed bombs are set
                liveCells[idx] = idx < totalAllowedBombs;
            }

            // Randomize the liveCells placement through sortOrder
            Array.Sort(sortOrder, liveCells);

            // Now iter through 2D array and initialize all cells
            int liveCellSeedIdx = 0;
            for (int row = 0; row < Grid.GetLength(0); row++)
            {
                for (int col = 0; col < Grid.GetLength(1); col++)
                {
                    Grid[row, col] = new CellModel(col, row, false, liveCells[liveCellSeedIdx], 0);
                    liveCellSeedIdx++;
                }
            }


            CalculateLiveNeighbors();
        }

        public void CalculateLiveNeighbors()
        {
            for (int row = 0; row < Grid.GetLength(0); row++)
            {
                for (int col = 0; col < Grid.GetLength(1); col++)
                {
                    CalculateLiveCellNeighbors(Grid[row, col]);
                }
            }
        }

        private void CalculateLiveCellNeighbors(CellModel c)
        {
            // Set to 0 automatically
            if (c.Live)
            {
                c.LiveNeighbors = 9;
                return;
            }

            CellModel def = new CellModel(0, 0, false, false, 0);
            CellModel left = (c.Column - 1 >= 0) ? Grid[c.Row, c.Column - 1] : def;
            CellModel right = (c.Column + 1 < Size) ? Grid[c.Row, c.Column + 1] : def;
            CellModel top = (c.Row - 1 >= 0) ? Grid[c.Row - 1, c.Column] : def;
            CellModel bottom = (c.Row + 1 < Size) ? Grid[c.Row + 1, c.Column] : def;
            CellModel topRight = (c.Row - 1 >= 0 && c.Column + 1 < Size) ? Grid[c.Row - 1, c.Column + 1] : def;
            CellModel topLeft = (c.Row - 1 >= 0 && c.Column - 1 >= 0) ? Grid[c.Row - 1, c.Column - 1] : def;
            CellModel bottomRight = (c.Row + 1 < Size && c.Column + 1 < Size) ? Grid[c.Row + 1, c.Column + 1] : def;
            CellModel bottomLeft = (c.Row + 1 < Size && c.Column - 1 >= 0) ? Grid[c.Row + 1, c.Column - 1] : def;

            int liveNeighbors = 0;
            foreach (CellModel neighbor in new CellModel[] { left, right, top, bottom, topRight, topLeft, bottomRight, bottomLeft })
            {
                liveNeighbors += neighbor.Live ? 1 : 0;
            }
            c.LiveNeighbors = liveNeighbors;
        }

        private bool isSafeCell(int r, int c)
        {
            return (r >= 0 && r < Size) && (c >= 0 && c < Size) && !Grid[r, c].Live && !Grid[r, c].Visited;
        }

        public void floodFill(int r, int c)
        {
            if (!Grid[r, c].Visited && isSafeCell(r, c))
            {
                // Mark grid element as visited
                Grid[r, c].Visited = true;
                // Recursively check all compass directions
                if (isSafeCell(r - 1, c))
                {
                    if (Grid[r - 1, c].LiveNeighbors == 0) floodFill(r - 1, c); // N
                    else Grid[r - 1, c].Visited = true; 
                }
                if (isSafeCell(r, c + 1))
                {
                    if (Grid[r, c + 1].LiveNeighbors == 0) floodFill(r, c + 1); // E
                    else Grid[r, c + 1].Visited = true;
                }
                if (isSafeCell(r + 1, c))
                {
                    if (Grid[r + 1, c].LiveNeighbors == 0) floodFill(r + 1, c); // S
                    else Grid[r + 1, c].Visited = true;
                }
                if (isSafeCell(r, c - 1))
                {
                    if (Grid[r, c - 1].LiveNeighbors == 0) floodFill(r, c - 1); // W
                    else Grid[r, c - 1].Visited = true;
                }
            }
            return;
        }

        public bool AllSafeTilesVisited()
        {
            int rows = Grid.GetLength(0);
            int cols = Grid.GetLength(1);
            bool someUnvisited = false;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (!Grid[row, col].Visited && !Grid[row, col].Live) someUnvisited = true;
                }
            }
            return !someUnvisited;
        }
    }
}