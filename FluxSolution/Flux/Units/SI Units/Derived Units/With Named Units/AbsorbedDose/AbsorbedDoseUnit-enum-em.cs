namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.AbsorbedDoseUnit unit)
      => unit switch
      {
        Units.AbsorbedDoseUnit.Gray => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AbsorbedDoseUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AbsorbedDoseUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AbsorbedDoseUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AbsorbedDoseUnit.Gray => "Gy",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.AbsorbedDoseUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
