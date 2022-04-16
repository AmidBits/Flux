using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      using var se = source.GetEnumerator();

      return target.TakeWhile(te => se.MoveNext() && equalityComparer.Equals(se.Current, te)).Count();
    }
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
