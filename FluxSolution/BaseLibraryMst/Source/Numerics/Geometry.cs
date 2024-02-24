#if NET7_0_OR_GREATER
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maths
{
  [TestClass]
  public class Geometry
  {
    //[TestMethod]
    //public void HexCoordinateI()
    //{
    //  var geometry = new Flux.Numerics.HexCoordinate<int>(2, 1, -3);

    //  Assert.AreEqual(2, geometry.Q);
    //  Assert.AreEqual(1, geometry.R);
    //  Assert.AreEqual(-3, geometry.S);
    //}

    //[TestMethod]
    //public void HexCoordinateR()
    //{
    //  var geometry = new Flux.Numerics.HexCoordinate<double>(2, 1, -3);

    //  Assert.AreEqual(2, geometry.Q);
    //  Assert.AreEqual(1, geometry.R);
    //  Assert.AreEqual(-3, geometry.S);
    //}

    [TestMethod]
    public void Ellipse()
    {
      var geometry = new Flux.Geometry.EllipseGeometry(5, 7);

      Assert.AreEqual(5, geometry.A);
      Assert.AreEqual(7, geometry.B);
    }

    [TestMethod]
    public void GivenTwoPointsOnEachLine()
    {
      var expected = (Flux.Tools.IntersectTestLine.LinesIntersect, 17d / 11d, 14d / 11d);
      var actual = Flux.Tools.GivenTwoPointsOnEachLine(1, 1, 3, 2, 1, 4, 2, -1);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GivenTwoPointsOnEachLineSegment()
    {
      var expected = (Flux.Tools.IntersectTestLineSegment.IntersectWithinFirst, 17d / 11d, 14d / 11d);
      var actual = Flux.Tools.GivenTwoPointsOnEachLineSegment(1, 1, 3, 2, 1, 4, 2, -1);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Line()
    {
      var geometry = new Flux.Geometry.LineSegment(5, 7, 11, 13);

      Assert.AreEqual(5, geometry.X1);
      Assert.AreEqual(7, geometry.Y1);
      Assert.AreEqual(11, geometry.X2);
      Assert.AreEqual(13, geometry.Y2);
    }

    //[TestMethod]
    //public void Point2()
    //{
    //  var geometry = new Flux.Point2(5, 7);

    //  Assert.AreEqual(5, geometry.X);
    //  Assert.AreEqual(7, geometry.Y);
    //}

    //[TestMethod]
    //public void Point3()
    //{
    //  var geometry = new Flux.Point3(5, 7, 11);

    //  Assert.AreEqual(5, geometry.X);
    //  Assert.AreEqual(7, geometry.Y);
    //  Assert.AreEqual(11, geometry.Z);
    //}

    //[TestMethod]
    //public void Size2()
    //{
    //  var geometry = new Flux.Size2(5, 7);

    //  Assert.AreEqual(5, geometry.Width);
    //  Assert.AreEqual(7, geometry.Height);
    //}

    //[TestMethod]
    //public void Size3()
    //{
    //  var geometry = new Flux.Size3(5, 7, 11);

    //  Assert.AreEqual(5, geometry.Width);
    //  Assert.AreEqual(7, geometry.Height);
    //  Assert.AreEqual(11, geometry.Depth);
    //}
  }
}
#endif
