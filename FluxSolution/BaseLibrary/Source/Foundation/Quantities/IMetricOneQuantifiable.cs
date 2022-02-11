namespace Flux
{
  public interface IMetricOneQuantifiable
  {
    string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format, bool useFullName, bool preferUnicode);
  }
}
