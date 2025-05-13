namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.LuminousIntensityUnit unit)
      => unit switch
      {
        Units.LuminousIntensityUnit.Candela => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.LuminousIntensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.LuminousIntensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.LuminousIntensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.LuminousIntensityUnit.Candela => preferUnicode ? "\u33C5" : "cd",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.LuminousIntensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
