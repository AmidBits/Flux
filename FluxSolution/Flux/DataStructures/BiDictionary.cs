namespace Flux.DataStructures
{
  public class BiDictionary<TKey, TValue>
    : System.Collections.Generic.IDictionary<TKey, TValue>
    where TKey : notnull
    where TValue : notnull
  {
    private readonly System.Collections.Generic.Dictionary<TKey, TValue> m_forward = [];
    private readonly System.Collections.Generic.Dictionary<TValue, TKey> m_reverse = [];

    public TValue this[TKey key]
    {
      get => m_forward[key];
      set => Set(key, value);
    }

    public int Count => m_forward.Count;

    public void Clear()
    {
      m_forward.Clear();
      m_reverse.Clear();
    }

    public bool ContainsKey(TKey key) => m_forward.ContainsKey(key);
    public bool ContainsValue(TValue value) => m_reverse.ContainsKey(value);

    public TValue LookupByKey(TKey key)
      => m_forward[key];
    public TKey LookupByValue(TValue value)
      => m_reverse[value];

    public bool TryGetKey(TValue value, out TKey key)
      => m_reverse.TryGetValue(value, out key!);
    public bool TryGetValue(TKey key, out TValue value)
      => m_forward.TryGetValue(key, out value!);

    public void Set(TKey key, TValue value)
    {
      if (m_forward.TryGetValue(key, out var oldValue)) // Remove old forward mapping.
        m_reverse.Remove(oldValue);

      if (m_reverse.TryGetValue(value, out var oldKey)) // Remove old reverse mapping.
        m_forward.Remove(oldKey);

      m_forward[key] = value;
      m_reverse[value] = key;
    }

    public bool RemoveByKey(TKey key)
    {
      if (!m_forward.TryGetValue(key, out var value))
        return false;

      m_forward.Remove(key);
      m_reverse.Remove(value);
      return true;
    }
    public bool RemoveByValue(TValue value)
    {
      if (!m_reverse.TryGetValue(value, out var key))
        return false;

      m_reverse.Remove(value);
      m_forward.Remove(key);
      return true;
    }

    #region Implemented interfaces

    // IDictionary<TKey, TValue>
    public System.Collections.Generic.ICollection<TKey> Keys => m_forward.Keys;
    public System.Collections.Generic.ICollection<TValue> Values => m_reverse.Keys;
    public void Add(TKey key, TValue value) => Set(key, value);
    public bool Remove(TKey key) => RemoveByKey(key);

    // ICollection<KeyValuePair<TKey, TValue>>
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Set(item.Key, item.Value);
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => ((System.Collections.Generic.ICollection<KeyValuePair<TKey, TValue>>)m_forward).Contains(item);
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((System.Collections.Generic.ICollection<KeyValuePair<TKey, TValue>>)m_forward).CopyTo(array, arrayIndex);
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => m_forward.TryGetValue(item.Key, out var value) && item.Value.Equals(value) && RemoveByKey(item.Key);

    // IEnumerable<KeyValuePair<TKey, TValue>>
    public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => (System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TValue>>)m_forward;
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
  }
}
