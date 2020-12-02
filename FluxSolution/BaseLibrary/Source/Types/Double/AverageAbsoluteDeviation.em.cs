using System.Linq;

namespace Flux
{
  public static partial class DoubleEm
  {
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static double AverageAbsoluteDeviation(this System.Collections.Generic.IList<double> source, double mean, double center)
    {
      return source.Sum(v => System.Math.Abs(v - center)) / mean;
    }
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static double AverageAbsoluteDeviationFromMean(this System.Collections.Generic.IEnumerable<double> source)
    {
      var mean = Mean(source, out System.Collections.Generic.List<double> values);

      return AverageAbsoluteDeviation(values, mean, mean);
    }
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static double AverageAbsoluteDeviationFromMedian(this System.Collections.Generic.IEnumerable<double> source)
    {
      var mean = Mean(source, out System.Collections.Generic.List<double> values);

      return AverageAbsoluteDeviation(values, mean, Median(values));
    }
    /// <summary>The average absolute deviation, or mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Average_absolute_deviation"/>
    public static double AverageAbsoluteDeviationFromMode(this System.Collections.Generic.IEnumerable<double> source)
    {
      var mean = Mean(source, out System.Collections.Generic.List<double> values);

      return AverageAbsoluteDeviation(values, mean, Mode(values));
    }
  }
}
