namespace Flux
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
		public static System.Collections.Generic.IEnumerable<Geometry.Point3> GetOctantCenterVectors(this Geometry.Point3 source, int subOctantSizeOfX, int subOctantSizeOfY, int subOctantSizeOfZ)
		{
			yield return new Geometry.Point3(source.X + subOctantSizeOfX, source.Y + subOctantSizeOfY, source.Z + subOctantSizeOfZ);
			yield return new Geometry.Point3(source.X - subOctantSizeOfX, source.Y + subOctantSizeOfY, source.Z + subOctantSizeOfZ);
			yield return new Geometry.Point3(source.X - subOctantSizeOfX, source.Y - subOctantSizeOfY, source.Z + subOctantSizeOfZ);
			yield return new Geometry.Point3(source.X + subOctantSizeOfX, source.Y - subOctantSizeOfY, source.Z + subOctantSizeOfZ);
			yield return new Geometry.Point3(source.X + subOctantSizeOfX, source.Y + subOctantSizeOfY, source.Z - subOctantSizeOfZ);
			yield return new Geometry.Point3(source.X - subOctantSizeOfX, source.Y + subOctantSizeOfY, source.Z - subOctantSizeOfZ);
			yield return new Geometry.Point3(source.X - subOctantSizeOfX, source.Y - subOctantSizeOfY, source.Z - subOctantSizeOfZ);
			yield return new Geometry.Point3(source.X + subOctantSizeOfX, source.Y - subOctantSizeOfY, source.Z - subOctantSizeOfZ);
		}
		/// <summary>Convert the 3D vector to a octant based on the specified axis vector.</summary>
		/// <returns>The octant identifer in the range 0-7, i.e. one of the eight octants.</returns>
		/// <see cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
		public static int ToOctantNumber(this Geometry.Point3 source, in Geometry.Point3 centerAxis) => ((source.X >= centerAxis.X ? 1 : 0) * 1) + ((source.Y >= centerAxis.Y ? 1 : 0) * 2) + ((source.Z >= centerAxis.Z ? 1 : 0) * 4);

		/// <summary>Convert a point to a 2D vector (the Z component is discarded).</summary>
		public static System.Numerics.Vector2 ToVector2(this in Geometry.Point3 source)
			=> new System.Numerics.Vector2(source.X, source.Y);
		/// <summary>Convert a point to a 3D vector.</summary>
		public static System.Numerics.Vector3 ToVector3(this in Geometry.Point3 source)
			=> new System.Numerics.Vector3(source.X, source.Y, source.Z);
	}

	namespace Geometry
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

			#region Static Members
			/// <summary>Create a new vector with the sum from the vector added by the other.</summary>
			public static Point3 Add(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_x + v2.m_x, v1.m_y + v2.m_y, v1.m_z + v2.m_z);
			/// <summary>Create a new vector with the sum from each member added to the value.</summary>
			public static Point3 Add(in Point3 v, int value)
				=> new Point3(v.m_x + value, v.m_y + value, v.m_z + value);
			/// <summary>Create a new vector by left bit shifting the members of the vector by the specified count.</summary>
			public static Point3 LeftShift(in Point3 v, int count)
				=> new Point3(v.m_x << count, v.m_y << count, v.m_z << count);
			/// <summary>Create a new vector by right bit shifting the members of the vector by the specified count.</summary>
			public static Point3 RightShift(in Point3 v, int count)
				=> new Point3(v.m_x << count, v.m_y << count, v.m_z << count);
			/// <summary>Create a new vector by performing an AND operation of each member on the vector and the other vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#AND"/>
			public static Point3 BitwiseAnd(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_x & v2.m_x, v1.m_y & v2.m_y, v1.m_z & v2.m_z);
			/// <summary>Create a new vector by performing an AND operation of each member on the vector and the value.</summary>
			public static Point3 BitwiseAnd(in Point3 v, int value)
				=> new Point3(v.m_x & value, v.m_y & value, v.m_z & value);
			/// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the other.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#XOR"/>
			public static Point3 Xor(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_x ^ v2.m_x, v1.m_y ^ v2.m_y, v1.m_z ^ v2.m_z);
			/// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the value.</summary>
			public static Point3 Xor(in Point3 v, int value)
				=> new Point3(v.m_x ^ value, v.m_y ^ value, v.m_z ^ value);
			/// <summary>Create a new vector by performing a NOT operation on each member of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#NOT"/>
			public static Point3 OnesComplement(in Point3 v)
				=> new Point3(~v.m_x, ~v.m_y, ~v.m_z); // .NET performs a one's complement (bitwise logical NOT) on integral types.
			/// <summary>Create a new vector by performing an OR operation on each member of the vector and the other.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#OR"/>
			public static Point3 BitwiseOr(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_x | v2.m_x, v1.m_y | v2.m_y, v1.m_z | v2.m_z);
			/// <summary>Create a new vector by performing an OR operation on each member of the vector and the value.</summary>
			public static Point3 BitwiseOr(in Point3 v, int value)
				=> new Point3(v.m_x | value, v.m_y | value, v.m_z | value);
			/// <summary>Compute the Chebyshev distance between the vectors.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
			public static double ChebyshevDistance(in Point3 v1, in Point3 v2)
				=> Maths.Max(System.Math.Abs(v2.m_x - v1.m_x), System.Math.Abs(v2.m_y - v1.m_y), System.Math.Abs(v2.m_z - v1.m_z));
			/// <summary>Create a new vector by computing the cross product, i.e. cross(a, b), of the vector (a) and vector b.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Cross_product"/>
			public static Point3 CrossProduct(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_y * v2.m_z - v1.m_z * v2.m_y, v1.m_z * v2.m_x - v1.m_x * v2.m_z, v1.m_x * v2.m_y - v1.m_y * v2.m_x);
			/// <summary>Create a new vector with each member subtracted by 1.</summary>
			public static Point3 Decrement(in Point3 v1)
				=> Subtract(v1, 1);
			/// <summary>Create a new vector with the quotient from the vector divided by the other.</summary>
			public static Point3 Divide(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_x / v2.m_x, v1.m_y / v2.m_y, v1.m_z / v2.m_z);
			/// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
			public static Point3 Divide(in Point3 v, int value)
				=> new Point3(v.m_x / value, v.m_y / value, v.m_z / value);
			/// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
			public static Point3 DivideCeiling(in Point3 v, double value)
				=> new Point3((int)System.Math.Ceiling(v.m_x / value), (int)System.Math.Ceiling(v.m_y / value), (int)System.Math.Ceiling(v.m_z / value));
			/// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
			public static Point3 DivideFloor(in Point3 v, double value)
				=> new Point3((int)System.Math.Floor(v.m_x / value), (int)System.Math.Floor(v.m_y / value), (int)System.Math.Floor(v.m_z / value));
			/// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
			public static int DotProduct(in Point3 v1, in Point3 v2)
				=> v1.m_x * v2.m_x + v1.m_y * v2.m_y + v1.m_z * v2.m_z;
			/// <summary>Compute the euclidean distance of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
			public static double EuclideanDistance(in Point3 v1, in Point3 v2)
				=> GetLength(v1 - v2);
			/// <summary>Compute the euclidean distance squared of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
			public static double EuclideanDistanceSquare(in Point3 v1, in Point3 v2)
				=> GetLengthSquared(v1 - v2);
			/// <summary>Create a new vector from the index and the length of the m_x and the length of the m_y axes.</summary>
			public static Point3 FromUniqueIndex(int index, int lengthX, int lengthY)
				=> index % (lengthX * lengthY) is var irxy ? new Point3(irxy % lengthX, irxy / lengthX, index / (lengthX * lengthY)) : throw new System.ArgumentOutOfRangeException(nameof(index));
			/// <summary>Compute the length (or magnitude) of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
			public static double GetLength(in Point3 v)
				=> System.Math.Sqrt(v.m_x * v.m_x + v.m_y * v.m_y + v.m_z * v.m_z);
			/// <summary>Compute the length (or magnitude) squared (or magnitude) of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
			public static double GetLengthSquared(in Point3 v)
				=> v.m_x * v.m_x + v.m_y * v.m_y + v.m_z * v.m_z;
			/// <summary>Create a new vector with 1 added to each member.</summary>
			public static Point3 Increment(in Point3 v1)
				=> Add(v1, 1);
			/// <summary>Compute the Manhattan distance between the vectors.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
			public static int ManhattanDistance(in Point3 v1, in Point3 v2)
				=> System.Math.Abs(v2.m_x - v1.m_x) + System.Math.Abs(v2.m_y - v1.m_y) + System.Math.Abs(v2.m_z - v1.m_z);
			/// <summary>Create a new vector with the product from the vector multiplied with the other.</summary>
			public static Point3 Multiply(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_x * v2.m_x, v1.m_y * v2.m_y, v1.m_z * v2.m_z);
			/// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
			public static Point3 Multiply(in Point3 v, int value)
				=> new Point3(v.m_x * value, v.m_y * value, v.m_z * value);
			/// <summary>Create a new vector with the ceiling(product) from each member multiplied with the value.</summary>
			public static Point3 MultiplyCeiling(in Point3 v, double value)
				=> new Point3((int)System.Math.Ceiling(v.m_x * value), (int)System.Math.Ceiling(v.m_y * value), (int)System.Math.Ceiling(v.m_z * value));
			/// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
			public static Point3 MultiplyFloor(in Point3 v, double value)
				=> new Point3((int)System.Math.Floor(v.m_x * value), (int)System.Math.Floor(v.m_y * value), (int)System.Math.Floor(v.m_z * value));
			/// <summary>Create a new vector from the additive inverse, i.e. a negation of the members in the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Additive_inverse"/>
			public static Point3 Negate(in Point3 v)
				=> new Point3(-v.m_x, -v.m_y, -v.m_z); // Negate the members of the vector.
			/// <summary>Create a new random vector using the crypto-grade rng.</summary>
			public static Point3 Random(in int toExlusiveX, in int toExclusiveY, in int toExclusiveZ)
				=> new Point3(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveZ));
			/// <summary>Create a new random vector in the range [(0, 0), toExclusive] using the crypto-grade rng.</summary>
			public static Point3 Random(in Point3 toExclusive)
				=> new Point3(Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.m_x), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.m_y));
			/// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
			public static Point3 RandomZero(in int toExlusiveX, in int toExclusiveY, in int toExclusiveZ)
				=> new Point3(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX * 2) - toExlusiveX, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY * 2) - toExclusiveY, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveZ * 2) - toExclusiveZ);
			/// <summary>Create a new random vector in the range [-toExclusive, toExclusive] using the crypto-grade rng.</summary>
			public static Point3 RandomZero(in Point3 toExclusive)
				=> RandomZero(toExclusive.m_x, toExclusive.m_y, toExclusive.m_z);
			/// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
			public static Point3 Remainder(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_x % v2.m_x, v1.m_y % v2.m_y, v1.m_z % v2.m_z);
			/// <summary>Create a new vector with the remainder from each member divided by the value.</summary>
			public static Point3 Remainder(in Point3 v, int value)
				=> new Point3(v.m_x % value, v.m_y % value, v.m_z % value);
			/// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
			public static Point3 Remainder(in Point3 v, double value)
				=> new Point3((int)(v.m_x % value), (int)(v.m_y % value), (int)(v.m_z % value));
			/// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
			public static int ScalarTripleProduct(in Point3 a, in Point3 b, in Point3 c)
				=> DotProduct(a, CrossProduct(b, c));
			/// <summary>Create a new vector with the difference from the vector subtracted by the other.</summary>
			public static Point3 Subtract(in Point3 v1, in Point3 v2)
				=> new Point3(v1.m_x - v2.m_x, v1.m_y - v2.m_y, v1.m_z - v2.m_z);
			/// <summary>Create a new vector with the difference from each member subtracted by the value.</summary>
			public static Point3 Subtract(in Point3 v, in int value)
				=> new Point3(v.m_x - value, v.m_y - value, v.m_z - value);
			/// <summary>Creates a <see cref='Size3'/> from a <see cref='Point3'/>.</summary>
			public static Size3 ToSize3(Point3 point)
				=> new Size3(point.m_x, point.m_y, point.m_z);
			/// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
			public static Point3 VectorTripleProduct(in Point3 a, in Point3 b, in Point3 c)
				=> CrossProduct(a, CrossProduct(b, c));
			#endregion Static Members

			#region "Unique" Index
			/// <summary>Convert a "mapped" index to a 3D vector. This index is uniquely mapped using the specified size vector.</summary>
			public static Point3 FromUniqueIndex(long index, in Size3 bounds)
			{
				var xy = (long)bounds.Width * (long)bounds.Height;
				var irxy = index % xy;

				return new Point3((int)(irxy % bounds.Width), (int)(irxy / bounds.Width), (int)(index / xy));
			}
			/// <summary>Converts the vector to a "mapped" index. This index is uniquely mapped using the specified size vector.</summary>
			public static long ToUniqueIndex(in Point3 vector, in Size3 bounds) 
				=> vector.m_x + (vector.m_y * bounds.Width) + (vector.m_z * bounds.Width * bounds.Height);
			#endregion "Unique" Index

			#region Overloaded Operators
			public static Point3 operator -(in Point3 v) => Negate(v);

			public static Point3 operator ~(in Point3 v) => OnesComplement(v);

			public static Point3 operator --(in Point3 v) => Decrement(v);
			public static Point3 operator ++(in Point3 v) => Increment(v);

			public static Point3 operator +(in Point3 v1, in Point3 v2) => Add(v1, v2);
			public static Point3 operator +(in Point3 v1, in int i) => Add(v1, i);

			public static Point3 operator -(in Point3 v1, in Point3 v2) => Subtract(v1, v2);
			public static Point3 operator -(in Point3 v1, in int i) => Subtract(v1, i);

			public static Point3 operator *(in Point3 v1, in Point3 v2) => Multiply(v1, v2);
			public static Point3 operator *(in Point3 v1, in int i) => Multiply(v1, i);

			public static Point3 operator /(in Point3 v1, in Point3 v2) => Divide(v1, v2);
			public static Point3 operator /(in Point3 v1, in int i) => Divide(v1, i);

			public static Point3 operator %(in Point3 v1, in int i) => Remainder(v1, i);
			public static Point3 operator %(in Point3 v1, in double d) => Remainder(v1, d);

			public static Point3 operator &(in Point3 v1, in Point3 v2) => BitwiseAnd(v1, v2);
			public static Point3 operator &(in Point3 v1, in int i) => BitwiseAnd(v1, i);

			public static Point3 operator |(in Point3 v1, in Point3 v2) => BitwiseOr(v1, v2);
			public static Point3 operator |(in Point3 v1, in int i) => BitwiseOr(v1, i);

			public static Point3 operator ^(in Point3 v1, in Point3 v2) => Xor(v1, v2);
			public static Point3 operator ^(in Point3 v1, in int i) => Xor(v1, i);

			public static Point3 operator <<(in Point3 v1, in int i) => LeftShift(v1, i);
			public static Point3 operator >>(in Point3 v1, in int i) => RightShift(v1, i);

			public static bool operator ==(in Point3 v1, in Point3 v2) => v1.Equals(v2);
			public static bool operator !=(in Point3 v1, in Point3 v2) => !v1.Equals(v2);
			#endregion Overloaded Operators

			// System.IEquatable<Vector3>
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
}
