namespace PathsBetweenCellInMatrix
{
    using System;
    using System.Collections.Generic;

    class PathsBetweenCellInMatrixMain
    {
        static bool[,] visitedMatrixCells;
        private static Matrix matrix; 
        private static Cell startCell = null;
        private static Cell endCell = null;
        static IList<char> currentPath = new List<char>(); 

        static void Main()
        {   
            // First layout
            int matrixWidth = 4;
            int matrixHeight = 5;

            // Second layout
//            int matrixWidth = 6;
//            int matrixHeight = 5;
            matrix = new Matrix(matrixHeight, matrixWidth);
            visitedMatrixCells = new bool[matrixHeight, matrixWidth];

            // Firs layout
            matrix.AddElementToTheBoard(0, 0, 'S');
            matrix.AddElementToTheBoard(1, 1, '*');
            matrix.AddElementToTheBoard(1, 2, '*');
            matrix.AddElementToTheBoard(2, 1, '*');
            matrix.AddElementToTheBoard(2, 2, '*');
            matrix.AddElementToTheBoard(3, 1, '*');
            matrix.AddElementToTheBoard(3, 2, 'E');
            startCell = new Cell(0, 0, 'S');
            endCell = new Cell(3, 2, 'E');

            // Second layout
//            matrix.AddElementToTheBoard(0, 0, 'S');
//            matrix.AddElementToTheBoard(1, 1, '*');
//            matrix.AddElementToTheBoard(1, 2, '*');
//            matrix.AddElementToTheBoard(1, 4, '*');
//            matrix.AddElementToTheBoard(2, 1, '*');
//            matrix.AddElementToTheBoard(2, 2, '*');
//            matrix.AddElementToTheBoard(2, 4, '*');
//            matrix.AddElementToTheBoard(3, 1, '*');
//            matrix.AddElementToTheBoard(3, 2, 'E');
//            matrix.AddElementToTheBoard(4, 3, '*');
//            startCell = new Cell(0, 0, 'S');
//            endCell = new Cell(3, 2, 'E');

            FindPath(startCell);
        }

        private static void FindPath(Cell currentCell)
        {
            if (!matrix.IsCoordinatesArePosibleToReach(currentCell)
                || visitedMatrixCells[currentCell.X, currentCell.Y] == true)
            {
                return;
            }

            if (currentCell.Value != 'S')
            {
                currentPath.Add(currentCell.Value);
            }
            
            visitedMatrixCells[currentCell.X, currentCell.Y] = true;

            if (currentCell.CompareTo(endCell) == 0)
            {
                PrintPath();
            }
            else
            {
                var upCell = new Cell(currentCell.X - 1, currentCell.Y, 'U');
                FindPath(upCell);

                var downCell = new Cell(currentCell.X + 1, currentCell.Y, 'D');
                FindPath(downCell);

                var leftCell = new Cell(currentCell.X, currentCell.Y + 1, 'R');
                FindPath(leftCell);

                var rightCell = new Cell(currentCell.X, currentCell.Y - 1, 'L');
                FindPath(rightCell);
            }
            if (currentCell.Value != 'S')
            {
                currentPath.RemoveAt(currentPath.Count - 1);
            }
            
            visitedMatrixCells[currentCell.X, currentCell.Y] = false;
        }

        private static void PrintPath()
        {
            Console.WriteLine(string.Join("", currentPath));
        }
    }
}
