using System.Linq;

namespace Flux.Model
{
  public static partial class ExtensionMethods
  {
    //public static readonly Vector3I[] HexagonDiagonals = new Vector3I[] { new Vector3I(2, -1, -1), new Vector3I(1, -2, 1), new Vector3I(-1, -1, 2), new Vector3I(-2, 1, 1), new Vector3I(-1, 2, -1), new Vector3I(1, 1, -2) };
    //public static readonly Vector3I[] HexagonDirections = new Vector3I[] { new Vector3I(1, 0, -1), new Vector3I(1, -1, 0), new Vector3I(0, -1, 1), new Vector3I(-1, 0, 1), new Vector3I(-1, 1, 0), new Vector3I(0, 1, -1) };

    ///// <summary>Computes a point on the line between the source and the target by amount, where 0 is the source (any number less than 0 would be before the source), and 1 is the target (any number above 1 would be after the target).</summary>
    ///// <param name="source"></param>
    ///// <param name="target"></param>
    ///// <param name="amount"></param>
    ///// <returns></returns>
    //public static System.Numerics.Vector3 HexagonCubeLerp(this Vector3I source, Vector3I target, double amount)
    //  => System.Numerics.Vector3.Lerp(new System.Numerics.Vector3(source.X, source.Y, source.Z), new System.Numerics.Vector3(target.X, target.Y, target.Z), (float)amount);
    ///// <summary>Ensures that computed hex cube coordinates are x+y+z=0 and that midpoint hex points gets rounded to one side if in the middle.</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/#rounding"/>
    //public static System.Numerics.Vector3 AsHexagonCubeRound(this System.Numerics.Vector3 cube)
    //{
    //  var rx = System.Math.Round(cube.X);
    //  var ry = System.Math.Round(cube.Y);
    //  var rz = System.Math.Round(cube.Z);

    //  var x_diff = System.Math.Abs(rx - cube.X);
    //  var y_diff = System.Math.Abs(ry - cube.Y);
    //  var z_diff = System.Math.Abs(rz - cube.Z);

    //  if (x_diff > y_diff && x_diff > z_diff) rx = -ry - rz;
    //  else if (y_diff > z_diff) ry = -rx - rz;
    //  else rz = -rx - ry;

    //  return new System.Numerics.Vector3((float)rx, (float)ry, (float)rz);
    //}
    //public static Vector3I HexagonDiagonalNeighbor(this Vector3I source, int direction)
    //  => source + HexagonDiagonals[direction];
    //public static System.Collections.Generic.IEnumerable<Vector3I> HexagonDiagonalNeighbors(this Vector3I source)
    //  => HexagonDiagonals.Select(diagonal => source + diagonal);
    ///// <summary>Compute the distance between the source and the target.</summary>
    //public static int HexagonDistanceTo(this Vector3I source, Vector3I target)
    //  => (source - target).HexagonLength();
    ///// <summary>Compute the length of the hexagon, relative to 0,0,0.</summary>
    //public static int HexagonLength(this Vector3I source)
    //  => (int)((System.Math.Abs(source.X) + System.Math.Abs(source.Y) + System.Math.Abs(source.Z)) / 2.0);
    //public static Vector3I HexagonNeighbor(this Vector3I source, int direction)
    //  => source + HexagonDirections[direction];
    //public static System.Collections.Generic.IEnumerable<Vector3I> HexagonNeighbors(this Vector3I source)
    //  => HexagonDirections.Select(direction => source + direction);
    //public static Vector3I HexagonRotateLeft(this Vector3I source)
    //  => new Vector3I(-source.Z, -source.X, -source.Y);
    //public static Vector3I HexagonRotateRight(this Vector3I source)
    //  => new Vector3I(-source.Y, -source.Z, -source.X);

    ///// <summary>Converts an arbitrary raw ungridded position into a hexagon cube coordinate.</summary>
    ///// <see cref="http://www-cs-students.stanford.edu/~amitp/Articles/Hexagon2.html"/>
    //public static Vector3I ToHexagonCubeCoordinate(this System.Numerics.Vector3 source)
    //{
    //  var rx = System.Math.Round(source.X);
    //  var ry = System.Math.Round(source.Y);
    //  var rz = System.Math.Round(source.Z);

    //  var ix = (int)rx;
    //  var iy = (int)ry;
    //  var iz = (int)rz;

