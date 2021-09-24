namespace Flux.Geometry
{
  public struct Point2
    : /*System.IComparable<Point2>,*/ System.IEquatable<Point2>
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
      if (array is null || array.Length - startIndex < 2) throw new System.ArgumentOutOfRangeException(nameof(array));

      m_x = array[startIndex++];
      m_y = array[startIndex];
    }

    public int X
      => m_x;
    public int Y
      => m_y;

    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public double ChebyshevLength()
      => System.Math.Max(System.Math.Abs(m_x), System.Math.Abs(m_y));

    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public double EuclideanLength()
      => System.Math.Sqrt(EuclideanLengthSquared());
    /// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public double EuclideanLengthSquared()
      => System.Math.Pow(m_x, 2) + System.Math.Pow(m_y, 2);

    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public int ManhattanLength()
      => System.Math.Abs(m_x) + System.Math.Abs(m_y);

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public Point2 PerpendicularCcw()
      => new Point2(-m_y, m_x);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public Point2 PerpendicularCw()
      => new Point2(m_y, -m_x);

    /// <summary>Returns the quadrant of the 2D vector based on some specified center vector. This is the more traditional quadrant.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public int QuadrantNumber(Point2 center)
      => m_y >= center.m_y ? (m_x >= center.m_x ? 0 : 1) : (m_x >= center.m_x ? 3 : 2);

    /// <summary>Returns the orthant (quadrant) of the 2D vector using binary numbering: X = 1 and Y = 2, which are then added up, based on the sign of the respective component.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public int OrthantNumber(Point2 center)
      => (m_x >= center.m_x ? 0 : 1) + (m_y >= center.m_y ? 0 : 2);

    public CartesianCoordinate2 ToCartesianCoordinate2()
      => new CartesianCoordinate2(m_x, m_y);
    /// <summary>Creates a <see cref='Size2'/> from a <see cref='Point2'/>.</summary>
    public Size2 ToSize2()
      => new Size2(m_x, m_y);
    public System.Numerics.Vector2 ToVector2()
      => new System.Numerics.Vector2(m_x, m_y);

    #region Static methods
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(Point2 p1, Point2 p2)
      => (p2 - p1).ChebyshevLength();

    /// <summary>Computes the closest cartesian coordinate point at the specified angle and distance.</summary>
    public static Point2 ComputePoint(double angle, double distance)
      => new Point2(System.Convert.ToInt32(distance * System.Math.Sin(angle)), System.Convert.ToInt32(distance * System.Math.Cos(angle)));

    /// <summary>Returns the cross product of the two vectors.</summary>
    /// <remarks>This is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static int CrossProduct(Point2 p1, Point2 p2)
      => p1.m_x * p2.m_y - p1.m_y * p2.m_x;

    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point2 DivideCeiling(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(System.Math.Ceiling(p.m_x / value)), System.Convert.ToInt32(System.Math.Ceiling(p.m_y / value)));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point2 DivideFloor(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(System.Math.Floor(p.m_x / value)), System.Convert.ToInt32(System.Math.Floor(p.m_y / value)));

    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(Point2 p1, Point2 p2)
      => p1.m_x * p2.m_x + p1.m_y * p2.m_y;

    /// <summary>Compute the euclidean distance of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(Point2 p1, Point2 p2)
      => (p2 - p1).EuclideanLength();
    /// <summary>Compute the euclidean distance squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceSquare(Point2 p1, Point2 p2)
      => (p2 - p1).EuclideanLengthSquared();

    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static Point2 FromRandomAbsolute(int toExclusiveX, int toExclusiveY)
      => new Point2(Randomization.NumberGenerator.Crypto.Next(toExclusiveX), Randomization.NumberGenerator.Crypto.Next(toExclusiveY));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static Point2 FromRandomCenterZero(int toExclusiveX, int toExclusiveY)
      => new Point2(Randomization.NumberGenerator.Crypto.Next(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Randomization.NumberGenerator.Crypto.Next(toExclusiveY * 2 - 1) - (toExclusiveY - 1));

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
    public static Point2 InterpolateCosine(Point2 y1, Point2 y2, double mu)
      => new Point2(System.Convert.ToInt32(Maths.InterpolateCosine(y1.X, y2.X, mu)), System.Convert.ToInt32(Maths.InterpolateCosine(y1.Y, y2.Y, mu)));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 InterpolateCubic(Point2 y0, Point2 y1, Point2 y2, Point2 y3, double mu)
      => new Point2(System.Convert.ToInt32(Maths.InterpolateCubic(y0.X, y1.X, y2.X, y3.X, mu)), System.Convert.ToInt32(Maths.InterpolateCubic(y0.Y, y1.Y, y2.Y, y3.Y, mu)));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 InterpolateHermite2(Point2 y0, Point2 y1, Point2 y2, Point2 y3, double mu, double tension, double bias)
      => new Point2(System.Convert.ToInt32(Maths.InterpolateHermite(y0.X, y1.X, y2.X, y3.X, mu, tension, bias)), System.Convert.ToInt32(Maths.InterpolateHermite(y0.Y, y1.Y, y2.Y, y3.Y, mu, tension, bias)));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 InterpolateLinear(Point2 y1, Point2 y2, double mu)
      => new Point2(System.Convert.ToInt32(Maths.InterpolateLinear(y1.X, y2.X, mu)), System.Convert.ToInt32(Maths.InterpolateLinear(y1.Y, y2.Y, mu)));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static Point2 Lerp(Point2 source, Point2 target, double mu)
    {
      var imu = 1 - mu;

      return new Point2(System.Convert.ToInt32(source.X * imu + target.X * mu), System.Convert.ToInt32(source.Y * imu + target.Y * mu));
    }

    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int ManhattanDistance(Point2 p1, Point2 p2)
      => (p2 - p1).ManhattanLength();

    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point2 MultiplyCeiling(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(System.Math.Ceiling(p.m_x * value)), System.Convert.ToInt32(System.Math.Ceiling(p.m_y * value)));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point2 MultiplyFloor(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(System.Math.Floor(p.m_x * value)), System.Convert.ToInt32(System.Math.Floor(p.m_y * value)));

    //public static Point2 Nlerp(Point2 source, Point2 target, double mu)
    //  => Lerp(source, target, mu).Normalized();

    private static readonly System.Text.RegularExpressions.Regex m_regexParse = new System.Text.RegularExpressions.Regex(@"^[^\d]*(?<X>\d+)[^\d]+(?<Y>\d+)[^\d]*$");
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
    #endregion Static methods

    #region "Unique" index
    /// <summary>Convert an index to a 2D point, based on the specified grid lengths of axes.</summary>
    public static Point2 FromUniqueIndex(long index, Size2 bounds)
      => new Point2((int)(index % bounds.Width), (int)(index / bounds.Width));

    /// <summary>Converts the 2D point to an index, based on the specified grid lengths of axes.</summary>
    public static long ToUniqueIndex(int x, int y, Size2 bounds)
      => x + (y * bounds.Width);
    public static long ToUniqueIndex(Point2 point, Size2 bounds)
      => ToUniqueIndex(point.m_x, point.m_y, bounds);
    #endregion "Unique" index

    #region Overloaded operators
    public static bool operator ==(Point2 p1, Point2 p2)
      => p1.Equals(p2);
    public static bool operator !=(Point2 p1, Point2 p2)
      => !p1.Equals(p2);

    public static Point2 operator -(Point2 v)
      => new Point2(-v.m_x, -v.m_y);

    public static Point2 operator ~(Point2 v)
      => new Point2(~v.m_x, ~v.m_y);

    public static Point2 operator --(Point2 p)
      => new Point2(p.m_x - 1, p.m_y - 1);
    public static Point2 operator ++(Point2 p)
      => new Point2(p.m_x + 1, p.m_y + 1);

    public static Point2 operator +(Point2 p1, Point2 p2)
      => new Point2(p1.m_x + p2.m_x, p1.m_y + p2.m_y);
    public static Point2 operator +(Point2 p, int v)
      => new Point2(p.m_x + v, p.m_y + v);
    public static Point2 operator +(int v, Point2 p)
      => new Point2(v + p.m_x, v + p.m_y);

    public static Point2 operator -(Point2 p1, Point2 p2)
      => new Point2(p1.m_x - p2.m_x, p1.m_y - p2.m_y);
    public static Point2 operator -(Point2 p, int v)
      => new Point2(p.m_x - v, p.m_y - v);
    public static Point2 operator -(int v, Point2 p)
      => new Point2(v - p.m_x, v - p.m_y);

    public static Point2 operator *(Point2 p1, Point2 p2)
      => new Point2(p1.m_x * p2.m_x, p1.m_y * p2.m_y);
    public static Point2 operator *(Point2 p, int v)
      => new Point2(p.m_x * v, p.m_y * v);
    public static Point2 operator *(Point2 p, double v)
      => new Point2((int)(p.m_x * v), (int)(p.m_y * v));
    public static Point2 operator *(int v, Point2 p)
      => new Point2(v * p.m_x, v * p.m_y);
    public static Point2 operator *(double v, Point2 p)
      => new Point2((int)(v * p.m_x), (int)(v * p.m_y));

    public static Point2 operator /(Point2 p1, Point2 p2)
      => new Point2(p1.m_x / p2.m_x, p1.m_y / p2.m_y);
    public static Point2 operator /(Point2 p, int v)
      => new Point2(p.m_x / v, p.m_y / v);
    public static Point2 operator /(Point2 p, double v)
      => new Point2((int)(p.m_x / v), (int)(p.m_y / v));
    public static Point2 operator /(int v, Point2 p)
      => new Point2(v / p.m_x, v / p.m_y);
    public static Point2 operator /(double v, Point2 p)
      => new Point2((int)(v / p.m_x), (int)(v / p.m_y));

    public static Point2 operator %(Point2 p1, Point2 p2)
      => new Point2(p1.m_x % p2.m_x, p1.m_y % p2.m_y);
    public static Point2 operator %(Point2 p, int v)
      => new Point2(p.m_x % v, p.m_y % v);
    public static Point2 operator %(Point2 p, double v)
      => new Point2((int)(p.m_x % v), (int)(p.m_y % v));
    public static Point2 operator %(int v, Point2 p)
      => new Point2(v % p.m_x, v % p.m_y);
    public static Point2 operator %(double v, Point2 p)
      => new Point2((int)(v % p.m_x), (int)(v % p.m_y));

    public static Point2 operator &(Point2 p1, Point2 p2)
      => new Point2(p1.m_x & p2.m_x, p1.m_y & p2.m_y);
    public static Point2 operator &(Point2 p, int v)
      => new Point2(p.m_x & v, p.m_y & v);
    public static Point2 operator &(int v, Point2 p)
      => new Point2(v & p.m_x, v & p.m_y);

    public static Point2 operator |(Point2 p1, Point2 p2)
      => new Point2(p1.m_x | p2.m_x, p1.m_y | p2.m_y);
    public static Point2 operator |(Point2 p, int v)
      => new Point2(p.m_x | v, p.m_y | v);
    public static Point2 operator |(int v, Point2 p)
      => new Point2(v | p.m_x, v | p.m_y);

    public static Point2 operator ^(Point2 p1, Point2 p2)
      => new Point2(p1.m_x ^ p2.m_x, p1.m_y ^ p2.m_y);
    public static Point2 operator ^(Point2 p, int v)
      => new Point2(p.m_x ^ v, p.m_y ^ v);
    public static Point2 operator ^(int v, Point2 p)
      => new Point2(v ^ p.m_x, v ^ p.m_y);

    public static Point2 operator <<(Point2 p, int v)
      => new Point2(p.m_x << v, p.m_y << v);
    public static Point2 operator >>(Point2 p, int v)
      => new Point2(p.m_x >> v, p.m_y >> v);
    #endregion Overloaded operators

    #region Implemented interfaces
    // System.IComparable
    //public int CompareTo(Point2 other)
    //  => m_x < other.m_x ? -1 : m_x > other.m_x ? 1 : m_y < other.m_y ? -1 : m_y > other.m_y ? 1 : 0;

    // System.IEquatable
    public bool Equals(Point2 other)
      => m_x == other.m_x && m_y == other.m_y;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
       => obj is Point2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y);
    public override string ToString()
      => $"<{GetType().Name}: {m_x}, {m_y}>";
    #endregion Object overrides
  }
}
