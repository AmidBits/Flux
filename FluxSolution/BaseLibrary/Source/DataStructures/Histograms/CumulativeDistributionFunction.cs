namespace Flux
{
  namespace DataStructures
  {
    /// <summary>
    /// <para>A cumulative distribution function (CDF) dictionary based on <see cref="SortedDictionary{TKey, double}"/>.</para>
    /// <para>A cumulative distribution function.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cumulative_distribution_function"/></para>
    /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TPercentRank"></typeparam>
    public sealed class CumulativeDistributionFunction<TKey>
      : System.Collections.Generic.SortedDictionary<TKey, double>
      where TKey : notnull
    {
      /// <summary>Get the CDF (percent rank) of the <paramref name="key"/>. If the key exists it is returned, otherwise the percent rank is located by enumeration.</summary>
      /// <param name="key">The key to lookup percent rank for.</param>
      /// <returns>The percent rank of the <paramref name="key"/>.</returns>
      public double Cdf(TKey key)
      {
        if (TryGetValue(key, out var percentRank))
          return percentRank;

        var comparer = System.Collections.Generic.Comparer<TKey>.Default;

        var cumulative = 1d;

        using var e = GetEnumerator();

        while (e.MoveNext() && e.Current is var current && comparer.Compare(key, current.Key) > 0)
          cumulative = current.Value;

        return cumulative;
      }
    }
  }
}
