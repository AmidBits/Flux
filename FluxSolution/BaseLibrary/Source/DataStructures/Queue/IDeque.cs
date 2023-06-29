namespace Flux.DataStructures
{
  /// <summary>A double ended queue is a generalized version of queue data structure that allows insert and delete at both ends.</summary>
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
