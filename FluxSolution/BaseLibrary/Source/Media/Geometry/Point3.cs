namespace Flux.Media.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Point3
    : System.IEquatable<Point3>
  {
    public static readonly Point3 Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private int m_x;
    [System.Runtime.InteropServices.FieldOffset(4)] private int m_y;
    [System.Runtime.InteropServices.FieldOffset(8)] private int m_z;

    public int X { get => m_x; set => m_x = value; }
    public int Y { get => m_y; set => m_y = value; }
    public int Z { get => m_z; set => m_z = value; }

    public Point3(int value)
      : this(value, value, value) { }
    public Point3(Point2 value, int z)
      : this(value.X, value.Y, z) { }
    public Point3(int x, int y)
      : this(x, y, 0) { }
    public Point3(int x, int y, int z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }
    public Point3(int[] array, int startIndex)
    {
      if (array is null) throw new System.ArgumentNullException(nameof(array));

      if (array.Length - startIndex < 3) throw new System.ArgumentOutOfRangeException(nameof(array));

      m_x = array[startIndex++];
      m_y = array[startIndex++];
      m_z = array[startIndex];
    }

    /// <summary>Convert the vector to a unique index using the length of the m_x and the m_y axes.</summary>
    public int ToUniqueIndex(int lengthX, int lengthY)
      => m_x + m_y * lengthX + m_z * lengthX * lengthY;
    public System.Numerics.Vector3 ToVector3()
      => new System.Numerics.Vector3(m_x, m_y, m_z);
    public int[] ToArray()
      => new int[] { m_x, m_y, m_z };

    #region Static members
    /// <summary>Create a new vector with the sum from the vector added by the other.</summary>
    public static Point3 Add(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_x + p2.m_x, p1.m_y + p2.m_y, p1.m_z + p2.m_z);
    /// <summary>Create a new vector with the sum from each member added to the value.</summary>
    public static Point3 Add(in Point3 p, int value)
      => new Point3(p.m_x + value, p.m_y + value, p.m_z + value);
    /// <summary>Create a new vector by left bit shifting the members of the vector by the specified count.</summary>
    public static Point3 LeftShift(in Point3 p, int count)
      => new Point3(p.m_x << count, p.m_y << count, p.m_z << count);
    /// <summary>Create a new vector by right bit shifting the members of the vector by the specified count.</summary>
    public static Point3 RightShift(in Point3 p, int count)
      => new Point3(p.m_x << count, p.m_y << count, p.m_z << count);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the other vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#AND"/>
    public static Point3 BitwiseAnd(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_x & p2.m_x, p1.m_y & p2.m_y, p1.m_z & p2.m_z);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the value.</summary>
    public static Point3 BitwiseAnd(in Point3 p, int value)
      => new Point3(p.m_x & value, p.m_y & value, p.m_z & value);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#XOR"/>
    public static Point3 Xor(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_x ^ p2.m_x, p1.m_y ^ p2.m_y, p1.m_z ^ p2.m_z);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the value.</summary>
    public static Point3 Xor(in Point3 p, int value)
      => new Point3(p.m_x ^ value, p.m_y ^ value, p.m_z ^ value);
    /// <summary>Create a new vector by performing a NOT operation on each member of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#NOT"/>
    public static Point3 OnesComplement(in Point3 p)
      => new Point3(~p.m_x, ~p.m_y, ~p.m_z); // .NET performs a one's complement (bitwise logical NOT) on integral types.
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#OR"/>
    public static Point3 BitwiseOr(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_x | p2.m_x, p1.m_y | p2.m_y, p1.m_z | p2.m_z);
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the value.</summary>
    public static Point3 BitwiseOr(in Point3 p, int value)
      => new Point3(p.m_x | value, p.m_y | value, p.m_z | value);
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in Point3 p1, in Point3 p2)
      => Maths.Max(System.Math.Abs(p2.m_x - p1.m_x), System.Math.Abs(p2.m_y - p1.m_y), System.Math.Abs(p2.m_z - p1.m_z));
    /// <summary>Create a new vector by computing the cross product, i.e. cross(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cross_product"/>
    public static Point3 CrossProduct(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_y * p2.m_z - p1.m_z * p2.m_y, p1.m_z * p2.m_x - p1.m_x * p2.m_z, p1.m_x * p2.m_y - p1.m_y * p2.m_x);
    /// <summary>Create a new vector with each member subtracted by 1.</summary>
    public static Point3 Decrement(in Point3 p1)
      => Subtract(p1, 1);
    /// <summary>Create a new vector with the quotient from the vector divided by the other.</summary>
    public static Point3 Divide(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_x / p2.m_x, p1.m_y / p2.m_y, p1.m_z / p2.m_z);
    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
    public static Point3 Divide(in Point3 p, int value)
      => new Point3(p.m_x / value, p.m_y / value, p.m_z / value);
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point3 DivideCeiling(in Point3 p, double value)
      => new Point3((int)System.Math.Ceiling(p.m_x / value), (int)System.Math.Ceiling(p.m_y / value), (int)System.Math.Ceiling(p.m_z / value));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point3 DivideFloor(in Point3 p, double value)
      => new Point3((int)System.Math.Floor(p.m_x / value), (int)System.Math.Floor(p.m_y / value), (int)System.Math.Floor(p.m_z / value));
    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(in Point3 p1, in Point3 p2)
      => p1.m_x * p2.m_x + p1.m_y * p2.m_y + p1.m_z * p2.m_z;
    /// <summary>Compute the euclidean distance of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(in Point3 p1, in Point3 p2)
      => GetLength(p1 - p2);
    /// <summary>Compute the euclidean distance squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceSquare(in Point3 p1, in Point3 p2)
      => GetLengthSquared(p1 - p2);
    /// <summary>Create a new vector from the index and the length of the m_x and the length of the m_y axes.</summary>
    public static Point3 FromUniqueIndex(int index, int lengthX, int lengthY)
      => index % (lengthX * lengthY) is var irxy ? new Point3(irxy % lengthX, irxy / lengthX, index / (lengthX * lengthY)) : throw new System.ArgumentOutOfRangeException(nameof(index));
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLength(in Point3 p)
      => System.Math.Sqrt(p.m_x * p.m_x + p.m_y * p.m_y + p.m_z * p.m_z);
    /// <summary>Compute the length (or magnitude) squared (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLengthSquared(in Point3 p)
      => p.m_x * p.m_x + p.m_y * p.m_y + p.m_z * p.m_z;
    /// <summary>Create a new vector with 1 added to each member.</summary>
    public static Point3 Increment(in Point3 p1)
      => Add(p1, 1);
    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int ManhattanDistance(in Point3 p1, in Point3 p2)
      => System.Math.Abs(p2.m_x - p1.m_x) + System.Math.Abs(p2.m_y - p1.m_y) + System.Math.Abs(p2.m_z - p1.m_z);
    /// <summary>Create a new vector with the product from the vector multiplied with the other.</summary>
    public static Point3 Multiply(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_x * p2.m_x, p1.m_y * p2.m_y, p1.m_z * p2.m_z);
    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
    public static Point3 Multiply(in Point3 p, int value)
      => new Point3(p.m_x * value, p.m_y * value, p.m_z * value);
    /// <summary>Create a new vector with the ceiling(product) from each member multiplied with the value.</summary>
    public static Point3 MultiplyCeiling(in Point3 p, double value)
      => new Point3((int)System.Math.Ceiling(p.m_x * value), (int)System.Math.Ceiling(p.m_y * value), (int)System.Math.Ceiling(p.m_z * value));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point3 MultiplyFloor(in Point3 p, double value)
      => new Point3((int)System.Math.Floor(p.m_x * value), (int)System.Math.Floor(p.m_y * value), (int)System.Math.Floor(p.m_z * value));
    /// <summary>Create a new vector from the additive inverse, i.e. a negation of the members in the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Additive_inverse"/>
    public static Point3 Negate(in Point3 p)
      => new Point3(-p.m_x, -p.m_y, -p.m_z); // Negate the members of the vector.
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
      => new Point3(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveZ));
    /// <summary>Create a new random vector in the range [(0, 0), toExclusive] using the crypto-grade rng.</summary>
    public static Point3 Random(in Point3 toExclusive)
      => new Point3(Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.m_x), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.m_y));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static Point3 RandomZero(int toExlusiveX, int toExclusiveY, int toExclusiveZ)
      => new Point3(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX * 2) - toExlusiveX, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY * 2) - toExclusiveY, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveZ * 2) - toExclusiveZ);
    /// <summary>Create a new random vector in the range [-toExclusive, toExclusive] using the crypto-grade rng.</summary>
    public static Point3 RandomZero(in Point3 toExclusive)
      => RandomZero(toExclusive.m_x, toExclusive.m_y, toExclusive.m_z);
    /// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
    public static Point3 Remainder(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_x % p2.m_x, p1.m_y % p2.m_y, p1.m_z % p2.m_z);
    /// <summary>Create a new vector with the remainder from each member divided by the value.</summary>
    public static Point3 Remainder(in Point3 p, int value)
      => new Point3(p.m_x % value, p.m_y % value, p.m_z % value);
    /// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
    public static Point3 Remainder(in Point3 p, double value)
      => new Point3((int)(p.m_x % value), (int)(p.m_y % value), (int)(p.m_z % value));
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static int ScalarTripleProduct(in Point3 p1, in Point3 p2, in Point3 p3)
      => DotProduct(p1, CrossProduct(p2, p3));
    /// <summary>Create a new vector with the difference from the vector subtracted by the other.</summary>
    public static Point3 Subtract(in Point3 p1, in Point3 p2)
      => new Point3(p1.m_x - p2.m_x, p1.m_y - p2.m_y, p1.m_z - p2.m_z);
    /// <summary>Create a new vector with the difference from each member subtracted by the value.</summary>
    public static Point3 Subtract(in Point3 p, int value)
      => new Point3(p.m_x - value, p.m_y - value, p.m_z - value);
    /// <summary>Creates a <see cref='Size3'/> from a <see cref='Point3'/>.</summary>
    public static Size3 ToSize3(Point3 point)
      => new Size3(point.m_x, point.m_y, point.m_z);
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static Point3 VectorTripleProduct(in Point3 p1, in Point3 p2, in Point3 p3)
      => CrossProduct(p1, CrossProduct(p2, p3));
    #endregion Static members

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
      => point.m_x + (point.m_y * bounds.Width) + (point.m_z * bounds.Width * bounds.Height);
    #endregion "Unique" index

    #region Overloaded operators
    public static Point3 operator -(in Point3 v) => Negate(v);

    public static Point3 operator ~(in Point3 v) => OnesComplement(v);

    public static Point3 operator --(in Point3 v) => Decrement(v);
    public static Point3 operator ++(in Point3 v) => Increment(v);

    public static Point3 operator +(in Point3 p1, in Point3 p2) => Add(p1, p2);
    public static Point3 operator +(in Point3 p1, in int i) => Add(p1, i);

    public static Point3 operator -(in Point3 p1, in Point3 p2) => Subtract(p1, p2);
    public static Point3 operator -(in Point3 p1, in int i) => Subtract(p1, i);

    public static Point3 operator *(in Point3 p1, in Point3 p2) => Multiply(p1, p2);
    public static Point3 operator *(in Point3 p1, in int i) => Multiply(p1, i);

    public static Point3 operator /(in Point3 p1, in Point3 p2) => Divide(p1, p2);
    public static Point3 operator /(in Point3 p1, in int i) => Divide(p1, i);

    public static Point3 operator %(in Point3 p1, in int i) => Remainder(p1, i);
    public static Point3 operator %(in Point3 p1, in double d) => Remainder(p1, d);

    public static Point3 operator &(in Point3 p1, in Point3 p2) => BitwiseAnd(p1, p2);
    public static Point3 operator &(in Point3 p1, in int i) => BitwiseAnd(p1, i);

    public static Point3 operator |(in Point3 p1, in Point3 p2) => BitwiseOr(p1, p2);
    public static Point3 operator |(in Point3 p1, in int i) => BitwiseOr(p1, i);

    public static Point3 operator ^(in Point3 p1, in Point3 p2) => Xor(p1, p2);
    public static Point3 operator ^(in Point3 p1, in int i) => Xor(p1, i);

    public static Point3 operator <<(in Point3 p1, in int i) => LeftShift(p1, i);
    public static Point3 operator >>(in Point3 p1, in int i) => RightShift(p1, i);

    public static bool operator ==(in Point3 p1, in Point3 p2) => p1.Equals(p2);
    public static bool operator !=(in Point3 p1, in Point3 p2) => !p1.Equals(p2);
    #endregion Overloaded operators

    // System.IEquatable<Point3>
    public bool Equals(Point3 other)
      => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z;

    // Overrides
    public override bool Equals(object? obj)
      => obj is Point3 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y, m_z);
    public override string ToString()
      => $"<Point {m_x}, {m_y}, {m_z}>";
  }
}
