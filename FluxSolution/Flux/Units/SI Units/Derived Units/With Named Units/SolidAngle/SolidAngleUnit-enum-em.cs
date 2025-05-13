namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.SolidAngleUnit unit)
      => unit switch
      {
        Units.SolidAngleUnit.Steradian => 1,

        Units.SolidAngleUnit.Spat => double.Tau + double.Tau,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.SolidAngleUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.SolidAngleUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.SolidAngleUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.SolidAngleUnit.Steradian => preferUnicode ? "\u33DB" : "sr",

        Units.SolidAngleUnit.Spat => "sp",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.SolidAngleUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
