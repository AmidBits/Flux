namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.DynamicViscosityUnit unit)
      => unit switch
      {
        Units.DynamicViscosityUnit.PascalSecond => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.DynamicViscosityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.DynamicViscosityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.DynamicViscosityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.DynamicViscosityUnit.PascalSecond => preferUnicode ? "Pa\u22C5s" : "Pa·s",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.DynamicViscosityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
