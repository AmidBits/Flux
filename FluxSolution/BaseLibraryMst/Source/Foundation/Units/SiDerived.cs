using Flux;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.Units
{
  [TestClass]
  public class SiDerived
  {
    [TestMethod]
    public void Acceleration()
    {
      var u = new Flux.Quantity.Acceleration(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Angle()
    {
      var u = new Flux.Quantity.Angle(1);

      Assert.AreEqual((0.5403023058681398, 0.8414709848078965), u.ToCartesian());
      Assert.AreEqual((0.8414709848078966, 0.5403023058681394), u.ToCartesianEx());
      Assert.AreEqual(57.29577951308232, u.Degree);
      Assert.AreEqual(63.66197723675813, u.ToUnitValue(Flux.Quantity.AngleUnit.Gradian));
      Assert.AreEqual(1, u.Radian);
      Assert.AreEqual(0.15915494309189535, u.ToUnitValue(Flux.Quantity.AngleUnit.Revolution));
    }

    [TestMethod]
    public void AngularAcceleration()
    {
      var u = new Flux.Quantity.AngularAcceleration(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AngularVelocity()
    {
      var u = new Flux.Quantity.AngularVelocity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Area()
    {
      var u = new Flux.Quantity.Area(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Capacitance()
    {
      var u = new Flux.Quantity.Capacitance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void CatalyticActivity()
    {
      var u = new Flux.Quantity.CatalyticActivity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Density()
    {
      var u = new Flux.Quantity.Density(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricalConductance()
    {
      var u = new Flux.Quantity.ElectricalConductance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricCharge()
    {
      var u = new Flux.Quantity.ElectricCharge(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricResistance()
    {
      var u = new Flux.Quantity.ElectricResistance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Energy()
    {
      var u = new Flux.Quantity.Energy(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Flow()
    {
      var u = new Flux.Quantity.Flow(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Force()
    {
      var u = new Flux.Quantity.Force(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Frequency()
    {
      var u = new Flux.Quantity.Frequency(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Illuminance()
    {
      var u = new Flux.Quantity.Illuminance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Inductance()
    {
      var u = new Flux.Quantity.Inductance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void LuminousFlux()
    {
      var u = new Flux.Quantity.LuminousFlux(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Power()
    {
      var u = new Flux.Quantity.Power(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Pressure()
    {
      var u = new Flux.Quantity.Pressure(1);

      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(0.0001450377377302092, u.ToUnitValue(Flux.Quantity.PressureUnit.PSI));
    }

    [TestMethod]
    public void Radioactivity()
    {
      var u = new Flux.Quantity.Radioactivity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Speed()
    {
      var u = new Flux.Quantity.Speed(1);

      Assert.AreEqual(3.2808398950131235, u.ToUnitValue(Flux.Quantity.SpeedUnit.FeetPerSecond));
      Assert.AreEqual(3.6, u.ToUnitValue(Flux.Quantity.SpeedUnit.KilometersPerHour));
      Assert.AreEqual(1.9438444924406046, u.ToUnitValue(Flux.Quantity.SpeedUnit.Knots));
      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(2.2369362920544025, u.ToUnitValue(Flux.Quantity.SpeedUnit.MilesPerHour));
    }

    [TestMethod]
    public void Torque()
    {
      var u = new Flux.Quantity.Torque(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Voltage()
    {
      var u = new Flux.Quantity.Voltage(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Volume()
    {
      var u = new Flux.Quantity.Volume(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
