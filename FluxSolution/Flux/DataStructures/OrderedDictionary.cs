namespace Flux.DataStructures
{
  /// <summary>
  /// <para>This is an ordered dictionary implementing <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <remarks>An ordered data structure maintains an indexed order, i.e. like a <see cref="System.Collections.Generic.List{TValue}"/> or a <typeparamref name="TValue"/>[].</remarks>
  public sealed class OrderedDictionary<TKey, TValue>
    : IOrderedDictionary<TKey, TValue>
    where TKey : notnull
  {
    private readonly System.Collections.Generic.Dictionary<TKey, TValue> m_dictionary;
    private readonly System.Collections.Generic.List<TKey> m_listOfKeys = new();
    private readonly System.Collections.Generic.List<TValue> m_listOfValues = new();

    public OrderedDictionary(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer) => m_dictionary = new(equalityComparer);

    public OrderedDictionary() : this(System.Collections.Generic.EqualityComparer<TKey>.Default) { }

    public System.Collections.Generic.IEnumerable<IndexKeyValueTrio<TKey, TValue>> CreateIndexKeyValue()
    {
      for (var index = 0; index < Count; index++)
        if (TryGetKey(index, out var key) && TryGetValue(index, out var value))
          yield return new IndexKeyValueTrio<TKey, TValue>(index, key, value);
        else // Throw if the key and value could not be retrieved.
          throw new System.InvalidOperationException();
    }

    //public bool TryGetIndexKeyValue(int index, out IndexKeyValueTrio<TKey, TValue> ikv)
    //{
    //  try
    //  {
    //    ikv = new(index, m_listOfKeys[index], m_listOfValues[index]);
    //    return true;
    //  }
    //  catch { }

    //  ikv = default!;
    //  return false;
    //}

    //public bool TryGetIndexKeyValue(TKey key, out IndexKeyValueTrio<TKey, TValue> ikv)
    //{
    //  try
    //  {
    //    if (m_dictionary.TryGetValue(key, out var value))
    //    {
    //      var index = m_listOfValues.IndexOf(value);

    //      // Additional retrieve for key, to make sure we get the stored value, and not the key passed.
    //      // The equality comparer for the dictionary could for example, be case insensitive which would yield a match between say "key" (searched for) and "Key" (stored).
    //      key = m_listOfKeys[index];

    //      ikv = new(index, key, value);
    //      return true;
    //    }
    //  }
    //  catch { }

    //  ikv = default!;
    //  return false;
    //}

    //public bool TryGetIndexKeyValue(TValue value, out IndexKeyValueTrio<TKey, TValue> ikv)
    //{
    //  try
    //  {
    //    var index = m_listOfValues.IndexOf(value);
    //    var key = m_listOfKeys[index];

    //    ikv = new(index, key, value);
    //    return true;
    //  }
    //  catch { }

    //  ikv = default!;
    //  return false;
    //}

    public bool TryGetIndexKeyValue(int index, out IndexKeyValueTrio<TKey, TValue> ikv)
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

    public bool TryGetIndexKeyValue(TKey key, out IndexKeyValueTrio<TKey, TValue> ikv)
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

    public bool TryGetIndexKeyValue(TValue value, out IndexKeyValueTrio<TKey, TValue> ikv)
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

    #region IOrderedDictionary<>

    public TValue this[int index]
    {
      get => m_listOfValues[index];
      set
      {
        m_listOfValues[index] = value;
        m_dictionary[m_listOfKeys[index]] = value;
      }
    }

    public System.Collections.Generic.IEnumerable<int> Indices => System.Linq.Enumerable.Range(0, Count);

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

    #endregion // IOrderedDictionary<>

    #region ICollection<>

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

    #endregion // ICollection<>

    #region IDictionary<>

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
          m_listOfValues.Add(value);
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

    #endregion // IDictionary<>

    #region IEnumerable<>

    public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator() => CreateIndexKeyValue().Select(ikvt => ikvt.ToKeyValuePair()).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion // IEnumerable<>

    #endregion // Implemented interfaces

    public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";

    public System.Collections.Generic.IEnumerable<System.Collections.DictionaryEntry> CreateDictionaryEntries()
    {
      for (var index = 0; index < Count; index++)
        if (TryGetKey(index, out var key) && TryGetValue(index, out var value))
          yield return new(key, value);
    }

    #region IDictionary

    bool System.Collections.IDictionary.IsFixedSize => false;

    System.Collections.ICollection System.Collections.IDictionary.Keys => throw new NotImplementedException();

    System.Collections.ICollection System.Collections.IDictionary.Values => throw new NotImplementedException();

    bool System.Collections.ICollection.IsSynchronized => false;

    object System.Collections.ICollection.SyncRoot => throw new NotImplementedException();

    object? System.Collections.IDictionary.this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    void System.Collections.IDictionary.Add(object key, object? value) => throw new NotImplementedException();
    bool System.Collections.IDictionary.Contains(object key) => throw new NotImplementedException();
    System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator() => throw new NotImplementedException();
    void System.Collections.IDictionary.Remove(object key) => throw new NotImplementedException();
    void System.Collections.ICollection.CopyTo(Array array, int index) => throw new NotImplementedException();

    private class SimpleDictionaryEnumerator
      : System.Collections.IDictionaryEnumerator
    {
      private System.Collections.DictionaryEntry[] items;

      private int index = -1;

      public SimpleDictionaryEnumerator(System.Collections.Generic.IEnumerable<System.Collections.DictionaryEntry> des)
      {
        items = [.. des];

        Reset();
      }

      public object Current => Entry;

      public System.Collections.DictionaryEntry Entry => items[AssertValidIndex()];

      public object Key => items[AssertValidIndex()].Key;
      public object Value => items[AssertValidIndex()].Value!;

      public bool MoveNext()
      {
        if (index < items.Length - 1)
          index++;

        return IsValidateIndex();
      }

      private int AssertValidIndex()
      {
        if (!IsValidateIndex())
          throw new System.ArgumentOutOfRangeException(nameof(index));

        return index;
      }

      private bool IsValidateIndex() => index >= 0 && index < items.Length;

      public void Reset() => index = -1;
    }
    #endregion
  }
}
