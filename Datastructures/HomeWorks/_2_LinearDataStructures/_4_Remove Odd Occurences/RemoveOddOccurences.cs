namespace _4_Remove_Odd_Occurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OccurenceDictionary;

    static class RemoveOddOccurences
    {
        static void Main()
        {
            Console.Write("Please enter a sequence:");
            var userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                throw new InvalidOperationException("Input sequence is invalid!");
            }

            var nums = userInput
                .Split(' ')
                .Select(n => int.Parse(n))
                .ToList();

            var dict = new OccurenceDictionary();

            foreach (var num in nums)
            {
               dict.Add(num); 
            }

            var resultList = nums
                .Where(num => dict.Get(num) % 2 == 0)
                .ToList();

            Console.WriteLine(String.Join(" ", resultList));
        }
    }
}
