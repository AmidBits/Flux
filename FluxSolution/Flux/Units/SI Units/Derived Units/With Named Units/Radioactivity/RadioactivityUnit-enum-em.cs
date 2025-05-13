namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.RadioactivityUnit unit)
      => unit switch
      {
        Units.RadioactivityUnit.Becquerel => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.RadioactivityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.RadioactivityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.RadioactivityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.RadioactivityUnit.Becquerel => preferUnicode ? "\u33C3" : "Bq",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.RadioactivityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
