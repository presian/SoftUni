namespace _7_LinkedList
{
    using System;

    class Test
    {
        static void Main()
        {
            var singleLinkedList = new LinkedList<int> {1, 2, 3, 4};
            Console.WriteLine(string.Format("Count: {0}", singleLinkedList.Count));
            singleLinkedList.Add(5);
            Console.WriteLine(string.Format("Count: {0}", singleLinkedList.Count));
            singleLinkedList.RemoveAt(3);
            foreach (var node in singleLinkedList)
            {
                Console.WriteLine(node);
            }
            singleLinkedList.Add(2);
            Console.WriteLine(singleLinkedList.FirstIndexOf(2));
            Console.WriteLine(singleLinkedList.LastIndexOf(2));
        }
    }
}
