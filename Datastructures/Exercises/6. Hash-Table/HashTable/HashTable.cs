using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int InitialCapacity = 16;
    private const float LoadFactor = 0.75f;

    private LinkedList<KeyValue<TKey, TValue>>[] slots; 
    public int Count { get; private set; }

    public int Capacity
    {
        get
        {
            return this.slots.Length;
        }
    }

    public HashTable()
        : this(InitialCapacity)
    {
    }

    public HashTable(int capacity)
    {
       this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.Count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        this.GrowIfNeeded();
        int slotNumber = this.FindSlotNumber(key);
        if (this.slots[slotNumber] == null)
        {
            this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var element in this.slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                throw new ArgumentException("Key already exists: " + key);
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        this.slots[slotNumber].AddLast(newElement);
        this.Count++;
    }

    private int FindSlotNumber(TKey key)
    {
        var slotNumber = Math.Abs(key.GetHashCode()) % this.slots.Length;
        return slotNumber;
    }

    private void GrowIfNeeded()
    {
        if (((float)this.Count + 1) / this.Capacity > LoadFactor)
        {
            this.Grow();
        }
    }

    private void Grow()
    {
        var newTable = new HashTable<TKey, TValue>(this.Capacity * 2);
        foreach (var element in this)
        {
            newTable.Add(element.Key, element.Value);
        }

        this.slots = newTable.slots;
        this.Count = newTable.Count;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.GrowIfNeeded();
        var element = this.Find(key);
        if (element == null)
        {
            this.Add(key, value);
            return true;
        }
        else
        {
            element.Value = value;
            return false;
        }
    }

    public TValue Get(TKey key)
    {
        var element = this.Find(key);
        if (element == null)
        {
            throw new KeyNotFoundException();
        }

        return element.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            return this.Get(key);
        }
        set
        {
            this.AddOrReplace(key, value);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var element = this.Find(key);

        if (element != null)
        {
            value = element.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        var slotNumber = this.FindSlotNumber(key);
        if (slotNumber < this.Capacity)
        {
            var element = this.slots[slotNumber];

            if (element != null)
            {
                foreach (var subElement in element)
                {
                    if (subElement.Key.Equals(key))
                    {
                        return subElement;
                    }
                }

                return null;
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        var element = this.Find(key);
        return element != null;
    }

    public bool Remove(TKey key)
    {
        var slotNumber = this.FindSlotNumber(key);
        var elements = this.slots[slotNumber];
        if (elements != null)
        {
            var currentElement = elements.First;
            while (currentElement != null)
            {
                if (currentElement.Value.Key.Equals(key))
                {
                    elements.Remove(currentElement);
                    this.Count--;
                    return true;
                }

                currentElement = currentElement.Next;
            }
        }

        return false;
    }

    public void Clear()
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get { return this.Select(e => e.Key); }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            return this.Select(e => e.Value);
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var slot in this.slots)
        {
            if (slot != null)
            {
                foreach (var element in slot)
                {
                    yield return element;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
