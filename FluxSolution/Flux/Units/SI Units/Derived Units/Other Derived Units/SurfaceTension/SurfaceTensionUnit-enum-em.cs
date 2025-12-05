namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.SurfaceTensionUnit unit)
      => unit switch
      {
        Units.SurfaceTensionUnit.NewtonPerMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.SurfaceTensionUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.SurfaceTensionUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.SurfaceTensionUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.SurfaceTensionUnit.NewtonPerMeter => "N/m",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.SurfaceTensionUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
