namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.WavelengthUnit unit)
      => unit switch
      {
        Units.WavelengthUnit.MeterPerRadian => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.WavelengthUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.WavelengthUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.WavelengthUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.WavelengthUnit.MeterPerRadian => "m/rad",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.WavelengthUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
