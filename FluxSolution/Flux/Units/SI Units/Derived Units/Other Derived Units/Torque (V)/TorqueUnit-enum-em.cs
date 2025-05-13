namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.TorqueUnit unit)
      => unit switch
      {
        Units.TorqueUnit.NewtonMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.TorqueUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.TorqueUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.TorqueUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.TorqueUnit.NewtonMeter => preferUnicode ? "N\u22C5m" : "N·m",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.TorqueUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
