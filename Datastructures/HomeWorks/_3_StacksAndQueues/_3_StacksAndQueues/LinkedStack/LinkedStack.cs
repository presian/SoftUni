namespace LinkedStack
{
    using System;

    public class ListNode<T>
    {
        public ListNode(T value, ListNode<T> nextNode)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }

        public T Value { get; set; }
        public ListNode<T> NextNode { get; set; }
    }

    public class LinkedStack<T>
    {
        public int Count { get; set; }

        public ListNode<T> FirstNode { get; set; }

        public void Push(T value)
        {
            var newNode = new ListNode<T>(value, null);

            if (this.Count > 0)
            {
                newNode.NextNode = this.FirstNode;
            }

            this.FirstNode = newNode;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count < 1)
            {
                throw new InvalidOperationException("The stack is empty!");
            }
            var element = this.FirstNode;
            this.FirstNode = this.FirstNode.NextNode;
            this.Count--;
            return element.Value;
        }

        public T[] ToArray()
        {
            T[] result = new T[this.Count];
            var currentNode = this.FirstNode;
            int index = 0;
            while (currentNode != null)
            {
                result[index] = currentNode.Value;
                index++;
                currentNode = currentNode.NextNode;
            }

            return result;
        }
    }
}
