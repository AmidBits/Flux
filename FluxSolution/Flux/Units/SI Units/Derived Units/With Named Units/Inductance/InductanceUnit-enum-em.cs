namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.InductanceUnit unit)
      => unit switch
      {
        Units.InductanceUnit.Henry => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.InductanceUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.InductanceUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.InductanceUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.InductanceUnit.Henry => "H",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.InductanceUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
