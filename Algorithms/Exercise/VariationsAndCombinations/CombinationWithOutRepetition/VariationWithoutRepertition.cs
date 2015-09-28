namespace CombinationWithOutRepetition
{
    using System;

    class CombinationWithOutRepetition
    {
        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            int[] array = new int[k];

            MakeVariation(array, n, 1);
        }

        private static void MakeVariation(int[] array, int setSize, int initialValue, int index = 0)
        {
            if (index >= array.Length)
            {
                PrintResult(array);
            }
            else
            {
                for (int i = initialValue; i <= setSize; i++)
                {
                    array[index] = i;
                    MakeVariation(array, setSize, initialValue, index + 1);
                    initialValue++;
                }
            }
        }

        private static void PrintResult(int[] array)
        {
            Console.WriteLine($"({string.Join(", ", array)})");
        }
    }
}
