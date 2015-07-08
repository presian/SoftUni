namespace _2_SortWords
{
    using System;
    using System.Linq;

    class SortWords
    {
        static void Main()
        {
            var inputRow = Console.ReadLine();
            if (string.IsNullOrEmpty(inputRow))
            {
                throw new NullReferenceException("Invalid input.");
            }

            var sortedArr = inputRow.Split().OrderBy(s => s);
            Console.WriteLine(String.Join(" ", sortedArr));
        }
    }
}
