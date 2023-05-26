//#if NET7_0_OR_GREATER
//namespace Flux
//{
//  #region ExtensionMethods
//  public static partial class GeometryExtensionMethods
//  {
//    /// <summary>Calculates the dot product of two Quaternions.</summary>
//    public static double DotProduct(this Geometry.IQuaternion q1, Geometry.IQuaternion q2)
//      => q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;

//    /// <summary>Returns the inverse of a Quaternion.</summary>
//    //  -1   (       a              -v       )
//    // q   = ( -------------   ------------- )
//    //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )
//    public static Geometry.Quaternion Inverse(this Geometry.IQuaternion source)
//      => source.Conjugate().Multiply(1 / source.LengthSquared());

//    /// <summary>Creates the conjugate of a specified Quaternion.</summary>
//    public static Geometry.Quaternion Conjugate(this Geometry.IQuaternion source)
//      => new(-source.X, -source.Y, -source.Z, source.W);

//    /// <summary>Calculates the length of the Quaternion.</summary>
//    public static double Length(this Geometry.IQuaternion source)
//      => System.Math.Sqrt(source.LengthSquared());

//    /// <summary>Calculates the length squared of the Quaternion. This operation is cheaper than Length().</summary>
//    public static double LengthSquared(this Geometry.IQuaternion source)
//      => source.X * source.X + source.Y * source.Y + source.Z * source.Z + source.W * source.W;

//    /// <summary>Linearly interpolates between two quaternions.</summary>
//    /// <param name="mu">The relative weight of the second source Quaternion in the interpolation.</param>
//    public static Geometry.Quaternion Lerp<TSelf>(this Geometry.IQuaternion q1, Geometry.IQuaternion q2, double mu)
//    {
//      var um = 1 - mu;

//      if (q1.DotProduct(q2) >= 0)
//        return new Geometry.Quaternion(
//          um * q1.X + mu * q2.X,
//          um * q1.Y + mu * q2.Y,
//          um * q1.Z + mu * q2.Z,
//          um * q1.W + mu * q2.W
//        ).Normalized();
//      else
//        return new Geometry.Quaternion(
//          um * q1.X - mu * q2.X,
//          um * q1.Y - mu * q2.Y,
//          um * q1.Z - mu * q2.Z,
//          um * q1.W - mu * q2.W
//        ).Normalized();
//    }

//    /// <summary>Multiplies a set of Quaternion components by a scalar value.</summary>
//    private static Geometry.Quaternion Multiply(this Geometry.IQuaternion source, double scalar)
//      => new(source.X * scalar, source.Y * scalar, source.Z * scalar, source.W * scalar);

//    /// <summary>Flips the sign of each component of the quaternion.</summary>
//    public static Geometry.Quaternion Negate(this Geometry.IQuaternion source)
//      => new(-source.X, -source.Y, -source.Z, -source.W);

//    /// <summary>Divides each component of the Quaternion by the length of the Quaternion.</summary>
//    public static Geometry.Quaternion Normalized(this Geometry.IQuaternion source)
//      => source.Multiply(1 / source.LengthSquared());

//    /// <summary>Interpolates between two quaternions, using spherical linear interpolation.</summary>
//    /// <param name="mu">The relative weight of the second source Quaternion in the interpolation.</param>
//    public static Geometry.Quaternion Slerp(this Geometry.IQuaternion q1, Geometry.IQuaternion q2, double mu)
//    {
//      var dot = q1.DotProduct(q2);

//      var flip = false;

//      if (dot < 0)
//      {
//        flip = true;
//        dot = -dot;
//      }

//      double s1, s2;

//      if (dot > (1d - 1E-6))
//      {
//        // Too close, do straight linear interpolation.

//        s1 = 1 - mu;
//        s2 = flip ? -mu : mu;
//      }
//      else
//      {
//        var angle = System.Math.Acos(dot);
//        var invSinAngle = 1 / System.Math.Sin(angle);

//        s1 = System.Math.Sin((1 - mu) * angle) * invSinAngle;
//        s2 = flip ? -System.Math.Sin(mu * angle) * invSinAngle : System.Math.Sin(mu * angle) * invSinAngle;
//      }

//      return new(s1 * q1.X + s2 * q2.X, s1 * q1.Y + s2 * q2.Y, s1 * q1.Z + s2 * q2.Z, s1 * q1.W + s2 * q2.W);
//    }

