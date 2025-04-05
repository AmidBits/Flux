
namespace Flux.Statistics
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

    private RunningStatistics(long count, double max, double min, double m1, double m2, double m3, double m4, double product, double reciprocalSum, double sum)
    {
      m_count = count;
      m_max = max;
      m_min = min;
      m_m1 = m1;
      m_m2 = m2;
      m_m3 = m3;
      m_m4 = m4;
      m_product = product;
      m_reciprocalSum = reciprocalSum;
      m_sum = sum;
    }

    public RunningStatistics() : this(
      0, // count
      double.NaN, // max
      double.NaN, // min
      double.NaN, // m1
      double.NaN, // m2
      double.NaN, // m3
      double.NaN, // m4
      double.NaN, // product
      double.NaN, // reciprocalSum
      double.NaN // sum
    )
    { }

    public RunningStatistics(System.Collections.Generic.IEnumerable<double> values) => AddRange(values);

    /// <summary>
    /// <para>The number of samples.</para>
    /// </summary>    
    public long Count => m_count;

    /// <summary>
    /// <para>Returns the maximum value of all samples.</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>
    public double Maximum => m_max;

    /// <summary>
    /// <para>Returns the minimum value of all samples.</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>
    public double Minimum => m_min;

    /// <summary>
    /// <para>The product of all samples.</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>
    public double Product => m_product;

    /// <summary>
    /// <para>The sum of all samples.</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>
    public double Sum => m_sum;

    /// <summary>
    /// <para>Evaluates the sample mean, an estimate of the population mean.</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>
    public double Mean => m_m1;

    /// <summary>
    /// <para>Evaluates the geometric mean of the enumerable, in a single pass without memoization.</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>    
    public double GeometricMean => double.Pow(m_product, 1d / m_count);

    /// <summary>
    /// <para>Evaluates the harmonic mean of the enumerable, in a single pass without memoization.</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>
    public double HarmonicMean => m_count / m_reciprocalSum;

    /// <summary>
    /// <para>Estimates the unbiased population variance from the provided samples. On a dataset of size N will use an N-1 normalizer (Bessel's correction).</para>
    /// <para>Returns NaN if insufficient (&lt;2) data.</para>
    /// </summary>
    public double Variance => m_count < 2 ? double.NaN : m_m2 / (m_count - 1);

    /// <summary>
    /// <para>Evaluates the variance from the provided full population. On a dataset of size N will use an N normalizer and would thus be biased if applied to a subset.</para>
    /// <para>Returns NaN if insufficient (&lt;2) data.</para>
    /// </summary>
    public double PopulationVariance => m_count < 2 ? double.NaN : m_m2 / m_count;

    /// <summary>
    /// <para>Estimates the unbiased population standard deviation from the provided samples. On a dataset of size N will use an N-1 normalizer (Bessel's correction).</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>
    public double StandardDeviation => double.Sqrt(Variance);

    /// <summary>
    /// <para>Evaluates the standard deviation from the provided full population. On a dataset of size N will use an N normalizer and would thus be biased if applied to a subset.</para>
    /// <para>Returns NaN if insufficient data.</para>
    /// </summary>
    public double PopulationStandardDeviation => double.Sqrt(PopulationVariance);

    /// <summary>
    /// <para>Estimates the unbiased population skewness from the provided samples. Uses a normalizer (Bessel's correction; type 2).</para>
    /// <para>Returns NaN if insufficient (&lt;3) data.</para>
    /// </summary>
    public double Skewness => m_count < 3 ? double.NaN : (m_count * m_m3 * double.Sqrt(m_m2 / (m_count - 1)) / (m_m2 * m_m2 * (m_count - 2))) * (m_count - 1);

    /// <summary>
    /// <para>Evaluates the population skewness from the full population. Does not use a normalizer and would thus be biased if applied to a subset (type 1).</para>
    /// <para>Returns NaN if insufficient (&lt;2) data.</para>
    /// </summary>
    public double PopulationSkewness => m_count < 2 ? double.NaN : double.Sqrt(m_count) * m_m3 / double.Pow(m_m2, 1.5);

    /// <summary>
    /// <para>Estimates the unbiased population kurtosis from the provided samples. Uses a normalizer (Bessel's correction; type 2).</para>
    /// <para>Returns NaN if insufficient (&lt;4) data.</para>
    /// </summary>
    public double Kurtosis => m_count < 4 ? double.NaN : ((double)m_count * m_count - 1) / ((m_count - 2) * (m_count - 3)) * (m_count * m_m4 / (m_m2 * m_m2) - 3d + 6d / (m_count + 1));

    /// <summary>
    /// <para>Evaluates the population kurtosis from the full population. Does not use a normalizer and would thus be biased if applied to a subset (type 1).</para>
    /// <para>Returns NaN if insufficient (&lt;3) data.</para>
    /// </summary>
    public double PopulationKurtosis => m_count < 3 ? double.NaN : m_count * m_m4 / (m_m2 * m_m2) - 3d;

    /// <summary>
    /// <para>Update the running statistics by adding an observed sample.</para>
    /// </summary>
    /// <param name="value"></param>
    public void Add(double value)
    {
      if (m_count == 0)
      {
        m_max = double.NegativeInfinity;
        m_min = double.PositiveInfinity;

        m_m1 = 0;
        m_m2 = 0;
        m_m3 = 0;
        m_m4 = 0;

        m_product = 1;
        m_reciprocalSum = 0;
        m_sum = 0;
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

      if (value > m_max || double.IsNaN(value))
        m_max = value;
      if (value < m_min || double.IsNaN(value))
        m_min = value;

      m_product *= value;
      m_reciprocalSum += 1 / value;
      m_sum += value;
    }

    /// <summary>
    /// <para>Update running statistics by adding a sequence of observed sample.</para>
    /// </summary>
    public void AddRange(System.Collections.Generic.IEnumerable<double> values)
    {
      foreach (double value in values ?? throw new System.ArgumentNullException(nameof(values)))
        Add(value);
    }

    #region Static members

    /// <summary>Create a new running statistics over the combined samples of two existing running statistics.</summary>
    public static RunningStatistics Combine(RunningStatistics a, RunningStatistics b)
    {
      if (a.m_count == 0)
        return b;
      if (b.m_count == 0)
        return a;

      var d1 = b.m_m1 - a.m_m1;
      var d2 = d1 * d1;
      var d3 = d2 * d1;
      var d4 = d2 * d2;

      var count = a.m_count + b.m_count;

      var max = double.Max(a.m_max, b.m_max);
      var min = double.Min(a.m_min, b.m_min);

      var m1 = (a.m_count * a.m_m1 + b.m_count * b.m_m1) / count;
      var m2 = a.m_m2 + b.m_m2 + d2 * a.m_count * b.m_count / count;
      var m3 = a.m_m3 + b.m_m3 + d3 * a.m_count * b.m_count * (a.m_count - b.m_count) / (count * count) + 3 * d1 * (a.m_count * b.m_m2 - b.m_count * a.m_m2) / count;
      var m4 = a.m_m4 + b.m_m4 + d4 * a.m_count * b.m_count * (a.m_count * a.m_count - a.m_count * b.m_count + b.m_count * b.m_count) / (count * count * count) + 6 * d2 * (a.m_count * a.m_count * b.m_m2 + b.m_count * b.m_count * a.m_m2) / (count * count) + 4 * d1 * (a.m_count * b.m_m3 - b.m_count * a.m_m3) / count;

      var product = a.m_product * b.m_product;
      var reciprocalSum = a.m_reciprocalSum + b.m_reciprocalSum;
      var sum = a.m_sum + b.m_sum;

      return new RunningStatistics(count, max, min, m1, m2, m3, m4, product, reciprocalSum, sum);
    }

    #endregion Static members

    #region Overloaded operators

    public static RunningStatistics operator +(RunningStatistics a, RunningStatistics b) => Combine(a, b);

    #endregion Overloaded operators

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N3";

      var sm = new SpanMaker<char>();

      sm = sm.Append(GetType().Name);

      sm = sm.Append(" { ");

      sm = sm.Append($" Count = {Count}");
      sm = sm.Append($", Min/Max = [{Minimum.ToDecimalFormattedNumberString(3)}, {Maximum.ToDecimalFormattedNumberString(3)}]");
      sm = sm.Append($", Mean = {Mean.ToDecimalFormattedNumberString(3)}");
      sm = sm.Append($", Product = {Product.ToDecimalFormattedNumberString(3)}");
      sm = sm.Append($", Sum = {Sum.ToDecimalFormattedNumberString(3)}");

      sm = sm.Append($", M* = [{m_m1.ToString(format, formatProvider)}, {m_m2.ToString(format, formatProvider)}, {m_m3.ToString(format, formatProvider)}, {m_m4.ToString(format, formatProvider)}]");

      sm = sm.Append(" }");

      return sm.ToString();
    }

    #endregion // Implemented interfaces

    public override string? ToString() => ToString(null, null);
  }
}
