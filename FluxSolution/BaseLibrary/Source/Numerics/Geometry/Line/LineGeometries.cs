namespace Flux.Geometry
{
  public static partial class LineGeometries
  {
    #region Line methods

    public static (double a, double b, double c) GetLineEquationCoefficients(double aX, double aY, double bX, double bY)
      => (aY - bY, bX - aX, aX * bY - bX * aY);

    public static (LineIntersectTest Outcome, double X, double Y) GivenTwoPointsOnEachLine(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
    {
      var sx1x2 = x1 - x2;
      var sy1y2 = y1 - y2;
      var sx3x4 = x3 - x4;
      var sy3y4 = y3 - y4;

      var nl = x1 * y2 - y1 * x2; // P numerator-left component.
      var nr = x3 * y4 - y3 * x4; // P numerator-right component.

      var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

      var px = (nl * sx3x4 - sx1x2 * nr);
      var py = (nl * sy3y4 - sy1y2 * nr);

      // if(d == 0)return // coincident or parallel, i.e. NOT intersecting.
      if (d != 0)
      {
        px /= d;
        py /= d;

        return (LineIntersectTest.LinesIntersect, px, py);
      }
      else
        return (LineIntersectTest.NonIntersectingLines, double.NaN, double.NaN);
    }

    public static (LineIntersectTest Outcome, System.Numerics.Vector2 Intersection) GivenTwoPointsOnEachLine(System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3, System.Numerics.Vector2 v4)
    {
      var (outcome, x, y) = GivenTwoPointsOnEachLine(v1.X, v1.Y, v2.X, v2.Y, v3.X, v3.Y, v4.X, v4.Y);

      return (outcome, new((float)x, (float)y));
    }

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
    public static double SideTestLine(double x, double y, double x1, double y1, double x2, double y2)
      => (x2 - x1) * (y - y1) - (y2 - y1) * (x - x1);

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
    public static int SideTestLine(System.Numerics.Vector2 test, System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => System.Math.Sign(SideTestLine(test.X, test.Y, a.X, a.Y, b.X, b.Y));

    #endregion // Line methods

    #region LineSegment methods

    public static (LineSegmentIntersectTest Outcome, double X, double Y) GivenTwoPointsOnEachLineSegment(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
    {
      var sx1x2 = x1 - x2;
      var sx1x3 = x1 - x3;
      var sy1y2 = y1 - y2;
      var sy1y3 = y1 - y3;
      var sx3x4 = x3 - x4;
      var sy3y4 = y3 - y4;

      var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

      var t = sx1x3 * sy3y4 - sy1y3 * sx3x4;
      var u = sx1x2 * sy1y3 - sy1y2 * sx1x3;

      if (d != 0)
      {
        t /= d;
        u /= d;

        var ptx = x1 + t * (x2 - x1);
        var pty = y1 + t * (y2 - y1);
        var pux = x3 + u * (x4 - x3);
        var puy = y3 + u * (y4 - y3);

        if (t >= 0 && t <= 1)
          return (LineSegmentIntersectTest.IntersectWithinFirst, ptx, pty);
        else if (u >= 0 && u <= 1)
          return (LineSegmentIntersectTest.IntersectWithinSecond, pux, puy);
        else // Not intersecting.
          return (LineSegmentIntersectTest.Unknown, double.NaN, double.NaN);
      }
      else
      {
        if (t == 0 || u == 0) // Coincident lines.
          return (LineSegmentIntersectTest.Coincident, double.NaN, double.NaN);
        else // Parallel lines.
          return (LineSegmentIntersectTest.Parallel, double.NaN, double.NaN);
      }
    }

    public static (LineSegmentIntersectTest Outcome, System.Numerics.Vector2 Intersection) GivenTwoPointsOnEachLineSegment(System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3, System.Numerics.Vector2 v4)
    {
      var (Outcome, X, Y) = GivenTwoPointsOnEachLineSegment(v1.X, v1.Y, v2.X, v2.Y, v3.X, v3.Y, v4.X, v4.Y);

      return (Outcome, new((float)X, (float)Y));
    }

    #endregion // LineSegment methods
  }
}
