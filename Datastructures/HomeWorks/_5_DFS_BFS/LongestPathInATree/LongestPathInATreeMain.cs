namespace LongestPathInATree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Node
    {
        public Node(int value, Node parentNode=null)
        {
            this.Value = value;
            this.ParentNode = parentNode;
            this.Childrens = new List<Node>();
            this.SumFromRoot = 0;
        }

        public Node ParentNode { get; set; }
        
        public int Value { get; set; }

        public ICollection<Node> Childrens { get; set; }

        public int SumFromRoot { get; set; }
    }

    public static class LongestPathInATreeMain
    {
        static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());
            var parents = new Dictionary<int, int?>();
            var tree = new Dictionary<int, List<int>>();

            for (int i = 0; i < edgesCount; i++)
            {
                var currentEdge = Console.ReadLine()
                    .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                if (parents.ContainsKey(currentEdge[1]))
                {
                    parents[currentEdge[1]] = currentEdge[0];
                }
                else
                {
                    parents.Add(currentEdge[1], currentEdge[0]);
                }

                if (!parents.ContainsKey(currentEdge[0]))
                {
                    parents.Add(currentEdge[0], null);
                }

                if (!tree.ContainsKey(currentEdge[0]))
                {
                    tree.Add(currentEdge[0], new List<int>());
                }

                if (!tree.ContainsKey(currentEdge[1]))
                {
                    tree.Add(currentEdge[1], new List<int>());
                }

                tree[currentEdge[0]].Add(currentEdge[1]);
            }

            var root = parents.FirstOrDefault(n => n.Value == null).Key;



            var leafs = tree.Where(n => n.Value.Count == 0).Select(n => n.Key);

            var sums = new Dictionary<int, int>();
            
            var paths = new Dictionary<int, List<int>>();
            
            foreach (var leaf in leafs)
            {
                paths.Add(leaf, new List<int>{leaf});
                var sum = leaf;
                var parent = parents[leaf];
                while (parent != root)
                {
                    sum += parent.Value;
                    paths[leaf].Add(parent.Value);
                    parent = parents[parent.Value];
                }
                
                sums.Add(leaf, sum);
            }

            int maxPath = int.MinValue;

            foreach (var path in paths)
            {
                foreach (var innerPath in paths)
                {
                    if (path.Key != innerPath.Key 
                        && path.Value.Intersect(innerPath.Value).Count() == 0)
                    {
                        var pathSum = path.Value.Sum();
                        var innerPathSum = innerPath.Value.Sum();
                        var currentSum = pathSum + innerPathSum + root;
                        if (currentSum > maxPath)
                        {
                            maxPath = currentSum;
                        }
                    }
                }
            }

            Console.WriteLine(maxPath);
        }
    }

 
}
