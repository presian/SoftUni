namespace _1_SumAndAverage
{
    using System;
    using System.Collections.Generic;

    static class SumAndAverageMain
    {
        static void Main()
        {
            Console.WriteLine("Please enter numbers separate with space!");
            var userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                userInput = "0 0";
            }
            
            var numbersAsString = userInput.Split(' ');
            var sumOfNumbers = 0;
            var integersCount = 0;
            var integers = new List<int>();
            foreach (var number in numbersAsString)
            {
                var currentNumber = int.Parse(number);
                sumOfNumbers += currentNumber;
                integersCount += 1;
                integers.Add(currentNumber);
            }

            Console.WriteLine(string.Format("Sum = {0}; Average = {1}", sumOfNumbers, (double)sumOfNumbers/integersCount));
        }
    }
}
