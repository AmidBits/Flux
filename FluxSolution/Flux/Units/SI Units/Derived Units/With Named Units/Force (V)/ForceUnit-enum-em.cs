namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.ForceUnit unit)
      => unit switch
      {
        Units.ForceUnit.Newton => 1,

        Units.ForceUnit.KilogramForce => 0.101971621,
        Units.ForceUnit.PoundForce => 0.224808943,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.ForceUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.ForceUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.ForceUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.ForceUnit.Newton => "N",

        Units.ForceUnit.KilogramForce => "kgf",
        Units.ForceUnit.PoundForce => "lbf",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.ForceUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
