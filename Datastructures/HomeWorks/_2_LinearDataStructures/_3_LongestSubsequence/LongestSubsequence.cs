namespace _3_LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class LongestSubsequence
    {
        static void Main()
        {
            Console.Write("Please enter a sequence: ");
            var userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                throw new InvalidOperationException("The input sequence is invalid");
            }

            var nums = userInput
                .Split(' ')
                .Select(number => int.Parse(number))
                .ToList();
            if (nums.Count < 1)
            {
                throw new InvalidOperationException("The input sequence is invalid");
            }

            var current = nums[0];
            var counter = 1;
            var startIndex = 0;
            var maxCount = 1;
            for (int i = 1; i < nums.Count; i++)
            {
                if (nums[i] == current)
                {
                    counter++;
                    if (i == nums.Count - 1)
                    {
                        CheckCounter(counter, ref maxCount, ref startIndex, i + 1);
                    }
                }
                else
                {
                    CheckCounter(counter, ref maxCount, ref startIndex, i);
                    counter = 1;
                    current = nums[i];
                }
            }

            PrintResult(nums, startIndex, maxCount);
        }

        private static void CheckCounter(int counter, ref int maxCount, ref int startIndex, int currentIndex)
        {
            if (counter > maxCount)
            {
                maxCount = counter;
                startIndex = currentIndex - counter;
            }
        }

        private static void PrintResult(List<int> nums, int startIndex, int maxCount)
        {
            for (int i = startIndex; i < startIndex + maxCount; i++)
            {
                Console.Write(nums[i] + " ");
            }

            Console.WriteLine();
        }
    }
}
