namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.ElectricPotentialUnit unit)
      => unit switch
      {
        Units.ElectricPotentialUnit.Volt => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ElectricPotentialUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ElectricPotentialUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ElectricPotentialUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ElectricPotentialUnit.Volt => "V",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.ElectricPotentialUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
