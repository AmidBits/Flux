namespace Flux.DataStructures
{
  /// <summary>
  /// <para>This is an ordered set implementing <see cref="IOrderedSet{TValue}"/>.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  /// <remarks>An ordered data structure maintains an indexed order, i.e. like a <see cref="System.Collections.Generic.List{TValue}"/> or a <typeparamref name="TValue"/>[].</remarks>
  public sealed class OrderedSet<TValue>
    : IOrderedSet<TValue>
    where TValue : notnull
  {
    private readonly System.Collections.Generic.IEqualityComparer<TValue> m_equalityComparer;

    private readonly System.Collections.Generic.Dictionary<TValue, int> m_dictionary;
    private readonly System.Collections.Generic.List<TValue> m_values;

    public OrderedSet(System.Collections.Generic.IEqualityComparer<TValue> equalityComparer)
    {
      m_equalityComparer = equalityComparer;
      m_dictionary = new System.Collections.Generic.Dictionary<TValue, int>(equalityComparer);
      m_values = new();
    }
    public OrderedSet()
      : this(System.Collections.Generic.EqualityComparer<TValue>.Default)
    { }
    public OrderedSet(System.Collections.Generic.IEnumerable<TValue> collection, System.Collections.Generic.IEqualityComparer<TValue> equalityComparer)
      : this(equalityComparer)
      => AddRange(collection);
    public OrderedSet(System.Collections.Generic.IEnumerable<TValue> collection)
      : this(collection, System.Collections.Generic.EqualityComparer<TValue>.Default)
    { }

    #region Implemented interfaces

    #region IOrderedSet<>

    public TValue this[int index]
    {
      get => m_values[index];
      set => m_values[index] = value;
    }

    public int AddRange(System.Collections.Generic.IEnumerable<TValue> collection)
    {
      var count = 0;

      foreach (var t in collection)
        if (Add(t))
          count++;

      return count;
    }

    public void Insert(int index, TValue value)
    {
      if (index < 0 || index > Count) throw new System.ArgumentOutOfRangeException(nameof(index));

      m_dictionary.Add(value, index);
      m_values.Insert(index, value);
    }

    public int InsertRange(int index, System.Collections.Generic.IEnumerable<TValue> collection)
    {
      var count = 0;

      foreach (var item in collection)
        if (!m_dictionary.ContainsKey(item))
        {
          Insert(index++, item);
          count++;
        }

      return count;
    }

    public void RemoveAt(int index)
    {
      if (index < 0 || index > Count) throw new System.ArgumentOutOfRangeException(nameof(index));

      m_dictionary.Remove(m_values[index]);
      m_values.RemoveAt(index);
    }

    public int RemoveRange(System.Collections.Generic.IEnumerable<TValue> collection)
    {
      var count = 0;

      foreach (var t in collection)
        if (Remove(t))
          count++;

      return count;
    }

    public bool TryGetIndex(TValue value, out int index)
      => m_dictionary.TryGetValue(value, out index);

    #endregion // IOrderedSet<>

    #region ICollection<>

    public int Count => m_dictionary.Count;

    public bool IsReadOnly => false;

    void System.Collections.Generic.ICollection<TValue>.Add(TValue item) => Add(item);

    public void Clear()
    {
      m_values.Clear();
      m_dictionary.Clear();
    }

    public bool Contains(TValue item) => m_dictionary.ContainsKey(item);

    public void CopyTo(TValue[] array, int arrayIndex) => m_values.CopyTo(array, arrayIndex);

    public bool Remove(TValue item)
    {
      if (!m_dictionary.TryGetValue(item, out var node))
        return false;

      m_dictionary.Remove(item);
      m_values.RemoveAt(node);
      return true;
    }

    #endregion // ICollection<>

    #region ISet<>

    public bool Add(TValue item)
    {
      if (m_dictionary.ContainsKey(item))
        return false;

      var node = m_values.Count;
      m_values.Add(item);
      m_dictionary.Add(item, node);
      return true;
    }

    public void ExceptWith(System.Collections.Generic.IEnumerable<TValue> other) => RemoveRange(other);

    public void IntersectWith(System.Collections.Generic.IEnumerable<TValue> other)
    {
      var removing = new System.Collections.Generic.HashSet<TValue>(this, m_equalityComparer); // Order does not matter for removal, so we can use the built-in type.
      removing.ExceptWith(other);
      RemoveRange(removing);
    }

    public bool IsProperSubsetOf(System.Collections.Generic.IEnumerable<TValue> other)
      => this.SetCounts(other, false) is var (unfoundCount, uniqueCount) && unfoundCount > 0 && uniqueCount == Count;

    public bool IsProperSupersetOf(System.Collections.Generic.IEnumerable<TValue> other)
      => this.SetCounts(other, true) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount < Count;

    public bool IsSubsetOf(System.Collections.Generic.IEnumerable<TValue> other)
      => this.SetCounts(other, false) is var (unfoundCount, uniqueCount) && unfoundCount >= 0 && uniqueCount == Count;

    public bool IsSupersetOf(System.Collections.Generic.IEnumerable<TValue> other) => this.ContainsAll(other);

    public bool Overlaps(System.Collections.Generic.IEnumerable<TValue> other) => this.ContainsAny(other);

    public bool SetEquals(System.Collections.Generic.IEnumerable<TValue> other)
      => this.SetCounts(other, true) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount == Count;

    public void SymmetricExceptWith(System.Collections.Generic.IEnumerable<TValue> other)
    {
      var adding = new OrderedSet<TValue>(m_equalityComparer);

      foreach (var o in other)
        if (Contains(o))
          Remove(o);
        else
          adding.Add(o);

      AddRange(adding);
    }

    public void UnionWith(System.Collections.Generic.IEnumerable<TValue> other) => AddRange(other);

    #endregion // ISet<>

    #region IEnumerable<>

    public System.Collections.Generic.IEnumerator<TValue> GetEnumerator() => m_values.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion // IEnumerable<>

    #endregion // Implemented interfaces

    public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";
  }
}
