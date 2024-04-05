using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class SiBase
  {
    [TestMethod]
    public void ElectricCurrent()
    {
      var u = new Flux.Quantities.ElectricCurrent(1);

      Assert.AreEqual(1000, u.GetUnitValue(Flux.Quantities.ElectricCurrentUnit.Milliampere));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Quantities.ElectricCurrentUnit.Ampere));
    }

    [TestMethod]
    public void Enplethy()
    {
      var u = new Flux.Quantities.AmountOfSubstance(1);

      Assert.AreEqual(1, u.GetUnitValue(Flux.Quantities.AmountOfSubstanceUnit.Mole));
    }

    [TestMethod]
    public void Length()
    {
      var u = new Flux.Quantities.Length(1);

      Assert.AreEqual(1000, u.GetUnitValue(Flux.Quantities.LengthUnit.Millimetre));
      Assert.AreEqual(100, u.GetUnitValue(Flux.Quantities.LengthUnit.Centimetre));
      Assert.AreEqual(39.37007874015748, u.GetUnitValue(Flux.Quantities.LengthUnit.Inch));
      Assert.AreEqual(10, u.GetUnitValue(Flux.Quantities.LengthUnit.Decimetre));
      Assert.AreEqual(3.280839895013123, u.GetUnitValue(Flux.Quantities.LengthUnit.Foot));
      Assert.AreEqual(1.0936132983377078, u.GetUnitValue(Flux.Quantities.LengthUnit.Yard));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Quantities.LengthUnit.Metre));
      Assert.AreEqual(0.0005399568034557236, u.GetUnitValue(Flux.Quantities.LengthUnit.NauticalMile));
      Assert.AreEqual(0.0006213711922373339, u.GetUnitValue(Flux.Quantities.LengthUnit.Mile));
      Assert.AreEqual(0.001, u.GetUnitValue(Flux.Quantities.LengthUnit.Kilometre));
    }

    [TestMethod]
    public void LuminousIntensity()
    {
      var u = new Flux.Quantities.LuminousIntensity(1);

      Assert.AreEqual(1, u.GetUnitValue(Flux.Quantities.LuminousIntensityUnit.Candela));
    }

    [TestMethod]
    public void Mass()
    {
      var u = new Flux.Quantities.Mass(1);

      //Assert.AreEqual(1000000, u.GetUnitValue(Flux.Units.MassUnit.Milligram));
      Assert.AreEqual(1000, u.GetUnitValue(Flux.Quantities.MassUnit.Gram));
      Assert.AreEqual(35.27396195, u.GetUnitValue(Flux.Quantities.MassUnit.Ounce));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Quantities.MassUnit.Kilogram));
      Assert.AreEqual(2.2046226218487757, u.GetUnitValue(Flux.Quantities.MassUnit.Pound));
    }

    [TestMethod]
    public void Temperature()
    {
      var u = new Flux.Quantities.ThermaldynamicTemperature(1);

      Assert.AreEqual(-272.15, u.GetUnitValue(Flux.Quantities.ThermaldynamicTemperatureUnit.Celsius));
      Assert.AreEqual(-457.87, u.GetUnitValue(Flux.Quantities.ThermaldynamicTemperatureUnit.Fahrenheit));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Quantities.ThermaldynamicTemperatureUnit.Kelvin));
      Assert.AreEqual(1.8, u.GetUnitValue(Flux.Quantities.ThermaldynamicTemperatureUnit.Rankine));
    }

    [TestMethod]
    public void Time()
    {
      var u = new Flux.Quantities.Time(1);

      Assert.AreEqual(1000000000, u.GetUnitValue(Flux.Quantities.TimeUnit.Nanosecond));
      Assert.AreEqual(1000000, u.GetUnitValue(Flux.Quantities.TimeUnit.Microsecond));
      Assert.AreEqual(1000, u.GetUnitValue(Flux.Quantities.TimeUnit.Millisecond));
      Assert.AreEqual(1, u.GetUnitValue(Flux.Quantities.TimeUnit.Second));
      Assert.AreEqual(0.016666666666666666, u.GetUnitValue(Flux.Quantities.TimeUnit.Minute));
      Assert.AreEqual(0.0002777777777777778, u.GetUnitValue(Flux.Quantities.TimeUnit.Hour));
      Assert.AreEqual(1.1574074074074073E-05, u.GetUnitValue(Flux.Quantities.TimeUnit.Day));
      Assert.AreEqual(1.6534391534391535E-06, u.GetUnitValue(Flux.Quantities.TimeUnit.Week));
      Assert.AreEqual(8.267195767195768E-07, u.GetUnitValue(Flux.Quantities.TimeUnit.Fortnight));
    }
  }
}
