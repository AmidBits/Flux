namespace Flux.DataStructures.Heaps
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
  public interface IHeap<TValue>
  {
    /// <summary>
    /// <para>Gets the number of items on the <see cref="IHeap{TValue}"/>.</para>
    /// </summary>
    int Count { get; }

    /// <summary>
    /// <para>Indicates whether the <see cref="IHeap{TValue}"/> is empty.</para>
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// <para>Remove all values from the <see cref="IHeap{TValue}"/>.</para>
    /// </summary>
    void Clear();

    /// <summary>
    /// <para>Indicates whether the <see cref="IHeap{TValue}"/> contains the <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    bool Contains(TValue value);

    /// <summary>
    /// <para>Removes the top value from the <see cref="IHeap{TValue}"/> and returns it.</para>
    /// </summary>
    TValue Extract();

    /// <summary>
    /// <para>Creates a new sequence with all items from the <see cref="IHeap{TValue}"/>.</para>
    /// </summary>
    System.Collections.Generic.IEnumerable<TValue> ExtractAll()
    {
      while (!IsEmpty)
        yield return Extract();
    }

    /// <summary>
    /// <para>Inserts an <paramref name="item"/> on to the <see cref="IHeap{TValue}"/>.</para>
    /// </summary>
    void Insert(TValue item);

    /// <summary>
    /// <para>Inserts all items from the <paramref name="collection"/> into the <see cref="IHeap{TValue}"/>.</para>
    /// </summary>
    void InsertRange(System.Collections.Generic.ICollection<TValue> collection)
    {
      foreach (TValue item in collection)
        Insert(item);
    }

    /// <summary>
    /// <para>Returns the top value on the <see cref="IHeap{TValue}"/> without extracting it.</para>
    /// </summary>
    TValue Peek();
  }
}
