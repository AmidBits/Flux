
//using System;
//using System.Collections;

//using System.Collections;
//using System.Collections.Generic;

using System.Collections;
using System.Collections.Generic;

namespace Flux.Collections.Generic
{
  public interface IIndexedDictionary<TKey, TValue>
    : System.Collections.Specialized.IOrderedDictionary//, System.Collections.Generic.IList<TValue>, System.Collections.Generic.IDictionary<TKey, TValue>
  {
  }

  public class IndexedDictionary<TKey, TValue>
    : IIndexedDictionary<TKey, TValue>
    where TKey : notnull, System.Collections.Generic.IEqualityComparer<TKey>
    where TValue : notnull
  {
    //    private System.Collections.Generic.IEqualityComparer<TKey> m_comparer = System.Collections.Generic.EqualityComparer<TKey>.Default;

    private readonly System.Collections.Generic.Dictionary<TKey, TValue> m_byKey = new System.Collections.Generic.Dictionary<TKey, TValue>();
    private readonly System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<TKey, TValue>> m_byIndex = new List<KeyValuePair<TKey, TValue>>();

    private readonly object m_syncRoot = new object();

    //#region IDictionary
    //public TValue this[TKey key] { get => m_byKey[key]; set => m_byKey[key] = value; }

    //public ICollection<TKey> Keys => throw new System.NotImplementedException();

    //public ICollection<TValue> Values => throw new System.NotImplementedException();

    ////public int Count => m_byKey.Count;

    ////public bool IsReadOnly => false;

    //public void Add(TKey key, TValue value) => throw new System.NotImplementedException();

    //public void Add(KeyValuePair<TKey, TValue> item) => throw new System.NotImplementedException();

    ////public void Clear() => m_byKey.Clear();

    //public bool Contains(KeyValuePair<TKey, TValue> item) => throw new System.NotImplementedException();

    //public bool ContainsKey(TKey key) => throw new System.NotImplementedException();

    //public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new System.NotImplementedException();

    //public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => throw new System.NotImplementedException();

    //public bool Remove(TKey key) => throw new System.NotImplementedException();

    //public bool Remove(KeyValuePair<TKey, TValue> item) => throw new System.NotImplementedException();

    //public bool TryGetValue(TKey key, out TValue value) => throw new System.NotImplementedException();

    //IEnumerator System.Collections.IEnumerable.GetEnumerator() => throw new System.NotImplementedException();
    //#endregion IDictionary

    //#region IList
    //public TValue this[int index] { get => m_byIndex[index]; set => m_byIndex[index] = value; }

    ////public int Count => m_byIndex.Count;

    ////public bool IsReadOnly => false;

    //public void Add(TValue item) => throw new System.NotImplementedException();

    ////public void Clear() => m_byIndex.Clear();

    //public bool Contains(TValue item) => throw new System.NotImplementedException();

    //public void CopyTo(TValue[] array, int arrayIndex) => throw new System.NotImplementedException();

    //public System.Collections.Generic.IEnumerator<TValue> GetEnumerator() => throw new System.NotImplementedException();

    //public int IndexOf(TValue item) => throw new System.NotImplementedException();

    //public void Insert(int index, TValue item) => throw new System.NotImplementedException();

    //public bool Remove(TValue item) => throw new System.NotImplementedException();

    //public void RemoveAt(int index) => throw new System.NotImplementedException();

    //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => throw new System.NotImplementedException();
    //#endregion IList

    //public void Insert(int index, TKey key, TValue value)
    //{
    //  // To Do: Implement
    //}

    public void Delete(int index)
    {
      lock (m_syncRoot)
      {
        m_byKey.Remove(m_byIndex[index].Key);
        m_byIndex.RemoveAt(index);
      }
    }
    public void Delete(TKey key)
    {
      lock (m_syncRoot)
      {
        m_byIndex.RemoveAt(FindIndexOfKey(key));
        m_byKey.Remove(key);
      }
    }
    public void Delete(TValue value)
    {
      lock (m_syncRoot)
      {
        m_byIndex.RemoveAt(FindIndexOfValue(value, null));
        m_byKey.Remove(FindKeyOfValue(value, null));
      }
    }

    public int FindIndexOfKey(TKey key)
    {
      for (int index = 0; index < m_byIndex.Count; index++)
        if (key.Equals(m_byIndex[index].Key))
          return index;

      return -1;
    }
    public int FindIndexOfValue(TValue value, System.Collections.Generic.IEqualityComparer<TValue>? comparer = null)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

      for (int index = 0; index < m_byIndex.Count; index++)
        if (comparer.Equals((TValue)value, m_byIndex[index].Value))
          return index;

      return -1;
    }
    public TKey FindKeyOfValue(TValue value, System.Collections.Generic.IEqualityComparer<TValue>? comparer = null)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

