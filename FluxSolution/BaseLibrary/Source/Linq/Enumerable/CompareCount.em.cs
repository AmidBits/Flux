namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>
    /// <para>Compares the number of elements in <paramref name="source"/> against the specified <paramref name="count"/>. If a <paramref name="predicate"/> is specified only matching elements are counted.</para>
    /// </summary>
    /// <returns>-1 when <paramref name="source"/> is less than, 0 when equal to, or 1 when greater than <paramref name="count"/>.</returns>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static int CompareCount<T>(this System.Collections.Generic.IEnumerable<T> source, int count, System.Func<T, int, bool>? predicate = null)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var counter = predicate is not null ? EnumerateCount() : source is System.Collections.ICollection c ? c.Count : source is System.Collections.Generic.ICollection<T> tc ? tc.Count : EnumerateCount();

      return counter > count ? 1 : counter < count ? -1 : 0;

      int EnumerateCount()
      {
        var counter = 0;

        using var e = source.ThrowIfNull().GetEnumerator();

        for (var index = 0; e.MoveNext() && counter <= count; index++)
          if (predicate?.Invoke(e.Current, index) ?? true)
            counter++;

        return counter > count ? 1 : counter < count ? -1 : 0;
      }
    }
  }
}
