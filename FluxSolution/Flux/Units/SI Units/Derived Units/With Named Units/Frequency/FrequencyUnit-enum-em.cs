namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.FrequencyUnit unit)
      => unit switch
      {
        Units.FrequencyUnit.Hertz => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.FrequencyUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.FrequencyUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.FrequencyUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.FrequencyUnit.Hertz => preferUnicode ? "\u3390" : "Hz",

        Units.FrequencyUnit.BeatsPerMinute => "BPM",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.FrequencyUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
