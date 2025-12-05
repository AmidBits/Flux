namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static double GetUnitFactor(this Units.VolumeUnit unit)
      => unit switch
      {
        Units.VolumeUnit.CubicMeter => 1,

        Units.VolumeUnit.Microliter => 0.000000001,
        Units.VolumeUnit.Milliliter => 0.000001,
        Units.VolumeUnit.Centiliter => 0.00001,
        Units.VolumeUnit.Deciliter => 0.0001,
        Units.VolumeUnit.Liter => 0.001,
        Units.VolumeUnit.UKGallon => 1 / 219.96924829909,
        Units.VolumeUnit.UKQuart => 1 / 879.87699319635,
        Units.VolumeUnit.USDryGallon => 1 / 227.02074456538,
        Units.VolumeUnit.USLiquidGallon => 1 / 264.17205124156,
        Units.VolumeUnit.USDryQuart => 0.00110122095,
        Units.VolumeUnit.USLiquidQuart => 0.00094635295,
        Units.VolumeUnit.CubicFoot => 1 / (1953125000.0 / 55306341.0),
        Units.VolumeUnit.CubicYard => 1 / (1953125000.0 / 1493271207.0),
        Units.VolumeUnit.CubicMile => (8140980127813632.0 / 1953125.0),
        Units.VolumeUnit.CubicKilometer => 1e-09,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.VolumeUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.VolumeUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.VolumeUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.VolumeUnit.CubicMeter => preferUnicode ? "\u33A5" : "m³",

        Units.VolumeUnit.Microliter => preferUnicode ? "\u3395" : "µl",
        Units.VolumeUnit.Milliliter => preferUnicode ? "\u3396" : "ml",
        Units.VolumeUnit.Centiliter => "cl",
        Units.VolumeUnit.Deciliter => preferUnicode ? "\u3397" : "dl",
        Units.VolumeUnit.Liter => "l",
        Units.VolumeUnit.UKGallon => preferUnicode ? "\u33FF" : "gal (UK)",
        Units.VolumeUnit.UKQuart => "qt (UK)",
        Units.VolumeUnit.USDryGallon => preferUnicode ? "\u33FF" : "gal (US-dry)",
        Units.VolumeUnit.USLiquidGallon => preferUnicode ? "\u33FF" : "gal (US-liquid)",
        Units.VolumeUnit.USDryQuart => "qt (US-dry)",
        Units.VolumeUnit.USLiquidQuart => "qt (US-liquid)",
        Units.VolumeUnit.CubicFoot => "ft³",
        Units.VolumeUnit.CubicYard => "yd³",
        Units.VolumeUnit.CubicMile => "mi³",
        Units.VolumeUnit.CubicKilometer => preferUnicode ? "\u33A6" : "km³",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.VolumeUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
