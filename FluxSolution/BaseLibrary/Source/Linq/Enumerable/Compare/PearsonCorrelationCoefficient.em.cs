namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Pearson correlation coefficient (PCC) is a measure of linear correlation between two sets of data. It is the ratio between the covariance of two variables and the product of their standard deviations. Thus, it is essentially a normalized measurement of the covariance, such that the result always has a value between -1 and 1.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Pearson_correlation_coefficient"/>
    public static (TSelf correlation, TSelf covariance) PearsonCorrelationCoefficient<TValueX, TValueY, TSelf>(this System.Collections.Generic.IEnumerable<TValueX> setX, System.Func<TValueX, TSelf> valueSelectorX, System.Collections.Generic.IEnumerable<TValueY> setY, System.Func<TValueY, TSelf> valueSelectorY)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
    {
      var ex = setX.ThrowIfNullOrEmpty().GetEnumerator();
      var ey = setY.ThrowIfNullOrEmpty().GetEnumerator();

      var sumX = TSelf.Zero;
      var sumX2 = TSelf.Zero;
      var sumY = TSelf.Zero;
      var sumY2 = TSelf.Zero;
      var sumXY = TSelf.Zero;
      var count = TSelf.Zero;

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

      var countP2 = count * count;

      var stdX = TSelf.Sqrt(sumX2 / count - sumX * sumX / countP2);
      var stdY = TSelf.Sqrt(sumY2 / count - sumY * sumY / countP2);

      var covariance = (sumXY / count - sumX * sumY / countP2);

      return (covariance / stdX / stdY, covariance);
    }
  }
}
