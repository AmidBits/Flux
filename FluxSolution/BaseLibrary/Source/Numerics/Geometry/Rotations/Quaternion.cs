//#if NET7_0_OR_GREATER
//namespace Flux.Geometry
//{
//  /// <summary>A structure encapsulating a four-dimensional vector (x,y,z,w), which is used to efficiently rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).</summary>
//  /// <remarks>All angles in radians.</remarks>
//  /// <see href="https://github.com/mono/mono/blob/c5b88ec4f323f2bdb7c7d0a595ece28dae66579c/mcs/class/referencesource/System.Numerics/System/Numerics/Quaternion.cs"/>
//  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//  public readonly record struct Quaternion
//    : IQuaternion
//  {
//    /// <summary>Returns a Quaternion representing no rotation.</summary>
//    public static readonly Quaternion Identity = new(0, 0, 0, 1);

//    public static readonly double Half = double.CreateChecked(0.5);

//    private readonly double m_x;
//    private readonly double m_y;
//    private readonly double m_z;
//    private readonly double m_w;

//    /// <summary>Constructs a Quaternion from the given components.</summary>
//    public Quaternion(double x, double y, double z, double w)
//    {
//      m_x = x;
//      m_y = y;
//      m_z = z;
//      m_w = w;
//    }
//    public Quaternion(ICartesianCoordinate3<double> xyz, double w) : this(xyz.X, xyz.Y, xyz.Z, w) { }

//    public void Deconstruct(out double x, out double y, out double z, out double w) { x = m_x; y = m_y; z = m_z; w = m_w; }

//    /// <summary>Specifies the X-value of the vector component of the Quaternion.</summary>
//    public double X { get => m_x; init => m_x = value; }
//    /// <summary>Specifies the Y-value of the vector component of the Quaternion.</summary>
//    public double Y { get => m_y; init => m_y = value; }
//    /// <summary>Specifies the Z-value of the vector component of the Quaternion.</summary>
//    public double Z { get => m_z; init => m_z = value; }
//    /// <summary>Specifies the rotation component of the Quaternion.</summary>
//    public double W { get => m_w; init => m_w = value; }

//    /// <summary>Returns whether the Quaternion is the identity Quaternion.</summary>
//    public bool IsIdentity => Equals(Identity);

//    /// <summary>Creates the conjugate of the quaternion.</summary>
//    public Quaternion Conjugate() => new(-m_x, -m_y, -m_z, m_w);

//    /// <summary>Returns the inverse of the Quaternion.</summary>
//    //  -1   (       a              -v       )
//    // q   = ( -------------   ------------- )
//    //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )
//    public Quaternion Inverse()
//    {
//      var inverseNormal = 1 / LengthSquared();

//      return new(
//        -m_x * inverseNormal,
//        -m_y * inverseNormal,
//        -m_z * inverseNormal,
//        m_w * inverseNormal
//      );
//    }

//    /// <summary>Returns the length squared of the Quaternion.</summary>
//    public double LengthSquared() => m_x * m_x + m_y * m_y + m_z * m_z + m_w * m_w;

//    #region Static methods

//    /// <summary>Creates a Quaternion from the given rotation matrix.</summary>
//    public static Quaternion CreateFromRotationMatrix(IMatrix4 matrix)
//    {
//      if (matrix.M11 + matrix.M22 + matrix.M33 is var trace && trace > 0)
//      {
//        var s = double.Sqrt(trace + 1);
//        var invS = Half / s;

//        return new(
//          (matrix.M23 - matrix.M32) * invS,
//          (matrix.M31 - matrix.M13) * invS,
//          (matrix.M12 - matrix.M21) * invS,
//          Half * s
//        );
//      }
//      else
//      {
//        if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
//        {
//          var s = double.Sqrt(1 + matrix.M11 - matrix.M22 - matrix.M33);
//          var invS = Half / s;

//          return new(
//            Half * s,
//            (matrix.M12 + matrix.M21) * invS,
//            (matrix.M13 + matrix.M31) * invS,
//            (matrix.M23 - matrix.M32) * invS
//          );
//        }
//        else if (matrix.M22 > matrix.M33)
//        {
//          var s = double.Sqrt(1 + matrix.M22 - matrix.M11 - matrix.M33);
//          var invS = Half / s;

//          return new(
//            (matrix.M21 + matrix.M12) * invS,
//            Half * s,
//            (matrix.M32 + matrix.M23) * invS,
//            (matrix.M31 - matrix.M13) * invS
//          );
//        }
//        else
//        {
//          var s = double.Sqrt(1 + matrix.M33 - matrix.M11 - matrix.M22);
//          var invS = Half / s;

//          return new(
//            (matrix.M31 + matrix.M13) * invS,
//            (matrix.M32 + matrix.M23) * invS,
//            Half * s,
//            (matrix.M12 - matrix.M21) * invS
//          );
//        }
//      }
//    }

