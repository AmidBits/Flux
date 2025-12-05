namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.HeatCapacityUnit unit)
      => unit switch
      {
        Units.HeatCapacityUnit.JoulePerKelvin => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.HeatCapacityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.HeatCapacityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.HeatCapacityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.HeatCapacityUnit.JoulePerKelvin => "J/K",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.HeatCapacityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
