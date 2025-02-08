using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class SiDerivedNamed
  {
    [TestMethod]
    public void AbsorbedDose()
    {
      var u = new Flux.Units.AbsorbedDose(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Angle()
    {
      var u = new Flux.Units.Angle(180, Flux.Units.AngleUnit.Degree);

      //var expected = (X: 0.5403023058681398, Y: 0.8414709848078965);
      //var actual = Flux.Units.Angle.ConvertRotationAngleToCartesian2(u.Value);
      //Assert.AreEqual(expected.X, actual.x, Flux.Maths.Epsilon1E15);
      //Assert.AreEqual(expected.Y, actual.y, Flux.Maths.Epsilon1E15);

      //expected = (0.8414709848078967, 0.5403023058681394);
      //actual = Flux.Units.Angle.ConvertRotationAngleToCartesian2Ex(u.Value);
      //Assert.AreEqual(expected.X, actual.x);
      //Assert.AreEqual(expected.Y, actual.y, Flux.Maths.Epsilon1E15);

      Assert.AreEqual(10800, u.GetUnitValue(Flux.Units.AngleUnit.Arcminute));
      Assert.AreEqual(648000, u.GetUnitValue(Flux.Units.AngleUnit.Arcsecond));
      Assert.AreEqual(180, u.GetUnitValue(Flux.Units.AngleUnit.Degree));
      Assert.AreEqual(200, u.GetUnitValue(Flux.Units.AngleUnit.Gradian));
      Assert.AreEqual(3141.592653589793, u.GetUnitValue(Flux.Units.AngleUnit.Milliradian));
      Assert.AreEqual(3200, u.GetUnitValue(Flux.Units.AngleUnit.NatoMil));
      Assert.AreEqual(3.141592653589793, u.GetUnitValue(Flux.Units.AngleUnit.Radian));
      Assert.AreEqual(0.5, u.GetUnitValue(Flux.Units.AngleUnit.Turn));
      Assert.AreEqual(3000, u.GetUnitValue(Flux.Units.AngleUnit.WarsawPactMil));
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
    public void ElectricalConductance()
    {
      var u = new Flux.Units.ElectricalConductance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricalResistance()
    {
      var u = new Flux.Units.ElectricalResistance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricCharge()
    {
      var u = new Flux.Units.ElectricCharge(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricPotential()
    {
      var u = new Flux.Units.ElectricPotential(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Energy()
    {
      var u = new Flux.Units.Energy(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void EquivalentDose()
    {
      var u = new Flux.Units.EquivalentDose(1);

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
      Assert.AreEqual(0.00001, u.GetUnitValue(Flux.Units.PressureUnit.Bar));
      Assert.AreEqual(0.01, u.GetUnitValue(Flux.Units.PressureUnit.Millibar));
      Assert.AreEqual(0.0001450377377302092, u.GetUnitValue(Flux.Units.PressureUnit.Psi));
    }

    [TestMethod]
    public void Radioctivity()
    {
      var u = new Flux.Units.Radioactivity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void SolidAngle()
    {
      var u = new Flux.Units.SolidAngle(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
