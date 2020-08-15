namespace Flux
{
  public static partial class XtendDouble
  {
    /// <summary>The Cumulative Distribution Function (CDF) is the function that maps values to their percentile rank in a distribution.</summary>
    public static double CumulativeDistributionFunction(this System.Collections.Generic.IEnumerable<double> source, double value, out int countTotal, out int countLessOrEqual)
    {
      countTotal = 0;
      countLessOrEqual = 0;

      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        countTotal++;

        if (item <= value)
          countLessOrEqual++;
      }

      return (double)countLessOrEqual / (double)countTotal;
    }

    /// <summary>The Cumulative Distribution Function (CDF) is the function that maps values to their percentile rank in a distribution.</summary>
    public static double CumulativeDistributionFunction(this System.Collections.Generic.IEnumerable<double> source, double value)
      => source.CumulativeDistributionFunction(value, out var _, out var _);
  }
}
