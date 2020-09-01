namespace Flux
{
  public static partial class XtendDouble
  {
    /// <summary>Calculate the variance (how far a set of numbers is spread out) of a sequence, using the Welford's Online algorithm. Variance is the mean squared deviation. To compute the standard deviation, simply do the System.Math.Sqrt(variance).</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Variance"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Algorithms_for_calculating_variance"/>
    public static (int count, double mean, double sampleVariance, double populationVariance, double standardDeviation) Variance(this System.Collections.Generic.IEnumerable<double> source)
    {
      var count = 0;
      double mean = 0.0, M2 = 0.0;

      foreach (var value in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        var delta1 = value - mean;
        mean += delta1 / ++count;

        var delta2 = value - mean;
        M2 += delta1 * delta2;
      }

      return (count >= 2) ? (count, mean, M2 / (count - 1), M2 / count, System.Math.Sqrt(M2 / count)) : throw new System.ArgumentException(@"The sequence must contain at least two elements.");
    }
  }
}
