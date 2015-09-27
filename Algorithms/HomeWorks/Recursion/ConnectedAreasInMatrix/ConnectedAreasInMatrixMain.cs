namespace ConnectedAreasInMatrix
{
    using System;
    using System.Collections.Generic;

    class ConnectedAreasInMatrixMain
    {
        public const char WallSymbol = '*';

        static void Main()
        {
            

            var walls = new List<Cell>();

            // first layout
            var width = 9;
            var height = 4;
            walls.Add(new Cell(0, 3, WallSymbol));
            walls.Add(new Cell(1, 3, WallSymbol));
            walls.Add(new Cell(2, 3, WallSymbol));
            walls.Add(new Cell(3, 4, WallSymbol));
            walls.Add(new Cell(3, 6, WallSymbol));
            walls.Add(new Cell(2, 7, WallSymbol));
            walls.Add(new Cell(1, 7, WallSymbol));
            walls.Add(new Cell(0, 7, WallSymbol));

            // second layout
//            var width = 10;
//            var height = 5;
//            walls.Add(new Cell(0, 0, WallSymbol));
//            walls.Add(new Cell(1, 0, WallSymbol));
//            walls.Add(new Cell(2, 0, WallSymbol));
//            walls.Add(new Cell(3, 0, WallSymbol));
//            walls.Add(new Cell(4, 0, WallSymbol));
//            walls.Add(new Cell(0, 3, WallSymbol));
//            walls.Add(new Cell(1, 3, WallSymbol));
//            walls.Add(new Cell(2, 3, WallSymbol));
//            walls.Add(new Cell(3, 3, WallSymbol));
//            walls.Add(new Cell(4, 3, WallSymbol));
//            walls.Add(new Cell(0, 7, WallSymbol));
//            walls.Add(new Cell(1, 7, WallSymbol));
//            walls.Add(new Cell(2, 7, WallSymbol));
//            walls.Add(new Cell(3, 7, WallSymbol));
//            walls.Add(new Cell(4, 7, WallSymbol));
//            walls.Add(new Cell(2, 4, WallSymbol));
//            walls.Add(new Cell(2, 5, WallSymbol));
//            walls.Add(new Cell(2, 6, WallSymbol));
            var matrix = new Matrix(width, height, walls);
            matrix.PrintAreas();
        }
    }
}
