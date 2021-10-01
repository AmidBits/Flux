namespace Flux.DataStructures
{
  public class OrderedSet<T>
    : System.Collections.Generic.ISet<T>
    where T : notnull
  {
    private readonly System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;
    private readonly System.Collections.Generic.IDictionary<T, System.Collections.Generic.LinkedListNode<T>> m_dictionary;
    private readonly System.Collections.Generic.LinkedList<T> m_linkedList;

    public OrderedSet(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      m_equalityComparer = equalityComparer;
      m_dictionary = new System.Collections.Generic.Dictionary<T, System.Collections.Generic.LinkedListNode<T>>(equalityComparer);
      m_linkedList = new System.Collections.Generic.LinkedList<T>();
    }
    public OrderedSet()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }
    public OrderedSet(System.Collections.Generic.IEnumerable<T> collection, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : this(equalityComparer)
    {
      foreach (var c in collection)
        if (!Contains(c))
          Add(c);
    }
    public OrderedSet(System.Collections.Generic.IEnumerable<T> collection)
      : this(collection, System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    public int Count
      => m_dictionary.Count;

    public virtual bool IsReadOnly
      => m_dictionary.IsReadOnly;

    public bool Add(T item)
    {
      if (m_dictionary.ContainsKey(item))
        return false;

      var node = m_linkedList.AddLast(item);
      m_dictionary.Add(item, node);
      return true;
    }
    void System.Collections.Generic.ICollection<T>.Add(T item)
      => Add(item);
    public int AddRange(System.Collections.Generic.IEnumerable<T> collection)
    {
      var count = 0;

      foreach (var t in collection)
        if (Add(t))
          count++;

      return count;
    }

    public void Clear()
    {
      m_linkedList.Clear();
      m_dictionary.Clear();
    }

    public bool Contains(T item)
      => m_dictionary.ContainsKey(item);

    public void CopyTo(T[] array, int arrayIndex)
        => m_linkedList.CopyTo(array, arrayIndex);

    //private (int unfoundCount, int uniqueCount) GetCounts(System.Collections.Generic.IEnumerable<T> other, bool returnIfUnfound)
    //{
    //  var unique = new System.Collections.Generic.HashSet<T>();

    //  var unfoundCount = 0;

    //  foreach (var t in other)
    //  {
    //    if (Contains(t))
    //    {
    //      if (!unique.Contains(t))
    //        unique.Add(t);
    //    }
    //    else
    //    {
    //      unfoundCount++;

    //      if (returnIfUnfound)
    //        break;
    //    }
    //  }

    //  return (unfoundCount, unique.Count);
    //}

    /// <summary>Removes all elements in the specified collection from the current set.</summary>
    public void ExceptWith(System.Collections.Generic.IEnumerable<T> other)
      => RemoveRange(other);

    public bool Remove(T item)
    {
      if (!m_dictionary.TryGetValue(item, out var node))
        return false;

      m_dictionary.Remove(item);
      m_linkedList.Remove(node);
      return true;
    }
    public int RemoveRange(System.Collections.Generic.IEnumerable<T> collection)
    {
      var count = 0;

      foreach (var t in collection)
        if (Remove(t))
          count++;

      return count;
    }

    /// <summary>Modifies the current set so that it contains only elements that are also in the specified collection.</summary>
    public void IntersectWith(System.Collections.Generic.IEnumerable<T> other)
    {
      var removing = new System.Collections.Generic.HashSet<T>(this, m_equalityComparer); // Order does not matter for removal, so we can use the built-in type.
      removing.ExceptWith(other);
      RemoveRange(removing);
    }

    public bool IsProperSubsetOf(System.Collections.Generic.IEnumerable<T> other)
      => SetOps.ComputeCounts(this, other, false) is var (uniqueCount, unfoundCount) && unfoundCount > 0 && uniqueCount == Count;
    public bool IsProperSupersetOf(System.Collections.Generic.IEnumerable<T> other)
      => SetOps.ComputeCounts(this, other, true) is var (uniqueCount, unfoundCount) && unfoundCount == 0 && uniqueCount < Count;

    public bool IsSubsetOf(System.Collections.Generic.IEnumerable<T> other)
      => SetOps.ComputeCounts(this, other, false) is var (uniqueCount, unfoundCount) && unfoundCount >= 0 && uniqueCount == Count;
    public bool IsSupersetOf(System.Collections.Generic.IEnumerable<T> other)
      => SetOps.ContainsAll(this, other);

    /// <summary>Determines whether the current set overlaps with the specified collection.</summary>
    public bool Overlaps(System.Collections.Generic.IEnumerable<T> other)
      => this.ContainsAny(other);

    public bool SetEquals(System.Collections.Generic.IEnumerable<T> other)
      => SetOps.ComputeCounts(this, other, true) is var (uniqueCount, unfoundCount) && unfoundCount == 0 && uniqueCount == Count;

    /// <summary>Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.</summary>
    public void SymmetricExceptWith(System.Collections.Generic.IEnumerable<T> other)
    {
      var adding = new OrderedSet<T>(m_equalityComparer);

      foreach (var o in other)
        if (Contains(o))
          Remove(o);
        else if (!adding.Contains(o))
          adding.Add(o);

      AddRange(adding);
    }

    /// <summary>Modifies the current set so that it contains all elements that are present in the current set, in the specified collection, or in both.</summary>
    public void UnionWith(System.Collections.Generic.IEnumerable<T> other)
      => AddRange(other);

    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
      => m_linkedList.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}