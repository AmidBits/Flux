namespace Flux
{
  public static partial class Xtensions
  {
    static public Collections.Immutable.IQueue<TValue> Reverse<TValue>(this Collections.Immutable.IQueue<TValue> queue)
    {
      if (queue is null) throw new System.ArgumentNullException(nameof(queue));

      var rq = Collections.Immutable.Queue<TValue>.Empty;
      for (var q = queue; !q.IsEmpty; q = q.Dequeue())
        rq = rq.Enqueue(q.Peek());
      return rq;
    }
  }

  namespace Collections.Immutable
  {
    public interface IQueue<TValue>
      : System.Collections.Generic.IEnumerable<TValue>
    {
      int Count { get; }
      bool IsEmpty { get; }
      IQueue<TValue> Dequeue();
      IQueue<TValue> Enqueue(TValue value);
      TValue Peek();
    }
  }
}
