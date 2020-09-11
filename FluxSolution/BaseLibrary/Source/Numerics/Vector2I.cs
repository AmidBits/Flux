using System.Linq;

namespace Flux.Numerics
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static System.Collections.Generic.IEnumerable<Vector2I> GetQuadrantCenterVectors(this Vector2I source, int subQuadrantSizeOfX, int subQuadrantSizeOfY)
    {
      yield return new Vector2I(source.X + subQuadrantSizeOfX, source.Y + subQuadrantSizeOfY);
      yield return new Vector2I(source.X - subQuadrantSizeOfX, source.Y + subQuadrantSizeOfY);
      yield return new Vector2I(source.X - subQuadrantSizeOfX, source.Y - subQuadrantSizeOfY);
      yield return new Vector2I(source.X + subQuadrantSizeOfX, source.Y - subQuadrantSizeOfY);
    }
    /// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static int ToQuadrant(this Vector2I source, in Vector2I centerAxis) => ((source.X >= centerAxis.X ? 1 : 0) * 1) + ((source.Y >= centerAxis.Y ? 1 : 0) * 2);
  }

  public struct Vector2I
    : System.IEquatable<Vector2I>, System.IFormattable
  {
    public int X { get; set; }
    public int Y { get; set; }

    #region Static Instances
    /// <summary>Returns the vector (1,0,0).</summary>
    public static readonly Vector2I UnitX = new Vector2I(1, 0);
    /// <summary>Returns the vector (0,1,0).</summary>
    public static readonly Vector2I UnitY = new Vector2I(0, 1);

    /// <summary>Returns the vector (0,0).</summary>
    public static readonly Vector2I Zero;

    /// <summary>Returns the vector (0,1).</summary>
    public static Vector2I North => new Vector2I(0, 1);
    /// <summary>Returns the vector (1,1).</summary>
    public static Vector2I NorthEast => new Vector2I(1, 1);
    /// <summary>Returns the vector (1,0).</summary>
    public static Vector2I East => new Vector2I(1, 0);
    /// <summary>Returns the vector (1,-1).</summary>
    public static Vector2I SouthEast => new Vector2I(1, -1);
    /// <summary>Returns the vector (0,-1).</summary>
    public static Vector2I South => new Vector2I(0, -1);
    /// <summary>Returns the vector (-1,-1).</summary>
    public static Vector2I SouthWest => new Vector2I(-1, -1);
    /// <summary>Returns the vector (-1,0).</summary>
    public static Vector2I West => new Vector2I(-1, 0);
    /// <summary>Returns the vector (-1,1).</summary>
    public static Vector2I NorthWest => new Vector2I(-1, 1);
    #endregion Static Instances

    public Vector2I(int value)
      : this(value, value) { }
    public Vector2I(int x, int y)
    {
      X = x;
      Y = y;
    }
    public Vector2I(int[] array, int startIndex)
    {
      if (array is null || array.Length - startIndex < 2) throw new System.ArgumentOutOfRangeException(nameof(array));

      X = array[startIndex++];
      Y = array[startIndex];
    }

    /// <summary>Convert the vector to a unique index using the length of the X axis.</summary>
    public (string column, string row) ToLabels(System.Collections.Generic.IList<string> columnLabels, System.Collections.Generic.IList<string> rowLabels)
      => (columnLabels.ElementAt(X), rowLabels.ElementAt(Y));
    public int ToUniqueIndex(int lengthX)
      => X * Y * lengthX;
    public System.Numerics.Vector2 ToVector2()
      => new System.Numerics.Vector2(X, Y);
    public int[] ToArray()
      => new int[] { X, Y };

    #region Static members
    /// <summary>Create a new vector with the sum from the vector added to the other.</summary>
    public static Vector2I Add(in Vector2I v1, in Vector2I v2)
      => new Vector2I(v1.X + v2.X, v1.Y + v2.Y);
    /// <summary>Create a new vector with the sum from each member added to the value.</summary>
    public static Vector2I Add(in Vector2I v, in int value)
      => new Vector2I(v.X + value, v.Y + value);
    /// <summary>Create a new vector by left bit shifting the members of the vector by the specified count.</summary>
    public static Vector2I LeftShift(in Vector2I v, in int count)
      => new Vector2I(v.X << count, v.Y << count);
    /// <summary>Create a new vector by right bit shifting the members of the vector by the specified count.</summary>
    public static Vector2I RightShift(in Vector2I v, in int count)
      => new Vector2I(v.X >> count, v.Y >> count);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the other vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#AND"/>
    public static Vector2I BitwiseAnd(in Vector2I v1, in Vector2I v2)
      => new Vector2I(v1.X & v2.X, v1.Y & v2.Y);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the value.</summary>
    public static Vector2I BitwiseAnd(in Vector2I v, in int value)
      => new Vector2I(v.X & value, v.Y & value);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#XOR"/>
    public static Vector2I Xor(in Vector2I v1, in Vector2I v2)
      => new Vector2I(v1.X ^ v2.X, v1.Y ^ v2.Y);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the value.</summary>
    public static Vector2I Xor(in Vector2I v, in int value)
      => new Vector2I(v.X ^ value, v.Y ^ value);
    /// <summary>Create a new vector by performing a NOT operation on each member of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#NOT"/>
    public static Vector2I OnesComplement(in Vector2I v)
      => new Vector2I(~v.X, ~v.Y); // .NET performs a one's complement (bitwise logical NOT) on integral types.
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#OR"/>
    public static Vector2I BitwiseOr(in Vector2I v1, in Vector2I v2)
      => new Vector2I(v1.X | v2.X, v1.Y | v2.Y);
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the value.</summary>
    public static Vector2I BitwiseOr(in Vector2I v, in int value)
      => new Vector2I(v.X | value, v.Y | value);
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in Vector2I v1, in Vector2I v2)
      => System.Math.Max(System.Math.Abs(v2.X - v1.X), System.Math.Abs(v2.Y - v1.Y));
    /// <summary>Computes the closest cartesian coordinate vector at the specified angle and distance from the vector.</summary>
    public static Vector2I ComputeVector(in Vector2I v, in double angle, in double distance = 1)
      => new Vector2I((int)(distance * System.Math.Sin(angle)) + v.X, (int)(distance * System.Math.Cos(angle)) + v.Y);
    /// <summary>Create a new vector with each member subtracted by 1.</summary>
    public static Vector2I Decrement(in Vector2I v1)
      => Subtract(v1, 1);
    /// <summary>Create a new vector with the quotient from the vector divided by the other.</summary>
    public static Vector2I Divide(in Vector2I v1, in Vector2I v2)
      => new Vector2I(v1.X / v2.X, v1.Y / v2.Y);
    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
    public static Vector2I Divide(in Vector2I v, in int value)
      => new Vector2I(v.X / value, v.Y / value);
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Vector2I DivideCeiling(in Vector2I v, in double value)
      => new Vector2I((int)(v.X / value), (int)(v.Y / value));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Vector2I DivideFloor(in Vector2I v, in double value)
      => new Vector2I((int)(v.X / value), (int)(v.Y / value));
    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(in Vector2I v1, in Vector2I v2)
      => v1.X * v2.X + v1.Y * v2.Y;
    /// <summary>Compute the euclidean distance of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(in Vector2I v1, in Vector2I v2)
      => GetLength(v1 - v2);
    /// <summary>Compute the euclidean distance squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceSquare(in Vector2I v1, in Vector2I v2)
      => GetLengthSquared(v1 - v2);
    /// <summary>Create a new vector from the labels and label definitions.</summary>
    public static Vector2I FromLabels(string column, string row, System.Collections.Generic.IList<string> columnLabels, System.Collections.Generic.IList<string> rowLabels)
      => new Vector2I(columnLabels?.IndexOf(column) ?? throw new System.ArgumentOutOfRangeException(nameof(column)), rowLabels?.IndexOf(row) ?? throw new System.ArgumentOutOfRangeException(nameof(column)));
    /// <summary>Create a new vector from the index and the length of the X axis.</summary>
    public static Vector2I FromUniqueIndex(int index, int lengthX)
      => new Vector2I(index % lengthX, index / lengthX);
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLength(in Vector2I v)
      => System.Math.Sqrt(v.X * v.X + v.Y * v.Y);
    /// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLengthSquared(in Vector2I v)
      => v.X * v.X + v.Y * v.Y;
    /// <summary>Create a new vector with 1 added to each member.</summary>
    public static Vector2I Increment(in Vector2I v1)
      => Add(v1, 1);
    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int ManhattanDistance(in Vector2I v1, in Vector2I v2)
      => System.Math.Abs(v2.X - v1.X) + System.Math.Abs(v2.Y - v1.Y);
    /// <summary>Create a new vector with the product from the vector multiplied with the other.</summary>
    public static Vector2I Multiply(in Vector2I v1, in Vector2I v2)
      => new Vector2I(v1.X * v2.X, v1.Y * v2.Y);
    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
    public static Vector2I Multiply(in Vector2I v, in int value)
      => new Vector2I(v.X * value, v.Y * value);
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Vector2I MultiplyCeiling(in Vector2I v, in double value)
      => new Vector2I((int)System.Math.Ceiling(v.X * value), (int)System.Math.Ceiling(v.Y * value));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Vector2I MultiplyFloor(in Vector2I v, in double value)
      => new Vector2I((int)System.Math.Floor(v.X * value), (int)System.Math.Floor(v.Y * value));
    /// <summary>Create a new vector from the additive inverse, i.e. a negation of the member in the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Additive_inverse"/>
    public static Vector2I Negate(in Vector2I v)
      => new Vector2I(-v.X, -v.Y); // Negate the members of the vector.
    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static Vector2I PerpendicularCcw(in Vector2I v)
      => new Vector2I(-v.Y, v.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static Vector2I PerpendicularCw(in Vector2I v)
      => new Vector2I(v.Y, -v.X);
    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static Vector2I Random(in int toExlusiveX, in int toExclusiveY)
      => new Vector2I(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY));
    /// <summary>Create a new random vector in the range [(0, 0), toExclusive] using the crypto-grade rng.</summary>
    public static Vector2I Random(in Vector2I toExclusive)
      => new Vector2I(Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.X), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.Y));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static Vector2I RandomZero(in int toExlusiveX, in int toExclusiveY)
      => new Vector2I(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX * 2) - toExlusiveX, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY * 2) - toExclusiveY);
    /// <summary>Create a new random vector in the range [-toExclusive, toExclusive] using the crypto-grade rng.</summary>
    public static Vector2I RandomZero(in Vector2I toExclusive)
      => RandomZero(toExclusive.X, toExclusive.Y);
    /// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
    public static Vector2I Remainder(in Vector2I v1, in Vector2I v2)
      => new Vector2I(v1.X % v2.X, v1.Y % v2.Y);
    /// <summary>Create a new vector with the remainder from each member divided by the value.</summary>
    public static Vector2I Remainder(in Vector2I v, in int value)
      => new Vector2I(v.X % value, v.Y % value);
    /// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
    public static Vector2I Remainder(in Vector2I v, in double value)
      => new Vector2I((int)(v.X % value), (int)(v.Y % value));
    /// <summary>Create a new vector with the difference from the vector subtracted by the other.</summary>
    public static Vector2I Subtract(in Vector2I v1, in Vector2I v2)
      => new Vector2I(v1.X - v2.X, v1.Y - v2.Y);
    /// <summary>Create a new vector with the difference from each member subtracted by the value.</summary>
    public static Vector2I Subtract(in Vector2I v, in int value)
      => new Vector2I(v.X - value, v.Y - value);
    #endregion Static members

    #region Overloaded Operators
    public static Vector2I operator -(in Vector2I v) => Negate(v);

    public static Vector2I operator ~(in Vector2I v) => OnesComplement(v);

    public static Vector2I operator --(in Vector2I v) => Subtract(v, 1);
    public static Vector2I operator ++(in Vector2I v) => Add(v, 1);

    public static Vector2I operator +(in Vector2I v1, in Vector2I v2) => Add(v1, v2);
    public static Vector2I operator +(in Vector2I v1, in int v2) => Add(v1, v2);

    public static Vector2I operator -(in Vector2I v1, in Vector2I v2) => Subtract(v1, v2);
    public static Vector2I operator -(in Vector2I v1, in int v2) => Subtract(v1, v2);

    public static Vector2I operator *(in Vector2I v1, in Vector2I v2) => Multiply(v1, v2);
    public static Vector2I operator *(in Vector2I v1, in int v2) => Multiply(v1, v2);
    public static Vector2I operator *(in Vector2I v1, in double v2) => MultiplyFloor(v1, v2);

    public static Vector2I operator /(in Vector2I v1, in Vector2I v2) => Divide(v1, v2);
    public static Vector2I operator /(in Vector2I v1, in int v2) => Divide(v1, v2);
    public static Vector2I operator /(in Vector2I v1, in double v2) => DivideFloor(v1, v2);

    public static Vector2I operator %(in Vector2I v1, in int v2) => Remainder(v1, v2);
    public static Vector2I operator %(in Vector2I v1, in double v2) => Remainder(v1, v2);

    public static Vector2I operator &(in Vector2I v1, in Vector2I v2) => BitwiseAnd(v1, v2);
    public static Vector2I operator &(in Vector2I v1, in int v2) => BitwiseAnd(v1, v2);

    public static Vector2I operator |(in Vector2I v1, in Vector2I v2) => BitwiseOr(v1, v2);
    public static Vector2I operator |(in Vector2I v1, in int v2) => BitwiseOr(v1, v2);

    public static Vector2I operator ^(in Vector2I v1, in Vector2I v2) => Xor(v1, v2);
    public static Vector2I operator ^(in Vector2I v1, in int v2) => Xor(v1, v2);

    public static Vector2I operator <<(in Vector2I v1, in int v2) => LeftShift(v1, v2);
    public static Vector2I operator >>(in Vector2I v1, in int v2) => RightShift(v1, v2);

    public static bool operator ==(in Vector2I v1, in Vector2I v2) => v1.Equals(v2);
    public static bool operator !=(in Vector2I v1, in Vector2I v2) => !v1.Equals(v2);
    #endregion Overloaded Operators

    // System.IEquatable<Vector2>
    public bool Equals(Vector2I other)
      => X == other.X && Y == other.Y;
    // System.IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}>";
    // Overrides
    public override bool Equals(object? obj)
       => obj is Vector2I o && Equals(o);
    public override int GetHashCode()
      => Flux.HashCode.CombineCore(X, Y);
    public override string ToString()
      => ToString(@"D", System.Globalization.CultureInfo.CurrentCulture);

    #region "Unique" Index
    /// <summary>Convert an index to a 3D vector, based on the specified lengths of axes.</summary>
    public static Vector2I FromUniqueIndex(long index, in Vector2I size) => unchecked(new Vector2I((int)(index % size.X), (int)(index / size.X)));

    /// <summary>Converts the vector to an index, based on the specified lengths of axes.</summary>
    public static long ToUniqueIndex(in Vector3I vector, in Vector2I size) => vector.X + (vector.Y * size.X);
    #endregion "Unique" Index
  }
}
