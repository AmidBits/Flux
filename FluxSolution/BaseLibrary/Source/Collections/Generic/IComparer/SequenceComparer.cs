namespace Flux.DataStructures.Generic
{
  public class SequenceComparer<T>
    : System.Collections.Generic.IComparer<System.Collections.Generic.IEnumerable<T>>
    where T : System.IComparable<T>
  {
    public int Compare([System.Diagnostics.CodeAnalysis.AllowNull] System.Collections.Generic.IEnumerable<T> a, [System.Diagnostics.CodeAnalysis.AllowNull] System.Collections.Generic.IEnumerable<T> b)
    {
      if (ReferenceEquals(a, b)) return 0;

      using var ae = (a ?? System.Linq.Enumerable.Empty<T>()).GetEnumerator();
      using var be = (b ?? System.Linq.Enumerable.Empty<T>()).GetEnumerator();

      var ac = 0;
      var bc = 0;

      while (ae.MoveNext() is var amn && be.MoveNext() is var bmn)
      {
        if (amn) ac++;
        if (bmn) bc++;

        if (ac != bc) return ac.CompareTo(bc);
        else if (ae.Current.CompareTo(be.Current) is var cmp && cmp != 0) return cmp;
      }

      return 0;
    }
  }
}
