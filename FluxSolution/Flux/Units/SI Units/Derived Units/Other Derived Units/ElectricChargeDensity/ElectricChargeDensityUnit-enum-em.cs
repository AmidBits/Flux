namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.ElectricChargeDensityUnit unit)
      => unit switch
      {
        Units.ElectricChargeDensityUnit.CoulombPerCubicMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ElectricChargeDensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ElectricChargeDensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ElectricChargeDensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ElectricChargeDensityUnit.CoulombPerCubicMeter => "C/m³",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.ElectricChargeDensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
