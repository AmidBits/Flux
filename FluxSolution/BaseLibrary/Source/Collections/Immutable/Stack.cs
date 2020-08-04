namespace Flux.Collections.Immutable
{
  public sealed class Stack<T>
    : IStack<T>
  {
    public static readonly IStack<T> Empty = new EmptyStack();

    private readonly T m_head;
    private readonly IStack<T> m_tail;
    private Stack(T head, IStack<T> tail)
    {
      m_head = head;
      m_tail = tail;
    }

    public int Count { get; private set; }
    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
    {
      for (IStack<T> stack = this; !stack.IsEmpty; stack = stack.Pop())
        yield return stack.Peek();
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    public bool IsEmpty => false;
    public T Peek() => m_head;
    public IStack<T> Pop() => m_tail;
    public IStack<T> Push(T value) => new Stack<T>(value, this) { Count = Count + 1 };

    private sealed class EmptyStack
      : IStack<T>
    {
      public int Count => 0;
      public bool IsEmpty => true;
      public T Peek() => throw new System.Exception(nameof(EmptyStack));
      public IStack<T> Push(T value) => new Stack<T>(value, this);
      public IStack<T> Pop() => throw new System.Exception(nameof(EmptyStack));
      public System.Collections.Generic.IEnumerator<T> GetEnumerator() { yield break; }
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
  }
}
