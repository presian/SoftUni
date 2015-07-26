using System;
using System.Collections.Generic;

public class Tree<T>
{
    private T value;
    private List<Tree<T>> childrens; 
    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Childrens = new List<Tree<T>>();
        foreach (var child in children)
        {
            this.Childrens.Add(child);
        }
    }

    public T Value
    {
        get
        {
            return this.value;
        }
        set
        {
            this.value = value;
        }
    }

    public List<Tree<T>> Childrens
    {
        get; private set;
    }

    public void Print(int indent = 0)
    {
        Console.Write(new string(' ', 2 * indent));
        Console.WriteLine(this.Value);
        foreach (var children in this.Childrens)
        {
            children.Print(indent + 1);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);
        foreach (var children in this.Childrens)
        {
            children.Each(action);
        }
    }
}
