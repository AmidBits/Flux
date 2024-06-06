namespace Flux
{
  public static partial class Fx
  {
    public static DataStructures.HashBased.OrderedDictionary<TKey, TValue> ToOrderedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
      where TKey : notnull
    {
      var od = new DataStructures.HashBased.OrderedDictionary<TKey, TValue>(equalityComparer ?? System.Collections.Generic.EqualityComparer<TKey>.Default);
      foreach (var item in source)
        od.Add(keySelector(item), valueSelector(item));
      return od;
    }
  }

  namespace DataStructures.HashBased
  {
    public readonly record struct IndexKeyValue<TKey, TValue>
      where TKey : notnull
    {
      private readonly int m_index;
      private readonly TKey m_key;
      private readonly TValue m_value;

      public IndexKeyValue(int index, TKey key, TValue value)
      {
        m_index = index;
        m_key = key;
        m_value = value;
      }

      public int Index => m_index;
      public TKey Key => m_key;
      public TValue Value => m_value;

      public System.Collections.Generic.KeyValuePair<TKey, TValue> ToKeyValuePair() => new(m_key, m_value);
    }

    public sealed class OrderedDictionary<TKey, TValue>
      : IOrderedDictionary<TKey, TValue>
    where TKey : notnull
    {
      private readonly System.Collections.Generic.Dictionary<TKey, TValue> m_dictionary;
      private readonly System.Collections.Generic.List<TKey> m_listOfKeys;
      private readonly System.Collections.Generic.List<TValue> m_listOfValues;

      public OrderedDictionary(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
      {
        m_dictionary = new System.Collections.Generic.Dictionary<TKey, TValue>(equalityComparer);
        m_listOfKeys = new();
        m_listOfValues = new();
      }
      public OrderedDictionary()
        : this(System.Collections.Generic.EqualityComparer<TKey>.Default)
      { }

      public System.Collections.Generic.IEnumerable<IndexKeyValue<TKey, TValue>> CreateIndexKeyValue()
      {
        for (var index = 0; index < Count; index++)
          if (TryGetKey(index, out var key) && TryGetValue(index, out var value))
            yield return new IndexKeyValue<TKey, TValue>(index, key, value);
          else // Throw if the key and value could not be retrieved.
            throw new System.InvalidOperationException();
      }

      public bool TryGetIndexKeyValue(int index, out IndexKeyValue<TKey, TValue> ikv)
      {
        try
        {
          if (TryGetKey(index, out var key) && TryGetValue(index, out var value))
          {
            ikv = new(index, key, value);
            return true;
          }
        }
        catch { }

        ikv = default!;
        return false;
      }

      public bool TryGetIndexKeyValue(TKey key, out IndexKeyValue<TKey, TValue> ikv)
      {
        try
        {
          // Additional retrieve for key, to make sure we get the stored value, and not the key passed.
          // The equality comparer for the dictionary could for example, be case insensitive which would yield a match between say "key" (searched for) and "Key" (stored).
          if (TryGetIndex(key, out var index) && TryGetKey(index, out key) && TryGetValue(key, out var value))
          {
            ikv = new(index, key, value);
            return true;
          }
        }
        catch { }

        ikv = default!;
        return false;
      }

      public bool TryGetIndexKeyValue(TValue value, out IndexKeyValue<TKey, TValue> ikv)
      {
        try
        {
          if (TryGetIndex(value, out var index) && TryGetKey(value, out var key))
          {
            ikv = new(index, key, value);
            return true;
          }
        }
        catch { }

        ikv = default!;
        return false;
      }

      #region Implemented interfaces

      // IOrderedDictionary<>
      public TValue this[int index]
      {
        get => m_listOfValues[index];
        set
        {
          m_listOfValues[index] = value;
          m_dictionary[m_listOfKeys[index]] = value;
        }
      }
      public System.Collections.Generic.ICollection<int> Indices => System.Linq.Enumerable.Range(0, Count).ToList();
      public bool ContainsValue(TValue value) => m_listOfValues.Contains(value);
      public void Insert(int index, TKey key, TValue value)
      {
        if (index < 0 || index > Count) throw new System.ArgumentOutOfRangeException(nameof(index));

        m_dictionary.Add(key, value);
        m_listOfKeys.Insert(index, key);
        m_listOfValues.Insert(index, value);
      }
      public bool TryGetIndex(TKey key, out int index) => (index = m_listOfKeys.IndexOf(key)) >= 0;
      public bool TryGetIndex(TValue value, out int index) => (index = m_listOfValues.IndexOf(value)) >= 0;
      public bool TryGetKey(int index, out TKey key)
      {
        if (index >= 0 && index < m_listOfKeys.Count)
        {
          key = m_listOfKeys[index];
          return true;
        }

        key = default!;
        return false;
      }
      public bool TryGetKey(TValue value, out TKey key) => TryGetKey(m_listOfValues.IndexOf(value), out key);
      public bool TryGetValue(int index, out TValue value)
      {
        if (index >= 0 && index < m_listOfValues.Count)
        {
          value = m_listOfValues[index];
          return true;
        }

        value = default!;
        return false;
      }

      // ICollection<>
      public int Count => m_dictionary.Count;
      public bool IsReadOnly => false;
      public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
      public void Clear()
      {
        m_dictionary.Clear();
        m_listOfKeys.Clear();
        m_listOfValues.Clear();
      }
      public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item) => m_dictionary.Contains(item); // && System.Collections.Generic.EqualityComparer<TValue>.Default.Equals(m_dictionary[item.Key], item.Value);
      public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
      {
        for (var i = 0; i < Count; i++)
          array[arrayIndex++] = new System.Collections.Generic.KeyValuePair<TKey, TValue>(m_listOfKeys[i], m_listOfValues[i]);
      }
      public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item) => Remove(item.Key);

      // IDictionary<>
      public TValue this[TKey key]
      {
        get => m_dictionary[key];
        set
        {
          if (TryGetIndex(key, out var index))
          {
            m_dictionary[key] = value;
            m_listOfValues[index] = value;
          }
          else // Key does not exist.
          {
            m_dictionary[key] = value;
            m_listOfKeys.Add(key);
            m_listOfValues[index] = value;
          }
        }
      }
      public System.Collections.Generic.ICollection<TKey> Keys => m_listOfKeys.ToList();
      public System.Collections.Generic.ICollection<TValue> Values => m_listOfValues.ToList();
      public void Add(TKey key, TValue value)
      {
        m_dictionary.Add(key, value);

        m_listOfKeys.Add(key);
        m_listOfValues.Add(value);
      }
      public bool ContainsKey(TKey key) => m_dictionary.ContainsKey(key);
      public bool Remove(TKey key)
      {
        if (m_dictionary.Remove(key))
        {
          if (TryGetIndex(key, out var index))
          {
            m_listOfKeys.RemoveAt(index);
            m_listOfValues.RemoveAt(index);
          }

          return true;
        }

        return false;
      }
      public bool TryGetValue(TKey key, out TValue value) => m_dictionary.TryGetValue(key, out value!);

      // IEnumerable<>
      public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator() => CreateIndexKeyValue().Select(ikv => ikv.ToKeyValuePair()).GetEnumerator();
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      #endregion Implemented interfaces

      public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";
    }
  }
}
