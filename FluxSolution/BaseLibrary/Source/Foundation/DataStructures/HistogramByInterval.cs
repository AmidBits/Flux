namespace Flux
{
  public static partial class Histogram
  {
    public static Histogram<TKey> CreateDegenerate<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector)
      where TKey : notnull
    {
      var h = new Histogram<TKey>();

      foreach (var item in source)
      {
        var key = keySelector(item);
        var frequency = frequencySelector(item);

        if (h.TryGetValue(key, out var currentFrequency))
          h[key] = currentFrequency + frequency;
        else
          h.Add(key, frequency);
      }

      return h;
    }

    public static Histogram<TKey> CreateClosedOpen<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IList<double> intervals, System.Func<TSource, double> valueSelector, System.Func<double, double, TKey> keySelector, System.Func<TSource, int> frequencySelector)
      where TKey : notnull
    {
      var h = new Histogram<TKey>();

      foreach (var item in source)
      {
        var key = GetIntervalKey(valueSelector(item), intervals);
        var frequency = frequencySelector(item);

        if (h.TryGetValue(key, out var currentFrequency))
          h[key] = currentFrequency + frequency;
        else
          h.Add(key, frequency);
      }

      return h;

      TKey GetIntervalKey(double value, System.Collections.Generic.IList<double> intervals)
      {
        if (value < intervals[0])
          return keySelector(double.NegativeInfinity, intervals[0]);

        for (var i = 1; i < intervals.Count; i++)
          if (value < intervals[i])
            return keySelector(intervals[i - 1], intervals[i]);

        return keySelector(intervals[intervals.Count - 1], double.PositiveInfinity);
      }
    }

    //public static void AddClosedOpen(int frequency, double value, System.Collections.Generic.IList<double> intervals, System.Func<double, double, TKey> keySelector)
    //{
    //  var key = GetIntervalKey(value, intervals);

    //  m_histogram[key] = m_histogram.TryGetValue(key, out var currentFrequency) ? currentFrequency + frequency : frequency;

    //  TKey GetIntervalKey(double value, System.Collections.Generic.IList<double> intervals)
    //  {
    //    if (value < intervals[0])
    //      return keySelector(double.NegativeInfinity, intervals[0]);

    //    for (var i = 1; i < intervals.Count; i++)
    //      if (value < intervals[i])
    //        return keySelector(intervals[i - 1], intervals[i]);

    //    return keySelector(intervals[intervals.Count - 1], double.PositiveInfinity);
    //  }
    //}

  }

  public class Histogram<TKey>
    : System.Collections.Generic.SortedDictionary<TKey, int>
    where TKey : notnull
  {
    public override string ToString() 
      => string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(this, kvp => $"{kvp.Key}\t:\t{kvp.Value}"));
  }

  public interface IBucket
  {
    int Frequency { get; }
    string Label { get; }

    void Add(int frequency);
    bool Contains(double value);
  }

  public class BucketValue
    : IBucket
  {
    private int m_frequency;
    private double m_value;

    public BucketValue(double value, int frequency)
    {
      m_value = value;
      m_frequency = frequency;
    }

    public int Frequency => m_frequency;

    public string Label => $"{Value} = {Frequency}";

    public double Value => m_value;

    public void Add(int frequency) => m_frequency += frequency;

    public bool Contains(double value) => value == m_value;

    public override string ToString() => $"{Label} = {Frequency}";
  }

  public class BucketClosedOpen
    : IBucket
  {
    private int m_frequency;
    private double m_valueLeftClosed;
    private double m_valueRightOpen;

    public BucketClosedOpen(double valueLeftClosed, double valueRightOpen, int frequency)
    {
      m_valueLeftClosed = valueLeftClosed;
      m_valueRightOpen = valueRightOpen;
      m_frequency = frequency;
    }

    public int Frequency => m_frequency;

    public string Label => $"[{m_valueLeftClosed}, {m_valueRightOpen})";

    public double ValueLeftClosed => m_valueLeftClosed;
    public double ValueRightOpen => m_valueRightOpen;

    public double Width => m_valueRightOpen - m_valueLeftClosed;

    public void Add(int frequency) => m_frequency += frequency;

    public bool Contains(double value) => value >= m_valueLeftClosed && value < m_valueRightOpen;

    public override string ToString() => $"{Label} = {Frequency}";
  }

  public class HistogramByInterval
  {
    private System.Collections.Generic.List<BucketClosedOpen> m_buckets;

    public HistogramByInterval(params double[] binIntervals)
    {
      m_buckets = new System.Collections.Generic.List<BucketClosedOpen>();

      m_buckets.Add(new BucketClosedOpen(double.NegativeInfinity, binIntervals[0], 0));

      for (var i = 1; i < binIntervals.Length; i++)
        m_buckets.Add(new BucketClosedOpen(binIntervals[i - 1], binIntervals[i], 0));

      m_buckets.Add(new BucketClosedOpen(binIntervals[binIntervals.Length - 1], double.PositiveInfinity, 0));
    }
    public HistogramByInterval(System.Collections.Generic.IEnumerable<double> binIntervals)
      : this(binIntervals.ToArray()) { }

    public int BucketCount
      => m_buckets.Count - 1;

    public void AddValue(double value)
    {
      if (GetBucket(value) is var bucket)
        bucket.Add(1);
    }

    public void AddValues(System.Collections.Generic.IEnumerable<double> values)
    {
      foreach (var value in values)
        AddValue(value);
    }
    public void AddValues(params double[] values)
    {
      for (var i = 0; i < values.Length; i++)
        AddValue(values[i]);
    }

    public IBucket GetBucket(double value)
    {
      var buckets = GetBuckets();

      for (var i = 0; i < buckets.Length; i++)
        if (buckets[i].Contains(value))
          return buckets[i];

      throw new System.ArgumentOutOfRangeException(nameof(value));
    }

    public System.ReadOnlySpan<IBucket> GetBuckets()
      => m_buckets.ToArray();

    //public System.Collections.Generic.IEnumerable<(double lowerBound, double upperBound, double frequency)> GetBuckets()
    //{
    //  //yield return (double.MinValue, m_intervals[0], m_frequencies[0]);

    //  foreach (var interval in m_buckets.PartitionTuple2(false, (low, high, index) => (low, high, index)))
    //    yield return (interval.low, interval.high, m_frequencies[interval.index + 1]);

    //  //yield return (m_intervals[m_intervals.Count - 1], double.MaxValue, m_frequencies[m_frequencies.Length - 1]);
    //}

    //public static bool TryGetBucket(double value, System.Collections.Generic.IList<double> binIntervals, out int index, out double width, out int compared)
    //{
    //  index = GetBucketIndex();

    //  if (index == 0)
    //  {
    //    width = binIntervals[0] - double.MinValue;
    //    compared = -1;
    //    return false;
    //  }

    //  if (index == binIntervals.Count)
    //  {
    //    width = double.MaxValue - binIntervals[binIntervals.Count - 1];
    //    compared = 1;
    //    return false;
    //  }

    //  width = binIntervals[index] - binIntervals[index - 1];
    //  compared = 0;
    //  return true;

    //  int GetBucketIndex()
    //  {
    //    for (var i = 0; i < binIntervals.Count; i++)
    //      if (value < binIntervals[i])
    //        return i;

    //    return binIntervals.Count;
    //  }
    //}

    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();

      var buckets = GetBuckets();

      for (var i = 0; i < buckets.Length; i++)
      {
        sb.AppendLine(buckets[i].ToString());
      }

      return sb.ToString();
    }
    //      => string.Join(System.Environment.NewLine, GetBuckets().Select(e => $"[{e.ClosedLowerBound}, {e.OpenUpperBound}) = {e.Frequency}"));
  }
}