      for (int index = 0; index < m_byIndex.Count; index++)
        if (comparer.Equals((TValue)value, m_byIndex[index].Value))
          return m_byIndex[index].Key;

      throw new System.ArgumentOutOfRangeException(nameof(value), @"Value not found.");
    }

    #region IIndexedDictionary
    public object this[int index] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public object this[object key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool IsFixedSize => false;

    public bool IsReadOnly => false;

    public System.Collections.ICollection Keys => m_byKey.Keys;

    public System.Collections.ICollection Values => m_byKey.Values;

    public int Count => m_byIndex.Count;

    public bool IsSynchronized => true;

    public object SyncRoot => m_syncRoot;

    public void Add(object key, object value)
    {
      if (key is null) throw new System.ArgumentNullException(nameof(key));
      if (!(key is TKey)) throw new System.ArgumentException(@"Type mismatch.", nameof(key));
      if (Contains(key)) throw new System.ArgumentException(@"Duplicate key.", nameof(key));
      if (value is null) throw new System.ArgumentNullException(nameof(value));
      if (!(value is TValue)) throw new System.ArgumentException(@"Type mismatch.", nameof(value));

      lock (m_syncRoot)
      {
        m_byIndex.Add(new System.Collections.Generic.KeyValuePair<TKey, TValue>((TKey)key, (TValue)value));
        m_byKey.Add((TKey)key, (TValue)value);
      }
    }

    public void Clear()
    {
      lock (m_syncRoot)
      {
        m_byIndex.Clear();
        m_byKey.Clear();
      }
    }

    public bool Contains(object key)
    {
      if (key is null) throw new System.ArgumentNullException(nameof(key));

      if (key is TKey)
        lock (m_syncRoot)
        {
          return m_byKey.ContainsKey((TKey)key);
        }
      else throw new System.ArgumentException(@"Type mismatch.", nameof(key));
    }

    public void CopyTo(System.Array array, int index) => throw new System.NotImplementedException();

    public System.Collections.IDictionaryEnumerator GetEnumerator() => new IndexedDictionaryEnumerator(this);

    public void Insert(int index, object key, object value)
    {
      if (index < 0 || index >= m_byIndex.Count) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (key is null) throw new System.ArgumentNullException(nameof(key));
      if (!(key is TKey)) throw new System.ArgumentException(@"Type mismatch.", nameof(key));
      if (Contains(key)) throw new System.ArgumentException(@"Duplicate key.", nameof(key));
      if (value is null) throw new System.ArgumentNullException(nameof(value));
      if (!(value is TValue)) throw new System.ArgumentException(@"Type mismatch.", nameof(value));

      lock (m_syncRoot)
      {
        m_byIndex.Add(new System.Collections.Generic.KeyValuePair<TKey, TValue>((TKey)key, (TValue)value));
        m_byKey.Add((TKey)key, (TValue)value);
      }
    }

    public void Remove(object key)
    {
      if (key is null) throw new System.ArgumentNullException(nameof(key));
      if (!(key is TKey)) throw new System.ArgumentException(@"Type mismatch.", nameof(key));
      if (!Contains(key)) throw new System.ArgumentException(@"Key missing.", nameof(key));

      Delete((TKey)key);
    }

    public void RemoveAt(int index)
    {
      if (index < 0 || index >= m_byIndex.Count) throw new System.ArgumentOutOfRangeException(nameof(index));

      Delete(index);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion IOrderedDictionary

    private class IndexedDictionaryEnumerator
      : System.Collections.IDictionaryEnumerator
    {
      private readonly IndexedDictionary<TKey, TValue> m_id;

      private int m_index = -1;

      public IndexedDictionaryEnumerator(IndexedDictionary<TKey, TValue> id)
      {
        m_id = id;
      }

      public object Current
        => m_index >= 0 && m_index < m_id.Count ? m_id.m_byIndex[m_index] : throw new System.InvalidOperationException(@"Enumerator is out of sync.");

      public DictionaryEntry Entry
        => (DictionaryEntry)Current;

      public object Key
        => m_index >= 0 && m_index < m_id.Count ? m_id.m_byIndex[m_index].Key : throw new System.InvalidOperationException(@"Enumerator is out of sync.");

      public object Value
        => m_index >= 0 && m_index < m_id.Count ? m_id.m_byIndex[m_index].Value : throw new System.InvalidOperationException(@"Enumerator is out of sync.");

      public bool MoveNext()
      {
        if (m_index < m_id.Count - 1)
        {
          m_index++;
          return true;
        }

        return false;
      }

      public void Reset()
        => m_index = -1;
    }
  }
}

