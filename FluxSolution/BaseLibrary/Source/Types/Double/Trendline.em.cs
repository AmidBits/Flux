using System.Linq;

namespace Flux
{
  public static partial class SystemDoubleEm
  {
    public static TrendLine<double> Trend(this System.Collections.Generic.IEnumerable<double> source)
    {
      return new TrendLine<double>(source, d => d);
    }
  }

  /// <summary>Computes slope and trending data on a sequence.</summary>
  public class TrendLine<T>
  {
    // http://dynamicnotions.blogspot.com/2009/05/linear-regression-in-c.html

    public double AvgX { get; }
    public double AvgY { get; }

    public int Count { get; }

    private readonly double v1, v2;

    public double Slope { get; }
    public double Intercept { get; }

    public TrendLine(System.Collections.Generic.IEnumerable<T> series, System.Func<T, double> valueSelector)
    {
      var list = series.Select(t => valueSelector(t)).ToList();

      foreach (var (value, index) in list.Select((v, i) => (value: v, index: i)))
      {
        AvgX += index;
        AvgY += value;
      }

      Count = series.Count();

      AvgX /= Count;
      AvgY /= Count;

      foreach (var (value, index) in list.Select((v, i) => (value: v, index: i)))
      {
        v1 += (index - AvgX) * (value - AvgY);
        v2 += System.Math.Pow(index - AvgX, 2);
      }

      Slope = v1 / v2;
      Intercept = AvgY - Slope * AvgX;
    }
  }
}
