using System;
using System.Collections.Generic;
using System.Linq;

public class GraphConnectedComponents
{
//    static new List<int>[] graph = new List<int>[]
//    {
//        new List<int>() {3, 6},
//        new List<int>() {3, 4, 5, 6}, 
//        new List<int>() {8}, 
//        new List<int>() {0, 1, 5}, 
//        new List<int>() {1, 6}, 
//        new List<int>() {1, 3}, 
//        new List<int>() {0, 1, 4}, 
//        new List<int>() {}, 
//        new List<int>() {2}, 
//    };

    private new static List<int>[] graph = ReadGraph();
    
    static bool[] visited = new bool[graph.Length];

    public static void Main()
    {
//        DFS(0);
//        Console.WriteLine();
        FindGraphConnectedComponents();
        Console.WriteLine();
    }

    public static void DFS(int startPoint)
    {
        if (!visited[startPoint])
        {
            visited[startPoint] = true;
            foreach (var edge in graph[startPoint])
            {
                DFS(edge);
            }

            Console.Write(" " + startPoint);
        }
    }

    public static void FindGraphConnectedComponents()
    {
        for (int i = 0; i < graph.Length; i++)
        {
            if (!visited[i])
            {
                Console.Write("Connected component: ");
                DFS(i);
                Console.WriteLine();
            }
        }
    }

    static List<int>[] ReadGraph()
    {
        int n = int.Parse(Console.ReadLine());
        var graph = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine();
            graph[i] =
                input
                    .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
        }
        return graph;
    } 
}
