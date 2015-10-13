namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class Quicksorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            this.Quicksort(collection, 0 , collection.Count - 1);
        }

        private void Quicksort(List<T> array, int start, int end)
        {
            var endIndex = end;
            var startIndex = 1;
            while (true)
            {
                if (startIndex > endIndex)
                {
                    break;
                }

                for (int i = startIndex; i <= endIndex; i++)
                {
                    var leftElement = array[i - 1];
                    var rightElement = array[i];
                    if (leftElement.CompareTo(rightElement) > 0)
                    {
                        this.Swap(ref array, leftElement, i);
                    }
                }
                endIndex--;
            }
//            if (start >= end)
//            {
//                return;
//            }
//
//            T pivot = array[start];
//            int storeIndex = start + 1;
//
//            for (int i = start + 1; i <= end; i++)
//            {
//                if (array[i].CompareTo(pivot) < 0)
//                {
//                    this.Swap(ref array, pivot, storeIndex);
//                    storeIndex++;
//                }
//
//                this.Quicksort(array, i, end);
//            }
//
//            storeIndex--;
        }

        private void Swap(ref List<T> array, T pivot, int storeIndex)
        {
            array[storeIndex - 1] = array[storeIndex];
            array[storeIndex] = pivot;
        }    
    }
}
