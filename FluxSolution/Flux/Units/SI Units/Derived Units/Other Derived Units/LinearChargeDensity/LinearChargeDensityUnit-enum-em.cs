namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.LinearChargeDensityUnit unit)
      => unit switch
      {
        Units.LinearChargeDensityUnit.CoulombPerMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.LinearChargeDensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.LinearChargeDensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.LinearChargeDensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.LinearChargeDensityUnit.CoulombPerMeter => "C/m",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.LinearChargeDensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
