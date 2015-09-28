namespace VariationsWithoutReprtition
{
    using System;

    class VariationsWithoutReprtition
    {
        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            int[] arr = new int[k];
            bool[] used = new bool[n + 1];
            MakeVariations(arr, n, used);
        }

        private static void MakeVariations(int[] arr, int setSize, bool[] used, int index = 0)
        {
            if (index >= arr.Length)
            {
                PrintResult(arr);
            }
            else
            {
                for (int i = 1; i <= setSize; i++)
                {
                    if (used[i] == false)
                    {
                        arr[index] = i;
                        used[i] = true;
                        MakeVariations(arr, setSize, used, index + 1);
                        used[i] = false;
                    }
                }
            }
        }

        private static void PrintResult(int[] arr)
        {
            Console.WriteLine($"({string.Join(", ", arr)})");
        }
    }
}
