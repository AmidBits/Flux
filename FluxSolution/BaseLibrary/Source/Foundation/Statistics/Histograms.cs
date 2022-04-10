namespace Flux
{
  public sealed class HistogramUniform
  {
    public int BucketCount { get; }

    public double MaximumValue { get; }
    public double MinimumValue { get; }

    private readonly int[] m_buckets;
    /// <summary>Contains the requested buckets plus underflow/overflow buckets for values outside of min/max.</summary>
    public System.ReadOnlySpan<int> Buckets
      => m_buckets;

    public HistogramUniform(int bucketCount, double minimumValue, double maximumValue)
    {
      BucketCount = bucketCount;

      m_buckets = new int[1 + BucketCount + 1];

      MaximumValue = maximumValue;
      MinimumValue = minimumValue;
    }
    public HistogramUniform(int bucketCount, double minimumValue, double maximumValue, System.Collections.Generic.IEnumerable<double> values)
      : this(bucketCount, minimumValue, maximumValue)
    {
      AddTo(values);
    }

    public void AddTo(System.Collections.Generic.IEnumerable<double> values)
    {
      foreach (var value in values ?? throw new System.ArgumentNullException(nameof(values)))
      {
        var bucketIndex = (int)(BucketCount * (value - MinimumValue) / (MaximumValue - MinimumValue));

        if (bucketIndex < 0)
          m_buckets[0]++;
        else if (bucketIndex >= BucketCount)
          m_buckets[BucketCount + 1]++;
        else // if (bucketIndex >= 0 && bucketIndex < BucketCount)
          m_buckets[bucketIndex + 1]++;
      }
    }
  }
}
