namespace Flux.DataStructures
{
  public interface IQueue<TValue>
    : System.Collections.Generic.IEnumerable<TValue>
  {
    bool IsEmpty { get; }
    IQueue<TValue> Dequeue();
    IQueue<TValue> Enqueue(TValue value);
    TValue Peek();
  }
}
