﻿using System;

public class BinaryTree<T>
{
    private T value;
    private BinaryTree<T> leftChild;
    private BinaryTree<T> rightChild;

    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.Value = value;
        this.LeftChild = leftChild;
        this.RightChild = rightChild;
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

    public BinaryTree<T> LeftChild
    {
        get
        {
            return this.leftChild;
        }
        set
        {
            this.leftChild = value;
        }
    }

    public BinaryTree<T> RightChild
    {
        get
        {
            return this.rightChild;
        }
        set
        {
            this.rightChild = value;
        }
    }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        Console.Write(new string(' ', 2 * indent));
        Console.WriteLine(this.Value);
        if (this.LeftChild != null)
        {
            this.LeftChild.PrintIndentedPreOrder(indent + 1);
        }
        if (this.RightChild != null)
        {
            this.RightChild.PrintIndentedPreOrder(indent + 1);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        if (this.LeftChild != null)
        {
            this.LeftChild.EachInOrder(action);
        }

        action(this.Value);
        if (this.RightChild != null)
        {
            this.RightChild.EachInOrder(action);
        }
    }

    public void EachPostOrder(Action<T> action)
    {
        if (this.LeftChild != null)
        {
            this.LeftChild.EachPostOrder(action);
        }

        if (this.RightChild != null)
        {
            this.RightChild.EachPostOrder(action);
        }

        action(this.Value);
    }
}
