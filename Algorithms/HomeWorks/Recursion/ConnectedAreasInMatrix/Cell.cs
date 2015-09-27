namespace ConnectedAreasInMatrix
{
    using System;

    public class Cell : IComparable<Cell>
    {
        private int x;
        private int y;
        private char value;

        public Cell(int x, int y, char value = ' ')
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
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
            return $"({this.X}, {this.Y})";
        }
    }
}