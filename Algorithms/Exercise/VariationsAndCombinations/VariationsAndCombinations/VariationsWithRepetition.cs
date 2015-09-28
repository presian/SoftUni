namespace VariationsAndCombinations
{
    using System;

    class VariationsWithRepetition
    {
        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            int[] arr = new int[k];
            MakeVariations(arr, n);
        }

        private static void MakeVariations(int[] arr, int setSize, int index=0)
        {
            if (index >= arr.Length)
            {
                PrintResult(arr);
            }
            else
            {
                for (int i = 1; i <= setSize; i++)
                {
                    arr[index] = i;
                    MakeVariations(arr, setSize, index + 1);
                }
                
            }
        }

        private static void PrintResult(int[] arr)
        {
            Console.WriteLine($"({string.Join(", ", arr)})");
        }
    }
}
