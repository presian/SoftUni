namespace ConnectedAreasInMatrix
{
    using System;
    using System.Collections.Generic;

    public class Area : IComparable<Area>
    {
        private Cell startCell;
        private SortedSet<Cell> cells;  

        public Area(Cell startCell)
        {
            this.StartCell = startCell;
            this.cells = new SortedSet<Cell>();
            this.AddCellToArea(startCell);
        }

        public int Size => this.cells.Count;

        public Cell StartCell
        {
            get
            {
                return this.startCell;
            }
            set
            {
                this.startCell = value;
            }
        }

        public void AddCellToArea(Cell cell)
        {
            this.cells.Add(cell);
        }

        public int CompareTo(Area other)
        {
            var comparator = this.Size.CompareTo(other.Size) * -1;
            if (comparator == 0)
            {
                comparator = this.StartCell.X.CompareTo(other.StartCell.X);
            }

            if (comparator == 0)
            {
                comparator = this.StartCell.Y.CompareTo(other.StartCell.Y);
            }

            return comparator;
        }
    }
}