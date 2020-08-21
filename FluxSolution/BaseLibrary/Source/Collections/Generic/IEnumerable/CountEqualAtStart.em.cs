using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var e = source.ThrowOnNull().GetEnumerator();

      return target.ThrowOnNull(nameof(target)).Count(item => e.MoveNext() && comparer.Equals(e.Current, item));
    }
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
