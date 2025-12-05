namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.IlluminanceUnit unit)
      => unit switch
      {
        Units.IlluminanceUnit.Lux => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.IlluminanceUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.IlluminanceUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.IlluminanceUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.IlluminanceUnit.Lux => preferUnicode ? "\u33D3" : "lx",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.IlluminanceUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
