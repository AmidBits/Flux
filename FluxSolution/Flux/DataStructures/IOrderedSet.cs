namespace Flux.DataStructures
{
  /// <summary>
  /// <para>This interface represents an ordered set.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  public interface IOrderedSet<TValue>
    : System.Collections.Generic.IReadOnlyCollection<TValue>, System.Collections.Generic.ISet<TValue>


      where TValue : notnull
  {
    /// <summary>
    /// <para>Gets or sets the element at the specified index in the <see cref="IOrderedSet{T}"/>.</para>
    /// </summary>
    TValue this[int index] { get; set; }

    /// <summary>
    /// <para>Adds all non-existing elements in the collection into the <see cref="IOrderedSet{T}"/> and returns the number of elements successfully added.</para>
    /// </summary>
    int AddRange(System.Collections.Generic.IEnumerable<TValue> collection);

    /// <summary>
    /// <para>Inserts an element into the <see cref="IOrderedSet{T}"/> at the specified index.</para>
    /// </summary>
    void Insert(int index, TValue value);

    /// <summary>
    /// <para>Inserts all non-existing elements in the collection into the <see cref="IOrderedSet{T}"/> (starting) at the specified index and returns the number of elements successfully inserted.</para>
    /// </summary>
    int InsertRange(int index, System.Collections.Generic.IEnumerable<TValue> collection);

    /// <summary>
    /// <para>Removes the element at the specified index of the <see cref="IOrderedSet{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    void RemoveAt(int index);

    /// <summary>
    /// <para>Removes all existing elements in the collection from the <see cref="IOrderedSet{T}"/> and returns the number of elements successfully found and removed.</para>
    /// </summary>
    int RemoveRange(System.Collections.Generic.IEnumerable<TValue> collection);

    /// <summary>
    /// <para>Gets the index associated with the specified value in the <see cref="IOrderedSet{T}"/>.</para>
    /// </summary>
    bool TryGetIndex(TValue value, out int index);
  }
}
