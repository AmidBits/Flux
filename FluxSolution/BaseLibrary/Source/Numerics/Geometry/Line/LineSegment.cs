//namespace Flux.Geometry
//{
//  //[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//  //public readonly record struct Line
//  //{
//  //  private readonly double m_x1;
//  //  private readonly double m_y1;
//  //  private readonly double m_x2;
//  //  private readonly double m_y2;

//  //  public Line(double x1, double y1, double x2, double y2)
//  //  {
//  //    m_x1 = x1;
//  //    m_y1 = y1;
//  //    m_x2 = x2;
//  //    m_y2 = y2;
//  //  }

//  //  public double X1 { get => m_x1; init => m_x1 = value; }
//  //  public double Y1 { get => m_y1; init => m_y1 = value; }
//  //  public double X2 { get => m_x2; init => m_x2 = value; }
//  //  public double Y2 { get => m_y2; init => m_y2 = value; }

//  //  public static (IntersectTestLine Outcome, double X, double Y) GivenTwoPointsOnEachLine(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
//  //  {
//  //    var sx1x2 = x1 - x2;
//  //    var sy1y2 = y1 - y2;
//  //    var sx3x4 = x3 - x4;
//  //    var sy3y4 = y3 - y4;

//  //    var nl = x1 * y2 - y1 * x2; // P numerator-left component.
//  //    var nr = x3 * y4 - y3 * x4; // P numerator-right component.

//  //    var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

//  //    var px = (nl * sx3x4 - sx1x2 * nr);
//  //    var py = (nl * sy3y4 - sy1y2 * nr);

//  //    // if(d == 0)return // coincident or parallel, i.e. NOT intersecting.
//  //    if (d != 0)
//  //    {
//  //      px /= d;
//  //      py /= d;

//  //      return (IntersectTestLine.LinesIntersect, px, py);
//  //    }
//  //    else
//  //      return (IntersectTestLine.NonIntersectingLines, double.NaN, double.NaN);
//  //  }
//  //}

//  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//  public readonly record struct LineSegment
//  {
//    private readonly double m_x1;
//    private readonly double m_y1;
//    private readonly double m_x2;
//    private readonly double m_y2;

//    public LineSegment(double x1, double y1, double x2, double y2)
//    {
//      m_x1 = x1;
//      m_y1 = y1;
//      m_x2 = x2;
//      m_y2 = y2;
//    }

//    public double X1 { get => m_x1; init => m_x1 = value; }
//    public double Y1 { get => m_y1; init => m_y1 = value; }
//    public double X2 { get => m_x2; init => m_x2 = value; }
//    public double Y2 { get => m_y2; init => m_y2 = value; }

//    public LineSlope ToLineSlope()
//      => new(m_x1, m_y1, m_x2, m_y2);

//    #region Static methods
//    public static (double a, double b, double c) GetLineEquationCoefficients(double aX, double aY, double bX, double bY)
//      => (aY - bY, bX - aX, aX * bY - bX * aY);

//    ///// <summary>Returns an intermediary point between the two specified points. 0 equals a, 0.5 equals the midpoint and 1 equals b.</summary>>
//    //public static Numerics.CartesianCoordinate2<double> IntermediaryPoint(Numerics.CartesianCoordinate2<double> a, Numerics.CartesianCoordinate2<double> b, double scalar = 0.5)
//    //  => (a + b) * scalar;

//    //public static (IntersectTestLine Outcome, double X, double Y) GivenTwoPointsOnEachLine(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
//    //{
//    //  var sx1x2 = x1 - x2;
//    //  var sy1y2 = y1 - y2;
//    //  var sx3x4 = x3 - x4;
//    //  var sy3y4 = y3 - y4;

//    //  var nl = x1 * y2 - y1 * x2; // P numerator-left component.
//    //  var nr = x3 * y4 - y3 * x4; // P numerator-right component.

//    //  var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

//    //  var px = (nl * sx3x4 - sx1x2 * nr);
//    //  var py = (nl * sy3y4 - sy1y2 * nr);

//    //  // if(d == 0)return // coincident or parallel, i.e. NOT intersecting.
//    //  if (d != 0)
//    //  {
//    //    px /= d;
//    //    py /= d;

//    //    return (IntersectTestLine.LinesIntersect, px, py);
//    //  }
//    //  else
//    //    return (IntersectTestLine.NonIntersectingLines, double.NaN, double.NaN);
//    //}

//    //public static (IntersectTestLineSegment Outcome, double X, double Y) GivenTwoPointsOnEachLineSegment(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
//    //{
//    //  var sx1x2 = x1 - x2;
//    //  var sx1x3 = x1 - x3;
//    //  var sy1y2 = y1 - y2;
//    //  var sy1y3 = y1 - y3;
//    //  var sx3x4 = x3 - x4;
//    //  var sy3y4 = y3 - y4;

//    //  var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

//    //  var t = sx1x3 * sy3y4 - sy1y3 * sx3x4;
//    //  var u = sx1x2 * sy1y3 - sy1y2 * sx1x3;

//    //  if (d != 0)
//    //  {
//    //    t /= d;
//    //    u /= d;

