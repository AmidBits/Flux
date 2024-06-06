namespace Flux
{
  public static partial class Fx
  {
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point, in the case of this function, all of mean, median and mode are computed simoultaneously.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static void AverageAbsoluteDeviationFrom<TSelf, TResult>(this System.Collections.Generic.IEnumerable<TSelf> source, out TResult madMean, out TResult madMedian, out TResult madMode)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      var list = source.ToList();

      list.Mean(out TResult mean, out var _, out var _);
      list.Median(out TResult median, out var _);
      var mode = TResult.CreateChecked(list.Mode().First().Key);

      madMean = TResult.Zero;
      madMedian = TResult.Zero;
      madMode = TResult.Zero;

      foreach (var value in list.Select(v => TResult.CreateChecked(v)))
      {
        madMean += TResult.Abs(value - mean);
        madMedian += TResult.Abs(value - median);
        madMode += TResult.Abs(value - mode);
      }

      madMean /= mean;
      madMedian /= mean;
      madMode /= mean;
    }
  }
}
