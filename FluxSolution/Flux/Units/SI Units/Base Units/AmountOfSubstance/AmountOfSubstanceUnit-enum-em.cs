namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.AmountOfSubstanceUnit unit)
      => unit switch
      {
        Units.AmountOfSubstanceUnit.Mole => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AmountOfSubstanceUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AmountOfSubstanceUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AmountOfSubstanceUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AmountOfSubstanceUnit.Mole => preferUnicode ? "\u33D6" : "mol",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.AmountOfSubstanceUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
