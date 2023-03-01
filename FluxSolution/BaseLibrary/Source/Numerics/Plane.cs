namespace Flux.Numerics
{
  /// <summary>A structure encapsulating a 3D Plane.</summary>
  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Plane.cs"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct Plane<TSelf>
    : IPlane<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    private readonly static TSelf NormalizeEpsilon = TSelf.CreateChecked(1.192092896e-07f); // Smallest such that 1.0+NormalizeEpsilon != 1.0

    /// <summary>The normal vector X of the Plane.</summary>
    private readonly TSelf m_x;
    /// <summary>The normal vector Y of the Plane.</summary>
    private readonly TSelf m_y;
    /// <summary>The normal vector Z of the Plane.</summary>
    private readonly TSelf m_z;

    /// <summary>The distance of the Plane along its normal from the origin.</summary>
    private readonly TSelf m_distance;

    /// <summary>Constructs a Plane from the X, Y, and Z components of its normal, and its distance from the origin on that normal.</summary>
    public Plane(TSelf x, TSelf y, TSelf z, TSelf distance)
    {
      m_x = x;
      m_y = y;
      m_z = z;

      m_distance = distance;
    }

    /// <summary>Constructs a Plane from the given normal (the W component is not used) and distance along the normal from the origin.</summary>
    public Plane(Numerics.CartesianCoordinate4 normal, TSelf distance)
      : this(TSelf.CreateChecked(normal.X), TSelf.CreateChecked(normal.Y), TSelf.CreateChecked(normal.Z), distance)
    {
    }

    /// <summary>Constructs a Plane from the given Vector4.</summary>
    /// <param name="value">A vector whose first 3 elements describe the normal vector, and whose W component defines the distance along that normal from the origin.</param>
    public Plane(Numerics.CartesianCoordinate4 value)
      : this(TSelf.CreateChecked(value.X), TSelf.CreateChecked(value.Y), TSelf.CreateChecked(value.Z), TSelf.CreateChecked(value.W))
    {
    }

    public void Deconstruct(out TSelf x, out TSelf y, out TSelf z, out TSelf distance) { x = m_x; y = m_y; z = m_z; distance = m_distance; }

    /// <summary>The normal vector X of the Plane.</summary>
    public TSelf X { get => m_x; init => m_x = value; }
    /// <summary>The normal vector Y of the Plane.</summary>
    public TSelf Y { get => m_y; init => m_y = value; }
    /// <summary>The normal vector Z of the Plane.</summary>
    public TSelf Z { get => m_z; init => m_z = value; }

    /// <summary>The distance of the Plane along its normal from the origin.</summary>
    public TSelf Distance { get => m_distance; init => m_distance = value; }

    public TSelf LengthSquared() => m_x * m_x + m_y * m_y + m_z * m_z;

    /// <summary>Creates a new Plane whose normal vector is the source Plane's normal vector normalized.</summary>
    public Plane<TSelf> Normalize()
    {
      var ls = LengthSquared();

      if (TSelf.Abs(ls - TSelf.One) < NormalizeEpsilon)
        return this; // It already normalized, so we don't need to further process.

      var invLen = TSelf.One / TSelf.Sqrt(ls);

      return new Plane<TSelf>(m_x * invLen, m_y * invLen, m_z * invLen, m_distance * invLen);
    }

    #region Static methods

    /// <summary>Creates a Plane that contains the three given points.</summary>
    public static Plane<TSelf> CreateFromVertices(Numerics.CartesianCoordinate4 point1, Numerics.CartesianCoordinate4 point2, Numerics.CartesianCoordinate4 point3)
    {
      var normal = Numerics.CartesianCoordinate4.Normalize(Numerics.CartesianCoordinate4.Cross(point2 - point1, point3 - point1));

      return new(normal, -TSelf.CreateChecked(Numerics.CartesianCoordinate4.EuclideanLengthSquared(normal)));

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
    public static TSelf Dot(Plane<TSelf> plane, Numerics.CartesianCoordinate4 value)
      => DotCoordinate(plane, value) * TSelf.CreateChecked(value.W);

    /// <summary>Returns the dot product of a specified Vector4 and the normal vector of this Plane plus the distance (D) value of the Plane.</summary>
    public static TSelf DotCoordinate(Plane<TSelf> plane, Numerics.CartesianCoordinate4 value)
      => DotNormal(plane, value) + plane.m_distance;

    /// <summary>Returns the dot product of a specified Vector4 and the Normal vector of this Plane.</summary>
    public static TSelf DotNormal(Plane<TSelf> plane, Numerics.CartesianCoordinate4 value)
      => plane.m_x * TSelf.CreateChecked(value.X) + plane.m_y * TSelf.CreateChecked(value.Y) + plane.m_z * TSelf.CreateChecked(value.Z);

    /// <summary>Transforms a normalized Plane by a Matrix.</summary>
    /// <param name="plane"> The normalized Plane to transform. This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
    /// <param name="matrix">The transformation matrix to apply to the Plane.</param>
    public static Plane<TSelf> Transform(Plane<TSelf> plane, Numerics.IMatrix4<TSelf> matrix)
    {
      matrix.TryGetInverseOptimized(out var m);

      var x = plane.m_x;
      var y = plane.m_y;
      var z = plane.m_z;
      var w = plane.m_distance;

      return new(
          x * m.M11 + y * m.M12 + z * m.M13 + w * m.M14,
          x * m.M21 + y * m.M22 + z * m.M23 + w * m.M24,
          x * m.M31 + y * m.M32 + z * m.M33 + w * m.M34,
          x * m.M41 + y * m.M42 + z * m.M43 + w * m.M44);
    }

    /// <summary>Transforms a normalized Plane by a Quaternion rotation.</summary>
    /// <param name="plane"> The normalized Plane to transform. This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
    /// <param name="rotation">The Quaternion rotation to apply to the Plane.</param>
    public static Plane<TSelf> Transform(Plane<TSelf> plane, Numerics.Quaternion<TSelf> rotation)
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

      var m11 = TSelf.One - yy2 - zz2;
      var m21 = xy2 - wz2;
      var m31 = xz2 + wy2;

      var m12 = xy2 + wz2;
      var m22 = TSelf.One - xx2 - zz2;
      var m32 = yz2 - wx2;

      var m13 = xz2 - wy2;
      var m23 = yz2 + wx2;
      var m33 = TSelf.One - xx2 - yy2;

      var x = plane.m_x;
      var y = plane.m_y;
      var z = plane.m_z;

      return new(
          x * m11 + y * m21 + z * m31,
          x * m12 + y * m22 + z * m32,
          x * m13 + y * m23 + z * m33,
          plane.m_distance);
    }

    #endregion Static methods
  }
}
