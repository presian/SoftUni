namespace ArrayBasedStack
{
    using System;

    public class ArrayStack <T>
    {
        private const int IitialSize = 16;

        private T[] elements;
        private int count;
        private int capacity;

        public ArrayStack(int capacity = IitialSize)
        {
            this.Count = 0;
            this.Capacity = capacity;
            this.elements = new T[this.Capacity];
        }

        public int Count { get; private set; }

        public int Capacity { get; private set; }

        public void Push(T element)
        {
            if (this.Count == this.Capacity)
            {
                this.Grow();
            }

            this.elements[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty!");
            }

            this.Count--;
            var element = this.elements[this.Count];
            return element;
        }

        public T[] ToArray()
        {
            var arr = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                arr[i] = this.elements[this.Count - 1 - i];
            }

            return arr;
        }

        private void Grow()
        {
            var newStack = new T[this.Capacity*2];
            this.elements = this.CopyAllElements(newStack);
            this.Capacity = newStack.Length;
        }

        private T[] CopyAllElements(T[] newStack)
        {
            for (int i = 0; i < this.Count; i++)
            {
                newStack[i] = this.elements[i];
            }

            return newStack;
        }
    }
}