//    /// <summary></summary>
//    /// <remarks>The quaternion must be normalized.</remarks>
//    public static Geometry.AxisAngle ToAxisAngle(this Geometry.IQuaternion source)
//    {
//      var n = source.Normalized(); // If w>1 acos and sqrt will produce errors, this will not happen if quaternion is normalized.

//      var w = n.W;

//      var angle = System.Math.Acos(w).Multiply(2);

//      var s = System.Math.Sqrt(1 - w * w); // Assuming quaternion normalized then w is less than 1, so term always positive.

//      if (s < 0.001) // Test to avoid divide by zero. If s close to zero then direction of axis not important.
//        return new(1, 0, 0, angle); // If it is important that axis is normalised then replace with x=1; y=z=0;

//      return new(n.X / s, n.Y / s, n.Z / s, angle);
//    }

//    public static Geometry.EulerAngles ToEulerAngles(this Geometry.IQuaternion source) // yaw (Z), pitch (Y), roll (X)
//    {
//      var x = source.X;
//      var y = source.Y;
//      var z = source.Z;
//      var w = source.W;

//      var sqx = x * x;
//      var sqy = y * y;
//      var sqz = z * z;
//      var sqw = w * w;

//      var unit = sqx + sqy + sqz + sqw; // If unit = 1 then normalized, otherwise unit is correction factor.
//      var test = x * y + z * w;

//      if (test > 0.499 * unit) // Singularity at north pole when pitch approaches +90.
//        return new(double.CreateChecked(System.Math.Atan2(x, w).Multiply(2)), System.Math.PI / 2, 0);

//      if (test < -0.499 * unit) // Singularity at south pole when pitch approaches -90.
//        return new(double.CreateChecked(System.Math.Atan2(x, w).Multiply(-2)), -System.Math.PI / 2, 0);

//      var h = System.Math.Atan2(y.Multiply(2) * w - x.Multiply(2) * z, sqx - sqy - sqz + sqw);
//      var a = System.Math.Asin(test.Multiply(2) / unit);
//      var b = System.Math.Atan2(x.Multiply(2) * w - y.Multiply(2) * z, -sqx + sqy - sqz + sqw);

//      return new(double.CreateChecked(h), double.CreateChecked(a), double.CreateChecked(b));
//    }

//    public static Geometry.Matrix4 ToMatrix4<TSelf>(this Geometry.IQuaternion source)
//    {
//      var xx = source.X * source.X;
//      var yy = source.Y * source.Y;
//      var zz = source.Z * source.Z;

//      var xy = source.X * source.Y;
//      var wz = source.Z * source.W;
//      var xz = source.Z * source.X;
//      var wy = source.Y * source.W;
//      var yz = source.Y * source.Z;
//      var wx = source.X * source.W;

//      return new(
//        1 - (yy + zz) * 2, (xy + wz) * 2, (xz - wy) * 2, 0,
//        (xy - wz) * 2, 1 - (zz + xx) * 2, (yz + wx) * 2, 0,
//        (xz + wy) * 2, (yz - wx) * 2, 1 - (yy + xx) * 2, 0,
//        0, 0, 0, 1
//      );
//    }

//    public static Geometry.Quaternion ToQuaternion(this Geometry.IQuaternion source)
//      => new(
//        source.X,
//        source.Y,
//        source.Z,
//        source.W
//      );

//    public static System.Numerics.Quaternion ToQuaternion(this Geometry.Quaternion source)
//      => new(
//        (float)source.X,
//        (float)source.Y,
//        (float)source.Z,
//        (float)source.W
//      );
//  }
//  #endregion ExtensionMethods

//  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
//  namespace Geometry
//  {
//    public interface IQuaternion
//      : System.IFormattable
//    {
//      double X { get; init; }
//      double Y { get; init; }
//      double Z { get; init; }
//      double W { get; init; }

//      /// <summary>Calculates the dot product of two Quaternions.</summary>
//      public static double Dot(IQuaternion q1, IQuaternion q2)
//        => q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;

//      string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
//        => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N1"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N1"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N1"}}}", Z)}, W = {string.Format($"{{0:{format ?? "N1"}}}", W)} }}";
//    }
//  }
//}
//#endif
