//using System.Linq;

namespace Flux.Media.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Point2
    : System.IComparable<Point2>, System.IEquatable<Point2>
  {
    public static readonly Point2 Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] public readonly int X;
    [System.Runtime.InteropServices.FieldOffset(4)] public readonly int Y;

    public Point2(int value)
      : this(value, value) { }
    public Point2(int x, int y)
    {
      X = x;
      Y = y;
    }
    public Point2(int[] array, int startIndex)
    {
      if (array is null || array.Length - startIndex < 2) throw new System.ArgumentOutOfRangeException(nameof(array));

      X = array[startIndex++];
      Y = array[startIndex];
    }

    #region Static Instances
    /// <summary>Returns the vector (1,0,0).</summary>
    public static readonly Point2 UnitX = new Point2(1, 0);
    /// <summary>Returns the vector (0,1,0).</summary>
    public static readonly Point2 UnitY = new Point2(0, 1);

    /// <summary>Returns the vector (0,0).</summary>
    public static readonly Point2 Zero;

    ///// <summary>Returns the vector (0,1).</summary>
    //public static Point2 North => new Point2(0, 1);
    ///// <summary>Returns the vector (1,1).</summary>
    //public static Point2 NorthEast => new Point2(1, 1);
    ///// <summary>Returns the vector (1,0).</summary>
    //public static Point2 East => new Point2(1, 0);
    ///// <summary>Returns the vector (1,-1).</summary>
    //public static Point2 SouthEast => new Point2(1, -1);
    ///// <summary>Returns the vector (0,-1).</summary>
    //public static Point2 South => new Point2(0, -1);
    ///// <summary>Returns the vector (-1,-1).</summary>
    //public static Point2 SouthWest => new Point2(-1, -1);
    ///// <summary>Returns the vector (-1,0).</summary>
    //public static Point2 West => new Point2(-1, 0);
    ///// <summary>Returns the vector (-1,1).</summary>
    //public static Point2 NorthWest => new Point2(-1, 1);
    #endregion Static Instances

    /// <summary>Convert the vector to a unique index using the length of the X axis.</summary>
    public (string column, string row) ToLabels(System.Collections.Generic.IList<string> columnLabels, System.Collections.Generic.IList<string> rowLabels)
      => (columnLabels[X], rowLabels[Y]);
    public int ToUniqueIndex(int lengthX)
      => X * Y * lengthX;
    public System.Numerics.Vector2 ToVector2()
      => new System.Numerics.Vector2(X, Y);
    public int[] ToArray()
      => new int[] { X, Y };

    #region Static members
    /// <summary>Create a new vector with the sum from the vector added to the other.</summary>
    public static Point2 Add(Point2 p1, Point2 p2)
      => new Point2(p1.X + p2.X, p1.Y + p2.Y);
    /// <summary>Create a new vector with the sum from each member added to the value.</summary>
    public static Point2 Add(Point2 p, int value)
      => new Point2(p.X + value, p.Y + value);
    /// <summary>Create a new vector by left bit shifting the members of the vector by the specified count.</summary>
    public static Point2 LeftShift(Point2 p, int count)
      => new Point2(p.X << count, p.Y << count);
    /// <summary>Create a new vector by right bit shifting the members of the vector by the specified count.</summary>
    public static Point2 RightShift(Point2 p, int count)
      => new Point2(p.X >> count, p.Y >> count);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the other vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#AND"/>
    public static Point2 BitwiseAnd(Point2 p1, Point2 p2)
      => new Point2(p1.X & p2.X, p1.Y & p2.Y);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the value.</summary>
    public static Point2 BitwiseAnd(Point2 p, int value)
      => new Point2(p.X & value, p.Y & value);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#XOR"/>
    public static Point2 Xor(Point2 p1, Point2 p2)
      => new Point2(p1.X ^ p2.X, p1.Y ^ p2.Y);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the value.</summary>
    public static Point2 Xor(Point2 p, int value)
      => new Point2(p.X ^ value, p.Y ^ value);
    /// <summary>Create a new vector by performing a NOT operation on each member of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#NOT"/>
    public static Point2 OnesComplement(Point2 p)
      => new Point2(~p.X, ~p.Y); // .NET performs a one's complement (bitwise logical NOT) on integral types.
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#OR"/>
    public static Point2 BitwiseOr(Point2 p1, Point2 p2)
      => new Point2(p1.X | p2.X, p1.Y | p2.Y);
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the value.</summary>
    public static Point2 BitwiseOr(Point2 p, int value)
      => new Point2(p.X | value, p.Y | value);
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(Point2 p1, Point2 p2)
      => System.Math.Max(System.Math.Abs(p2.X - p1.X), System.Math.Abs(p2.Y - p1.Y));
    /// <summary>Computes the closest cartesian coordinate vector at the specified angle and distance from the vector.</summary>
    public static Point2 ComputeVector(Point2 p, double angle, double distance = 1)
      => new Point2((int)(distance * System.Math.Sin(angle)) + p.X, (int)(distance * System.Math.Cos(angle)) + p.Y);
    /// <summary>Create a new vector with each member subtracted by 1.</summary>
    public static Point2 Decrement(Point2 p1)
      => Subtract(p1, 1);
    /// <summary>Create a new vector with the quotient from the vector divided by the other.</summary>
    public static Point2 Divide(Point2 p1, Point2 p2)
      => new Point2(p1.X / p2.X, p1.Y / p2.Y);
    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
    public static Point2 Divide(Point2 p, int value)
      => new Point2(p.X / value, p.Y / value);
    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
    public static Point2 Divide(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(p.X / value), System.Convert.ToInt32(p.Y / value));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point2 DivideCeiling(Point2 p, double value)
      => new Point2((int)(p.X / value), (int)(p.Y / value));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point2 DivideFloor(Point2 p, double value)
      => new Point2((int)(p.X / value), (int)(p.Y / value));
    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(Point2 p1, Point2 p2)
      => p1.X * p2.X + p1.Y * p2.Y;
    /// <summary>Compute the euclidean distance of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(Point2 p1, Point2 p2)
      => GetLength(p1 - p2);
    /// <summary>Compute the euclidean distance squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceSquare(Point2 p1, Point2 p2)
      => GetLengthSquared(p1 - p2);
    /// <summary>Create a new vector from the labels and label definitions.</summary>
    public static Point2 FromLabels(string column, string row, System.Collections.Generic.IList<string> columnLabels, System.Collections.Generic.IList<string> rowLabels)
      => new Point2(columnLabels?.IndexOf(column) ?? throw new System.ArgumentOutOfRangeException(nameof(column)), rowLabels?.IndexOf(row) ?? throw new System.ArgumentOutOfRangeException(nameof(column)));
    /// <summary>Create a new vector from the index and the length of the X axis.</summary>
    public static Point2 FromUniqueIndex(int index, int lengthX)
      => new Point2(index % lengthX, index / lengthX);
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLength(in Point2 p)
      => System.Math.Sqrt(p.X * p.X + p.Y * p.Y);
    /// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLengthSquared(in Point2 p)
      => p.X * p.X + p.Y * p.Y;
    /// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static System.Collections.Generic.IEnumerable<Point2> GetQuadrantCenterVectors(Point2 source, Size2 subQuadrant)
    {
      yield return new Point2(source.X + subQuadrant.Width, source.Y + subQuadrant.Height);
      yield return new Point2(source.X - subQuadrant.Width, source.Y + subQuadrant.Height);
      yield return new Point2(source.X - subQuadrant.Width, source.Y - subQuadrant.Height);
      yield return new Point2(source.X + subQuadrant.Width, source.Y - subQuadrant.Height);
    }
    /// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static int GetQuadrantNumber(Point2 source, in Point2 center)
      => (source.X >= center.X ? 1 : 0) + (source.Y >= center.Y ? 2 : 0);
    /// <summary>Create a new vector with 1 added to each member.</summary>
    public static Point2 Increment(in Point2 p1)
      => Add(p1, 1);
    public static Point2 InterpolateCosine(Point2 y1, Point2 y2, double mu)
      => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2
      ? (y1 * (1.0 - mu2) + y2 * mu2)
      : throw new System.ArgumentNullException(nameof(mu));
    public static Point2 InterpolateCubic(Point2 y0, Point2 y1, Point2 y2, Point2 y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
    public static Point2 InterpolateHermite(Point2 y0, Point2 y1, Point2 y2, Point2 y3, double mu, double tension, double bias)
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
    public static Point2 InterpolateLinear(Point2 y1, Point2 y2, double mu)
      => y1 * (1 - mu) + y2 * mu;
    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int ManhattanDistance(Point2 p1, Point2 p2)
      => System.Math.Abs(p2.X - p1.X) + System.Math.Abs(p2.Y - p1.Y);
    /// <summary>Create a new vector with the product from the vector multiplied with the other.</summary>
    public static Point2 Multiply(Point2 p1, Point2 p2)
      => new Point2(p1.X * p2.X, p1.Y * p2.Y);
    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
    public static Point2 Multiply(Point2 p, int value)
      => new Point2(p.X * value, p.Y * value);
    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
    public static Point2 Multiply(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(p.X * value), System.Convert.ToInt32(p.Y * value));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point2 MultiplyCeiling(Point2 p, double value)
      => new Point2((int)System.Math.Ceiling(p.X * value), (int)System.Math.Ceiling(p.Y * value));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point2 MultiplyFloor(Point2 p, double value)
      => new Point2((int)System.Math.Floor(p.X * value), (int)System.Math.Floor(p.Y * value));
    /// <summary>Create a new vector from the additive inverse, i.e. a negation of the member in the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Additive_inverse"/>
    public static Point2 Negate(in Point2 p)
      => new Point2(-p.X, -p.Y); // Negate the members of the vector.
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
    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static Point2 PerpendicularCcw(Point2 p)
      => new Point2(-p.Y, p.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static Point2 PerpendicularCw(Point2 p)
      => new Point2(p.Y, -p.X);
    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static Point2 Random(int toExlusiveX, int toExclusiveY)
      => new Point2(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY));
    /// <summary>Create a new random vector in the range [(0, 0), toExclusive] using the crypto-grade rng.</summary>
    public static Point2 Random(Point2 toExclusive)
      => new Point2(Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.X), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.Y));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static Point2 RandomZero(int toExlusiveX, int toExclusiveY)
      => new Point2(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX * 2) - toExlusiveX, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY * 2) - toExclusiveY);
    /// <summary>Create a new random vector in the range [-toExclusive, toExclusive] using the crypto-grade rng.</summary>
    public static Point2 RandomZero(Point2 toExclusive)
      => RandomZero(toExclusive.X, toExclusive.Y);
    /// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
    public static Point2 Remainder(Point2 p1, Point2 p2)
      => new Point2(p1.X % p2.X, p1.Y % p2.Y);
    /// <summary>Create a new vector with the remainder from each member divided by the value. Integer math is used.</summary>
    public static Point2 Remainder(Point2 p, int value)
      => new Point2(p.X % value, p.Y % value);
    /// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
    public static Point2 Remainder(Point2 p, double value)
      => new Point2((int)(p.X % value), (int)(p.Y % value));
    /// <summary>Create a new vector with the difference from the vector subtracted by the other.</summary>
    public static Point2 Subtract(Point2 p1, Point2 p2)
      => new Point2(p1.X - p2.X, p1.Y - p2.Y);
    /// <summary>Create a new vector with the difference from each member subtracted by the value.</summary>
    public static Point2 Subtract(Point2 p, int value)
      => new Point2(p.X - value, p.Y - value);
    /// <summary>Creates a <see cref='Size2'/> from a <see cref='Point2'/>.</summary>
    public static Size2 ToSize2(Point2 point)
      => new Size2(point.X, point.Y);
    #endregion Static members

    #region "Unique" index
    /// <summary>Convert an index to a 3D vector, based on the specified lengths of axes.</summary>
    public static Point2 FromUniqueIndex(long index, Size2 bounds)
      => new Point2((int)(index % bounds.Width), (int)(index / bounds.Width));

    /// <summary>Converts the vector to an index, based on the specified lengths of axes.</summary>
    public static long ToUniqueIndex(Point2 point, Size2 bounds)
      => point.X + (point.Y * bounds.Width);
    #endregion "Unique" index

    #region Overloaded operators
    public static Point2 operator -(Point2 v) => Negate(v);

    public static Point2 operator ~(Point2 v) => OnesComplement(v);

    public static Point2 operator --(Point2 v) => Subtract(v, 1);
    public static Point2 operator ++(Point2 v) => Add(v, 1);

    public static Point2 operator +(Point2 p1, Point2 p2) => Add(p1, p2);
    public static Point2 operator +(Point2 p1, int v) => Add(p1, v);
    public static Point2 operator +(int p1, Point2 p2) => Add(p2, p1);

    public static Point2 operator -(Point2 p1, Point2 p2) => Subtract(p1, p2);
    public static Point2 operator -(Point2 p1, int v) => Subtract(p1, v);

    public static Point2 operator *(Point2 p1, Point2 p2) => Multiply(p1, p2);
    public static Point2 operator *(Point2 p1, int v) => Multiply(p1, v);
    public static Point2 operator *(int v, Point2 p2) => Multiply(p2, v);
    public static Point2 operator *(Point2 p1, double v) => Multiply(p1, v);
    public static Point2 operator *(double v, Point2 p2) => Multiply(p2, v);

    public static Point2 operator /(Point2 p1, Point2 p2) => Divide(p1, p2);
    public static Point2 operator /(Point2 p1, int v) => Divide(p1, v);
    public static Point2 operator /(int v, Point2 p1) => Divide(p1, v);
    public static Point2 operator /(Point2 p1, double v) => Divide(p1, v);
    public static Point2 operator /(double v, Point2 p1) => Divide(p1, v);

    public static Point2 operator %(Point2 p1, int v) => Remainder(p1, v);
    public static Point2 operator %(Point2 p1, double v) => Remainder(p1, v);

    public static Point2 operator &(Point2 p1, Point2 p2) => BitwiseAnd(p1, p2);
    public static Point2 operator &(Point2 p1, int v) => BitwiseAnd(p1, v);

    public static Point2 operator |(Point2 p1, Point2 p2) => BitwiseOr(p1, p2);
    public static Point2 operator |(Point2 p1, int v) => BitwiseOr(p1, v);

    public static Point2 operator ^(Point2 p1, Point2 p2) => Xor(p1, p2);
    public static Point2 operator ^(Point2 p1, int v) => Xor(p1, v);

    public static Point2 operator <<(Point2 p1, int v) => LeftShift(p1, v);
    public static Point2 operator >>(Point2 p1, int v) => RightShift(p1, v);

    public static bool operator ==(Point2 p1, Point2 p2) => p1.Equals(p2);
    public static bool operator !=(Point2 p1, Point2 p2) => !p1.Equals(p2);
    #endregion Overloaded operators

    // System.IComparable
    public int CompareTo(Point2 other)
      => X < other.X ? -1 : X > other.X ? 1 : Y < other.Y ? -1 : Y > other.Y ? 1 : 0;

    // System.IEquatable
    public bool Equals(Point2 other)
      => X == other.X && Y == other.Y;

    // Overrides
    public override bool Equals(object? obj)
       => obj is Point2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y);
    public override string ToString()
      => $"<{GetType().Name}: {X}, {Y}>";
  }
}
