namespace Flux.Numerics
{
  /// <summary>A structure encapsulating four double precision floating point values and provides hardware accelerated methods.</summary>
  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Vector4.cs"/>
  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Vector4_Intrinsics.cs"/>
  public struct Vector4
    : System.IEquatable<Vector4>
  {
    /// <summary>Returns the vector (0,0,0,0).</summary>
    public static Vector4 Zero
      => new Vector4();
    /// <summary>Returns the vector (1,1,1,1).</summary>
    public static Vector4 One
      => new Vector4(1, 1, 1, 1);
    /// <summary>Returns the vector (1,0,0,0).</summary>
    public static Vector4 UnitX
      => new Vector4(1, 0, 0, 0);
    /// <summary>Returns the vector (0,1,0,0).</summary>
    public static Vector4 UnitY
      => new Vector4(0, 1, 0, 0);
    /// <summary>Returns the vector (0,0,1,0).</summary>
    public static Vector4 UnitZ
      => new Vector4(0, 0, 1, 0);
    /// <summary>Returns the vector (0,0,0,1).</summary>
    public static Vector4 UnitW
      => new Vector4(0, 0, 0, 1);

    /// <summary>The X component of the vector.</summary>
    public double X { get; set; }
    /// <summary>The Y component of the vector.</summary>
    public double Y { get; set; }
    /// <summary>The Z component of the vector.</summary>
    public double Z { get; set; }
    /// <summary>The W component of the vector.</summary>
    public double W { get; set; }

    /// <summary>Constructs a vector with the specified x, y and z values, while W will be assigned a value of 1.</summary>
    public Vector4(double x, double y, double z)
      : this(x, y, z, 1)
    {
    }
    /// <summary>Constructs a vector with the specified x, y, z and w values.</summary>
    public Vector4(double x, double y, double z, double w)
    {
      W = w;
      X = x;
      Y = y;
      Z = z;
    }

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
    /// <summary>Returns the length of the vector. This operation is cheaper than Length().</summary>
    public double Length()
      => System.Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
    /// <summary>Returns the length of the vector squared.</summary>
    public double LengthSquared()
      => X * X + Y * Y + Z * Z + W * W;

    #region Static methods
    /// <summary>Returns a vector whose elements are the absolute values of each of the source vector's elements.</summary>
    public static Vector4 Abs(in Vector4 v)
      => new Vector4(System.Math.Abs(v.X), System.Math.Abs(v.Y), System.Math.Abs(v.Z), System.Math.Abs(v.W));
    /// <summary>Adds two vectors together.</summary>
    public static Vector4 Add(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    /// <summary>Calculate the angle between the source vector and the specified target vector. (2D/3D)
    /// when dot eq 0 then the vectors are perpendicular
    /// when dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction)
    /// when dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction)
    /// </summary>
    public static double Angle(in Vector4 v1, in Vector4 v2)
    {
      var cross = Cross(v1, v2);

      return System.Math.Atan2(Dot(Normalize(cross), cross), Dot(v1, v2));
    }
    public static double AngleToXaxis(in Vector4 v)
      => System.Math.Atan2(System.Math.Sqrt(v.Y * v.Y + v.Z * v.Z), v.X);
    public static double AngleToYaxis(in Vector4 v)
      => System.Math.Atan2(System.Math.Sqrt(v.Z * v.Z + v.X * v.X), v.Y);
    public static double AngleToZaxis(in Vector4 v)
      => System.Math.Atan2(System.Math.Sqrt(v.X * v.X + v.Y * v.Y), v.Z);
    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in Vector4 a, in Vector4 b, double edgeLength = 1)
      => Maths.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength, (b.Z - a.Z) / edgeLength);
    /// <summary>Restricts a vector between a min and max value.</summary>
    public static Vector4 Clamp(in Vector4 v, in Vector4 min, in Vector4 max)
    {
      // This compare order is very important!!!
      // We must follow HLSL behavior in the case user specified min value is bigger than max value.

      var x = v.X;
      x = (x > max.X) ? max.X : x;
      x = (x < min.X) ? min.X : x;

      var y = v.Y;
      y = (y > max.Y) ? max.Y : y;
      y = (y < min.Y) ? min.Y : y;

      var z = v.Z;
      z = (z > max.Z) ? max.Z : z;
      z = (z < min.Z) ? min.Z : z;

      var w = v.W;
      w = (w > max.W) ? max.W : w;
      w = (w < min.W) ? min.W : w;

      return new Vector4(x, y, z, w);
    }
    /// <summary>Computes the cross product of two vectors.</summary>
    public static Vector4 Cross(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X, 1.0);
    /// <summary>
    /// Returns the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="v1">The first point.</param>
    /// <param name="v2">The second point.</param>
    /// <returns>The distance.</returns>
    public static double Distance(in Vector4 v1, in Vector4 v2)
    {
      var x = v1.X - v2.X;
      var y = v1.Y - v2.Y;
      var z = v1.Z - v2.Z;
      var w = v1.W - v2.W;

      return System.Math.Sqrt(x * x + y * y + z * z + w * w);
    }
    /// <summary>
    /// Returns the Euclidean distance squared between the two given points.
    /// </summary>
    /// <param name="v1">The first point.</param>
    /// <param name="v2">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static double DistanceSquared(in Vector4 v1, in Vector4 v2)
    {
      var dx = v1.X - v2.X;
      var dy = v1.Y - v2.Y;
      var dz = v1.Z - v2.Z;
      var dw = v1.W - v2.W;

      return dx * dx + dy * dy + dz * dz + dw * dw;
    }
    /// <summary>Divides the first vector by the second.</summary>
    public static Vector4 Divide(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z, v1.W / v2.W);
    /// <summary>Divides the vector by the given scalar.</summary>
    public static Vector4 Divide(in Vector4 v, double divisor)
      => new Vector4(v.X / divisor, v.Y / divisor, v.Z / divisor, v.W / divisor);
    /// <summary>Returns the dot product of two vectors.</summary>
    public static double Dot(in Vector4 v1, in Vector4 v2)
      => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
    /// <summary>Linearly interpolates between two vectors based on the given weighting.</summary>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
    public static Vector4 Lerp(in Vector4 v1, in Vector4 v2, double amount)
      => new Vector4(v1.X + (v2.X - v1.X) * amount, v1.Y + (v2.Y - v1.Y) * amount, v1.Z + (v2.Z - v1.Z) * amount, v1.W + (v2.W - v1.W) * amount);
    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(in Vector4 v1, in Vector4 v2, double edgeLength = 1)
      => System.Math.Abs(v2.X - v1.X) / edgeLength + System.Math.Abs(v2.Y - v1.Y) / edgeLength + System.Math.Abs(v2.Z - v1.Z) / edgeLength;
    /// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.</summary>
    public static Vector4 Max(in Vector4 v1, in Vector4 v2)
      => new Vector4((v1.X > v2.X) ? v1.X : v2.X, (v1.Y > v2.Y) ? v1.Y : v2.Y, (v1.Z > v2.Z) ? v1.Z : v2.Z, (v1.W > v2.W) ? v1.W : v2.W);
    /// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.</summary>
    public static Vector4 Min(in Vector4 v1, in Vector4 v2)
      => new Vector4((v1.X < v2.X) ? v1.X : v2.X, (v1.Y < v2.Y) ? v1.Y : v2.Y, (v1.Z < v2.Z) ? v1.Z : v2.Z, (v1.W < v2.W) ? v1.W : v2.W);
    /// <summary>Multiplies two vectors together.</summary>
    public static Vector4 Multiply(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
    /// <summary>Multiplies a vector by the given scalar.</summary>
    public static Vector4 Multiply(in Vector4 v1, double v2)
      => v1 * new Vector4(v2, v2, v2, v2);
    /// <summary>Multiplies a vector by the given scalar.</summary>
    public static Vector4 Multiply(double v1, Vector4 v2)
      => new Vector4(v1, v1, v1, v1) * v2;
    /// <summary>Returns a vector with the same direction as the given vector, but with a length of 1.</summary>
    public static Vector4 Normalize(in Vector4 vector)
      => Multiply(vector, 1 / vector.LengthSquared());
    /// <summary>Negates a given vector.</summary>
    public static Vector4 Negate(in Vector4 v)
      => new Vector4(-v.X, -v.Y, -v.Z, -v.W);
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(in Vector4 a, in Vector4 b, in Vector4 c) => Dot(a, Cross(b, c));
    /// <summary>Returns a vector whose elements are the square root of each of the source vector's elements.</summary>
    public static Vector4 Sqrt(in Vector4 v)
      => new Vector4(System.Math.Sqrt(v.X), System.Math.Sqrt(v.Y), System.Math.Sqrt(v.Z), System.Math.Sqrt(v.W));
    /// <summary>Subtracts the second vector from the first.</summary>
    public static Vector4 Subtract(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    /// <summary>Transforms a vector by the given matrix.</summary>
    public static Vector4 Transform(in Vector4 v, in Matrix4x4 m) => new Vector4(
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
    public static Vector4 VectorTripleProduct(in Vector4 a, in Vector4 b, in Vector4 c) => Cross(a, Cross(b, c));
    #endregion Static methods

    #region Operator overloads
    /// <summary>Returns a boolean indicating whether the two given vectors are equal.</summary>
    public static bool operator ==(in Vector4 v1, in Vector4 v2)
      => v1.Equals(v2);
    /// <summary>Returns a boolean indicating whether the two given vectors are not equal.</summary>
    public static bool operator !=(in Vector4 v1, in Vector4 v2)
      => !v1.Equals(v2);

    /// <summary>Adds two vectors together.</summary>
    public static Vector4 operator +(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    /// <summary>Subtracts the second vector from the first.</summary>
    public static Vector4 operator -(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    /// <summary>Multiplies two vectors together.</summary>
    public static Vector4 operator *(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
    /// <summary>Multiplies a vector by the given scalar.</summary>
    public static Vector4 operator *(in Vector4 v, double scalar)
      => new Vector4(v.X * scalar, v.Y * scalar, v.Z * scalar, v.W * scalar);
    /// <summary>Multiplies a vector by the given scalar.</summary>
    public static Vector4 operator *(double scalar, in Vector4 v)
      => new Vector4(scalar * v.X, scalar * v.Y, scalar * v.Z, scalar * v.W);
    /// <summary>Divides the first vector by the second.</summary>
    public static Vector4 operator /(in Vector4 v1, in Vector4 v2)
      => new Vector4(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z, v1.W / v2.W);
    /// <summary>Divides the vector by the given scalar.</summary>
    public static Vector4 operator /(in Vector4 v, double divisor)
      => new Vector4(v.X / divisor, v.Y / divisor, v.Z / divisor, v.W / divisor);
    /// <summary>Negates a given vector.</summary>
    public static Vector4 operator -(in Vector4 v)
      => new Vector4(-v.X, -v.Y, -v.Z, -v.W);
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
      => $"<{GetType().Name}: X={X} Y={Y} Z={Z} W={W}>";
    #endregion Object overrides
  }
}
