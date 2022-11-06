using Flux.Hashing;
using System.Runtime.InteropServices;

namespace Flux
{
  /// <summary>A cartesian coordinate using integers.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct Point2
    : System.IComparable<Point2>, System.IEquatable<Point2>
#if NET7_0_OR_GREATER
    , IPoint2<int>
#else
    , IPoint2
#endif
  {
    /// <summary>Returns the vector (0,0).</summary>
    public static readonly Point2 Zero;

    private readonly int m_x;
    private readonly int m_y;

    public Point2(int x, int y)
    {
      m_x = x;
      m_y = y;
    }
    public Point2(int value)
      : this(value, value)
    { }
    public Point2(int[] array, int startIndex)
    {
      if (array is null) throw new System.ArgumentNullException(nameof(array));

      if (array.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(array));
      if (startIndex + 2 >= array.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));

      m_x = array[startIndex++];
      m_y = array[startIndex];
    }

    [System.Diagnostics.Contracts.Pure] public int X { get => m_x; init => m_x = value; }
    [System.Diagnostics.Contracts.Pure] public int Y { get => m_y; init => m_y = value; }

    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    [System.Diagnostics.Contracts.Pure]
    public int ChebyshevLength(int edgeLength = 1)
      => System.Math.Max(System.Math.Abs(m_x / edgeLength), System.Math.Abs(m_y / edgeLength));

//#if NET7_0_OR_GREATER
//    public IPoint2<int> Create(int x, int y)
//      => new Point2(x, y);
//#else
//    public IPoint2 Create(int x, int y)
//      => new Point2(x, y);
//#endif

    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    [System.Diagnostics.Contracts.Pure]
    public double EuclideanLength()
      => System.Math.Sqrt(EuclideanLengthSquared());
    /// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    [System.Diagnostics.Contracts.Pure]
    public double EuclideanLengthSquared()
      => m_x * m_x + m_y * m_y;

    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    [System.Diagnostics.Contracts.Pure]
    public int ManhattanLength(int edgeLength = 1)
      => System.Math.Abs(m_x / edgeLength) + System.Math.Abs(m_y / edgeLength);

