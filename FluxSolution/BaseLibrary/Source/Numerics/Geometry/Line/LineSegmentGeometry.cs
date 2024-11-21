using Flux.Coordinates;

namespace Flux
{
  public static partial class Em
  {
    public static (Geometry.LineSegmentIntersectTest Outcome, CartesianCoordinate Intersection) IntersectsWith(this Geometry.LineSegmentGeometry a, Geometry.LineSegmentGeometry b)
    {
      var (Outcome, Intersection) = Geometry.LineSegmentGeometry.GivenTwoPointsOnEach(a.V1, a.V2, b.V1, b.V2);

      return (Outcome, new(Intersection));
    }
  }

  namespace Geometry
  {
    /// <summary>
    /// <para>Represents an finite line segment geometry.</para>
    /// </summary>
    public readonly record struct LineSegmentGeometry
      : System.IFormattable
    {
      private readonly System.Runtime.Intrinsics.Vector128<double> m_v1;
      private readonly System.Runtime.Intrinsics.Vector128<double> m_v2;

      public LineSegmentGeometry(System.Runtime.Intrinsics.Vector128<double> v1, System.Runtime.Intrinsics.Vector128<double> v2)
      {
        m_v1 = v1;
        m_v2 = v2;
      }
      public LineSegmentGeometry(double x1, double y1, double x2, double y2)
      {
        m_v1 = System.Runtime.Intrinsics.Vector128.Create(x1, y1);
        m_v2 = System.Runtime.Intrinsics.Vector128.Create(x2, y2);
      }

      public System.Runtime.Intrinsics.Vector128<double> V1 => m_v1;
      public System.Runtime.Intrinsics.Vector128<double> V2 => m_v2;

      public double X1 => m_v1[0];
      public double Y1 => m_v1[1];
      public double X2 => m_v2[0];
      public double Y2 => m_v2[1];

      public double Length => m_v1.Subtract(m_v2).EuclideanLength();

      public CartesianCoordinate Lerp(double p) => new(p < 0 ? m_v1 : p > 1 ? m_v2 : m_v1.Lerp(m_v2, p));

      public LineGeometry ToLineGeometry() => new(m_v1, m_v2);

      #region Static methods

      public static (LineSegmentIntersectTest Outcome, double X, double Y) GivenTwoPointsOnEach(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
      {
        var sx1x2 = x1 - x2;
        var sx1x3 = x1 - x3;
        var sy1y2 = y1 - y2;
        var sy1y3 = y1 - y3;
        var sx3x4 = x3 - x4;
        var sy3y4 = y3 - y4;

        var t = sx1x3 * sy3y4 - sy1y3 * sx3x4;
        var u = sx1x2 * sy1y3 - sy1y2 * sx1x3;

        var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

        if (d != 0)
        {
          t /= d;
          u /= d;

          if (t >= 0 && t <= 1)
          {
            var ptx = x1 + t * (x2 - x1);
            var pty = y1 + t * (y2 - y1);

            return (LineSegmentIntersectTest.IntersectWithinFirstLineSegment, ptx, pty);
          }
          else if (u >= 0 && u <= 1)
          {
            var pux = x3 + u * (x4 - x3);
            var puy = y3 + u * (y4 - y3);

            return (LineSegmentIntersectTest.IntersectWithinSecondLineSegment, pux, puy);
          }
          else // Not intersecting.
            return (LineSegmentIntersectTest.NonIntersectingLineSegments, double.NaN, double.NaN);
        }
        else // Line segments are coincident or parallel, i.e. NOT intersecting.
        {
          if (t == 0 || u == 0) // Coincident line segments.
            return (LineSegmentIntersectTest.CoincidentLineSegments, double.NaN, double.NaN);
          else // Parallel line segments.
            return (LineSegmentIntersectTest.ParallelLineSegments, double.NaN, double.NaN);
        }
      }

      public static (LineSegmentIntersectTest Outcome, System.Numerics.Vector2 Intersection) GivenTwoPointsOnEach(System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3, System.Numerics.Vector2 v4)
      {
        var (Outcome, X, Y) = GivenTwoPointsOnEach(v1.X, v1.Y, v2.X, v2.Y, v3.X, v3.Y, v4.X, v4.Y);

        return (Outcome, new((float)X, (float)Y));
      }

      public static (LineSegmentIntersectTest Outcome, System.Runtime.Intrinsics.Vector128<double> Intersection) GivenTwoPointsOnEach(System.Runtime.Intrinsics.Vector128<double> v1, System.Runtime.Intrinsics.Vector128<double> v2, System.Runtime.Intrinsics.Vector128<double> v3, System.Runtime.Intrinsics.Vector128<double> v4)
      {
        var (Outcome, X, Y) = GivenTwoPointsOnEach(v1[0], v1[1], v2[0], v2[1], v3[0], v3[1], v4[0], v4[1]);

        return (Outcome, System.Runtime.Intrinsics.Vector128.Create(X, Y));
      }

      #endregion // Static methods

      public string ToString(string? format, IFormatProvider? formatProvider) => GetType().Name;

      public override string ToString() => ToString(null, null);
    }
  }
}