//    //    var ptx = x1 + t * (x2 - x1);
//    //    var pty = y1 + t * (y2 - y1);
//    //    var pux = x3 + u * (x4 - x3);
//    //    var puy = y3 + u * (y4 - y3);

//    //    if (t >= 0 && t <= 1)
//    //      return (IntersectTestLineSegment.IntersectWithinFirst, ptx, pty);
//    //    else if (u >= 0 && u <= 1)
//    //      return (IntersectTestLineSegment.IntersectWithinSecond, pux, puy);
//    //    else // Not intersecting.
//    //      return (IntersectTestLineSegment.Unknown, double.NaN, double.NaN);
//    //  }
//    //  else
//    //  {
//    //    if (t == 0 || u == 0) // Coincident lines.
//    //      return (IntersectTestLineSegment.Coincident, double.NaN, double.NaN);
//    //    else // Parallel lines.
//    //      return (IntersectTestLineSegment.Parallel, double.NaN, double.NaN);
//    //  }
//    //}

//    /// <summary>Returns the sign indicating whether the test point is Left|On|Right of the (infinite) line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
//    public static LineTestResult IntersectionTest(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
//    {
//      var p13x = x1 - x3;
//      var p13y = y1 - y3;
//      var p21x = x2 - x1;
//      var p21y = y2 - y1;
//      var p43x = x4 - x3;
//      var p43y = y4 - y3;

//      var ab = p43y * p21x - p43x * p21y;

//      var a = p43x * p13y - p43y * p13x;
//      var b = p21x * p13y - p21y * p13x;

//      if (ab != 0)
//      {
//        a /= ab;
//        b /= ab;

//        if (a >= 0 && a <= 1 && b >= 0 && b <= 1) // Intersecting.
//          return new LineTestResult(LineTestOutcome.LinesIntersecting, x1 + a * (x2 - x1), y1 + a * (y2 - y1));
//        else // Not intersecting.
//          return new LineTestResult(LineTestOutcome.Unknown);
//      }
//      else
//      {
//        if (a == 0 || b == 0) // Coincident.
//          return new LineTestResult(LineTestOutcome.CoincidentLines);
//        else // Parallel.
//          return new LineTestResult(LineTestOutcome.ParallelLines);
//      }
//    }

//    ///// <summary>Returns the sign indicating whether the test point is Left|On|Right of the (infinite) line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
//    //public static LineTestResult IntersectionTest(CartesianCoordinate2<double> a1, CartesianCoordinate2<double> a2, CartesianCoordinate2<double> b1, CartesianCoordinate2<double> b2)
//    //  => IntersectionTest(a1.X, a1.Y, a2.X, a2.Y, b1.X, b1.Y, b2.X, b2.Y);

//    /// <summary>Returns the sign indicating whether the test point is Left|On|Right of the (infinite) line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
//    public static LineTestResult IntersectionTest(System.Numerics.Vector2 a1, System.Numerics.Vector2 a2, System.Numerics.Vector2 b1, System.Numerics.Vector2 b2)
//      => IntersectionTest(a1.X, a1.Y, a2.X, a2.Y, b1.X, b1.Y, b2.X, b2.Y);

//    /// <summary>Returns the corresponding y from the specified x, slope and the point.</summary>
//    /// <param name="x"></param>
//    /// <param name="slope"></param>
//    /// <param name="p1"></param>
//    public static double PointSlopeForm(double x, double slope, System.Numerics.Vector2 p1)
//      => p1.Y + slope * (x - p1.X);

//    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
//    /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
//    public static double SideTestLine(double x, double y, double x1, double y1, double x2, double y2)
//      => (x2 - x1) * (y - y1) - (y2 - y1) * (x - x1);

//    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
//    public static int SideTestLine(System.Numerics.Vector2 test, System.Numerics.Vector2 a, System.Numerics.Vector2 b)
//      => System.Math.Sign(SideTestLine(test.X, test.Y, a.X, a.Y, b.X, b.Y));

//    /// <summary>Returns the corresponding y from the specified x and the two specified points, from which the slope and absolute x are derived and returned as out arguments.</summary>
//    /// <param name="x"></param>
//    /// <param name="p1"></param>
//    /// <param name="p2"></param>
//    /// <param name="slope"></param>
//    /// <param name="absoluteX"></param>
//    public static float TwoPointForm(float x, in System.Numerics.Vector2 p1, in System.Numerics.Vector2 p2, out float slope, out float absoluteX)
//      => p1.Y + (slope = TwoPointSlope(p1, p2)) * (absoluteX = (x - p1.X));
//    /// <summary>Returns the slope of the line/edge, between point1 and point2. THe slope is the change in y divided by the corresponding change in x. Basically slope represents vertial change divided by horizontal change, or rise divided by run.</summary>
//    public static float TwoPointSlope(in System.Numerics.Vector2 p0, in System.Numerics.Vector2 p1)
//      => (p0.X - p1.X) / (p0.Y - p1.Y);
//    #endregion Static methods

//    // https://math.stackexchange.com/questions/637922/how-can-i-find-coefficients-a-b-c-given-two-points
//    // https://www.mathsisfun.com/algebra/line-equation-2points.html
//    // https://keisan.casio.com/exec/system/1223508685
//  }
//}
