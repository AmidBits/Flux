using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static TSelf AverageAbsoluteDeviation<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source, TSelf mean, TSelf center)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => source.Select(v => TSelf.Abs(v - center)).Sum() / mean;

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
  }
}