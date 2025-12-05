namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.ElectricChargeUnit unit)
      => unit switch
      {
        Units.ElectricChargeUnit.Coulomb => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ElectricChargeUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ElectricChargeUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ElectricChargeUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ElectricChargeUnit.Coulomb => "C",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.ElectricChargeUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