    //  if (ix + iy + iz is var s)
    //  {
    //    var ax = System.Math.Abs(rx - source.X);
    //    var ay = System.Math.Abs(ry - source.Y);
    //    var az = System.Math.Abs(rz - source.Z);

    //    if (ax >= ay && ax >= az) ix -= s; // ax is max
    //    else if (ay >= ax && ay >= az) iy -= s; // ay is max
    //    else iz -= s; // az is max
    //  }

    //  return new Vector3I(ix, iy, iz);
    //}

    ///// <summary>Converts a hexagon cube coordinate to a hexagon axial coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector2I AsHexagonCubeToAxial(this Vector3I source)
    //  => new Vector2I(source.X, source.Z);
    ///// <summary>Converts a hexagon axial coordinate to a hexagon cube coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector3I AsHexagonAxialToCube(this Vector2I source)
    //  => new Vector3I(source.X, -source.X - source.Y, source.Y);

    ///// <summary>Converts a hexagon double height coordinate to a cube coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector3I AsHexagonDoubleHeightToCube(this Vector2I source)
    //{
    //  var z = (source.Y - source.X) / 2;

    //  return new Vector3I(source.X, -source.X - z, z);
    //}
    ///// <summary>Converts a hexagon cube coordinate to a double height coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector2I AsHexagonCubeToDoubleHeight(this Vector3I source)
    //  => new Vector2I(source.X, 2 * source.Z + source.X);

    ///// <summary>Converts a hexagon double width coordinate to a cube coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector3I AsHexagonDoubleWidthToCube(this Vector2I source)
    //{
    //  var x = (source.X - source.Y) / 2;

    //  return new Vector3I(x, -x - source.Y, source.Y);
    //}
    ///// <summary>Converts a hexagon cube coordinate to a double width coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector2I AsHexagonCubeToDoubleWidth(this Vector3I source)
    //  => new Vector2I(2 * source.X + source.Z, source.Z);

    /// <summary>Creates eight vectors, each of which represents the center axis for each of the octants for the vector and the specified sizes of X, Y and Z.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
    public static System.Collections.Generic.IEnumerable<Vector3I> GetOctantCenterVectors(this Vector3I source, int subOctantSizeOfX, int subOctantSizeOfY, int subOctantSizeOfZ)
    {
      yield return new Vector3I(source.X + subOctantSizeOfX, source.Y + subOctantSizeOfY, source.Z + subOctantSizeOfZ);
      yield return new Vector3I(source.X - subOctantSizeOfX, source.Y + subOctantSizeOfY, source.Z + subOctantSizeOfZ);
      yield return new Vector3I(source.X - subOctantSizeOfX, source.Y - subOctantSizeOfY, source.Z + subOctantSizeOfZ);
      yield return new Vector3I(source.X + subOctantSizeOfX, source.Y - subOctantSizeOfY, source.Z + subOctantSizeOfZ);
      yield return new Vector3I(source.X + subOctantSizeOfX, source.Y + subOctantSizeOfY, source.Z - subOctantSizeOfZ);
      yield return new Vector3I(source.X - subOctantSizeOfX, source.Y + subOctantSizeOfY, source.Z - subOctantSizeOfZ);
      yield return new Vector3I(source.X - subOctantSizeOfX, source.Y - subOctantSizeOfY, source.Z - subOctantSizeOfZ);
      yield return new Vector3I(source.X + subOctantSizeOfX, source.Y - subOctantSizeOfY, source.Z - subOctantSizeOfZ);
    }
    /// <summary>Convert the 3D vector to a octant based on the specified axis vector.</summary>
    /// <returns>The octant identifer in the range 0-7, i.e. one of the eight octants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
    public static int ToOctantNumber(this Vector3I source, in Vector3I centerAxis) => ((source.X >= centerAxis.X ? 1 : 0) * 1) + ((source.Y >= centerAxis.Y ? 1 : 0) * 2) + ((source.Z >= centerAxis.Z ? 1 : 0) * 4);
  }

