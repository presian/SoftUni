namespace ConnectedAreasInMatrix
{
    using System;
    using System.Collections.Generic;

    class Matrix
    {
        private int width;
        private int height;
        private Cell[,] board;
        private bool[,] visitedOnTraversal;
        private int nonVisitedCellsCount;
        private SortedSet<Area> areas; 

        public Matrix(int width, int height, List<Cell>walls = null)
        {
            this.width = width;
            this.height = height;
            this.board = new Cell[this.Height, this.Width];
            this.areas = new SortedSet<Area>();
            this.visitedOnTraversal = new bool[this. Height, this.Width];
            this.nonVisitedCellsCount = this.Height*this.Width;
            this.PopulateMatrix(walls);
            this.FindAllAreas();
        }

        private void PopulateMatrix(List<Cell> walls)
        {
            if (walls != null)
            {
                foreach (var wall in walls)
                {
                    this.board[wall.X, wall.Y] = wall;
                    this.MakeCellVisited(wall);
                }
            }

            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    if (this.board[row, col] == null)
                    {
                        this.board[row, col] = new Cell(row, col);
                    }
                }
            }
        }

        private void MakeCellVisited(Cell visited)
        {
            this.visitedOnTraversal[visited.X, visited.Y] = true;
            this.nonVisitedCellsCount--;
        }

        public Cell GetCell(Cell cell)
        {
            return this.GetCell(cell.X, cell.Y);
        }

        public Cell GetCell(int row, int col)
        {
            if (this.IsCurrentCellTraversable(row, col))
            {
                return this.board[row, col];
            }

            return null;
        }

        public bool IsCurrentCellTraversable(Cell cell)
        {
            return this.IsCurrentCellTraversable(cell.X, cell.Y);
        }

        public bool IsCurrentCellTraversable(int row, int col)
        {
            if (row >= 0 && row < this.Height
                && col >= 0 && col < this.Width
                && this.board[row, col].Value != '*'
                && this.visitedOnTraversal[row, col] == false)
            {
                return true;
            }

            return false;
        }

        public int Width => this.width;

        public int Height => this.height;

        public void PrintAreas()
        {
            Console.WriteLine($"Total areas found: {this.areas.Count}");
            var index = 1;
            foreach (var area in this.areas)
            {
                Console.WriteLine($"Area #{index} at {area.StartCell}, size: {area.Size}");
                index++;
            }
        }

        private void FindAllAreas()
        {
            while (this.nonVisitedCellsCount > 0)
            {
                var firstTraversableCell = this.FindFirstTraversalCell();
                this.areas.Add(this.FindAreaFromPoint(firstTraversableCell));
            }
        }

        private Area FindAreaFromPoint(Cell startCell, Area currentArea = null)
        {
            if (currentArea == null)
            {
                currentArea = new Area(startCell);
            }

            if (!this.IsCurrentCellTraversable(startCell))
            {
                return currentArea;
            }
            else
            {
                this.MakeCellVisited(startCell);
                currentArea.AddCellToArea(startCell);
                var upCell = new Cell(startCell.X - 1, startCell.Y);
                currentArea = this.FindAreaFromPoint(upCell, currentArea);

                var downCell = new Cell(startCell.X + 1, startCell.Y);
                currentArea = this.FindAreaFromPoint(downCell, currentArea);

                var rightCell = new Cell(startCell.X, startCell.Y - 1);
                currentArea = this.FindAreaFromPoint(rightCell, currentArea);

                var leftCell = new Cell(startCell.X, startCell.Y + 1);
                currentArea = this.FindAreaFromPoint(leftCell, currentArea);
            }

            return currentArea;
        }

        private Cell FindFirstTraversalCell()
        {
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    var cell = this.GetCell(row, col);
                    if (cell != null)
                    {
                        if (cell.Value != '*')
                        {
                            return cell;
                        }
                    }
                }
            }

            return null;
        }
    }
}
