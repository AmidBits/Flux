using System.Linq;

namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var se = source.ThrowOnNull().GetEnumerator();

      return target.ThrowOnNull().TakeWhile(t => se.MoveNext() && equalityComparer.Equals(t, se.Current)).Count();
    }
  }
}
