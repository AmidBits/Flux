namespace Flux
{
  public interface IMetricPrefixFormattable
  {
    string GetMetricPrefixString(MetricMultiplicativePrefix prefix, string? format, bool useNameInstead, bool useUnicodeIfAvailable);
  }
}
