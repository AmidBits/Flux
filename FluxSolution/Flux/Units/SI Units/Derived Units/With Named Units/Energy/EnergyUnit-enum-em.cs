namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.EnergyUnit unit)
      => unit switch
      {
        Units.EnergyUnit.Joule => 1,

        Units.EnergyUnit.ElectronVolt => Units.ElectricCharge.ElementaryCharge,
        Units.EnergyUnit.Calorie => 4.184,
        Units.EnergyUnit.WattHour => 3.6e3,
        Units.EnergyUnit.KilowattHour => 3.6e6,
        Units.EnergyUnit.BritishThermalUnits => 1055.05585262,

        _ => double.NaN
      };

    public static bool TryGetUnitFactor(this Units.EnergyUnit unit, out double factor)
      => !double.IsNaN(factor = unit.GetUnitFactor());

    public static string GetUnitName(this Units.EnergyUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.EnergyUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.EnergyUnit.Joule => "J",

        Units.EnergyUnit.BritishThermalUnits => "BTU",
        Units.EnergyUnit.ElectronVolt => "eV",
        Units.EnergyUnit.Calorie => preferUnicode ? "\u3388" : "cal",
        Units.EnergyUnit.WattHour => "W\u22C5h",
        Units.EnergyUnit.KilowattHour => "kW\u22C5h",

        _ => string.Empty
      };

    public static bool TryGetUnitSymbol(this Units.EnergyUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
