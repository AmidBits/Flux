namespace Flux
{
  public static partial class XtensionsImmutable
  {
    static public Collections.Immutable.IStack<TValue> Reverse<TValue>(this Collections.Immutable.IStack<TValue> stack)
    {
      if (stack is null) throw new System.ArgumentNullException(nameof(stack));

      var rs = Collections.Immutable.Stack<TValue>.Empty;
      for (var s = stack; !s.IsEmpty; s = s.Pop())
        rs = rs.Push(s.Peek());
      return rs;
    }
  }

  namespace Collections.Immutable
  {
    public interface IStack<TValue>
      : System.Collections.Generic.IEnumerable<TValue>
    {
      int Count { get; }
      bool IsEmpty { get; }
      TValue Peek();
      IStack<TValue> Pop();
      IStack<TValue> Push(TValue value);
    }
  }
}
