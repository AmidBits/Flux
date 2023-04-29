using System.Linq;

namespace Flux
{
  public static partial class NumericsExtensionMethods
  {
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static double AverageAbsoluteDeviation<TSelf>(this System.Collections.Generic.IEnumerable<double> source, double mean, double center)
      => source.Select(v => System.Math.Abs(v - center)).Sum() / mean;

#if NET7_0_OR_GREATER

    /// <summary>The average absolute deviation of a data set is the average of the absolute deviations from a central point, in the case of this function: mean, median and mode.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static void AverageAbsoluteDeviationFrom<TSelf, TResult>(this System.Collections.Generic.IEnumerable<TSelf> source, out TResult madMean, out TResult madMedian, out TResult madMode)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      var list = source.ToList();

      list.Mean(out TResult mean, out var _, out var _);
      list.Median(out TResult median, out var _);
      list.Mode(out var rawMode, out var _); // Mode can be applied on any type of object (not just numerics) and therefor no conversion is performed as in mean and median.
      var mode = TResult.CreateChecked(rawMode); // We perform our own conversion here.

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

#else

    /// <summary>The average absolute deviation of a data set is the average of the absolute deviations from a central point, in the case of this function: mean, median and mode.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static void AverageAbsoluteDeviationFrom<TSelf, TResult>(this System.Collections.Generic.IEnumerable<double> source, out double madMean, out double madMedian, out double madMode)
    {
      var list = source.ToList();

      list.Mean(out double mean, out var _, out var _);
      list.Median(out double median, out var _);
      list.Mode(out var rawMode, out var _); // Mode can be applied on any type of object (not just numerics) and therefor no conversion is performed as in mean and median.
      var mode = rawMode; // We perform our own conversion here.

      madMean = 0;
      madMedian = 0;
      madMode = 0;

      foreach (var value in list)
      {
        madMean += System.Math.Abs(value - mean);
        madMedian += System.Math.Abs(value - median);
        madMode += System.Math.Abs(value - mode);
      }

      madMean /= mean;
      madMedian /= mean;
      madMode /= mean;
    }

#endif
  }
}
