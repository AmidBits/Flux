using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var se = source.GetEnumerator();

      return target.TakeWhile(tv => se.MoveNext() && equalityComparer.Equals(se.Current, tv)).Count();
    }
  }
}