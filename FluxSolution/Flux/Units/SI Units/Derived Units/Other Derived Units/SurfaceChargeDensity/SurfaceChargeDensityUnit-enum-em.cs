namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.SurfaceChargeDensityUnit unit)
      => unit switch
      {
        Units.SurfaceChargeDensityUnit.CoulombPerSquareMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.SurfaceChargeDensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.SurfaceChargeDensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.SurfaceChargeDensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.SurfaceChargeDensityUnit.CoulombPerSquareMeter => "C/m²",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.SurfaceChargeDensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
