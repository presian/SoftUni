namespace _2_CalculateSequence
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    static class CalculateSequence
    {
        static void Main()
        {
            var queue = new Queue<int>();
            var n = 0;
            try
            {
                n = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {    
                throw new InvalidOperationException("Invalid input.");
            }

            queue.Enqueue(n);
            var resultAsArr = new List<int>();
            while (true)
            {
                var current = queue.Dequeue();
                resultAsArr.Add(current);
                if (resultAsArr.Count == 50)
                {
                    break;
                }

                queue.Enqueue(current + 1);
                queue.Enqueue(2*current + 1);
                queue.Enqueue(current + 2);
            }

            Console.WriteLine(string.Join(", ", resultAsArr));
        }
    }
}
