using System.Linq;

namespace Flux.DataStructures
{
  public class OrderedDictionary<TKey, TValue>
    : IOrderedDictionary<TKey, TValue>
    where TKey : notnull
  {
    private System.Collections.Generic.Dictionary<TKey, TValue> m_dictionary = new System.Collections.Generic.Dictionary<TKey, TValue>();
    private System.Collections.Generic.List<TKey> m_listOfKeys = new System.Collections.Generic.List<TKey>();
    private System.Collections.Generic.List<TValue> m_listOfValues = new System.Collections.Generic.List<TValue>();

    // IOrderedDictionary

    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> CreateKeyValuePairs()
    {
      for (var index = 0; index < Count; index++)
        yield return new System.Collections.Generic.KeyValuePair<TKey, TValue>(m_listOfKeys[index], m_listOfValues[index]);
    }

    public int GetIndex(TKey key)
      => m_listOfKeys.IndexOf(key);
    public int GetIndex(TValue value)
      => m_listOfValues.IndexOf(value);

    public TKey GetKey(int index)
      => m_listOfKeys[index];
    public TKey GetKey(TValue value)
      => m_listOfKeys[m_listOfValues.IndexOf(value)];

    public void Insert(int index, TKey key, TValue value)
    {
      if (index < 0 || index > Count) throw new System.ArgumentOutOfRangeException(nameof(index));

      m_dictionary.Add(key, value);
      m_listOfKeys.Insert(index, key);
      m_listOfValues.Insert(index, value);
    }

    // IDictionary

    public TValue this[int index]
    {
      get => m_listOfValues[index];
      set
      {
        m_listOfValues[index] = value;
        m_dictionary[m_listOfKeys[index]] = value;
      }
    }
    public TValue this[TKey key]
    {
      get => m_dictionary[key];
      set
      {
        m_dictionary[key] = value;
        m_listOfValues[GetIndex(key)] = value;
      }
    }

    public void Add(TKey key, TValue value)
    {
      m_dictionary.Add(key, value);
      m_listOfKeys.Add(key);
      m_listOfValues.Add(value);
    }
    public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
      => Add(item.Key, item.Value);

    public void Clear()
    {
      m_dictionary.Clear();
      m_listOfKeys.Clear();
      m_listOfValues.Clear();
    }

    public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
      => m_dictionary.Contains(item);

    public bool ContainsKey(TKey key)
      => m_dictionary.ContainsKey(key);

    public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      for (var i = 0; i < Count; i++)
        array[arrayIndex++] = new System.Collections.Generic.KeyValuePair<TKey, TValue>(m_listOfKeys[i], m_listOfValues[i]);
    }

    public int Count
      => m_dictionary.Count;

    public bool IsReadOnly
      => false;

    public System.Collections.Generic.ICollection<TKey> Keys
      => m_listOfKeys;

    public bool Remove(TKey key)
    {
      if (m_dictionary.Remove(key))
      {
        var index = GetIndex(key);

        m_listOfKeys.RemoveAt(index);
        m_listOfValues.RemoveAt(index);

        return true;
      }

      return false;
    }
    public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
      => Remove(item.Key);

    public bool TryGetValue(TKey key, out TValue value)
      => m_dictionary.TryGetValue(key, out value!);

    public System.Collections.Generic.ICollection<TValue> Values
      => m_listOfValues;

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => CreateKeyValuePairs().GetEnumerator();

    System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>.GetEnumerator()
      => CreateKeyValuePairs().GetEnumerator();
  }
}