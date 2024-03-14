namespace Flux.Units
{
  #region Extension methods

  public static partial class Em
  {
    //  public static System.Collections.Generic.Dictionary<TUnit, string> ToStringOfAllUnits<TType, TUnit>(this IUnitValueQuantifiable<TType, TUnit> source, TextOptions options = default)
    //    where TType : struct, System.IEquatable<TType>
    //    where TUnit : notnull, System.Enum
    //  {
    //    var d = new System.Collections.Generic.Dictionary<TUnit, string>();

    //    foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
    //      d.Add(unit, source.ToUnitValueString(unit, options));

    //    return d;
    //  }
  }

  #endregion // Extension methods

  public interface IMetricMultiplicable<TValue>
    : IValueQuantifiable<TValue>
    where TValue : struct, System.Numerics.INumber<TValue>
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
    string ToMetricValueString(MetricPrefix prefix, string? format, System.IFormatProvider? formatProvider, UnicodeSpacing unitSpacing);
  }
}
