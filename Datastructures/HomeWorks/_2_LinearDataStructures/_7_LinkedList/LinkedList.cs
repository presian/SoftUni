namespace _7_LinkedList
{
    using System;
    using System.Collections;

    public class LinkedList<T> : IEnumerable
    {
        private ListNode<T> Head { set; get; }

        private ListNode<T> Tail { set; get; }

        public int Count { get; private set; }

        public void Add(T element)
        {
            if (this.Count == 0)
            {
                this.Head = this.Tail = new ListNode<T>(element);
            }
            else
            {
                var newTail = new ListNode<T>(element);
                this.Tail.NextNode = newTail;
                this.Tail = newTail;
            }

            this.Count++;
        }

        public void RemoveAt(int index)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }

            if (index < 0 || index > this.Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                this.Head = this.Head.NextNode;
                if (this.Head == null)
                {
                    this.Tail = this.Head;
                }
            }
            else
            {
                var currentNode = this.Head;
                for (int i = 1; i < index - 1; i++)
                {
                    currentNode = currentNode.NextNode;
                }

                var nodeOnIndex = currentNode.NextNode;
                var nextToIndex = nodeOnIndex.NextNode;

                currentNode.NextNode = nextToIndex;
                if (nextToIndex == null)
                {
                    this.Tail = currentNode;
                }
            }

            this.Count--;
        }

        public int FirstIndexOf(T item)
        {
            return this.SearchFor(item, SearchTypes.FirtOccurrence);
        }

        public int LastIndexOf(T item)
        {
            return this.SearchFor(item, SearchTypes.LastOccurrence);
        }

        public IEnumerator GetEnumerator()
        {
            var currentNode = this.Head;
            while (currentNode != null)
            {
                yield return currentNode.NodeValue;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int SearchFor(T value, SearchTypes type)
        {
            var current = this.Head;
            var lastIndex = -1;
            if (current.NodeValue.Equals(value))
            {
                if (type == SearchTypes.FirtOccurrence)
                {
                    return 0;
                }
                else
                {
                    lastIndex = 0;
                }
                
            }

            for (int i = 1; i < this.Count; i++)
            {
                current = current.NextNode;
                if (current.NodeValue.Equals(value))
                {
                    if (type == SearchTypes.FirtOccurrence)
                    {
                        return i;
                    }
                    else
                    {
                        lastIndex = i;
                    }
                }
            }

            return lastIndex;
        }
    }
}
