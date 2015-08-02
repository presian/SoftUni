namespace RideTheHorse
{
    using System;
    using System.Collections.Generic;

    class Cell
    {
        public Cell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.Value = 0;
        }

        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }

    static class RideTheHorseMain
    {
        static void Main()
        {
            var rowCount = int.Parse(Console.ReadLine());
            var columnCount = int.Parse(Console.ReadLine());
            var startRow = int.Parse(Console.ReadLine());
            var startColumn = int.Parse(Console.ReadLine());

            var matrix = new Cell[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    matrix[i, j] = new Cell(i, j);
                }
            }
            
            var startCell = matrix[startRow, startColumn];
            startCell.Value = 1;
            var resultMatrix = BFS(matrix, startCell, rowCount, columnCount);


            Console.WriteLine("============================================");
            Console.WriteLine("                 MATRIX:");
            Console.WriteLine("============================================");
            PrintMatrix(resultMatrix, rowCount, columnCount);
            Console.WriteLine("============================================");
            Console.WriteLine("                 RESULT:");
            Console.WriteLine("============================================");
            for (int row = 0; row < rowCount; row++)
            {
                Console.WriteLine(matrix[row, columnCount/2].Value);
            }
        }

        static Cell[,] BFS(Cell[,] matrix, Cell startCell, int rowCount, int columnCount)
        {
            var visited = new bool[rowCount, columnCount];
            var queue = new Queue<Cell>();
            queue.Enqueue(startCell);
            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();
                visited[currentCell.Row, currentCell.Column] = true;
                MakeHoresMoves(ref matrix, ref queue, ref visited,currentCell, rowCount, columnCount);
            }

            return matrix;
        }

        private static bool IsInMatrix(int row, int col, int matrixHeight, int matrixWidth)
        {
            if (row >= 0 && row < matrixHeight && col >= 0 && col < matrixWidth)
            {
                return true;
            }

            return false;
        }

        // It's little ugly but works
        private static void MakeHoresMoves(
            ref Cell[,] matrix,
            ref Queue<Cell> queue,
            ref bool [,] visited,
            Cell currentCell, 
            int rowCount, 
            int columnCount)
        {
            var coords = MakeCoordsForCell(currentCell);
            foreach (var coord in coords)
            {
                if (IsInMatrix(coord[0], coord[1], rowCount, columnCount))
                {
                    var cell = matrix[coord[0], coord[1]];
                    if (visited[cell.Row, cell.Column] == false)
                    {
                        cell.Value = currentCell.Value+1;
                        queue.Enqueue(cell);
                    }
                }
            }
        }

        private static List<int[]> MakeCoordsForCell(Cell currentCell)
        {
            var list = new List<int[]>
            {
                new int[2] {currentCell.Row - 2, currentCell.Column - 1},
                new int[2] {currentCell.Row - 2, currentCell.Column + 1},
                new int[2] {currentCell.Row + 2, currentCell.Column - 1},
                new int[2] {currentCell.Row + 2, currentCell.Column + 1},
                new int[2] {currentCell.Row - 1, currentCell.Column - 2},
                new int[2] {currentCell.Row - 1, currentCell.Column + 2},
                new int[2] {currentCell.Row + 1, currentCell.Column - 2},
                new int[2] {currentCell.Row + 1, currentCell.Column + 2}
            };
            return list;
        }

        static void PrintMatrix(Cell[,] matrix, int rowCount, int columnCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (j > 0)
                    {
                        Console.Write(string.Format(", {0}", matrix[i, j].Value));
                    }
                    else
                    {
                        Console.Write( matrix[i, j].Value);
                    }
                    
                }

                Console.WriteLine();
            }
        }
    }
}
