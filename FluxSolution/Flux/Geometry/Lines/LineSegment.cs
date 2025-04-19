namespace Flux.Geometry.Lines
{
  /// <summary>
  /// <para>Represents an finite line segment geometry.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Line_segment"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Line_(geometry)"/></para>
  /// </summary>
  public readonly record struct LineSegment
    : System.IFormattable
  {
    private readonly System.Runtime.Intrinsics.Vector128<double> m_v1;
    private readonly System.Runtime.Intrinsics.Vector128<double> m_v2;

    public LineSegment(System.Runtime.Intrinsics.Vector128<double> v1, System.Runtime.Intrinsics.Vector128<double> v2)
    {
      m_v1 = v1;
      m_v2 = v2;
    }
    public LineSegment(double x1, double y1, double x2, double y2)
    {
      m_v1 = System.Runtime.Intrinsics.Vector128.Create(x1, y1);
      m_v2 = System.Runtime.Intrinsics.Vector128.Create(x2, y2);
    }

    public void Deconstruct(out double px, out double py, out double qx, out double qy)
    {
      px = m_v1[0];
      py = m_v1[1];
      qx = m_v2[0];
      qy = m_v2[1];
    }

    public System.Runtime.Intrinsics.Vector128<double> V1 => m_v1;
    public System.Runtime.Intrinsics.Vector128<double> V2 => m_v2;

    public double X1 => m_v1[0];
    public double Y1 => m_v1[1];
    public double X2 => m_v2[0];
    public double Y2 => m_v2[1];

    public double Length => m_v1.Subtract(m_v2).EuclideanLength();

    public CoordinateSystems.CartesianCoordinate Lerp(double p) => new(p < 0 ? m_v1 : p > 1 ? m_v2 : m_v1.Lerp(m_v2, p));

    public Line ToLineGeometry() => new(m_v1, m_v2);

    #region Static methods

    public static (double a, double b, double c) GetLineEquationCoefficients(double x0, double y0, double x1, double y1)
    {
      var a = y0 - y1;
      var b = x1 - x0;
      var c = -a * x0 - b * y0;

      var z = double.Sqrt(a * a + b * b);

      return (a / z, b / z, c / z);
    }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/></para>
    /// </summary>
    /// <param name="x0"></param>
    /// <param name="y0"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="x3"></param>
    /// <param name="y3"></param>
    /// <returns></returns>
    public static (LineSegmentIntersectTest Outcome, double X, double Y) GivenTwoPointsOnEach(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3)
    {
      var sx0x1 = x0 - x1;
      var sx0x2 = x0 - x2;
      var sy0y1 = y0 - y1;
      var sy0y2 = y0 - y2;
      var sx2x3 = x2 - x3;
      var sy2y3 = y2 - y3;

      var t = sx0x2 * sy2y3 - sy0y2 * sx2x3;
      var u = sx0x1 * sy0y2 - sy0y1 * sx0x2;

      var d = sx0x1 * sy2y3 - sy0y1 * sx2x3;

      if (d != 0)
      {
        t /= d;
        u /= d;

        if (t >= 0 && t <= 1)
        {
          var ptx = x0 + t * (x1 - x0);
          var pty = y0 + t * (y1 - y0);

          return (LineSegmentIntersectTest.IntersectWithinFirstLineSegment, ptx, pty);
        }
        else if (u >= 0 && u <= 1)
        {
          var pux = x2 + u * (x3 - x2);
          var puy = y2 + u * (y3 - y2);

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

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/></para>
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <returns></returns>
    public static (LineSegmentIntersectTest Outcome, System.Runtime.Intrinsics.Vector128<double> Intersection) GivenTwoPointsOnEach(System.Runtime.Intrinsics.Vector128<double> p1, System.Runtime.Intrinsics.Vector128<double> p2, System.Runtime.Intrinsics.Vector128<double> p3, System.Runtime.Intrinsics.Vector128<double> p4)
    {
      var (outcome, x, y) = GivenTwoPointsOnEach(p1[0], p1[1], p2[0], p2[1], p3[0], p3[1], p4[0], p4[1]);

      return (outcome, System.Runtime.Intrinsics.Vector128.Create(x, y));
    }

    //public static (int Points, double X0, double Y0, double X1, double Y1) IntersectionCircleLineEquation(double r, double a, double b, double c)
    //{

    //  if (c * c > r * r * (a * a + b * b) + 1E-6) // No points:
    //    return (0, 0, 0, 0, 0);

    //  var x0 = -a * c / (a * a + b * b);
    //  var y0 = -b * c / (a * a + b * b);

    //  if (double.Abs(c * c - r * r * (a * a + b * b)) < 1E-6) // 1 point:
    //    return (1, x0, y0, 0, 0);

    //  // 2 points:

    //  var d = r * r - c * c / (a * a + b * b);
    //  var m = double.Sqrt(d / (a * a + b * b));

    //  var x1 = x0 - b * m;
    //  var y1 = y0 + a * m;

    //  x0 += b * m;
    //  y0 -= a * m;

    //  return (2, x0, y0, x1, y1);
    //}

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N3";

      return $"{GetType().Name} {{ X1 = {X1.ToString(format, formatProvider)}, Y1 = {Y1.ToString(format, formatProvider)}, X2 = {X2.ToString(format, formatProvider)}, Y2 = {Y2.ToString(format, formatProvider)} }}";
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
