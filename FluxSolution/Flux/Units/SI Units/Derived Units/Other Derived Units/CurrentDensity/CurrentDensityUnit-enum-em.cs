namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.CurrentDensityUnit unit)
      => unit switch
      {
        Units.CurrentDensityUnit.AmperePerSquareMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.CurrentDensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.CurrentDensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.CurrentDensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.CurrentDensityUnit.AmperePerSquareMeter => "A/m²",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.CurrentDensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
