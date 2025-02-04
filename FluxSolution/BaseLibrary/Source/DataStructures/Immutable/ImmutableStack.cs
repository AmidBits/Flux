namespace Flux.DataStructures.Immutable
{
  /// <summary>
  /// <para>An immutable stack.</para>
  /// <para><see href="https://ericlippert.com/2007/12/04/immutability-in-c-part-two-a-simple-immutable-stack/"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Stack_(abstract_data_type)"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  public sealed class ImmutableStack<TValue>
    : IStack<TValue>
  {
    public static IStack<TValue> Empty { get; } = new EmptyImmutableStack();

    private readonly TValue m_head;
    private readonly IStack<TValue> m_tail;

    private ImmutableStack(TValue head, IStack<TValue> tail)
    {
      this.m_head = head;
      this.m_tail = tail;
    }

    #region IStack<T>

    public bool IsEmpty => false;
    public TValue Peek() => m_head;
    public IStack<TValue> Pop() => m_tail;
    public IStack<TValue> Push(TValue value) => new ImmutableStack<TValue>(value, this);

    #endregion // IStack<T>

    #region IEnumerator<T>

    public IEnumerator<TValue> GetEnumerator()
    {
      for (IStack<TValue> stack = this; !stack.IsEmpty; stack = stack.Pop())
        yield return stack.Peek();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => this.GetEnumerator();

    #endregion // IEnumerator<T>

    private sealed class EmptyImmutableStack
      : IStack<TValue>
    {
      #region IStack<T>

      public bool IsEmpty => true;
      public TValue Peek() => throw new System.Exception(nameof(EmptyImmutableStack));
      public IStack<TValue> Push(TValue value) => new ImmutableStack<TValue>(value, this);
      public IStack<TValue> Pop() => throw new System.Exception(nameof(EmptyImmutableStack));

      #endregion // IStack<T>

      #region IEnumerator<T>

      public System.Collections.Generic.IEnumerator<TValue> GetEnumerator() { yield break; }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      #endregion // IEnumerator<T>
    }
  }
}
