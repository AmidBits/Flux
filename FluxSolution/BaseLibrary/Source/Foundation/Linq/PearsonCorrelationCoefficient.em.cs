namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Determines how close two variables are to having a linear relationship with each other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Pearson_correlation_coefficient"/>
    public static (double correlation, double covariance) PearsonCorrelationCoefficient<TValueX, TValueY>(this System.Collections.Generic.IEnumerable<TValueX> setX, System.Func<TValueX, double> valueSelectorX, System.Collections.Generic.IEnumerable<TValueY> setY, System.Func<TValueY, double> valueSelectorY)
    {
      var sumX = 0.0;
      var sumX2 = 0.0;
      var sumY = 0.0;
      var sumY2 = 0.0;
      var sumXY = 0.0;

      var ex = setX.GetEnumerator();
      var ey = setY.GetEnumerator();

      if (!ex.MoveNext()) throw new System.ArgumentException(@"Sequence is empty.", nameof(setX));
      if (!ey.MoveNext()) throw new System.ArgumentException(@"Sequence is empty.", nameof(setY));

      var count = 0;

      do
      {
        var vx = valueSelectorX(ex.Current);
        var vy = valueSelectorY(ey.Current);

        sumX += vx;
        sumX2 += vx * vx;

        sumY += vy;
        sumY2 += vy * vy;

        sumXY += vx * vy;

        count++;
      }
      while (ex.MoveNext() && ey.MoveNext());

      var stdX = System.Math.Sqrt(sumX2 / count - sumX * sumX / count / count);
      var stdY = System.Math.Sqrt(sumY2 / count - sumY * sumY / count / count);

      var covariance = (sumXY / count - sumX * sumY / count / count);

      return (covariance / stdX / stdY, covariance);
    }
  }
}
