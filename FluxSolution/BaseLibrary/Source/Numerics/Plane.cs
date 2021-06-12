//namespace Flux.Numerics
//{
//  /// <summary>A structure encapsulating a 3D Plane.</summary>
//  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Plane.cs"/>
//  public struct Plane
//    : System.IEquatable<Plane>, System.IFormattable
//  {
//    private const double NormalizeEpsilon = 1.192092896e-07f; // smallest such that 1.0+NormalizeEpsilon != 1.0

//    /// <summary>The normal vector of the Plane.</summary>
//    public Vector4 Normal { get; set; }
//    /// <summary>The distance of the Plane along its normal from the origin.</summary>
//    public double D { get; set; }

//    /// <summary>Constructs a Plane from the X, Y, and Z components of its normal, and its distance from the origin on that normal.</summary>
//    public Plane(double x, double y, double z, double d)
//    {
//      Normal = new Vector4(x, y, z);
//      D = d;
//    }

//    /// <summary>Constructs a Plane from the given normal and distance along the normal from the origin.</summary>
//    public Plane(Vector4 normal, double d)
//    {
//      Normal = normal;
//      D = d;
//    }

//    /// <summary>Constructs a Plane from the given Vector4.</summary>
//    /// <param name="value">A vector whose first 3 elements describe the normal vector, and whose W component defines the distance along that normal from the origin.</param>
//    public Plane(Vector4 value)
//    {
//      Normal = new Vector4(value.X, value.Y, value.Z);
//      D = value.W;
//    }

//    /// <summary>Returns a boolean indicating whether the two given Planes are equal.</summary>
//    public static bool operator ==(Plane value1, Plane value2)
//      => value1.Normal.X == value2.Normal.X && value1.Normal.Y == value2.Normal.Y && value1.Normal.Z == value2.Normal.Z && value1.D == value2.D;
//    /// <summary>Returns a boolean indicating whether the two given Planes are not equal.</summary>
//    public static bool operator !=(Plane value1, Plane value2)
//      => value1.Normal.X != value2.Normal.X || value1.Normal.Y != value2.Normal.Y || value1.Normal.Z != value2.Normal.Z || value1.D != value2.D;

//    /// <summary>Creates a Plane that contains the three given points.</summary>
//    public static Plane CreateFromVertices(Vector4 point1, Vector4 point2, Vector4 point3)
//    {
//      double ax = point2.X - point1.X;
//      double ay = point2.Y - point1.Y;
//      double az = point2.Z - point1.Z;

//      double bx = point3.X - point1.X;
//      double by = point3.Y - point1.Y;
//      double bz = point3.Z - point1.Z;

//      // N=Cross(a,b)
//      double nx = ay * bz - az * by;
//      double ny = az * bx - ax * bz;
//      double nz = ax * by - ay * bx;

//      // Normalize(N)
//      var invNorm = 1 / System.Math.Sqrt(nx * nx + ny * ny + nz * nz);

//      Vector4 normal = new Vector4(nx * invNorm, ny * invNorm, nz * invNorm);

//      return new Plane(normal, -(normal.X * point1.X + normal.Y * point1.Y + normal.Z * point1.Z));
//    }
//    /// <summary>Calculates the dot product of a Plane and Vector4.</summary>
//    public static double Dot(Plane plane, Vector4 value)
//      => plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D * value.W;
//    /// <summary>Returns the dot product of a specified Vector4 and the normal vector of this Plane plus the distance (D) value of the Plane.</summary>
//    public static double DotCoordinate(Plane plane, Vector4 value)
//      => plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D;
//    /// <summary>Returns the dot product of a specified Vector4 and the Normal vector of this Plane.</summary>
//    public static double DotNormal(Plane plane, Vector4 value)
//      => plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z;
//    /// <summary>Creates a new Plane whose normal vector is the source Plane's normal vector normalized.</summary>
//    public static Plane Normalize(Plane value)
//    {
//      var f = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;

//      if (System.Math.Abs(f - 1) < NormalizeEpsilon)
//      {
//        return value; // It already normalized, so we don't need to further process.
//      }

//      double fInv = 1 / System.Math.Sqrt(f);

