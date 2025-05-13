namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.RelativeHumidityUnit unit)
      => unit switch
      {
        Units.RelativeHumidityUnit.Percent => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.RelativeHumidityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.RelativeHumidityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.RelativeHumidityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.RelativeHumidityUnit.Percent => "%",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.RelativeHumidityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
