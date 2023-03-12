namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns whether the sequence has at least a minimum count (inclusive) and at most a maximum count (inclusive), matching the predicate.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool IsCountBetween<T>(this System.Collections.Generic.IEnumerable<T> source, int minCount, int maxCount, System.Func<T, int, bool>? predicate = null)
    {
      if (minCount < 0) throw new System.ArgumentOutOfRangeException(nameof(minCount));
      if (maxCount < minCount) throw new System.ArgumentOutOfRangeException(nameof(maxCount));

      var counter = 0;

      using var e = source.ThrowIfNull().GetEnumerator();

      for (var index = 0; counter <= maxCount && e.MoveNext(); index++)
        if (predicate?.Invoke(e.Current, index) ?? true)
          counter++;

      return counter >= minCount && counter <= maxCount;
    }
  }
}
