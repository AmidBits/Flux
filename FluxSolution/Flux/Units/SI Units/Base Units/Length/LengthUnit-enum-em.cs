namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.LengthUnit unit)
      => unit switch
      {
        Units.LengthUnit.Meter => 1,

        Units.LengthUnit.AstronomicalUnit => 149597870700,
        Units.LengthUnit.Fathom => 1.8288,
        Units.LengthUnit.Foot => 0.3048,
        Units.LengthUnit.Inch => 0.0254,
        Units.LengthUnit.LightYear => 9460730472580800,
        Units.LengthUnit.Mile => 1609.344,
        Units.LengthUnit.NauticalMile => 1852,
        Units.LengthUnit.Parsec => 30856775814913672,
        Units.LengthUnit.Twip => 0.0000176389, // 1 / 1.7639E-5,
        Units.LengthUnit.Yard => 0.9144,
        Units.LengthUnit.Ångström => 1E-10,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.LengthUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.LengthUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.LengthUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.LengthUnit.Meter => "m",

        Units.LengthUnit.AstronomicalUnit => preferUnicode ? "\u3373" : "au",
        Units.LengthUnit.Fathom => "ftm",
        Units.LengthUnit.Foot => "ft",
        Units.LengthUnit.Inch => preferUnicode ? "\u33CC" : "in",
        Units.LengthUnit.LightYear => "ly",
        Units.LengthUnit.Mile => "mi",
        Units.LengthUnit.NauticalMile => "nmi", // There is no single internationally agreed symbol. Others used are "N", "NM", "nmi" and "nm".
        Units.LengthUnit.Parsec => preferUnicode ? "\u3376" : "pc",
        Units.LengthUnit.Twip => "twip",
        Units.LengthUnit.Yard => "yd",
        Units.LengthUnit.Ångström => preferUnicode ? "\u212B" : "Å",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.LengthUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
