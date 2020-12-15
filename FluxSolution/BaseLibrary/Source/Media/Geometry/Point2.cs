using System.Linq;

namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
		public static System.Collections.Generic.IEnumerable<Geometry.Point2> GetQuadrantCenterVectors(this Geometry.Point2 source, int subQuadrantSizeOfX, int subQuadrantSizeOfY)
		{
			yield return new Geometry.Point2(source.X + subQuadrantSizeOfX, source.Y + subQuadrantSizeOfY);
			yield return new Geometry.Point2(source.X - subQuadrantSizeOfX, source.Y + subQuadrantSizeOfY);
			yield return new Geometry.Point2(source.X - subQuadrantSizeOfX, source.Y - subQuadrantSizeOfY);
			yield return new Geometry.Point2(source.X + subQuadrantSizeOfX, source.Y - subQuadrantSizeOfY);
		}
		/// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
		/// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
		/// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
		public static int ToQuadrant(this Geometry.Point2 source, in Geometry.Point2 centerAxis) => ((source.X >= centerAxis.X ? 1 : 0) * 1) + ((source.Y >= centerAxis.Y ? 1 : 0) * 2);

		/// <summary>Convert a point to a 2D vector.</summary>
		public static System.Numerics.Vector2 ToVector2(this in Geometry.Point2 source)
			=> new System.Numerics.Vector2(source.X, source.Y);
		/// <summary>Convert a point to a 3D vector.</summary>
		public static System.Numerics.Vector3 ToVector3(this in Geometry.Point2 source)
			=> new System.Numerics.Vector3(source.X, source.Y, 0);
	}

	namespace Geometry
	{
		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
		public struct Point2
			: System.IEquatable<Point2>
		{
			public static readonly Point2 Empty;
			public bool IsEmpty => Equals(Empty);

			[System.Runtime.InteropServices.FieldOffset(0)] private int m_x;
			[System.Runtime.InteropServices.FieldOffset(4)] private int m_y;

			public int X { get => m_x; set => m_x = value; }
			public int Y { get => m_y; set => m_y = value; }

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

			public Point2(int value)
				: this(value, value) { }
			public Point2(int x, int y)
			{
				m_x = x;
				m_y = y;
			}
			public Point2(int[] array, int startIndex)
			{
				if (array is null || array.Length - startIndex < 2) throw new System.ArgumentOutOfRangeException(nameof(array));

				m_x = array[startIndex++];
				m_y = array[startIndex];
			}

			/// <summary>Convert the vector to a unique index using the length of the m_x axis.</summary>
			public (string column, string row) ToLabels(System.Collections.Generic.IList<string> columnLabels, System.Collections.Generic.IList<string> rowLabels)
				=> (columnLabels.ElementAt(m_x), rowLabels.ElementAt(m_y));
			public int ToUniqueIndex(int lengthX)
				=> m_x * m_y * lengthX;
			public System.Numerics.Vector2 ToVector2()
				=> new System.Numerics.Vector2(m_x, m_y);
			public int[] ToArray()
				=> new int[] { m_x, m_y };

			#region Static members
			/// <summary>Create a new vector with the sum from the vector added to the other.</summary>
			public static Point2 Add(in Point2 v1, in Point2 v2)
				=> new Point2(v1.m_x + v2.m_x, v1.m_y + v2.m_y);
			/// <summary>Create a new vector with the sum from each member added to the value.</summary>
			public static Point2 Add(in Point2 v, in int value)
				=> new Point2(v.m_x + value, v.m_y + value);
			/// <summary>Create a new vector by left bit shifting the members of the vector by the specified count.</summary>
			public static Point2 LeftShift(in Point2 v, in int count)
				=> new Point2(v.m_x << count, v.m_y << count);
			/// <summary>Create a new vector by right bit shifting the members of the vector by the specified count.</summary>
			public static Point2 RightShift(in Point2 v, in int count)
				=> new Point2(v.m_x >> count, v.m_y >> count);
			/// <summary>Create a new vector by performing an AND operation of each member on the vector and the other vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#AND"/>
			public static Point2 BitwiseAnd(in Point2 v1, in Point2 v2)
				=> new Point2(v1.m_x & v2.m_x, v1.m_y & v2.m_y);
			/// <summary>Create a new vector by performing an AND operation of each member on the vector and the value.</summary>
			public static Point2 BitwiseAnd(in Point2 v, in int value)
				=> new Point2(v.m_x & value, v.m_y & value);
			/// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the other.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#XOR"/>
			public static Point2 Xor(in Point2 v1, in Point2 v2)
				=> new Point2(v1.m_x ^ v2.m_x, v1.m_y ^ v2.m_y);
			/// <summary>Create a new vector by performing an eXclusive OR operation on each member of the vector and the value.</summary>
			public static Point2 Xor(in Point2 v, in int value)
				=> new Point2(v.m_x ^ value, v.m_y ^ value);
			/// <summary>Create a new vector by performing a NOT operation on each member of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#NOT"/>
			public static Point2 OnesComplement(in Point2 v)
				=> new Point2(~v.m_x, ~v.m_y); // .NET performs a one's complement (bitwise logical NOT) on integral types.
			/// <summary>Create a new vector by performing an OR operation on each member of the vector and the other.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Bitwise_operation#OR"/>
			public static Point2 BitwiseOr(in Point2 v1, in Point2 v2)
				=> new Point2(v1.m_x | v2.m_x, v1.m_y | v2.m_y);
			/// <summary>Create a new vector by performing an OR operation on each member of the vector and the value.</summary>
			public static Point2 BitwiseOr(in Point2 v, in int value)
				=> new Point2(v.m_x | value, v.m_y | value);
			/// <summary>Compute the Chebyshev distance between the vectors.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
			public static double ChebyshevDistance(in Point2 v1, in Point2 v2)
				=> System.Math.Max(System.Math.Abs(v2.m_x - v1.m_x), System.Math.Abs(v2.m_y - v1.m_y));
			/// <summary>Computes the closest cartesian coordinate vector at the specified angle and distance from the vector.</summary>
			public static Point2 ComputeVector(in Point2 v, in double angle, in double distance = 1)
				=> new Point2((int)(distance * System.Math.Sin(angle)) + v.m_x, (int)(distance * System.Math.Cos(angle)) + v.m_y);
			/// <summary>Create a new vector with each member subtracted by 1.</summary>
			public static Point2 Decrement(in Point2 v1)
				=> Subtract(v1, 1);
			/// <summary>Create a new vector with the quotient from the vector divided by the other.</summary>
			public static Point2 Divide(in Point2 v1, in Point2 v2)
				=> new Point2(v1.m_x / v2.m_x, v1.m_y / v2.m_y);
			/// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
			public static Point2 Divide(in Point2 v, in int value)
				=> new Point2(v.m_x / value, v.m_y / value);
			/// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
			public static Point2 DivideCeiling(in Point2 v, in double value)
				=> new Point2((int)(v.m_x / value), (int)(v.m_y / value));
			/// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
			public static Point2 DivideFloor(in Point2 v, in double value)
				=> new Point2((int)(v.m_x / value), (int)(v.m_y / value));
			/// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
			public static int DotProduct(in Point2 v1, in Point2 v2)
				=> v1.m_x * v2.m_x + v1.m_y * v2.m_y;
			/// <summary>Compute the euclidean distance of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
			public static double EuclideanDistance(in Point2 v1, in Point2 v2)
				=> GetLength(v1 - v2);
			/// <summary>Compute the euclidean distance squared of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
			public static double EuclideanDistanceSquare(in Point2 v1, in Point2 v2)
				=> GetLengthSquared(v1 - v2);
			/// <summary>Create a new vector from the labels and label definitions.</summary>
			public static Point2 FromLabels(string column, string row, System.Collections.Generic.IList<string> columnLabels, System.Collections.Generic.IList<string> rowLabels)
				=> new Point2(columnLabels?.IndexOf(column) ?? throw new System.ArgumentOutOfRangeException(nameof(column)), rowLabels?.IndexOf(row) ?? throw new System.ArgumentOutOfRangeException(nameof(column)));
			/// <summary>Create a new vector from the index and the length of the m_x axis.</summary>
			public static Point2 FromUniqueIndex(int index, int lengthX)
				=> new Point2(index % lengthX, index / lengthX);
			/// <summary>Compute the length (or magnitude) of the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
			public static double GetLength(in Point2 v)
				=> System.Math.Sqrt(v.m_x * v.m_x + v.m_y * v.m_y);
			/// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
			public static double GetLengthSquared(in Point2 v)
				=> v.m_x * v.m_x + v.m_y * v.m_y;
			/// <summary>Create a new vector with 1 added to each member.</summary>
			public static Point2 Increment(in Point2 v1)
				=> Add(v1, 1);
			/// <summary>Compute the Manhattan distance between the vectors.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
			public static int ManhattanDistance(in Point2 v1, in Point2 v2)
				=> System.Math.Abs(v2.m_x - v1.m_x) + System.Math.Abs(v2.m_y - v1.m_y);
			/// <summary>Create a new vector with the product from the vector multiplied with the other.</summary>
			public static Point2 Multiply(in Point2 v1, in Point2 v2)
				=> new Point2(v1.m_x * v2.m_x, v1.m_y * v2.m_y);
			/// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
			public static Point2 Multiply(in Point2 v, in int value)
				=> new Point2(v.m_x * value, v.m_y * value);
			/// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
			public static Point2 MultiplyCeiling(in Point2 v, in double value)
				=> new Point2((int)System.Math.Ceiling(v.m_x * value), (int)System.Math.Ceiling(v.m_y * value));
			/// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
			public static Point2 MultiplyFloor(in Point2 v, in double value)
				=> new Point2((int)System.Math.Floor(v.m_x * value), (int)System.Math.Floor(v.m_y * value));
			/// <summary>Create a new vector from the additive inverse, i.e. a negation of the member in the vector.</summary>
			/// <see cref="https://en.wikipedia.org/wiki/Additive_inverse"/>
			public static Point2 Negate(in Point2 v)
				=> new Point2(-v.m_x, -v.m_y); // Negate the members of the vector.
			/// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only m_x and m_y.</summary>
			public static Point2 PerpendicularCcw(in Point2 v)
				=> new Point2(-v.m_y, v.m_x);
			/// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only m_x and m_y.</summary>
			public static Point2 PerpendicularCw(in Point2 v)
				=> new Point2(v.m_y, -v.m_x);
			/// <summary>Create a new random vector using the crypto-grade rng.</summary>
			public static Point2 Random(in int toExlusiveX, in int toExclusiveY)
				=> new Point2(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY));
			/// <summary>Create a new random vector in the range [(0, 0), toExclusive] using the crypto-grade rng.</summary>
			public static Point2 Random(in Point2 toExclusive)
				=> new Point2(Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.m_x), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusive.m_y));
			/// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
			public static Point2 RandomZero(in int toExlusiveX, in int toExclusiveY)
				=> new Point2(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX * 2) - toExlusiveX, Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY * 2) - toExclusiveY);
			/// <summary>Create a new random vector in the range [-toExclusive, toExclusive] using the crypto-grade rng.</summary>
			public static Point2 RandomZero(in Point2 toExclusive)
				=> RandomZero(toExclusive.m_x, toExclusive.m_y);
			/// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
			public static Point2 Remainder(in Point2 v1, in Point2 v2)
				=> new Point2(v1.m_x % v2.m_x, v1.m_y % v2.m_y);
			/// <summary>Create a new vector with the remainder from each member divided by the value. Integer math is used.</summary>
			public static Point2 Remainder(in Point2 v, in int value)
				=> new Point2(v.m_x % value, v.m_y % value);
			/// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
			public static Point2 Remainder(in Point2 v, in double value)
				=> new Point2((int)(v.m_x % value), (int)(v.m_y % value));
			/// <summary>Create a new vector with the difference from the vector subtracted by the other.</summary>
			public static Point2 Subtract(in Point2 v1, in Point2 v2)
				=> new Point2(v1.m_x - v2.m_x, v1.m_y - v2.m_y);
			/// <summary>Create a new vector with the difference from each member subtracted by the value.</summary>
			public static Point2 Subtract(in Point2 v, in int value)
				=> new Point2(v.m_x - value, v.m_y - value);
			/// <summary>Creates a <see cref='Size2'/> from a <see cref='Point2'/>.</summary>
			public static Size2 ToSize2(Point2 point)
				=> new Size2(point.m_x, point.m_y);
			#endregion Static members

			#region "Unique" Index
			/// <summary>Convert an index to a 3D vector, based on the specified lengths of axes.</summary>
			public static Point2 FromUniqueIndex(long index, in Point2 size) => unchecked(new Point2((int)(index % size.m_x), (int)(index / size.m_x)));

			/// <summary>Converts the vector to an index, based on the specified lengths of axes.</summary>
			public static long ToUniqueIndex(in Point2 vector, in Point2 size) => vector.m_x + (vector.m_y * size.m_x);
			#endregion "Unique" Index

			#region Overloaded Operators
			public static Point2 operator -(in Point2 v) => Negate(v);

			public static Point2 operator ~(in Point2 v) => OnesComplement(v);

			public static Point2 operator --(in Point2 v) => Subtract(v, 1);
			public static Point2 operator ++(in Point2 v) => Add(v, 1);

			public static Point2 operator +(Point2 v1, Point2 v2) => Add(v1, v2);
			public static Point2 operator +(Point2 v1, int v2) => Add(v1, v2);
			public static Point2 operator +(int v1, Point2 v2) => Add(v2, v1);

			public static Point2 operator -(Point2 v1, Point2 v2) => Subtract(v1, v2);
			public static Point2 operator -(Point2 v1, int v2) => Subtract(v1, v2);

			public static Point2 operator *(Point2 v1, Point2 v2) => Multiply(v1, v2);
			public static Point2 operator *(Point2 v1, int v2) => Multiply(v1, v2);
			public static Point2 operator *(int v1, Point2 v2) => Multiply(v2, v1);

			public static Point2 operator /(in Point2 v1, in Point2 v2) => Divide(v1, v2);
			public static Point2 operator /(in Point2 v1, in int v2) => Divide(v1, v2);

			public static Point2 operator %(in Point2 v1, in int v2) => Remainder(v1, v2);
			public static Point2 operator %(in Point2 v1, in double v2) => Remainder(v1, v2);

			public static Point2 operator &(in Point2 v1, in Point2 v2) => BitwiseAnd(v1, v2);
			public static Point2 operator &(in Point2 v1, in int v2) => BitwiseAnd(v1, v2);

			public static Point2 operator |(in Point2 v1, in Point2 v2) => BitwiseOr(v1, v2);
			public static Point2 operator |(in Point2 v1, in int v2) => BitwiseOr(v1, v2);

			public static Point2 operator ^(in Point2 v1, in Point2 v2) => Xor(v1, v2);
			public static Point2 operator ^(in Point2 v1, in int v2) => Xor(v1, v2);

			public static Point2 operator <<(in Point2 v1, in int v2) => LeftShift(v1, v2);
			public static Point2 operator >>(in Point2 v1, in int v2) => RightShift(v1, v2);

			public static bool operator ==(in Point2 v1, in Point2 v2) => v1.Equals(v2);
			public static bool operator !=(in Point2 v1, in Point2 v2) => !v1.Equals(v2);
			#endregion Overloaded Operators

			// System.IEquatable<Vector2>
			public bool Equals(Point2 other)
				=> m_x == other.m_x && m_y == other.m_y;

			// Overrides
			public override bool Equals(object? obj)
				 => obj is Point2 o && Equals(o);
			public override int GetHashCode()
				=> System.HashCode.Combine(m_x, m_y);
			public override string ToString()
				=> $"<{nameof(Point2)}: {m_x}, {m_y}>";
		}
	}
}
