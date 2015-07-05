using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    public class ListNode<T>
    {
        public T Value { get; private set; }

        public ListNode<T> PrevNode { get; set; }

        public ListNode<T> NextNode { get; set; }

        public ListNode(T value)
        {
            this.Value = value;
        }
    }

    private ListNode<T> Head {set; get;} 
    
    private ListNode<T> Tail {set; get;} 


    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        if (this.Count == 0)
        {
            this.Head = this.Tail = new ListNode<T>(element);
        }
        else
        {
            var newHead = new ListNode<T>(element);
            newHead.NextNode = this.Head;
            this.Head.PrevNode = newHead;
            this.Head = newHead;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        var newNode = new ListNode<T>(element);
        if (this.Count == 0)
        {
            this.Head = newNode;
            this.Tail = newNode;
        }
        else
        {
            this.Tail.NextNode = newNode;
            newNode.PrevNode = this.Tail;
            this.Tail = newNode;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }

        var oldFirst = this.Head;
        var newFirst = this.Head.NextNode;

        if (newFirst != null)
        {
            newFirst.PrevNode = null;
            this.Head = newFirst;
        }
        else
        {
            this.Head = null;
            this.Tail = null;
        }

        this.Count--;
        return oldFirst.Value;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }

        var oldTail = this.Tail;
        var newTail = this.Tail.PrevNode;

        if (newTail != null)
        {
            newTail.NextNode = null;
            this.Tail = newTail;
        }
        else
        {
            this.Head = null;
            this.Tail = null;
        }

        this.Count--;
        return oldTail.Value;
    }

    public void ForEach(Action<T> action)
    {
        var currentNode = this.Head;
        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.Head;
        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.NextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        T[] arr = new T[this.Count];
        var index = 0;
        var currentNode = this.Head;
        while (currentNode != null)
        {
            arr[index] = currentNode.Value;
            currentNode = currentNode.NextNode;
            index++;
        }

        return arr;
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
