namespace Flux
{
  /// <summary>A 3D cartesian coordinate using integers.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct Point3
    : IPoint3<int>
  {
    /// <summary>Returns the vector (0,0).</summary>
    public static readonly Point3 Zero;

    private readonly int m_x;
    private readonly int m_y;
    private readonly int m_z;

    public Point3(int x, int y, int z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }
    public Point3(int value) : this(value, value, value) { }

    [System.Diagnostics.Contracts.Pure] public int X { get => m_x; init => m_x = value; }
    [System.Diagnostics.Contracts.Pure] public int Y { get => m_y; init => m_y = value; }
    [System.Diagnostics.Contracts.Pure] public int Z { get => m_z; init => m_z = value; }

    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    [System.Diagnostics.Contracts.Pure]
    public int ChebyshevLength(int edgeLength = 1)
      => System.Math.Max(System.Math.Max(System.Math.Abs(m_x / edgeLength), System.Math.Abs(m_y / edgeLength)), System.Math.Abs(m_z / edgeLength));

    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    [System.Diagnostics.Contracts.Pure]
    public int EuclideanLength()
      //=> GenericMath.IntegerSqrt(EuclideanLengthSquared());
      => (int)System.Math.Sqrt(EuclideanLengthSquared());

    /// <summary>Compute the length (or magnitude) squared (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    [System.Diagnostics.Contracts.Pure]
    public int EuclideanLengthSquared()
      => m_x * m_x + m_y * m_y + m_z * m_z;

    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    [System.Diagnostics.Contracts.Pure]
    public int ManhattanLength(int edgeLength = 1)
      => System.Math.Abs(m_x / edgeLength) + System.Math.Abs(m_y / edgeLength) + System.Math.Abs(m_z / edgeLength);

    [System.Diagnostics.Contracts.Pure]
    public Point3 Normalized()
      => EuclideanLength() is var m && m != 0 ? this / m : this;

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    [System.Diagnostics.Contracts.Pure]
    public int OrthantNumber(Point3 center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => m_z >= center.m_z ? (m_y >= center.m_y ? (m_x >= center.m_x ? 0 : 1) : (m_x >= center.m_x ? 3 : 2)) : (m_y >= center.m_y ? (m_x >= center.m_x ? 7 : 6) : (m_x >= center.m_x ? 4 : 5)),
        OrthantNumbering.BinaryNegativeAs1 => (m_x >= center.m_x ? 0 : 1) + (m_y >= center.m_y ? 0 : 2) + (m_z >= center.m_z ? 0 : 4),
        OrthantNumbering.BinaryPositiveAs1 => (m_x < center.m_x ? 0 : 1) + (m_y < center.m_y ? 0 : 2) + (m_z < center.m_z ? 0 : 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    #region To..

    /// <summary>Converts the <see cref="Point3"/> to a <see cref="Vector3"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Vector3 ToCartesianCoordinate3R()
      => new(m_x, m_y, m_z);

    /// <summary>Converts the <see cref="Point2"/> to a <see cref="Size2"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Size3 ToSize3()
      => new(m_x, m_y, m_z);

    /// <summary>Converts the <see cref="Point3"/> to a 'mapped' unique index. This index is uniquely mapped using the specified <paramref name="gridWidth"/> and <paramref name="gridHeight"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public long ToUniqueIndex(int gridWidth, int gridHeight)
      => ToUniqueIndex(m_x, m_y, m_z, gridWidth, gridHeight);

    /// <summary>Converts the <see cref="Point2"/> to a <see cref="System.Numerics.Vector2"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.Vector3 ToVector3()
      => new(m_x, m_y, m_z);

    #endregion

    #region Static methods

    /// <summary>Create a new vector by computing the cross product, i.e. cross(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cross_product"/>
    public static Point3 CrossProduct(Point3 p1, Point3 p2)
      => new(p1.m_y * p2.m_z - p1.m_z * p2.m_y, p1.m_z * p2.m_x - p1.m_x * p2.m_z, p1.m_x * p2.m_y - p1.m_y * p2.m_x);

    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(Point3 p1, Point3 p2)
      => p1.m_x * p2.m_x + p1.m_y * p2.m_y + p1.m_z * p2.m_z;

    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static Point3 FromRandomAbsolute(int toExclusiveX, int toExclusiveY, int toExclusiveZ)
      => new(Random.NumberGenerators.Crypto.Next(toExclusiveX), Random.NumberGenerators.Crypto.Next(toExclusiveY), Random.NumberGenerators.Crypto.Next(toExclusiveZ));
    /// <summary>Create a new random vector in the range (-toExlusive, toExclusive) using the crypto-grade rng.</summary>
    public static Point3 FromRandomCenterZero(int toExclusiveX, int toExclusiveY, int toExclusiveZ)
      => new(Random.NumberGenerators.Crypto.Next(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Random.NumberGenerators.Crypto.Next(toExclusiveY * 2 - 1) - (toExclusiveY - 1), Random.NumberGenerators.Crypto.Next(toExclusiveZ * 2 - 1) - (toExclusiveZ - 1));

    /// <summary>Convert a 'mapped' unique index to a <see cref="Point3"/>. This index is uniquely mapped using the specified <paramref name="gridWidth"/> and <paramref name="gridHeight"/>.</summary>
    public static Point3 FromUniqueIndex(long index, int gridWidth, int gridHeight)
    {
      var xy = gridWidth * gridHeight;
      var irxy = index % xy;

      return new((int)(irxy % gridWidth), (int)(irxy / gridWidth), (int)(index / xy));
    }

    ///// <summary>Creates eight vectors, each of which represents the center axis for each of the octants for the vector and the specified sizes of X, Y and Z.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
    //public static System.Collections.Generic.IEnumerable<Point3> GetOctantCenterVectors(Point3 source, Size3 subOctant)
    //{
    //  yield return new Point3(source.m_x + subOctant.Width, source.m_y + subOctant.Height, source.m_z + subOctant.Depth);
    //  yield return new Point3(source.m_x - subOctant.Width, source.m_y + subOctant.Height, source.m_z + subOctant.Depth);
    //  yield return new Point3(source.m_x - subOctant.Width, source.m_y - subOctant.Height, source.m_z + subOctant.Depth);
    //  yield return new Point3(source.m_x + subOctant.Width, source.m_y - subOctant.Height, source.m_z + subOctant.Depth);
    //  yield return new Point3(source.m_x + subOctant.Width, source.m_y + subOctant.Height, source.m_z - subOctant.Depth);
    //  yield return new Point3(source.m_x - subOctant.Width, source.m_y + subOctant.Height, source.m_z - subOctant.Depth);
    //  yield return new Point3(source.m_x - subOctant.Width, source.m_y - subOctant.Height, source.m_z - subOctant.Depth);
    //  yield return new Point3(source.m_x + subOctant.Width, source.m_y - subOctant.Height, source.m_z - subOctant.Depth);
    //}

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point3 Interpolate(Point3 p1, Point3 p2, double mu, I2NodeInterpolatable<double, double> mode)
    {
      mode ??= new Interpolation.LinearInterpolation<double, double>();

      return new(System.Convert.ToInt32(mode.Interpolate2Node(p1.X, p2.X, mu)), System.Convert.ToInt32(mode.Interpolate2Node(p1.Y, p2.Y, mu)), System.Convert.ToInt32(mode.Interpolate2Node(p1.Z, p2.Z, mu)));
    }

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point3 Interpolate(Point3 p0, Point3 p1, Point3 p2, Point3 p3, double mu, I4NodeInterpolatable<double, double> mode)
    {
      mode ??= new Interpolation.CubicInterpolation<double, double>();

      return new(System.Convert.ToInt32(mode.Interpolate4Node(p0.X, p1.X, p2.X, p3.X, mu)), System.Convert.ToInt32(mode.Interpolate4Node(p0.Y, p1.Y, p2.Y, p3.Y, mu)), System.Convert.ToInt32(mode.Interpolate4Node(p0.Z, p1.Z, p2.Z, p3.Z, mu)));
    }

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static Point3 InterpolateCosine(Point3 p1, Point3 p2, double mu)
    //  => new(System.Convert.ToInt32(CosineInterpolation.Interpolate(p1.X, p2.X, mu)), System.Convert.ToInt32(CosineInterpolation.Interpolate(p1.Y, p2.Y, mu)), System.Convert.ToInt32(CosineInterpolation.Interpolate(p1.Z, p2.Z, mu)));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static Point3 InterpolateCubic(Point3 p0, Point3 p1, Point3 p2, Point3 p3, double mu)
    //  => new(System.Convert.ToInt32(CubicInterpolation.Interpolate(p0.X, p1.X, p2.X, p3.X, mu)), System.Convert.ToInt32(CubicInterpolation.Interpolate(p0.Y, p1.Y, p2.Y, p3.Y, mu)), System.Convert.ToInt32(CubicInterpolation.Interpolate(p0.Z, p1.Z, p2.Z, p3.Z, mu)));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static Point3 InterpolateHermite2(Point3 p0, Point3 p1, Point3 p2, Point3 p3, double mu, double tension, double bias)
    //  => new(System.Convert.ToInt32(HermiteInterpolation.Interpolate(p0.X, p1.X, p2.X, p3.X, mu, tension, bias)), System.Convert.ToInt32(HermiteInterpolation.Interpolate(p0.Y, p1.Y, p2.Y, p3.Y, mu, tension, bias)), System.Convert.ToInt32(HermiteInterpolation.Interpolate(p0.Z, p1.Z, p2.Z, p3.Z, mu, tension, bias)));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static Point3 InterpolateLinear(Point3 p1, Point3 p2, double mu)
    //  => new(System.Convert.ToInt32(LinearInterpolation.Interpolate(p1.X, p2.X, mu)), System.Convert.ToInt32(LinearInterpolation.Interpolate(p1.Y, p2.Y, mu)), System.Convert.ToInt32(LinearInterpolation.Interpolate(p1.Z, p2.Z, mu)));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static Point3 Lerp(Point3 source, Point3 target, double mu)
    {
      var imu = 1 - mu;

      return new(System.Convert.ToInt32(source.X * imu + target.X * mu), System.Convert.ToInt32(source.Y * imu + target.Y * mu), System.Convert.ToInt32(source.Z * imu + target.Z * mu));
    }

    //public static Point3 Nlerp(Point3 source, Point3 target, double mu)
    //  => Lerp(source, target, mu).Normalized();

    /// <summary>Returns the orthant (octant) of the 3D vector using binary numbering: X = 1, Y = 2 and Z = 4, which are then added up, based on the sign of the respective component.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantBinaryNumber(Point3 p, Point3 center)
      => (p.m_x >= center.m_x ? 0 : 1) + (p.m_y >= center.m_y ? 0 : 2) + (p.m_z >= center.m_z ? 0 : 4);

    /// <summary>Returns the octant of the 3D vector based on the specified axis vector. This is the more traditional octant.</summary>
    /// <returns>The octant identifer in the range 0-7, i.e. one of the eight octants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
    public static int OctantNumber(Point3 p, Point3 center)
      => p.m_z >= center.m_z ? (p.m_y >= center.m_y ? (p.m_x >= center.m_x ? 0 : 1) : (p.m_x >= center.m_x ? 3 : 2)) : (p.m_y >= center.m_y ? (p.m_x >= center.m_x ? 7 : 6) : (p.m_x >= center.m_x ? 4 : 5));

    private static readonly System.Text.RegularExpressions.Regex m_regexParse = new(@"^[^\d]*(?<X>\d+)[^\d]+(?<Y>\d+)[^\d]+(?<Z>\d+)[^\d]*$");
    public static Point3 Parse(string pointAsString)
      => m_regexParse.Match(pointAsString) is var m && m.Success && m.Groups["X"] is var gX && gX.Success && int.TryParse(gX.Value, out var x) && m.Groups["Y"] is var gY && gY.Success && int.TryParse(gY.Value, out var y) && m.Groups["Z"] is var gZ && gZ.Success && int.TryParse(gZ.Value, out var z)
      ? new Point3(x, y, z)
      : throw new System.ArgumentOutOfRangeException(nameof(pointAsString));
    public static bool TryParse(string pointAsString, out Point3 point)
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

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static int ScalarTripleProduct(Point3 p1, Point3 p2, Point3 p3)
      => DotProduct(p1, CrossProduct(p2, p3));

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static Point3 Slerp(Point3 source, Point3 target, double mu)
    {
      var dp = System.Math.Clamp(DotProduct(source, target), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dp) * mu; // Angle between start and desired.
      var cos = System.Math.Cos(theta);
      var sin = System.Math.Sin(theta);

      return new(System.Convert.ToInt32(source.m_x * cos + (target.m_x - source.m_x) * dp * sin), System.Convert.ToInt32(source.m_y * cos + (target.m_y - source.m_y) * dp * sin), System.Convert.ToInt32(source.m_z * cos + (target.m_z - source.m_z) * dp * sin));
    }

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static Point3 VectorTripleProduct(Point3 p1, Point3 p2, Point3 p3)
      => CrossProduct(p1, CrossProduct(p2, p3));

    /// <summary>Converts the (x, y, z) point to a 'mapped' unique index. This index is uniquely mapped using the specified <paramref name="gridWidth"/> and <paramref name="gridHeight"/>.</summary>
    public static long ToUniqueIndex(int x, int y, int z, int gridWidth, int gridHeight)
      => x + (y * gridWidth) + (z * gridWidth * gridHeight);

    #endregion Static methods

    #region Overloaded operators
    public static Point3 operator -(Point3 p)
      => new(-p.m_x, -p.m_y, -p.m_z);

    public static Point3 operator ~(Point3 p)
      => new(~p.m_x, ~p.m_y, ~p.m_z);

    public static Point3 operator --(Point3 p)
      => new(p.m_x - 1, p.m_y - 1, p.m_z - 1);
    public static Point3 operator ++(Point3 p)
      => new(p.m_x + 1, p.m_y + 1, p.m_z + 1);

    public static Point3 operator +(Point3 p1, Point3 p2)
      => new(p1.m_x + p2.m_x, p1.m_y + p2.m_y, p1.m_z + p2.m_z);
    public static Point3 operator +(Point3 p, int v)
      => new(p.m_x + v, p.m_y + v, p.m_z + v);
    public static Point3 operator +(int v, Point3 p)
      => new(v + p.m_x, v + p.m_y, v + p.m_z);

    public static Point3 operator -(Point3 p1, Point3 p2)
      => new(p1.m_x - p2.m_x, p1.m_y - p2.m_y, p1.m_z - p2.m_z);
    public static Point3 operator -(Point3 p, int v)
      => new(p.m_x - v, p.m_y - v, p.m_z - v);
    public static Point3 operator -(int v, Point3 p)
      => new(v - p.m_x, v - p.m_y, v - p.m_z);

    public static Point3 operator *(Point3 p1, Point3 p2)
      => new(p1.m_x * p2.m_x, p1.m_y * p2.m_y, p1.m_z * p2.m_z);
    public static Point3 operator *(Point3 p, int v)
      => new(p.m_x * v, p.m_y * v, p.m_z * v);
    public static Point3 operator *(Point3 p, double v)
      => new((int)(p.m_x * v), (int)(p.m_y * v), (int)(p.m_z * v));
    public static Point3 operator *(int v, Point3 p)
      => new(v * p.m_x, v * p.m_y, v * p.m_z);
    public static Point3 operator *(double v, Point3 p)
      => new((int)(v * p.m_x), (int)(v * p.m_y), (int)(v * p.m_z));

    public static Point3 operator /(Point3 p1, Point3 p2)
      => new(p1.m_x / p2.m_x, p1.m_y / p2.m_y, p1.m_z / p2.m_z);
    public static Point3 operator /(Point3 p, int v)
      => new(p.m_x / v, p.m_y / v, p.m_z / v);
    public static Point3 operator /(Point3 p, double v)
      => new((int)(p.m_x / v), (int)(p.m_y / v), (int)(p.m_z / v));
    public static Point3 operator /(int v, Point3 p)
      => new(v / p.m_x, v / p.m_y, v / p.m_z);
    public static Point3 operator /(double v, Point3 p)
      => new((int)(v / p.m_x), (int)(v / p.m_y), (int)(v / p.m_z));

    public static Point3 operator %(Point3 p1, Point3 p2)
      => new(p1.m_x % p2.m_x, p1.m_y % p2.m_y, p1.m_z % p2.m_z);
    public static Point3 operator %(Point3 p, int v)
      => new(p.m_x % v, p.m_y % v, p.m_z % v);
    public static Point3 operator %(Point3 p, double v)
      => new((int)(p.m_x % v), (int)(p.m_y % v), (int)(p.m_z % v));
    public static Point3 operator %(int v, Point3 p)
      => new(v % p.m_x, v % p.m_y, v % p.m_z);
    public static Point3 operator %(double v, Point3 p)
      => new((int)(v % p.m_x), (int)(v % p.m_y), (int)(v % p.m_z));

    public static Point3 operator &(Point3 p1, Point3 p2)
      => new(p1.m_x & p2.m_x, p1.m_y & p2.m_y, p1.m_z & p2.m_z);
    public static Point3 operator &(Point3 p, int v)
      => new(p.m_x & v, p.m_y & v, p.m_z & v);
    public static Point3 operator &(int v, Point3 p)
      => new(v & p.m_x, v & p.m_y, v & p.m_z);

    public static Point3 operator |(Point3 p1, Point3 p2)
      => new(p1.m_x | p2.m_x, p1.m_y | p2.m_y, p1.m_z | p2.m_z);
    public static Point3 operator |(Point3 p, int v)
      => new(p.m_x | v, p.m_y | v, p.m_z | v);
    public static Point3 operator |(int v, Point3 p)
      => new(v | p.m_x, v | p.m_y, v | p.m_z);

    public static Point3 operator ^(Point3 p1, Point3 p2)
      => new(p1.m_x ^ p2.m_x, p1.m_y ^ p2.m_y, p1.m_z ^ p2.m_z);
    public static Point3 operator ^(Point3 p, int v)
      => new(p.m_x ^ v, p.m_y ^ v, p.m_z ^ v);
    public static Point3 operator ^(int v, Point3 p)
      => new(v ^ p.m_x, v ^ p.m_y, v ^ p.m_z);

    public static Point3 operator <<(Point3 p, int v)
      => new(p.m_x << v, p.m_y << v, p.m_z << v);
    public static Point3 operator >>(Point3 p, int v)
      => new(p.m_x >> v, p.m_y >> v, p.m_z >> v);
    #endregion Overloaded operators
  }
}