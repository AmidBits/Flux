using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Geodesy
  {
    [TestMethod]
    public void Azimuth()
    {
      var a = new Flux.Quantities.Angle(1, Flux.Quantities.AngleUnit.Degree);

      var u = new Flux.Quantities.Azimuth(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Quantities.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Latitude()
    {
      var a = new Flux.Quantities.Angle(1, Flux.Quantities.AngleUnit.Degree);

      var u = new Flux.Quantities.Latitude(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Quantities.AngleUnit.Degree), u.Value);
    }

    [TestMethod]
    public void Longitude()
    {
      var a = new Flux.Quantities.Angle(1, Flux.Quantities.AngleUnit.Degree);

      var u = new Flux.Quantities.Longitude(a);

      Assert.AreEqual(a.GetUnitValue(Flux.Quantities.AngleUnit.Degree), u.Value);
    }
  }
}
