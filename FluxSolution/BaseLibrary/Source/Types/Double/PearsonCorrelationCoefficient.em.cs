namespace Flux
{
  public static partial class SystemDoubleEm
  {
    /// <summary>Determines how close two variables are to having a linear relationship with each other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Pearson_correlation_coefficient"/>
    public static (double correlation, double covariance) PearsonCorrelationCoefficient(this System.Collections.Generic.IList<double> x, System.Collections.Generic.IList<double> y)
    {
      if (x is null) throw new System.ArgumentNullException(nameof(x));
      if (y is null) throw new System.ArgumentNullException(nameof(y));

      var sumX = 0.0;
      var sumX2 = 0.0;
      var sumY = 0.0;
      var sumY2 = 0.0;
      var sumXY = 0.0;

      int count = x.Count < y.Count ? x.Count : y.Count;

      for (int index = 0; index < count; index++)
      {
        var xi = x[index];
        var yi = y[index];

        sumX += xi;
        sumX2 += xi * xi;
        sumY += yi;
        sumY2 += yi * yi;
        sumXY += xi * yi;
      }

      var stdX = System.Math.Sqrt(sumX2 / count - sumX * sumX / count / count);
      var stdY = System.Math.Sqrt(sumY2 / count - sumY * sumY / count / count);

      var covariance = (sumXY / count - sumX * sumY / count / count);

      return (covariance / stdX / stdY, covariance);
    }
  }
}
