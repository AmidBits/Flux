namespace Flux
{
  public interface IMetricOneQuantifiable<TType>
  {
    string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format, bool useFullName, bool preferUnicode);
  }
}
