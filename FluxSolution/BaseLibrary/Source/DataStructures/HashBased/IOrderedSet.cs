namespace Flux.DataStructures
{
  public interface IOrderedSet<T>
    : System.Collections.Generic.ISet<T>
      where T : notnull
  {
    /// <summary>
    /// <para>Gets or sets the element at the specified index in the <see cref="IOrderedSet{T}"/>.</para>
    /// </summary>
    T this[int index] { get; set; }

    /// <summary>
    /// <para>Gets the index associated with the specified value in the <see cref="IOrderedSet{T}"/>.</para>
    /// </summary>
    bool TryGetIndex(T value, out int index);

    /// <summary>
    /// <para>Inserts an element into the <see cref="IOrderedSet{T}"/> at the specified index.</para>
    /// </summary>
    void Insert(int index, T value);

    /// <summary>
    /// <para>Removes the element at the specified index of the <see cref="IOrderedSet{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    void RemoveAt(int index);
  }
}
