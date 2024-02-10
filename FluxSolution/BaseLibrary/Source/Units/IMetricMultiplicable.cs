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
    //abstract static TValue FromMetricValue(MetricMultiplicativePrefix prefix, TValue value);

    TValue ToMetricValue(MetricPrefix prefix);

    string ToMetricValueString(MetricPrefix prefix, string? format, System.IFormatProvider? formatProvider, UnitSpacing spacing);
  }
}
