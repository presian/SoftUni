namespace LoopCombinations
{
    using System;
    using System.Linq;

    static class LoopCombinationsMain
    {
        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());
            var array = Enumerable.Range(1, k).ToArray();
            bool done = false;
            while (!done)
            {
                // Show the current one and set up for the next one
                // The next two lines just output the array.
                // Change them to whatever you need them to be to output the array
                Console.WriteLine(string.Join(" ", array));

                // Find the last element in the array that can be incremented
                int index = k - 1;
                int position_max = n;
                while (index >= 0 && array[index] == position_max)
                {
                    position_max--;
                    index--;
                }
                if (index < 0)
                {
                    done = true;
                }
                else
                {
                    // increment
                    array[index]++;
                    // reset the ones to the right
                    for (int i = index + 1; i < k; ++i)
                    {
                        array[i] = array[i - 1] + 1;
                    }
                }
            }
        }
    }
}
