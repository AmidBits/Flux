namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.IrradianceUnit unit)
      => unit switch
      {
        Units.IrradianceUnit.WattPerSquareMeter => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.IrradianceUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.IrradianceUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.IrradianceUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.IrradianceUnit.WattPerSquareMeter => "W/m²",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.IrradianceUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