//namespace Flux.Collections.Generic
//{
//  public interface IIndexedDictionary<TKey, TValue>
//  {
//  }

//  public class test : System.Collections.Specialized.OrderedDictionary
//  {
//    public test()
//    {
//this.It      
//    }
//  }
//}

//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace FluxNs.Flux.Collections.Generic
//{ /// <summary>Represents a generic collection of key/value pairs that are ordered independently of the key and value.</summary>
//	public interface IOrderedDictionary<TKey, TValue>
//    : System.Collections.Specialized.IOrderedDictionary, System.Collections.Generic.IDictionary<TKey, TValue>
//  {
//    /// <summary>Adds an entry with the specified key and value into the <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> collection with the lowest available index.</summary>
//    /// <param name="key">The key of the entry to add.</param>
//    /// <param name="value">The value of the entry to add.</param>
//    /// <returns>The index of the newly added entry</returns>
//    /// <remarks>
//    /// <para>You can also use the <see cref="P:System.Collections.Generic.IDictionary{TKey,TValue}.Item(TKey)"/> property to add new elements by setting the value of a key that does not exist in the <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> collection; however, if the specified key already exists in the <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see>, setting the <see cref="P:Item(TKey)"/> property overwrites the old value. In contrast, the <see cref="M:Add"/> method does not modify existing elements.</para></remarks>
//    /// <exception cref="ArgumentException">An element with the same key already exists in the <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see></exception>
//    /// <exception cref="NotSupportedException">The <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> is read-only.<br/>
//    /// -or-<br/>
//    /// The <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> has a fized size.</exception>
//    new int Add(TKey key, TValue value);

//    /// <summary>
//    /// Inserts a new entry into the <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> collection with the specified key and value at the specified index.
//    /// </summary>
//    /// <param name="index">The zero-based index at which the element should be inserted.</param>
//    /// <param name="key">The key of the entry to add.</param>
//    /// <param name="value">The value of the entry to add. The value can be <null/> if the type of the values in the dictionary is a reference type.</param>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.<br/>
//    /// -or-<br/>
//    /// <paramref name="index"/> is greater than <see cref="System.Collections.ICollection.Count"/>.</exception>
//    /// <exception cref="ArgumentException">An element with the same key already exists in the <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see>.</exception>
//    /// <exception cref="NotSupportedException">The <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> is read-only.<br/>
//    /// -or-<br/>
//    /// The <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> has a fized size.</exception>
//    void Insert(int index, TKey key, TValue value);

//    /// <summary>
//    /// Gets or sets the value at the specified index.
//    /// </summary>
//    /// <param name="index">The zero-based index of the value to get or set.</param>
//    /// <value>The value of the item at the specified index.</value>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.<br/>
//    /// -or-<br/>
//    /// <paramref name="index"/> is equal to or greater than <see cref="System.Collections.ICollection.Count"/>.</exception>
//    new TValue this[int index]
//    {
//      get;
//      set;
//    }
//  }

//  /// <summary>
//  /// Represents a generic collection of key/value pairs that are ordered independently of the key and value.
//  /// </summary>
//  /// <typeparam name="TKey">The type of the keys in the dictionary</typeparam>
//  /// <typeparam name="TValue">The type of the values in the dictionary</typeparam>
//  public class OrderedDictionary<TKey, TValue>
//    : IOrderedDictionary<TKey, TValue>
//  {
//    private const int DefaultInitialCapacity = 0;

//    private static readonly string _keyTypeName = typeof(TKey).FullName;
//    private static readonly string _valueTypeName = typeof(TValue).FullName;
//    private static readonly bool _valueTypeIsReferenceType = !typeof(ValueType).IsAssignableFrom(typeof(TValue));

//    private Dictionary<TKey, TValue> _dictionary;
//    private List<KeyValuePair<TKey, TValue>> _list;
//    private IEqualityComparer<TKey> _comparer;
//    private object _syncRoot;
//    private int _initialCapacity;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> class.
//    /// </summary>
//    public OrderedDictionary()
//      : this(DefaultInitialCapacity, null)
//    {
//    }

