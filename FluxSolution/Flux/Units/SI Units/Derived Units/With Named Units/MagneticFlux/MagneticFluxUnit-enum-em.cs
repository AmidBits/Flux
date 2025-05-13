namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.MagneticFluxUnit unit)
      => unit switch
      {
        Units.MagneticFluxUnit.Weber => 1,

        Units.MagneticFluxUnit.Maxwell => 1e8,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.MagneticFluxUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.MagneticFluxUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.MagneticFluxUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.MagneticFluxUnit.Weber => preferUnicode ? "\u33DD" : "Wb",

        Units.MagneticFluxUnit.Maxwell => "Mx",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.MagneticFluxUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
