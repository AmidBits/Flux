using Flux;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.Units
{
  [TestClass]
  public class SiBase
  {
    [TestMethod]
    public void ElectricCurrent()
    {
      var u = new Flux.Quantity.ElectricCurrent(1);

      Assert.AreEqual(1000, u.ToUnitValue(Flux.Quantity.ElectricCurrentUnit.Milliampere));
      Assert.AreEqual(1, u.ToUnitValue(Flux.Quantity.ElectricCurrentUnit.Ampere));
    }

    [TestMethod]
    public void Enplethy()
    {
      var u = new Flux.Quantity.Enplethy(1);

      Assert.AreEqual(1, u.ToUnitValue(Flux.Quantity.EnplethyUnit.Mole));
    }

    [TestMethod]
    public void Length()
    {
      var u = new Flux.Quantity.Length(1);

      Assert.AreEqual(1000, u.ToUnitValue(Flux.Quantity.LengthUnit.Millimeter));
      Assert.AreEqual(100, u.ToUnitValue(Flux.Quantity.LengthUnit.Centimeter));
      Assert.AreEqual(39.37007874015748, u.ToUnitValue(Flux.Quantity.LengthUnit.Inch));
      Assert.AreEqual(10, u.ToUnitValue(Flux.Quantity.LengthUnit.Decimeter));
      Assert.AreEqual(3.280839895013123, u.ToUnitValue(Flux.Quantity.LengthUnit.Foot));
      Assert.AreEqual(1.0936132983377078, u.ToUnitValue(Flux.Quantity.LengthUnit.Yard));
      Assert.AreEqual(1, u.ToUnitValue(Flux.Quantity.LengthUnit.Meter));
      Assert.AreEqual(0.0005399568034557236, u.ToUnitValue(Flux.Quantity.LengthUnit.NauticalMile));
      Assert.AreEqual(0.0006213711922373339, u.ToUnitValue(Flux.Quantity.LengthUnit.Mile));
      Assert.AreEqual(0.001, u.ToUnitValue(Flux.Quantity.LengthUnit.Kilometer));
    }

    [TestMethod]
    public void LuminousIntensity()
    {
      var u = new Flux.Quantity.LuminousIntensity(1);

      Assert.AreEqual(1, u.ToUnitValue(Flux.Quantity.LuminousIntensityUnit.Candela));
    }

    [TestMethod]
    public void Mass()
    {
      var u = new Flux.Quantity.Mass(1);

      Assert.AreEqual(1000000, u.ToUnitValue(Flux.Quantity.MassUnit.Milligram));
      Assert.AreEqual(1000, u.ToUnitValue(Flux.Quantity.MassUnit.Gram));
      Assert.AreEqual(35.27396195, u.ToUnitValue(Flux.Quantity.MassUnit.Ounce));
      Assert.AreEqual(1, u.ToUnitValue(Flux.Quantity.MassUnit.Kilogram));
      Assert.AreEqual(2.2046226218487757, u.ToUnitValue(Flux.Quantity.MassUnit.Pound));
    }

    [TestMethod]
    public void Temperature()
    {
      var u = new Flux.Quantity.Temperature(1);

      Assert.AreEqual(-272.15, u.ToUnitValue(Flux.Quantity.TemperatureUnit.Celsius));
      Assert.AreEqual(-457.87, u.ToUnitValue(Flux.Quantity.TemperatureUnit.Fahrenheit));
      Assert.AreEqual(1, u.ToUnitValue(Flux.Quantity.TemperatureUnit.Kelvin));
      Assert.AreEqual(1.8, u.ToUnitValue(Flux.Quantity.TemperatureUnit.Rankine));
    }

    [TestMethod]
    public void Time()
    {
      var u = new Flux.Quantity.Time(1);

      Assert.AreEqual(1000000000, u.ToUnitValue(Flux.Quantity.TimeUnit.Nanosecond));
      Assert.AreEqual(1000000, u.ToUnitValue(Flux.Quantity.TimeUnit.Microsecond));
      Assert.AreEqual(1000, u.ToUnitValue(Flux.Quantity.TimeUnit.Millisecond));
      Assert.AreEqual(1, u.ToUnitValue(Flux.Quantity.TimeUnit.Second));
      Assert.AreEqual(0.016666666666666666, u.ToUnitValue(Flux.Quantity.TimeUnit.Minute));
      Assert.AreEqual(0.0002777777777777778, u.ToUnitValue(Flux.Quantity.TimeUnit.Hour));
      Assert.AreEqual(1.1574074074074073E-05, u.ToUnitValue(Flux.Quantity.TimeUnit.Day));
      Assert.AreEqual(1.6534391534391535E-06, u.ToUnitValue(Flux.Quantity.TimeUnit.Week));
      Assert.AreEqual(8.267195767195768E-07, u.ToUnitValue(Flux.Quantity.TimeUnit.Fortnight));
    }
  }
}