//    /// <summary>
//    /// Initializes a new instance of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> class using the specified initial capacity.
//    /// </summary>
//    /// <param name="capacity">The initial number of elements that the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> can contain.</param>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0</exception>
//    public OrderedDictionary(int capacity)
//      : this(capacity, null)
//    {
//    }

//    /// <summary>
//    /// Initializes a new instance of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> class using the specified comparer.
//    /// </summary>
//    /// <param name="comparer">The <see cref="IEqualityComparer{TKey}">IEqualityComparer&lt;TKey&gt;</see> to use when comparing keys, or <null/> to use the default <see cref="EqualityComparer{TKey}">EqualityComparer&lt;TKey&gt;</see> for the type of the key.</param>
//    public OrderedDictionary(IEqualityComparer<TKey> comparer)
//      : this(DefaultInitialCapacity, comparer)
//    {
//    }

//    /// <summary>
//    /// Initializes a new instance of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> class using the specified initial capacity and comparer.
//    /// </summary>
//    /// <param name="capacity">The initial number of elements that the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection can contain.</param>
//    /// <param name="comparer">The <see cref="IEqualityComparer{TKey}">IEqualityComparer&lt;TKey&gt;</see> to use when comparing keys, or <null/> to use the default <see cref="EqualityComparer{TKey}">EqualityComparer&lt;TKey&gt;</see> for the type of the key.</param>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0</exception>
//    public OrderedDictionary(int capacity, IEqualityComparer<TKey> comparer)
//    {
//      if (0 > capacity)
//        throw new ArgumentOutOfRangeException("capacity", "'capacity' must be non-negative");

//      _initialCapacity = capacity;
//      _comparer = comparer;
//    }

//    /// <summary>
//    /// Converts the object passed as a key to the key type of the dictionary
//    /// </summary>
//    /// <param name="keyObject">The key object to check</param>
//    /// <returns>The key object, cast as the key type of the dictionary</returns>
//    /// <exception cref="ArgumentNullException"><paramref name="keyObject"/> is <null/>.</exception>
//    /// <exception cref="ArgumentException">The key type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is not in the inheritance hierarchy of <paramref name="keyObject"/>.</exception>
//    private static TKey ConvertToKeyType(object keyObject)
//    {
//      if (null == keyObject)
//      {
//        throw new ArgumentNullException("key");
//      }
//      else
//      {
//        if (keyObject is TKey)
//          return (TKey)keyObject;
//      }
//      throw new ArgumentException("'key' must be of type " + _keyTypeName, "key");
//    }

//    /// <summary>
//    /// Converts the object passed as a value to the value type of the dictionary
//    /// </summary>
//    /// <param name="value">The object to convert to the value type of the dictionary</param>
//    /// <returns>The value object, converted to the value type of the dictionary</returns>
//    /// <exception cref="ArgumentNullException"><paramref name="valueObject"/> is <null/>, and the value type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is a value type.</exception>
//    /// <exception cref="ArgumentException">The value type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is not in the inheritance hierarchy of <paramref name="valueObject"/>.</exception>
//    private static TValue ConvertToValueType(object value)
//    {
//      if (null == value)
//      {
//        if (_valueTypeIsReferenceType)
//          return default(TValue);
//        else
//          throw new ArgumentNullException("value");
//      }
//      else
//      {
//        if (value is TValue)
//          return (TValue)value;
//      }
//      throw new ArgumentException("'value' must be of type " + _valueTypeName, "value");
//    }

//    /// <summary>
//    /// Gets the dictionary object that stores the keys and values
//    /// </summary>
//    /// <value>The dictionary object that stores the keys and values for the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see></value>
//    /// <remarks>Accessing this property will create the dictionary object if necessary</remarks>
//    private Dictionary<TKey, TValue> Dictionary => _dictionary ??= new Dictionary<TKey, TValue>(_initialCapacity, _comparer)

//    /// <summary>
//    /// Gets the list object that stores the key/value pairs.
//    /// </summary>
//    /// <value>The list object that stores the key/value pairs for the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see></value>
//    /// <remarks>Accessing this property will create the list object if necessary.</remarks>
//    private List<KeyValuePair<TKey, TValue>> List => _list ??= new List<KeyValuePair<TKey, TValue>>(_initialCapacity);

//    System.Collections.IDictionaryEnumerator System.Collections.Specialized.IOrderedDictionary.GetEnumerator() => Dictionary.GetEnumerator();

