﻿using Flux;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.Units
{
  [TestClass]
  public class SiDerived
  {
    [TestMethod]
    public void Acceleration()
    {
      var u = new Flux.Acceleration(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Angle()
    {
      var u = new Flux.Angle(1);

      var expected = new Flux.CartesianCoordinate2(0.5403023058681398, 0.8414709848078965);
      var actual = u.ToCartesian2();
      Assert.AreEqual(expected.X, actual.X, Flux.Maths.Epsilon1E15);
      Assert.AreEqual(expected.Y, actual.Y, Flux.Maths.Epsilon1E15);

      expected = new Flux.CartesianCoordinate2(0.8414709848078966, 0.5403023058681394);
      actual = u.ToCartesian2Ex();
      Assert.AreEqual(expected.X, actual.X);
      Assert.AreEqual(expected.Y, actual.Y, Flux.Maths.Epsilon1E15);

      Assert.AreEqual(57.29577951308232, u.ToUnitValue(Flux.AngleUnit.Degree));
      Assert.AreEqual(63.66197723675813, u.ToUnitValue(Flux.AngleUnit.Gradian));
      Assert.AreEqual(1, u.GeneralUnitValue);
      Assert.AreEqual(0.15915494309189535, u.ToUnitValue(Flux.AngleUnit.Turn));
    }

    [TestMethod]
    public void AngularAcceleration()
    {
      var u = new Flux.AngularAcceleration(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void AngularVelocity()
    {
      var u = new Flux.AngularVelocity(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Area()
    {
      var u = new Flux.Area(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Capacitance()
    {
      var u = new Flux.Capacitance(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void CatalyticActivity()
    {
      var u = new Flux.CatalyticActivity(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Density()
    {
      var u = new Flux.Density(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void ElectricalConductance()
    {
      var u = new Flux.ElectricalConductance(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void ElectricCharge()
    {
      var u = new Flux.ElectricCharge(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void ElectricResistance()
    {
      var u = new Flux.ElectricResistance(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Energy()
    {
      var u = new Flux.Energy(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Flow()
    {
      var u = new Flux.Flow(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Force()
    {
      var u = new Flux.Force(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Frequency()
    {
      var u = new Flux.Frequency(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Illuminance()
    {
      var u = new Flux.Illuminance(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Inductance()
    {
      var u = new Flux.Inductance(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void LuminousFlux()
    {
      var u = new Flux.LuminousFlux(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Power()
    {
      var u = new Flux.Power(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Pressure()
    {
      var u = new Flux.Pressure(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
      Assert.AreEqual(0.0001450377377302092, u.ToUnitValue(Flux.PressureUnit.Psi));
    }

    [TestMethod]
    public void Radioactivity()
    {
      var u = new Flux.Radioactivity(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Speed()
    {
      var u = new Flux.Speed(1);

      Assert.AreEqual(3.2808398950131235, u.ToUnitValue(Flux.SpeedUnit.FeetPerSecond));
      Assert.AreEqual(3.6, u.ToUnitValue(Flux.SpeedUnit.KilometersPerHour));
      Assert.AreEqual(1.9438444924406046, u.ToUnitValue(Flux.SpeedUnit.Knots));
      Assert.AreEqual(1, u.GeneralUnitValue);
      Assert.AreEqual(2.2369362920544025, u.ToUnitValue(Flux.SpeedUnit.MilesPerHour));
    }

    [TestMethod]
    public void Torque()
    {
      var u = new Flux.Torque(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Voltage()
    {
      var u = new Flux.Voltage(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }

    [TestMethod]
    public void Volume()
    {
      var u = new Flux.Volume(1);

      Assert.AreEqual(1, u.GeneralUnitValue);
    }
  }
}