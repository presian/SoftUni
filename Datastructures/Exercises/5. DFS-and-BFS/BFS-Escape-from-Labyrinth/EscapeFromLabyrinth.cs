using System;
using System.Collections.Generic;
using System.Text;

public class EscapeFromLabyrinth
{
    private const char VisitedCell = 's';
    private static int width = 9;
    private static int height = 7;

    private static char[,] labyrinth =
    {
        { '*', '*', '*', '*', '*', '*', '*', '*', '*'},
        { '*', '-', '-', '-', '-', '*', '*', '-', '-'},
        { '*', '*', '-', '*', '-', '-', '-', '-', '*'},
        { '*', '-', '-', '*', '-', '*', '-', '*', '*'},
        { '*', 's', '*', '-', '-', '*', '-', '*', '*'},
        { '*', '*', '-', '-', '-', '-', '-', '-', '*'},
        { '*', '*', '*', '*', '*', '*', '*', '-', '*'},
    };

    public class Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public string Direction { get; set; }

        public Point PreviousPoint { get; set; }
    }

    static string FindShortestPathToEcit()
    {
        var queue = new Queue<Point>();
        var currnetCell = FindStartPosition();
        if (currnetCell == null)
        {
            return null;
        }

        queue.Enqueue(currnetCell);
        while (queue.Count > 0)
        {
            var currentCell = queue.Dequeue();
            if (IsExit(currentCell))
            {
                return TracePathBack(currnetCell);
            }

            TryDirection(queue, currentCell, "U", 0, -1);
            TryDirection(queue, currentCell, "R", +1, 0);
            TryDirection(queue, currentCell, "D", 0, +1);
            TryDirection(queue, currentCell, "L", -1, 0);
            
        }

        return null;
    }

    static Point FindStartPosition()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (labyrinth[y, x] == VisitedCell)
                {
                    return new Point
                    {
                        X = x,
                        Y = y
                    };
                }
            }
        }

        return null;
    }

    static bool IsExit(Point currentCell)
    {
        bool isOnBorderX = currentCell.X == 0 || currentCell.X == width - 1;
        bool isOnBorderY = currentCell.Y == 0 || currentCell.Y == height - 1;
        return isOnBorderX || isOnBorderY;
    }

    static void TryDirection(Queue<Point> queue, Point currentCell, string direction, int deltaX, int deltaY)
    {
        int newX = currentCell.X + deltaX;
        int newY = currentCell.Y + deltaY;
        if (newX >= 0 && newX < width && newY >= 0 && newY < height && labyrinth[newY, newX] == '-')
        {
            labyrinth[newY, newX] = VisitedCell;
            var nextCell = new Point
            {
                X = newX,
                Y = newY,
                Direction = direction,
                PreviousPoint = currentCell
            };

            queue.Enqueue(nextCell);
        }
    }

    static string TracePathBack(Point currentCell)
    {
        var path = new StringBuilder();
        while (currentCell.PreviousPoint != null)
        {
            path.Append(currentCell.Direction);
            currentCell = currentCell.PreviousPoint;
        }
        
        var pathReversed = new StringBuilder(path.Length);
        for (int i = 0; i < path.Length; i++)
        {
            pathReversed.Append(path[i]);
        }

        return pathReversed.ToString();
    }

    public static void Main()
    {
        string shortestPathToExit = FindShortestPathToEcit();
        if (shortestPathToExit == null)
        {
            Console.WriteLine("No exit!");
        }
        else if (shortestPathToExit == "")
        {
            Console.WriteLine("Start is at the exit.");
        }
        else
        {
            Console.WriteLine("Shortest exit: " + shortestPathToExit);
        }
    }
}
