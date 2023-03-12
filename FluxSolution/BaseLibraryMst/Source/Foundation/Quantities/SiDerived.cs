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
      var u = new Flux.Quantities.Acceleration(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Action()
    {
      var u = new Flux.Quantities.Action(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Angle()
    {
      var u = new Flux.Quantities.Angle(1);

      var expected = new Flux.Numerics.CartesianCoordinate2<double>(0.5403023058681398, 0.8414709848078965);
      var actual = Convert.RotationAngleToCartesian2(u.Value);
      Assert.AreEqual(expected.X, actual.x, Flux.GenericMath.Epsilon1E15);
      Assert.AreEqual(expected.Y, actual.y, Flux.GenericMath.Epsilon1E15);

      expected = new Flux.Numerics.CartesianCoordinate2<double>(0.8414709848078966, 0.5403023058681394);
      actual = Convert.RotationAngleToCartesian2Ex(u.Value);
      Assert.AreEqual(expected.X, actual.x);
      Assert.AreEqual(expected.Y, actual.y, Flux.GenericMath.Epsilon1E15);

      Assert.AreEqual(57.29577951308232, u.ToUnitValue(Flux.Quantities.AngleUnit.Degree));
      Assert.AreEqual(63.66197723675813, u.ToUnitValue(Flux.Quantities.AngleUnit.Gradian));
      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(0.15915494309189535, u.ToUnitValue(Flux.Quantities.AngleUnit.Turn));
    }

    [TestMethod]
    public void AngularAcceleration()
    {
      var u = new Flux.Quantities.AngularAcceleration(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AngularVelocity()
    {
      var u = new Flux.Quantities.AngularVelocity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Area()
    {
      var u = new Flux.Quantities.Area(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Capacitance()
    {
      var u = new Flux.Quantities.Capacitance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void CatalyticActivity()
    {
      var u = new Flux.Quantities.CatalyticActivity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Density()
    {
      var u = new Flux.Quantities.Density(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricalConductance()
    {
      var u = new Flux.Quantities.ElectricalConductance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricCharge()
    {
      var u = new Flux.Quantities.ElectricCharge(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricResistance()
    {
      var u = new Flux.Quantities.ElectricalResistance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Energy()
    {
      var u = new Flux.Quantities.Energy(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Flow()
    {
      var u = new Flux.Quantities.Flow(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Force()
    {
      var u = new Flux.Quantities.Force(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Frequency()
    {
      var u = new Flux.Quantities.Frequency(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Illuminance()
    {
      var u = new Flux.Quantities.Illuminance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Inductance()
    {
      var u = new Flux.Quantities.Inductance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void LuminousFlux()
    {
      var u = new Flux.Quantities.LuminousFlux(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void MagneticFlux()
    {
      var u = new Flux.Quantities.MagneticFlux(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void MagneticFluxDensity()
    {
      var u = new Flux.Quantities.MagneticFluxDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Power()
    {
      var u = new Flux.Quantities.Power(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Pressure()
    {
      var u = new Flux.Quantities.Pressure(1);

      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(0.0001450377377302092, u.ToUnitValue(Flux.Quantities.PressureUnit.Psi));
    }

    [TestMethod]
    public void Activity()
    {
      var u = new Flux.Quantities.Activity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void SolidAngle()
    {
      var u = new Flux.Quantities.SolidAngle(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Speed()
    {
      var u = new Flux.Quantities.LinearVelocity(1);

      Assert.AreEqual(3.2808398950131235, u.ToUnitValue(Flux.Quantities.LinearVelocityUnit.FootPerSecond));
      Assert.AreEqual(3.6, u.ToUnitValue(Flux.Quantities.LinearVelocityUnit.KilometerPerHour));
      Assert.AreEqual(1.9438444924406046, u.ToUnitValue(Flux.Quantities.LinearVelocityUnit.Knot));
      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(2.2369362920544025, u.ToUnitValue(Flux.Quantities.LinearVelocityUnit.MilePerHour));
    }

    [TestMethod]
    public void Torque()
    {
      var u = new Flux.Quantities.Torque(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Voltage()
    {
      var u = new Flux.Quantities.Voltage(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Volume()
    {
      var u = new Flux.Quantities.Volume(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
