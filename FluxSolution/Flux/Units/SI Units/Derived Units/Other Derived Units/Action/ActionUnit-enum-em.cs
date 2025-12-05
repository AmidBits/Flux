namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.ActionUnit unit)
      => unit switch
      {
        Units.ActionUnit.JouleSecond => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ActionUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ActionUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ActionUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ActionUnit.JouleSecond => preferUnicode ? "J\u22C5s" : "J·s",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.ActionUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
