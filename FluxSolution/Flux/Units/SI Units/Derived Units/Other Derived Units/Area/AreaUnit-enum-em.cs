namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.AreaUnit unit)
      => unit switch
      {
        Units.AreaUnit.SquareMeter => 1,

        Units.AreaUnit.Hectare => 10000,
        Units.AreaUnit.Acre => 4046.85642,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AreaUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AreaUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AreaUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AreaUnit.SquareMeter => preferUnicode ? "\u33A1" : "m²",

        Units.AreaUnit.Acre => "ac",
        Units.AreaUnit.Hectare => preferUnicode ? "\u33CA" : "ha",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.AreaUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
