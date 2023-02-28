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
    /// <summary>
    /// <para>A histogram dictionary based on <see cref="SortedDictionary{TKey, TFrequency}"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Histogram"/></para>
    /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TFrequency"></typeparam>
    public class Histogram<TKey, TFrequency>
      : System.Collections.Generic.IDictionary<TKey, TFrequency>
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
        if (!m_data.TryGetValue(key, out var storedFrequency))
          storedFrequency = TFrequency.Zero;

        m_data[key] = storedFrequency + frequency;

        m_totalFrequency += frequency;
      }

      public Histogram<TKey, TFrequency> AddRange<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector)
      {
        foreach (var item in collection.ThrowIfNull())
          Add(keySelector(item), frequencySelector(item));

        return this;
      }
      public Histogram<TKey, TFrequency> AddRange(System.Collections.Generic.IEnumerable<TKey> collection) => AddRange(collection, key => key, key => TFrequency.One);
      public Histogram<TKey, TFrequency> AddRange(Histogram<TKey, TFrequency> histogram) => AddRange(histogram, he => he.Key, he => he.Value);

      /// <summary>Computes the <typeparamref name="TPercentRank"/> [0, 1] of the <paramref name="key"/> in the histogram, and scales the result with <paramref name="factor"/>.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public TPercentRank ComputeCdfPercentRank<TPercentRank>(TKey key, TPercentRank factor)
        where TPercentRank : System.Numerics.IFloatingPoint<TPercentRank>
      {
        var comparer = System.Collections.Generic.Comparer<TKey>.Default;

        var cumulativeFrequency = TFrequency.Zero;

        foreach (var kvp in m_data)
          if (comparer.Compare(kvp.Key, key) <= 0)
            cumulativeFrequency += kvp.Value;

        return TPercentRank.CreateChecked(cumulativeFrequency) / TPercentRank.CreateChecked(m_totalFrequency) * factor;
      }

      /// <summary>Computes the <typeparamref name="TProbability"/> [0, 1] of the <paramref name="key"/> in the histogram, and scales the result with <paramref name="factor"/>.</summary>
      /// <remarks>This function maps </remarks>
      /// <exception cref="System.ArgumentNullException"/>
      public TProbability ComputePmfProbability<TProbability>(TKey key, TProbability factor)
        where TProbability : System.Numerics.IFloatingPoint<TProbability>
        => m_data.TryGetValue(key, out var frequency) ? TProbability.CreateChecked(frequency) / TProbability.CreateChecked(m_totalFrequency) * factor : TProbability.Zero;

      /// <summary>
      /// <para>A cumulative distribution function creates a new CDF dictionary.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Cumulative_distribution_function"/></para>
      /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
      /// </summary>
      /// <typeparam name="TPercentRank"></typeparam>
      /// <returns>A new <see cref="CumulativeDistributionFunction{TKey, TPercentRank}"/> (CDF) dictionary.</returns>
      public CumulativeDistributionFunction<TKey, TPercentRank> ToCumulativeDistributionFunction<TPercentRank>(TPercentRank factor)
        where TPercentRank : System.Numerics.IFloatingPoint<TPercentRank>
      {
        var totalFrequency = TPercentRank.CreateChecked(m_totalFrequency);

        var cmf = new CumulativeDistributionFunction<TKey, TPercentRank>();

        var cumulativeFrequency = TPercentRank.Zero;

        foreach (var kvp in m_data)
        {
          cumulativeFrequency += TPercentRank.CreateChecked(kvp.Value);

          cmf[kvp.Key] = cumulativeFrequency / totalFrequency * factor;
        }

        return cmf;
      }

      /// <summary>
      /// <para>A probability mass function creates a new PMF dictionary.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
      /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
      /// </summary>
      /// <typeparam name="TProbability"></typeparam>
      /// <remarks>If there's a need to add a key with a POST NORMALIZED probability, divide the probability by the probability remainder, e.g. Add(key, 0.2) is pre-normalized, and Add(key, 0.2 / 0.8) is post-normalized.</remarks>
      /// <returns>A new <see cref="ProbabilityMassFunction{TKey, TProbability}"/> (PMF) dictionary.</returns>
      public ProbabilityMassFunction<TKey, TProbability> ToProbabilityMassFunction<TProbability>(TProbability factor)
        where TProbability : System.Numerics.IFloatingPoint<TProbability>
      {
        var totalFrequency = TProbability.CreateChecked(m_totalFrequency);

        var pmf = new ProbabilityMassFunction<TKey, TProbability>();

        foreach (var kvp in m_data)
          pmf[kvp.Key] = TProbability.CreateChecked(kvp.Value) / totalFrequency * factor;

        return pmf;
      }

      #region Implemented interfaces

      // IDictionary<>
      public TFrequency this[TKey key] { get => m_data.TryGetValue(key, out var frequency) ? frequency : TFrequency.Zero; set => m_data[key] = value; }

      public int Count => m_data.Count;

      public bool IsReadOnly => false;

      public System.Collections.Generic.ICollection<TKey> Keys => m_data.Keys;

      public System.Collections.Generic.ICollection<TFrequency> Values => m_data.Values;

      public void Add(System.Collections.Generic.KeyValuePair<TKey, TFrequency> kvp) => Add(kvp.Key, kvp.Value);

      public void Clear()
      {
        m_data.Clear();

        m_totalFrequency = TFrequency.Zero;
      }

      public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TFrequency> kvp) => m_data.Contains(kvp);

      public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

      public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TFrequency>[] array, int index) => m_data.CopyTo(array, index);

      public bool Remove(TKey key)
      {
        if (m_data.TryGetValue(key, out var frequency))
        {
          var removed = m_data.Remove(key);

          if (removed)
            m_totalFrequency -= frequency;

          return removed;
        }

        return false;
      }

      public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TFrequency> kvp) => m_data.Contains(kvp) ? Remove(kvp.Key) : false;

      public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TFrequency value) => m_data.TryGetValue(key, out value);

      public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TFrequency>> GetEnumerator() => m_data.GetEnumerator();

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      #endregion // Implemented interfaces
    }
  }
}
