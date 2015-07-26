namespace PlayWithTrees
{
    using System;

    static class Play
    {
        static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            for (int i = 1; i < nodesCount; i++)
            {
                string[] edge = Console.ReadLine().Split(' ');
                int parentValue = int.Parse(edge[0]);
                Tree<int> parent = Tree<int>.GetTreeNodeByValue(parentValue);
                int childValue = int.Parse(edge[1]);
                Tree<int> child = Tree<int>.GetTreeNodeByValue(childValue);
                parent.Children.Add(child);
                child.Parent = parent;
            }

            int pathSum = int.Parse(Console.ReadLine());
            int subtreeSum = int.Parse(Console.ReadLine());
        }
    }
}
