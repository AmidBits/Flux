namespace Flux.Units
{
  #region Extension methods

  public static partial class Em
  {
    public static System.Collections.Generic.Dictionary<(MetricPrefix, TUnit), string> ToStringsOfMetricPrefixes<TValue, TUnit>(this IMetricMultiplicable<TValue, TUnit> source, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      where TValue : struct, System.Numerics.INumber<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<(MetricPrefix, TUnit), string>();

      foreach (MetricPrefix mp in System.Enum.GetValues<MetricPrefix>().OrderDescending())
        d.Add((mp, source.MetricUnprefixedUnit), source.ToMetricValueString(mp, format, formatProvider, preferUnicode, unitSpacing, useFullName));

      return d;
    }
  }

  #endregion // Extension methods

  public interface IMetricMultiplicable<TValue, TUnit>
    : IUnitValueQuantifiable<TValue, TUnit>
    where TValue : struct, System.Numerics.INumber<TValue>
    where TUnit : System.Enum
  {
    /// <summary>
    /// <para>Gets the metric value in the <see cref="MetricPrefix"/> multiplicable specified by <paramref name="prefix"/>.</para>
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    TValue GetMetricValue(MetricPrefix prefix);

    /// <summary>
    /// <para>Creates the metric value string in the <see cref="MetricPrefix"/> multiplicable specified by <paramref name="prefix"/>, <paramref name="format"/>, <paramref name="formatProvider"/> and <paramref name="unitSpacing"/>.</para>
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <param name="unitSpacing"></param>
    /// <returns></returns>
    string ToMetricValueString(MetricPrefix prefix, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unitSpacing, bool useFullName);
  }
}