//    System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator() => Dictionary.GetEnumerator();

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => List.GetEnumerator();

//    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => List.GetEnumerator();

//    /// <summary>
//    /// Inserts a new entry into the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection with the specified key and value at the specified index.
//    /// </summary>
//    /// <param name="index">The zero-based index at which the element should be inserted.</param>
//    /// <param name="key">The key of the entry to add.</param>
//    /// <param name="value">The value of the entry to add. The value can be <null/> if the type of the values in the dictionary is a reference type.</param>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.<br/>
//    /// -or-<br/>
//    /// <paramref name="index"/> is greater than <see cref="Count"/>.</exception>
//    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <null/>.</exception>
//    /// <exception cref="ArgumentException">An element with the same key already exists in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.</exception>
//    public void Insert(int index, TKey key, TValue value)
//    {
//      if (index > Count || index < 0)
//        throw new ArgumentOutOfRangeException("index");

//      Dictionary.Add(key, value);
//      List.Insert(index, new KeyValuePair<TKey, TValue>(key, value));
//    }

//    /// <summary>
//    /// Inserts a new entry into the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection with the specified key and value at the specified index.
//    /// </summary>
//    /// <param name="index">The zero-based index at which the element should be inserted.</param>
//    /// <param name="key">The key of the entry to add.</param>
//    /// <param name="value">The value of the entry to add. The value can be <null/> if the type of the values in the dictionary is a reference type.</param>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.<br/>
//    /// -or-<br/>
//    /// <paramref name="index"/> is greater than <see cref="Count"/>.</exception>
//    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <null/>.<br/>
//    /// -or-<br/>
//    /// <paramref name="value"/> is <null/>, and the value type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is a value type.</exception>
//    /// <exception cref="ArgumentException">The key type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is not in the inheritance hierarchy of <paramref name="key"/>.<br/>
//    /// -or-<br/>
//    /// The value type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is not in the inheritance hierarchy of <paramref name="value"/>.<br/>
//    /// -or-<br/>
//    /// An element with the same key already exists in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.</exception>
//    void System.Collections.Specialized.IOrderedDictionary.Insert(int index, object key, object value) => Insert(index, ConvertToKeyType(key), ConvertToValueType(value));

//    /// <summary>
//    /// Removes the entry at the specified index from the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.
//    /// </summary>
//    /// <param name="index">The zero-based index of the entry to remove.</param>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.<br/>
//    /// -or-<br/>
//    /// index is equal to or greater than <see cref="Count"/>.</exception>
//    public void RemoveAt(int index)
//    {
//      if (index >= Count || index < 0)
//        throw new ArgumentOutOfRangeException("index", "'index' must be non-negative and less than the size of the collection");

//      TKey key = List[index].Key;

//      List.RemoveAt(index);
//      Dictionary.Remove(key);
//    }

//    /// <summary>
//    /// Gets or sets the value at the specified index.
//    /// </summary>
//    /// <param name="index">The zero-based index of the value to get or set.</param>
//    /// <value>The value of the item at the specified index.</value>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.<br/>
//    /// -or-<br/>
//    /// index is equal to or greater than <see cref="Count"/>.</exception>
//    public TValue this[int index]
//    {
//      get
//      {
//        return List[index].Value;
//      }

//      set
//      {
//        if (index >= Count || index < 0)
//          throw new ArgumentOutOfRangeException("index", "'index' must be non-negative and less than the size of the collection");

//        TKey key = List[index].Key;

//        List[index] = new KeyValuePair<TKey, TValue>(key, value);
//        Dictionary[key] = value;
//      }
//    }

//    /// <summary>
//    /// Gets or sets the value at the specified index.
//    /// </summary>
//    /// <param name="index">The zero-based index of the value to get or set.</param>
//    /// <value>The value of the item at the specified index.</value>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.<br/>
//    /// -or-<br/>
//    /// index is equal to or greater than <see cref="Count"/>.</exception>
//    /// <exception cref="ArgumentNullException"><paramref name="valueObject"/> is a null reference, and the value type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is a value type.</exception>
//    /// <exception cref="ArgumentException">The value type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is not in the inheritance hierarchy of <paramref name="valueObject"/>.</exception>
//    object System.Collections.Specialized.IOrderedDictionary.this[int index] { get => this[index]; set => this[index] = ConvertToValueType(value); }

