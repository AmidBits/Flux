namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.AngularFrequencyUnit unit)
      => unit switch
      {
        Units.AngularFrequencyUnit.RadianPerSecond => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AngularFrequencyUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AngularFrequencyUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AngularFrequencyUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AngularFrequencyUnit.RadianPerSecond => preferUnicode ? "\u33AE" : "rad/s",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.AngularFrequencyUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
