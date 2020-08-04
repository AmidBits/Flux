using System.Linq;

namespace Flux
{
  public static partial class XtensionsDouble
  {
    public static TrendLine<double> Trend(this System.Collections.Generic.IEnumerable<double> source)
    {
      return new TrendLine<double>(source.Select(v => System.Convert.ToDouble(source)), d => d);
    }
  }

  /// <summary>Computes slope and trending data on a sequence.</summary>
  public class TrendLine<T>
  {
    // http://dynamicnotions.blogspot.com/2009/05/linear-regression-in-c.html

    public double xAvg = 0, yAvg = 0;

    public int count;

    public double v1 = 0, v2 = 0;

    public double Slope = 0, Intercept = 0;

    public TrendLine(System.Collections.Generic.IEnumerable<T> series, System.Func<T, double> valueSelector)
    {
      var list = series.Select(t => valueSelector(t)).ToList();

      foreach (var item in list.Select((v, i) => (value: v, index: i)))
      {
        xAvg += item.index;
        yAvg += item.value;
      }

      count = series.Count();

      xAvg = xAvg / count;
      yAvg = yAvg / count;

      foreach (var item in list.Select((v, i) => (value: v, index: i)))
      {
        v1 += (item.index - xAvg) * (item.value - yAvg);
        v2 += System.Math.Pow(item.index - xAvg, 2);
      }

      Slope = v1 / v2;
      Intercept = yAvg - Slope * xAvg;
    }
  }
}
