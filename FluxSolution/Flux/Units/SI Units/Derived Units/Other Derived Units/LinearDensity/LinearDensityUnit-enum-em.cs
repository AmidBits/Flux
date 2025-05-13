namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.LinearDensityUnit unit)
      => unit switch
      {
        Units.LinearDensityUnit.KilogramPerMeter => 1,

        Units.LinearDensityUnit.GramPerMeter => 1000,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.LinearDensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.LinearDensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.LinearDensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.LinearDensityUnit.KilogramPerMeter => "kg/m",

        Units.LinearDensityUnit.GramPerMeter => "g/m",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.LinearDensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
