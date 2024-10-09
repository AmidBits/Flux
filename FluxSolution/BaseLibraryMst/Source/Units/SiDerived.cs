using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
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
      var u = new Flux.Quantities.Angle(180, Flux.Quantities.AngleUnit.Degree);

      //var expected = (X: 0.5403023058681398, Y: 0.8414709848078965);
      //var actual = Flux.Units.Angle.ConvertRotationAngleToCartesian2(u.Value);
      //Assert.AreEqual(expected.X, actual.x, Flux.Maths.Epsilon1E15);
      //Assert.AreEqual(expected.Y, actual.y, Flux.Maths.Epsilon1E15);

      //expected = (0.8414709848078967, 0.5403023058681394);
      //actual = Flux.Units.Angle.ConvertRotationAngleToCartesian2Ex(u.Value);
      //Assert.AreEqual(expected.X, actual.x);
      //Assert.AreEqual(expected.Y, actual.y, Flux.Maths.Epsilon1E15);

      Assert.AreEqual(10800, u.GetUnitValue(Flux.Quantities.AngleUnit.Arcminute));
      Assert.AreEqual(648000, u.GetUnitValue(Flux.Quantities.AngleUnit.Arcsecond));
      Assert.AreEqual(180, u.GetUnitValue(Flux.Quantities.AngleUnit.Degree));
      Assert.AreEqual(200, u.GetUnitValue(Flux.Quantities.AngleUnit.Gradian));
      Assert.AreEqual(0.0031415926535897933, u.GetUnitValue(Flux.Quantities.AngleUnit.Milliradian));
      Assert.AreEqual(3200, u.GetUnitValue(Flux.Quantities.AngleUnit.NatoMil));
      Assert.AreEqual(3.141592653589793, u.GetUnitValue(Flux.Quantities.AngleUnit.Radian));
      Assert.AreEqual(0.5, u.GetUnitValue(Flux.Quantities.AngleUnit.Turn));
      Assert.AreEqual(3000, u.GetUnitValue(Flux.Quantities.AngleUnit.WarsawPactMil));
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
      var u = new Flux.Quantities.AngularFrequency(1);

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
      Assert.AreEqual(0.00001, u.GetUnitValue(Flux.Quantities.PressureUnit.Bar));
      Assert.AreEqual(0.01, u.GetUnitValue(Flux.Quantities.PressureUnit.Millibar));
      Assert.AreEqual(0.0001450377377302092, u.GetUnitValue(Flux.Quantities.PressureUnit.Psi));
    }

    [TestMethod]
    public void Activity()
    {
      var u = new Flux.Quantities.Radioactivity(1);

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
      var u = new Flux.Quantities.Speed(1);

      Assert.AreEqual(3.280839895013123, u.GetUnitValue(Flux.Quantities.SpeedUnit.FootPerSecond));
      Assert.AreEqual(3.5999999999999996, u.GetUnitValue(Flux.Quantities.SpeedUnit.KilometerPerHour));
      Assert.AreEqual(1.9438444924406046, u.GetUnitValue(Flux.Quantities.SpeedUnit.Knot));
      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(2.2369362920544025, u.GetUnitValue(Flux.Quantities.SpeedUnit.MilePerHour));
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
      var u = new Flux.Quantities.ElectricPotential(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Volume()
    {
      var u = new Flux.Quantities.Volume(1);

      Assert.AreEqual(35.31466672148859, u.GetUnitValue(Flux.Quantities.VolumeUnit.CubicFoot));
      Assert.AreEqual(1000000000, u.GetUnitValue(Flux.Quantities.VolumeUnit.CubicKilometer));
      Assert.AreEqual(2.3991275857892774E-10, u.GetUnitValue(Flux.Quantities.VolumeUnit.CubicMile));
      Assert.AreEqual(1.3079506193143922, u.GetUnitValue(Flux.Quantities.VolumeUnit.CubicYard));
      Assert.AreEqual(1000, u.GetUnitValue(Flux.Quantities.VolumeUnit.Liter));
      Assert.AreEqual(219.96924829909, u.GetUnitValue(Flux.Quantities.VolumeUnit.UKGallon));
      Assert.AreEqual(879.87699319635, u.GetUnitValue(Flux.Quantities.VolumeUnit.UKQuart));
      Assert.AreEqual(227.02074456538, u.GetUnitValue(Flux.Quantities.VolumeUnit.USDryGallon));
      Assert.AreEqual(264.17205124156, u.GetUnitValue(Flux.Quantities.VolumeUnit.USLiquidGallon));
      Assert.AreEqual(908.0829782615377, u.GetUnitValue(Flux.Quantities.VolumeUnit.USDryQuart));
      Assert.AreEqual(1056.6882049662338, u.GetUnitValue(Flux.Quantities.VolumeUnit.USLiquidQuart));

      Assert.AreEqual(1, u.Value);
    }
  }
}
