namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Locate the index, item and value of both the largest element that is less-than(-or-equal) and the smallest element that is greater-than(-or-equal) to the specified reference value (set S) identified by the <paramref name="valueSelector"/> (in set P). Uses the specified comparer (null for default).</para>
    /// <see href="https://en.wikipedia.org/wiki/Infimum_and_supremum"/>
    /// </summary>
    /// <remarks>By definition of infimum and supremum, the function is supposed to return both the less-than-or-equal and greater-than-or-equal, but this version makes the (-or-equal) optional via the <paramref name="proper"/> parameter. Also, infimum and supremum are positive constructs, so to accomodate negatives we return as toward-zero and away-from-zero for clarity.</remarks>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="referenceValue"></param>
    /// <param name="valueSelector"></param>
    /// <param name="proper"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static (int TowardZeroIndex, TSource? TowardZeroItem, TValue? TowardZeroValue, int AwayFromZeroIndex, TSource? AwayFromZeroItem, TValue? AwayFromZeroValue) InfimumAndSupremum<TSource, TValue>(this System.ReadOnlySpan<TSource> source, TValue referenceValue, System.Func<TSource, TValue> valueSelector, bool proper, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var tzIndex = -1;
      var tzItem = default(TSource);
      var tzValue = referenceValue;

      var afzIndex = -1;
      var afzItem = default(TSource);
      var afzValue = referenceValue;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var item = source[index];

        var value = valueSelector(item);

        var cmp = comparer.Compare(value, referenceValue);

        if ((!proper ? cmp <= 0 : cmp < 0) && (tzIndex < 0 || comparer.Compare(value, tzValue) > 0))
        {
          tzIndex = index;
          tzItem = item;
          tzValue = value;
        }

        if ((!proper ? cmp >= 0 : cmp > 0) && (afzIndex < 0 || comparer.Compare(value, afzValue) < 0))
        {
          afzIndex = index;
          afzItem = item;
          afzValue = value;
        }
      }

      return (tzIndex, tzItem, tzValue, afzIndex, afzItem, afzValue);
    }
  }
}
