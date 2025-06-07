namespace Flux.DataStructures
{
  /// <summary>
  /// <para>A histogram dictionary based on <see cref="SortedDictionary{TKey, System.Numerics.BigInteger}"/>.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Histogram"/></para>
  /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  public sealed class Histogram<TKey, TFrequency>
    : System.Collections.Generic.IDictionary<TKey, TFrequency>
    where TKey : System.Numerics.INumber<TKey>
    where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
  {
    private readonly System.Collections.Generic.Dictionary<TKey, TFrequency> m_data = [];

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
      foreach (var item in collection.ThrowOnNull())
        Add(keySelector(item), frequencySelector(item));

      return this;
    }
    public Histogram<TKey, TFrequency> AddRange(System.Collections.Generic.IEnumerable<TKey> collection) => AddRange(collection, key => key, key => TFrequency.One);
    public Histogram<TKey, TFrequency> AddRange(Histogram<TKey, TFrequency> histogram) => AddRange(histogram, he => he.Key, he => he.Value);

    /// <summary>Computes the <typeparamref name="TPercentRank"/> [0, 1] of the <paramref name="key"/> in the histogram, and scales the result with <paramref name="factor"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public double ComputeCdfPercentRank(TKey key, double factor)
    {
      var comparer = System.Collections.Generic.Comparer<TKey>.Default;

      var cumulativeFrequency = TFrequency.Zero;

      foreach (var kvp in m_data)
        if (comparer.Compare(kvp.Key, key) <= 0)
          cumulativeFrequency += kvp.Value;

      return double.CreateChecked(cumulativeFrequency) / double.CreateChecked(m_totalFrequency) * factor;
    }

    /// <summary>Computes the <typeparamref name="TProbability"/> [0, 1] of the <paramref name="key"/> in the histogram, and scales the result with <paramref name="factor"/>.</summary>
    /// <remarks>This function maps </remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public double ComputePmfProbability(TKey key, double factor)
      => m_data.TryGetValue(key, out var frequency) ? double.CreateChecked(frequency) / double.CreateChecked(m_totalFrequency) * factor : 0d;

    /// <summary>
    /// <para>A cumulative distribution function creates a new CDF dictionary.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cumulative_distribution_function"/></para>
    /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
    /// </summary>
    /// <typeparam name="TPercentRank"></typeparam>
    /// <returns>A new <see cref="CumulativeDistributionFunction{TKey, TPercentRank}"/> (CDF) dictionary.</returns>
    public CumulativeDistributionFunction<TKey, TPercentRank> ToCumulativeDistributionFunction<TPercentRank>(TPercentRank factor)
      where TPercentRank : System.Numerics.IFloatingPointIeee754<TPercentRank>
    {
      var totalFrequency = TPercentRank.CreateChecked(m_totalFrequency);

      var cmf = new CumulativeDistributionFunction<TKey, TPercentRank>();

      var cumulativeFrequency = TFrequency.Zero;

      foreach (var kvp in m_data)
      {
        cumulativeFrequency += kvp.Value;

        cmf[kvp.Key] = TPercentRank.CreateChecked(cumulativeFrequency) / totalFrequency * factor;
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
      where TProbability : System.Numerics.IFloatingPointIeee754<TProbability>
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

    public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TFrequency> kvp) => m_data.TryGetValue(kvp.Key, out var frequency) && kvp.Value == frequency;

    public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

    public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TFrequency>[] array, int index)
    {
      foreach (var kvp in m_data)
        array[index++] = kvp;
    }

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

    public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TFrequency> kvp) => Contains(kvp) && Remove(kvp.Key);

    public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TFrequency value) => m_data.TryGetValue(key, out value);

    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TFrequency>> GetEnumerator() => m_data.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion // Implemented interfaces
  }
}
