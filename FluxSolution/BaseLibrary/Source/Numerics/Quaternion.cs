namespace Flux.Numerics
{
  /// <summary>A structure encapsulating a four-dimensional vector (x,y,z,w), which is used to efficiently rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).</summary>
  /// <see cref="https://github.com/mono/mono/blob/c5b88ec4f323f2bdb7c7d0a595ece28dae66579c/mcs/class/referencesource/System.Numerics/System/Numerics/Quaternion.cs"/>
  public struct Quaternion
    : System.IEquatable<Quaternion>
  {
    /// <summary>Specifies the X-value of the vector component of the Quaternion.</summary>
    public double X { get; set; }
    /// <summary>Specifies the Y-value of the vector component of the Quaternion.</summary>
    public double Y { get; set; }
    /// <summary>Specifies the Z-value of the vector component of the Quaternion.</summary>
    public double Z { get; set; }
    /// <summary>Specifies the rotation component of the Quaternion.</summary>
    public double W { get; set; }

    /// <summary>Returns a Quaternion representing no rotation.</summary>
    public static Quaternion Identity
      => new Quaternion(0, 0, 0, 1);

    /// <summary>Returns whether the Quaternion is the identity Quaternion.</summary>
    public bool IsIdentity
      => X == 0 && Y == 0 && Z == 0 && W == 1;

    /// <summary>Constructs a Quaternion from the given components.</summary>
    public Quaternion(double x, double y, double z, double w)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
      this.W = w;
    }

    /// <summary>Calculates the length of the Quaternion.</summary>
    public double Length()
      => System.Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
    /// <summary>Calculates the length squared of the Quaternion. This operation is cheaper than Length().</summary>
    public double LengthSquared()
      => X * X + Y * Y + Z * Z + W * W;

    #region Static methods
    /// <summary>Concatenates two Quaternions; the result represents the value1 rotation followed by the value2 rotation.</summary>
    /// <remarks>Concatenate rotation is actually q2 * q1 instead of q1 * q2.</remarks>
    public static Quaternion Concatenate(Quaternion q1, Quaternion q2)
      => Multiply(q2, q1);
    /// <summary>Creates the conjugate of a specified Quaternion.</summary>
    public static Quaternion Conjugate(Quaternion q)
      => new Quaternion(-q.X, -q.Y, -q.Z, q.W);
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
    public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
    {
      var q = new Quaternion();

      var trace = matrix.M11 + matrix.M22 + matrix.M33;

      if (trace > 0.0)
      {
        var s = System.Math.Sqrt(trace + 1);
        q.W = s * 0.5;
        s = 0.5 / s;
        q.X = (matrix.M23 - matrix.M32) * s;
        q.Y = (matrix.M31 - matrix.M13) * s;
        q.Z = (matrix.M12 - matrix.M21) * s;
      }
      else
      {
        if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
        {
          var s = System.Math.Sqrt(1 + matrix.M11 - matrix.M22 - matrix.M33);
          var invS = 0.5 / s;
          q.X = 0.5 * s;
          q.Y = (matrix.M12 + matrix.M21) * invS;
          q.Z = (matrix.M13 + matrix.M31) * invS;
          q.W = (matrix.M23 - matrix.M32) * invS;
        }
        else if (matrix.M22 > matrix.M33)
        {
          var s = System.Math.Sqrt(1 + matrix.M22 - matrix.M11 - matrix.M33);
          var invS = 0.5 / s;
          q.X = (matrix.M21 + matrix.M12) * invS;
          q.Y = 0.5 * s;
          q.Z = (matrix.M32 + matrix.M23) * invS;
          q.W = (matrix.M31 - matrix.M13) * invS;
        }
        else
        {
          var s = System.Math.Sqrt(1 + matrix.M33 - matrix.M11 - matrix.M22);
          var invS = 0.5 / s;
          q.X = (matrix.M31 + matrix.M13) * invS;
          q.Y = (matrix.M32 + matrix.M23) * invS;
          q.Z = 0.5 * s;
          q.W = (matrix.M12 - matrix.M21) * invS;
        }
      }

      return q;
    }
    /// <summary>Divides a Quaternion by another Quaternion.</summary>
    public static Quaternion Divide(Quaternion q1, Quaternion q2)
      => Multiply(q1, Inverse(q2));
    //public static Quaternion Divide(Quaternion value1, Quaternion value2)
    //{
    //  var invNorm = 1 / value2.LengthSquared();

    //  return Multiply(value1.X, value1.Y, value1.Z, value1.W, -value2.X * invNorm, -value2.Y * invNorm, -value2.Z * invNorm, value2.W * invNorm);
    //}
    /// <summary>Calculates the dot product of two Quaternions.</summary>
    public static double Dot(Quaternion q1, Quaternion q2)
      => q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;
    /// <summary>Returns the inverse of a Quaternion.</summary>
    //  -1   (       a              -v       )
    // q   = ( -------------   ------------- )
    //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )
    public static Quaternion Inverse(Quaternion q)
      => Multiply(-q.X, -q.Y, -q.Z, q.W, 1 / q.LengthSquared());
    /// <summary>Linearly interpolates between two quaternions.</summary>
    /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
    public static Quaternion Lerp(Quaternion q1, Quaternion q2, double amount)
    {
      double t = amount;
      double t1 = 1 - t;

      Quaternion r = new Quaternion();

      double dot = q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;

      if (dot >= 0)
      {
        r.X = t1 * q1.X + t * q2.X;
        r.Y = t1 * q1.Y + t * q2.Y;
        r.Z = t1 * q1.Z + t * q2.Z;
        r.W = t1 * q1.W + t * q2.W;
      }
      else
      {
        r.X = t1 * q1.X - t * q2.X;
        r.Y = t1 * q1.Y - t * q2.Y;
        r.Z = t1 * q1.Z - t * q2.Z;
        r.W = t1 * q1.W - t * q2.W;
      }

      return Normalize(r);
    }
    /// <summary>Multiplies two sets of Quaternion components as if they were two Quaternions.</summary>
    public static Quaternion Multiply(double q1x, double q1y, double q1z, double q1w, double q2x, double q2y, double q2z, double q2w) => new Quaternion(
      q1x * q2w + q2x * q1w + (q1y * q2z - q1z * q2y),
      q1y * q2w + q2y * q1w + (q1z * q2x - q1x * q2z),
      q1z * q2w + q2z * q1w + (q1x * q2y - q1y * q2x),
      q1w * q2w - (q1x * q2x + q1y * q2y + q1z * q2z)
    );
    /// <summary>Multiplies two Quaternions together.</summary>
    public static Quaternion Multiply(Quaternion q1, Quaternion q2)
      => Multiply(q1.X, q1.Y, q1.Z, q1.W, q2.X, q2.Y, q2.Z, q2.W);
    /// <summary>Multiplies a set of Quaternion components by a scalar value.</summary>
    public static Quaternion Multiply(double qx, double qy, double qz, double qw, double scalar)
      => new Quaternion(qx * scalar, qy * scalar, qz * scalar, qw * scalar);
    /// <summary>Multiplies a Quaternion by a scalar value.</summary>
    public static Quaternion Multiply(Quaternion q, double scalar)
      => Multiply(q.X, q.Y, q.Z, q.W, scalar);
    /// <summary>Flips the sign of each component of the quaternion.</summary>
    public static Quaternion Negate(Quaternion q)
      => new Quaternion(-q.X, -q.Y, -q.Z, -q.W);
    /// <summary>Divides each component of the Quaternion by the length of the Quaternion.</summary>
    public static Quaternion Normalize(Quaternion q)
      => Multiply(q.X, q.Y, q.Z, q.W, 1 / q.LengthSquared());
    /// <summary>Adds two Quaternions element-by-element.</summary>
    public static Quaternion Add(Quaternion q1, Quaternion q2)
      => new Quaternion(q1.X + q2.X, q1.Y + q2.Y, q1.Z + q2.Z, q1.W + q2.W);
    /// <summary>Interpolates between two quaternions, using spherical linear interpolation.</summary>
    /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
    public static Quaternion Slerp(Quaternion q1, Quaternion q2, double amount)
    {
      const double epsilon = 1e-6f;

      var t = amount;

      var cosOmega = q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;

      var flip = false;

      if (cosOmega < 0)
      {
        flip = true;
        cosOmega = -cosOmega;
      }

      double s1, s2;

      if (cosOmega > (1 - epsilon))
      {
        // Too close, do straight linear interpolation.
        s1 = 1 - t;
        s2 = flip ? -t : t;
      }
      else
      {
        var omega = System.Math.Acos(cosOmega);
        var invSinOmega = 1 / System.Math.Sin(omega);

        s1 = System.Math.Sin((1 - t) * omega) * invSinOmega;
        s2 = flip ? -System.Math.Sin(t * omega) * invSinOmega : System.Math.Sin(t * omega) * invSinOmega;
      }

      return new Quaternion(s1 * q1.X + s2 * q2.X, s1 * q1.Y + s2 * q2.Y, s1 * q1.Z + s2 * q2.Z, s1 * q1.W + s2 * q2.W);
    }
    /// <summary>Subtracts one Quaternion from another.</summary>
    public static Quaternion Subtract(Quaternion q1, Quaternion q2)
      => new Quaternion(q1.X - q2.X, q1.Y - q2.Y, q1.Z - q2.Z, q1.W - q2.W);
    #endregion Static methods

    #region Operator overloads
    /// <summary>Returns a boolean indicating whether the two given Quaternions are equal.</summary>
    public static bool operator ==(Quaternion q1, Quaternion q2)
      => q1.X == q2.X && q1.Y == q2.Y && q1.Z == q2.Z && q1.W == q2.W;
    /// <summary>Returns a boolean indicating whether the two given Quaternions are not equal.</summary>
    public static bool operator !=(Quaternion q1, Quaternion q2)
      => q1.X != q2.X || q1.Y != q2.Y || q1.Z != q2.Z || q1.W != q2.W;

    /// <summary>Flips the sign of each component of the quaternion.</summary>
    public static Quaternion operator -(Quaternion q)
      => new Quaternion(-q.X, -q.Y, -q.Z, -q.W);
    /// <summary>Adds two Quaternions element-by-element.</summary>
    public static Quaternion operator +(Quaternion q1, Quaternion q2)
      => new Quaternion(q1.X + q2.X, q1.Y + q2.Y, q1.Z + q2.Z, q1.W + q2.W);
    /// <summary>Subtracts one Quaternion from another.</summary>
    public static Quaternion operator -(Quaternion q1, Quaternion q2)
      => new Quaternion(q1.X - q2.X, q1.Y - q2.Y, q1.Z - q2.Z, q1.W - q2.W);
    /// <summary>Multiplies two Quaternions together.</summary>
    public static Quaternion operator *(Quaternion q1, Quaternion q2)
      => Multiply(q1, q2);
    /// <summary>Multiplies a Quaternion by a scalar value.</summary>
    public static Quaternion operator *(Quaternion q1, double q2)
      => new Quaternion(q1.X * q2, q1.Y * q2, q1.Z * q2, q1.W * q2);
    /// <summary>Divides a Quaternion by another Quaternion.</summary>
    public static Quaternion operator /(Quaternion q1, Quaternion q2)
      => Divide(q1, q2);
    #endregion Operator overloads

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Quaternion q)
      => X == q.X && Y == q.Y && Z == q.Z && W == q.W;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Quaternion o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y, Z, W);
    public override string ToString()
      => $"<{GetType().Name}: {X}, {Y}, {Z}, {W}>";
    #endregion Object overrides
  }
}
