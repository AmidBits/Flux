namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.EquivalentDoseUnit unit)
      => unit switch
      {
        Units.EquivalentDoseUnit.Sievert => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.EquivalentDoseUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.EquivalentDoseUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.EquivalentDoseUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.EquivalentDoseUnit.Sievert => preferUnicode ? "\u33DC" : "Sv",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.EquivalentDoseUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
