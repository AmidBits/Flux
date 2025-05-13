namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.AmplitudeRatioUnit unit)
      => unit switch
      {
        Units.AmplitudeRatioUnit.DecibelVolt => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AmplitudeRatioUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AmplitudeRatioUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AmplitudeRatioUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AmplitudeRatioUnit.DecibelVolt => "dBV",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.AmplitudeRatioUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
