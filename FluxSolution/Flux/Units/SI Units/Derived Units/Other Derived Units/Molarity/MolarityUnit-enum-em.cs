namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.MolarityUnit unit)
      => unit switch
      {
        Units.MolarityUnit.MolesPerCubicMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.MolarityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.MolarityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.MolarityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.MolarityUnit.MolesPerCubicMeter => "mol/m³",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.MolarityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
