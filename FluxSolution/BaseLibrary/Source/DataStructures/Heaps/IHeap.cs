namespace Flux.DataStructures
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
  public interface IHeap<T>
  {
    /// <summary>Yields the number of items on the <see cref="IHeap{T}"/>.</summary>
    int Count { get; }

    /// <summary>Indicates whether the <see cref="IHeap{T}"/> is empty.</summary>
    bool IsEmpty { get; }

    /// <summary>
    /// <para>Empty the <see cref="IHeap{T}"/>.</para>
    /// </summary>
    void Clear();

    /// <summary>
    /// <para>Indicates whether the <see cref="IHeap{T}"/> contains the <paramref name="item"/>.</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool Contains(T item);

    /// <summary>Removes the first item from the <see cref="IHeap{T}"/> and returns it.</summary>
    T Extract();

    /// <summary>Creates a new sequence with all items from the <see cref="IHeap{T}"/>.</summary>
    System.Collections.Generic.IEnumerable<T> ExtractAll()
    {
      while (!IsEmpty)
        yield return Extract();
    }

    /// <summary>Inserts an item into the <see cref="IHeap{T}"/>.</summary>
    void Insert(T item);

    /// <summary>Inserts all items from the <paramref name="collection"/> into the <see cref="IHeap{T}"/>.</summary>
    void InsertRange(System.Collections.Generic.ICollection<T> collection)
    {
      foreach (T item in collection)
        Insert(item);
    }

    /// <summary>Returns the first item in the <see cref="IHeap{T}"/> without removing it.</summary>
    T Peek();
  }
}
