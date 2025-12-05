namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.LuminousFluxUnit unit)
      => unit switch
      {
        Units.LuminousFluxUnit.Lumen => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.LuminousFluxUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.LuminousFluxUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.LuminousFluxUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.LuminousFluxUnit.Lumen => preferUnicode ? "\u33D0" : "lm",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.LuminousFluxUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
