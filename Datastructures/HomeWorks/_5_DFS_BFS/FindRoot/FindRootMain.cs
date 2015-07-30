namespace FindRoot
{
    using System;
    using System.Linq;

    static class FindRootMain
    {
        static void Main()
        {
            var edgesCount = int.Parse(Console.ReadLine());

            bool[] hasParent = new bool[edgesCount + 1];
            var tree = new int[edgesCount, 2];

            for (int i = 0; i < edgesCount; i++)
            {
                var pair = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                hasParent[pair[1]] = true;
            }
            Console.WriteLine(GetResult(hasParent));
            
        }

        private static string GetResult(bool[] hasParent)
        {
            var rootIndex = -1;

            for (int i = 0; i < hasParent.Length; i++)
            {
                if (hasParent[i] == false)
                {
                    if (rootIndex != -1)
                    {
                        return "Forest is not a tree!";
                    }
                    else
                    {
                        rootIndex = i;
                    }
                }
            }

            if (rootIndex == -1)
            {
                return "No root!";
            }
            else
            {
                return rootIndex.ToString();
            }
        }
    }
}
