namespace Flux
{
  public static partial class Fx
  {
    public static DataStructures.HashBased.OrderedKeySet<T> ToOrderedHashSet<T>(this System.Collections.Generic.IEnumerable<T> source)
      where T : notnull
    {
      var ohs = new DataStructures.HashBased.OrderedKeySet<T>();
      foreach (var item in source)
        ohs.Add(item);
      return ohs;
    }
  }

  namespace DataStructures.HashBased
  {
    /// <summary>
    /// <para>This is an ordered key set, based on the .NET built-in <see cref="System.Collections.ObjectModel.KeyedCollection{TKey, TItem}"/> which is then extended by the <see cref="IOrderedSet{T}"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OrderedKeySet<T>
      : System.Collections.ObjectModel.KeyedCollection<T, T>, IOrderedSet<T>
      where T : notnull
    {
      protected override T GetKeyForItem(T item) => item; // This is the minimum implementation needed for the abstract .NET System.Collections.ObjectModel.KeyedCollection<TKey, TItem>.

      #region Implemented interfaces

      // IOrderedSet<>

      public int AddRange(System.Collections.Generic.IEnumerable<T> collection)
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

      public int InsertRange(int index, System.Collections.Generic.IEnumerable<T> collection)
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

      public int RemoveRange(System.Collections.Generic.IEnumerable<T> collection)
      {
        var count = 0;

        foreach (var item in collection)
          if (Remove(item))
            count++;

        return count;
      }

      public bool TryGetIndex(T value, out int index)
        => (index = IndexOf(value)) > -1;

      // ISet<>

      new public bool Add(T item)
      {
        var existed = base.Contains(item);

        base.Add(item);

        return !existed;
      }

      public void ExceptWith(System.Collections.Generic.IEnumerable<T> other) => RemoveRange(other);

      public void IntersectWith(System.Collections.Generic.IEnumerable<T> other)
      {
        var removing = new System.Collections.Generic.HashSet<T>(this, Comparer); // Order does not matter for removal, so we can use the built-in type.
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
        var adding = new OrderedSet<T>(Comparer);

        foreach (var o in other)
          if (Contains(o))
            Remove(o);
          else
            adding.Add(o);

        AddRange(adding);
      }

      public void UnionWith(System.Collections.Generic.IEnumerable<T> other) => AddRange(other);

      #endregion // Implemented interfaces

      public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";
    }
  }
}
