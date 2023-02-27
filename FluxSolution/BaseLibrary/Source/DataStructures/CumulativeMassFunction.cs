namespace Flux
{
  //public static partial class ExtensionMethods
  //{
  //  /// <summary>Computes the CDF of the elements in the histogram. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
  //  /// <exception cref="System.ArgumentNullException"/>
  //  /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
  //  public static DataStructures.CumulativeMassFunction<TKey, TPercentRank> ToCumulativeMassFunction<TKey, TFrequency, TPercentRank>(this DataStructures.Histogram<TKey, TFrequency> source, TPercentRank factor)
  //    where TKey : notnull
  //    where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
  //    where TPercentRank : System.Numerics.IFloatingPoint<TPercentRank>
  //    => DataStructures.CumulativeMassFunction<TKey, TPercentRank>.Create(source, factor);

  //  public static TPercentRank ToCmfPercentRank<TKey, TFrequency, TPercentRank>(this DataStructures.Histogram<TKey, TFrequency> source, TKey key, TPercentRank factor, System.Collections.Generic.IComparer<TKey>? comparer = null)
  //    where TKey : notnull
  //    where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
  //    where TPercentRank : System.Numerics.IFloatingPoint<TPercentRank>
  //    => DataStructures.CumulativeMassFunction<TKey, TPercentRank>.ComputePercentileRank(source, key, factor, comparer);
  //}

  namespace DataStructures
  {
    public class CumulativeDistributionFunction<TKey, TPercentRank>
      : System.Collections.Generic.IReadOnlyDictionary<TKey, TPercentRank>
      where TKey : notnull
      where TPercentRank : System.Numerics.IFloatingPoint<TPercentRank>
    {
      internal System.Collections.Generic.SortedDictionary<TKey, TPercentRank> m_data = new System.Collections.Generic.SortedDictionary<TKey, TPercentRank>();

      private TPercentRank m_cumulativeFrequency = TPercentRank.Zero;

      internal CumulativeDistributionFunction() { }

      public TPercentRank CumulativeFrequency => m_cumulativeFrequency;

      #region Static methods.

      /// <summary>Computes the percentile rank of the <paramref name="key"/> and scales the result with a <paramref name="factor"/> using the <paramref name="histogram"/>. Uses the specified <paramref name="comparer"/>.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public static TPercentRank ComputePercentileRank<TFrequency>(Histogram<TKey, TFrequency> histogram, TKey key, TPercentRank factor, System.Collections.Generic.IComparer<TKey>? comparer = null)
        where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      {
        comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

        var cumulativeFrequency = TFrequency.Zero;

        foreach (var item in histogram.ThrowIfNull())
          if (comparer.Compare(item.Key, key) <= 0)
            cumulativeFrequency += item.Value;

        return TPercentRank.CreateChecked(cumulativeFrequency) / TPercentRank.CreateChecked(histogram.TotalFrequency) * factor;
      }

      #endregion Static methods.

      #region Implemented interfaces.
      public TPercentRank this[TKey key] => m_data.TryGetValue(key, out var percentRank) ? percentRank : TPercentRank.Zero;

      public System.Collections.Generic.IEnumerable<TKey> Keys => m_data.Keys;

      public System.Collections.Generic.IEnumerable<TPercentRank> Values => m_data.Values;

      public int Count => m_data.Count;

      public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

      public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TPercentRank>> GetEnumerator() => m_data.GetEnumerator();

      public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TPercentRank value) => m_data.TryGetValue(key, out value);

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
      #endregion Implemented interfaces.
    }
  }
}
