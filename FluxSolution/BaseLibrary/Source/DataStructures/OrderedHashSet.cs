namespace Flux
{
  public static partial class Em
  {
    public static DataStructures.OrderedHashSet<T> ToOrderedHashSet<T>(this System.Collections.Generic.IEnumerable<T> source)
      where T : notnull
    {
      var ohs = new DataStructures.OrderedHashSet<T>();
      foreach (var item in source)
        ohs.Add(item);
      return ohs;
    }
  }

  namespace DataStructures
  {
    public sealed class OrderedHashSet<TValue>
      : System.Collections.ObjectModel.KeyedCollection<TValue, TValue>
      where TValue : notnull
    {
      /// <summary>
      /// <para>Adds all non-existing elements in the collection into the <see cref="OrderedHashSet{T}"/> and returns the number of elements successfully added.</para>
      /// </summary>
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

      /// <summary>
      /// <para>Inserts all non-existing elements in the collection into the <see cref="OrderedHashSet{T}"/> (starting) at the specified index and returns the number of elements successfully inserted.</para>
      /// </summary>
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

      /// <summary>
      /// <para>Removes all existing elements in the collection from the <see cref="OrderedHashSet{T}"/> and returns the number of elements successfully found and removed.</para>
      /// </summary>
      public int RemoveRange(System.Collections.Generic.IEnumerable<TValue> collection)
      {
        var count = 0;

        foreach (var item in collection)
          if (Remove(item))
            count++;

        return count;
      }

      protected override TValue GetKeyForItem(TValue item) => item;

      public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";
    }
  }
}
