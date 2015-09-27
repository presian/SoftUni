namespace NestedLoopToRecursion
{
    using System;

    class NestedLoopToRecursionMain
    {
        private static int[] loops;
        private static int size;

        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            size = n;
            loops = new int[size];

            MakeLoops(0);
        }

        private static void MakeLoops(int startIndex)
        {
            if (startIndex == size)
            {
                PrintResult();
                return;
            }

            for (int i = 0; i < size; i++)
            {
                loops[startIndex] = i + 1;
                MakeLoops(startIndex + 1);
            }
        }

        private static void PrintResult()
        {
            Console.WriteLine(string.Join(" ", loops));
        }
    }
}
