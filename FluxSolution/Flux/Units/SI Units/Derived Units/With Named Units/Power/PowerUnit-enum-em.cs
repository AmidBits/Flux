namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.PowerUnit unit)
      => unit switch
      {
        Units.PowerUnit.Watt => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.PowerUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.PowerUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.PowerUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.PowerUnit.Watt => "W",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.PowerUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
