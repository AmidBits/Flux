//namespace Flux.DataStructures.Statistics
//{
//  //public interface IHistogramBin
//  //{
//  //  public int Count { get; }
//  //}

//  /// <summary>A bin (a.k.a. bucket) for a histogram.</summary>
//  public record struct Bin
//    : System.IComparable<double>, System.IComparable<Bin>
//  {
//    private int m_count;

//    private double m_lowerBound;
//    private double m_upperBound;

//    public Bin(double lowerBound, double upperBound, int count)
//    {
//      if (lowerBound > upperBound) throw new System.ArgumentOutOfRangeException(nameof(lowerBound));
//      if (upperBound < lowerBound) throw new System.ArgumentOutOfRangeException(nameof(upperBound));
//      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

//      m_count = count;

//      m_lowerBound = lowerBound;
//      m_upperBound = upperBound;
//    }
//    public Bin(double value, int count)
//      : this(value, value, count)
//    {
//    }

//    /// <summary>The number of datapoints in the bin.</summary>
//    public int Count { get => m_count; set => m_count = value; }

//    //public bool HasWidth => m_lowerBound != m_upperBound;

//    /// <summary>The lower bound of the bin.</summary>
//    public double LowerBound { get => m_lowerBound; set => m_lowerBound = value; }
//    /// <summary>The upper bound of the bin.</summary>
//    public double UpperBound { get => m_upperBound; set => m_upperBound = value; }

//    public string AutoLabel => m_lowerBound != m_upperBound ? $"{m_lowerBound} - {m_upperBound}" : m_lowerBound.ToString();

//    /// <summary>Width of the bin.</summary>
//    public double Width => m_upperBound - m_lowerBound;

//    #region Implemented interfaces

//    /// <summary>Compares the bucket to the datapoint.</summary>
//    /// <returns>A value that represents the bucket in relation to the datapoint:
//    ///  0 if the bucket contains the datapoint (i.e. the datapoint is within the bucket boundaries),
//    /// -1 if the bucket is smaller than the datapoint,
//    /// +1 if the bucket is larger than the datapoint.
//    /// </returns>
//    public int CompareTo(double x)
//      => UpperBound < x ? -1 : LowerBound > x ? 1 : 0;

//    public int CompareTo(Bin other)
//    {
//      if (UpperBound > other.LowerBound && LowerBound < other.UpperBound)
//        throw new System.ArgumentException("The bins overlap.");

//      return LowerBound < other.LowerBound ? -1 : 1;
//    }
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override int GetHashCode()
//      => System.HashCode.Combine(m_count, m_lowerBound, m_upperBound);
//    public override string ToString()
//      => $"{GetType().Name} {{ {AutoLabel} = {m_count} }}";
//    #endregion Object overrides
//  }

//  public class Histogram
//  {
//    private readonly List<Bin> m_bins;

//    private Bin m_binGreaterThan;
//    private Bin m_binInBetween;
//    private Bin m_binLessThan;

//    public Histogram(params Bin[] bins)
//    {
//      m_bins = new();

//      foreach (var bin in bins)
//        AddBin(bin);

//      m_bins.Sort();

//      m_binGreaterThan = new(double.MaxValue, 0);
//      m_binInBetween = new(0, 0);
//      m_binLessThan = new(double.MinValue, 0);
//    }

//    public Bin AddBin(Bin bin)
//    {
//      if (m_bins.Contains(bin)) throw new System.ArgumentException($"The bin already exist.");

//      m_bins.Add(bin);
//      m_bins.Sort();

//      return bin;
//    }

//    public Bin AddValue(double value)
//    {
//      for (var i = 0; i < m_bins.Count; i++)
//      {
//        var bin = m_bins[i];

//        if (bin.CompareTo(value) == 0)
//        {
//          bin.Count += 1;

//          m_bins[i] = bin;

//          return bin;
//        }
//      }

//      if (m_bins[0].CompareTo(value) < 0)
//        m_binLessThan.Count++;
//      if (m_bins[^1].CompareTo(value) > 0)
//        m_binGreaterThan.Count++;
//      return AddBin(new Bin(value, value, 1)); // No bin exists, create one specific for the value.
//    }

//    public override string ToString()
//    {
//      return $"{GetType().Name} {{ {string.Join(", ", m_bins)} }}";
//    }
//  }

//  /// <summary>A simple generic histogram. The property Count represents the number of bins, and Frequencies the number of total frequencies across all bins.</summary>
//  public sealed class SimpleHistogram<TKey>
//    : System.Collections.Generic.IReadOnlyDictionary<TKey, int>
//    where TKey : System.IComparable<TKey>
//  {
//    private readonly System.Collections.Generic.SortedDictionary<TKey, int> m_bins;

//    private int m_frequencies;

//    public SimpleHistogram()
//    {
//      m_bins = new System.Collections.Generic.SortedDictionary<TKey, int>();

//      m_frequencies = 0;
//    }

//    public int Frequencies => m_frequencies;

//    public void Add(TKey key, int frequency)
//    {
//      if (TryGetValue(key, out var currentCount))
//        m_bins[key] = currentCount + frequency;
//      else
//        m_bins.Add(key, frequency);

//      m_frequencies += frequency;
//    }
//    public void Add(TKey key)
//      => Add(key, 1);
//    public void Add(System.Collections.Generic.IEnumerable<TKey> keys)
//    {
//      foreach (var key in keys)
//        Add(key, 1);
//    }
//    public void Add<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector)
//    {
//      foreach (var element in collection)
//        Add(keySelector(element), frequencySelector(element));
//    }

//    public double CumulativeDistributionFunction(TKey value)
//    {
//      var count = 0;

//      foreach (var kvp in m_bins)
//        if (kvp.Key.CompareTo(value) <= 0)
//          count += kvp.Value;

//      return (double)count / m_frequencies;
//    }

//    public double ProbabilityMassFunction(TKey key)
//    {
//      if (m_bins.TryGetValue(key, out var value))
//        return (double)value / m_frequencies;

//      throw new System.ArgumentOutOfRangeException(nameof(key));
//    }

//    public System.Collections.Generic.SortedDictionary<TKey, double> ToProbabilityMassFunction()
//    {
//      return this.ToSortedDictionary(kvp => kvp.Key, kvp => (double)kvp.Value / Frequencies);
//    }

//    #region Implemented interfaces
//    // IReadOnlyDictionary<>
//    public int this[TKey key] => m_bins[key];
//    public System.Collections.Generic.IEnumerable<TKey> Keys => m_bins.Keys;
//    public System.Collections.Generic.IEnumerable<int> Values => m_bins.Values;
//    public int Count => m_bins.Count;
//    public bool ContainsKey(TKey key) => m_bins.ContainsKey(key);
//    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, int>> GetEnumerator() => m_bins.GetEnumerator();
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => m_bins.GetEnumerator();
//    public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out int value) => m_bins.TryGetValue(key, out value);
//    #endregion Implemented interfaces
//  }
//}
