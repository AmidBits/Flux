using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class SiBase
  {
    [TestMethod]
    public void AmountOfSubstance()
    {
      var u = new Flux.Units.AmountOfSubstance(1);

      Assert.AreEqual(1, u.GetUnitValue(Flux.Units.AmountOfSubstanceUnit.Mole));
    }

    [TestMethod]
    public void ElectricCurrent()
    {
      var u = new Flux.Units.ElectricCurrent(1);

      Assert.AreEqual(1000, u.GetUnitValue(Flux.Units.ElectricCurrentUnit.Milliampere));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Units.ElectricCurrentUnit.Ampere));
    }

    [TestMethod]
    public void Length()
    {
      var u = new Flux.Units.Length(1);

      Assert.AreEqual(39.37007874015748, u.GetUnitValue(Flux.Units.LengthUnit.Inch));
      Assert.AreEqual(3.280839895013123, u.GetUnitValue(Flux.Units.LengthUnit.Foot));
      Assert.AreEqual(1.0936132983377078, u.GetUnitValue(Flux.Units.LengthUnit.Yard));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Units.LengthUnit.Meter));
      Assert.AreEqual(0.0005399568034557236, u.GetUnitValue(Flux.Units.LengthUnit.NauticalMile));
      Assert.AreEqual(0.0006213711922373339, u.GetUnitValue(Flux.Units.LengthUnit.Mile));
    }

    [TestMethod]
    public void LuminousIntensity()
    {
      var u = new Flux.Units.LuminousIntensity(1);

      Assert.AreEqual(1, u.GetUnitValue(Flux.Units.LuminousIntensityUnit.Candela));
    }

    [TestMethod]
    public void Mass()
    {
      var u = new Flux.Units.Mass(1);

      //Assert.AreEqual(1000000, u.GetUnitValue(Flux.Units.MassUnit.Milligram));
      Assert.AreEqual(1000, u.GetUnitValue(Flux.Units.MassUnit.Gram));
      Assert.AreEqual(35.27396194958041, u.GetUnitValue(Flux.Units.MassUnit.Ounce));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Units.MassUnit.Kilogram));
      Assert.AreEqual(2.2046226218487757, u.GetUnitValue(Flux.Units.MassUnit.Pound));
    }

    [TestMethod]
    public void Temperature()
    {
      var u = new Flux.Units.Temperature(1);

      Assert.AreEqual(-272.15, u.GetUnitValue(Flux.Units.TemperatureUnit.Celsius));
      Assert.AreEqual(-457.87, u.GetUnitValue(Flux.Units.TemperatureUnit.Fahrenheit));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Units.TemperatureUnit.Kelvin));
      Assert.AreEqual(1.8, u.GetUnitValue(Flux.Units.TemperatureUnit.Rankine));
    }

    [TestMethod]
    public void Time()
    {
      var u = new Flux.Units.Time(1);

      Assert.AreEqual(1000000000, u.GetSiUnitValue(Flux.Units.MetricPrefix.Nano));
      Assert.AreEqual(1000000, u.GetSiUnitValue(Flux.Units.MetricPrefix.Micro));
      Assert.AreEqual(1000, u.GetSiUnitValue(Flux.Units.MetricPrefix.Milli));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Units.TimeUnit.Second));
      Assert.AreEqual(0.016666666666666666, u.GetUnitValue(Flux.Units.TimeUnit.Minute));
      Assert.AreEqual(0.0002777777777777778, u.GetUnitValue(Flux.Units.TimeUnit.Hour));
      Assert.AreEqual(1.1574074074074073E-05, u.GetUnitValue(Flux.Units.TimeUnit.Day));
      Assert.AreEqual(1.6534391534391535E-06, u.GetUnitValue(Flux.Units.TimeUnit.Week));
    }
  }
}