//    /// <summary>
//    /// Adds an entry with the specified key and value into the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection with the lowest available index.
//    /// </summary>
//    /// <param name="key">The key of the entry to add.</param>
//    /// <param name="value">The value of the entry to add. This value can be <null/>.</param>
//    /// <remarks>A key cannot be <null/>, but a value can be.
//    /// <para>You can also use the <see cref="P:OrderedDictionary{TKey,TValue}.Item(TKey)"/> property to add new elements by setting the value of a key that does not exist in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection; however, if the specified key already exists in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>, setting the <see cref="P:OrderedDictionary{TKey,TValue}.Item(TKey)"/> property overwrites the old value. In contrast, the <see cref="M:Add"/> method does not modify existing elements.</para></remarks>
//    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <null/></exception>
//    /// <exception cref="ArgumentException">An element with the same key already exists in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see></exception>
//    void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);

//    /// <summary>
//    /// Adds an entry with the specified key and value into the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection with the lowest available index.
//    /// </summary>
//    /// <param name="key">The key of the entry to add.</param>
//    /// <param name="value">The value of the entry to add. This value can be <null/>.</param>
//    /// <returns>The index of the newly added entry</returns>
//    /// <remarks>A key cannot be <null/>, but a value can be.
//    /// <para>You can also use the <see cref="P:OrderedDictionary{TKey,TValue}.Item(TKey)"/> property to add new elements by setting the value of a key that does not exist in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection; however, if the specified key already exists in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>, setting the <see cref="P:OrderedDictionary{TKey,TValue}.Item(TKey)"/> property overwrites the old value. In contrast, the <see cref="M:Add"/> method does not modify existing elements.</para></remarks>
//    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <null/></exception>
//    /// <exception cref="ArgumentException">An element with the same key already exists in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see></exception>
//    public int Add(TKey key, TValue value)
//    {
//      Dictionary.Add(key, value);
//      List.Add(new KeyValuePair<TKey, TValue>(key, value));
//      return Count - 1;
//    }

//    /// <summary>
//    /// Adds an entry with the specified key and value into the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection with the lowest available index.
//    /// </summary>
//    /// <param name="key">The key of the entry to add.</param>
//    /// <param name="value">The value of the entry to add. This value can be <null/>.</param>
//    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <null/>.<br/>
//    /// -or-<br/>
//    /// <paramref name="value"/> is <null/>, and the value type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is a value type.</exception>
//    /// <exception cref="ArgumentException">The key type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is not in the inheritance hierarchy of <paramref name="key"/>.<br/>
//    /// -or-<br/>
//    /// The value type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is not in the inheritance hierarchy of <paramref name="value"/>.</exception>
//    void System.Collections.IDictionary.Add(object key, object value) => Add(ConvertToKeyType(key), ConvertToValueType(value));

//    /// <summary>
//    /// Removes all elements from the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.
//    /// </summary>
//    /// <remarks>The capacity is not changed as a result of calling this method.</remarks>
//    public void Clear()
//    {
//      Dictionary.Clear();
//      List.Clear();
//    }

//    /// <summary>
//    /// Determines whether the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection contains a specific key.
//    /// </summary>
//    /// <param name="key">The key to locate in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.</param>
//    /// <returns><see langword="true"/> if the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
//    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <null/></exception>
//    public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

//    /// <summary>
//    /// Determines whether the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection contains a specific key.
//    /// </summary>
//    /// <param name="key">The key to locate in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.</param>
//    /// <returns><see langword="true"/> if the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
//    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <null/></exception>
//    /// <exception cref="ArgumentException">The key type of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is not in the inheritance hierarchy of <paramref name="key"/>.</exception>
//    bool System.Collections.IDictionary.Contains(object key) => ContainsKey(ConvertToKeyType(key));

//    /// <summary>
//    /// Gets a value indicating whether the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> has a fixed size.
//    /// </summary>
//    /// <value><see langword="true"/> if the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> has a fixed size; otherwise, <see langword="false"/>. The default is <see langword="false"/>.</value>
//    bool System.Collections.IDictionary.IsFixedSize => false;

//    /// <summary>
//    /// Gets a value indicating whether the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection is read-only.
//    /// </summary>
//    /// <value><see langword="true"/> if the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> is read-only; otherwise, <see langword="false"/>. The default is <see langword="false"/>.</value>
//    /// <remarks>
//    /// A collection that is read-only does not allow the addition, removal, or modification of elements after the collection is created.
//    /// <para>A collection that is read-only is simply a collection with a wrapper that prevents modification of the collection; therefore, if changes are made to the underlying collection, the read-only collection reflects those changes.</para>
//    /// </remarks>
//    public bool IsReadOnly => false;

