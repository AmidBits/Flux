namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.CapacitanceUnit unit)
      => unit switch
      {
        Units.CapacitanceUnit.Farad => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.CapacitanceUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.CapacitanceUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.CapacitanceUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.CapacitanceUnit.Farad => "F",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.CapacitanceUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
