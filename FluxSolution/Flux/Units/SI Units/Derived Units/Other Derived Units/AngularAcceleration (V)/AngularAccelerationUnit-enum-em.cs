namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.AngularAccelerationUnit unit)
      => unit switch
      {
        Units.AngularAccelerationUnit.RadianPerSecondSquared => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AngularAccelerationUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AngularAccelerationUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AngularAccelerationUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AngularAccelerationUnit.RadianPerSecondSquared => preferUnicode ? "\u33AF" : "rad/s²",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.AngularAccelerationUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
