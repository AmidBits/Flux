namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.ElectricalConductanceUnit unit)
      => unit switch
      {
        Units.ElectricalConductanceUnit.Siemens => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ElectricalConductanceUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ElectricalConductanceUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ElectricalConductanceUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ElectricalConductanceUnit.Siemens => preferUnicode ? "\u2127" : "S",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.ElectricalConductanceUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
