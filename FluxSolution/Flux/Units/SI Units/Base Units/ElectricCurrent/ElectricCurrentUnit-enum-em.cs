namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.ElectricCurrentUnit unit)
      => unit switch
      {
        Units.ElectricCurrentUnit.Ampere => 1,

        Units.ElectricCurrentUnit.Milliampere => 0.001,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ElectricCurrentUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ElectricCurrentUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ElectricCurrentUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ElectricCurrentUnit.Ampere => "A",

        Units.ElectricCurrentUnit.Milliampere => preferUnicode ? "\u3383" : "mA",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.ElectricCurrentUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
