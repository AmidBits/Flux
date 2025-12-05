namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.PartsPerNotationUnit unit)
      => unit switch
      {
        Units.PartsPerNotationUnit.One => 1,

        Units.PartsPerNotationUnit.PartPerBillion => 1e9,
        Units.PartsPerNotationUnit.PartPerMillion => 1e6,
        Units.PartsPerNotationUnit.PartPerQuadrillion => 1e15,
        Units.PartsPerNotationUnit.PartPerTrillion => 1e12,
        Units.PartsPerNotationUnit.PerCentMille => 1e5,
        Units.PartsPerNotationUnit.PerMille => 1e3,
        Units.PartsPerNotationUnit.PerMyriad => 1e4,
        Units.PartsPerNotationUnit.Percent => 1e2,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.PartsPerNotationUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.PartsPerNotationUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.PartsPerNotationUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.PartsPerNotationUnit.One => "pp1",

        Units.PartsPerNotationUnit.PartPerBillion => "ppb",
        Units.PartsPerNotationUnit.PartPerMillion => preferUnicode ? "\u33D9" : "ppm",
        Units.PartsPerNotationUnit.PartPerQuadrillion => "ppq",
        Units.PartsPerNotationUnit.PartPerTrillion => "ppt",
        Units.PartsPerNotationUnit.PerCentMille => "pcm",
        Units.PartsPerNotationUnit.PerMille => "\u2030",
        Units.PartsPerNotationUnit.PerMyriad => "\u2031",
        Units.PartsPerNotationUnit.Percent => "\u0025",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.PartsPerNotationUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
