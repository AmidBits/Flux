namespace Flux
{
  public static partial class Em
  {
    public static (Geometry.Shapes.Line.LineIntersectTest Outcome, Geometry.CoordinateSystems.CartesianCoordinate Intersection) IntersectsWith(this Geometry.Shapes.Line.LineGeometry a, Geometry.Shapes.Line.LineGeometry b)
    {
      var (Outcome, Intersection) = Geometry.Shapes.Line.LineGeometry.GivenTwoPointsOnEach(a.V1, a.V2, b.V1, b.V2);

      return (Outcome, new(Intersection));
    }
  }

  namespace Geometry.Shapes.Line
  {
    /// <summary>
    /// <para>Represents an infinite line geometry defined the same as a line segment.</para>
    /// <para><seealso href="https://math.stackexchange.com/questions/637922/how-can-i-find-coefficients-a-b-c-given-two-points"/></para>
    /// <para><seealso href="https://www.mathsisfun.com/algebra/line-equation-2points.html"/></para>
    /// <para><seealso href="https://keisan.casio.com/exec/system/1223508685"/></para>
    /// </summary>
    public readonly record struct LineGeometry
      : System.IFormattable
    {
      public static LineGeometry UnitX { get; } = new(0, 0, 1, 0);
      public static LineGeometry UnitY { get; } = new(0, 0, 0, 1);

      private readonly System.Runtime.Intrinsics.Vector128<double> m_v1;
      private readonly System.Runtime.Intrinsics.Vector128<double> m_v2;

      public LineGeometry(System.Runtime.Intrinsics.Vector128<double> v1, System.Runtime.Intrinsics.Vector128<double> v2)
      {
        m_v1 = v1;
        m_v2 = v2;
      }
      public LineGeometry(double x1, double y1, double x2, double y2)
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

      public CoordinateSystems.CartesianCoordinate Lerp(double p) => new(m_v1.Lerp(m_v2, p));

      public double SideTest(double x, double y) => SideTest(x, y, X1, Y1, X2, Y2);

      public LineSegmentGeometry ToLineSegmentGeometry() => new(m_v1, m_v2);

      /// <summary>
      /// <para>Returns the slope of the line/edge, between point1 and point2. THe slope is the change in y divided by the corresponding change in x. Basically slope represents vertial change divided by horizontal change, or rise divided by run.</para>
      /// </summary>
      public double TwoPointSlope() => (m_v1[0] - m_v2[0]) / (m_v1[1] - m_v2[1]);

      #region Static methods

      public static (double a, double b, double c) GetLineEquationCoefficients(double aX, double aY, double bX, double bY)
        => (aY - bY, bX - aX, aX * bY - bX * aY);

      public static (LineIntersectTest Outcome, double X, double Y) GivenTwoPointsOnEach(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
      {
        var sx1x2 = x1 - x2;
        var sy1y2 = y1 - y2;
        var sx3x4 = x3 - x4;
        var sy3y4 = y3 - y4;

        var nl = x1 * y2 - y1 * x2; // P numerator-left component.
        var nr = x3 * y4 - y3 * x4; // P numerator-right component.

        var px = (nl * sx3x4 - sx1x2 * nr);
        var py = (nl * sy3y4 - sy1y2 * nr);

        var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

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

      public static (LineIntersectTest Outcome, System.Runtime.Intrinsics.Vector128<double> Intersection) GivenTwoPointsOnEach(System.Runtime.Intrinsics.Vector128<double> v1, System.Runtime.Intrinsics.Vector128<double> v2, System.Runtime.Intrinsics.Vector128<double> v3, System.Runtime.Intrinsics.Vector128<double> v4)
      {
        var (outcome, x, y) = GivenTwoPointsOnEach(v1[0], v1[1], v2[0], v2[1], v3[0], v3[1], v4[0], v4[1]);

        return (outcome, System.Runtime.Intrinsics.Vector128.Create(x, y));
      }

      //    /// <summary>Returns the corresponding y from the specified x, slope and the point.</summary>
      //    /// <param name="x"></param>
      //    /// <param name="slope"></param>
      //    /// <param name="p1"></param>
      //    public static double PointSlopeForm(double x, double slope, System.Numerics.Vector2 p1)
      //      => p1.Y + slope * (x - p1.X);

      /// <summary>
      /// <para>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</para>
      /// </summary>
      /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
      public static double SideTest(double testX, double testY, double x1, double y1, double x2, double y2)
        => (x2 - x1) * (testY - y1) - (y2 - y1) * (testX - x1);

      /// <summary>
      /// <para>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</para>
      /// </summary>
      /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
      public static float SideTest(System.Numerics.Vector2 testV, System.Numerics.Vector2 v1, System.Numerics.Vector2 v2)
        => (v2.X - v1.X) * (testV.Y - v1.Y) - (v2.Y - v1.Y) * (testV.X - v1.X);

      /// <summary>
      /// <para>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</para>
      /// </summary>
      /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
      public static double SideTest(System.Runtime.Intrinsics.Vector128<double> testV, System.Runtime.Intrinsics.Vector128<double> v1, System.Runtime.Intrinsics.Vector128<double> v2)
        => (v2[0] - v1[0]) * (testV[1] - v1[1]) - (v2[1] - v1[1]) * (testV[0] - v1[0]);

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

      public string ToString(string? format, IFormatProvider? formatProvider)
      {
        format ??= "N3";

        return $"{GetType().Name} {{ X1 = {X1.ToString(format, formatProvider)}, Y1 = {Y1.ToString(format, formatProvider)}, X2 = {X2.ToString(format, formatProvider)}, Y2 = {Y2.ToString(format, formatProvider)} }}";
      }

      public override string ToString() => ToString(null, null);
    }
  }
}
