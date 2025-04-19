namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>The mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</para>
    /// <para>In this function, both M(AD)ean, M(AD)edian and the regular mode are computed.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Average_absolute_deviation"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static (double MeanAbsoluteDeviation, double MedianAbsoluteDeviation) MeanMedianAbsoluteDeviation<TNumber>(this System.Collections.Generic.IList<TNumber> values)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var mmo = new Statistics.OnlineMeanMedianMode<TNumber>(values);

      var madMean = 0d;
      var madMedian = 0d;

      foreach (var value in values.Select(v => double.CreateChecked(v)))
      {
        madMean += double.Abs(value - mmo.Mean);
        madMedian += double.Abs(value - mmo.Median);
      }

      madMean /= mmo.Count;
      madMedian /= mmo.Count;

      return (madMean, madMedian);
    }
  }
}
