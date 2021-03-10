using System.Linq;

namespace Flux.Collections.Immutable
{
  // https://blogs.msdn.microsoft.com/bclteam/2012/12/18/preview-of-immutable-collections-released-on-nuget/

  public class SortedDictionary<TKey, TValue>
    : IReadOnlyDictionary<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    public static readonly IReadOnlyDictionary<TKey, TValue> Empty = new EmptyDictionary();

    private readonly IBinarySearchTree<TKey, TValue> m_tree = AvlTree<TKey, TValue>.Empty;

    private SortedDictionary(IBinarySearchTree<TKey, TValue> tree) => m_tree = tree;

    public TValue this[TKey key] => m_tree.Lookup(key);
    public bool IsEmpty => false;
    public IReadOnlyDictionary<TKey, TValue> Add(TKey key, TValue value) => new SortedDictionary<TKey, TValue>(m_tree.Add(key, value));
    public IReadOnlyDictionary<TKey, TValue> Remove(TKey key) => new SortedDictionary<TKey, TValue>(m_tree.Remove(key));

    // IMap<K, V>
    public System.Collections.Generic.IEnumerable<TKey> Keys => m_tree.Keys;
    public System.Collections.Generic.IEnumerable<TValue> Values => m_tree.Values;
    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs => m_tree.Pairs;
    public bool Contains(TKey key) => m_tree.Contains(key);
    public TValue Lookup(TKey key) => this[key];
    IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);
    IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key) => Remove(key);

    // System.Collections.Generic.IReadOnlyDictionary<K, V>
    public int Count => m_tree.GetNodeCount();
    public bool ContainsKey(TKey key) => Contains(key);
    public bool TryGetValue(TKey key, out TValue value)
    {
      try
      {
        value = Lookup(key);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      value = default!;
      return false;
    }
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator() => m_tree.GetNodesInOrder().Select(bst => new System.Collections.Generic.KeyValuePair<TKey, TValue>(bst.Key, bst.Value)).GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    private class EmptyDictionary : IReadOnlyDictionary<TKey, TValue>
    {
      // IDictionary<K, V>
      public TValue this[TKey key] => throw new System.Exception(nameof(EmptyDictionary));
      public bool IsEmpty => true;
      public IReadOnlyDictionary<TKey, TValue> Add(TKey key, TValue value) => new SortedDictionary<TKey, TValue>(AvlTree<TKey, TValue>.Empty.Add(key, value));
      public IReadOnlyDictionary<TKey, TValue> Remove(TKey key) => throw new System.Exception(nameof(EmptyDictionary));

      // IMap<K, V>
      public System.Collections.Generic.IEnumerable<TKey> Keys { get { yield break; } }
      public System.Collections.Generic.IEnumerable<TValue> Values { get { yield break; } }
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs { get { yield break; } }
      public bool Contains(TKey key) => false;
      public TValue Lookup(TKey key) => throw new System.Exception(nameof(EmptyDictionary));
      IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);
      IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key) => Remove(key);

      // ReadOnlyDictionary<K, V>
      public int Count => 0;
      public bool ContainsKey(TKey key) => false;
      public bool TryGetValue(TKey key, out TValue value)
      {
        value = default!;
        return false;
      }
      public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator() { yield break; }
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { yield break; }
    }
  }
}
