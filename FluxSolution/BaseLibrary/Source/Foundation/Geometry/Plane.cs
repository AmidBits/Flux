namespace Flux
{
  /// <summary>A structure encapsulating a 3D Plane.</summary>
  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Plane.cs"/>
  public struct Plane
    : System.IEquatable<Plane>
  {
    private const double NormalizeEpsilon = 1.192092896e-07f; // smallest such that 1.0+NormalizeEpsilon != 1.0

    /// <summary>The normal vector X of the Plane.</summary>
    public double X;
    /// <summary>The normal vector Y of the Plane.</summary>
    public double Y;
    /// <summary>The normal vector Z of the Plane.</summary>
    public double Z;

    /// <summary>The distance of the Plane along its normal from the origin.</summary>
    public double Distance;

    /// <summary>Constructs a Plane from the X, Y, and Z components of its normal, and its distance from the origin on that normal.</summary>
    public Plane(double x, double y, double z, double d)
    {
      X = x;
      Y = y;
      Z = z;
      Distance = d;
    }

    /// <summary>Constructs a Plane from the given normal and distance along the normal from the origin.</summary>
    public Plane(Vector4 normal, double d)
      : this(normal.X, normal.Y, normal.Z, d)
    {
    }

    /// <summary>Constructs a Plane from the given Vector4.</summary>
    /// <param name="value">A vector whose first 3 elements describe the normal vector, and whose W component defines the distance along that normal from the origin.</param>
    public Plane(Vector4 value)
      : this(value.X, value.Y, value.Z, value.W)
    {
    }

    #region Static methods
    /// <summary>Creates a Plane that contains the three given points.</summary>
    public static Plane CreateFromVertices(Vector4 point1, Vector4 point2, Vector4 point3)
    {
      var normal = Vector4.Normalize(Vector4.Cross(point2 - point1, point3 - point1));

      return new(normal, -(normal.X * point1.X + normal.Y * point1.Y + normal.Z * point1.Z));

      //double ax = point2.X - point1.X;
      //double ay = point2.Y - point1.Y;
      //double az = point2.Z - point1.Z;

      //double bx = point3.X - point1.X;
      //double by = point3.Y - point1.Y;
      //double bz = point3.Z - point1.Z;

      //// N=Cross(a,b)
      //double nx = ay * bz - az * by;
      //double ny = az * bx - ax * bz;
      //double nz = ax * by - ay * bx;

      //// Normalize(N)
      //var invNorm = 1 / System.Math.Sqrt(nx * nx + ny * ny + nz * nz);

      //Vector4 normal = new(nx * invNorm, ny * invNorm, nz * invNorm);

      //return new(normal, -(normal.X * point1.X + normal.Y * point1.Y + normal.Z * point1.Z));
    }
    /// <summary>Calculates the dot product of a Plane and Vector4.</summary>
    public static double Dot(Plane plane, Vector4 value)
      => plane.X * value.X + plane.Y * value.Y + plane.Z * value.Z + plane.Distance * value.W;
    /// <summary>Returns the dot product of a specified Vector4 and the normal vector of this Plane plus the distance (D) value of the Plane.</summary>
    public static double DotCoordinate(Plane plane, Vector4 value)
      => plane.X * value.X + plane.Y * value.Y + plane.Z * value.Z + plane.Distance;
    /// <summary>Returns the dot product of a specified Vector4 and the Normal vector of this Plane.</summary>
    public static double DotNormal(Plane plane, Vector4 value)
      => plane.X * value.X + plane.Y * value.Y + plane.Z * value.Z;
    /// <summary>Creates a new Plane whose normal vector is the source Plane's normal vector normalized.</summary>
    public static Plane Normalize(Plane value)
    {
      var f = value.X * value.X + value.Y * value.Y + value.Z * value.Z;

      if (System.Math.Abs(f - 1) < NormalizeEpsilon)
      {
        return value; // It already normalized, so we don't need to further process.
      }

      double fInv = 1 / System.Math.Sqrt(f);

      return new Plane(value.X * fInv, value.Y * fInv, value.Z * fInv, value.Distance * fInv);
    }
    /// <summary>Transforms a normalized Plane by a Matrix.</summary>
    /// <param name="plane"> The normalized Plane to transform. This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
    /// <param name="matrix">The transformation matrix to apply to the Plane.</param>
    public static Plane Transform(Plane plane, Matrix4x4 matrix)
    {
      Matrix4x4.OptimizedInverse(matrix, out var m);

      double x = plane.X, y = plane.Y, z = plane.Z, w = plane.Distance;

      return new Plane(
          x * m.M11 + y * m.M12 + z * m.M13 + w * m.M14,
          x * m.M21 + y * m.M22 + z * m.M23 + w * m.M24,
          x * m.M31 + y * m.M32 + z * m.M33 + w * m.M34,
          x * m.M41 + y * m.M42 + z * m.M43 + w * m.M44);
    }
    /// <summary>Transforms a normalized Plane by a Quaternion rotation.</summary>
    /// <param name="plane"> The normalized Plane to transform. This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
    /// <param name="rotation">The Quaternion rotation to apply to the Plane.</param>
    public static Plane Transform(Plane plane, Quaternion rotation)
    {
      // Compute rotation matrix.
      var x2 = rotation.X + rotation.X;
      var y2 = rotation.Y + rotation.Y;
      var z2 = rotation.Z + rotation.Z;

      var wx2 = rotation.W * x2;
      var wy2 = rotation.W * y2;
      var wz2 = rotation.W * z2;
      var xx2 = rotation.X * x2;
      var xy2 = rotation.X * y2;
      var xz2 = rotation.X * z2;
      var yy2 = rotation.Y * y2;
      var yz2 = rotation.Y * z2;
      var zz2 = rotation.Z * z2;

      var m11 = 1.0f - yy2 - zz2;
      var m21 = xy2 - wz2;
      var m31 = xz2 + wy2;

      var m12 = xy2 + wz2;
      var m22 = 1.0f - xx2 - zz2;
      var m32 = yz2 - wx2;

      var m13 = xz2 - wy2;
      var m23 = yz2 + wx2;
      var m33 = 1.0f - xx2 - yy2;

      double x = plane.X, y = plane.Y, z = plane.Z;

      return new Plane(
          x * m11 + y * m21 + z * m31,
          x * m12 + y * m22 + z * m32,
          x * m13 + y * m23 + z * m33,
          plane.Distance);
    }
    #endregion Static methods

    #region Overloaded operators
    /// <summary>Returns a boolean indicating whether the two given Planes are equal.</summary>
    public static bool operator ==(Plane value1, Plane value2)
      => value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.Distance == value2.Distance;
    /// <summary>Returns a boolean indicating whether the two given Planes are not equal.</summary>
    public static bool operator !=(Plane value1, Plane value2)
      => value1.X != value2.X || value1.Y != value2.Y || value1.Z != value2.Z || value1.Distance != value2.Distance;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    /// <summary>Returns a boolean indicating whether the given Plane is equal to this Plane instance.</summary>
    public readonly bool Equals(Plane p)
      => X == p.X && Y == p.Y && Z == p.Z && Distance == p.Distance;
    #endregion Implemented interfaces

    #region Object overrides
    /// <summary>Returns a boolean indicating whether the given Object is equal to this Plane instance.</summary>
    public override readonly bool Equals(object? obj)
      => obj is Plane o && Equals(o);
    /// <summary>Returns the hash code for this instance.</summary>
    public override readonly int GetHashCode()
      => System.HashCode.Combine(X, Y, Z, Distance);
    /// <summary>Returns a String representing this Plane instance.</summary>
    public override readonly string ToString()
      => $"{GetType().Name} {{ X = {X} Y = {Y} Z = {Z}, Distance = {Distance} }}";
    #endregion Object overrides
  }
}