namespace PathsBetweenCellInMatrix
{
    using System;

    class Cell : IComparable<Cell>
    {
        private int x;
        private int y;
        private char value;

        public Cell(int x, int y, char value=' ')
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }

        public char Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public int CompareTo(Cell other)
        {
            var comparator = this.X.CompareTo(other.X);
            if (comparator == 0)
            {
                comparator = this.Y.CompareTo(other.Y);
            }

            return comparator;
        }

        public override string ToString()
        {
            return $"Value: {this.Value}, X: {this.X}, Y: {this.Y}";
        }
    }

    class Matrix
    {
        private int width;
        private int height;
        private Cell[,] board;

        public Matrix(int height, int width)
        {
            this.Width = width;
            this.Height = height;
            this.board = new Cell[height, width];
            this.PopulateBoard();
        }

        private void PopulateBoard()
        {
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    this.Board[row, col] = new Cell(row, col, ' ');
                }
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public Cell[,] Board
        {
            get
            {
                return this.board;
            }
            set
            {
                this.board = value;
            }
        }

        public void AddElementToTheBoard(int row, int col, char element)
        {
            if (row >= 0 && row < this.Height 
                && col >=  0 && col < this.Width)
            {
                this.board[row, col].Value = element;
            }
        }

        public bool IsCoordinatesArePosibleToReach(Cell cell)
        {
            if (cell.X >= 0 && cell.X < this.Height
                && cell.Y >= 0 && cell.Y < this.Width
                && this.board[cell.X, cell.Y].Value != '*')
            {
                return true;
            }

            return false;
        }
    }
}
