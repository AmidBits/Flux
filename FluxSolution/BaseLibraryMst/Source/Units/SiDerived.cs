using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class SiDerived
  {
    [TestMethod]
    public void Acceleration()
    {
      var u = new Flux.Units.Acceleration(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Action()
    {
      var u = new Flux.Units.Action(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Angle()
    {
      var u = new Flux.Units.Angle(1);

      var expected = (X: 0.5403023058681398, Y: 0.8414709848078965);
      var actual = Flux.Units.Angle.ConvertRotationAngleToCartesian2(u.Value);
      Assert.AreEqual(expected.X, actual.x, Flux.Maths.Epsilon1E15);
      Assert.AreEqual(expected.Y, actual.y, Flux.Maths.Epsilon1E15);

      expected = (0.8414709848078967, 0.5403023058681394);
      actual = Flux.Units.Angle.ConvertRotationAngleToCartesian2Ex(u.Value);
      Assert.AreEqual(expected.X, actual.x);
      Assert.AreEqual(expected.Y, actual.y, Flux.Maths.Epsilon1E15);

      Assert.AreEqual(57.29577951308232, u.GetUnitValue(Flux.Units.AngleUnit.Degree));
      Assert.AreEqual(63.66197723675813, u.GetUnitValue(Flux.Units.AngleUnit.Gradian));
      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(0.15915494309189535, u.GetUnitValue(Flux.Units.AngleUnit.Turn));
    }

    [TestMethod]
    public void AngularAcceleration()
    {
      var u = new Flux.Units.AngularAcceleration(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AngularVelocity()
    {
      var u = new Flux.Units.AngularFrequency(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Area()
    {
      var u = new Flux.Units.Area(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Capacitance()
    {
      var u = new Flux.Units.Capacitance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void CatalyticActivity()
    {
      var u = new Flux.Units.CatalyticActivity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Density()
    {
      var u = new Flux.Units.Density(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricalConductance()
    {
      var u = new Flux.Units.ElectricalConductance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricCharge()
    {
      var u = new Flux.Units.ElectricCharge(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricResistance()
    {
      var u = new Flux.Units.ElectricalResistance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Energy()
    {
      var u = new Flux.Units.Energy(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Flow()
    {
      var u = new Flux.Units.Flow(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Force()
    {
      var u = new Flux.Units.Force(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Frequency()
    {
      var u = new Flux.Units.Frequency(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Illuminance()
    {
      var u = new Flux.Units.Illuminance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Inductance()
    {
      var u = new Flux.Units.Inductance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void LuminousFlux()
    {
      var u = new Flux.Units.LuminousFlux(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void MagneticFlux()
    {
      var u = new Flux.Units.MagneticFlux(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void MagneticFluxDensity()
    {
      var u = new Flux.Units.MagneticFluxDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Power()
    {
      var u = new Flux.Units.Power(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Pressure()
    {
      var u = new Flux.Units.Pressure(1);

      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(0.0001450377377302092, u.GetUnitValue(Flux.Units.PressureUnit.Psi));
    }

    [TestMethod]
    public void Activity()
    {
      var u = new Flux.Units.Activity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void SolidAngle()
    {
      var u = new Flux.Units.SolidAngle(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Speed()
    {
      var u = new Flux.Units.Speed(1);

      Assert.AreEqual(3.2808398950131235, u.GetUnitValue(Flux.Units.SpeedUnit.FootPerSecond));
      Assert.AreEqual(3.6, u.GetUnitValue(Flux.Units.SpeedUnit.KilometerPerHour));
      Assert.AreEqual(1.9438444924406046, u.GetUnitValue(Flux.Units.SpeedUnit.Knot));
      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(2.2369362920544025, u.GetUnitValue(Flux.Units.SpeedUnit.MilePerHour));
    }

    [TestMethod]
    public void Torque()
    {
      var u = new Flux.Units.Torque(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Voltage()
    {
      var u = new Flux.Units.Voltage(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Volume()
    {
      var u = new Flux.Units.Volume(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
