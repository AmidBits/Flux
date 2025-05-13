namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.TurbidityUnit unit)
      => unit switch
      {
        Units.TurbidityUnit.NephelometricTurbidityUnits => 1,

        Units.TurbidityUnit.FormazinAttenuationUnit => 1,
        Units.TurbidityUnit.FormazinNephelometricUnit => 1,
        Units.TurbidityUnit.FormazinTurbidityUnit => 1,
        Units.TurbidityUnit.AmericanSocietyOfBrewingChemists => 1 / 17.5,
        Units.TurbidityUnit.EuropeanBreweryConvention => 1 / 0.25,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.TurbidityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.TurbidityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.TurbidityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.TurbidityUnit.NephelometricTurbidityUnits => "NTU",

        Units.TurbidityUnit.FormazinAttenuationUnit => "FAU",
        Units.TurbidityUnit.FormazinNephelometricUnit => "FNU",
        Units.TurbidityUnit.FormazinTurbidityUnit => "FTU",
        Units.TurbidityUnit.AmericanSocietyOfBrewingChemists => "ASBC",
        Units.TurbidityUnit.EuropeanBreweryConvention => "EBC",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.TurbidityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
