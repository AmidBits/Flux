namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.AbsoluteHumidityUnit unit)
      => unit switch
      {
        Units.AbsoluteHumidityUnit.GramPerCubicMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AbsoluteHumidityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AbsoluteHumidityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AbsoluteHumidityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AbsoluteHumidityUnit.GramPerCubicMeter => "g/m³",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.AbsoluteHumidityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
