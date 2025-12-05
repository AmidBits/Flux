namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.AccelerationUnit unit)
      => unit switch
      {
        Units.AccelerationUnit.MeterPerSecondSquared => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AccelerationUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AccelerationUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AccelerationUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AccelerationUnit.MeterPerSecondSquared => preferUnicode ? "\u33A8" : "m/s²",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.AccelerationUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
