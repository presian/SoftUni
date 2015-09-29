namespace Permutations
{
    using System;
    using System.Linq;

    class PermutationsMain
    {
        static int totalPermutationsCount = 0;

        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var array = Enumerable.Range(1, n).ToArray();
            var used = new bool[n];


            MakePermutation(array, used, 0);
            Console.WriteLine($"Total permutations: {totalPermutationsCount}");
        }

        private static void MakePermutation(int[] array, bool[] used, int index)
        {
            if (index == array.Length)
            {
                totalPermutationsCount++;
                PirntResult(array);
            }
            else
            {
                for (int i = 1; i <= array.Length; i++)
                {
                    if (used[i-1] == false)
                    {
                        array[index] = i;
                        used[i - 1] = true;
                        MakePermutation(array, used, index + 1);
                        used[i - 1] = false;
                    }
                }
            }
        }

        private static void PirntResult(int[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }
    }
}
