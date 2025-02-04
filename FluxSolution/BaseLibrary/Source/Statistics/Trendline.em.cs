namespace Flux
{
  public static partial class Fx
  {
    public static Statistics.TrendLine<double> Trend(this System.Collections.Generic.IEnumerable<double> source)
      => new(source, d => d);
  }

  namespace Statistics
  {
    /// <summary>Computes slope and trending data on a sequence.</summary>
    public sealed record class TrendLine<T>
    {
      // http://dynamicnotions.blogspot.com/2009/05/linear-regression-in-c.html

      private readonly double m_avgX;
      private readonly double m_avgY;

      private readonly int m_count;

      private readonly double m_slope;
      private readonly double m_intercept;

      public TrendLine(System.Collections.Generic.IEnumerable<T> series, System.Func<T, double> valueSelector)
      {
        var list = series.Select(t => valueSelector(t)).ToList();

        foreach (var (value, index) in list.Select((v, i) => (value: v, index: i)))
        {
          m_avgX += index;
          m_avgY += value;
        }

        m_count = series.Count();

        m_avgX /= m_count;
        m_avgY /= m_count;

        var v1 = 0d;
        var v2 = 0d;

        foreach (var (value, index) in list.Select((v, i) => (value: v, index: i)))
        {
          v1 += (index - m_avgX) * (value - m_avgY);
          v2 += System.Math.Pow(index - m_avgX, 2);
        }

        m_slope = v1 / v2;
        m_intercept = m_avgY - m_slope * m_avgX;
      }

      public double AvgX => m_avgX;

      public double AvgY => m_avgY;

      public int Count => m_count;

      public double Slope => m_slope;

      public double Intercept => m_intercept;
    }
  }
}
