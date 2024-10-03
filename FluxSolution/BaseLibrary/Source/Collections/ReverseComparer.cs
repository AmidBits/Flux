namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.Comparer<T> CreateReverseComparer<T>(this System.Collections.Generic.IComparer<T> source)
      => new ReverseComparer<T>(source);
  }

  public sealed class ReverseComparer<T>
    : System.Collections.Generic.Comparer<T>
  {
    private readonly System.Collections.Generic.IComparer<T> m_sourceComparer;

    private ReverseComparer() : this(System.Collections.Generic.Comparer<T>.Default) { }
    public ReverseComparer(System.Collections.Generic.IComparer<T> comparer) => m_sourceComparer = comparer;

    public System.Collections.Generic.IComparer<T> SourceComparer => m_sourceComparer;

    public override int Compare(T? x, T? y) => -m_sourceComparer.Compare(x, y);

    public override string ToString() => $"{GetType().Name} {{ {m_sourceComparer} }}";
  }
}
