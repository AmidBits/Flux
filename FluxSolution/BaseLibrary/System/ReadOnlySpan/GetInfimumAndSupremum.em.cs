namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Locate the index and value of both the greatest element that is less-than(-or-equal) and the least element that is greater-than(-or-equal) to the specified reference value (set S) identified by the <paramref name="valueSelector"/> (in set P). Uses the specified comparer (null for default).</para>
    /// <see href="https://en.wikipedia.org/wiki/Infimum_and_supremum"/>
    /// </summary>
    /// <remarks>By definition of infimum and supremum, the function is supposed to return both the less-than-or-equal and greater-than-or-equal, but this version makes the (-or-equal) optional via the <paramref name="proper"/> parameter.</remarks>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="referenceValue"></param>
    /// <param name="valueSelector"></param>
    /// <param name="proper">When true (default) then "-or-equal", otherwise never equal.</param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static (int IndexInf, TValue ValueInf, int IndexSup, TValue ValueSup) GetInfimumAndSupremum<TSource, TValue>(this System.ReadOnlySpan<TSource> source, TValue referenceValue, System.Func<TSource, TValue> valueSelector, bool proper = true, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var indexInf = -1;
      var valueInf = default(TValue);
      var indexSup = -1;
      var valueSup = default(TValue);

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var value = valueSelector(source[index]);

        var cmp = comparer.Compare(value, referenceValue);

        if ((proper ? cmp <= 0 : cmp < 0) && (indexInf < 0 || comparer.Compare(value, valueInf) > 0))
        {
          indexInf = index;
          valueInf = value;
        }

        if ((proper ? cmp >= 0 : cmp > 0) && (indexSup < 0 || comparer.Compare(value, valueSup) < 0))
        {
          indexSup = index;
          valueSup = value;
        }
      }

      return (indexInf, valueInf!, indexSup, valueSup!);
    }
  }
}
