namespace Flux.DataStructures
{
  /// <summary>
  /// <para>A cumulative distribution function (CDF) dictionary based on <see cref="SortedDictionary{TKey, double}"/>.</para>
  /// <para>A cumulative distribution function.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Cumulative_distribution_function"/></para>
  /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TPercentRank"></typeparam>
  public sealed class CumulativeDistributionFunction<TKey, TPercentRank>
      : System.Collections.Generic.SortedDictionary<TKey, TPercentRank>
      where TKey : System.Numerics.INumber<TKey>
      where TPercentRank : System.Numerics.IFloatingPointIeee754<TPercentRank>
  {
    /// <summary>Get the CDF (percent rank) of the <paramref name="key"/>. If the key exists it is returned, otherwise the percent rank is located by enumeration.</summary>
    /// <param name="key">The key to lookup percent rank for.</param>
    /// <returns>The percent rank of the <paramref name="key"/>.</returns>
    public TPercentRank Cdf(TKey key, System.Collections.Generic.IComparer<TKey>? comparer = null)
    {
      if (TryGetValue(key, out var percentRank))
        return percentRank;

      comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

      var cumulative = TPercentRank.One;

      using var e = GetEnumerator();

      while (e.MoveNext() && e.Current is var current && comparer.Compare(key, current.Key) > 0)
        cumulative = TPercentRank.CreateChecked(current.Value);

      return cumulative;
    }

    public ProbabilityMassFunction<TKey, TPercentRank> ToProbabilityMassFunction()
    {
      var pmf = new ProbabilityMassFunction<TKey, TPercentRank>();

      var previous = TPercentRank.Zero;

      foreach (var key in Keys.ToList())
      {
        var current = this[key];

        pmf.Add(key, current - previous);

        previous = current;
      }

      return pmf;
    }
  }
}
