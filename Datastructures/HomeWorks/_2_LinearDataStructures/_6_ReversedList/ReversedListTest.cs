namespace _6_ReversedList
{
    using System;

    class ReversedListTest
    {
        static void Main()
        {
            var reversedList = new ReversedList<int>();
            reversedList.PrintList();
            Console.WriteLine("==========================");
            reversedList.Add(1);
            reversedList.Add(2);
            reversedList.Add(3);
            reversedList.Add(4);
            reversedList.Add(5);
            reversedList.Add(6);
            reversedList.Add(7);
            reversedList.Add(8);
            reversedList.Add(9);
            reversedList.Add(10);
            reversedList.Add(11);
            reversedList.PrintList();
            Console.WriteLine("==========================");
            Console.WriteLine(reversedList.Count);
            Console.WriteLine(reversedList.Capacity);
            Console.WriteLine(reversedList.Index);
            Console.WriteLine(reversedList[10]);
            reversedList[10] = 10;
            Console.WriteLine(reversedList[10]);
            Console.WriteLine("==========================");
            reversedList[10] = 11;
            foreach (var item in reversedList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
