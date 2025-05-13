namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.CatalyticActivityUnit unit)
      => unit switch
      {
        Units.CatalyticActivityUnit.Katal => 1,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.CatalyticActivityUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.CatalyticActivityUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.CatalyticActivityUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.CatalyticActivityUnit.Katal => preferUnicode ? "\u33CF" : "kat",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.CatalyticActivityUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
