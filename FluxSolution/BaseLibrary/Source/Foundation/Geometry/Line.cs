namespace Flux.Geometry
{
  public struct Line
    : System.IEquatable<Line>
  {
    public static readonly Line Empty;

    public double X1;
    public double Y1;
    public double X2;
    public double Y2;

    public Line(double x1, double y1, double x2, double y2)
    {
      X1 = x1;
      Y1 = y1;
      X2 = x2;
      Y2 = y2;
    }

    #region Static methods
    public static (double a, double b, double c) GetLineEquationCoefficients(double aX, double aY, double bX, double bY)
      => (aY - bY, bX - aX, aX * bY - bX * aY);

    /// <summary>Returns an intermediary point between the two specified points. 0 equals a, 0.5 equals the midpoint and 1 equals b.</summary>>
    public static CartesianCoordinate2 IntermediaryPoint(CartesianCoordinate2 a, CartesianCoordinate2 b, double scalar = 0.5)
      => (a + b) * scalar;

    /// <summary>Returns the sign indicating whether the test point is Left|On|Right of the (infinite) line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static LineTestResult IntersectionTest(double aX1, double aY1, double aX2, double aY2, double bX1, double bY1, double bX2, double bY2)
    {
      var p13x = aX1 - bX1;
      var p13y = aY1 - bY1;
      var p21x = aX2 - aX1;
      var p21y = aY2 - aY1;
      var p43x = bX2 - bX1;
      var p43y = bY2 - bY1;

      var a = p43x * p13y - p43y * p13x;
      var b = p21x * p13y - p21y * p13x;
      var ab = p43y * p21x - p43x * p21y;

      if (ab != 0)
      {
        a /= ab;
        b /= ab;

        if (a >= 0 && a <= 1 && b >= 0 && b <= 1) // Intersecting.
          return new LineTestResult(LineTestOutcome.LinesIntersecting, aX1 + a * (aX2 - aX1), aY1 + a * (aY2 - aY1));
        else // Not intersecting.
          return new LineTestResult(LineTestOutcome.Unknown);
      }
      else
      {
        if (a == 0 || b == 0) // Coincident.
          return new LineTestResult(LineTestOutcome.CoincidentLines);
        else // Parallel.
          return new LineTestResult(LineTestOutcome.ParallelLines);
      }
    }
    /// <summary>Returns the sign indicating whether the test point is Left|On|Right of the (infinite) line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static LineTestResult IntersectionTest(CartesianCoordinate2 a1, CartesianCoordinate2 a2, CartesianCoordinate2 b1, CartesianCoordinate2 b2)
      => IntersectionTest(a1.X, a1.Y, a2.X, a2.Y, b1.X, b1.Y, b2.X, b2.Y);
    /// <summary>Returns the sign indicating whether the test point is Left|On|Right of the (infinite) line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static LineTestResult IntersectionTest(System.Numerics.Vector2 a1, System.Numerics.Vector2 a2, System.Numerics.Vector2 b1, System.Numerics.Vector2 b2)
      => IntersectionTest(a1.X, a1.Y, a2.X, a2.Y, b1.X, b1.Y, b2.X, b2.Y);

    /// <summary>Returns the corresponding y from the specified x, slope and the point.</summary>
    /// <param name="x"></param>
    /// <param name="slope"></param>
    /// <param name="p1"></param>
    public static double PointSlopeForm(double x, double slope, CartesianCoordinate2 p1)
      => p1.Y + slope * (x - p1.X);

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static int SideTest(System.Numerics.Vector2 test, System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => System.Math.Sign((b.X - a.X) * (test.Y - a.Y) - (test.X - a.X) * (b.Y - a.Y));

    /// <summary>Returns the corresponding y from the specified x and the two specified points, from which the slope and absolute x are derived and returned as out arguments.</summary>
    /// <param name="x"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="slope"></param>
    /// <param name="absoluteX"></param>
    public static float TwoPointForm(float x, in System.Numerics.Vector2 p1, in System.Numerics.Vector2 p2, out float slope, out float absoluteX)
      => p1.Y + (slope = TwoPointSlope(p1, p2)) * (absoluteX = (x - p1.X));
    /// <summary>Returns the slope of the line/edge, between point1 and point2. THe slope is the change in y divided by the corresponding change in x. Basically slope represents vertial change divided by horizontal change, or rise divided by run.</summary>
    public static float TwoPointSlope(in System.Numerics.Vector2 p0, in System.Numerics.Vector2 p1)
      => (p0.X - p1.X) / (p0.Y - p1.Y);
    #endregion Static methods

    // https://math.stackexchange.com/questions/637922/how-can-i-find-coefficients-a-b-c-given-two-points
    // https://www.mathsisfun.com/algebra/line-equation-2points.html
    // https://keisan.casio.com/exec/system/1223508685

    #region Overloaded operators
    public static bool operator ==(Line a, Line b)
      => a.Equals(b);
    public static bool operator !=(Line a, Line b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Line other)
      => X1 == other.X1 && Y1 == other.Y1 && X2 == other.X2 && Y2 == other.Y2;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Line o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X1, Y1, X2, Y2);
    public override string? ToString()
      => $"<{nameof(Line)}: {X1}, {Y1}, {X2}, {Y2}>";
    #endregion Object overrides
  }
}
