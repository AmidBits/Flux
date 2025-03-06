namespace Flux.Collections.Generic
{
  public sealed class ReverseComparer<T>
    : System.Collections.Generic.Comparer<T>
  {
    private System.Collections.Generic.IComparer<T> m_comparer;

    public ReverseComparer(System.Collections.Generic.IComparer<T> comparer) => m_comparer = comparer;

    private ReverseComparer() : this(System.Collections.Generic.Comparer<T>.Default) { }

    public System.Collections.Generic.IComparer<T> SourceComparer => m_comparer;

    public override int Compare(T? x, T? y) => -SourceComparer.Compare(x, y);

    public override string ToString() => $"{GetType().Name} {{ {SourceComparer} }}";
  }
}
