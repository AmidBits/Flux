namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.RadiationExposureUnit unit)
      => unit switch
      {
        Units.RadiationExposureUnit.CoulombPerKilogram => 1,

        Units.RadiationExposureUnit.Röntgen => 1.0 / 3876.0,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.RadiationExposureUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.RadiationExposureUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.RadiationExposureUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.RadiationExposureUnit.CoulombPerKilogram => "C/kg",

        Units.RadiationExposureUnit.Röntgen => "R",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.RadiationExposureUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
