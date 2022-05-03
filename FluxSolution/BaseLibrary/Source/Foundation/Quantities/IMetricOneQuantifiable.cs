namespace Flux
{
  public interface IMetricOneQuantifiable
  {
    /// <summary>Create a string representing the quantity with the specified multiplicative prefix for the default unit.</summary>
    /// <param name="prefix">The multiplicative metric prefix of the default unit to represent.</param>
    /// <param name="format">Optionally format the value.</param>
    /// <param name="useFullName">Optionally use the full name, rather than symbols or shorter (e.g. acronym) variants.</param>
    /// <param name="preferUnicode">Optionally prefer Unicode characters where available.</param>
    /// <returns></returns>
    string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format, bool useFullName, bool preferUnicode);
  }
}
