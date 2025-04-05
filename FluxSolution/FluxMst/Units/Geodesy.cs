using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Geodesy
  {
    [TestMethod]
    public void Azimuth()
    {
      var a = new Flux.Units.Angle(1, Flux.Units.AngleUnit.Degree);

      var u = new Flux.PlanetaryScience.Azimuth(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Units.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Latitude()
    {
      var a = new Flux.Units.Angle(1, Flux.Units.AngleUnit.Degree);

      var u = new Flux.PlanetaryScience.Latitude(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Units.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Longitude()
    {
      var a = new Flux.Units.Angle(1, Flux.Units.AngleUnit.Degree);

      var u = new Flux.PlanetaryScience.Longitude(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Units.AngleUnit.Degree), u.Value);
    }
  }
}
