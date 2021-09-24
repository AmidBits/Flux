namespace Flux
{
  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  public struct CartesianCoordinate3
    : System.IEquatable<CartesianCoordinate3>
  {
    public static readonly CartesianCoordinate3 Zero;

    private readonly double m_x;
    private readonly double m_y;
    private readonly double m_z;

    public CartesianCoordinate3(double x, double y, double z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }

    public double X
      => m_x;
    public double Y
      => m_y;
    public double Z
      => m_z;

    /// <summary>Returns the angle to the 3D X-axis.</summary>
    public double AngleToAxisX()
      => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_y, 2) + System.Math.Pow(m_z, 2)), m_x);
    /// <summary>Returns the angle to the 3D Y-axis.</summary>
    public double AngleToAxisY()
      => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_z, 2) + System.Math.Pow(m_x, 2)), m_y);
    /// <summary>Returns the angle to the 3D Z-axis.</summary>
    public double AngleToAxisZ()
      => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_x, 2) + System.Math.Pow(m_y, 2)), m_z);

    /// <summary>Compute the Chebyshev length of the vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public double ChebyshevLength(double edgeLength = 1)
      => Maths.Max(System.Math.Abs(m_x / edgeLength), System.Math.Abs(m_y / edgeLength), System.Math.Abs(m_z / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public double EuclideanLength()
      => System.Math.Sqrt(EuclideanLengthSquared());
    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    public double EuclideanLengthSquared()
      => System.Math.Pow(m_x, 2) + System.Math.Pow(m_y, 2) + System.Math.Pow(m_z, 2);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public double ManhattanLength(double edgeLength = 1)
      => System.Math.Abs(m_x / edgeLength) + System.Math.Abs(m_y / edgeLength) + System.Math.Abs(m_z / edgeLength);

    public CartesianCoordinate3 Normalized()
      => EuclideanLength() is var m && m != 0 ? this / m : this;

    /// <summary>Returns the octant of the 3D vector based on the specified axis vector. This is the more traditional octant.</summary>
    /// <returns>The octant identifer in the range 0-7, i.e. one of the eight octants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
    public int OctantNumber(CartesianCoordinate3 center)
      => m_z >= center.m_z ? (m_y >= center.m_y ? (m_x >= center.m_x ? 0 : 1) : (m_x >= center.m_x ? 3 : 2)) : (m_y >= center.m_y ? (m_x >= center.m_x ? 7 : 6) : (m_x >= center.m_x ? 4 : 5));
    /// <summary>Returns the orthant (octant) of the 3D vector using binary numbering: X = 1, Y = 2 and Z = 4, which are then added up, based on the sign of the respective component.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public int OrthantNumber(CartesianCoordinate3 center)
      => (m_x >= center.m_x ? 0 : 1) + (m_y >= center.m_y ? 0 : 2) + (m_z >= center.m_z ? 0 : 4);

    /// <summary>Always works if the input is non-zero. Does not require the input to be normalized, and does not normalize the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    public CartesianCoordinate3 Orthogonal()
      => System.Math.Abs(m_x) > System.Math.Abs(m_z) ? new CartesianCoordinate3(-m_y, m_x, 0) : new CartesianCoordinate3(0, -m_x, m_y);

    public CylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(System.Math.Sqrt(m_x * m_x + m_y * m_y), (System.Math.Atan2(m_y, m_x) + Maths.PiX2) % Maths.PiX2, m_z);
    public PolarCoordinate ToPolarCoordinate()
      => new PolarCoordinate(System.Math.Sqrt(m_x * m_x + m_y * m_y), System.Math.Atan2(m_y, m_x));
    public SphericalCoordinate ToSphericalCoordinate()
    {
      var x2y2 = m_x * m_x + m_y * m_y;
      return new SphericalCoordinate(System.Math.Sqrt(x2y2 + m_z * m_z), System.Math.Atan2(System.Math.Sqrt(x2y2), m_z) + System.Math.PI, System.Math.Atan2(m_y, m_x) + System.Math.PI);
    }

    #region Static methods
    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleBetween(CartesianCoordinate3 a, CartesianCoordinate3 b)
      => System.Math.Acos(System.Math.Clamp(DotProduct(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -1, 1));

    /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
    public static CartesianCoordinate3 CrossProduct(CartesianCoordinate3 a, CartesianCoordinate3 b)
      => new CartesianCoordinate3(a.m_y * b.m_z - a.m_z * b.m_y, a.m_z * b.m_x - a.m_x * b.m_z, a.m_x * b.m_y - a.m_y * b.m_x);

    /// <summary>Returns the dot product of two 3D vectors.</summary>
    public static double DotProduct(CartesianCoordinate3 a, CartesianCoordinate3 b)
      => a.m_x * b.m_x + a.m_y * b.m_y + a.m_z * b.m_z;

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(CartesianCoordinate3 source, CartesianCoordinate3 target, double edgeLength = 1)
      => (target - source).ChebyshevLength(edgeLength);

    /// <summary>Compute the Euclidean distance from vector a to vector b.</summary>
    public static double EuclideanDistance(CartesianCoordinate3 source, CartesianCoordinate3 target)
      => (target - source).EuclideanLength();

    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static CartesianCoordinate3 FromRandomAbsolute(int toExclusiveX, int toExclusiveY, int toExclusiveZ)
      => new CartesianCoordinate3(Randomization.NumberGenerator.Crypto.Next(toExclusiveX), Randomization.NumberGenerator.Crypto.Next(toExclusiveY), Randomization.NumberGenerator.Crypto.Next(toExclusiveZ));
    /// <summary>Create a new random vector in the range (-toExlusive, toExclusive) using the crypto-grade rng.</summary>
    public static CartesianCoordinate3 FromRandomCenterZero(int toExclusiveX, int toExclusiveY, int toExclusiveZ)
      => new CartesianCoordinate3(Randomization.NumberGenerator.Crypto.Next(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Randomization.NumberGenerator.Crypto.Next(toExclusiveY * 2 - 1) - (toExclusiveY - 1), Randomization.NumberGenerator.Crypto.Next(toExclusiveZ * 2 - 1) - (toExclusiveZ - 1));

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static CartesianCoordinate3 InterpolateCosine(CartesianCoordinate3 p1, CartesianCoordinate3 p2, double mu)
      => new CartesianCoordinate3(Maths.InterpolateCosine(p1.m_x, p2.m_x, mu), Maths.InterpolateCosine(p1.m_y, p2.m_y, mu), Maths.InterpolateCosine(p1.Z, p2.Z, mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static CartesianCoordinate3 InterpolateCubic(CartesianCoordinate3 p0, CartesianCoordinate3 p1, CartesianCoordinate3 p2, CartesianCoordinate3 p3, double mu)
      => new CartesianCoordinate3(Maths.InterpolateCubic(p0.m_x, p1.m_x, p2.m_x, p3.m_x, mu), Maths.InterpolateCubic(p0.m_y, p1.m_y, p2.m_y, p3.m_y, mu), Maths.InterpolateCubic(p0.m_z, p1.m_z, p2.m_z, p3.m_z, mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static CartesianCoordinate3 InterpolateHermite2(CartesianCoordinate3 p0, CartesianCoordinate3 p1, CartesianCoordinate3 p2, CartesianCoordinate3 p3, double mu, double tension, double bias)
      => new CartesianCoordinate3(Maths.InterpolateHermite(p0.m_x, p1.m_x, p2.m_x, p3.m_x, mu, tension, bias), Maths.InterpolateHermite(p0.m_y, p1.m_y, p2.m_y, p3.m_y, mu, tension, bias), Maths.InterpolateHermite(p0.m_z, p1.m_z, p2.m_z, p3.m_z, mu, tension, bias));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static CartesianCoordinate3 InterpolateLinear(CartesianCoordinate3 p1, CartesianCoordinate3 p2, double mu)
      => new CartesianCoordinate3(Maths.InterpolateLinear(p1.m_x, p2.m_x, mu), Maths.InterpolateLinear(p1.m_y, p2.m_y, mu), Maths.InterpolateLinear(p1.m_z, p2.m_z, mu));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static CartesianCoordinate3 Lerp(CartesianCoordinate3 source, CartesianCoordinate3 target, double mu)
    {
      var imu = 1 - mu;

      return new CartesianCoordinate3(source.m_x * imu + target.m_x * mu, source.m_y * imu + target.m_y * mu, source.m_z * imu + target.m_z * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(CartesianCoordinate3 source, CartesianCoordinate3 target, double edgeLength = 1)
      => (target - source).ManhattanLength(edgeLength);

    public static CartesianCoordinate3 Nlerp(CartesianCoordinate3 source, CartesianCoordinate3 target, double mu)
      => Lerp(source, target, mu).Normalized();

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(CartesianCoordinate3 a, CartesianCoordinate3 b, CartesianCoordinate3 c)
      => DotProduct(a, CrossProduct(b, c));

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static CartesianCoordinate3 Slerp(CartesianCoordinate3 source, CartesianCoordinate3 target, double mu)
    {
      var dp = System.Math.Clamp(DotProduct(source, target), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dp) * mu; // Angle between start and desired.
      var cos = System.Math.Cos(theta);
      var sin = System.Math.Sin(theta);

      return new CartesianCoordinate3(source.m_x * cos + (target.m_x - source.m_x) * dp * sin, source.m_y * cos + (target.m_y - source.m_y) * dp * sin, source.m_z * cos + (target.m_z - source.m_z) * dp * sin);
    }

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static CartesianCoordinate3 VectorTripleProduct(CartesianCoordinate3 a, CartesianCoordinate3 b, CartesianCoordinate3 c)
      => CrossProduct(a, CrossProduct(b, c));
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator CartesianCoordinate3(System.ValueTuple<double, double, double> xyz)
      => new CartesianCoordinate3(xyz.Item1, xyz.Item2, xyz.Item3);

    public static bool operator ==(CartesianCoordinate3 a, CartesianCoordinate3 b)
      => a.Equals(b);
    public static bool operator !=(CartesianCoordinate3 a, CartesianCoordinate3 b)
      => !a.Equals(b);

    public static CartesianCoordinate3 operator -(CartesianCoordinate3 cc)
      => new CartesianCoordinate3(-cc.X, -cc.Y, -cc.Z);

    public static CartesianCoordinate3 operator --(CartesianCoordinate3 cc)
      => cc - 1;
    public static CartesianCoordinate3 operator ++(CartesianCoordinate3 cc)
      => cc + 1;

    public static CartesianCoordinate3 operator +(CartesianCoordinate3 cc1, CartesianCoordinate3 cc2)
      => new CartesianCoordinate3(cc1.X + cc2.X, cc1.Y + cc2.Y, cc1.Z + cc2.Z);
    public static CartesianCoordinate3 operator +(CartesianCoordinate3 cc, double scalar)
      => new CartesianCoordinate3(cc.X + scalar, cc.Y + scalar, cc.Z + scalar);
    public static CartesianCoordinate3 operator +(double scalar, CartesianCoordinate3 cc)
      => new CartesianCoordinate3(scalar + cc.X, scalar + cc.Y, scalar + cc.Z);

    public static CartesianCoordinate3 operator -(CartesianCoordinate3 cc1, CartesianCoordinate3 cc2)
      => new CartesianCoordinate3(cc1.X - cc2.X, cc1.Y - cc2.Y, cc1.Z - cc2.Z);
    public static CartesianCoordinate3 operator -(CartesianCoordinate3 cc, double scalar)
      => new CartesianCoordinate3(cc.X - scalar, cc.Y - scalar, cc.Z - scalar);
    public static CartesianCoordinate3 operator -(double scalar, CartesianCoordinate3 cc)
      => new CartesianCoordinate3(scalar - cc.X, scalar - cc.Y, scalar - cc.Z);

    public static CartesianCoordinate3 operator *(CartesianCoordinate3 cc1, CartesianCoordinate3 cc2)
      => new CartesianCoordinate3(cc1.X * cc2.X, cc1.Y * cc2.Y, cc1.Z * cc2.Z);
    public static CartesianCoordinate3 operator *(CartesianCoordinate3 cc, double scalar)
      => new CartesianCoordinate3(cc.X * scalar, cc.Y * scalar, cc.Z * scalar);
    public static CartesianCoordinate3 operator *(double scalar, CartesianCoordinate3 cc)
      => new CartesianCoordinate3(scalar * cc.X, scalar * cc.Y, scalar * cc.Z);

    public static CartesianCoordinate3 operator /(CartesianCoordinate3 cc1, CartesianCoordinate3 cc2)
      => new CartesianCoordinate3(cc1.X / cc2.X, cc1.Y / cc2.Y, cc1.Z / cc2.Z);
    public static CartesianCoordinate3 operator /(CartesianCoordinate3 cc, double scalar)
      => new CartesianCoordinate3(cc.X / scalar, cc.Y / scalar, cc.Z / scalar);
    public static CartesianCoordinate3 operator /(double scalar, CartesianCoordinate3 cc)
      => new CartesianCoordinate3(scalar / cc.X, scalar / cc.Y, scalar / cc.Z);

    public static CartesianCoordinate3 operator %(CartesianCoordinate3 cc1, CartesianCoordinate3 cc2)
      => new CartesianCoordinate3(cc1.X % cc2.X, cc1.Y % cc2.Y, cc1.Z % cc2.Z);
    public static CartesianCoordinate3 operator %(CartesianCoordinate3 cc, double scalar)
      => new CartesianCoordinate3(cc.X % scalar, cc.Y % scalar, cc.Z % scalar);
    public static CartesianCoordinate3 operator %(double scalar, CartesianCoordinate3 cc)
      => new CartesianCoordinate3(scalar % cc.X, scalar % cc.Y, scalar % cc.Z);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(CartesianCoordinate3 other)
      => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CartesianCoordinate3 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y, m_z);
    public override string ToString()
      => $"<{GetType().Name}: {m_x} x, {m_y} y, {m_z} z, ({EuclideanLength()} length)>";
    #endregion Object overrides
  }
}
