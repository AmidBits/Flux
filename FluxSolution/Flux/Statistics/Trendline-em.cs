namespace Flux
{
  public static partial class StatisticsExtensions
  {
    public static Statistics.TrendLine<double> Trend(this System.Collections.Generic.IEnumerable<double> source)
      => new(source, d => d);
  }
}
