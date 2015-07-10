namespace OccurenceDictionary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OccurenceDictionary
    {
        private Dictionary<int, int> occurenceDict;

        public OccurenceDictionary()
        {
            this.OccurenceDict = new Dictionary<int, int>();
        }

        public Dictionary<int, int> OccurenceDict
        {
            get
            {
                return this.occurenceDict;
            }
            set
            {
                this.occurenceDict = value;
            }
        }

        public void Add(int key)
        {
            if (this.OccurenceDict.ContainsKey(key))
            {
                this.OccurenceDict[key]++;
            }
            else
            {
                this.OccurenceDict[key] = 1;
            }
        }

        public int Get(int key)
        {
            return this.OccurenceDict[key];
        }

        public void Print()
        {
            foreach (var element in this.OccurenceDict.OrderBy(e => e.Key))
            {
                Console.WriteLine(string.Format("{0} -> {1} times", element.Key, element.Value));
            }
        }
    }
}
