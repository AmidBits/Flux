namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.LuminousEfficacyUnit unit)
      => unit switch
      {
        Units.LuminousEfficacyUnit.LumenPerWatt => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.LuminousEfficacyUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.LuminousEfficacyUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.LuminousEfficacyUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.LuminousEfficacyUnit.LumenPerWatt => "lm/W",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.LuminousEfficacyUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
