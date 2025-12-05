namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.EnergyDensityUnit unit)
      => unit switch
      {
        Units.EnergyDensityUnit.JoulePerCubicMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.EnergyDensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.EnergyDensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.EnergyDensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.EnergyDensityUnit.JoulePerCubicMeter => "J/m³",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.EnergyDensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
