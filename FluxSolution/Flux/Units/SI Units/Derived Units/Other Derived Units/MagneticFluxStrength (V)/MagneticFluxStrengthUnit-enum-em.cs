namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.MagneticFluxStrengthUnit unit)
      => unit switch
      {
        Units.MagneticFluxStrengthUnit.AmperePerMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.MagneticFluxStrengthUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.MagneticFluxStrengthUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.MagneticFluxStrengthUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.MagneticFluxStrengthUnit.AmperePerMeter => "A/m",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.MagneticFluxStrengthUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
