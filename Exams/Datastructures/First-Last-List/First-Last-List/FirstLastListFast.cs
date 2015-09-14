using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastListFast<T> : IFirstLastList<T>
    where T : IComparable<T>
{
    private OrderedDictionary<T, List<T>> OrderedElements = new OrderedDictionary<T, List<T>>(); 
    private LinkedList<T> elements = new LinkedList<T>();

    public void Add(T newElement)
    {
        this.elements.AddLast(newElement);
        if (this.OrderedElements.ContainsKey(newElement))
        {
            this.OrderedElements[newElement].Add(newElement);
        }
        else
        {
            this.OrderedElements.Add(newElement, new List<T>() { {newElement} });
        } 
    }

    public int Count => this.elements.Count;

    public IEnumerable<T> First(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.elements.Take(count);
    }

    public IEnumerable<T> Last(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return elements.Reverse().Take(count);
    }

    public IEnumerable<T> Min(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.OrderedElements.SelectMany(p => p.Value).Take(count);
    }

    public IEnumerable<T> Max(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.OrderedElements.Reversed().SelectMany(p => p.Value).Take(count);
    }

    public int RemoveAll(T element)
    {
        if (!this.OrderedElements.ContainsKey(element))
        {
            return 0;
        }

        var elementsForDeleting = this.OrderedElements[element];
        var count = elementsForDeleting.Count;
        foreach (var el in elementsForDeleting)
        {
            this.elements.Remove(el);
        }

        this.OrderedElements.RemoveAll(p => p.Key.CompareTo(element) == 0);
        return count;
    }

    public void Clear()
    {
        this.elements = new LinkedList<T>();
        this.OrderedElements = new OrderedDictionary<T, List<T>>();
    }
}