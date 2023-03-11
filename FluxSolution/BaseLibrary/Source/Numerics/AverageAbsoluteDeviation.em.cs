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

    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static TSelf AverageAbsoluteDeviationFromMean<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      Mean(source, out System.Collections.Generic.List<TSelf> values, out var _, out TSelf mean);

      return AverageAbsoluteDeviation(values, mean, mean);
    }
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static TSelf AverageAbsoluteDeviationFromMedian<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      Mean(source, out System.Collections.Generic.List<TSelf> values, out var _, out TSelf mean);

      return AverageAbsoluteDeviation(values, mean, Median(values));
    }
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static TSelf AverageAbsoluteDeviationFromMode<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      Mean(source, out System.Collections.Generic.List<TSelf> values, out var _, out TSelf mean);

      return AverageAbsoluteDeviation(values, mean, Mode(values).First().Key);
    }
  }
}
