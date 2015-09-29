namespace LoopPermutations
{
    using System;
    using System.Linq;

    class LoopPermutationsMain  
    {
        static int totalPermutationsCount = 0;

        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var array = Enumerable.Range(1, n).ToArray();
            var controlArray = Enumerable.Range(1, n).ToArray();


            MakePermutation(array, controlArray, 0);
            Console.WriteLine($"Total permutations: {totalPermutationsCount}");
        }

        private static void MakePermutation(int[] array, int[] contolArray, int index)
        {
            while (true)
            {
                
            }
        }

        private static void PirntResult(int[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }
    }
}
