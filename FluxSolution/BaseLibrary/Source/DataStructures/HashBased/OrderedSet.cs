namespace Flux
{
  public static partial class Em
  {
    public static DataStructures.OrderedSet<T> ToOrderedSet<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
      => new(source, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
  }

  namespace DataStructures
  {
    /// <summary>
    /// <para>This is an ordered set implementing <see cref="IOrderedSet{T}"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OrderedSet<T>
      : IOrderedSet<T>
      where T : notnull
    {
      private readonly System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;

      private readonly System.Collections.Generic.Dictionary<T, int> m_dictionary;
      private readonly System.Collections.Generic.List<T> m_values;

      public OrderedSet(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      {
        m_equalityComparer = equalityComparer;
        m_dictionary = new System.Collections.Generic.Dictionary<T, int>(equalityComparer);
        m_values = new();
      }
      public OrderedSet()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      { }
      public OrderedSet(System.Collections.Generic.IEnumerable<T> collection, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        : this(equalityComparer)
        => AddRange(collection);
      public OrderedSet(System.Collections.Generic.IEnumerable<T> collection)
        : this(collection, System.Collections.Generic.EqualityComparer<T>.Default)
      { }

      #region Implemented interfaces

      // IOrderedSet<>

      public T this[int index]
      {
        get => m_values[index];
        set => m_values[index] = value;
      }

      public int AddRange(System.Collections.Generic.IEnumerable<T> collection)
      {
        var count = 0;

        foreach (var t in collection)
          if (Add(t))
            count++;

        return count;
      }

      public void Insert(int index, T value)
      {
        if (index < 0 || index > Count) throw new System.ArgumentOutOfRangeException(nameof(index));

        m_dictionary.Add(value, index);
        m_values.Insert(index, value);
      }

      public int InsertRange(int index, System.Collections.Generic.IEnumerable<T> collection)
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

      public int RemoveRange(System.Collections.Generic.IEnumerable<T> collection)
      {
        var count = 0;

        foreach (var t in collection)
          if (Remove(t))
            count++;

        return count;
      }

      public bool TryGetIndex(T value, out int index)
        => m_dictionary.TryGetValue(value, out index);

      // ICollection<>

      public int Count => m_dictionary.Count;

      public bool IsReadOnly => false;

      void System.Collections.Generic.ICollection<T>.Add(T item) => Add(item);

      public void Clear()
      {
        m_values.Clear();
        m_dictionary.Clear();
      }

      public bool Contains(T item) => m_dictionary.ContainsKey(item);

      public void CopyTo(T[] array, int arrayIndex) => m_values.CopyTo(array, arrayIndex);

      public bool Remove(T item)
      {
        if (!m_dictionary.TryGetValue(item, out var node))
          return false;

        m_dictionary.Remove(item);
        m_values.RemoveAt(node);
        return true;
      }

      // ISet<>

      public bool Add(T item)
      {
        if (m_dictionary.ContainsKey(item))
          return false;

        var node = m_values.Count;
        m_values.Add(item);
        m_dictionary.Add(item, node);
        return true;
      }

      public void ExceptWith(System.Collections.Generic.IEnumerable<T> other) => RemoveRange(other);

      public void IntersectWith(System.Collections.Generic.IEnumerable<T> other)
      {
        var removing = new System.Collections.Generic.HashSet<T>(this, m_equalityComparer); // Order does not matter for removal, so we can use the built-in type.
        removing.ExceptWith(other);
        RemoveRange(removing);
      }

      public bool IsProperSubsetOf(System.Collections.Generic.IEnumerable<T> other)
        => this.SetCounts(other, false) is var (unfoundCount, uniqueCount) && unfoundCount > 0 && uniqueCount == Count;
      public bool IsProperSupersetOf(System.Collections.Generic.IEnumerable<T> other)
        => this.SetCounts(other, true) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount < Count;

      public bool IsSubsetOf(System.Collections.Generic.IEnumerable<T> other)
        => this.SetCounts(other, false) is var (unfoundCount, uniqueCount) && unfoundCount >= 0 && uniqueCount == Count;
      public bool IsSupersetOf(System.Collections.Generic.IEnumerable<T> other) => this.ContainsAll(other);

      public bool Overlaps(System.Collections.Generic.IEnumerable<T> other) => this.ContainsAny(other);

      public bool SetEquals(System.Collections.Generic.IEnumerable<T> other)
        => this.SetCounts(other, true) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount == Count;

      public void SymmetricExceptWith(System.Collections.Generic.IEnumerable<T> other)
      {
        var adding = new OrderedSet<T>(m_equalityComparer);

        foreach (var o in other)
          if (Contains(o))
            Remove(o);
          else
            adding.Add(o);

        AddRange(adding);
      }

      public void UnionWith(System.Collections.Generic.IEnumerable<T> other) => AddRange(other);

      // IEnumerable<>

      public System.Collections.Generic.IEnumerator<T> GetEnumerator() => m_values.GetEnumerator();
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      #endregion Implemented interfaces

      public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";
    }
  }
}
