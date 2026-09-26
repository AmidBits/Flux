namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.TempoUnit unit)
      => unit switch
      {
        Units.TempoUnit.BeatsPerMinute => 1.0,

        Units.TempoUnit.BeatsPerSecond => 1.0 / 60.0,

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit))
      };

    public static bool TryGetUnitFactor(this Units.TempoUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.TempoUnit unit, bool preferPlural = false)
      => unit.ToString();

    public static string GetUnitSymbol(this Units.TempoUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.TempoUnit.BeatsPerMinute => "bpm",

        Units.TempoUnit.BeatsPerSecond => "bps",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.TempoUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
