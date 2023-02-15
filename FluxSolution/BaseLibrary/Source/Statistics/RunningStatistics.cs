namespace Flux
{
  public record class RunningStatistics
    : System.IEquatable<RunningStatistics>, System.IFormattable
  {
    public static readonly RunningStatistics Empty = new();

    private long m_count;

    private double m_max;
    private double m_min;

    private double m_m1;
    private double m_m2;
    private double m_m3;
    private double m_m4;

    private double m_product; // The product of all values. Initialized to 1 when the first value is added.
    private double m_reciprocalSum; // The sum of reciprocal of all values.
    private double m_sum; // The sum of all values.

    /// <summary>The number of samples.</summary>    
    public long Count => m_count;

    /// <summary>Returns the maximum value of all samples, or NaN if no data/any entry is NaN.</summary>
    public double Maximum => m_count > 0 ? m_max : double.NaN;
    /// <summary>Returns the minimum value of all samples. Returns NaN if data is empty or if any entry is NaN.</summary>
    public double Minimum => m_count > 0 ? m_min : double.NaN;

    /// <summary>The product of all samples. Returns NaN if no data or any sample is NaN.</summary>
    public double Product => m_count > 0 ? m_product : double.NaN;

    /// <summary>The sum of all samples. Returns NaN if no data or any sample is NaN.</summary>
    public double Sum => m_count > 0 ? m_sum : double.NaN;

    /// <summary>Evaluates the sample mean, an estimate of the population mean. Returns NaN if data is empty or if any entry is NaN.</summary>
    public double Mean => m_count > 0 ? m_m1 : double.NaN;
    /// <summary>Evaluates the geometric mean of the enumerable, in a single pass without memoization. Returns NaN if data is empty or any entry is NaN.</summary>    
    public double GeometricMean => m_count > 0 ? System.Math.Pow(m_product, 1.0 / m_count) : double.NaN;
    /// <summary>Evaluates the harmonic mean of the enumerable, in a single pass without memoization. Returns NaN if data is empty or any entry is NaN.</summary>
    public double HarmonicMean => m_count > 0 ? m_count / m_reciprocalSum : double.NaN;

    /// <summary>Estimates the unbiased population variance from the provided samples. On a dataset of size N will use an N-1 normalizer (Bessel's correction). Returns NaN if data has less than two entries or if any entry is NaN.</summary>
    public double Variance => m_count < 2 ? double.NaN : m_m2 / (m_count - 1);
    /// <summary>Evaluates the variance from the provided full population. On a dataset of size N will use an N normalizer and would thus be biased if applied to a subset. Returns NaN if data is empty or if any entry is NaN.</summary>
    public double PopulationVariance => m_count < 2 ? double.NaN : m_m2 / m_count;

    /// <summary> Estimates the unbiased population standard deviation from the provided samples. On a dataset of size N will use an N-1 normalizer (Bessel's correction). Returns NaN if data has less than two entries or if any entry is NaN.</summary>
    public double StandardDeviation => m_count < 2 ? double.NaN : System.Math.Sqrt(m_m2 / (m_count - 1));
    /// <summary>Evaluates the standard deviation from the provided full population. On a dataset of size N will use an N normalizer and would thus be biased if applied to a subset. Returns NaN if data is empty or if any entry is NaN.</summary>
    public double PopulationStandardDeviation => m_count < 2 ? double.NaN : System.Math.Sqrt(m_m2 / m_count);

    /// <summary>Estimates the unbiased population skewness from the provided samples. Uses a normalizer (Bessel's correction; type 2). Returns NaN if data has less than three entries or if any entry is NaN.</summary>
    public double Skewness => m_count < 3 ? double.NaN : (m_count * m_m3 * System.Math.Sqrt(m_m2 / (m_count - 1)) / (m_m2 * m_m2 * (m_count - 2))) * (m_count - 1);
    /// <summary>Evaluates the population skewness from the full population. Does not use a normalizer and would thus be biased if applied to a subset (type 1). Returns NaN if data has less than two entries or if any entry is NaN. </summary>
    public double PopulationSkewness => m_count < 2 ? double.NaN : System.Math.Sqrt(m_count) * m_m3 / System.Math.Pow(m_m2, 1.5);

    /// <summary>Estimates the unbiased population kurtosis from the provided samples. Uses a normalizer (Bessel's correction; type 2). Returns NaN if data has less than four entries or if any entry is NaN.</summary>
    public double Kurtosis => m_count < 4 ? double.NaN : ((double)m_count * m_count - 1) / ((m_count - 2) * (m_count - 3)) * (m_count * m_m4 / (m_m2 * m_m2) - 3 + 6.0 / (m_count + 1));
    /// <summary>Evaluates the population kurtosis from the full population. Does not use a normalizer and would thus be biased if applied to a subset (type 1). Returns NaN if data has less than three entries or if any entry is NaN.</summary>
    public double PopulationKurtosis => m_count < 3 ? double.NaN : m_count * m_m4 / (m_m2 * m_m2) - 3.0;

    /// <summary>Update the running statistics by adding an observed sample (in-place).</summary>
    public void Add(double value)
    {
      if (m_count == 0)
      {
        m_max = double.NegativeInfinity;
        m_min = double.PositiveInfinity;

        m_product = 1;
      }

      m_count++;

      var d = value - m_m1;
      var s = d / m_count;
      var s2 = s * s;
      var t = d * s * (m_count - 1);

      m_m1 += s;
      m_m4 += t * s2 * (m_count * m_count - 3 * m_count + 3) + 6 * s2 * m_m2 - 4 * s * m_m3;
      m_m3 += t * s * (m_count - 2) - 3 * s * m_m2;
      m_m2 += t;

      if (value > m_max || double.IsNaN(value)) m_max = value;
      if (value < m_min || double.IsNaN(value)) m_min = value;

      m_product *= value;
      m_reciprocalSum += 1.0 / value;
      m_sum += value;
    }
    /// <summary>Update the running statistics by adding observed samples (in-place).</summary>
    public void Add(params double[] values)
      => AddRange(values);

    /// <summary>Update the running statistics by adding a sequence of observed sample (in-place).</summary>
    public void AddRange(System.Collections.Generic.IEnumerable<double> values)
    {
      foreach (double value in values ?? throw new System.ArgumentNullException(nameof(values)))
      {
        Add(value);
      }
    }

    #region Static members
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

        m_m1 = m1,
        m_m2 = m2,
        m_m3 = m3,
        m_m4 = m4,

        m_max = System.Math.Max(a.m_max, b.m_max),
        m_min = System.Math.Min(a.m_min, b.m_min),

        m_product = a.m_product * b.m_product,
        m_reciprocalSum = a.m_reciprocalSum + b.m_reciprocalSum,
        m_sum = a.m_sum + b.m_sum,
      };
    }


    public static RunningStatistics Create()
    {
      return new RunningStatistics()
      {
        m_count = 0,
        m_reciprocalSum = 0,
        m_m1 = 0,
        m_m2 = 0,
        m_m3 = 0,
        m_m4 = 0,
        m_max = double.NegativeInfinity,
        m_min = double.PositiveInfinity,
        m_sum = 0,
        m_product = 1
      };
    }

    public static RunningStatistics Create(System.Collections.Generic.IEnumerable<double> values)
    {
      var rs = Create();
      rs.AddRange(values);
      return rs;
    }
    #endregion Static members

    #region Overloaded operators
    public static RunningStatistics operator +(RunningStatistics a, RunningStatistics b)
      => Combine(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"{GetType().Name} {{ Count = {m_count}, M = [{m_m1}, {m_m2}, {m_m3}, {m_m4}], Min/Max = [{m_min}, {m_max}], Product = {m_product}, Sum = {m_sum} }}";
    #endregion Implemented interfaces

    #region Object overrides
    public override string? ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
