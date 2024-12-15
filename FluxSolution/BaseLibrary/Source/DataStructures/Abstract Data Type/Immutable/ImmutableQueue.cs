namespace Flux
{
  namespace DataStructure.Immutable
  {
    /// <summary>
    /// <para>An immutable queue. O(1) for all but one entry which is O(n).</para>
    /// <para><see href="https://ericlippert.com/2007/12/10/immutability-in-c-part-four-an-immutable-queue/"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Queue_(abstract_data_type)"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class ImmutableQueue<TValue>
      : IQueue<TValue>
    {
      public static IQueue<TValue> Empty { get; } = new EmptyImmutableQueue();

      private readonly IStack<TValue> m_backwards;
      private readonly IStack<TValue> m_forwards;

      private ImmutableQueue(IStack<TValue> forwards, IStack<TValue> backwards)
      {
        m_forwards = forwards;
        m_backwards = backwards;
      }

      #region IQueue<T>

      public bool IsEmpty => false;
      public IQueue<TValue> Dequeue()
        => m_forwards.Pop() is var forward && !forward.IsEmpty
        ? new ImmutableQueue<TValue>(forward, m_backwards)
        : m_backwards.IsEmpty
        ? Empty
        : new ImmutableQueue<TValue>(m_backwards.Reverse(), ImmutableStack<TValue>.Empty);
      public IQueue<TValue> Enqueue(TValue value) => new ImmutableQueue<TValue>(m_forwards, m_backwards.Push(value));
      public TValue Peek() => m_forwards.Peek();

      #endregion // IQueue<T>

      #region IEnumerable<T>

      public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
      {
        foreach (TValue t in m_forwards) yield return t;
        foreach (TValue t in m_backwards.Reverse()) yield return t;
      }
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      #endregion // IEnumerable<T>

      private sealed class EmptyImmutableQueue
        : IQueue<TValue>
      {
        #region IQueue<T>

        public bool IsEmpty { get { return true; } }
        public IQueue<TValue> Dequeue() { throw new System.Exception(nameof(EmptyImmutableQueue)); }
        public IQueue<TValue> Enqueue(TValue value) => new ImmutableQueue<TValue>(ImmutableStack<TValue>.Empty.Push(value), ImmutableStack<TValue>.Empty);
        public TValue Peek() { throw new System.Exception(nameof(EmptyImmutableQueue)); }

        #endregion // IQueue<T>

        #region IEnumerable<T>

        public System.Collections.Generic.IEnumerator<TValue> GetEnumerator() { yield break; }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion // IEnumerable<T>
      }
    }
  }
}