//      return new Plane(value.Normal.X * fInv, value.Normal.Y * fInv, value.Normal.Z * fInv, value.D * fInv);
//    }
//    /// <summary>Transforms a normalized Plane by a Matrix.</summary>
//    /// <param name="plane"> The normalized Plane to transform. This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
//    /// <param name="matrix">The transformation matrix to apply to the Plane.</param>
//    public static Plane Transform(Plane plane, Matrix4x4 matrix)
//    {
//      Matrix4x4 m;
//      Matrix4x4.Invert(matrix, out m);

//      double x = plane.Normal.X, y = plane.Normal.Y, z = plane.Normal.Z, w = plane.D;

//      return new Plane(
//          x * m.M11 + y * m.M12 + z * m.M13 + w * m.M14,
//          x * m.M21 + y * m.M22 + z * m.M23 + w * m.M24,
//          x * m.M31 + y * m.M32 + z * m.M33 + w * m.M34,
//          x * m.M41 + y * m.M42 + z * m.M43 + w * m.M44);
//    }
//    /// <summary>Transforms a normalized Plane by a Quaternion rotation.</summary>
//    /// <param name="plane"> The normalized Plane to transform. This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
//    /// <param name="rotation">The Quaternion rotation to apply to the Plane.</param>
//    public static Plane Transform(Plane plane, Quaternion rotation)
//    {
//      // Compute rotation matrix.
//      var x2 = rotation.X + rotation.X;
//      var y2 = rotation.Y + rotation.Y;
//      var z2 = rotation.Z + rotation.Z;

//      var wx2 = rotation.W * x2;
//      var wy2 = rotation.W * y2;
//      var wz2 = rotation.W * z2;
//      var xx2 = rotation.X * x2;
//      var xy2 = rotation.X * y2;
//      var xz2 = rotation.X * z2;
//      var yy2 = rotation.Y * y2;
//      var yz2 = rotation.Y * z2;
//      var zz2 = rotation.Z * z2;

//      var m11 = 1.0f - yy2 - zz2;
//      var m21 = xy2 - wz2;
//      var m31 = xz2 + wy2;

//      var m12 = xy2 + wz2;
//      var m22 = 1.0f - xx2 - zz2;
//      var m32 = yz2 - wx2;

//      var m13 = xz2 - wy2;
//      var m23 = yz2 + wx2;
//      var m33 = 1.0f - xx2 - yy2;

//      double x = plane.Normal.X, y = plane.Normal.Y, z = plane.Normal.Z;

//      return new Plane(
//          x * m11 + y * m21 + z * m31,
//          x * m12 + y * m22 + z * m32,
//          x * m13 + y * m23 + z * m33,
//          plane.D);
//    }

//    // System.Equatable
//    /// <summary>Returns a boolean indicating whether the given Plane is equal to this Plane instance.</summary>
//    public readonly bool Equals(Plane p)
//      => Normal.X == p.Normal.X && Normal.Y == p.Normal.Y && Normal.Z == p.Normal.Z && D == p.D;
//    // System.Formattable
//    public string ToString(string? format, System.IFormatProvider? provider)
//    {
//      provider ??= System.Globalization.CultureInfo.CurrentCulture;

//      return string.Format(provider, @"<X:{0} Y:{1} Z:{2} W:{3}, D:{4}>", Normal.X.ToString(format, provider), Normal.Y.ToString(format, provider), Normal.Z.ToString(format, provider), Normal.W.ToString(format, provider), D.ToString(format, provider));
//    }
//    public string ToString(string? format)
//      => ToString(format, System.Globalization.CultureInfo.CurrentCulture);

//    /// <summary>Returns a boolean indicating whether the given Object is equal to this Plane instance.</summary>
//    public override readonly bool Equals(object? obj)
//      => obj is Plane ? Equals((Plane)obj) : false;
//    /// <summary>Returns the hash code for this instance.</summary>
//    public override readonly int GetHashCode()
//      => Normal.GetHashCode() + D.GetHashCode();
//    /// <summary>Returns a String representing this Plane instance.</summary>
//    public override readonly string ToString()
//    {
//      var ci = System.Globalization.CultureInfo.CurrentCulture;

//      return string.Format(ci, "{{Normal:{0} D:{1}}}", Normal.ToString(), D.ToString(ci));
//    }
//  }
//}
