using System;
using System.Collections.Generic;
using System.Linq;

// I make one more implementation for this problem in FirstLastListFast class
public class FirstLastList<T> : IFirstLastList<T>
    where T : IComparable<T>
{
    private List<T> elements = new List<T>();

    public void Add(T newElement)
    {
        this.elements.Add(newElement);
    }

    public int Count => this.elements.Count;

    public IEnumerable<T> First(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }
        return this.elements.GetRange(0, count);
    }

    public IEnumerable<T> Last(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }
        var currentElements = this.elements.GetRange(this.Count - count, count);
        currentElements.Reverse(0, count);
        return currentElements;
    }

    public IEnumerable<T> Min(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.elements.OrderBy(p => p).ToList().GetRange(0, count);
    }

    public IEnumerable<T> Max(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }
        return this.elements.OrderByDescending(p => p).ToList().GetRange(0, count);
    }

    public int RemoveAll(T element)
    {
        var count = this.elements.Count(p => p.CompareTo(element) == 0);
        this.elements.RemoveAll(p => p.CompareTo(element) == 0);

        return count;
    }

    public void Clear()
    {
        this.elements = new List<T>();
    }
}
