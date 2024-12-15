namespace Flux.DataStructure
{
  /// <summary>
  /// <para>This interface represents the abstract data type queue.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Queue_(abstract_data_type)"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  public interface IQueue<TValue>
    : System.Collections.Generic.IEnumerable<TValue>
  {
    /// <summary>
    /// <para>Indicates whether this <see cref="IQueue{TValue}"/> is empty.</para>
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// <para>Dequeues the first item (as in FIFO) in the <see cref="IQueue{TValue}"/>.</para>
    /// </summary>
    /// <returns>The new immutable <see cref="IQueue{TValue}"/>.</returns>
    IQueue<TValue> Dequeue();

    /// <summary>
    /// <para>Enqueues <paramref name="value"/> as the last item (as in FIFO) in the <see cref="IQueue{TValue}"/>.</para>
    /// </summary>
    /// <param name="value">The value to enqueue.</param>
    /// <returns>The new immutable <see cref="IQueue{TValue}"/>.</returns>
    IQueue<TValue> Enqueue(TValue value);

    /// <summary>
    /// <para>Peeks at the first item (as in FIFO) in the <see cref="IQueue{TValue}"/> without dequeueing it.</para>
    /// </summary>
    /// <returns>The first item (as in FIFO) in the <see cref="IQueue{TValue}"/>.</returns>
    TValue Peek();
  }
}
