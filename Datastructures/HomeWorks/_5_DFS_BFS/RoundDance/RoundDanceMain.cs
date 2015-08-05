namespace RoundDance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RoundDanceMain
    {
        private static int kPoint;
        private static readonly Dictionary<int,List<int>> Tree = new Dictionary<int, List<int>>();
        private static readonly Dictionary<int, bool> Visited  = new Dictionary<int,bool>();
        private static int maxPath = 0;

        static void Main()
        {
            var countOfFriendShips = int.Parse(Console.ReadLine());
            kPoint = int.Parse(Console.ReadLine());
            var edges = new int[countOfFriendShips, 2];
            for (int i = 0; i < countOfFriendShips; i++)
            {
                var pear = Console.ReadLine()
                    .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                edges[i, 0] = pear[0];
                edges[i, 1] = pear[1];

                if (!Tree.ContainsKey(pear[0]))
                {
                    Tree.Add(pear[0], new List<int>());
                }
                if (!Tree.ContainsKey(pear[1]))
                {
                    Tree.Add(pear[1], new List<int>());
                }

                Tree[pear[0]].Add(pear[1]);

                if (!Tree[pear[1]].Contains(pear[0]))
                {
                    Tree[pear[1]].Add(pear[0]);
                }
            }

            if (!Tree.ContainsKey(kPoint))
            {
                Console.WriteLine(0);
            }
            else
            {
                var result = DFS(kPoint, 0);
                Console.WriteLine(maxPath);
            }
        }

        private static int DFS(int startPoint, int count)
        {
            if (!Tree.ContainsKey(startPoint))
            {
                throw new ArgumentOutOfRangeException("startPoint", "Dictionary is not contains this key");
            }

            if (!Visited.ContainsKey(startPoint))
            {
                Visited.Add(startPoint, true);
                var currentPointEdgesCount = Tree[startPoint].Count;

                for (int i = 0; i < currentPointEdgesCount; i++)
                {
                    var currentStartPoint = Tree[startPoint][i];
                    count++;
                    count = DFS(currentStartPoint, count);
                }
            }

            if (maxPath < count)
            {
                maxPath = count;
            }
            
            return count-1;
        }
    }
}
