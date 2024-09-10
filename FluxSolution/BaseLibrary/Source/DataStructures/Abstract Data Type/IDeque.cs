namespace Flux.DataStructures
{
  /// <summary>
  /// <para>A double ended queue is a generalized version of queue data structure that allows insert and delete at both ends.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Double-ended_queue"/></para>
  /// <para><seealso href="https://ericlippert.com/2008/01/22/immutability-in-c-part-ten-a-double-ended-queue/"/></para>
  /// <para><seealso href="https://ericlippert.com/2008/02/12/immutability-in-c-part-eleven-a-working-double-ended-queue/"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue">The type of value for the double-ended queue node.</typeparam>
  public interface IDeque<TValue>
  {
    bool IsEmpty { get; }
    IDeque<TValue> DequeueLeft();
    IDeque<TValue> DequeueRight();
    IDeque<TValue> EnqueueLeft(TValue value);
    IDeque<TValue> EnqueueRight(TValue value);
    TValue PeekLeft();
    TValue PeekRight();
  }
}
