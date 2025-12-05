namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.MassUnit unit)
      => unit switch
      {
        Units.MassUnit.Kilogram => 1,

        Units.MassUnit.Gram => 0.001,
        Units.MassUnit.Ounce => 0.028349523125,
        Units.MassUnit.Pound => 0.45359237,
        Units.MassUnit.Tonne => 1000,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.MassUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.MassUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.MassUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.MassUnit.Kilogram => preferUnicode ? "\u338F" : "kg",

        Units.MassUnit.Gram => "g",
        Units.MassUnit.Ounce => "oz",
        Units.MassUnit.Pound => "lb",
        Units.MassUnit.Tonne => "t",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.MassUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
