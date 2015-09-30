namespace LoopPermutations
{
    using System;
    using System.Linq;

    class LoopPermutationsMain  
    {
        // Initialize permutations count to one because first permutation is initial array.
        static int totalPermutationsCount = 1;

        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var array = Enumerable.Range(1, n).ToArray();
            var controlArray = Enumerable.Range(0, n + 1).ToArray();
            var index = 1;

            // Print first permutations (initial array)
            PrintResult(array);
            MakePermutation(array, controlArray, index, n);
            Console.WriteLine($"Total permutations: {totalPermutationsCount}");
        }

        private static void MakePermutation(int[] array, int[] controlArray, int index, int n)
        {
            while (index < n)
            {
                controlArray[index]--;
                var j = 0;
                if (index % 2 != 0)
                {
                    j = controlArray[index];
                }

                Swap(ref array[index], ref array[j]);
                index = 1;

                while (controlArray[index] == 0)
                {
                    controlArray[index] = index;
                    index++;
                }
                PrintResult(array);
                totalPermutationsCount++;
            }
        }

        private static void Swap(ref int i, ref int j)
        {
            int temp = i;
            i = j;
            j = temp;
        }

        private static void PrintResult(int[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }
    }
}
