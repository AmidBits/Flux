namespace Flux
{
  public static partial class Em
  {
    public static bool HasUnitSpacing(this Units.AngleUnit unit, bool preferUnicode)
      => !((unit == Units.AngleUnit.Degree && preferUnicode)
      || (unit == Units.AngleUnit.Gradian && preferUnicode)
      || (unit == Units.AngleUnit.Radian && preferUnicode)
      || unit == Units.AngleUnit.Arcminute
      || unit == Units.AngleUnit.Arcsecond);

    public static double GetUnitFactor(this Units.AngleUnit unit)
      => unit switch
      {
        Units.AngleUnit.Radian => 1,

        Units.AngleUnit.Arcminute => 1 / 3437.7467707849396,
        Units.AngleUnit.Arcsecond => 1 / 206264.80624709636,
        Units.AngleUnit.Milliradian => 0.001,
        Units.AngleUnit.Turn => double.Tau,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.AngleUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.AngleUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.AngleUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.AngleUnit.Radian => preferUnicode ? "\u33AD" : "rad",

        Units.AngleUnit.Arcminute => preferUnicode ? "\u2032" : "\u0027",
        Units.AngleUnit.Arcsecond => preferUnicode ? "\u2033" : "\u0022",
        Units.AngleUnit.Degree => preferUnicode ? "\u00B0" : "deg",
        Units.AngleUnit.Gradian => preferUnicode ? "\u1D4D" : "gon",
        Units.AngleUnit.NatoMil => $"mils (NATO)",
        Units.AngleUnit.Milliradian => "mrad",
        Units.AngleUnit.Turn => "turn",
        Units.AngleUnit.WarsawPactMil => $"mils (Warsaw Pact)",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.AngleUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
