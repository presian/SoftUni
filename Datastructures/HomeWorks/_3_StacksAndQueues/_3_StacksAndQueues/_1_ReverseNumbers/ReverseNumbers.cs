namespace _1_ReverseNumbers
{
    using System;
    using System.Collections.Generic;

    static class ReverseNumbers 
    {
        static void Main()
        {
            var stack = new Stack<int>();
            try
            {
                int numbersCount = int.Parse(Console.ReadLine());

                for (int i = 0; i < numbersCount; i++)
                {
                    stack.Push(int.Parse(Console.ReadLine()));
                }
            }
            catch (Exception)
            {
                
                throw new ArgumentException("Invalid input.");
            }
            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());   
            }
        }
    }
}
