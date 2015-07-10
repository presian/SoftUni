namespace _6_ReversedList
{
    using System;
    using System.Collections;

    public class ReversedList<T> : IEnumerable
    {
        private T[] list;
        private int capacity;
        private int index;
        private int count;

        public ReversedList()
        {
            this.list = new T[5];
            this.capacity = 5;
            this.index = 4;
            this.count = 0;
        }

        public T[] List
        {
            get
            {
                return this.list;
            }
            set
            {
                this.list = value;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            set
            {
                this.capacity = value;
            }
        }

        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
            set
            {
                this.count = value;
            }
        }

        public T this[int number]
        {
            get
            {
                if (number < this.Count && number > 0)
	            {
		            return this.List[this.Capacity - number - 1];
	            }

                throw new IndexOutOfRangeException();
	        }
	        set
	        {
	            if (number < this.Count  && number > 0)
	            {
		            this.List[this.Capacity - number - 1] = value;
	            }
	            else
	            {
                    throw new IndexOutOfRangeException();
	            }
	        }
        }

        public void Add(T item)
        {
            if (this.index == -1)
            {
                var newList = new T[this.Capacity*2];
                var newListLen = newList.Length;
                for (int i = 0; i < this.Count; i++)
                {
                    newList[this.Count + i] = this.List[i];
                }

                newList[this.Count - 1] = item;
                this.index = newListLen - this.Count - 1;
                this.Capacity = newListLen;
                this.List = newList;
            }
            else
            {
                this.List[this.Index] = item;
            }

            this.index--;
            this.Count++;
        }

        public void PrintList()
        {
            foreach (var element in this.List)
            {
                Console.WriteLine(element);
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = this.Capacity -1 ; i > this.index; i--)
            {
                yield return this.List[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
