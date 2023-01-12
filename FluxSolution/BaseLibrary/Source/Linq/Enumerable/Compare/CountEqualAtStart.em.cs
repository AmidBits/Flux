namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var se = source.ThrowIfNull().GetEnumerator();

      return target.ThrowIfNull().TakeWhile(t => se.MoveNext() && equalityComparer.Equals(t, se.Current)).Count();
    }
  }
}
