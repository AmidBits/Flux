namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Quaternion ToQuaternion(this System.Numerics.Quaternion source)
      => new(source.X, source.Y, source.Z, source.W);
  }

  /// <summary>A structure encapsulating a four-dimensional vector (x,y,z,w), which is used to efficiently rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).</summary>
  /// <see cref="https://github.com/mono/mono/blob/c5b88ec4f323f2bdb7c7d0a595ece28dae66579c/mcs/class/referencesource/System.Numerics/System/Numerics/Quaternion.cs"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct Quaternion
  {
    /// <summary>Returns a Quaternion representing no rotation.</summary>
    public static Quaternion Identity
      => new(0, 0, 0, 1);

    private readonly double m_x;
    private readonly double m_y;
    private readonly double m_z;
    private readonly double m_w;

    /// <summary>Constructs a Quaternion from the given components.</summary>
    public Quaternion(double x, double y, double z, double w)
    {
      m_x = x;
      m_y = y;
      m_z = z;
      m_w = w;
    }

    /// <summary>Specifies the X-value of the vector component of the Quaternion.</summary>
    public double X { get => m_x; init => m_x = value; }
    /// <summary>Specifies the Y-value of the vector component of the Quaternion.</summary>
    public double Y { get => m_y; init => m_y = value; }
    /// <summary>Specifies the Z-value of the vector component of the Quaternion.</summary>
    public double Z { get => m_z; init => m_z = value; }
    /// <summary>Specifies the rotation component of the Quaternion.</summary>
    public double W { get => m_w; init => m_w = value; }

    /// <summary>Returns whether the Quaternion is the identity Quaternion.</summary>
    public bool IsIdentity
      => Equals(Identity);

    /// <summary>Returns the inverse of a Quaternion.</summary>
    //  -1   (       a              -v       )
    // q   = ( -------------   ------------- )
    //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )
    public Quaternion Inverse()
      => Multiply(-m_x, -m_y, -m_z, m_w, 1 / LengthSquared());
    /// <summary>Creates the conjugate of a specified Quaternion.</summary>
    public Quaternion Conjugate()
      => new(-m_x, -m_y, -m_z, m_w);
    /// <summary>Calculates the length of the Quaternion.</summary>
    public double Length()
      => System.Math.Sqrt(LengthSquared());
    /// <summary>Calculates the length squared of the Quaternion. This operation is cheaper than Length().</summary>
    public double LengthSquared()
      => m_x * m_x + m_y * m_y + m_z * m_z + m_w * m_w;
    /// <summary>Divides each component of the Quaternion by the length of the Quaternion.</summary>
    public Quaternion Normalized()
      => Multiply(m_x, m_y, m_z, m_w, 1 / LengthSquared());

    public AxisAngle ToAxisAngle()
    {
      var n = Normalized(); // If w>1 acos and sqrt will produce errors, this will not happen if quaternion is normalized.

      var angle = 2 * System.Math.Acos(n.m_w);

      var s = System.Math.Sqrt(1 - n.m_w * n.m_w); // Assuming quaternion normalized then w is less than 1, so term always positive.

      if (s < 0.001) // Test to avoid divide by zero. If s close to zero then direction of axis not important.
        return new(1, 0, 0, angle); // If it is important that axis is normalised then replace with x=1; y=z=0;

      return new(n.m_x / s, n.m_y / s, n.m_z / s, angle);
    }
    public EulerAngles ToEulerAngles() // yaw (Z), pitch (Y), roll (X)
    {
      var sqw = m_w * m_w;
      var sqx = m_x * m_x;
      var sqy = m_y * m_y;
      var sqz = m_z * m_z;

      var unit = sqx + sqy + sqz + sqw; // If unit = 1 then normalised, otherwise unit is correction factor.
      var test = m_x * m_y + m_z * m_w;

      if (test > 0.499 * unit) // Singularity at north pole when pitch approaches +90.
        return new(2 * System.Math.Atan2(m_x, m_w), System.Math.PI / 2, 0);

      if (test < -0.499 * unit) // Singularity at south pole when pitch approaches -90.
        return new(-2 * System.Math.Atan2(m_x, m_w), -System.Math.PI / 2, 0);

      var h = System.Math.Atan2(2 * m_y * m_w - 2 * m_x * m_z, sqx - sqy - sqz + sqw);
      var a = System.Math.Asin(2 * test / unit);
      var b = System.Math.Atan2(2 * m_x * m_w - 2 * m_y * m_z, -sqx + sqy - sqz + sqw);

      return new(h, a, b);
    }

    public System.Numerics.Quaternion ToQuaternion()
      => new System.Numerics.Quaternion((float)m_x, (float)m_y, (float)m_z, (float)m_w);

    #region Static methods
    public static Quaternion CreateNormalized(double x, double y, double z, double w)
     => Multiply(x, y, z, w, 1 / LengthSquared(x, y, z, w));

    /// <summary>Creates a new Quaternion from the given yaw, pitch, and roll, in radians.</summary>
    /// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
    /// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
    /// <param name="roll">The roll angle, in radians, around the Z-axis.</param>
    public static Quaternion CreateFromYawPitchRoll(double yaw, double pitch, double roll)
    {
      //  Roll first, about axis the object is facing, then pitch upward, then yaw to face into the new heading.

      var halfRoll = roll * 0.5;
      var sr = System.Math.Sin(halfRoll);
      var cr = System.Math.Cos(halfRoll);

      var halfPitch = pitch * 0.5;
      var sp = System.Math.Sin(halfPitch);
      var cp = System.Math.Cos(halfPitch);

      var halfYaw = yaw * 0.5;
      var sy = System.Math.Sin(halfYaw);
      var cy = System.Math.Cos(halfYaw);

      return new Quaternion(cy * sp * cr + sy * cp * sr, sy * cp * cr - cy * sp * sr, cy * cp * sr - sy * sp * cr, cy * cp * cr + sy * sp * sr);
    }

    /// <summary>Creates a Quaternion from the given rotation matrix.</summary>
    //public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
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
    /// <summary>Calculates the dot product of two Quaternions.</summary>
    public static double DotProduct(Quaternion q1, Quaternion q2)
      => q1.m_x * q2.m_x + q1.m_y * q2.m_y + q1.m_z * q2.m_z + q1.m_w * q2.m_w;
    // http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final
    /// <summary>Returns a quaternion from two vectors.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/>
    public static Quaternion FromTwoVectors(Vector3 u, Vector3 v)
    {
      var norm_u_norm_v = (float)System.Math.Sqrt(Vector3.DotProduct(u, u) * Vector3.DotProduct(v, v));
      var real_part = norm_u_norm_v + Vector3.DotProduct(u, v);

      Vector3 w;

      if (real_part < Maths.Epsilon1E7 * norm_u_norm_v)
      {
        real_part = 0.0f;

        // If u and v are exactly opposite, rotate 180 degrees around an arbitrary orthogonal axis. Axis normalisation can happen later, when we normalise the quaternion.
        w = System.Math.Abs(u.X) > System.Math.Abs(u.Z) ? new Vector3(-u.Y, u.X, 0f) : new Vector3(0f, -u.Z, u.Y);
      }
      else
      {
        w = Vector3.CrossProduct(u, v);
      }

      return CreateNormalized(w.X, w.Y, w.Z, real_part);
    }
    /// <summary>Calculates the length squared of the Quaternion. This operation is cheaper than Length().</summary>
    private static double LengthSquared(double x, double y, double z, double w)
      => x * x + y * y + z * z + w * w;
    /// <summary>Linearly interpolates between two quaternions.</summary>
    /// <param name="mu">The relative weight of the second source Quaternion in the interpolation.</param>
    public static Quaternion Lerp(Quaternion q1, Quaternion q2, double mu)
    {
      var um = 1 - mu;

      if (DotProduct(q1, q2) >= 0)
        return new Quaternion(
          um * q1.m_x + mu * q2.m_x,
          um * q1.m_y + mu * q2.m_y,
          um * q1.m_z + mu * q2.m_z,
          um * q1.m_w + mu * q2.m_w
        ).Normalized();
      else
        return new Quaternion(
          um * q1.m_x - mu * q2.m_x,
          um * q1.m_y - mu * q2.m_y,
          um * q1.m_z - mu * q2.m_z,
          um * q1.m_w - mu * q2.m_w
        ).Normalized();
    }
    /// <summary>Multiplies a set of Quaternion components by a scalar value.</summary>
    private static Quaternion Multiply(double qx, double qy, double qz, double qw, double scalar)
      => new(qx * scalar, qy * scalar, qz * scalar, qw * scalar);
    /// <summary>Flips the sign of each component of the quaternion.</summary>
    public static Quaternion Negate(Quaternion q)
      => new(-q.m_x, -q.m_y, -q.m_z, -q.m_w);
    /// <summary>Divides each component of the Quaternion by the length of the Quaternion.</summary>
    public static Quaternion Normalize(Quaternion q)
      => Multiply(q.m_x, q.m_y, q.m_z, q.m_w, 1 / q.LengthSquared());
    /// <summary>Interpolates between two quaternions, using spherical linear interpolation.</summary>
    /// <param name="mu">The relative weight of the second source Quaternion in the interpolation.</param>
    public static Quaternion Slerp(Quaternion q1, Quaternion q2, double mu)
    {
      var dot = q1.m_x * q2.m_x + q1.m_y * q2.m_y + q1.m_z * q2.m_z + q1.m_w * q2.m_w;

      var flip = false;

      if (dot < 0)
      {
        flip = true;
        dot = -dot;
      }

      double s1, s2;

      if (dot > (1 - 1E-6))
      {
        // Too close, do straight linear interpolation.

        s1 = 1 - mu;
        s2 = flip ? -mu : mu;
      }
      else
      {
        var angle = System.Math.Acos(dot);
        var invSinAngle = 1 / System.Math.Sin(angle);

        s1 = System.Math.Sin((1 - mu) * angle) * invSinAngle;
        s2 = flip ? -System.Math.Sin(mu * angle) * invSinAngle : System.Math.Sin(mu * angle) * invSinAngle;
      }

      return new(s1 * q1.m_x + s2 * q2.m_x, s1 * q1.m_y + s2 * q2.m_y, s1 * q1.m_z + s2 * q2.m_z, s1 * q1.m_w + s2 * q2.m_w);
    }
    #endregion Static methods

    #region Operator overloads
    public static implicit operator Quaternion(double value)
      => new(0, 0, 0, value);

    public static explicit operator Quaternion(System.ValueTuple<double, double, double, double> xyzw)
      => new(xyzw.Item1, xyzw.Item2, xyzw.Item3, xyzw.Item4);

    /// <summary>Flips the sign of each component of the quaternion.</summary>
    public static Quaternion operator -(Quaternion q)
      => new(-q.m_x, -q.m_y, -q.m_z, -q.m_w);
    /// <summary>Adds two Quaternions element-by-element.</summary>
    public static Quaternion operator +(Quaternion q1, Quaternion q2)
      => new(q1.m_x + q2.m_x, q1.m_y + q2.m_y, q1.m_z + q2.m_z, q1.m_w + q2.m_w);
    /// <summary>Subtracts one Quaternion from another.</summary>
    public static Quaternion operator -(Quaternion q1, Quaternion q2)
      => new(q1.m_x - q2.m_x, q1.m_y - q2.m_y, q1.m_z - q2.m_z, q1.m_w - q2.m_w);
    /// <summary>Multiplies two Quaternions together.</summary>
    public static Quaternion operator *(Quaternion q1, Quaternion q2)
      => new(
        q1.m_x * q2.m_w + q2.m_x * q1.m_w + (q1.m_y * q2.m_z - q1.m_z * q2.m_y),
        q1.m_y * q2.m_w + q2.m_y * q1.m_w + (q1.m_z * q2.m_x - q1.m_x * q2.m_z),
        q1.m_z * q2.m_w + q2.m_z * q1.m_w + (q1.m_x * q2.m_y - q1.m_y * q2.m_x),
        q1.m_w * q2.m_w - (q1.m_x * q2.m_x + q1.m_y * q2.m_y + q1.m_z * q2.m_z)
      );
    public static Quaternion operator *(Quaternion q, double scalar)
      => new(q.m_x * scalar, q.m_y * scalar, q.m_z * scalar, q.m_w * scalar);
    /// <summary>Divides a Quaternion by another Quaternion.</summary>
    public static Quaternion operator /(Quaternion q1, Quaternion q2)
      => q1 * q2.Inverse();
    #endregion Operator overloads

  }
}
