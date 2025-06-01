namespace Flux
{
  public static partial class ReadOnlySpans
  {
    #region GetInfimumAndSupremum

    /// <summary>
    /// <para>Locate the index, item and value of both the largest element that is less-than(-or-equal) and the smallest element that is greater-than(-or-equal) to the singleton set {<paramref name="referenceValue"/>} (set S) identified by the <paramref name="valueSelector"/> (in set P). Uses the specified comparer (null for default).</para>
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
    public static (int InfimumIndex, TSource? InfimumItem, TValue? InfimumValue, int SupremumIndex, TSource? SupremumItem, TValue? SupremumValue) GetInfimumAndSupremum<TSource, TValue>(this System.ReadOnlySpan<TSource> source, TValue referenceValue, System.Func<TSource, TValue> valueSelector, bool proper, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var infIndex = -1;
      var infItem = default(TSource);
      var infValue = referenceValue;

      var supIndex = -1;
      var supItem = default(TSource);
      var supValue = referenceValue;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var item = source[index];

        var value = valueSelector(item);

        var cmp = comparer.Compare(value, referenceValue);

        if ((!proper ? cmp <= 0 : cmp < 0) && (infIndex < 0 || comparer.Compare(value, infValue) > 0))
        {
          infIndex = index;
          infItem = item;
          infValue = value;
        }

        if ((!proper ? cmp >= 0 : cmp > 0) && (supIndex < 0 || comparer.Compare(value, supValue) < 0))
        {
          supIndex = index;
          supItem = item;
          supValue = value;
        }
      }

      return (infIndex, infItem, infValue, supIndex, supItem, supValue);
    }

    #endregion // GetInfimumAndSupremum
  }
}
