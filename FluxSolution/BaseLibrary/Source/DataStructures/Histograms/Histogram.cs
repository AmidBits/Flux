namespace Flux
{
  public static partial class DataStructuresExtensionMethods
  {
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see href="https://en.wikipedia.org/wiki/Histogram"/>
    /// <seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    public static DataStructures.Histogram<TKey> ToHistogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, System.Numerics.BigInteger> frequencySelector)
      where TKey : notnull
      => new DataStructures.Histogram<TKey>().AddRange(source, keySelector, frequencySelector);
  }

  namespace DataStructures
  {
    /// <summary>
    /// <para>A histogram dictionary based on <see cref="SortedDictionary{TKey, System.Numerics.BigInteger}"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Histogram"/></para>
    /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public sealed class Histogram<TKey>
      : System.Collections.Generic.IDictionary<TKey, System.Numerics.BigInteger>
      where TKey : notnull
    {
      private readonly System.Collections.Generic.SortedDictionary<TKey, System.Numerics.BigInteger> m_data = new();

      private System.Numerics.BigInteger m_totalFrequency = System.Numerics.BigInteger.Zero;

      public Histogram() { }
      public Histogram(System.Collections.Generic.IEnumerable<TKey> collection) => AddRange(collection);

      public System.Numerics.BigInteger TotalFrequency => m_totalFrequency;

      public void Add(TKey key, System.Numerics.BigInteger frequency)
      {
        if (!m_data.TryGetValue(key, out var storedFrequency))
          storedFrequency = System.Numerics.BigInteger.Zero;

        m_data[key] = storedFrequency + frequency;

        m_totalFrequency += frequency;
      }

      public Histogram<TKey> AddRange<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, System.Numerics.BigInteger> frequencySelector)
      {
        foreach (var item in collection.ThrowOnNull())
          Add(keySelector(item), frequencySelector(item));

        return this;
      }
      public Histogram<TKey> AddRange(System.Collections.Generic.IEnumerable<TKey> collection) => AddRange(collection, key => key, key => System.Numerics.BigInteger.One);
      public Histogram<TKey> AddRange(Histogram<TKey> histogram) => AddRange(histogram, he => he.Key, he => he.Value);

      /// <summary>Computes the <typeparamref name="TPercentRank"/> [0, 1] of the <paramref name="key"/> in the histogram, and scales the result with <paramref name="factor"/>.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public double ComputeCdfPercentRank(TKey key, double factor)
      {
        var comparer = System.Collections.Generic.Comparer<TKey>.Default;

        var cumulativeFrequency = System.Numerics.BigInteger.Zero;

        foreach (var kvp in m_data)
          if (comparer.Compare(kvp.Key, key) <= 0)
            cumulativeFrequency += kvp.Value;

        return (double)cumulativeFrequency / (double)m_totalFrequency * factor;
      }

      /// <summary>Computes the <typeparamref name="TProbability"/> [0, 1] of the <paramref name="key"/> in the histogram, and scales the result with <paramref name="factor"/>.</summary>
      /// <remarks>This function maps </remarks>
      /// <exception cref="System.ArgumentNullException"/>
      public double ComputePmfProbability(TKey key, double factor)
        => m_data.TryGetValue(key, out var frequency) ? (double)frequency / (double)m_totalFrequency * factor : 0d;

      /// <summary>
      /// <para>A cumulative distribution function creates a new CDF dictionary.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Cumulative_distribution_function"/></para>
      /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
      /// </summary>
      /// <typeparam name="TPercentRank"></typeparam>
      /// <returns>A new <see cref="CumulativeDistributionFunction{TKey, TPercentRank}"/> (CDF) dictionary.</returns>
      public CumulativeDistributionFunction<TKey> ToCumulativeDistributionFunction(double factor)
      {
        var totalFrequency = (double)m_totalFrequency;

        var cmf = new CumulativeDistributionFunction<TKey>();

        var cumulativeFrequency = System.Numerics.BigInteger.Zero;

        foreach (var kvp in m_data)
        {
          cumulativeFrequency += kvp.Value;

          cmf[kvp.Key] = (double)cumulativeFrequency / totalFrequency * factor;
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
      public ProbabilityMassFunction<TKey> ToProbabilityMassFunction(double factor)
      {
        var totalFrequency = (double)m_totalFrequency;

        var pmf = new ProbabilityMassFunction<TKey>();

        foreach (var kvp in m_data)
          pmf[kvp.Key] = (double)kvp.Value / totalFrequency * factor;

        return pmf;
      }

      #region Implemented interfaces

      // IDictionary<>
      public System.Numerics.BigInteger this[TKey key] { get => m_data.TryGetValue(key, out var frequency) ? frequency : System.Numerics.BigInteger.Zero; set => m_data[key] = value; }

      public int Count => m_data.Count;

      public bool IsReadOnly => false;

      public System.Collections.Generic.ICollection<TKey> Keys => m_data.Keys;

      public System.Collections.Generic.ICollection<System.Numerics.BigInteger> Values => m_data.Values;

      public void Add(System.Collections.Generic.KeyValuePair<TKey, System.Numerics.BigInteger> kvp) => Add(kvp.Key, kvp.Value);

      public void Clear()
      {
        m_data.Clear();

        m_totalFrequency = System.Numerics.BigInteger.Zero;
      }

      public bool Contains(System.Collections.Generic.KeyValuePair<TKey, System.Numerics.BigInteger> kvp) => m_data.TryGetValue(kvp.Key, out var frequency) && kvp.Value == frequency;

      public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

      public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, System.Numerics.BigInteger>[] array, int index) => m_data.CopyTo(array, index);

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

      public bool Remove(System.Collections.Generic.KeyValuePair<TKey, System.Numerics.BigInteger> kvp) => Contains(kvp) && Remove(kvp.Key);

      public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out System.Numerics.BigInteger value) => m_data.TryGetValue(key, out value);

      public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, System.Numerics.BigInteger>> GetEnumerator() => m_data.GetEnumerator();

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      #endregion // Implemented interfaces
    }
  }
}
