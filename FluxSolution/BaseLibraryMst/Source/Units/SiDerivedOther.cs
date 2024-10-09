using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class SiDerivedOther
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
    public void AngularAcceleration()
    {
      var u = new Flux.Quantities.AngularAcceleration(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void AngularFrequency()
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
    public void AreaDensity()
    {
      var u = new Flux.Quantities.AreaDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void CurrentDensity()
    {
      var u = new Flux.Quantities.CurrentDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Density()
    {
      var u = new Flux.Quantities.Density(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void DynamicViscosity()
    {
      var u = new Flux.Quantities.DynamicViscosity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void ElectricChargeDensity()
    {
      var u = new Flux.Quantities.ElectricChargeDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void EnergyDensity()
    {
      var u = new Flux.Quantities.EnergyDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Flow()
    {
      var u = new Flux.Quantities.Flow(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void HeatCapacity()
    {
      var u = new Flux.Quantities.HeatCapacity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Impulse()
    {
      var u = new Flux.Quantities.Impulse(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Irradiance()
    {
      var u = new Flux.Quantities.Irradiance(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void LinearChargeDensity()
    {
      var u = new Flux.Quantities.LinearChargeDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void LinearDensity()
    {
      var u = new Flux.Quantities.LinearDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void LuminousEfficacy()
    {
      var u = new Flux.Quantities.LuminousEfficacy(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void MagneticFluxStrength()
    {
      var u = new Flux.Quantities.MagneticFluxStrength(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Molarity()
    {
      var u = new Flux.Quantities.Molarity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Permeability()
    {
      var u = new Flux.Quantities.Permeability(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void RadiationExposure()
    {
      var u = new Flux.Quantities.RadiationExposure(1);

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
    public void SurfaceChargeDensity()
    {
      var u = new Flux.Quantities.SurfaceChargeDensity(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void SurfaceTension()
    {
      var u = new Flux.Quantities.SurfaceTension(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void Torque()
    {
      var u = new Flux.Quantities.Torque(1);

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

    [TestMethod]
    public void Wavelength()
    {
      var u = new Flux.Quantities.Wavelength(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
