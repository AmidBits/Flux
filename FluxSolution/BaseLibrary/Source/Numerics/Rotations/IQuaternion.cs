namespace Flux
{
  #region ExtensionMethods
  public static partial class NumericsExtensionMethods
  {
    /// <summary>Calculates the dot product of two Quaternions.</summary>
    public static TSelf DotProduct<TSelf>(this Numerics.IQuaternion<TSelf> q1, Numerics.IQuaternion<TSelf> q2)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;

    /// <summary>Returns the inverse of a Quaternion.</summary>
    //  -1   (       a              -v       )
    // q   = ( -------------   ------------- )
    //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )
    public static Numerics.Quaternion<TSelf> Inverse<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => source.Conjugate().Multiply(TSelf.One / source.LengthSquared());

    /// <summary>Creates the conjugate of a specified Quaternion.</summary>
    public static Numerics.Quaternion<TSelf> Conjugate<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(-source.X, -source.Y, -source.Z, source.W);

    /// <summary>Calculates the length of the Quaternion.</summary>
    public static TSelf Length<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.Sqrt(source.LengthSquared());

    /// <summary>Calculates the length squared of the Quaternion. This operation is cheaper than Length().</summary>
    public static TSelf LengthSquared<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => source.X * source.X + source.Y * source.Y + source.Z * source.Z + source.W * source.W;

    /// <summary>Linearly interpolates between two quaternions.</summary>
    /// <param name="mu">The relative weight of the second source Quaternion in the interpolation.</param>
    public static Numerics.Quaternion<TSelf> Lerp<TSelf>(this Numerics.IQuaternion<TSelf> q1, Numerics.IQuaternion<TSelf> q2, TSelf mu)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var um = TSelf.One - mu;

      if (q1.DotProduct(q2) >= TSelf.Zero)
        return new Numerics.Quaternion<TSelf>(
          um * q1.X + mu * q2.X,
          um * q1.Y + mu * q2.Y,
          um * q1.Z + mu * q2.Z,
          um * q1.W + mu * q2.W
        ).Normalized();
      else
        return new Numerics.Quaternion<TSelf>(
          um * q1.X - mu * q2.X,
          um * q1.Y - mu * q2.Y,
          um * q1.Z - mu * q2.Z,
          um * q1.W - mu * q2.W
        ).Normalized();
    }

    /// <summary>Multiplies a set of Quaternion components by a scalar value.</summary>
    private static Numerics.Quaternion<TSelf> Multiply<TSelf>(this Numerics.IQuaternion<TSelf> source, TSelf scalar)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(source.X * scalar, source.Y * scalar, source.Z * scalar, source.W * scalar);

    /// <summary>Flips the sign of each component of the quaternion.</summary>
    public static Numerics.Quaternion<TSelf> Negate<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(-source.X, -source.Y, -source.Z, -source.W);

    /// <summary>Divides each component of the Quaternion by the length of the Quaternion.</summary>
    public static Numerics.Quaternion<TSelf> Normalized<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => source.Multiply(TSelf.One / source.LengthSquared());

    /// <summary>Interpolates between two quaternions, using spherical linear interpolation.</summary>
    /// <param name="mu">The relative weight of the second source Quaternion in the interpolation.</param>
    public static Numerics.Quaternion<TSelf> Slerp<TSelf>(this Numerics.IQuaternion<TSelf> q1, Numerics.IQuaternion<TSelf> q2, TSelf mu)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var dot = q1.DotProduct(q2);

      var flip = false;

      if (dot < TSelf.Zero)
      {
        flip = true;
        dot = -dot;
      }

      TSelf s1, s2;

      if (dot > (TSelf.CreateChecked(1 - 1E-6)))
      {
        // Too close, do straight linear interpolation.

        s1 = TSelf.One - mu;
        s2 = flip ? -mu : mu;
      }
      else
      {
        var angle = TSelf.Acos(dot);
        var invSinAngle = TSelf.One / TSelf.Sin(angle);

        s1 = TSelf.Sin((TSelf.One - mu) * angle) * invSinAngle;
        s2 = flip ? -TSelf.Sin(mu * angle) * invSinAngle : TSelf.Sin(mu * angle) * invSinAngle;
      }

      return new(s1 * q1.X + s2 * q2.X, s1 * q1.Y + s2 * q2.Y, s1 * q1.Z + s2 * q2.Z, s1 * q1.W + s2 * q2.W);
    }

    /// <summary></summary>
    /// <remarks>The quaternion must be normalized.</remarks>
    public static Numerics.AxisAngle<TSelf> ToAxisAngle<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var n = source.Normalized(); // If w>1 acos and sqrt will produce errors, this will not happen if quaternion is normalized.

      var w = n.W;

      var angle = TSelf.Acos(w).Multiply(2);

      var s = TSelf.Sqrt(TSelf.One - w * w); // Assuming quaternion normalized then w is less than 1, so term always positive.

      if (s < TSelf.CreateChecked(0.001)) // Test to avoid divide by zero. If s close to zero then direction of axis not important.
        return new(TSelf.One, TSelf.Zero, TSelf.Zero, angle); // If it is important that axis is normalised then replace with x=1; y=z=0;

      return new(n.X / s, n.Y / s, n.Z / s, angle);
    }

    public static Numerics.EulerAngles<TSelf> ToEulerAngles<TSelf>(this Numerics.IQuaternion<TSelf> source) // yaw (Z), pitch (Y), roll (X)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var x = source.X;
      var y = source.Y;
      var z = source.Z;
      var w = source.W;

      var sqx = x * x;
      var sqy = y * y;
      var sqz = z * z;
      var sqw = w * w;

      var unit = sqx + sqy + sqz + sqw; // If unit = 1 then normalized, otherwise unit is correction factor.
      var test = x * y + z * w;

      if (test > TSelf.CreateChecked(0.499) * unit) // Singularity at north pole when pitch approaches +90.
        return new(TSelf.Atan2(x, w).Multiply(2), TSelf.Pi.Divide(2), TSelf.Zero);

      if (test < -TSelf.CreateChecked(0.499) * unit) // Singularity at south pole when pitch approaches -90.
        return new(TSelf.Atan2(x, w).Multiply(-2), -TSelf.Pi.Divide(2), TSelf.Zero);

      var h = TSelf.Atan2(y.Multiply(2) * w - x.Multiply(2) * z, sqx - sqy - sqz + sqw);
      var a = TSelf.Asin(test.Multiply(2) / unit);
      var b = TSelf.Atan2(x.Multiply(2) * w - y.Multiply(2) * z, -sqx + sqy - sqz + sqw);

      return new(h, a, b);
    }

    public static Numerics.Matrix4<TSelf> ToMatrix4<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var xx = source.X * source.X;
      var yy = source.Y * source.Y;
      var zz = source.Z * source.Z;

      var xy = source.X * source.Y;
      var wz = source.Z * source.W;
      var xz = source.Z * source.X;
      var wy = source.Y * source.W;
      var yz = source.Y * source.Z;
      var wx = source.X * source.W;

      return new(
        TSelf.One - (yy + zz).Multiply(2), (xy + wz).Multiply(2), (xz - wy).Multiply(2), TSelf.Zero,
        (xy - wz).Multiply(2), TSelf.One - (zz + xx).Multiply(2), (yz + wx).Multiply(2), TSelf.Zero,
        (xz + wy).Multiply(2), (yz - wx).Multiply(2), TSelf.One - (yy + xx).Multiply(2), TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public static Numerics.Quaternion<TResult> ToQuaternion<TSelf, TResult>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.IFloatingPointIeee754<TResult>
      => new(
        TResult.CreateChecked(source.X),
        TResult.CreateChecked(source.Y),
        TResult.CreateChecked(source.Z),
        TResult.CreateChecked(source.W)
      );

    public static System.Numerics.Quaternion ToQuaternion<TSelf>(this Numerics.IQuaternion<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => new(
        float.CreateChecked(source.X),
        float.CreateChecked(source.Y),
        float.CreateChecked(source.Z),
        float.CreateChecked(source.W)
      );
  }
  #endregion ExtensionMethods

  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  namespace Numerics
  {
    public interface IQuaternion<TSelf>
      : System.IFormattable
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      TSelf X { get; init; }
      TSelf Y { get; init; }
      TSelf Z { get; init; }
      TSelf W { get; init; }

      /// <summary>Calculates the dot product of two Quaternions.</summary>
      public static TSelf Dot(IQuaternion<TSelf> q1, IQuaternion<TSelf> q2)
        => q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;

      string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
        => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N1"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N1"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N1"}}}", Z)}, W = {string.Format($"{{0:{format ?? "N1"}}}", W)} }}";
    }
  }
}
