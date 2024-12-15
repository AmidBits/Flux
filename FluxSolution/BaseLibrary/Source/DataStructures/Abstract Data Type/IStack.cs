namespace Flux.DataStructure
{
  /// <summary>
  /// <para>This interface represents the abstract data type stack.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Stack_(abstract_data_type)"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  public interface IStack<TValue>
    : System.Collections.Generic.IEnumerable<TValue>
  {
    /// <summary>
    /// <para>Indicates whether this <see cref="IStack{TValue}"/> is empty.</para>
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// <para>Peeks at the last item (as in LIFO) on the <see cref="IStack{TValue}"/> without popping it.</para>
    /// </summary>
    /// <returns>The last item (as in LIFO) on the <see cref="IStack{TValue}"/>.</returns>
    TValue Peek();

    /// <summary>
    /// <para>Pops the last item (as in LIFO) from the <see cref="IStack{TValue}"/>.</para>
    /// </summary>
    /// <returns>The new immutable <see cref="IStack{TValue}"/>.</returns>
    IStack<TValue> Pop();

    /// <summary>
    /// <para>Pushes <paramref name="value"/> as the last item (as in LIFO) on the <see cref="IStack{TValue}"/>.</para>
    /// </summary>
    /// <param name="value">The value to push.</param>
    /// <returns>The new immutable <see cref="IStack{TValue}"/>.</returns>
    IStack<TValue> Push(TValue value);
  }
}