  public struct Vector3I
    : System.IEquatable<Vector3I>, System.IFormattable
  {
    #region Static Instances
    /// <summary>Returns the vector (0,0,0).</summary>
    public static Vector3I Zero => new Vector3I();
    /// <summary>Returns the vector (1,0,0).</summary>
    public static Vector3I UnitX => new Vector3I(1, 0, 0);
    /// <summary>Returns the vector (0,1,0).</summary>
    public static Vector3I UnitY => new Vector3I(0, 1, 0);
    /// <summary>Returns the vector (0,0,1).</summary>
    public static Vector3I UnitZ => new Vector3I(0, 0, 1);
    #endregion Static Instances

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Vector3I(int value)
      : this(value, value, value) { }
    public Vector3I(Vector2I value, int z)
      : this(value.X, value.Y, z) { }
    public Vector3I(int x, int y)
      : this(x, y, 0) { }
    public Vector3I(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
    }
    public Vector3I(int[] array, int startIndex)
    {
      if (array is null) throw new System.ArgumentNullException(nameof(array));

      if (array.Length - startIndex < 3) throw new System.ArgumentOutOfRangeException(nameof(array));

      X = array[startIndex++];
      Y = array[startIndex++];
      Z = array[startIndex];
    }

    /// <summary>Convert the vector to a unique index using the length of the X and the Y axes.</summary>
    public int ToUniqueIndex(int lengthX, int lengthY)
      => X + Y * lengthX + Z * lengthX * lengthY;

    #region Explicit Conversions
    public static explicit operator int[](in Vector3I v) => new int[] { v.X, v.Y, v.Z };
    public static explicit operator System.Numerics.Vector3(in Vector3I v) => new System.Numerics.Vector3(v.X, v.Y, v.Z);
    #endregion Explicit Conversions

    #region Static Members
    /// <summary>Create a new vector with the sum from the vector added by the other.</summary>
    public static Vector3I Add(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    /// <summary>Create a new vector with the sum from each member added to the value.</summary>
    public static Vector3I Add(in Vector3I v, int value)
      => new Vector3I(v.X + value, v.Y + value, v.Z + value);
    /// <summary>Create a new vector by left bit shifting the members of the vector by the specified count.</summary>
    public static Vector3I BitShiftLeft(in Vector3I v, int count)
      => new Vector3I(v.X << count, v.Y << count, v.Z << count);
    /// <summary>Create a new vector by right bit shifting the members of the vector by the specified count.</summary>
    public static Vector3I BitShiftRight(in Vector3I v, int count)
      => new Vector3I(v.X << count, v.Y << count, v.Z << count);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the other vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#AND"/>
    public static Vector3I BitwiseAnd(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.X & v2.X, v1.Y & v2.Y, v1.Z & v2.Z);
    /// <summary>Create a new vector by performing an AND operation of each member on the vector and the value.</summary>
    public static Vector3I BitwiseAnd(in Vector3I v, int value)
      => new Vector3I(v.X & value, v.Y & value, v.Z & value);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#XOR"/>
    public static Vector3I BitwiseExclusiveOr(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.X ^ v2.X, v1.Y ^ v2.Y, v1.Z ^ v2.Z);
    /// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the value.</summary>
    public static Vector3I BitwiseExclusiveOr(in Vector3I v, int value)
      => new Vector3I(v.X ^ value, v.Y ^ value, v.Z ^ value);
    /// <summary>Create a new vector by performing a NOT operation on each member of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#NOT"/>
    public static Vector3I BitwiseNot(in Vector3I v)
      => new Vector3I(~v.X, ~v.Y, ~v.Z); // .NET performs a one's complement (bitwise logical NOT) on integral types.
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the other.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#OR"/>
    public static Vector3I BitwiseOr(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.X | v2.X, v1.Y | v2.Y, v1.Z | v2.Z);
    /// <summary>Create a new vector by performing an OR operation on each member of the vector and the value.</summary>
    public static Vector3I BitwiseOr(in Vector3I v, int value)
      => new Vector3I(v.X | value, v.Y | value, v.Z | value);
    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in Vector3I v1, in Vector3I v2)
      => System.Math.Max(System.Math.Max(System.Math.Abs(v2.X - v1.X), System.Math.Abs(v2.Y - v1.Y)), System.Math.Abs(v2.Z - v1.Z));
    /// <summary>Create a new vector by computing the cross product, i.e. cross(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cross_product"/>
    public static Vector3I CrossProduct(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
    /// <summary>Create a new vector with the quotient from the vector divided by the other.</summary>
    public static Vector3I Divide(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
    public static Vector3I Divide(in Vector3I v, int value)
      => new Vector3I(v.X / value, v.Y / value, v.Z / value);
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Vector3I DivideCeiling(in Vector3I v, double value)
      => new Vector3I((int)System.Math.Ceiling(v.X / value), (int)System.Math.Ceiling(v.Y / value), (int)System.Math.Ceiling(v.Z / value));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Vector3I DivideFloor(in Vector3I v, double value)
      => new Vector3I((int)System.Math.Floor(v.X / value), (int)System.Math.Floor(v.Y / value), (int)System.Math.Floor(v.Z / value));
    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(in Vector3I v1, in Vector3I v2)
      => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    /// <summary>Compute the euclidean length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(in Vector3I v)
      => System.Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
    /// <summary>Create a new vector from the index and the length of the X and the length of the Y axes.</summary>
    public static Vector3I FromUniqueIndex(int index, int lengthX, int lengthY)
      => index % (lengthX * lengthY) is var irxy ? new Vector3I(irxy % lengthX, irxy / lengthX, index / (lengthX * lengthY)) : throw new System.ArgumentOutOfRangeException(nameof(index));
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLength(in Vector3I v)
      => System.Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
    /// <summary>Compute the length (or magnitude) squared (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLengthSquared(in Vector3I v)
      => v.X * v.X + v.Y * v.Y + v.Z * v.Z;
    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int RectilinearDistance(in Vector3I a, in Vector3I b)
      => System.Math.Abs(b.X - a.X) + System.Math.Abs(b.Y - a.Y) + System.Math.Abs(b.Z - a.Z);
    /// <summary>Create a new vector with the product from the vector multiplied with the other.</summary>
    public static Vector3I Multiply(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
    public static Vector3I Multiply(in Vector3I v, int value)
      => new Vector3I(v.X * value, v.Y * value, v.Z * value);
    /// <summary>Create a new vector with the ceiling(product) from each member multiplied with the value.</summary>
    public static Vector3I MultiplyCeiling(in Vector3I v, double value)
      => new Vector3I((int)System.Math.Ceiling(v.X * value), (int)System.Math.Ceiling(v.Y * value), (int)System.Math.Ceiling(v.Z * value));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Vector3I MultiplyFloor(in Vector3I v, double value)
      => new Vector3I((int)System.Math.Floor(v.X * value), (int)System.Math.Floor(v.Y * value), (int)System.Math.Floor(v.Z * value));
    /// <summary>Create a new vector from the additive inverse, i.e. a negation of the members in the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Additive_inverse"/>
    public static Vector3I Negate(in Vector3I v)
      => new Vector3I(-v.X, -v.Y, -v.Z); // Negate the members of the vector.
    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static Vector3I Random(in int toExlusiveX, in int toExclusiveY, in int toExclusiveZ)
      => new Vector3I(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveZ));
    /// <summary>Create a new random vector in the range [(0, 0), toExclusive] using the crypto-grade rng.</summary>
    public static Vector3I Random(in Vector3I toExclusive)
      => new Vector3I(Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.X), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.Y));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static Vector3I RandomZero(in int toExlusiveX, in int toExclusiveY, in int toExclusiveZ)
      => new Vector3I(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX * 2) - toExlusiveX, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY * 2) - toExclusiveY, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveZ * 2) - toExclusiveZ);
    /// <summary>Create a new random vector in the range [-toExclusive, toExclusive] using the crypto-grade rng.</summary>
    public static Vector3I RandomZero(in Vector3I toExclusive)
      => RandomZero(toExclusive.X, toExclusive.Y, toExclusive.Z);
    /// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
    public static Vector3I Remainder(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.X % v2.X, v1.Y % v2.Y, v1.Z % v2.Z);
    /// <summary>Create a new vector with the remainder from each member divided by the value.</summary>
    public static Vector3I Remainder(in Vector3I v, int value)
      => new Vector3I(v.X % value, v.Y % value, v.Z % value);
    /// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
    public static Vector3I Remainder(in Vector3I v, double value)
      => new Vector3I((int)(v.X % value), (int)(v.Y % value), (int)(v.Z % value));
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static int ScalarTripleProduct(in Vector3I a, in Vector3I b, in Vector3I c)
      => DotProduct(a, CrossProduct(b, c));
    /// <summary>Create a new vector with the difference from the vector subtracted by the other.</summary>
    public static Vector3I Subtract(in Vector3I v1, in Vector3I v2)
      => new Vector3I(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    /// <summary>Create a new vector with the difference from each member subtracted by the value.</summary>
    public static Vector3I Subtract(in Vector3I v, in int value)
      => new Vector3I(v.X - value, v.Y - value, v.Z - value);
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static Vector3I VectorTripleProduct(in Vector3I a, in Vector3I b, in Vector3I c)
      => CrossProduct(a, CrossProduct(b, c));
    #endregion Static Members

    #region Overloaded Operators
    public static Vector3I operator -(in Vector3I v) => Negate(v);

    public static Vector3I operator ~(in Vector3I v) => BitwiseNot(v);

    public static Vector3I operator --(in Vector3I v) => Subtract(v, 1);
    public static Vector3I operator ++(in Vector3I v) => Add(v, 1);

    public static Vector3I operator +(in Vector3I v1, in Vector3I v2) => Add(v1, v2);
    public static Vector3I operator +(in Vector3I v1, in int i) => Add(v1, i);

    public static Vector3I operator -(in Vector3I v1, in Vector3I v2) => Subtract(v1, v2);
    public static Vector3I operator -(in Vector3I v1, in int i) => Subtract(v1, i);

    public static Vector3I operator *(in Vector3I v1, in Vector3I v2) => Multiply(v1, v2);
    public static Vector3I operator *(in Vector3I v1, in int i) => Multiply(v1, i);
    public static Vector3I operator *(in Vector3I v1, in double d) => MultiplyFloor(v1, d);

    public static Vector3I operator /(in Vector3I v1, in Vector3I v2) => Divide(v1, v2);
    public static Vector3I operator /(in Vector3I v1, in int i) => Divide(v1, i);
    public static Vector3I operator /(in Vector3I v1, in double d) => DivideFloor(v1, d);

    public static Vector3I operator %(in Vector3I v1, in int i) => Remainder(v1, i);
    public static Vector3I operator %(in Vector3I v1, in double d) => Remainder(v1, d);

    public static Vector3I operator &(in Vector3I v1, in Vector3I v2) => BitwiseAnd(v1, v2);
    public static Vector3I operator &(in Vector3I v1, in int i) => BitwiseAnd(v1, i);

    public static Vector3I operator |(in Vector3I v1, in Vector3I v2) => BitwiseOr(v1, v2);
    public static Vector3I operator |(in Vector3I v1, in int i) => BitwiseOr(v1, i);

    public static Vector3I operator ^(in Vector3I v1, in Vector3I v2) => BitwiseExclusiveOr(v1, v2);
    public static Vector3I operator ^(in Vector3I v1, in int i) => BitwiseExclusiveOr(v1, i);

    public static Vector3I operator <<(in Vector3I v1, in int i) => BitShiftLeft(v1, i);
    public static Vector3I operator >>(in Vector3I v1, in int i) => BitShiftRight(v1, i);

    public static bool operator ==(in Vector3I v1, in Vector3I v2) => v1.Equals(v2);
    public static bool operator !=(in Vector3I v1, in Vector3I v2) => !v1.Equals(v2);
    #endregion Overloaded Operators

    #region System.IEquatable<Vector3>
    public bool Equals(Vector3I other) => X == other.X && Y == other.Y && Z == other.Z;
    #endregion

    #region Overrides
    public override int GetHashCode()
    {
      var hash = X.GetHashCode();
      hash = ((hash << 5) + hash) ^ Y.GetHashCode();
      hash = ((hash << 5) + hash) ^ Z.GetHashCode();
      return hash;
    }
    public override bool Equals(object? obj) => obj is Vector3I && Equals((Vector3I)obj);
    #endregion Overrides

    #region "Unique" Index
    /// <summary>Convert a "mapped" index to a 3D vector. This index is uniquely mapped using the specified size vector.</summary>
    public static Vector3I FromUniqueIndex(long index, in Vector3I length)
    {
      var xy = (long)length.X * (long)length.Y;
      var irxy = index % xy;

      return new Vector3I((int)(irxy % length.X), (int)(irxy / length.X), (int)(index / xy));
    }
    /// <summary>Converts the vector to a "mapped" index. This index is uniquely mapped using the specified size vector.</summary>
    public static long ToUniqueIndex(in Vector3I vector, in Vector3I length) => vector.X + (vector.Y * length.X) + (vector.Z * length.X * length.Y);
    #endregion "Unique" Index

    #region System.IFormattable
    public override string ToString() => ToString(@"D", System.Globalization.CultureInfo.CurrentCulture);
    public string ToString(string format) => ToString(format, System.Globalization.CultureInfo.CurrentCulture);
    public string ToString(string? format, System.IFormatProvider? formatProvider) => $"<{((System.IFormattable)X).ToString(format, formatProvider)}, {((System.IFormattable)Y).ToString(format, formatProvider)}, {((System.IFormattable)Z).ToString(format, formatProvider)}>";
    #endregion System.IFormattable
  }
}