//    /// <summary>
//    /// Gets an <see cref="ICollection"/> object containing the keys in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.
//    /// </summary>
//    /// <value>An <see cref="ICollection"/> object containing the keys in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.</value>
//    /// <remarks>The returned <see cref="ICollection"/> object is not a static copy; instead, the collection refers back to the keys in the original <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>. Therefore, changes to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> continue to be reflected in the key collection.</remarks>
//    System.Collections.ICollection System.Collections.IDictionary.Keys => (System.Collections.ICollection)Keys;

//    /// <summary>
//    /// Returns the zero-based index of the specified key in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>
//    /// </summary>
//    /// <param name="key">The key to locate in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see></param>
//    /// <returns>The zero-based index of <paramref name="key"/>, if <paramref name="ley"/> is found in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>; otherwise, -1</returns>
//    /// <remarks>This method performs a linear search; therefore it has a cost of O(n) at worst.</remarks>
//    public int IndexOfKey(TKey key)
//    {
//      if (null == key)
//        throw new ArgumentNullException("key");

//      for (int index = 0; index < List.Count; index++)
//      {
//        KeyValuePair<TKey, TValue> entry = List[index];
//        TKey next = entry.Key;
//        if (null != _comparer)
//        {
//          if (_comparer.Equals(next, key))
//          {
//            return index;
//          }
//        }
//        else if (next.Equals(key))
//        {
//          return index;
//        }
//      }

//      return -1;
//    }

//    /// <summary>
//    /// Removes the entry with the specified key from the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.
//    /// </summary>
//    /// <param name="key">The key of the entry to remove</param>
//    /// <returns><see langword="true"/> if the key was found and the corresponding element was removed; otherwise, <see langword="false"/></returns>
//    public bool Remove(TKey key)
//    {
//      if (null == key)
//        throw new ArgumentNullException("key");

//      int index = IndexOfKey(key);
//      if (index >= 0)
//      {
//        if (Dictionary.Remove(key))
//        {
//          List.RemoveAt(index);
//          return true;
//        }
//      }
//      return false;
//    }

//    /// <summary>
//    /// Removes the entry with the specified key from the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.
//    /// </summary>
//    /// <param name="key">The key of the entry to remove</param>
//    void System.Collections.IDictionary.Remove(object key) => Remove(ConvertToKeyType(key));

//    /// <summary>
//    /// Gets an <see cref="ICollection"/> object containing the values in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.
//    /// </summary>
//    /// <value>An <see cref="ICollection"/> object containing the values in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.</value>
//    /// <remarks>The returned <see cref="ICollection"/> object is not a static copy; instead, the <see cref="ICollection"/> refers back to the values in the original <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection. Therefore, changes to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> continue to be reflected in the <see cref="ICollection"/>.</remarks>
//    System.Collections.ICollection System.Collections.IDictionary.Values => (System.Collections.ICollection)Values;

//    /// <summary>
//    /// Gets or sets the value with the specified key.
//    /// </summary>
//    /// <param name="key">The key of the value to get or set.</param>
//    /// <value>The value associated with the specified key. If the specified key is not found, attempting to get it returns <null/>, and attempting to set it creates a new element using the specified key.</value>
//    public TValue this[TKey key]
//    {
//      get => Dictionary[key];
//      set
//      {
//        if (Dictionary.ContainsKey(key))
//        {
//          Dictionary[key] = value;
//          List[IndexOfKey(key)] = new KeyValuePair<TKey, TValue>(key, value);
//        }
//        else
//        {
//          Add(key, value);
//        }
//      }
//    }

//    /// <summary>
//    /// Gets or sets the value with the specified key.
//    /// </summary>
//    /// <param name="key">The key of the value to get or set.</param>
//    /// <value>The value associated with the specified key. If the specified key is not found, attempting to get it returns <null/>, and attempting to set it creates a new element using the specified key.</value>
//    object System.Collections.IDictionary.this[object key] { get => this[ConvertToKeyType(key)]; set => this[ConvertToKeyType(key)] = ConvertToValueType(value); }

