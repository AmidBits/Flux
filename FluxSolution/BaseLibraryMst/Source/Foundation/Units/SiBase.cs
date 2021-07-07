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
      var u = new Flux.Units.ElectricCurrent(1);

      Assert.AreEqual(1, u.Ampere);
    }

    [TestMethod]
    public void Enplethy()
    {
      var u = new Flux.Units.Enplethy(1);

      Assert.AreEqual(1, u.Mole);
    }

    [TestMethod]
    public void Length()
    {
      var u = new Flux.Units.Length(1);

      Assert.AreEqual(3.280839895013123, u.Foot);
      Assert.AreEqual(0.001, u.Kilometer);
      Assert.AreEqual(1, u.Meter);
      Assert.AreEqual(0.0006213711922373339, u.Mile);
      Assert.AreEqual(1000, u.Millimeter);
      Assert.AreEqual(0.0005399568034557236, u.NauticalMile);
    }

    [TestMethod]
    public void LuminousIntensity()
    {
      var u = new Flux.Units.LuminousIntensity(1);

      Assert.AreEqual(1, u.Candela);
    }

    [TestMethod]
    public void Mass()
    {
      var u = new Flux.Units.Mass(1);

      Assert.AreEqual(1000, u.Gram);
      Assert.AreEqual(1, u.Kilogram);
      Assert.AreEqual(2.2046226218487757, u.Pound);
    }

    [TestMethod]
    public void Temperature()
    {
      var u = new Flux.Units.Temperature(1);

      Assert.AreEqual(-272.15, u.Celsius);
      Assert.AreEqual(-457.87, u.Fahrenheit);
      Assert.AreEqual(1, u.Kelvin);
      Assert.AreEqual(1.8, u.Rankine);
    }

    [TestMethod]
    public void Time()
    {
      var u = new Flux.Units.Time(1);

      Assert.AreEqual(1, u.Second);
    }
  }
}
