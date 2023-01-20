namespace Flux
{
  public class Histogram<TKey, TFrequency>
    : System.Collections.Generic.IReadOnlyDictionary<TKey, TFrequency>
    where TKey : notnull
    where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
  {
    private System.Collections.Generic.SortedDictionary<TKey, TFrequency> m_data = new System.Collections.Generic.SortedDictionary<TKey, TFrequency>();

    private TFrequency m_totalFrequency = TFrequency.Zero;

    private Histogram() { }

    public TFrequency TotalFrequency => m_totalFrequency;

    #region Static methods.
    public static Histogram<TKey, TFrequency> Create<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector)
    {
      var histogram = new Histogram<TKey, TFrequency>();

      foreach (var item in collection.ThrowIfNull())
      {
        var key = keySelector(item);
        var frequency = frequencySelector(item);

        histogram.m_totalFrequency += frequency;

        if (histogram.TryGetValue(key, out var storedFrequency))
          frequency += storedFrequency;

        histogram.m_data[key] = frequency;
      }

      return histogram;
    }
    #endregion Static methods.

    #region Implemented interfaces.
    public TFrequency this[TKey key] => m_data[key];

    public System.Collections.Generic.IEnumerable<TKey> Keys => m_data.Keys;

    public System.Collections.Generic.IEnumerable<TFrequency> Values => m_data.Values;

    public int Count => m_data.Count;

    public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

    public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TFrequency>> GetEnumerator() => m_data.GetEnumerator();

    public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TFrequency value) => m_data.TryGetValue(key, out value);

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion Implemented interfaces.
  }

  public class ProbabilityMassFunction<TKey, TProbability>
    : System.Collections.Generic.IReadOnlyDictionary<TKey, TProbability>
    where TKey : notnull
    where TProbability : System.Numerics.IFloatingPoint<TProbability>
  {
    private System.Collections.Generic.SortedDictionary<TKey, TProbability> m_data = new System.Collections.Generic.SortedDictionary<TKey, TProbability>();

    private TProbability m_totalFrequency = TProbability.Zero;

    private ProbabilityMassFunction() { }

    public TProbability TotalFrequency => m_totalFrequency;

    #region Static methods.
    public static ProbabilityMassFunction<TKey, TProbability> Create<TFrequency>(Histogram<TKey, TFrequency> histogram)
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
    {
      var totalFrequencies = TProbability.CreateChecked(histogram.TotalFrequency);

      var pmf = new ProbabilityMassFunction<TKey, TProbability>();

      foreach (var item in histogram.ThrowIfNull())
        pmf.m_data[item.Key] = TProbability.CreateChecked(item.Value) / totalFrequencies;

      return pmf;
    }
    #endregion Static methods.

    #region Implemented interfaces.
    public TProbability this[TKey key] => m_data[key];

    public System.Collections.Generic.IEnumerable<TKey> Keys => m_data.Keys;

    public System.Collections.Generic.IEnumerable<TProbability> Values => m_data.Values;

    public int Count => m_data.Count;

    public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

    public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TProbability>> GetEnumerator() => m_data.GetEnumerator();

    public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TProbability value) => m_data.TryGetValue(key, out value);

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion Implemented interfaces.
  }

  public class CumulativeMassFunction<TKey, TPercentRank>
    : System.Collections.Generic.IReadOnlyDictionary<TKey, TPercentRank>
    where TKey : notnull
    where TPercentRank : System.Numerics.IFloatingPoint<TPercentRank>
  {
    private System.Collections.Generic.SortedDictionary<TKey, TPercentRank> m_data = new System.Collections.Generic.SortedDictionary<TKey, TPercentRank>();

    private TPercentRank m_totalFrequency = TPercentRank.Zero;

    private CumulativeMassFunction() { }

    public TPercentRank TotalFrequency => m_totalFrequency;

    #region Static methods.
    public static CumulativeMassFunction<TKey, TPercentRank> Create<TFrequency>(Histogram<TKey, TFrequency> histogram)
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
    {
      var totalFrequencies = TPercentRank.CreateChecked(histogram.TotalFrequency);

      var cmf = new CumulativeMassFunction<TKey, TPercentRank>();

      var cumulativeFrequencies = TPercentRank.Zero;

      foreach (var item in histogram.ThrowIfNull())
      {
        cumulativeFrequencies += TPercentRank.CreateChecked(histogram[item.Key]);

        cmf.m_data[item.Key] = cumulativeFrequencies / totalFrequencies;
      }

      return cmf;
    }
    #endregion Static methods.

    #region Implemented interfaces.
    public TPercentRank this[TKey key] => m_data[key];

    public System.Collections.Generic.IEnumerable<TKey> Keys => m_data.Keys;

    public System.Collections.Generic.IEnumerable<TPercentRank> Values => m_data.Values;

    public int Count => m_data.Count;

    public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

    public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TPercentRank>> GetEnumerator() => m_data.GetEnumerator();

    public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TPercentRank value) => m_data.TryGetValue(key, out value);

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion Implemented interfaces.
  }

  public static partial class Enumerable
  {
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, TFrequency> ToHistogram<TSource, TKey, TFrequency>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector, out TFrequency sumOfAllFrequencies, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

      var histogram = new System.Collections.Generic.SortedDictionary<TKey, TFrequency>(comparer ?? System.Collections.Generic.Comparer<TKey>.Default);

      sumOfAllFrequencies = TFrequency.Zero;

      foreach (var item in source.ThrowIfNull())
      {
        var key = keySelector(item);
        var frequency = frequencySelector(item);

        sumOfAllFrequencies += frequency;

        if (histogram.TryGetValue(key, out var storedFrequency))
          frequency += storedFrequency;

        histogram[key] = frequency;
      }

      return histogram;
    }

    ///// <summary>The PMF is the function that maps from values to probabilities in a distribution, each in the probability range [0, 1].</summary>
    //public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToProbabilityMassFunction<TKey, TFrequency, TFactor>(this System.Collections.Generic.SortedDictionary<TKey, TFrequency> source, TFactor factor, TFrequency sumOfAllFrequencies)
    //  where TKey : notnull
    //  where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
    //  where TFactor : System.Numerics.IFloatingPoint<TFactor>
    //{
    //  var pmf = new System.Collections.Generic.SortedDictionary<TKey, TFactor>(source.Comparer);

    //  foreach (var key in source.Keys)
    //    pmf[key] = TFactor.CreateChecked(source[key]) / TFactor.CreateChecked(sumOfAllFrequencies) * factor;

    //  return pmf;
    //}
    ///// <summary>The PMF is the function that maps from values to probabilities in a distribution, each in the probability range [0, 1].</summary>
    //public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToProbabilityMassFunction<TKey, TFrequency, TFactor>(this System.Collections.Generic.SortedDictionary<TKey, TFrequency> source, TFactor factor)
    //  where TKey : notnull
    //  where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
    //  where TFactor : System.Numerics.IFloatingPoint<TFactor>
    //  => ToProbabilityMassFunction(source, factor, source.Values.Sum());

    ///// <summary>The PMF is the function that maps from values to probabilities in a distribution, each in the probability range [0, 1].</summary>
    //public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToProbabilityMassFunction<TSource, TKey, TFrequency, TFactor>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector, TFactor factor, System.Collections.Generic.IComparer<TKey>? comparer = null)
    //  where TKey : notnull
    //  where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
    //  where TFactor : System.Numerics.IFloatingPoint<TFactor>
    //{
    //  var histogram = ToHistogram(source, keySelector, frequencySelector, out var sumOfAllFrequencies, comparer);

    //  return ToProbabilityMassFunction(histogram, factor, sumOfAllFrequencies);
    //}

    /// <summary>The CDF is the function that maps values to their percentile rank in a distribution, each in the probability range [0, 1].</summary>
    /// <remarks>For consistency, a discrete CDF should be called a cumulative mass function(CMF), but that seems just ignored.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToCumulativeMassFunction<TKey, TFrequency, TFactor>(this System.Collections.Generic.SortedDictionary<TKey, TFrequency> source, TFactor factor, TFrequency sumOfAllFrequencies)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      where TFactor : System.Numerics.IFloatingPoint<TFactor>
    {
      var cmf = new System.Collections.Generic.SortedDictionary<TKey, TFactor>(source.Comparer);

      var cumulativeFrequencies = TFactor.Zero;

      foreach (var key in source.Keys)
      {
        cumulativeFrequencies += TFactor.CreateChecked(source[key]);

        cmf[key] = cumulativeFrequencies / TFactor.CreateChecked(sumOfAllFrequencies) * factor;
      }

      return cmf;
    }
    /// <summary>The CDF is the function that maps values to their percentile rank in a distribution, each in the probability range [0, 1].</summary>
    /// <remarks>For consistency, a discrete CDF should be called a cumulative mass function(CMF), but that seems just ignored.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToCumulativeMassFunction<TKey, TFrequency, TFactor>(this System.Collections.Generic.SortedDictionary<TKey, TFrequency> source, TFactor factor)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      where TFactor : System.Numerics.IFloatingPoint<TFactor>
      => ToCumulativeMassFunction(source, factor, source.Values.Sum());

    /// <summary>The CDF is the function that maps values to their percentile rank in a distribution. Using a factor of 100 </summary>
    /// <remarks>For consistency, a discrete CDF should be called a cumulative mass function(CMF), but that seems just ignored.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToCumulativeMassFunction<TSource, TKey, TFrequency, TFactor>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector, TFactor factor, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      where TFactor : System.Numerics.IFloatingPoint<TFactor>
    {
      var histogram = ToHistogram(source, keySelector, frequencySelector, out var sumOfAllFrequencies, comparer);

      return ToCumulativeMassFunction(histogram, factor, sumOfAllFrequencies);
    }
  }
}
