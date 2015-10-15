namespace Sortable_Collection.Sorters
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    public class BucketSorter : ISorter<int>
    {
        private int bucketCount;
        private List<int>[] buckets;
        private int collectionMaxValue;

        public void Sort(List<int> collection)
        {
            this.bucketCount = collection.Count;
            this.buckets = new List<int>[this.bucketCount];
            this.collectionMaxValue = collection.Max();
            this.BucketSort(collection);
        }

        private void BucketSort(List<int> collection)
        {           
            for (int i = 0; i < this.bucketCount; i++)
            {
                int bucketIndex = this.GetBucketIndex(collection, i);
                if (this.buckets[bucketIndex] == null)
                {
                    this.buckets[bucketIndex] = new List<int>();
                }

                this.buckets[bucketIndex].Add(collection[i]);
            }

            this.SortBuckets();
            this.PutBackSortedValuesInIntialCollection(ref collection);
        }

        private void PutBackSortedValuesInIntialCollection(ref List<int> collection)
        {
            int collectionCurrentIndex = 0;
            foreach (var bucket in this.buckets)
            {
                if (bucket != null)
                {
                    foreach (var element in bucket)
                    {
                        collection[collectionCurrentIndex] = element;
                        collectionCurrentIndex++;
                    }
                }
            }
        }

        private void SortBuckets()
        {
            var sorter = new Quicksorter<int>();
            foreach (var bucket in this.buckets)
            {
                if (bucket != null)
                {
                    sorter.Sort(bucket);
                }
            }
        }

        private int GetBucketIndex(List<int> collection, int collectionIndex)
        {
            int bucketIndex = collection[collectionIndex] / (this.collectionMaxValue + 1) * this.bucketCount;
            return bucketIndex;
        }

        public int Max
        {
            get
            {
                return this.collectionMaxValue;
            }
            set
            {
                this.collectionMaxValue = value;
            }
        }
    }
}
