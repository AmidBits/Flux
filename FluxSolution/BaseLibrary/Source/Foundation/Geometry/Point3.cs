namespace Flux.Geometry
{
  public struct Point3
    : System.IComparable<Point3>, System.IEquatable<Point3>
  {
    public static readonly Point3 Empty;
    public bool IsEmpty => Equals(Empty);

    private int m_x;
    private int m_y;
    private int m_z;

    public int X { get => m_x; set => m_x = value; }
    public int Y { get => m_y; set => m_y = value; }
    public int Z { get => m_z; set => m_z = value; }

    public Point3(int x, int y, int z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }
    public Point3(Point2 value, int z)
      : this(value.X, value.Y, z) { }
    public Point3(int x, int y)
      : this(x, y, 0) { }
    public Point3(int value)
      : this(value, value, value) { }
    public Point3(int[] array, int startIndex)
    {
      if (array is null) throw new System.ArgumentNullException(nameof(array));

      if (array.Length - startIndex < 3) throw new System.ArgumentOutOfRangeException(nameof(array));

      m_x = array[startIndex++];
      m_y = array[startIndex++];
      m_z = array[startIndex];
    }

    /// <summary>Convert the vector to a unique index using the length of the X and the Y axes.</summary>
    public System.Numerics.Vector3 ToVector3()
      => new System.Numerics.Vector3(X, Y, Z);

    #region Static methods
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(Point3 p1, Point3 p2)
      => Maths.Max(System.Math.Abs(p2.X - p1.X), System.Math.Abs(p2.Y - p1.Y), System.Math.Abs(p2.Z - p1.Z));
    /// <summary>Create a new vector by computing the cross product, i.e. cross(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cross_product"/>
    public static Point3 CrossProduct(Point3 p1, Point3 p2)
      => new Point3(p1.Y * p2.Z - p1.Z * p2.Y, p1.Z * p2.X - p1.X * p2.Z, p1.X * p2.Y - p1.Y * p2.X);
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point3 DivideCeiling(Point3 p, double value)
      => new Point3((int)System.Math.Ceiling(p.X / value), (int)System.Math.Ceiling(p.Y / value), (int)System.Math.Ceiling(p.Z / value));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point3 DivideFloor(Point3 p, double value)
      => new Point3((int)System.Math.Floor(p.X / value), (int)System.Math.Floor(p.Y / value), (int)System.Math.Floor(p.Z / value));
    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(Point3 p1, Point3 p2)
      => p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
    /// <summary>Compute the euclidean distance of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(Point3 p1, Point3 p2)
      => GetLength(p1 - p2);
    /// <summary>Compute the euclidean distance squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceSquare(Point3 p1, Point3 p2)
      => GetLengthSquared(p1 - p2);
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLength(Point3 p)
      => System.Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
    /// <summary>Compute the length (or magnitude) squared (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLengthSquared(Point3 p)
      => p.X * p.X + p.Y * p.Y + p.Z * p.Z;
    /// <summary>Creates eight vectors, each of which represents the center axis for each of the octants for the vector and the specified sizes of X, Y and Z.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
    public static System.Collections.Generic.IEnumerable<Point3> GetOctantCenterVectors(Point3 source, Size3 subOctant)
    {
      yield return new Point3(source.X + subOctant.Width, source.Y + subOctant.Height, source.Z + subOctant.Depth);
      yield return new Point3(source.X - subOctant.Width, source.Y + subOctant.Height, source.Z + subOctant.Depth);
      yield return new Point3(source.X - subOctant.Width, source.Y - subOctant.Height, source.Z + subOctant.Depth);
      yield return new Point3(source.X + subOctant.Width, source.Y - subOctant.Height, source.Z + subOctant.Depth);
      yield return new Point3(source.X + subOctant.Width, source.Y + subOctant.Height, source.Z - subOctant.Depth);
      yield return new Point3(source.X - subOctant.Width, source.Y + subOctant.Height, source.Z - subOctant.Depth);
      yield return new Point3(source.X - subOctant.Width, source.Y - subOctant.Height, source.Z - subOctant.Depth);
      yield return new Point3(source.X + subOctant.Width, source.Y - subOctant.Height, source.Z - subOctant.Depth);
    }
    /// <summary>Convert the 3D vector to a octant based on the specified axis vector.</summary>
    /// <returns>The octant identifer in the range 0-7, i.e. one of the eight octants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
    public static int GetOctantNumber(Point3 source, Point3 center)
      => (source.X >= center.X ? 1 : 0) + (source.Y >= center.Y ? 2 : 0) + (source.Z >= center.Z ? 4 : 0);
    public static Point3 InterpolateCosine(Point3 y1, Point3 y2, double mu)
      => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2
      ? (y1 * (1.0 - mu2) + y2 * mu2)
      : throw new System.ArgumentNullException(nameof(mu));
    public static Point3 InterpolateCubic(Point3 y0, Point3 y1, Point3 y2, Point3 y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
    public static Point3 InterpolateHermite(Point3 y0, Point3 y1, Point3 y2, Point3 y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (y1 - y0) * onePbias * oneMtension / 2;
      m0 += (y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (y2 - y1) * onePbias * oneMtension / 2;
      m1 += (y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return y1 * a0 + m0 * a1 + m1 * a2 + y2 * a3;
    }
    public static Point3 InterpolateLinear(Point3 y1, Point3 y2, double mu)
      => y1 * (1 - mu) + y2 * mu;
    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int ManhattanDistance(Point3 p1, Point3 p2)
      => System.Math.Abs(p2.X - p1.X) + System.Math.Abs(p2.Y - p1.Y) + System.Math.Abs(p2.Z - p1.Z);
    /// <summary>Create a new vector with the ceiling(product) from each member multiplied with the value.</summary>
    public static Point3 MultiplyCeiling(Point3 p, double value)
      => new Point3((int)System.Math.Ceiling(p.X * value), (int)System.Math.Ceiling(p.Y * value), (int)System.Math.Ceiling(p.Z * value));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point3 MultiplyFloor(Point3 p, double value)
      => new Point3((int)System.Math.Floor(p.X * value), (int)System.Math.Floor(p.Y * value), (int)System.Math.Floor(p.Z * value));
    private static readonly System.Text.RegularExpressions.Regex m_regexParse = new System.Text.RegularExpressions.Regex(@"^[^\d]*(?<X>\d+)[^\d]+(?<Y>\d+)[^\d]+(?<Z>\d+)[^\d]*$");
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
    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static Point3 FromRandom(int toExclusiveX, int toExclusiveY, int toExclusiveZ)
      => new Point3(Randomization.NumberGenerator.Crypto.Next(toExclusiveX), Randomization.NumberGenerator.Crypto.Next(toExclusiveY), Randomization.NumberGenerator.Crypto.Next(toExclusiveZ));
    /// <summary>Create a new random vector in the range (-toExlusive, toExclusive) using the crypto-grade rng.</summary>
    public static Point3 FromRandomZero(int toExclusiveX, int toExclusiveY, int toExclusiveZ)
      => new Point3(Randomization.NumberGenerator.Crypto.Next(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Randomization.NumberGenerator.Crypto.Next(toExclusiveY * 2 - 1) - (toExclusiveY - 1), Randomization.NumberGenerator.Crypto.Next(toExclusiveZ * 2 - 1) - (toExclusiveZ - 1));
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static int ScalarTripleProduct(Point3 p1, Point3 p2, Point3 p3)
      => DotProduct(p1, CrossProduct(p2, p3));
    /// <summary>Creates a <see cref='Size3'/> from a <see cref='Point3'/>.</summary>
    public static Size3 ToSize3(Point3 point)
      => new Size3(point.X, point.Y, point.Z);
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static Point3 VectorTripleProduct(Point3 p1, Point3 p2, Point3 p3)
      => CrossProduct(p1, CrossProduct(p2, p3));
    #endregion Static methods

    #region "Unique" index
    /// <summary>Convert a "mapped" index to a 3D point. This index is uniquely mapped using the specified size vector.</summary>
    public static Point3 FromUniqueIndex(long index, in Size3 bounds)
    {
      var xy = (long)bounds.Width * (long)bounds.Height;
      var irxy = index % xy;

      return new Point3((int)(irxy % bounds.Width), (int)(irxy / bounds.Width), (int)(index / xy));
    }
    /// <summary>Converts the point to a "mapped" index. This index is uniquely mapped using the specified size vector.</summary>
    public static long ToUniqueIndex(in Point3 point, in Size3 bounds)
      => point.X + (point.Y * bounds.Width) + (point.Z * bounds.Width * bounds.Height);
    #endregion "Unique" index

    #region Overloaded operators
    public static bool operator ==(Point3 p1, Point3 p2)
      => p1.Equals(p2);
    public static bool operator !=(Point3 p1, Point3 p2)
      => !p1.Equals(p2);

    public static Point3 operator -(Point3 p)
      => new Point3(-p.X, -p.Y, -p.Z);

    public static Point3 operator ~(Point3 p)
      => new Point3(~p.X, ~p.Y, ~p.Z);

    public static Point3 operator --(Point3 p)
      => new Point3(p.X - 1, p.Y - 1, p.Z - 1);
    public static Point3 operator ++(Point3 p)
      => new Point3(p.X + 1, p.Y + 1, p.Z + 1);

    public static Point3 operator +(Point3 p1, Point3 p2)
      => new Point3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
    public static Point3 operator +(Point3 p, int v)
      => new Point3(p.X + v, p.Y + v, p.Z + v);
    public static Point3 operator +(int v, Point3 p)
      => new Point3(v + p.X, v + p.Y, v + p.Z);

    public static Point3 operator -(Point3 p1, Point3 p2)
      => new Point3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
    public static Point3 operator -(Point3 p, int v)
      => new Point3(p.X - v, p.Y - v, p.Z - v);
    public static Point3 operator -(int v, Point3 p)
      => new Point3(v - p.X, v - p.Y, v - p.Z);

    public static Point3 operator *(Point3 p1, Point3 p2)
      => new Point3(p1.X * p2.X, p1.Y * p2.Y, p1.Z * p2.Z);
    public static Point3 operator *(Point3 p, int v)
      => new Point3(p.X * v, p.Y * v, p.Z * v);
    public static Point3 operator *(Point3 p, double v)
      => new Point3((int)(p.X * v), (int)(p.Y * v), (int)(p.Z * v));
    public static Point3 operator *(int v, Point3 p)
      => new Point3(v * p.X, v * p.Y, v * p.Z);
    public static Point3 operator *(double v, Point3 p)
      => new Point3((int)(v * p.X), (int)(v * p.Y), (int)(v * p.Z));

    public static Point3 operator /(Point3 p1, Point3 p2)
      => new Point3(p1.X / p2.X, p1.Y / p2.Y, p1.Z / p2.Z);
    public static Point3 operator /(Point3 p, int v)
      => new Point3(p.X / v, p.Y / v, p.Z / v);
    public static Point3 operator /(Point3 p, double v)
      => new Point3((int)(p.X / v), (int)(p.Y / v), (int)(p.Z / v));
    public static Point3 operator /(int v, Point3 p)
      => new Point3(v / p.X, v / p.Y, v / p.Z);
    public static Point3 operator /(double v, Point3 p)
      => new Point3((int)(v / p.X), (int)(v / p.Y), (int)(v / p.Z));

    public static Point3 operator %(Point3 p1, Point3 p2)
      => new Point3(p1.X % p2.X, p1.Y % p2.Y, p1.Z % p2.Z);
    public static Point3 operator %(Point3 p, int v)
      => new Point3(p.X % v, p.Y % v, p.Z % v);
    public static Point3 operator %(Point3 p, double v)
      => new Point3((int)(p.X % v), (int)(p.Y % v), (int)(p.Z % v));
    public static Point3 operator %(int v, Point3 p)
      => new Point3(v % p.X, v % p.Y, v % p.Z);
    public static Point3 operator %(double v, Point3 p)
      => new Point3((int)(v % p.X), (int)(v % p.Y), (int)(v % p.Z));

    public static Point3 operator &(Point3 p1, Point3 p2)
      => new Point3(p1.X & p2.X, p1.Y & p2.Y, p1.Z & p2.Z);
    public static Point3 operator &(Point3 p, int v)
      => new Point3(p.X & v, p.Y & v, p.Z & v);
    public static Point3 operator &(int v, Point3 p)
      => new Point3(v & p.X, v & p.Y, v & p.Z);

    public static Point3 operator |(Point3 p1, Point3 p2)
      => new Point3(p1.X | p2.X, p1.Y | p2.Y, p1.Z | p2.Z);
    public static Point3 operator |(Point3 p, int v)
      => new Point3(p.X | v, p.Y | v, p.Z | v);
    public static Point3 operator |(int v, Point3 p)
      => new Point3(v | p.X, v | p.Y, v | p.Z);

    public static Point3 operator ^(Point3 p1, Point3 p2)
      => new Point3(p1.X ^ p2.X, p1.Y ^ p2.Y, p1.Z ^ p2.Z);
    public static Point3 operator ^(Point3 p, int v)
      => new Point3(p.X ^ v, p.Y ^ v, p.Z ^ v);
    public static Point3 operator ^(int v, Point3 p)
      => new Point3(v ^ p.X, v ^ p.Y, v ^ p.Z);

    public static Point3 operator <<(Point3 p, int v)
      => new Point3(p.X << v, p.Y << v, p.Z << v);
    public static Point3 operator >>(Point3 p, int v)
      => new Point3(p.X >> v, p.Y >> v, p.Z >> v);
    #endregion Overloaded operators

    #region Implemented interfaces
    // System.IComparable
    public int CompareTo(Point3 other)
      => X < other.X ? -1 : X > other.X ? 1 : Y < other.Y ? -1 : Y > other.Y ? 1 : Z < other.Z ? -1 : Z > other.Z ? 1 : 0;

    // System.IEquatable
    public bool Equals(Point3 other)
      => X == other.X && Y == other.Y && Z == other.Z;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Point3 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y, Z);
    public override string ToString()
      => $"<{GetType().Name}: {X}, {Y}, {Z}>";
    #endregion Object overrides
  }
}
