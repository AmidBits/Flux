namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see href="https://en.wikipedia.org/wiki/Histogram"/>
    /// <seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    public static DataStructures.Histogram<TKey, TFrequency> ToHistogram<TSource, TKey, TFrequency>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      => new DataStructures.Histogram<TKey, TFrequency>().AddRange(source, keySelector, frequencySelector);
  }

  namespace DataStructures
  {
    public class Histogram<TKey, TFrequency>
      : System.Collections.Generic.IReadOnlyDictionary<TKey, TFrequency>
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
    {
      private System.Collections.Generic.SortedDictionary<TKey, TFrequency> m_data = new System.Collections.Generic.SortedDictionary<TKey, TFrequency>();

      private TFrequency m_totalFrequency = TFrequency.Zero;

      public Histogram() { }
      public Histogram(System.Collections.Generic.IEnumerable<TKey> collection) => AddRange(collection);

      public TFrequency TotalFrequency => m_totalFrequency;

      public void Add(TKey key, TFrequency frequency)
      {
        m_totalFrequency += frequency;

        if (m_data.TryGetValue(key, out var storedFrequency))
          frequency += storedFrequency;

        m_data[key] = frequency;
      }
      public void Add(TKey key) => Add(key, TFrequency.One);

      public Histogram<TKey, TFrequency> AddRange(System.Collections.Generic.IEnumerable<TKey> collection)
      {
        foreach (var key in collection.ThrowIfNull())
          Add(key, TFrequency.One);

        return this;
      }
      public Histogram<TKey, TFrequency> AddRange<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector)
      {
        foreach (var item in collection.ThrowIfNull())
          Add(keySelector(item), frequencySelector(item));

        return this;
      }

      /// <summary>Computes the percentile rank of the <paramref name="key"/> and scales the result with a <paramref name="factor"/> using the <paramref name="histogram"/>. Uses the specified <paramref name="comparer"/>.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public TPercentRank ComputeCdfPercentRank<TPercentRank>(TKey key, TPercentRank factor, System.Collections.Generic.IComparer<TKey>? comparer = null)
        where TPercentRank : System.Numerics.IFloatingPoint<TPercentRank>
      {
        comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

        var cumulativeFrequency = TFrequency.Zero;

        foreach (var kvp in m_data)
          if (comparer.Compare(kvp.Key, key) <= 0)
            cumulativeFrequency += kvp.Value;

        return TPercentRank.CreateChecked(cumulativeFrequency) / TPercentRank.CreateChecked(m_totalFrequency) * factor;
      }

      /// <summary>Computes the <typeparamref name="TProbability"/> of <paramref name="key"/>, within the probability range [0, 1], in a distribution.</summary>
      /// <remarks>This function maps </remarks>
      /// <exception cref="System.ArgumentNullException"/>
      public TProbability ComputePmfProbability<TProbability>(TKey key, TProbability factor)
        where TProbability : System.Numerics.IFloatingPoint<TProbability>
        => m_data.TryGetValue(key, out var frequency) ? TProbability.CreateChecked(frequency) / TProbability.CreateChecked(m_totalFrequency) * factor : TProbability.Zero;

      public CumulativeDistributionFunction<TKey, TPercentRank> ToCumulativeDistributionFunction<TPercentRank>(TPercentRank factor)
        where TPercentRank : System.Numerics.IFloatingPoint<TPercentRank>
      {
        var totalFrequency = TPercentRank.CreateChecked(m_totalFrequency);

        var cmf = new CumulativeDistributionFunction<TKey, TPercentRank>();

        var cumulativeFrequency = TPercentRank.Zero;

        foreach (var kvp in m_data)
        {
          cumulativeFrequency += TPercentRank.CreateChecked(kvp.Value);

          cmf.m_data[kvp.Key] = cumulativeFrequency / totalFrequency * factor;
        }

        return cmf;
      }

      public ProbabilityMassFunction<TKey, TProbability> ToProbabilityMassFunction<TProbability>(TProbability factor)
        where TProbability : System.Numerics.IFloatingPoint<TProbability>
      {
        var totalFrequency = TProbability.CreateChecked(m_totalFrequency);

        var pmf = new ProbabilityMassFunction<TKey, TProbability>();

        foreach (var kvp in m_data)
          pmf.m_data[kvp.Key] = TProbability.CreateChecked(kvp.Value) / totalFrequency * factor;

        return pmf;
      }

      #region Implemented interfaces.
      public TFrequency this[TKey key] => m_data.TryGetValue(key, out var frequency) ? frequency : TFrequency.Zero;

      public System.Collections.Generic.IEnumerable<TKey> Keys => m_data.Keys;

      public System.Collections.Generic.IEnumerable<TFrequency> Values => m_data.Values;

      public int Count => m_data.Count;

      public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

      public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TFrequency>> GetEnumerator() => m_data.GetEnumerator();

      public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TFrequency value) => m_data.TryGetValue(key, out value);

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
      #endregion Implemented interfaces.
    }
  }
}
