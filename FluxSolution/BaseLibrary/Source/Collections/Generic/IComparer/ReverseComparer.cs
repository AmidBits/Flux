using System.Diagnostics.CodeAnalysis;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    public static System.Collections.Generic.IComparer<T> CreateReverseComparer<T>(this System.Collections.Generic.IComparer<T> source)
      => new Collections.Generic.ReverseComparer<T>(source);
  }

  namespace Collections.Generic
  {
    public sealed class ReverseComparer<T>
      : System.Collections.Generic.Comparer<T>
    {
      private readonly System.Collections.Generic.IComparer<T> m_comparer;

      private ReverseComparer()
        : this(System.Collections.Generic.Comparer<T>.Default) { }
      public ReverseComparer(System.Collections.Generic.IComparer<T> comparer)
        => m_comparer = comparer;

      public override int Compare([System.Diagnostics.CodeAnalysis.AllowNull] T x, [System.Diagnostics.CodeAnalysis.AllowNull] T y)
        => -m_comparer.Compare(x, y);
    }
  }
}
