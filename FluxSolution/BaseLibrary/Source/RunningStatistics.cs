namespace Flux
{
  public class RunningStatistics
  {
    private long m_count;
    /// <summary>The number of samples.</summary>
    public long Count
      => m_count;

    private double m_hm;

    private double m_m1;
    private double m_m2;
    private double m_m3;
    private double m_m4;

    private double m_max = double.NegativeInfinity;
    /// <summary>Returns the maximum value of all samples, or NaN if no data/any entry is NaN.</summary>
    public double Maximum
      => m_count > 0 ? m_max : double.NaN;

    private double m_max_abs = double.NegativeInfinity;
    /// <summary>Returns the largest absolute value of all samples. Returns NaN if data is empty or any entry is NaN.</summary>
    public double MaximumAbsolute
      => m_count > 0 ? m_max_abs : double.NaN;

    private double m_min = double.PositiveInfinity;
    /// <summary>Returns the minimum value of all samples. Returns NaN if data is empty or if any entry is NaN.</summary>
    public double Minimum
      => m_count > 0 ? m_min : double.NaN;

    private double m_min_abs = double.PositiveInfinity;
    /// <summary>Returns the smallest absolute value of all samples. Returns NaN if data is empty or any entry is NaN.</summary>
    public double MinimumAbsolute
      => m_count > 0 ? m_min_abs : double.NaN;

    private double m_sum = 0;
    /// <summary>The sum of all samples. Returns NaN if no data or any sample is NaN.</summary>
    public double SumOfSamples
      => m_count > 0 ? m_sum : double.NaN;

    private double m_product = 1;
    /// <summary>The product of all samples. Returns NaN if no data or any sample is NaN.</summary>
    public double ProductOfSamples
      => m_count > 0 ? m_product : double.NaN;

    /// <summary>Evaluates the sample mean, an estimate of the population mean. Returns NaN if data is empty or if any entry is NaN.</summary>
    public double Mean
      => m_count > 0 ? m_m1 : double.NaN;

    /// <summary>Evaluates the geometric mean of the enumerable, in a single pass without memoization. Returns NaN if data is empty or any entry is NaN.</summary>    
    public double GeometricMean
      => m_count > 0 ? System.Math.Pow(m_product, 1.0 / m_count) : double.NaN;

    /// <summary>Evaluates the harmonic mean of the enumerable, in a single pass without memoization. Returns NaN if data is empty or any entry is NaN.</summary>
    public double HarmonicMean
      => m_count > 0 ? m_count / m_hm : double.NaN;

    /// <summary>Estimates the unbiased population variance from the provided samples. On a dataset of size N will use an N-1 normalizer (Bessel's correction). Returns NaN if data has less than two entries or if any entry is NaN.</summary>
    public double Variance
      => m_count < 2 ? double.NaN : m_m2 / (m_count - 1);

    /// <summary>Evaluates the variance from the provided full population. On a dataset of size N will use an N normalizer and would thus be biased if applied to a subset. Returns NaN if data is empty or if any entry is NaN.</summary>
    public double PopulationVariance
      => m_count < 2 ? double.NaN : m_m2 / m_count;

    /// <summary> Estimates the unbiased population standard deviation from the provided samples. On a dataset of size N will use an N-1 normalizer (Bessel's correction). Returns NaN if data has less than two entries or if any entry is NaN.</summary>
    public double StandardDeviation
      => m_count < 2 ? double.NaN : System.Math.Sqrt(m_m2 / (m_count - 1));

    /// <summary>Evaluates the standard deviation from the provided full population. On a dataset of size N will use an N normalizer and would thus be biased if applied to a subset. Returns NaN if data is empty or if any entry is NaN.</summary>
    public double PopulationStandardDeviation
      => m_count < 2 ? double.NaN : System.Math.Sqrt(m_m2 / m_count);

    /// <summary>Estimates the unbiased population skewness from the provided samples. Uses a normalizer (Bessel's correction; type 2). Returns NaN if data has less than three entries or if any entry is NaN.</summary>
    public double Skewness
      => m_count < 3 ? double.NaN : (m_count * m_m3 * System.Math.Sqrt(m_m2 / (m_count - 1)) / (m_m2 * m_m2 * (m_count - 2))) * (m_count - 1);

    /// <summary>Evaluates the population skewness from the full population. Does not use a normalizer and would thus be biased if applied to a subset (type 1). Returns NaN if data has less than two entries or if any entry is NaN. </summary>
    public double PopulationSkewness
      => m_count < 2 ? double.NaN : System.Math.Sqrt(m_count) * m_m3 / System.Math.Pow(m_m2, 1.5);

    /// <summary>Estimates the unbiased population kurtosis from the provided samples. Uses a normalizer (Bessel's correction; type 2). Returns NaN if data has less than four entries or if any entry is NaN.</summary>
    public double Kurtosis
      => m_count < 4 ? double.NaN : ((double)m_count * m_count - 1) / ((m_count - 2) * (m_count - 3)) * (m_count * m_m4 / (m_m2 * m_m2) - 3 + 6.0 / (m_count + 1));

    /// <summary>Evaluates the population kurtosis from the full population. Does not use a normalizer and would thus be biased if applied to a subset (type 1). Returns NaN if data has less than three entries or if any entry is NaN.</summary>
    public double PopulationKurtosis
      => m_count < 3 ? double.NaN : m_count * m_m4 / (m_m2 * m_m2) - 3.0;

    public RunningStatistics() { }
    public RunningStatistics(System.Collections.Generic.IEnumerable<double> values) => PushRange(values);

    /// <summary>Update the running statistics by adding another observed sample (in-place).</summary>
    public void Push(double value)
    {
      m_count++;

      var d = value - m_m1;
      var s = d / m_count;
      var s2 = s * s;
      var t = d * s * (m_count - 1);

      m_hm += 1.0 / value;

      m_m1 += s;
      m_m4 += t * s2 * (m_count * m_count - 3 * m_count + 3) + 6 * s2 * m_m2 - 4 * s * m_m3;
      m_m3 += t * s * (m_count - 2) - 3 * s * m_m2;
      m_m2 += t;

      m_product *= value;

      m_sum += value;

      var isNaN = double.IsNaN(value);

      if (value > m_max || isNaN) m_max = value;
      if (value < m_min || isNaN) m_min = value;

      var abs = System.Math.Abs(value);

      if (abs > m_max_abs || isNaN) m_max_abs = abs;
      if (abs < m_min_abs || isNaN) m_min_abs = abs;
    }

    /// <summary>Update the running statistics by adding a sequence of observed sample (in-place).</summary>
    public void PushRange(System.Collections.Generic.IEnumerable<double> values)
    {
      foreach (double value in values ?? throw new System.ArgumentNullException(nameof(values)))
      {
        Push(value);
      }
    }

    /// <summary>Create a new running statistics over the combined samples of two existing running statistics.</summary>
    public static RunningStatistics Combine(RunningStatistics a, RunningStatistics b)
    {
      if (a.m_count == 0) return b;
      else if (b.m_count == 0) return a;

      var count = a.m_count + b.m_count;

      var d1 = b.m_m1 - a.m_m1;
      var d2 = d1 * d1;
      var d3 = d2 * d1;
      var d4 = d2 * d2;

      var m1 = (a.m_count * a.m_m1 + b.m_count * b.m_m1) / count;
      var m2 = a.m_m2 + b.m_m2 + d2 * a.m_count * b.m_count / count;
      var m3 = a.m_m3 + b.m_m3 + d3 * a.m_count * b.m_count * (a.m_count - b.m_count) / (count * count) + 3 * d1 * (a.m_count * b.m_m2 - b.m_count * a.m_m2) / count;
      var m4 = a.m_m4 + b.m_m4 + d4 * a.m_count * b.m_count * (a.m_count * a.m_count - a.m_count * b.m_count + b.m_count * b.m_count) / (count * count * count) + 6 * d2 * (a.m_count * a.m_count * b.m_m2 + b.m_count * b.m_count * a.m_m2) / (count * count) + 4 * d1 * (a.m_count * b.m_m3 - b.m_count * a.m_m3) / count;

      return new RunningStatistics
      {
        m_count = count,
        m_hm = a.m_hm + b.m_hm,
        m_m1 = m1,
        m_m2 = m2,
        m_m3 = m3,
        m_m4 = m4,
        m_max = System.Math.Max(a.m_max, b.m_max),
        m_max_abs = System.Math.Max(a.m_max_abs, b.m_max_abs),
        m_min = System.Math.Min(a.m_min, b.m_min),
        m_min_abs = System.Math.Min(a.m_min_abs, b.m_min_abs),
        m_product = a.m_product * b.m_product,
        m_sum = a.m_sum + b.m_sum
      };
    }
    //public static RunningStatistics operator +(RunningStatistics a, RunningStatistics b) => Combine(a, b);

    // Operators
    public static bool operator ==(in RunningStatistics a, in RunningStatistics b)
      => a.Equals(b);
    public static bool operator !=(in RunningStatistics a, in RunningStatistics b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals(in RunningStatistics other)
      => m_count == other.m_count && m_hm == other.m_hm && m_m1 == other.m_m1 && m_m2 == other.m_m2 && m_m3 == other.m_m3 && m_m4 == other.m_m4 && m_max == other.m_max && m_max_abs == other.m_max_abs && m_min == other.m_min && m_min_abs == other.m_min_abs && m_product == other.m_product && m_sum == other.m_sum;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Empty;
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is VersionEx ? Equals((VersionEx)obj) : false;
    public override int GetHashCode()
      => Flux.HashCode.CombineCore(m_count, m_hm, m_m1, m_m2, m_m3, m_m4);
    public override string? ToString()
      => $"{this.GetType().Name}(count: {m_count}, sum: {m_sum}, m1: {m_m1}, m2: {m_m2}, m3: {m_m3}, m4: {m_m4}, min/max:[{m_min}, {m_max}][|{m_min_abs}|, |{m_max_abs}|])";
  }
}