//    /// <summary>Returns a quaternion from two vectors.
//    /// <para><see href="http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final"/></para>
//    /// <para><see href="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/></para>
//    /// </summary>
//    public static Quaternion CreateFromTwoVectors(ICartesianCoordinate3<double> u, ICartesianCoordinate3<double> v)
//    {
//      var norm_uv = double.Sqrt(ICartesianCoordinate3<double>.DotProduct(u, u) * ICartesianCoordinate3<double>.DotProduct(v, v));

//      var real_part = norm_uv + ICartesianCoordinate3<double>.DotProduct(u, v);

//      var w = (real_part < GenericMath.Epsilon1E7 * norm_uv)
//        ? new Quaternion(
//            // If u and v are exactly opposite, rotate 180 degrees around an arbitrary orthogonal axis. Axis normalisation can happen later, when we normalise the quaternion.
//            System.Math.Abs(u.X) > System.Math.Abs(u.Z) ? new CartesianCoordinate3<double>(-u.Y, u.X, 0) : new CartesianCoordinate3<double>(0, -u.Z, u.Y),
//            0 // Eliminate the negligable "real_part" by simply using zero.
//          )
//        : new Quaternion(
//            ICartesianCoordinate3<double>.CrossProduct(u, v),
//            real_part
//          );

//      return w.Normalized();
//    }

//    /// <summary>Creates a new Quaternion from the given yaw, pitch, and roll, in radians.</summary>
//    /// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
//    /// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
//    /// <param name="roll">The roll angle, in radians, around the Z-axis.</param>
//    public static Quaternion CreateFromYawPitchRoll(double yaw, double pitch, double roll)
//    {
//      //  Roll first, about axis the object is facing, then pitch upward, then yaw to face into the new heading.

//      var halfRoll = roll * Half;
//      var sr = double.Sin(halfRoll);
//      var cr = double.Cos(halfRoll);

//      var halfPitch = pitch * Half;
//      var sp = double.Sin(halfPitch);
//      var cp = double.Cos(halfPitch);

//      var halfYaw = yaw * Half;
//      var sy = double.Sin(halfYaw);
//      var cy = double.Cos(halfYaw);

//      return new(
//        cy * sp * cr + sy * cp * sr,
//        sy * cp * cr - cy * sp * sr,
//        cy * cp * sr - sy * sp * cr,
//        cy * cp * cr + sy * sp * sr
//      );
//    }

//    /// <summary>
//    /// Concatenates two Quaternions; the result represents the value1 rotation followed by the value2 rotation.
//    /// </summary>
//    /// <param name="q1">The first Quaternion rotation in the series.</param>
//    /// <param name="q2">The second Quaternion rotation in the series.</param>
//    /// <returns>A new Quaternion representing the concatenation of the value1 rotation followed by the value2 rotation.</returns>
//    public static Quaternion Concatenate(IQuaternion q1, IQuaternion q2)
//    {
//      // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
//      // So that's why q2 goes q1 and q1 goes q2.

//      var q1x = q2.X;
//      var q1y = q2.Y;
//      var q1z = q2.Z;
//      var q1w = q2.W;

//      var q2x = q1.X;
//      var q2y = q1.Y;
//      var q2z = q1.Z;
//      var q2w = q1.W;

//      // cross(av, bv)
//      var cx = q1y * q2z - q1z * q2y;
//      var cy = q1z * q2x - q1x * q2z;
//      var cz = q1x * q2y - q1y * q2x;

//      var dot = q1x * q2x + q1y * q2y + q1z * q2z;

//      return new(
//        q1x * q2w + q2x * q1w + cx,
//        q1y * q2w + q2y * q1w + cy,
//        q1z * q2w + q2z * q1w + cz,
//        q1w * q2w - dot
//      );
//    }

//    /// <summary>Calculates the dot product of two Quaternions.</summary>
//    public static double Dot(IQuaternion q1, IQuaternion q2) => q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;

//    /// <summary>Linearly interpolates between two quaternions.</summary>
//    public static Quaternion Lerp(Quaternion q1, Quaternion q2, double mu)
//    {
//      var t = mu;
//      var t1 = 1 - t;

//      var dot = Dot(q1, q2);

//      var r = (dot >= 0)
//      ? new Quaternion(
//        t1 * q1.X + t * q2.X,
//        t1 * q1.Y + t * q2.Y,
//        t1 * q1.Z + t * q2.Z,
//        t1 * q1.W + t * q2.W
//      )
//      : new Quaternion(
//        t1 * q1.X - t * q2.X,
//        t1 * q1.Y - t * q2.Y,
//        t1 * q1.Z - t * q2.Z,
//        t1 * q1.W - t * q2.W
//      );

//      var inverseNormal = 1 / double.Sqrt(r.LengthSquared());

//      return new(
//        r.X * inverseNormal,
//        r.Y * inverseNormal,
//        r.Z * inverseNormal,
//        r.W * inverseNormal
//      );
//    }

