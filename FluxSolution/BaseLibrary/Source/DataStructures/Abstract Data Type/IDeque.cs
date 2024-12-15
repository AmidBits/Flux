namespace Flux.DataStructure
{
  /// <summary>
  /// <para>This interface represents a double ended queue, a generalized version of a queue, that allows insert and delete at both ends.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Double-ended_queue"/></para>
  /// <para><seealso href="https://ericlippert.com/2008/01/22/immutability-in-c-part-ten-a-double-ended-queue/"/></para>
  /// <para><seealso href="https://ericlippert.com/2008/02/12/immutability-in-c-part-eleven-a-working-double-ended-queue/"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue">The type of value for the double-ended queue node.</typeparam>
  public interface IDeque<TValue>
  {
    /// <summary>
    /// <para>Indicates whether this <see cref="IDeque{TValue}"/> is empty.</para>
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// <para>Dequeues the last item (as in FIFO) in the <see cref="IDeque{TValue}"/>.</para>
    /// </summary>
    /// <returns>The new immutable <see cref="IDeque{TValue}"/>.</returns>
    IDeque<TValue> DequeueLeft();

    /// <summary>
    /// <para>Dequeues the first item (as in FIFO) in the <see cref="IDeque{TValue}"/>.</para>
    /// </summary>
    /// <returns>The new immutable <see cref="IDeque{TValue}"/>.</returns>
    IDeque<TValue> DequeueRight();

    /// <summary>
    /// <para>Enqueues <paramref name="value"/> as the first item (as in FIFO) in the <see cref="IDeque{TValue}"/>.</para>
    /// </summary>
    /// <param name="value">The value to enqueue.</param>
    /// <returns>The new immutable <see cref="IDeque{TValue}"/>.</returns>
    IDeque<TValue> EnqueueLeft(TValue value);

    /// <summary>
    /// <para>Enqueues <paramref name="value"/> as the last item (as in FIFO) in the <see cref="IDeque{TValue}"/>.</para>
    /// </summary>
    /// <param name="value">The value to enqueue.</param>
    /// <returns>The new immutable <see cref="IDeque{TValue}"/>.</returns>
    IDeque<TValue> EnqueueRight(TValue value);

    /// <summary>
    /// <para>Peeks at the last item (as in FIFO) in the <see cref="IDeque{TValue}"/> without dequeueing it.</para>
    /// </summary>
    /// <returns>The last item (as in FIFO) in the <see cref="IDeque{TValue}"/>.</returns>
    TValue PeekLeft();

    /// <summary>
    /// <para>Peeks at the first item (as in FIFO) in the <see cref="IDeque{TValue}"/> without dequeueing it.</para>
    /// </summary>
    /// <returns>The first item (as in FIFO) in the <see cref="IDeque{TValue}"/>.</returns>
    TValue PeekRight();
  }
}
