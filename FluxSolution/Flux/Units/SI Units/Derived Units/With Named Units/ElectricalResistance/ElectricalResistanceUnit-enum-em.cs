namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.ElectricalResistanceUnit unit)
      => unit switch
      {
        Units.ElectricalResistanceUnit.Ohm => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ElectricalResistanceUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ElectricalResistanceUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ElectricalResistanceUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ElectricalResistanceUnit.Ohm => preferUnicode ? "\u2126" : "ohm",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.ElectricalResistanceUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
