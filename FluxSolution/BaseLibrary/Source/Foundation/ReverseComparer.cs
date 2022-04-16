namespace Flux
{
  public static partial class IComparerEm
  {
    public static System.Collections.Generic.IComparer<T> CreateReverseComparer<T>(this System.Collections.Generic.IComparer<T> source)
      => new DataStructures.Generic.ReverseComparer<T>(source);
  }

  namespace DataStructures.Generic
  {
    public sealed class ReverseComparer<T>
      : System.Collections.Generic.Comparer<T>
    {
      private readonly System.Collections.Generic.IComparer<T> m_sourceComparer;

      private ReverseComparer()
        : this(System.Collections.Generic.Comparer<T>.Default) { }
      public ReverseComparer(System.Collections.Generic.IComparer<T> sourceComparer)
        => m_sourceComparer = sourceComparer;

      public System.Collections.Generic.IComparer<T> Source
        => m_sourceComparer;

      [System.Diagnostics.Contracts.Pure]
      public override int Compare([System.Diagnostics.CodeAnalysis.AllowNull] T x, [System.Diagnostics.CodeAnalysis.AllowNull] T y)
        => -m_sourceComparer.Compare(x, y);

      [System.Diagnostics.Contracts.Pure]
      public override string ToString()
        => $"{GetType().Name} {{ {m_sourceComparer} }}";
    }
  }
}
