using System;
using System.Linq;

public class CircularQueue<T>
{
    private const int InitialSize = 16;

    private T[] elements;
    private int startIndex;
    private int endIndex;

    public int Count { get; private set; }

    public CircularQueue()
        : this(InitialSize)
    {
        
    }

    public CircularQueue(int capacity)
    {
        this.Elements = new T[capacity];
        this.Count = 0;
        this.StartIndex = 0;
        this.EndIndex = 0;
    }

    public T[] Elements
    {
        get
        {
            return this.elements;
        }
        set
        {
            this.elements = value;
        }
    }

    public int StartIndex
    {
        get
        {
            return this.startIndex;
        }
        set
        {
            this.startIndex = value;
        }
    }

    public int EndIndex
    {
        get
        {
            return this.endIndex;
        }
        set
        {
            this.endIndex = value;
        }
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.Elements.Length)
        {
            this.Grow();
        }
        if (this.Count == 0)
        {
        }

        this.Elements[this.EndIndex] = element;
        this.EndIndex = (this.EndIndex + 1) % this.Elements.Length;
        this.Count++;
    }

    private void Grow()
    {
        var currentLength = this.Elements.Length;
        var newElements = new T[currentLength*2];
        this.Elements = this.CopyAllElements(newElements);
        this.StartIndex = 0;
        this.EndIndex = this.Count;
    }

    private T[] CopyAllElements(T[] newElements)
    {
        var sourceIndex = this.StartIndex;
        for (int i = 0; i < this.Count; i++)
        {
            newElements[i] = this.Elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.Elements.Length;
        }

        return newElements;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty!");
        }

        var element = this.Elements[this.StartIndex];
        this.StartIndex = (this.StartIndex + 1) % this.Elements.Length;
        this.Count--;
        return element;
    }

    public T[] ToArray()
    {
        var newArr = new T[this.Count];
        var resultArr = this.CopyAllElements(newArr);
        return resultArr;
    }
}


class Example
{
    static void Main()
    {
        var queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        var first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
