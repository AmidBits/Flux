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


    public static (TSelf fromMean, TSelf fromMedian, TSelf fromMode) AverageAbsoluteDeviationFrom<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var list = source.ToList();

      Mean(list, out var _, out var _, out TSelf mean);
      var median = Median(list, out var _);
      var mode = Mode(list).First().Key;

      var fromMean = TSelf.Zero;
      var fromMedian = TSelf.Zero;
      var fromMode = TSelf.Zero;

      foreach (var value in list)
      {
        fromMean += TSelf.Abs(value - mean);
        fromMedian += TSelf.Abs(value - median);
        fromMode += TSelf.Abs(value - mode);
      }

      fromMean /= mean;
      fromMedian /= mean;
      fromMode /= mean;

      return (fromMean, fromMedian, fromMode);
    }
  }
}
