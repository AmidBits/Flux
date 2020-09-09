//using System.Linq;

//namespace Flux
//{
//  public static partial class XtendDouble
//  {
//    /// <summary>Computes the percentile rank of the specified value within the source distribution.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Percentile_rank"/>
//    public static double PercentileRank(this System.Collections.Generic.IEnumerable<double> source, double value, out int countTotal, out int countLessOrEqual)
//    {
//      countTotal = 0;
//      countLessOrEqual = 0;

//      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
//      {
//        countTotal++;

//        if (item <= value) countLessOrEqual++;
//      }

//      return 100.0 * (double)countLessOrEqual / (double)countTotal;
//    }

//    /// <summary>Computes the percentile rank of the specified value within the source distribution.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Percentile_rank"/>
//    public static double PercentileRank(this System.Collections.Generic.IEnumerable<double> source, double value)
//      => source.PercentileRank(value, out var _, out var _);

//    /// <summary>Computes the percentile rank of the source within the values distribution.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Percentile_rank"/>
//    public static double PercentileRank(this double source, params double[] values)
//      => values.PercentileRank(source);
//  }
//}
