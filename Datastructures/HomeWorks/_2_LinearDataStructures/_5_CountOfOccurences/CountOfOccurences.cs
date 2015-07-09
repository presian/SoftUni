namespace _5_CountOfOccurences
{
    using System;
    using System.Linq;

    using OccurenceDictionary;

    class CountOfOccurences
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

            dict.Print();
        }
    }
}
