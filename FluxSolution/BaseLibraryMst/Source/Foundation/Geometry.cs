using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Foundation
{
  [TestClass]
  public class Geometry
  {
    [TestMethod]
    public void Hex()
    {
      var geometry = new Flux.Geometry.Hexagon.Hex(2, 1, -3);

      Assert.AreEqual(2, geometry.Q);
      Assert.AreEqual(1, geometry.R);
      Assert.AreEqual(-3, geometry.S);
    }

    [TestMethod]
    public void HexF()
    {
      var geometry = new Flux.Geometry.Hexagon.HexF(2, 1, -3);

      Assert.AreEqual(2, geometry.Q);
      Assert.AreEqual(1, geometry.R);
      Assert.AreEqual(-3, geometry.S);
    }

    [TestMethod]
    public void Ellipse()
    {
      var geometry = new Flux.Geometry.Ellipse(5, 7, 11);

      Assert.AreEqual(11, geometry.Angle);
      Assert.AreEqual(7, geometry.Height);
      Assert.AreEqual(5, geometry.Width);
    }

    [TestMethod]
    public void Line()
    {
      var geometry = new Flux.Geometry.Line(5, 7, 11, 13);

      Assert.AreEqual(5, geometry.P1.X);
      Assert.AreEqual(7, geometry.P1.Y);
      Assert.AreEqual(11, geometry.P2.X);
      Assert.AreEqual(13, geometry.P2.Y);
    }

    [TestMethod]
    public void Point2()
    {
      var geometry = new Flux.Geometry.Point2(5, 7);

      Assert.AreEqual(5, geometry.X);
      Assert.AreEqual(7, geometry.Y);
    }

    [TestMethod]
    public void Point3()
    {
      var geometry = new Flux.Geometry.Point3(5, 7, 11);

      Assert.AreEqual(5, geometry.X);
      Assert.AreEqual(7, geometry.Y);
      Assert.AreEqual(11, geometry.Z);
    }

    [TestMethod]
    public void Size2()
    {
      var geometry = new Flux.Geometry.Size2(5, 7);

      Assert.AreEqual(5, geometry.Width);
      Assert.AreEqual(7, geometry.Height);
    }

    [TestMethod]
    public void Size3()
    {
      var geometry = new Flux.Geometry.Size3(5, 7, 11);

      Assert.AreEqual(5, geometry.Width);
      Assert.AreEqual(7, geometry.Height);
      Assert.AreEqual(11, geometry.Depth);
    }
  }
}
