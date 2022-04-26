namespace Flux.DataStructures
{
  //public interface IHistogramBin
  //{
  //  public int Count { get; }
  //}

  public struct HistogramBin
    : System.IComparable<double>, System.IComparable<HistogramBin>, System.IEquatable<HistogramBin>
  {
    private int m_count;

    private readonly double m_lowerBound;
    private readonly double m_upperBound;

    public HistogramBin(double lowerBound, double upperBound, int count = 0)
    {
      m_count = count;

      m_lowerBound = lowerBound;
      m_upperBound = upperBound;
    }


    /// <summary>The number of datapoints in the bin.</summary>
    public int Count { get => m_count; set => m_count = value; }

    /// <summary>The lower bound of the bin.</summary>
    public double LowerBound => m_lowerBound;
    /// <summary>The upper bound of the bin.</summary>
    public double UpperBound => m_upperBound;

    /// <summary>Width of the bin.</summary>
    public double Width => m_upperBound - m_lowerBound;

    #region Implemented interfaces

    /// <summary>Compares the bucket to the datapoint.</summary>
    /// <returns>A value that represents the bucket in relation to the datapoint:
    ///  0 if the bucket contains the datapoint (i.e. the datapoint is within the bucket boundaries),
    /// -1 if the bucket is smaller than the datapoint,
    /// +1 if the bucket is larger than the datapoint.
    /// </returns>
    public int CompareTo(double x)
      => UpperBound < x ? -1 : LowerBound > x ? 1 : 0;

    public int CompareTo(HistogramBin other)
    {
      if (UpperBound > other.LowerBound && LowerBound < other.UpperBound)
        throw new System.ArgumentException("The bins overlap.");

      return LowerBound < other.LowerBound ? -1 : 1;
    }

    public bool Equals(HistogramBin other)
      => m_count == other.m_count && m_lowerBound == other.m_lowerBound && m_upperBound == other.m_upperBound;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj)
      => obj is var o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_count, m_lowerBound, m_upperBound);
    public override string ToString()
      => $"{GetType().Name} {{ {m_lowerBound} - {m_upperBound} = {m_count} }}";
    #endregion Object overrides
  }

  public class HistogramX
  {
    private readonly List<HistogramBin> m_bins;

    public HistogramX(params HistogramBin[] bins)
    {
      m_bins = new();

      foreach (var bin in bins)
        AddBin(bin);
    }

    public void AddBin(HistogramBin bin)
    {
      if (m_bins.Contains(bin)) throw new System.ArgumentException($"The bin already exist.");

      m_bins.Add(bin);
      m_bins.Sort();
    }

    public void AddValue(double value)
    {
      if (m_bins.Any(b => b.CompareTo(value) == 0))
      {
        for (var i = 0; i < m_bins.Count; i++)
        {
          var bin = m_bins[i];
          if (bin.CompareTo(value) == 0)
          {
            bin.Count += 1;
            m_bins[i] = bin;
          }
        }
      }
      else
      {
        var bin = new HistogramBin(value, value, 1);
        AddBin(bin);
      }
    }
  }

  /// <summary>A simple generic histogram. The property Count represents the number of bins, and Frequencies the number of total frequencies across all bins.</summary>
  public sealed class SimpleHistogram<TKey>
    : System.Collections.Generic.IReadOnlyDictionary<TKey, int>
    where TKey : System.IComparable<TKey>
  {
    private readonly System.Collections.Generic.SortedDictionary<TKey, int> m_bins;

    private int m_frequencies;

    public SimpleHistogram()
    {
      m_bins = new System.Collections.Generic.SortedDictionary<TKey, int>();

      m_frequencies = 0;
    }

    public int Frequencies => m_frequencies;

    public void Add(TKey key, int frequency)
    {
      if (TryGetValue(key, out var currentCount))
        m_bins[key] = currentCount + frequency;
      else
        m_bins.Add(key, frequency);

      m_frequencies += frequency;
    }
    public void Add(TKey key)
      => Add(key, 1);
    public void Add(System.Collections.Generic.IEnumerable<TKey> keys)
    {
      foreach (var key in keys)
        Add(key, 1);
    }
    public void Add<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector)
    {
      foreach (var element in collection)
        Add(keySelector(element), frequencySelector(element));
    }

    public double CumulativeDistributionFunction(TKey value)
    {
      var count = 0;

      foreach (var kvp in m_bins)
        if (kvp.Key.CompareTo(value) <= 0)
          count += kvp.Value;

      return (double)count / m_frequencies;
    }

    public double ProbabilityMassFunction(TKey key)
    {
      if (m_bins.ContainsKey(key))
        return (double)m_bins[key] / m_frequencies;

      throw new System.ArgumentOutOfRangeException(nameof(key));
    }

    public System.Collections.Generic.SortedDictionary<TKey, double> ToProbabilityMassFunction()
    {
      return this.ToSortedDictionary(kvp => kvp.Key, kvp => (double)kvp.Value / Frequencies);
    }

    #region Implemented interfaces
    // IReadOnlyDictionary<>
    public int this[TKey key] => m_bins[key];
    public System.Collections.Generic.IEnumerable<TKey> Keys => m_bins.Keys;
    public System.Collections.Generic.IEnumerable<int> Values => m_bins.Values;
    public int Count => m_bins.Count;
    public bool ContainsKey(TKey key) => m_bins.ContainsKey(key);
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, int>> GetEnumerator() => m_bins.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => m_bins.GetEnumerator();
    public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out int value) => m_bins.TryGetValue(key, out value);
    #endregion Implemented interfaces
  }
}
