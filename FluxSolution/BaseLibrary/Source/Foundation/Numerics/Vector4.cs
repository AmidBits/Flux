using System.Runtime.Intrinsics;

namespace Flux
{
  /// <summary>A structure encapsulating four double precision floating point values and provides hardware accelerated methods.</summary>
  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Vector4.cs"/>
  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Vector4_Intrinsics.cs"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct Vector4
    : System.IEquatable<Vector4>
  {
    /// <summary>Returns the vector (0,0,0,0).</summary>
    public static Vector4 Zero
      => new();
    /// <summary>Returns the vector (1,1,1,1).</summary>
    public static Vector4 One
      => new(1, 1, 1, 1);
    /// <summary>Returns the vector (1,0,0,0).</summary>
    public static Vector4 UnitX
      => new(1, 0, 0, 0);
    /// <summary>Returns the vector (0,1,0,0).</summary>
    public static Vector4 UnitY
      => new(0, 1, 0, 0);
    /// <summary>Returns the vector (0,0,1,0).</summary>
    public static Vector4 UnitZ
      => new(0, 0, 1, 0);
    /// <summary>Returns the vector (0,0,0,1).</summary>
    public static Vector4 UnitW
      => new(0, 0, 0, 1);

    private readonly System.Runtime.Intrinsics.Vector256<double> m_v256d;

    /// <summary>Constructs a vector with the specified Vector256.</summary>
    public Vector4(System.Runtime.Intrinsics.Vector256<double> v256d)
      => m_v256d = v256d;
    /// <summary>Constructs a vector with the specified x, y, z and w values.</summary>
    public Vector4(double x, double y, double z, double w)
      : this(System.Runtime.Intrinsics.Vector256.Create(x, y, z, w))
    {
    }
    /// <summary>Constructs a vector with the specified x, y and z values, while W will be assigned a value of 1.</summary>
    public Vector4(double x, double y, double z)
      : this(x, y, z, 0)
    {
    }

    /// <summary>The X component of the vector.</summary>
    public double X
      => m_v256d.GetElement(0);
    /// <summary>The Y component of the vector.</summary>
    public double Y
      => m_v256d.GetElement(1);
    /// <summary>The Z component of the vector.</summary>
    public double Z
      => m_v256d.GetElement(2);
    /// <summary>The W component of the vector.</summary>
    public double W
      => m_v256d.GetElement(3);

    /// <summary>Copies the contents of the vector into the given array.</summary>
    public void CopyTo(double[] array)
      => CopyTo(array, 0);
    /// <summary>Copies the contents of the vector into the given array, starting from index.</summary>
    /// <exception cref="System.NullReferenceException">If array is null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">If index is greater than end of the array or index is less than zero.</exception>
    /// <exception cref="System.ArgumentException">If number of elements in source vector is greater than those available in destination array.</exception>
    public void CopyTo(double[] array, int index)
    {
      if (array == null) throw new System.NullReferenceException(nameof(array));
      if (index < 0 || index >= array.Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      if ((array.Length - index) < 4) throw new System.ArgumentOutOfRangeException(nameof(index));

      array[index] = X;
      array[index + 1] = Y;
      array[index + 2] = Z;
      array[index + 3] = W;
    }

    #region Static methods
    /// <summary>Returns a vector whose elements are the absolute values of each of the source vector's elements.</summary>
    public static Vector4 Abs(in Vector4 v)
      => (Vector4)v.m_v256d.Abs(); // new(System.Math.Abs(v.X), System.Math.Abs(v.Y), System.Math.Abs(v.Z), System.Math.Abs(v.W));
    /// <summary>Calculate the angle between the source vector and the specified target vector. (2D/3D)
    /// when dot eq 0 then the vectors are perpendicular
    /// when dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction)
    /// when dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction)
    /// </summary>
    public static double Angle(in Vector4 v1, in Vector4 v2)
    {
      var cross = v1.m_v256d.Cross(v2.m_v256d);

      return System.Math.Atan2(cross.Normalize().Dot(cross).GetElement(0), v1.m_v256d.Dot(v2.m_v256d).GetElement(0));

      //var cross = Cross(v1, v2);

      //return System.Math.Atan2(Dot(Normalize(cross), cross), Dot(v1, v2));
    }
    public static double AngleToXaxis(in Vector4 v)
      => System.Math.Atan2(System.Math.Sqrt(v.Y * v.Y + v.Z * v.Z), v.X);
    public static double AngleToYaxis(in Vector4 v)
      => System.Math.Atan2(System.Math.Sqrt(v.Z * v.Z + v.X * v.X), v.Y);
    public static double AngleToZaxis(in Vector4 v)
      => System.Math.Atan2(System.Math.Sqrt(v.X * v.X + v.Y * v.Y), v.Z);
    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in Vector4 v1, in Vector4 v2, double edgeLength = 1)
      => v1.m_v256d.ChebyshevDistance(v2.m_v256d, edgeLength).GetElement(0);
    /// <summary>Compute the Chebyshev length of vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevLength(in Vector4 v, double edgeLength = 1)
      => v.m_v256d.ChebyshevLength(edgeLength).GetElement(0);
    /// <summary>Restricts a vector between a min and max value.</summary>
    public static Vector4 Clamp(in Vector4 v, in Vector4 min, in Vector4 max)
      => (Vector4)v.m_v256d.Clamp(min.m_v256d, max.m_v256d);
    /// <summary>Restricts a vector between a min and max value.</summary>
    public static Vector4 Clamp(in Vector4 v, double min, double max)
      => (Vector4)v.m_v256d.Clamp(min, max);
    /// <summary>Computes the cross product of two vectors.</summary>
    public static Vector4 Cross(in Vector4 v1, in Vector4 v2)
      => (Vector4)v1.m_v256d.Cross(v2.m_v256d);
    /// <summary>Returns the dot product of two vectors.</summary>
    public static double Dot(in Vector4 v1, in Vector4 v2)
      => v1.m_v256d.Dot(v2.m_v256d).GetElement(0);
    /// <summary>
    /// Returns the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="v1">The first point.</param>
    /// <param name="v2">The second point.</param>
    /// <returns>The distance.</returns>
    public static double EuclideanDistance(in Vector4 v1, in Vector4 v2)
      => v1.m_v256d.EuclideanDistance(v2.m_v256d).GetElement(0);
    /// <summary>
    /// Returns the Euclidean distance squared between the two given points.
    /// </summary>
    /// <param name="v1">The first point.</param>
    /// <param name="v2">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static double EuclideanDistanceSquared(in Vector4 v1, in Vector4 v2)
      => v1.m_v256d.Subtract(v2.m_v256d).EuclideanLengthSquared().GetElement(0);
    /// <summary>Returns the length of the vector. This operation is cheaper than Length().</summary>
    public static double EuclideanLength(in Vector4 v)
      => v.m_v256d.EuclideanLength().GetElement(0);
    /// <summary>Returns the length of the vector squared.</summary>
    public static double EuclideanLengthSquared(in Vector4 v)
      => v.m_v256d.EuclideanLengthSquared().GetElement(0);
    /// <summary>Linearly interpolates between two vectors based on the given weighting.</summary>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
    public static Vector4 Lerp(in Vector4 v1, in Vector4 v2, double amount)
      => (Vector4)v1.m_v256d.Lerp(v2.m_v256d, amount);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(in Vector4 v1, in Vector4 v2, double edgeLength = 1)
      => v2.m_v256d.ManhattanDistance(v1.m_v256d, edgeLength).GetElement(0);
    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanLength(in Vector4 v, double edgeLength = 1)
      => v.m_v256d.ManhattanLength(edgeLength).GetElement(0);

    /// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.</summary>
    public static Vector4 Max(in Vector4 v1, in Vector4 v2)
      => (Vector4)v1.m_v256d.Max(v2.m_v256d);
    /// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.</summary>
    public static Vector4 Min(in Vector4 v1, in Vector4 v2)
      => (Vector4)v1.m_v256d.Min(v2.m_v256d);

    public static double MinkowskiDistance(Vector4 v1, Vector4 v2, int order)
      => v1.m_v256d.MinkowskiDistance(v2.m_v256d, order).GetElement(0);
    public static double MinkowskiLength(Vector4 v, int order)
      => v.m_v256d.MinkowskiLength(order).GetElement(0);

    /// <summary>Returns a vector with the same direction as the given vector, but with a length of 1.</summary>
    public static Vector4 Normalize(in Vector4 vector)
      => (Vector4)vector.m_v256d.Normalize();
    /// <summary>Negates a given vector.</summary>
    public static Vector4 Negate(in Vector4 v)
      => (Vector4)v.m_v256d.Negate();
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(in Vector4 a, in Vector4 b, in Vector4 c)
      => a.m_v256d.Dot(b.m_v256d.Cross(c.m_v256d)).GetElement(0);
    /// <summary>Returns a vector whose elements are the square root of each of the source vector's elements.</summary>
    public static Vector4 Sqrt(in Vector4 v)
      => (Vector4)v.m_v256d.Sqrt();
    /// <summary>Transforms a vector by the given matrix.</summary>
    public static Vector4 Transform(in Vector4 v, in Matrix4 m)
      => new(
        v.X * m.M11 + v.Y * m.M21 + v.Z * m.M31 + v.W * m.M41,
        v.X * m.M12 + v.Y * m.M22 + v.Z * m.M32 + v.W * m.M42,
        v.X * m.M13 + v.Y * m.M23 + v.Z * m.M33 + v.W * m.M43,
        v.X * m.M14 + v.Y * m.M24 + v.Z * m.M34 + v.W * m.M44
      );
    /// <summary>Transforms a vector by the given Quaternion rotation value.</summary>
    public static Vector4 Transform(in Vector4 v, in Quaternion q)
    {
      var x2 = q.X + q.X;
      var y2 = q.Y + q.Y;
      var z2 = q.Z + q.Z;

      var wx2 = q.W * x2;
      var wy2 = q.W * y2;
      var wz2 = q.W * z2;
      var xx2 = q.X * x2;
      var xy2 = q.X * y2;
      var xz2 = q.X * z2;
      var yy2 = q.Y * y2;
      var yz2 = q.Y * z2;
      var zz2 = q.Z * z2;

      return new Vector4(
        v.X * (1 - yy2 - zz2) + v.Y * (xy2 - wz2) + v.Z * (xz2 + wy2),
        v.X * (xy2 + wz2) + v.Y * (1 - xx2 - zz2) + v.Z * (yz2 - wx2),
        v.X * (xz2 - wy2) + v.Y * (yz2 + wx2) + v.Z * (1 - xx2 - yy2),
        v.W
      );
    }
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static Vector4 VectorTripleProduct(in Vector4 a, in Vector4 b, in Vector4 c)
      => (Vector4)a.m_v256d.Cross(b.m_v256d.Cross(c.m_v256d));
    #endregion Static methods

    #region Operator overloads
    public static explicit operator System.Runtime.Intrinsics.Vector256<double>(Vector4 v)
      => v.m_v256d;
    public static explicit operator Vector4(System.Runtime.Intrinsics.Vector256<double> v)
      => new(v);

    /// <summary>Returns a boolean indicating whether the two given vectors are equal.</summary>
    public static bool operator ==(in Vector4 v1, in Vector4 v2)
      => v1.Equals(v2);
    /// <summary>Returns a boolean indicating whether the two given vectors are not equal.</summary>
    public static bool operator !=(in Vector4 v1, in Vector4 v2)
      => !v1.Equals(v2);

    /// <summary>Negates a given vector.</summary>
    public static Vector4 operator -(in Vector4 v)
      => (Vector4)v.m_v256d.Negate();
    /// <summary>Adds two vectors together.</summary>
    public static Vector4 operator +(in Vector4 v1, in Vector4 v2)
      => (Vector4)v1.m_v256d.Add(v2.m_v256d);
    /// <summary>Subtracts the second vector from the first.</summary>
    public static Vector4 operator -(in Vector4 v1, in Vector4 v2)
      => (Vector4)v1.m_v256d.Subtract(v2.m_v256d);
    /// <summary>Multiplies two vectors together.</summary>
    public static Vector4 operator *(in Vector4 v1, in Vector4 v2)
      => (Vector4)v1.m_v256d.Multiply(v2.m_v256d);
    /// <summary>Multiplies a vector by the given scalar.</summary>
    public static Vector4 operator *(in Vector4 v, double scalar)
      => (Vector4)v.m_v256d.Add(scalar);
    /// <summary>Multiplies a vector by the given scalar.</summary>
    public static Vector4 operator *(double scalar, in Vector4 v)
      => (Vector4)v.m_v256d.Add(scalar);
    /// <summary>Divides the first vector by the second.</summary>
    public static Vector4 operator /(in Vector4 v1, in Vector4 v2)
      => (Vector4)v1.m_v256d.Divide(v2.m_v256d);
    /// <summary>Divides the vector by the given scalar.</summary>
    public static Vector4 operator /(in Vector4 v, double divisor)
      => (Vector4)v.m_v256d.Add(divisor);
    #endregion Operator overloads

    #region Implemented interfaces
    // IEquatable
    /// <summary>Returns a boolean indicating whether the given Vector4 is equal to this Vector4 instance.</summary>
    public bool Equals(Vector4 v)
      => X == v.X && Y == v.Y && Z == v.Z && W == v.W;
    #endregion Implemented interfaces

    #region Object overrides
    /// <summary>Returns a boolean indicating whether the given Object is equal to this Vector4 instance.</summary>
    public override bool Equals(object? obj)
      => obj is Vector4 o && Equals(o);
    /// <summary>Returns the hash code for this instance.</summary>
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y, Z, W);
    /// <summary>Returns a String representing this Quaternion instance.</summary>
    public override string ToString()
      => $"{GetType().Name} {{ X = {X} Y = {Y} Z = {Z} W = {W} }}";
    #endregion Object overrides
  }
}
