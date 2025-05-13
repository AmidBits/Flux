namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.PermeabilityUnit unit)
      => unit switch
      {
        Units.PermeabilityUnit.HenryPerMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.PermeabilityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.PermeabilityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.PermeabilityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.PermeabilityUnit.HenryPerMeter => "H/m",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.PermeabilityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
