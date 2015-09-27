namespace CombinationWhitoutRepetition
{
    using System;

    class CombinationWhitoutRepetitionMain
    {
        private static int[] combinations;
        private static int combinationLength;
        private static int combinationsCount;

        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            combinationLength = k;
            combinationsCount = n;
            combinations = new int[combinationLength];

            MakeCombinations(0);
        }

        private static void MakeCombinations(int startIndex)
        {
            if (startIndex == combinationLength)
            {
                PrintResult();
                return;
            }

            for (int i = 0; i < combinationsCount; i++)
            {
                if (startIndex > 0 && i + 1 > combinations[startIndex - 1])
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
