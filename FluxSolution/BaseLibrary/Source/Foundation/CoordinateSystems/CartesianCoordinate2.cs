namespace Flux
{
  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  public struct CartesianCoordinate2
    : System.IEquatable<CartesianCoordinate2>
  {
    public readonly static CartesianCoordinate2 Zero;

    private readonly double m_x;
    private readonly double m_y;

    public CartesianCoordinate2(double x, double y)
    {
      m_x = x;
      m_y = y;
    }

    public double X { get => m_x; }
    public double Y { get => m_y; }

    /// <summary>Returns the angle to the 2D X-axis.</summary>
    public double AngleToAxisX()
      => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_y, 2)), m_x);
    /// <summary>Returns the angle to the 2D Y-axis.</summary>
    public double AngleToAxisY()
      => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_x, 2)), m_y);

    /// <summary>Compute the Chebyshev length of the vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public double ChebyshevLength(double edgeLength = 1)
      => System.Math.Max(System.Math.Abs(X / edgeLength), System.Math.Abs(Y / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public double EuclideanLength()
      => System.Math.Sqrt(EuclideanLengthSquared());
    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    public double EuclideanLengthSquared()
      => X * X + Y * Y;

    /// <summary>Returns the X-slope of the line to the point (x, y).</summary>
    public double LineSlopeX()
      => System.Math.CopySign(X / Y, X);
    /// <summary>Returns the Y-slope of the line to the point (x, y).</summary>
    public double LineSlopeY()
      => System.Math.CopySign(Y / X, Y);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public double ManhattanLength(double edgeLength = 1)
      => System.Math.Abs(X / edgeLength) + System.Math.Abs(Y / edgeLength);

    public CartesianCoordinate2 Normalized()
      => EuclideanLength() is var m && m != 0 ? this / m : this;

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public CartesianCoordinate2 PerpendicularCcw()
      => new CartesianCoordinate2(-Y, X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public CartesianCoordinate2 PerpendicularCw()
      => new CartesianCoordinate2(Y, -X);

    /// <summary>Returns the perpendicular distance from the 2D point (x, y) to the to the 2D line (x1, y1) to (x2, y2).</summary>
    public double PerpendicularDistanceTo(CartesianCoordinate2 a, CartesianCoordinate2 b)
    {
      var cc21 = b - a;

      return (cc21 * (this - a)).EuclideanLength() / cc21.EuclideanLength();
    }

    /// <summary>Find foot of perpendicular from a point in 2D a plane to a line equation (ax+by+c=0).</summary>
    /// <see cref="https://www.geeksforgeeks.org/find-foot-of-perpendicular-from-a-point-in-2-d-plane-to-a-line/"/>
    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    /// <param name="source">A given point.</param>
    public CartesianCoordinate2 PerpendicularFoot(double a, double b, double c)
    {
      var m = -1 * (a * m_x + b * m_y + c) / (a * a + b * b);

      return new CartesianCoordinate2(m * (a + m_x), m * (b + m_y));
    }

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public int SideTest(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => System.Math.Sign((m_x - b.m_x) * (a.m_y - b.m_y) - (m_y - b.m_y) * (a.m_x - b.m_x));

    /// <summary>Convert the 2D vector to a quadrant based on some specified center vector.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public int QuadrantNumber(CartesianCoordinate2 center)
      => X >= center.X ? (Y >= center.Y ? 0 : 3) : (Y >= center.Y ? 1 : 2);
    /// <summary>Convert the 2D vector to a quadrant.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public int QuadrantNumber()
      => QuadrantNumber(Zero);

    public Quantity.Angle ToRotationAngle()
      => new Quantity.Angle(ConvertToRotationAngle(m_x, m_y));
    public Quantity.Angle ToRotationAngleEx()
      => new Quantity.Angle(ConvertToRotationAngleEx(m_x, m_y));
    public PolarCoordinate ToPolarCoordinate()
      => new PolarCoordinate(System.Math.Sqrt(m_x * m_x + m_y * m_y), System.Math.Atan2(m_y, m_x));

    #region Static methods
    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleBetween(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => System.Math.Acos(System.Math.Clamp(DotProduct(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -1, 1));

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Maths.PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertToRotationAngleEx(double x, double y)
      => Maths.PiX2 - ConvertToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.

    /// <summary>Returns the cross product of two 2D vectors.</summary>
    /// <remarks>For 2D vectors, this is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static double CrossProduct(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => a.X * b.Y - a.Y * b.X;

    /// <summary>Returns the dot product of two 2D vectors.</summary>
    public static double DotProduct(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => a.X * b.X + a.Y * b.Y;

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(CartesianCoordinate2 source, CartesianCoordinate2 target, double edgeLength = 1)
      => (target - source).ChebyshevLength(edgeLength);

    /// <summary>Compute the Euclidean distance from vector a to vector b.</summary>
    public static double EuclideanDistance(CartesianCoordinate2 source, CartesianCoordinate2 target)
      => (target - source).EuclideanLength();

    /// <summary>Create a new random vector in the range [(0, 0), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static CartesianCoordinate2 FromRandomAbsolute(double toExclusiveX, double toExclusiveY)
      => new CartesianCoordinate2(Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveX), Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveY));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static CartesianCoordinate2 FromRandomCenterZero(double toExclusiveX, double toExclusiveY)
      => new CartesianCoordinate2(Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveY * 2 - 1) - (toExclusiveY - 1));

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static CartesianCoordinate2 InterpolateCosine(CartesianCoordinate2 y1, CartesianCoordinate2 y2, double mu)
      => new CartesianCoordinate2(Maths.InterpolateCosine(y1.m_x, y2.m_x, mu), Maths.InterpolateCosine(y1.m_y, y2.m_y, mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static CartesianCoordinate2 InterpolateCubic(CartesianCoordinate2 y0, CartesianCoordinate2 y1, CartesianCoordinate2 y2, CartesianCoordinate2 y3, double mu)
      => new CartesianCoordinate2(Maths.InterpolateCubic(y0.m_x, y1.m_x, y2.m_x, y3.m_x, mu), Maths.InterpolateCubic(y0.m_y, y1.m_y, y2.m_y, y3.m_y, mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static CartesianCoordinate2 InterpolateHermite2(CartesianCoordinate2 y0, CartesianCoordinate2 y1, CartesianCoordinate2 y2, CartesianCoordinate2 y3, double mu, double tension, double bias)
      => new CartesianCoordinate2(Maths.InterpolateHermite(y0.m_x, y1.m_x, y2.m_x, y3.m_x, mu, tension, bias), Maths.InterpolateHermite(y0.m_y, y1.m_y, y2.m_y, y3.m_y, mu, tension, bias));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static CartesianCoordinate2 InterpolateLinear(CartesianCoordinate2 y1, CartesianCoordinate2 y2, double mu)
      => new CartesianCoordinate2(Maths.InterpolateLinear(y1.m_x, y2.m_x, mu), Maths.InterpolateLinear(y1.m_y, y2.m_y, mu));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static CartesianCoordinate2 Lerp(CartesianCoordinate2 source, CartesianCoordinate2 target, double mu)
    {
      var imu = 1 - mu;

      return new CartesianCoordinate2(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(CartesianCoordinate2 source, CartesianCoordinate2 target, double edgeLength = 1)
      => (target - source).ManhattanLength(edgeLength);

    public static CartesianCoordinate2 Nlerp(CartesianCoordinate2 source, CartesianCoordinate2 target, double mu)
      => Lerp(source, target, mu).Normalized();

    /// <summary>Slerp is a sherical linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0). Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static CartesianCoordinate2 Slerp(CartesianCoordinate2 source, CartesianCoordinate2 target, double mu)
    {
      var dp = System.Math.Clamp(DotProduct(source, target), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dp) * mu; // Angle between start and desired.
      var cos = System.Math.Cos(theta);
      var sin = System.Math.Sin(theta);

      return new CartesianCoordinate2(source.m_x * cos + (target.m_x - source.m_x) * dp * sin, source.m_y * cos + (target.m_y - source.m_y) * dp * sin);
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator CartesianCoordinate2(System.ValueTuple<double, double> xy)
      => new CartesianCoordinate2(xy.Item1, xy.Item2);

    public static bool operator ==(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => a.Equals(b);
    public static bool operator !=(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => !a.Equals(b);

    public static CartesianCoordinate2 operator -(CartesianCoordinate2 cc)
      => new CartesianCoordinate2(-cc.X, -cc.Y);

    public static CartesianCoordinate2 operator --(CartesianCoordinate2 cc)
      => cc - 1;
    public static CartesianCoordinate2 operator ++(CartesianCoordinate2 cc)
      => cc + 1;

    public static CartesianCoordinate2 operator +(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2)
      => new CartesianCoordinate2(cc1.X + cc2.X, cc1.Y + cc2.Y);
    public static CartesianCoordinate2 operator +(CartesianCoordinate2 cc, double scalar)
      => new CartesianCoordinate2(cc.X + scalar, cc.Y + scalar);
    public static CartesianCoordinate2 operator +(double scalar, CartesianCoordinate2 cc)
      => new CartesianCoordinate2(scalar + cc.X, scalar + cc.Y);

    public static CartesianCoordinate2 operator -(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2)
      => new CartesianCoordinate2(cc1.X - cc2.X, cc1.Y - cc2.Y);
    public static CartesianCoordinate2 operator -(CartesianCoordinate2 cc, double scalar)
      => new CartesianCoordinate2(cc.X - scalar, cc.Y - scalar);
    public static CartesianCoordinate2 operator -(double scalar, CartesianCoordinate2 cc)
      => new CartesianCoordinate2(scalar - cc.X, scalar - cc.Y);

    public static CartesianCoordinate2 operator *(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2)
      => new CartesianCoordinate2(cc1.X * cc2.X, cc1.Y * cc2.Y);
    public static CartesianCoordinate2 operator *(CartesianCoordinate2 cc, double scalar)
      => new CartesianCoordinate2(cc.X * scalar, cc.Y * scalar);
    public static CartesianCoordinate2 operator *(double scalar, CartesianCoordinate2 cc)
      => new CartesianCoordinate2(scalar * cc.X, scalar * cc.Y);

    public static CartesianCoordinate2 operator /(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2)
      => new CartesianCoordinate2(cc1.X / cc2.X, cc1.Y / cc2.Y);
    public static CartesianCoordinate2 operator /(CartesianCoordinate2 cc, double scalar)
      => new CartesianCoordinate2(cc.X / scalar, cc.Y / scalar);
    public static CartesianCoordinate2 operator /(double scalar, CartesianCoordinate2 cc)
      => new CartesianCoordinate2(scalar / cc.X, scalar / cc.Y);

    public static CartesianCoordinate2 operator %(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2)
      => new CartesianCoordinate2(cc1.X % cc2.X, cc1.Y % cc2.Y);
    public static CartesianCoordinate2 operator %(CartesianCoordinate2 cc, double scalar)
      => new CartesianCoordinate2(cc.X % scalar, cc.Y % scalar);
    public static CartesianCoordinate2 operator %(double scalar, CartesianCoordinate2 cc)
      => new CartesianCoordinate2(scalar % cc.X, scalar % cc.Y);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(CartesianCoordinate2 other)
      => m_x == other.m_x && m_y == other.m_y;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CartesianCoordinate2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y);
    public override string ToString()
      => $"<{GetType().Name}: {m_x} x, {m_y} y, ({EuclideanLength()} length)>";
    #endregion Object overrides
  }
}
