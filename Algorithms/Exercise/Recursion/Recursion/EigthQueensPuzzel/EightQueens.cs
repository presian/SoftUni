namespace EigthQuinsPuzzel
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    class EightQueens
    {
        private const int Size = 8;
        private bool [,] chessboard = new bool[Size, Size];
        private int foundSolutions = 0;

        private HashSet<int> attackedRows = new HashSet<int>();
        private HashSet<int> attackedColumns = new HashSet<int>();
        private HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        private HashSet<int> attackedRightDiagonals = new HashSet<int>();

        public void PutQueens(int row)
        {
            if (row == Size)
            {
                this.PrintSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (this.CanPutTheQuin(row, col))
                    {
                        this.MarkAllAttackedPositions(row, col);
                        this.PutQueens(row + 1);
                        this.UnmarkAllAttackedPositions(row, col);
                    }
                }
            }
        }

        public int FoundSolutions => this.foundSolutions;

        private void UnmarkAllAttackedPositions(int row, int col)
        {
            this.attackedRows.Remove(row);
            this.attackedColumns.Remove(col);
            this.attackedLeftDiagonals.Remove(col - row);
            this.attackedRightDiagonals.Remove(row + col);
            this.chessboard[row, col] = false;
        }

        private void MarkAllAttackedPositions(int row, int col)
        {
            this.attackedRows.Add(row);
            this.attackedColumns.Add(col);
            this.attackedLeftDiagonals.Add(col - row);
            this.attackedRightDiagonals.Add(row + col);
            this.chessboard[row, col] = true;
        }

        private bool CanPutTheQuin(int row, int col)
        {
            if (this.attackedRows.Contains(row) 
                || this.attackedColumns.Contains(col)
                || this.attackedLeftDiagonals.Contains(col - row)
                || this.attackedRightDiagonals.Contains(row + col))
            {
                return false;
            }

            return true;
        }

        private void PrintSolution()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (this.chessboard[row, col] == true)
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write('-');   
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            this.foundSolutions++;
        }
    }
}
