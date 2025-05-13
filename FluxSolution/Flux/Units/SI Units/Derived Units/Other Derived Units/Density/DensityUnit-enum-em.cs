namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.DensityUnit unit)
      => unit switch
      {
        Units.DensityUnit.KilogramPerCubicMeter => 1,

        Units.DensityUnit.GramPerCubicMeter => 1000,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.DensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.DensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.DensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.DensityUnit.KilogramPerCubicMeter => "kg/m³",

        Units.DensityUnit.GramPerCubicMeter => "g/m³",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.DensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
