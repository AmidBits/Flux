namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.TimeUnit unit)
      => unit switch
      {
        Units.TimeUnit.Second => 1,

        Units.TimeUnit.Tick => 0.0000001,
        Units.TimeUnit.Minute => 60,
        Units.TimeUnit.Hour => 3600,
        Units.TimeUnit.Day => 86400,
        Units.TimeUnit.Week => 604800,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.TimeUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.TimeUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.TimeUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.TimeUnit.Second => "s",

        Units.TimeUnit.Tick => "ticks",
        Units.TimeUnit.Minute => "min",
        Units.TimeUnit.Hour => "h",
        Units.TimeUnit.Day => "d",
        Units.TimeUnit.Week => "wk",
        Units.TimeUnit.BeatPerMinute => "bpm",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.TimeUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