//    /// <summary>
//    /// Interpolates between two quaternions, using spherical linear interpolation.
//    /// </summary>
//    /// <param name="q1">The first source Quaternion.</param>
//    /// <param name="q2">The second source Quaternion.</param>
//    /// <param name="mu">The relative weight of the second source Quaternion in the interpolation.</param>
//    /// <returns>The interpolated Quaternion.</returns>
//    public static Quaternion Slerp(Quaternion q1, Quaternion q2, double mu)
//    {
//      var epsilon = double.CreateChecked(1E-6);

//      var t = mu;
//      var t1 = 1 - t;

//      var cosOmega = Dot(q1, q2);

//      bool flip = false;

//      if (cosOmega < 0)
//      {
//        flip = true;
//        cosOmega = -cosOmega;
//      }

//      double s1, s2;

//      if (cosOmega > (1 - epsilon))
//      {
//        // Too close, do straight linear interpolation.
//        s1 = t1;
//        s2 = flip ? -t : t;
//      }
//      else
//      {
//        var omega = double.Acos(cosOmega);
//        var invSinOmega = 1 / double.Sin(omega);

//        s1 = double.Sin(t1 * omega) * invSinOmega;
//        s2 = flip ? -double.Sin(t * omega) * invSinOmega : double.Sin(t * omega) * invSinOmega;
//      }

//      return new(
//        s1 * q1.X + s2 * q2.X,
//        s1 * q1.Y + s2 * q2.Y,
//        s1 * q1.Z + s2 * q2.Z,
//        s1 * q1.W + s2 * q2.W
//      );
//    }

//    #endregion Static methods

//    #region Operator overloads

//    public static implicit operator Quaternion(double value) => new(0, 0, 0, value);

//    public static explicit operator Quaternion(System.ValueTuple<double, double, double, double> xyzw) => new(xyzw.Item1, xyzw.Item2, xyzw.Item3, xyzw.Item4);

//    /// <summary>Flips the sign of each component of the quaternion.</summary>
//    public static Quaternion operator -(Quaternion q) => new(-q.m_x, -q.m_y, -q.m_z, -q.m_w);

//    /// <summary>Adds two Quaternions element-by-element.</summary>
//    public static Quaternion operator +(Quaternion q1, Quaternion q2) => new(q1.m_x + q2.m_x, q1.m_y + q2.m_y, q1.m_z + q2.m_z, q1.m_w + q2.m_w);

//    /// <summary>Divides one Quaternion by another.</summary>
//    public static Quaternion operator /(Quaternion q1, Quaternion q2)
//    {
//      var q1x = q1.X;
//      var q1y = q1.Y;
//      var q1z = q1.Z;
//      var q1w = q1.W;

//      var inverseNormal = 1 / q2.LengthSquared();

//      var q2x = -q2.X * inverseNormal;
//      var q2y = -q2.Y * inverseNormal;
//      var q2z = -q2.Z * inverseNormal;
//      var q2w = q2.W * inverseNormal;

//      // cross(av, bv)
//      var cx = q1y * q2z - q1z * q2y;
//      var cy = q1z * q2x - q1x * q2z;
//      var cz = q1x * q2y - q1y * q2x;

//      var dot = q1x * q2x + q1y * q2y + q1z * q2z;

//      return new(
//        q1x * q2w + q2x * q1w + cx,
//        q1y * q2w + q2y * q1w + cy,
//        q1z * q2w + q2z * q1w + cz,
//        q1w * q2w - dot
//      );
//    }

//    /// <summary>Subtracts one Quaternion from another.</summary>
//    public static Quaternion operator -(Quaternion q1, Quaternion q2) => new(q1.m_x - q2.m_x, q1.m_y - q2.m_y, q1.m_z - q2.m_z, q1.m_w - q2.m_w);

//    /// <summary>Multiplies two Quaternions together.</summary>
//    public static Quaternion operator *(Quaternion q1, Quaternion q2)
//      => new(
//        q1.m_x * q2.m_w + q2.m_x * q1.m_w + (q1.m_y * q2.m_z - q1.m_z * q2.m_y),
//        q1.m_y * q2.m_w + q2.m_y * q1.m_w + (q1.m_z * q2.m_x - q1.m_x * q2.m_z),
//        q1.m_z * q2.m_w + q2.m_z * q1.m_w + (q1.m_x * q2.m_y - q1.m_y * q2.m_x),
//        q1.m_w * q2.m_w - (q1.m_x * q2.m_x + q1.m_y * q2.m_y + q1.m_z * q2.m_z)
//      );

//    /// <summary>Multiplies a Quaternion with a scalar value.</summary>
//    public static Quaternion operator *(Quaternion q, double scalar) => new(q.m_x * scalar, q.m_y * scalar, q.m_z * scalar, q.m_w * scalar);

//    #endregion Operator overloads

//    public override string ToString() => $"{{ X: {m_x}, Y: {m_y}, Z: {m_z}, W: {m_w} }}";
//  }
//}
//#endif
