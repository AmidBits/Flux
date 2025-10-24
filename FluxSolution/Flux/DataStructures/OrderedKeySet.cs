namespace Flux.DataStructures
{
  /// <summary>
  /// <para>This is an ordered key set, based on the built-in <see cref="System.Collections.ObjectModel.KeyedCollection{TKey, TItem}"/>, and extended by the <see cref="IOrderedSet{T}"/>.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  /// <remarks>An ordered data structure maintains an indexed order, i.e. like a <see cref="System.Collections.Generic.List{TValue}"/> or a <typeparamref name="TValue"/>[].</remarks>
  public sealed class OrderedKeySet<TValue>
    : System.Collections.ObjectModel.KeyedCollection<TValue, TValue>, IOrderedSet<TValue>
    where TValue : notnull
  {
    protected override TValue GetKeyForItem(TValue item) => item; // This is the minimum implementation needed for the abstract .NET System.Collections.ObjectModel.KeyedCollection<TKey, TItem>.

    #region Implemented interfaces

    #region IOrderedSet<>

    public int AddRange(System.Collections.Generic.IEnumerable<TValue> collection)
    {
      var count = 0;

      foreach (var item in collection)
        if (!Contains(item))
        {
          Add(item);

          count++;
        }

      return count;
    }

    public int InsertRange(int index, System.Collections.Generic.IEnumerable<TValue> collection)
    {
      var count = 0;

      foreach (var item in collection)
        if (!Contains(item))
        {
          Insert(index++, item);

          count++;
        }

      return count;
    }

    public int RemoveRange(System.Collections.Generic.IEnumerable<TValue> collection)
    {
      var count = 0;

      foreach (var item in collection)
        if (Remove(item))
          count++;

      return count;
    }

    public bool TryGetIndex(TValue value, out int index)
      => (index = IndexOf(value)) > -1;

    #endregion // IOrderedSet<>

    #region ISet<>

    new public bool Add(TValue item)
    {
      var existed = base.Contains(item);

      base.Add(item);

      return !existed;
    }

    public void ExceptWith(System.Collections.Generic.IEnumerable<TValue> other) => RemoveRange(other);

    public void IntersectWith(System.Collections.Generic.IEnumerable<TValue> other)
    {
      var removing = new System.Collections.Generic.HashSet<TValue>(this, Comparer); // Order does not matter for removal, so we can use the built-in type.
      removing.ExceptWith(other);
      RemoveRange(removing);
    }

    public bool IsProperSubsetOf(System.Collections.Generic.IEnumerable<TValue> other)
      => this.ScanSetCounts(other, false) is var (unfoundCount, uniqueCount) && unfoundCount > 0 && uniqueCount == Count;

    public bool IsProperSupersetOf(System.Collections.Generic.IEnumerable<TValue> other)
      => this.ScanSetCounts(other, true) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount < Count;

    public bool IsSubsetOf(System.Collections.Generic.IEnumerable<TValue> other)
      => this.ScanSetCounts(other, false) is var (unfoundCount, uniqueCount) && unfoundCount >= 0 && uniqueCount == Count;

    public bool IsSupersetOf(System.Collections.Generic.IEnumerable<TValue> other) => this.ContainsAll(other);

    public bool Overlaps(System.Collections.Generic.IEnumerable<TValue> other) => this.ContainsAny(other);

    public bool SetEquals(System.Collections.Generic.IEnumerable<TValue> other)
      => this.ScanSetCounts(other, true) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount == Count;

    public void SymmetricExceptWith(System.Collections.Generic.IEnumerable<TValue> other)
    {
      var adding = new DataStructures.OrderedSet<TValue>(Comparer);

      foreach (var o in other)
        if (Contains(o))
          Remove(o);
        else
          adding.Add(o);

      AddRange(adding);
    }

    public void UnionWith(System.Collections.Generic.IEnumerable<TValue> other) => AddRange(other);

    #endregion // ISet<>

    #endregion // Implemented interfaces

    public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";
  }
}
