namespace Flux.DataStructures
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
  public interface IHeap<TValue>
  {
    /// <summary>Yields the number of items on the <see cref="IHeap{TValue}"/>.</summary>
    int Count { get; }

    /// <summary>Indicates whether the <see cref="IHeap{TValue}"/> is empty.</summary>
    bool IsEmpty { get; }

    /// <summary>
    /// <para>Empty the <see cref="IHeap{TValue}"/>.</para>
    /// </summary>
    void Clear();

    /// <summary>
    /// <para>Indicates whether the <see cref="IHeap{TValue}"/> contains the <paramref name="item"/>.</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool Contains(TValue item);

    /// <summary>Removes the first item from the <see cref="IHeap{TValue}"/> and returns it.</summary>
    TValue Extract();

    /// <summary>Creates a new sequence with all items from the <see cref="IHeap{TValue}"/>.</summary>
    System.Collections.Generic.IEnumerable<TValue> ExtractAll()
    {
      while (!IsEmpty)
        yield return Extract();
    }

    /// <summary>Inserts an item into the <see cref="IHeap{TValue}"/>.</summary>
    void Insert(TValue item);

    /// <summary>Inserts all items from the <paramref name="collection"/> into the <see cref="IHeap{TValue}"/>.</summary>
    void InsertRange(System.Collections.Generic.ICollection<TValue> collection)
    {
      foreach (TValue item in collection)
        Insert(item);
    }

    /// <summary>Returns the first item in the <see cref="IHeap{TValue}"/> without removing it.</summary>
    TValue Peek();
  }
}
