namespace EigthQueensPuzzel
{
    using System;
    using EigthQuinsPuzzel;

    class EigthQueensPuzzelMain
    {
        static void Main()
        {
            var puzzel = new EightQueens();
            puzzel.PutQueens(0);
            Console.WriteLine(puzzel.FoundSolutions);
        }
    }
}
