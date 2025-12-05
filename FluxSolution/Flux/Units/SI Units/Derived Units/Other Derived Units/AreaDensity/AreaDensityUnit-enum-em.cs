namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.AreaDensityUnit unit)
      => unit switch
      {
        Units.AreaDensityUnit.KilogramPerSquareMeter => 1,

        Units.AreaDensityUnit.GramPerSquareMeter => 1000,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AreaDensityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AreaDensityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AreaDensityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AreaDensityUnit.KilogramPerSquareMeter => "kg/m²",

        Units.AreaDensityUnit.GramPerSquareMeter => "g/m²",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.AreaDensityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
