namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.PressureUnit unit)
      => unit switch
      {
        Units.PressureUnit.Pascal => 1,

        Units.PressureUnit.Millibar => 100,
        Units.PressureUnit.Bar => 100000,
        Units.PressureUnit.Psi => 1 / (1290320000.0 / 8896443230521.0),

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.PressureUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.PressureUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.PressureUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.PressureUnit.Pascal => preferUnicode ? "\u33A9" : "Pa",

        Units.PressureUnit.Millibar => "mbar",
        Units.PressureUnit.Bar => preferUnicode ? "\u3374" : "bar",
        //Quantities.PressureUnit.HectoPascal => preferUnicode ? "\u3371" : "hPa",
        //Quantities.PressureUnit.KiloPascal => preferUnicode ? "\u33AA" : "kPa",
        Units.PressureUnit.Psi => "psi",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.PressureUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
