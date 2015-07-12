namespace LinkedQueue
{
    using System;

    public class Node <T>
    {
        public Node(T value, Node<T> nextNode = null, Node<T> previousNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
            this.PreviousNode = previousNode;
        }

        public T Value { get; set; }

        public Node<T> NextNode { get; set; }

        public Node<T> PreviousNode { get; set; }
    }

    public class LinkedQueue <T>
    {
        public Node<T> FirstNode { get; set; }

        public Node<T> LastNode { get; set; }

        public int Count { get; set; }

        public void Enqueue(T element)
        {
            var newNode = new Node<T>(element);
            if (this.Count == 0)
            {
                this.FirstNode = this.LastNode = newNode;
            }
            else
            {
                this.LastNode.NextNode = newNode;
                this.LastNode = newNode;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count < 1)
            {
                throw new InvalidOperationException("The queue is empty!");
            }

            var returnNode = this.FirstNode;
            this.FirstNode = this.FirstNode.NextNode;
            this.Count--;
            return returnNode.Value;
        }

        public T[] ToArray()
        {
            var currentNode = this.FirstNode;
            var resultArr = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                resultArr[i] = currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            return resultArr;
        }
    }
}
