namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.PowerRatioUnit unit)
      => unit switch
      {
        Units.PowerRatioUnit.DecibelWatt => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.PowerRatioUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.PowerRatioUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.PowerRatioUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.PowerRatioUnit.DecibelWatt => "dBW",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.PowerRatioUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
