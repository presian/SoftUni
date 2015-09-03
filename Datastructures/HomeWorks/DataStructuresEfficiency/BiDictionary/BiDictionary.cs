namespace BiDictionary
{
    using System;
    using System.Collections.Generic;

    public class BiDictionary<TK1, TK2, T>
    {
        private Dictionary<TK1, List<T>> byFirstKey;
        private Dictionary<TK2, List<T>> bySecondKey;
        private Dictionary<Tuple<TK1, TK2>, List<T>> byAllKeys;

        public BiDictionary()
        {
            this.ByFirstKey = new Dictionary<TK1, List<T>>();
            this.bySecondKey = new Dictionary<TK2, List<T>>();
            this.byAllKeys = new Dictionary<Tuple<TK1, TK2>, List<T>>();
        }

        public Dictionary<TK1, List<T>> ByFirstKey
        {
            get
            {
                return this.byFirstKey;
            }
            set
            {
                this.byFirstKey = value;
            }
        }

        public Dictionary<TK2, List<T>> BySecondKey
        {
            get
            {
                return this.bySecondKey;
            }
            set
            {
                this.bySecondKey = value;
            }
        }

        public Dictionary<Tuple<TK1, TK2>, List<T>> ByAllKeys
        {
            get
            {
                return this.byAllKeys;
            }
            set
            {
                this.byAllKeys = value;
            }
        }

        public IEnumerable<T> Find(TK1 keyOne, TK2 keyTwo)
        {
            return this.Search(keyOne, keyTwo);
        }

        public IEnumerable<T> FindByKey1(TK1 keyOne)
        {
            return this.SearchByOne(keyOne);
        }

        public IEnumerable<T> FindByKey2(TK2 keyTwo)
        {
            return this.SearchByTwo(keyTwo);
        }

        public bool Add(TK1 keyOne, TK2 keyTwo, T value)
        {
            return this.Insert(keyOne: keyOne, keyTwo: keyTwo, value: value);
        }

        public bool Remove(TK1 keyOne, TK2 keyTwo)
        {
            return this.Delete(keyOne, keyTwo);
        }

        private bool Insert(TK1 keyOne, TK2 keyTwo, T value)
        {
            try
            {
                var key = new Tuple<TK1, TK2>(keyOne, keyTwo);
                if (this.ByAllKeys.ContainsKey(key))
                {
                    this.ByFirstKey[keyOne].Add(value);
                    this.BySecondKey[keyTwo].Add(value);
                    this.ByAllKeys[key].Add(value);

                }
                else
                {
                    if (this.ByFirstKey.ContainsKey(keyOne))
                    {
                        this.ByFirstKey[keyOne].Add(value);
                    }
                    else
                    {
                        this.ByFirstKey.Add(keyOne, new List<T> { value });
                    }

                    if (this.BySecondKey.ContainsKey(keyTwo))
                    {
                        this.BySecondKey[keyTwo].Add(value);
                    }
                    else
                    {
                        this.BySecondKey.Add(keyTwo, new List<T> { value });
                    }

                    this.ByAllKeys.Add(key, new List<T> { value });
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool Delete(TK1 keyOne, TK2 keyTwo)
        {
            try
            {
                var key = new Tuple<TK1, TK2>(keyOne, keyTwo);
                List<T> values;
                if (this.ByAllKeys.TryGetValue(key, out values))
                {
                    foreach (var singelValue in values)
                    {
                        this.ByFirstKey[keyOne].Remove(singelValue);
                        this.BySecondKey[keyTwo].Remove(singelValue);
                    }

                    this.ByAllKeys.Remove(key);

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private IEnumerable<T> Search(TK1 keyOne, TK2 keyTwo)
        {
            var key = new Tuple<TK1, TK2>(keyOne, keyTwo);
            var list = new List<T>();
            this.ByAllKeys.TryGetValue(key, out list);
            var resultList = new List<T>();
            if (list != null)
            {
                resultList.AddRange(list);
            }

            return resultList;
        }

        private IEnumerable<T> SearchByOne(TK1 key)
        {
            var list = new List<T>();
            this.ByFirstKey.TryGetValue(key, out list);
            var resultList = new List<T>();
            if (list != null)
            {
                resultList.AddRange(list);
            }

            return resultList;
        }

        private IEnumerable<T> SearchByTwo(TK2 key)
        {
            var list = new List<T>();
            this.BySecondKey.TryGetValue(key, out list);
            var resultList = new List<T>();
            if (list != null)
            {
                resultList.AddRange(list);
            }

            return resultList;
        }

    }
}
