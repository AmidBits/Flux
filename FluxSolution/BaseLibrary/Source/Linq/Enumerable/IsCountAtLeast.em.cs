namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns whether the sequence has at least the specified count (number of elements) matching the predicate.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool IsCountAtLeast<T>(this System.Collections.Generic.IEnumerable<T> source, int minCount, System.Func<T, int, bool> predicate)
    {
      if (minCount < 0) throw new System.ArgumentOutOfRangeException(nameof(minCount));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.ThrowIfNull().GetEnumerator();

      var counter = 0;

      for (var index = 0; counter < minCount && e.MoveNext(); index++)
        if (predicate(e.Current, index))
          counter++;

      return counter >= minCount;
    }
  }
}