//    /// <summary>
//    /// Copies the elements of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> elements to a one-dimensional Array object at the specified index.
//    /// </summary>
//    /// <param name="array">The one-dimensional <see cref="Array"/> object that is the destination of the <see cref="T:KeyValuePair`2>"/> objects copied from the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>. The <see cref="Array"/> must have zero-based indexing.</param>
//    /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
//    /// <remarks>The <see cref="M:CopyTo"/> method preserves the order of the elements in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see></remarks>
//    void System.Collections.ICollection.CopyTo(Array array, int index) => ((System.Collections.ICollection)List).CopyTo(array, index);

//    /// <summary>
//    /// Gets the number of key/values pairs contained in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.
//    /// </summary>
//    /// <value>The number of key/value pairs contained in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> collection.</value>
//    public int Count => List.Count;

//    /// <summary>
//    /// Gets a value indicating whether access to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> object is synchronized (thread-safe).
//    /// </summary>
//    /// <value>This method always returns false.</value>
//    bool System.Collections.ICollection.IsSynchronized => false;

//    /// <summary>
//    /// Gets an object that can be used to synchronize access to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> object.
//    /// </summary>
//    /// <value>An object that can be used to synchronize access to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> object.</value>
//    object System.Collections.ICollection.SyncRoot
//    {
//      get
//      {
//        if (this._syncRoot == null)
//        {
//          System.Threading.Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
//        }
//        return this._syncRoot;
//      }
//    }

//    /// <summary>
//    /// Gets an <see cref="T:System.Collections.Generic.ICollection{TKey}">ICollection&lt;TKey&gt;</see> object containing the keys in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.
//    /// </summary>
//    /// <value>An <see cref="T:System.Collections.Generic.ICollection{TKey}">ICollection&lt;TKey&gt;</see> object containing the keys in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.</value>
//    /// <remarks>The returned <see cref="T:System.Collections.Generic.ICollection{TKey}">ICollection&lt;TKey&gt;</see> object is not a static copy; instead, the collection refers back to the keys in the original <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>. Therefore, changes to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> continue to be reflected in the key collection.</remarks>
//    public ICollection<TKey> Keys => Dictionary.Keys;

//    /// <summary>
//    /// Gets the value associated with the specified key.
//    /// </summary>
//    /// <param name="key">The key of the value to get.</param>
//    /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of <paramref name="value"/>. This parameter can be passed uninitialized.</param>
//    /// <returns><see langword="true"/> if the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
//    public bool TryGetValue(TKey key, out TValue value) => Dictionary.TryGetValue(key, out value);

//    /// <summary>
//    /// Gets an <see cref="T:ICollection{TValue}">ICollection&lt;TValue&gt;</see> object containing the values in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.
//    /// </summary>
//    /// <value>An <see cref="T:ICollection{TValue}">ICollection&lt;TValue&gt;</see> object containing the values in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.</value>
//    /// <remarks>The returned <see cref="T:ICollection{TValue}">ICollection&lt;TKey&gt;</see> object is not a static copy; instead, the collection refers back to the values in the original <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>. Therefore, changes to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> continue to be reflected in the value collection.</remarks>
//    public ICollection<TValue> Values => Dictionary.Values;

//    /// <summary>Adds the specified value to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> with the specified key.</summary>
//    /// <param name="item">The <see cref="T:KeyValuePair{TKey,TValue}">KeyValuePair&lt;TKey,TValue&gt;</see> structure representing the key and value to add to the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.</param>
//    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

//    /// <summary>Determines whether the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> contains a specific key and value.</summary>
//    /// <param name="item">The <see cref="T:KeyValuePair{TKey,TValue}">KeyValuePair&lt;TKey,TValue&gt;</see> structure to locate in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>.</param>
//    /// <returns><see langword="true"/> if <paramref name="keyValuePair"/> is found in the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>; otherwise, <see langword="false"/>.</returns>
//    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Contains(item);

//    /// <summary>Copies the elements of the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see> to an array of type <see cref="T:KeyValuePair`2>"/>, starting at the specified index.</summary>
//    /// <param name="array">The one-dimensional array of type <see cref="T:KeyValuePair{TKey,TValue}">KeyValuePair&lt;TKey,TValue&gt;</see> that is the destination of the <see cref="T:KeyValuePair{TKey,TValue}">KeyValuePair&lt;TKey,TValue&gt;</see> elements copied from the <see cref="OrderedDictionary{TKey,TValue}">OrderedDictionary&lt;TKey,TValue&gt;</see>. The array must have zero-based indexing.</param>
//    /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
//    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex);

//    /// <summary>Removes a key and value from the dictionary.</summary>
//    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
//  }
//}
