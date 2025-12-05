namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.FlowUnit unit)
      => unit switch
      {
        Units.FlowUnit.CubicMeterPerSecond => 1,

        Units.FlowUnit.USGallonPerMinute => 15850.323074494,
        Units.FlowUnit.Sverdrup => 1000000,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.FlowUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.FlowUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.FlowUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.FlowUnit.CubicMeterPerSecond => "m³/s",

        Units.FlowUnit.USGallonPerMinute => "gpm (US)",
        Units.FlowUnit.Sverdrup => "Sv",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.FlowUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
