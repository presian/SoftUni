namespace RoundDance
{
    using System;
    using System.Linq;

    public static class RoundDanceMain
    {
        static void Main()
        {
            var countOfFriendShips = int.Parse(Console.ReadLine());
            var startPoin = int.Parse(Console.ReadLine());
            var tree = new int[countOfFriendShips, 2];
            for (int i = 0; i < countOfFriendShips; i++)
            {
                var pear = Console.ReadLine()
                    .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                tree[i, 0] = pear[0];
                tree[i, 1] = pear[1];
            }


        }
    }
}
