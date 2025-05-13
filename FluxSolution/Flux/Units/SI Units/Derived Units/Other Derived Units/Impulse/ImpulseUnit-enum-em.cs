namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.ImpulseUnit unit)
      => unit switch
      {
        Units.ImpulseUnit.NewtonSecond => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ImpulseUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ImpulseUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ImpulseUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ImpulseUnit.NewtonSecond => preferUnicode ? "N\u22C5s" : "N·s",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.ImpulseUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
