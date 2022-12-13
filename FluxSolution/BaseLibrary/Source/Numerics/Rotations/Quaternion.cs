namespace Flux.Numerics
{
  /// <summary>A structure encapsulating a four-dimensional vector (x,y,z,w), which is used to efficiently rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).</summary>
  /// <remarks>All angles in radians.</remarks>
  /// <see cref="https://github.com/mono/mono/blob/c5b88ec4f323f2bdb7c7d0a595ece28dae66579c/mcs/class/referencesource/System.Numerics/System/Numerics/Quaternion.cs"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct Quaternion<TSelf>
    : IQuaternion<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    /// <summary>Returns a Quaternion representing no rotation.</summary>
    public static Quaternion<TSelf> Identity
      => new(TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One);

    private readonly TSelf m_x;
    private readonly TSelf m_y;
    private readonly TSelf m_z;
    private readonly TSelf m_w;

    /// <summary>Constructs a Quaternion from the given components.</summary>
    public Quaternion(TSelf x, TSelf y, TSelf z, TSelf w)
    {
      m_x = x;
      m_y = y;
      m_z = z;
      m_w = w;
    }

    /// <summary>Specifies the X-value of the vector component of the Quaternion.</summary>
    public TSelf X { get => m_x; init => m_x = value; }
    /// <summary>Specifies the Y-value of the vector component of the Quaternion.</summary>
    public TSelf Y { get => m_y; init => m_y = value; }
    /// <summary>Specifies the Z-value of the vector component of the Quaternion.</summary>
    public TSelf Z { get => m_z; init => m_z = value; }
    /// <summary>Specifies the rotation component of the Quaternion.</summary>
    public TSelf W { get => m_w; init => m_w = value; }

    /// <summary>Returns whether the Quaternion is the identity Quaternion.</summary>
    public bool IsIdentity
      => Equals(Identity);

    #region Static methods

    /// <summary>Creates a Quaternion from the given rotation matrix.</summary>
    //public static Quaternion CreateFromRotationMatrix(IMatrix4<TSelf> matrix)
    //{
    //  var q = new Quaternion();

    //  var trace = matrix.M11 + matrix.M22 + matrix.M33;

    //  if (trace > 0.0)
    //  {
    //    var s = System.Math.Sqrt(trace + 1);
    //    q.W = s * 0.5;
    //    s = 0.5 / s;
    //    q.X = (matrix.M23 - matrix.M32) * s;
    //    q.Y = (matrix.M31 - matrix.M13) * s;
    //    q.Z = (matrix.M12 - matrix.M21) * s;
    //  }
    //  else
    //  {
    //    if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
    //    {
    //      var s = System.Math.Sqrt(1 + matrix.M11 - matrix.M22 - matrix.M33);
    //      var invS = 0.5 / s;
    //      q.X = 0.5 * s;
    //      q.Y = (matrix.M12 + matrix.M21) * invS;
    //      q.Z = (matrix.M13 + matrix.M31) * invS;
    //      q.W = (matrix.M23 - matrix.M32) * invS;
    //    }
    //    else if (matrix.M22 > matrix.M33)
    //    {
    //      var s = System.Math.Sqrt(1 + matrix.M22 - matrix.M11 - matrix.M33);
    //      var invS = 0.5 / s;
    //      q.X = (matrix.M21 + matrix.M12) * invS;
    //      q.Y = 0.5 * s;
    //      q.Z = (matrix.M32 + matrix.M23) * invS;
    //      q.W = (matrix.M31 - matrix.M13) * invS;
    //    }
    //    else
    //    {
    //      var s = System.Math.Sqrt(1 + matrix.M33 - matrix.M11 - matrix.M22);
    //      var invS = 0.5 / s;
    //      q.X = (matrix.M31 + matrix.M13) * invS;
    //      q.Y = (matrix.M32 + matrix.M23) * invS;
    //      q.Z = 0.5 * s;
    //      q.W = (matrix.M12 - matrix.M21) * invS;
    //    }
    //  }

    //  return q;
    //}

    ///// <summary>Returns a quaternion from two vectors.
    ///// <para><see href="http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final"/></para>
    ///// <para><see href="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/></para>
    ///// </summary>
    //public static Quaternion<TSelf> CreateFromTwoVectors(Numerics.ICartesianCoordinate3<TSelf> u, Numerics.ICartesianCoordinate3<TSelf> v)
    //{
    //  var norm_u_norm_v = TSelf.Sqrt(Numerics.ICartesianCoordinate3<TSelf>.DotProduct(u, u) * Numerics.ICartesianCoordinate3<TSelf>.DotProduct(v, v));
    //  var real_part = norm_u_norm_v + Numerics.ICartesianCoordinate3<TSelf>.DotProduct(u, v);

    //  Numerics.CartesianCoordinate3<TSelf> w;

    //  if (real_part < TSelf.CreateChecked(GenericMath.Epsilon1E7) * norm_u_norm_v)
    //  {
    //    real_part = TSelf.Zero;

    //    // If u and v are exactly opposite, rotate 180 degrees around an arbitrary orthogonal axis. Axis normalisation can happen later, when we normalise the quaternion.
    //    w = TSelf.Abs(u.X) > TSelf.Abs(u.Z) ? new Numerics.CartesianCoordinate3<TSelf>(-u.Y, u.X, TSelf.Zero) : new Numerics.CartesianCoordinate3<TSelf>(TSelf.Zero, -u.Z, u.Y);
    //  }
    //  else
    //  {
    //    w = Numerics.ICartesianCoordinate3<TSelf>.CrossProduct(u, v);
    //  }

    //  return new Quaternion<TSelf>(w.X, w.Y, w.Z, real_part).Normalized();
    //}

    ///// <summary>Creates a new Quaternion from the given yaw, pitch, and roll, in radians.</summary>
    ///// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
    ///// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
    ///// <param name="roll">The roll angle, in radians, around the Z-axis.</param>
    //public static Quaternion<TSelf> CreateFromYawPitchRoll(TSelf yaw, TSelf pitch, TSelf roll)
    //{
    //  //  Roll first, about axis the object is facing, then pitch upward, then yaw to face into the new heading.

    //  var halfRoll = roll * TSelf.CreateChecked(0.5);
    //  var sr = TSelf.Sin(halfRoll);
    //  var cr = TSelf.Cos(halfRoll);

    //  var halfPitch = pitch * TSelf.CreateChecked(0.5);
    //  var sp = TSelf.Sin(halfPitch);
    //  var cp = TSelf.Cos(halfPitch);

    //  var halfYaw = yaw * TSelf.CreateChecked(0.5);
    //  var sy = TSelf.Sin(halfYaw);
    //  var cy = TSelf.Cos(halfYaw);

    //  return new(
    //    cy * sp * cr + sy * cp * sr,
    //    sy * cp * cr - cy * sp * sr,
    //    cy * cp * sr - sy * sp * cr,
    //    cy * cp * cr + sy * sp * sr
    //  );
    //}

    /// <summary>Calculates the dot product of two Quaternions.</summary>
    public static TSelf DotProduct(IQuaternion<TSelf> q1, IQuaternion<TSelf> q2)
      => q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;

    #endregion Static methods

    #region Operator overloads
    public static implicit operator Quaternion<TSelf>(TSelf value)
      => new(TSelf.Zero, TSelf.Zero, TSelf.Zero, value);

    public static explicit operator Quaternion<TSelf>(System.ValueTuple<TSelf, TSelf, TSelf, TSelf> xyzw)
      => new(xyzw.Item1, xyzw.Item2, xyzw.Item3, xyzw.Item4);

    /// <summary>Flips the sign of each component of the quaternion.</summary>
    public static Quaternion<TSelf> operator -(Quaternion<TSelf> q)
      => new(-q.m_x, -q.m_y, -q.m_z, -q.m_w);

    /// <summary>Adds two Quaternions element-by-element.</summary>
    public static Quaternion<TSelf> operator +(Quaternion<TSelf> q1, Quaternion<TSelf> q2)
      => new(q1.m_x + q2.m_x, q1.m_y + q2.m_y, q1.m_z + q2.m_z, q1.m_w + q2.m_w);

    /// <summary>Subtracts one Quaternion from another.</summary>
    public static Quaternion<TSelf> operator -(Quaternion<TSelf> q1, Quaternion<TSelf> q2)
      => new(q1.m_x - q2.m_x, q1.m_y - q2.m_y, q1.m_z - q2.m_z, q1.m_w - q2.m_w);

    /// <summary>Multiplies two Quaternions together.</summary>
    public static Quaternion<TSelf> operator *(Quaternion<TSelf> q1, Quaternion<TSelf> q2)
      => new(
        q1.m_x * q2.m_w + q2.m_x * q1.m_w + (q1.m_y * q2.m_z - q1.m_z * q2.m_y),
        q1.m_y * q2.m_w + q2.m_y * q1.m_w + (q1.m_z * q2.m_x - q1.m_x * q2.m_z),
        q1.m_z * q2.m_w + q2.m_z * q1.m_w + (q1.m_x * q2.m_y - q1.m_y * q2.m_x),
        q1.m_w * q2.m_w - (q1.m_x * q2.m_x + q1.m_y * q2.m_y + q1.m_z * q2.m_z)
      );
    public static Quaternion<TSelf> operator *(Quaternion<TSelf> q, TSelf scalar)
  => new(q.m_x * scalar, q.m_y * scalar, q.m_z * scalar, q.m_w * scalar);

    ///// <summary>Divides a Quaternion by another Quaternion.</summary>
    //public static IQuaternion<TSelf> operator /(Quaternion<TSelf> q1, Quaternion<TSelf> q2)
    //  => q1 * q2.Inverse();
    #endregion Operator overloads
  }
}
