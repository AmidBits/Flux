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
      var u = new Flux.Units.Acceleration(1);

      Assert.AreEqual(1, u.MeterPerSecondSquare);
    }

    [TestMethod]
    public void Angle()
    {
      var u = new Flux.Units.Angle(1);

      Assert.AreEqual((0.5403023058681398, 0.8414709848078965), u.ToCartesian());
      Assert.AreEqual((0.8414709848078966, 0.5403023058681394), u.ToCartesianEx());
      Assert.AreEqual(57.29577951308232, u.Degree);
      Assert.AreEqual(63.66197723675813, u.ToUnitValue(Flux.Units.AngleUnit.Gradian));
      Assert.AreEqual(1, u.Radian);
      Assert.AreEqual(0.15915494309189535, u.ToUnitValue(Flux.Units.AngleUnit.Revolution));
    }

    [TestMethod]
    public void AngularAcceleration()
    {
      var u = new Flux.Units.AngularAcceleration(1);

      Assert.AreEqual(1, u.RadianPerSecondSquare);
    }

    [TestMethod]
    public void AngularVelocity()
    {
      var u = new Flux.Units.AngularVelocity(1);

      Assert.AreEqual(1, u.RadianPerSecond);
    }

    [TestMethod]
    public void Area()
    {
      var u = new Flux.Units.Area(1);

      Assert.AreEqual(1, u.SquareMeter);
    }

    [TestMethod]
    public void Capacitance()
    {
      var u = new Flux.Units.Capacitance(1);

      Assert.AreEqual(1, u.Farad);
    }

    [TestMethod]
    public void CatalyticActivity()
    {
      var u = new Flux.Units.CatalyticActivity(1);

      Assert.AreEqual(1, u.Katal);
    }

    [TestMethod]
    public void Density()
    {
      var u = new Flux.Units.Density(1);

      Assert.AreEqual(1, u.KilogramPerCubicMeter);
    }

    [TestMethod]
    public void ElectricalConductance()
    {
      var u = new Flux.Units.ElectricalConductance(1);

      Assert.AreEqual(1, u.Siemens);
    }

    [TestMethod]
    public void ElectricCharge()
    {
      var u = new Flux.Units.ElectricCharge(1);

      Assert.AreEqual(1, u.Coulomb);
    }

    [TestMethod]
    public void ElectricResistance()
    {
      var u = new Flux.Units.ElectricResistance(1);

      Assert.AreEqual(1, u.Ohm);
    }

    [TestMethod]
    public void Energy()
    {
      var u = new Flux.Units.Energy(1);

      Assert.AreEqual(1, u.Joule);
    }

    [TestMethod]
    public void Flow()
    {
      var u = new Flux.Units.Flow(1);

      Assert.AreEqual(1, u.CubicMeterPerSecond);
    }

    [TestMethod]
    public void Force()
    {
      var u = new Flux.Units.Force(1);

      Assert.AreEqual(1, u.Newton);
    }

    [TestMethod]
    public void Frequency()
    {
      var u = new Flux.Units.Frequency(1);

      Assert.AreEqual(1, u.Hertz);
    }

    [TestMethod]
    public void Illuminance()
    {
      var u = new Flux.Units.Illuminance(1);

      Assert.AreEqual(0.0929, u.Lumens);
      Assert.AreEqual(1, u.Lux);
    }

    [TestMethod]
    public void Inductance()
    {
      var u = new Flux.Units.Inductance(1);

      Assert.AreEqual(1, u.Henry);
    }

    [TestMethod]
    public void LuminousFlux()
    {
      var u = new Flux.Units.LuminousFlux(1);

      Assert.AreEqual(1, u.Lumen);
    }

    [TestMethod]
    public void Power()
    {
      var u = new Flux.Units.Power(1);

      Assert.AreEqual(1, u.Watt);
    }

    [TestMethod]
    public void Pressure()
    {
      var u = new Flux.Units.Pressure(1);

      Assert.AreEqual(1, u.Pascal);
      Assert.AreEqual(0.00014503773772954367, u.Psi);
    }

    [TestMethod]
    public void Radioactivity()
    {
      var u = new Flux.Units.Radioactivity(1);

      Assert.AreEqual(1, u.Becquerel);
    }

    [TestMethod]
    public void Speed()
    {
      var u = new Flux.Units.Speed(1);

      Assert.AreEqual(3.280839895013123, u.FootPerSecond);
      Assert.AreEqual(3.6, u.KilometerPerHour);
      Assert.AreEqual(1.9438444924406046, u.Knot);
      Assert.AreEqual(1, u.MeterPerSecond);
      Assert.AreEqual(2.2369362920544, u.MilePerHour);
      Assert.AreEqual(1.9438444924406, u.NauticalMilePerHour);
    }

    [TestMethod]
    public void Torque()
    {
      var u = new Flux.Units.Torque(1);

      Assert.AreEqual(1, u.NewtonMeter);
    }

    [TestMethod]
    public void Voltage()
    {
      var u = new Flux.Units.Voltage(1);

      Assert.AreEqual(1, u.Volt);
    }

    [TestMethod]
    public void Volume()
    {
      var u = new Flux.Units.Volume(1);

      Assert.AreEqual(1, u.CubicMeter);
    }
  }
}
