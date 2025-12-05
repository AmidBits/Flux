namespace Flux
{
  public static partial class EUnitsExtensionsm
  {
    public static double GetUnitFactor(this Units.SpeedUnit unit)
      => unit switch
      {
        Units.SpeedUnit.MeterPerSecond => 1,

        Units.SpeedUnit.FootPerSecond => (381.0 / 1250.0),
        Units.SpeedUnit.KilometerPerHour => (5.0 / 18.0),
        Units.SpeedUnit.Knot => (1852.0 / 3600.0),
        Units.SpeedUnit.Mach => Units.Speed.SpeedOfSound,
        Units.SpeedUnit.MilePerHour => (1397.0 / 3125.0),

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.SpeedUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.SpeedUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.SpeedUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.SpeedUnit.MeterPerSecond => preferUnicode ? "\u33A7" : "m/s",

        Units.SpeedUnit.FootPerSecond => "ft/s",
        Units.SpeedUnit.KilometerPerHour => "km/h",
        Units.SpeedUnit.Knot => preferUnicode ? "\u33CF" : "knot",
        Units.SpeedUnit.Mach => "Mach",
        Units.SpeedUnit.MilePerHour => "mph",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.SpeedUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
