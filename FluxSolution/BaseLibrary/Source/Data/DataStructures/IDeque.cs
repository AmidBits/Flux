namespace Flux.DataStructures
{
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
