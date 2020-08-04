namespace Flux.Collections.Immutable
{
  public sealed class Queue<TValue>
    : IQueue<TValue>
  {
    public static readonly IQueue<TValue> Empty = new EmptyQueue();

    private readonly IStack<TValue> m_backwards;
    private readonly IStack<TValue> m_forwards;
    private Queue(IStack<TValue> forwards, IStack<TValue> backwards)
    {
      m_forwards = forwards;
      m_backwards = backwards;
    }

    public int Count { get; private set; }
    public bool IsEmpty => false;
    public TValue Peek() => m_forwards.Peek();
    public IQueue<TValue> Enqueue(TValue value) => new Queue<TValue>(m_forwards, m_backwards.Push(value)) { Count = Count + 1 };
    public IQueue<TValue> Dequeue()
    {
      var f = m_forwards.Pop();

      if (!f.IsEmpty) return new Queue<TValue>(f, m_backwards);
      else if (m_backwards.IsEmpty) return Queue<TValue>.Empty;
      else return new Queue<TValue>(m_backwards.Reverse(), Stack<TValue>.Empty);
    }
    public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
    {
      foreach (TValue t in m_forwards) yield return t;
      foreach (TValue t in m_backwards.Reverse()) yield return t;
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class EmptyQueue
      : IQueue<TValue>
    {
      public int Count => 0;
      public bool IsEmpty => true;
      public TValue Peek() => throw new System.Exception(nameof(EmptyQueue));
      public IQueue<TValue> Enqueue(TValue value) => new Queue<TValue>(Stack<TValue>.Empty.Push(value), Stack<TValue>.Empty);
      public IQueue<TValue> Dequeue() => throw new System.Exception(nameof(EmptyQueue));
      public System.Collections.Generic.IEnumerator<TValue> GetEnumerator() { yield break; }
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
  }
}
