namespace CombinationWithRepetition
{
    using System;

    class CombinationWithRepetitionMain
    {
        private static int[] combinations;
        private static int size = 0;
        private static int loopSize = 0;

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            size = k;
            loopSize = n;

            combinations = new int[size];

            MakeCombinations(0);
        }

        private static void MakeCombinations(int startIndex)
        {
            if (startIndex == size)
            {
                PrintResult();
                return;
            }

            for (int i = 0; i < loopSize; i++)
            {
                if (startIndex > 0 && i + 1 >= combinations[startIndex - 1])
                {
                    combinations[startIndex] = i + 1;
                    MakeCombinations(startIndex + 1);
                }
                else if (startIndex == 0)
                {
                    combinations[startIndex] = i + 1;
                    MakeCombinations(startIndex + 1);
                }
                
            }
        }

        private static void PrintResult()
        {
            Console.WriteLine(string.Join(" ", combinations));
        }
    }
}
