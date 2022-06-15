namespace Flux
{
  public interface IHistogram<TKey>
    where TKey : System.IComparable<TKey>
  {
    void Add(System.Collections.Generic.IEnumerable<TKey> keys);
    System.ReadOnlySpan<int> Bins { get; }
    int Count { get; }
    TKey MaxValue { get; }
    TKey MinValue { get; }
  }

  public sealed class Histogram1
    : IHistogram<double>
  {
    private readonly int[] m_bins;

    public readonly double[] m_binRanges;

    public Histogram1(params double[] binRanges)
    {
      m_bins = new int[1 + binRanges.Length + 1];

      m_binRanges = binRanges;
    }

    /// <summary>Contains the requested buckets plus underflow/overflow buckets for values outside of min/max.</summary>
    public System.ReadOnlySpan<int> Bins
      => (System.ReadOnlySpan<int>)m_bins;

    public int Count
      => m_bins.Length;

    public double MaxValue
      => m_binRanges[^1];
    public double MinValue
      => m_binRanges[0];

    public void Add(double value)
    {
      //var v = value / MinValue;
      //v += 1;
      //var binIndex = System.Convert.ToInt32(v);
      //var binIndex = System.Convert.ToInt32(Maths.Rescale(value, MinValue, MaxValue, 1, m_binRanges.Length));

      //if (value < MinValue)
      //  m_bins[0]++;
      //else if (value > MaxValue)
      //  m_bins[m_bins.Length - 1]++;
      //else if (binIndex == 0)
      //  throw new Exception();
      //else // If within the boundary of min and max.
      {
        //var binIndex = System.Convert.ToInt32(Maths.Rescale(value, MinValue, MaxValue, 1, m_binRanges.Length));
        var binIndex = System.Convert.ToInt32(value * 10);
        m_bins[binIndex]++;
      }
    }
    public void Add(System.Collections.Generic.IEnumerable<double> values)
    {
      foreach (var value in values ?? throw new System.ArgumentNullException(nameof(values)))
        Add(value);
    }
  }

  //public sealed class Histogram2<TKey>
  //  : IHistogram<int>
  //{
  //  private readonly int[] m_bins;

  //  public readonly int m_maxValue;
  //  public readonly int m_minValue;

  //  public Histogram2(int minValue, int maxValue)
  //  {
  //    m_bins = new int[1 + binRanges.Length + 1];

  //    m_minValue = minValue;
  //    m_maxValue = maxValue;
  //  }

  //  /// <summary>Contains the requested buckets plus underflow/overflow buckets for values outside of min/max.</summary>
  //  public System.ReadOnlySpan<int> Bins
  //    => (System.ReadOnlySpan<int>)m_bins;

  //  public int Count
  //    => m_bins.Length;

  //  public int MaxValue
  //    => m_maxValue;
  //  public int MinValue
  //    => m_minValue;

  //  public void Add(System.Collections.Generic.IEnumerable<int> values)
  //  {
  //    foreach (var value in values ?? throw new System.ArgumentNullException(nameof(values)))
  //    {
  //      var binIndex = System.Convert.ToInt32(Maths.Rescale(value, MinValue, MaxValue, 1, m_bins.Length));

  //      if (value < MinValue)
  //        m_bins[0]++;
  //      else if (value > MaxValue)
  //        m_bins[Count + 1]++;
  //      else // If within the boundary of min and max.
  //        m_bins[binIndex]++;
  //    }
  //  }
  //}
}
