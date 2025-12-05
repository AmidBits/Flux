namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.TemperatureUnit unit)
      => unit switch
      {
        Units.TemperatureUnit.Kelvin => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.TemperatureUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.TemperatureUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.TemperatureUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.TemperatureUnit.Kelvin => preferUnicode ? "\u212A" : "K",

        Units.TemperatureUnit.Celsius => preferUnicode ? "\u2103" : "\u00B0C",
        Units.TemperatureUnit.Fahrenheit => preferUnicode ? "\u2109" : "\u00B0F",
        Units.TemperatureUnit.Rankine => $"\u00B0Ra",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.TemperatureUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
