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
    public int ToUniqueIndex(int lengthX, int lengthY)
      => X + Y * lengthX + Z * lengthX * lengthY;
    public System.Numerics.Vector3 ToVector3()
      => new System.Numerics.Vector3(X, Y, Z);
    public int[] ToArray()
      => new int[] { X, Y, Z };

    #region Static methods
    /// <summary>Create a new vector with the sum from the vector added by the other.</summary>
    public static Point3 Add(in Point3 p1, in Point3 p2)
      => new Point3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
    /// <summary>Create a new vector with the sum from each member added to the value.</summary>
    public static Point3 Add(in Point3 p, int value)
      => new Point3(p.X + value, p.Y + value, p.Z + value);
    /// <summary>Create a new vector by left bit shifting the members of the vector by the specified count.</summary>
    public static Point3 LeftShift(in Point3 p, int count)
      => new Point3(p.X << count, p.Y << count, p.Z << count);
    /// <summary>Create a new vector by right bit shifting the members of the vector by the specified count.</summary>
    public static Point3 RightShift(in Point3 p, int count)
      => new Point3(p.X << count, p.Y << count, p.Z << count);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the other vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#AND"/>
    public static Point3 BitwiseAnd(in Point3 p1, in Point3 p2)
      => new Point3(p1.X & p2.X, p1.Y & p2.Y, p1.Z & p2.Z);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the value.</summary>
    public static Point3 BitwiseAnd(in Point3 p, int value)
      => new Point3(p.X & value, p.Y & value, p.Z & value);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#XOR"/>
    public static Point3 Xor(in Point3 p1, in Point3 p2)
      => new Point3(p1.X ^ p2.X, p1.Y ^ p2.Y, p1.Z ^ p2.Z);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the value.</summary>
    public static Point3 Xor(in Point3 p, int value)
      => new Point3(p.X ^ value, p.Y ^ value, p.Z ^ value);
    /// <summary>Create a new vector by performing a NOT operation on each member of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#NOT"/>
    public static Point3 OnesComplement(in Point3 p)
      => new Point3(~p.X, ~p.Y, ~p.Z); // .NET performs a one's complement (bitwise logical NOT) on integral types.
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#OR"/>
    public static Point3 BitwiseOr(in Point3 p1, in Point3 p2)
      => new Point3(p1.X | p2.X, p1.Y | p2.Y, p1.Z | p2.Z);
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the value.</summary>
    public static Point3 BitwiseOr(in Point3 p, int value)
      => new Point3(p.X | value, p.Y | value, p.Z | value);
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in Point3 p1, in Point3 p2)
      => Maths.Max(System.Math.Abs(p2.X - p1.X), System.Math.Abs(p2.Y - p1.Y), System.Math.Abs(p2.Z - p1.Z));
    /// <summary>Create a new vector by computing the cross product, i.e. cross(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cross_product"/>
    public static Point3 CrossProduct(in Point3 p1, in Point3 p2)
      => new Point3(p1.Y * p2.Z - p1.Z * p2.Y, p1.Z * p2.X - p1.X * p2.Z, p1.X * p2.Y - p1.Y * p2.X);
    /// <summary>Create a new vector with each member subtracted by 1.</summary>
    public static Point3 Decrement(in Point3 p1)
      => Subtract(p1, 1);
    /// <summary>Create a new vector with the quotient from the vector divided by the other.</summary>
    public static Point3 Divide(in Point3 p1, in Point3 p2)
      => new Point3(p1.X / p2.X, p1.Y / p2.Y, p1.Z / p2.Z);
    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
    public static Point3 Divide(in Point3 p, int value)
      => new Point3(p.X / value, p.Y / value, p.Z / value);
    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
    public static Point3 Divide(in Point3 p, double value)
      => new Point3(System.Convert.ToInt32(p.X / value), System.Convert.ToInt32(p.Y / value), System.Convert.ToInt32(p.Z / value));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point3 DivideCeiling(in Point3 p, double value)
      => new Point3((int)System.Math.Ceiling(p.X / value), (int)System.Math.Ceiling(p.Y / value), (int)System.Math.Ceiling(p.Z / value));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point3 DivideFloor(in Point3 p, double value)
      => new Point3((int)System.Math.Floor(p.X / value), (int)System.Math.Floor(p.Y / value), (int)System.Math.Floor(p.Z / value));
    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(in Point3 p1, in Point3 p2)
      => p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
    /// <summary>Compute the euclidean distance of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(in Point3 p1, in Point3 p2)
      => GetLength(p1 - p2);
    /// <summary>Compute the euclidean distance squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceSquare(in Point3 p1, in Point3 p2)
      => GetLengthSquared(p1 - p2);
    /// <summary>Create a new vector from the index and the length of the X and the length of the Y axes.</summary>
    public static Point3 FromUniqueIndex(int index, int lengthX, int lengthY)
      => index % (lengthX * lengthY) is var irxy ? new Point3(irxy % lengthX, irxy / lengthX, index / (lengthX * lengthY)) : throw new System.ArgumentOutOfRangeException(nameof(index));
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLength(in Point3 p)
      => System.Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
    /// <summary>Compute the length (or magnitude) squared (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLengthSquared(in Point3 p)
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
    public static int GetOctantNumber(Point3 source, in Point3 center)
      => (source.X >= center.X ? 1 : 0) + (source.Y >= center.Y ? 2 : 0) + (source.Z >= center.Z ? 4 : 0);
    /// <summary>Create a new vector with 1 added to each member.</summary>
    public static Point3 Increment(in Point3 p1)
      => Add(p1, 1);

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

      return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
    }
    public static Point3 InterpolateLinear(Point3 y1, Point3 y2, double mu)
      => y1 * (1 - mu) + y2 * mu;

    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int ManhattanDistance(in Point3 p1, in Point3 p2)
      => System.Math.Abs(p2.X - p1.X) + System.Math.Abs(p2.Y - p1.Y) + System.Math.Abs(p2.Z - p1.Z);
    /// <summary>Create a new vector with the product from the vector multiplied with the other.</summary>
    public static Point3 Multiply(in Point3 p1, in Point3 p2)
      => new Point3(p1.X * p2.X, p1.Y * p2.Y, p1.Z * p2.Z);
    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
    public static Point3 Multiply(in Point3 p, int value)
      => new Point3(p.X * value, p.Y * value, p.Z * value);
    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
    public static Point3 Multiply(in Point3 p, double value)
      => new Point3(System.Convert.ToInt32(p.X * value), System.Convert.ToInt32(p.Y * value), System.Convert.ToInt32(p.Z * value));
    /// <summary>Create a new vector with the ceiling(product) from each member multiplied with the value.</summary>
    public static Point3 MultiplyCeiling(in Point3 p, double value)
      => new Point3((int)System.Math.Ceiling(p.X * value), (int)System.Math.Ceiling(p.Y * value), (int)System.Math.Ceiling(p.Z * value));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point3 MultiplyFloor(in Point3 p, double value)
      => new Point3((int)System.Math.Floor(p.X * value), (int)System.Math.Floor(p.Y * value), (int)System.Math.Floor(p.Z * value));
    /// <summary>Create a new vector from the additive inverse, i.e. a negation of the members in the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Additive_inverse"/>
    public static Point3 Negate(in Point3 p)
      => new Point3(-p.X, -p.Y, -p.Z); // Negate the members of the vector.
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
    public static Point3 Random(int toExlusiveX, int toExclusiveY, int toExclusiveZ)
      => new Point3(Flux.Random.NumberGenerator.Crypto.Next(toExlusiveX), Flux.Random.NumberGenerator.Crypto.Next(toExclusiveY), Flux.Random.NumberGenerator.Crypto.Next(toExclusiveZ));
    /// <summary>Create a new random vector in the range [(0, 0), toExclusive] using the crypto-grade rng.</summary>
    public static Point3 Random(in Point3 toExclusive)
      => new Point3(Flux.Random.NumberGenerator.Crypto.Next(toExclusive.X), Flux.Random.NumberGenerator.Crypto.Next(toExclusive.Y));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static Point3 RandomZero(int toExlusiveX, int toExclusiveY, int toExclusiveZ)
      => new Point3(Flux.Random.NumberGenerator.Crypto.Next(toExlusiveX * 2) - toExlusiveX, Flux.Random.NumberGenerator.Crypto.Next(toExclusiveY * 2) - toExclusiveY, Flux.Random.NumberGenerator.Crypto.Next(toExclusiveZ * 2) - toExclusiveZ);
    /// <summary>Create a new random vector in the range [-toExclusive, toExclusive] using the crypto-grade rng.</summary>
    public static Point3 RandomZero(in Point3 toExclusive)
      => RandomZero(toExclusive.X, toExclusive.Y, toExclusive.Z);
    /// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
    public static Point3 Remainder(in Point3 p1, in Point3 p2)
      => new Point3(p1.X % p2.X, p1.Y % p2.Y, p1.Z % p2.Z);
    /// <summary>Create a new vector with the remainder from each member divided by the value.</summary>
    public static Point3 Remainder(in Point3 p, int value)
      => new Point3(p.X % value, p.Y % value, p.Z % value);
    /// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
    public static Point3 Remainder(in Point3 p, double value)
      => new Point3((int)(p.X % value), (int)(p.Y % value), (int)(p.Z % value));
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static int ScalarTripleProduct(in Point3 p1, in Point3 p2, in Point3 p3)
      => DotProduct(p1, CrossProduct(p2, p3));
    /// <summary>Create a new vector with the difference from the vector subtracted by the other.</summary>
    public static Point3 Subtract(in Point3 p1, in Point3 p2)
      => new Point3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
    /// <summary>Create a new vector with the difference from each member subtracted by the value.</summary>
    public static Point3 Subtract(in Point3 p, int value)
      => new Point3(p.X - value, p.Y - value, p.Z - value);
    /// <summary>Creates a <see cref='Size3'/> from a <see cref='Point3'/>.</summary>
    public static Size3 ToSize3(Point3 point)
      => new Size3(point.X, point.Y, point.Z);
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static Point3 VectorTripleProduct(in Point3 p1, in Point3 p2, in Point3 p3)
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
    public static bool operator ==(in Point3 p1, in Point3 p2)
      => p1.Equals(p2);
    public static bool operator !=(in Point3 p1, in Point3 p2)
      => !p1.Equals(p2);

    public static Point3 operator -(in Point3 v) => Negate(v);

    public static Point3 operator ~(in Point3 v) => OnesComplement(v);

    public static Point3 operator --(in Point3 v) => Decrement(v);
    public static Point3 operator ++(in Point3 v) => Increment(v);

    public static Point3 operator +(in Point3 p1, in Point3 p2) => Add(p1, p2);
    public static Point3 operator +(in Point3 p1, int v) => Add(p1, v);

    public static Point3 operator -(in Point3 p1, in Point3 p2) => Subtract(p1, p2);
    public static Point3 operator -(in Point3 p1, int v) => Subtract(p1, v);

    public static Point3 operator *(in Point3 p1, in Point3 p2) => Multiply(p1, p2);
    public static Point3 operator *(in Point3 p1, int v) => Multiply(p1, v);
    public static Point3 operator *(int v, in Point3 p1) => Multiply(p1, v);
    public static Point3 operator *(in Point3 p1, double v) => Multiply(p1, v);
    public static Point3 operator *(double v, in Point3 p1) => Multiply(p1, v);

    public static Point3 operator /(in Point3 p1, in Point3 p2) => Divide(p1, p2);
    public static Point3 operator /(in Point3 p1, int v) => Divide(p1, v);
    public static Point3 operator /(int v, in Point3 p1) => Divide(p1, v);
    public static Point3 operator /(in Point3 p1, double v) => Divide(p1, v);
    public static Point3 operator /(double v, in Point3 p1) => Divide(p1, v);

    public static Point3 operator %(in Point3 p1, int v) => Remainder(p1, v);
    public static Point3 operator %(in Point3 p1, double v) => Remainder(p1, v);

    public static Point3 operator &(in Point3 p1, in Point3 p2) => BitwiseAnd(p1, p2);
    public static Point3 operator &(in Point3 p1, int v) => BitwiseAnd(p1, v);

    public static Point3 operator |(in Point3 p1, in Point3 p2) => BitwiseOr(p1, p2);
    public static Point3 operator |(in Point3 p1, int v) => BitwiseOr(p1, v);

    public static Point3 operator ^(in Point3 p1, in Point3 p2) => Xor(p1, p2);
    public static Point3 operator ^(in Point3 p1, int v) => Xor(p1, v);

    public static Point3 operator <<(in Point3 p1, int v) => LeftShift(p1, v);
    public static Point3 operator >>(in Point3 p1, int v) => RightShift(p1, v);
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
      => $"<Point {X}, {Y}, {Z}>";
    #endregion Object overrides
  }
}
