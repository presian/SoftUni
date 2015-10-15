namespace Sortable_Collection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sortable_Collection.Contracts;

    public class SortableCollection<T> where T : IComparable<T>
    {
        public SortableCollection()
        {
        }

        public SortableCollection(IEnumerable<T> items)
        {
            this.Items = new List<T>(items);
        }

        public SortableCollection(params T[] items)
            : this(items.AsEnumerable())
        {
        }

        public List<T> Items { get; } = new List<T>();

        public int Count => this.Items.Count;

        public void Sort(ISorter<T> sorter)
        {
            sorter.Sort(this.Items);
        }

        public int BinarySearch(T item)
        {
            int startIndex = 0;
            int endIndex = this.Count;
            if (this.Count < 1)
            {
                return -1;
            }

            int elementIndex = this.BinarySearchProcedure(item, startIndex, endIndex);
            return elementIndex;
        }

        private int BinarySearchProcedure(T item, int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
            {
                return -1;
            }
            int midpoint = startIndex + (endIndex - startIndex)/2;

            if (this.Items[midpoint].CompareTo(item) > 0)
            {
                this.BinarySearchProcedure(item, 0, midpoint);
            }

            if (this.Items[midpoint].CompareTo(item) < 0)
            {
                this.BinarySearchProcedure(item, midpoint + 1, endIndex);
            }

            return midpoint;
        }

//
//        public int InterpolationSearch(T item)
//        {
//            throw new NotImplementedException();
//        }
//
//        public void Shuffle()
//        {
//            throw new NotImplementedException();
//        }

        public T[] ToArray()
        {
            return this.Items.ToArray();
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", this.Items)}]";
        }        
    }
}