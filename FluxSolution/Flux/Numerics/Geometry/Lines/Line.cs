namespace Flux.Numerics.Geometry.Lines
{
  /// <summary>
  /// <para>Represents an infinite line geometry defined the same as a line segment.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Line_(geometry)"/></para>
  /// <para><seealso href="https://math.stackexchange.com/questions/637922/how-can-i-find-coefficients-a-b-c-given-two-points"/></para>
  /// <para><seealso href="https://www.mathsisfun.com/algebra/line-equation-2points.html"/></para>
  /// <para><seealso href="https://keisan.casio.com/exec/system/1223508685"/></para>
  /// </summary>
  public readonly record struct Line
    : System.IFormattable
  {
    public static Line UnitX { get; } = new(0, 0, 1, 0);
    public static Line UnitY { get; } = new(0, 0, 0, 1);

    private readonly System.Runtime.Intrinsics.Vector128<double> m_v0;
    private readonly System.Runtime.Intrinsics.Vector128<double> m_v1;

    public Line(System.Runtime.Intrinsics.Vector128<double> v0, System.Runtime.Intrinsics.Vector128<double> v1)
    {
      m_v0 = v0;
      m_v1 = v1;
    }
    public Line(double x0, double y0, double x1, double y1)
    {
      m_v0 = System.Runtime.Intrinsics.Vector128.Create(x0, y0);
      m_v1 = System.Runtime.Intrinsics.Vector128.Create(x1, y1);
    }

    public void Deconstruct(out double x0, out double y0, out double x1, out double y1)
    {
      x0 = m_v0[0];
      y0 = m_v0[1];
      x1 = m_v1[0];
      y1 = m_v1[1];
    }

    public System.Runtime.Intrinsics.Vector128<double> V0 => m_v0;
    public System.Runtime.Intrinsics.Vector128<double> V1 => m_v1;

    public double X0 => m_v0[0];
    public double Y0 => m_v0[1];
    public double X1 => m_v1[0];
    public double Y1 => m_v1[1];

    public CoordinateSystems.CartesianCoordinate Lerp(double p) => new(m_v0.Lerp(m_v1, p));

    public double SideTest(double x, double y) => LineSideTest(x, y, m_v0[0], m_v0[1], m_v1[0], m_v1[1]);

    public LineSegment ToLineSegmentGeometry() => new(m_v0, m_v1);

    public Numerics.Slope ToSlope() => new(TwoPointSlope(m_v0, m_v1));

    #region Static methods

    //public static (double a, double b, double c) GetLineEquationCoefficients(double aX, double aY, double bX, double bY)
    //  => (aY - bY, bX - aX, aX * bY - bX * aY);

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line"/></para>
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
    public static (LineIntersectTest Outcome, double X, double Y) GivenTwoPointsOnEach(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3)
    {
      var sx0x1 = x0 - x1;
      var sy0y1 = y0 - y1;
      var sx2x3 = x2 - x3;
      var sy2y3 = y2 - y3;

      var nl = x0 * y1 - y0 * x1; // P numerator-left component.
      var nr = x2 * y3 - y2 * x3; // P numerator-right component.

      var px = (nl * sx2x3 - sx0x1 * nr);
      var py = (nl * sy2y3 - sy0y1 * nr);

      var d = sx0x1 * sy2y3 - sy0y1 * sx2x3;

      if (d != 0)
      {
        px /= d;
        py /= d;

        return (LineIntersectTest.LinesIntersect, px, py);
      }
      else // Lines are coincident or parallel, i.e. NOT intersecting.
        return (LineIntersectTest.NonIntersectingLines, double.NaN, double.NaN);
    }

    public static (LineIntersectTest Outcome, System.Numerics.Vector2 Intersection) GivenTwoPointsOnEach(System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3, System.Numerics.Vector2 v4)
    {
      var (outcome, x, y) = GivenTwoPointsOnEach(v1.X, v1.Y, v2.X, v2.Y, v3.X, v3.Y, v4.X, v4.Y);

      return (outcome, new((float)x, (float)y));
    }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line"/></para>
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <returns></returns>
    public static (LineIntersectTest Outcome, System.Runtime.Intrinsics.Vector128<double> Intersection) GivenTwoPointsOnEach(System.Runtime.Intrinsics.Vector128<double> p1, System.Runtime.Intrinsics.Vector128<double> p2, System.Runtime.Intrinsics.Vector128<double> p3, System.Runtime.Intrinsics.Vector128<double> p4)
    {
      var (outcome, x, y) = GivenTwoPointsOnEach(p1[0], p1[1], p2[0], p2[1], p3[0], p3[1], p4[0], p4[1]);

      return (outcome, System.Runtime.Intrinsics.Vector128.Create(x, y));
    }


    /// <summary>
    /// <para>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</para>
    /// </summary>
    /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
    public static double LineSideTest(double testX, double testY, double x0, double y0, double x1, double y1)
      => (x1 - x0) * (testY - y0) - (y1 - y0) * (testX - x0);

    ///// <summary>
    ///// <para>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</para>
    ///// </summary>
    ///// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
    //public static float SideTest(System.Numerics.Vector2 testV, System.Numerics.Vector2 v1, System.Numerics.Vector2 v2)
    //  => (v2.X - v1.X) * (testV.Y - v1.Y) - (v2.Y - v1.Y) * (testV.X - v1.X);

    /// <summary>
    /// <para>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</para>
    /// </summary>
    /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
    public static double LineSideTest(System.Runtime.Intrinsics.Vector128<double> testV, System.Runtime.Intrinsics.Vector128<double> v0, System.Runtime.Intrinsics.Vector128<double> v1)
      => (v1 - v0).Cross(testV - v0); // The 2D cross product of v1-to-v0 and testV-to-v0.
    //=> LineSideTest(testV[0], testV[1], v0[0], v0[1], v1[0], v1[1]);

    /// <summary>
    /// <para>Returns the slope of a line/edge, between point(<paramref name="x0"/>, <paramref name="y0"/>) and point(<paramref name="x1"/>, <paramref name="y1"/>). The slope is the change in y divided by the corresponding change in x. Basically slope represents vertial change divided by horizontal change, or rise divided by run.</para>
    /// </summary>
    /// <param name="x0"></param>
    /// <param name="y0"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <returns></returns>
    public static double TwoPointSlope(double x0, double y0, double x1, double y1) => (x0 - x1) / (y0 - y1);

    /// <summary>
    /// <para>Returns the slope of a line/edge, between <paramref name="p1"/> and <paramref name="p2"/>. The slope is the change in y divided by the corresponding change in x. Basically slope represents vertial change divided by horizontal change, or rise divided by run.</para>
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static double TwoPointSlope(System.Runtime.Intrinsics.Vector128<double> p1, System.Runtime.Intrinsics.Vector128<double> p2) => (p1[0] - p2[0]) / (p1[1] - p2[1]);


    //    /// <summary>Returns the corresponding y from the specified x and the two specified points, from which the slope and absolute x are derived and returned as out arguments.</summary>
    //    /// <param name="x"></param>
    //    /// <param name="p1"></param>
    //    /// <param name="p2"></param>
    //    /// <param name="slope"></param>
    //    /// <param name="absoluteX"></param>
    //    public static float TwoPointForm(float x, in System.Numerics.Vector2 p1, in System.Numerics.Vector2 p2, out float slope, out float absoluteX)
    //      => p1.Y + (slope = TwoPointSlope(p1, p2)) * (absoluteX = (x - p1.X));

    //    /// <summary>Returns the slope of the line/edge, between point1 and point2. The slope is the change in y divided by the corresponding change in x. Basically slope represents vertial change divided by horizontal change, or rise divided by run.</summary>
    //    public static float TwoPointSlope(in System.Numerics.Vector2 p0, in System.Numerics.Vector2 p1)
    //      => (p0.X - p1.X) / (p0.Y - p1.Y);

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N3";

      return $"{GetType().Name} {{ X1 = {X0.ToString(format, formatProvider)}, Y1 = {Y0.ToString(format, formatProvider)}, X2 = {X1.ToString(format, formatProvider)}, Y2 = {Y1.ToString(format, formatProvider)} }}";
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
