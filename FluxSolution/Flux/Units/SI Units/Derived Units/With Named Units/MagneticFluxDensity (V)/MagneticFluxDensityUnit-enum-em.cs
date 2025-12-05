namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.MagneticFluxDensityUnit unit)
      => unit switch
      {
        Units.MagneticFluxDensityUnit.Tesla => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.MagneticFluxDensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.MagneticFluxDensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.MagneticFluxDensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.MagneticFluxDensityUnit.Tesla => "T",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.MagneticFluxDensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