    [System.Diagnostics.Contracts.Pure]
    public Point2 Normalized()
      => EuclideanLength() is var m && m != 0 ? this / m : this;

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    [System.Diagnostics.Contracts.Pure]
    public int OrthantNumber(Point2 center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => m_y >= center.m_y ? (m_x >= center.m_x ? 0 : 1) : (m_x >= center.m_x ? 3 : 2),
        OrthantNumbering.BinaryNegativeAs1 => (m_x >= center.m_x ? 0 : 1) + (m_y >= center.m_y ? 0 : 2),
        OrthantNumbering.BinaryPositiveAs1 => (m_x < center.m_x ? 0 : 1) + (m_y < center.m_y ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Point2 PerpendicularCcw()
      => new(-m_y, m_x);

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Point2 PerpendicularCw()
      => new(m_y, -m_x);

    #region To..

    /// <summary>Converts the <see cref="Point2"/> to a <see cref="Vector3"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Vector2 ToCartesianCoordinate2R()
      => new(m_x, m_y);

    /// <summary>Converts the <see cref="Point2"/> to a <see cref="Size2"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Size2 ToSize2()
      => new(m_x, m_y);

    /// <summary>Converts the <see cref="Point2"/> to a 'mapped' unique index. This index is uniquely mapped using the specified <paramref name="gridWidth"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public long ToUniqueIndex(int gridWidth)
      => ToUniqueIndex(m_x, m_y, gridWidth);

    /// <summary>Converts the <see cref="Point2"/> to a <see cref="System.Numerics.Vector2"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.Vector2 ToVector2()
      => new(m_x, m_y);

    #endregion

    #region Static methods
    /// <summary>Computes the closest cartesian coordinate point at the specified angle and distance.</summary>
    public static Point2 ComputePoint(double angle, double distance)
      => new(System.Convert.ToInt32(distance * System.Math.Sin(angle)), System.Convert.ToInt32(distance * System.Math.Cos(angle)));

    /// <summary>Returns the cross product of the two vectors.</summary>
    /// <remarks>This is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static int CrossProduct(Point2 p1, Point2 p2)
      => p1.m_x * p2.m_y - p1.m_y * p2.m_x;

    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(Point2 p1, Point2 p2)
      => p1.m_x * p2.m_x + p1.m_y * p2.m_y;

    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static Point2 FromRandomAbsolute(int toExclusiveX, int toExclusiveY)
      => new(Random.NumberGenerators.Crypto.Next(toExclusiveX), Random.NumberGenerators.Crypto.Next(toExclusiveY));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static Point2 FromRandomCenterZero(int toExclusiveX, int toExclusiveY)
      => new(Random.NumberGenerators.Crypto.Next(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Random.NumberGenerators.Crypto.Next(toExclusiveY * 2 - 1) - (toExclusiveY - 1));

    /// <summary>Convert a 'mapped' unique index to a <see cref="Point2"/>. This index is uniquely mapped using the specified <paramref name="gridWidth"/>.</summary>
    public static Point2 FromUniqueIndex(long index, int gridWidth)
      => new((int)(index % gridWidth), (int)(index / gridWidth));

    /// <summary>Returns the average rate of change, or simply the slope between two points.</summary>
    public static Geometry.LineSlope GetLineSlope(Point2 source, Point2 target)
      => new(source.X, source.Y, target.X, target.Y);

    ///// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    //public static System.Collections.Generic.IEnumerable<Point2> GetQuadrantCenterVectors(Point2 source, Size2 subQuadrant)
    //{
    //  yield return new Point2(source.m_x + subQuadrant.Width, source.m_y + subQuadrant.Height);
    //  yield return new Point2(source.m_x - subQuadrant.Width, source.m_y + subQuadrant.Height);
    //  yield return new Point2(source.m_x - subQuadrant.Width, source.m_y - subQuadrant.Height);
    //  yield return new Point2(source.m_x + subQuadrant.Width, source.m_y - subQuadrant.Height);
    //}

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 InterpolateCosine(Point2 p1, Point2 p2, double mu)
      => new(System.Convert.ToInt32(CosineInterpolation.Interpolate(p1.X, p2.X, mu)), System.Convert.ToInt32(CosineInterpolation.Interpolate(p1.Y, p2.Y, mu)));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 InterpolateCubic(Point2 p0, Point2 p1, Point2 p2, Point2 p3, double mu)
      => new(System.Convert.ToInt32(CubicInterpolation.Interpolate(p0.X, p1.X, p2.X, p3.X, mu)), System.Convert.ToInt32(CubicInterpolation.Interpolate(p0.Y, p1.Y, p2.Y, p3.Y, mu)));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 InterpolateHermite2(Point2 p0, Point2 p1, Point2 p2, Point2 p3, double mu, double tension, double bias)
      => new(System.Convert.ToInt32(HermiteInterpolation.Interpolate(p0.X, p1.X, p2.X, p3.X, mu, tension, bias)), System.Convert.ToInt32(HermiteInterpolation.Interpolate(p0.Y, p1.Y, p2.Y, p3.Y, mu, tension, bias)));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 InterpolateLinear(Point2 p1, Point2 p2, double mu)
      => new(System.Convert.ToInt32(LinearInterpolation.Interpolate(p1.X, p2.X, mu)), System.Convert.ToInt32(LinearInterpolation.Interpolate(p1.Y, p2.Y, mu)));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static Point2 Lerp(Point2 source, Point2 target, double mu)
    {
      var imu = 1 - mu;

      return new Point2(System.Convert.ToInt32(source.X * imu + target.X * mu), System.Convert.ToInt32(source.Y * imu + target.Y * mu));
    }

    //public static Point2 Nlerp(Point2 source, Point2 target, double mu)
    //  => Lerp(source, target, mu).Normalized();

    /// <summary>Returns the orthant (quadrant) of the 2D vector using binary numbering: X = 0 or 1, Y = 0 or 2, which are then added up, based on the sign of the respective component.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantBinaryNumber(Point2 p, Point2 center)
      => (p.m_x >= center.m_x ? 0 : 1) + (p.m_y >= center.m_y ? 0 : 2);

    /// <summary>Returns the quadrant of the 2D vector based on some specified center vector. Enumerated according to the mathematical custom, where the numbering goes counter-clockwise starting from the upper right ("northeast") quadrant.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static int QuadrantNumber(Point2 p, Point2 center)
      => p.m_y >= center.m_y ? (p.m_x >= center.m_x ? 0 : 1) : (p.m_x >= center.m_x ? 3 : 2);

    private static readonly System.Text.RegularExpressions.Regex m_regexParse = new(@"^[^\d]*(?<X>\d+)[^\d]+(?<Y>\d+)[^\d]*$");
    public static Point2 Parse(string pointAsString)
      => m_regexParse.Match(pointAsString) is var m && m.Success && m.Groups["X"] is var gX && gX.Success && int.TryParse(gX.Value, out var x) && m.Groups["Y"] is var gY && gY.Success && int.TryParse(gY.Value, out var y)
      ? new Point2(x, y)
      : throw new System.ArgumentOutOfRangeException(nameof(pointAsString));
    public static bool TryParse(string pointAsString, out Point2 point)
    {
      try
      {
        point = Parse(pointAsString);
        return true;
      }
      catch
      {
        point = default!;
        return false;
      }
    }

    /// <summary>Slerp is a sherical linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0). Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static Point2 Slerp(Point2 source, Point2 target, double mu)
    {
      var dp = System.Math.Clamp(DotProduct(source, target), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dp) * mu; // Angle between start and desired.
      var cos = System.Math.Cos(theta);
      var sin = System.Math.Sin(theta);

      return new Point2(System.Convert.ToInt32(source.m_x * cos + (target.m_x - source.m_x) * dp * sin), System.Convert.ToInt32(source.m_y * cos + (target.m_y - source.m_y) * dp * sin));
    }

    /// <summary>Converts the (x, y) point to a 'mapped' unique index. This index is uniquely mapped using the specified <paramref name="gridWidth"/>.</summary>
    public static long ToUniqueIndex(int x, int y, int gridWidth)
      => x + (y * gridWidth);

    #endregion Static methods

    #region Overloaded operators
    public static explicit operator Point2(System.ValueTuple<int, int> xy)
      => new(xy.Item1, xy.Item2);

    public static bool operator ==(Point2 p1, Point2 p2)
      => p1.Equals(p2);
    public static bool operator !=(Point2 p1, Point2 p2)
      => !p1.Equals(p2);

    public static Point2 operator -(Point2 v)
      => new(-v.m_x, -v.m_y);

    public static Point2 operator ~(Point2 v)
      => new(~v.m_x, ~v.m_y);

    public static Point2 operator --(Point2 p)
      => new(p.m_x - 1, p.m_y - 1);
    public static Point2 operator ++(Point2 p)
      => new(p.m_x + 1, p.m_y + 1);

    public static Point2 operator +(Point2 p1, Point2 p2)
      => new(p1.m_x + p2.m_x, p1.m_y + p2.m_y);
    public static Point2 operator +(Point2 p, int v)
      => new(p.m_x + v, p.m_y + v);
    public static Point2 operator +(int v, Point2 p)
      => new(v + p.m_x, v + p.m_y);

    public static Point2 operator -(Point2 p1, Point2 p2)
      => new(p1.m_x - p2.m_x, p1.m_y - p2.m_y);
    public static Point2 operator -(Point2 p, int v)
      => new(p.m_x - v, p.m_y - v);
    public static Point2 operator -(int v, Point2 p)
      => new(v - p.m_x, v - p.m_y);

    public static Point2 operator *(Point2 p1, Point2 p2)
      => new(p1.m_x * p2.m_x, p1.m_y * p2.m_y);
    public static Point2 operator *(Point2 p, int v)
      => new(p.m_x * v, p.m_y * v);
    public static Point2 operator *(Point2 p, double v)
      => new((int)(p.m_x * v), (int)(p.m_y * v));
    public static Point2 operator *(int v, Point2 p)
      => new(v * p.m_x, v * p.m_y);
    public static Point2 operator *(double v, Point2 p)
      => new((int)(v * p.m_x), (int)(v * p.m_y));

    public static Point2 operator /(Point2 p1, Point2 p2)
      => new(p1.m_x / p2.m_x, p1.m_y / p2.m_y);
    public static Point2 operator /(Point2 p, int v)
      => new(p.m_x / v, p.m_y / v);
    public static Point2 operator /(Point2 p, double v)
      => new((int)(p.m_x / v), (int)(p.m_y / v));
    public static Point2 operator /(int v, Point2 p)
      => new(v / p.m_x, v / p.m_y);
    public static Point2 operator /(double v, Point2 p)
      => new((int)(v / p.m_x), (int)(v / p.m_y));

    public static Point2 operator %(Point2 p1, Point2 p2)
      => new(p1.m_x % p2.m_x, p1.m_y % p2.m_y);
    public static Point2 operator %(Point2 p, int v)
      => new(p.m_x % v, p.m_y % v);
    public static Point2 operator %(Point2 p, double v)
      => new((int)(p.m_x % v), (int)(p.m_y % v));
    public static Point2 operator %(int v, Point2 p)
      => new(v % p.m_x, v % p.m_y);
    public static Point2 operator %(double v, Point2 p)
      => new((int)(v % p.m_x), (int)(v % p.m_y));

    public static Point2 operator &(Point2 p1, Point2 p2)
      => new(p1.m_x & p2.m_x, p1.m_y & p2.m_y);
    public static Point2 operator &(Point2 p, int v)
      => new(p.m_x & v, p.m_y & v);
    public static Point2 operator &(int v, Point2 p)
      => new(v & p.m_x, v & p.m_y);

    public static Point2 operator |(Point2 p1, Point2 p2)
      => new(p1.m_x | p2.m_x, p1.m_y | p2.m_y);
    public static Point2 operator |(Point2 p, int v)
      => new(p.m_x | v, p.m_y | v);
    public static Point2 operator |(int v, Point2 p)
      => new(v | p.m_x, v | p.m_y);

    public static Point2 operator ^(Point2 p1, Point2 p2)
      => new(p1.m_x ^ p2.m_x, p1.m_y ^ p2.m_y);
    public static Point2 operator ^(Point2 p, int v)
      => new(p.m_x ^ v, p.m_y ^ v);
    public static Point2 operator ^(int v, Point2 p)
      => new(v ^ p.m_x, v ^ p.m_y);

    public static Point2 operator <<(Point2 p, int v)
      => new(p.m_x << v, p.m_y << v);
    public static Point2 operator >>(Point2 p, int v)
      => new(p.m_x >> v, p.m_y >> v);
    #endregion Overloaded operators

    #region Implemented interfaces
    public int CompareTo(Point2 other)
      => m_x > other.m_x ? 1 : m_x < other.m_x ? -1 : m_y > other.m_y ? 1 : m_y < other.m_y ? -1 : 0;
    public bool Equals(Point2 other)
      => m_x == other.m_x && m_y == other.m_y;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
       => obj is Point2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y);
    public override string ToString()
      => $"{GetType().Name} {{ X = {m_x}, Y = {m_y} }}";
    #endregion Object overrides
  }
}
