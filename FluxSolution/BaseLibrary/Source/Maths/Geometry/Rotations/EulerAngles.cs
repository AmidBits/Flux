namespace Flux
{
  #region ExtensionMethods

  public static partial class Em
  {
    public static Geometry.AxisAngle ToAxisAngle(this Geometry.EulerAngles source)
    {
      var halfYaw = source.Yaw / 2;
      var halfPitch = source.Pitch / 2;
      var halfRoll = source.Roll / 2;

      var (s1, c1) = System.Math.SinCos(halfYaw);
      var (s2, c2) = System.Math.SinCos(halfPitch);
      var (s3, c3) = System.Math.SinCos(halfRoll);

      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;

      var w = c1c2 * c3 - s1s2 * s3;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;

      var angle = System.Math.Acos(w) * 2;

      var square = x * x + y * y + z * z;

      if (square < 0.001) // If all euler angles are zero angles, i.e. = 0, so we can set the axis to anything to avoid divide by zero.
        return new(1, 0, 0, angle);

      square = System.Math.Sqrt(square);

      x /= square;
      y /= square;
      z /= square;

      return new(x, y, z, angle);
    }

    public static System.Numerics.Matrix4x4 ToMatrixTaitBryanXYZ(this Geometry.EulerAngles source)
    {
      var (s1, c1) = System.Math.SinCos(source.Yaw);
      var (s2, c2) = System.Math.SinCos(source.Pitch);
      var (s3, c3) = System.Math.SinCos(source.Roll);

      return new(
        (float)(c2 * c3), (float)-s2, (float)(c2 * s3), 0,
        (float)(s1 * s3 + c1 * c3 * s2), (float)(c1 * s2), (float)(c1 * s2 * s3 - c3 * s1), 0,
        (float)(c3 * s1 * s2 - c1 * s3), (float)(c2 * s1), (float)(c1 * c3 + s1 * s2 * s3), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhTaitBryanYXZ(this Geometry.EulerAngles source)
    {
      var (s1, c1) = System.Math.SinCos(source.Yaw);
      var (s2, c2) = System.Math.SinCos(source.Pitch);
      var (s3, c3) = System.Math.SinCos(source.Roll);

      return new(
        (float)(c1 * c3 + s1 * s2 * s3), (float)(c3 * s1 * s2 - c1 * s3), (float)(c2 * s1), 0,
        (float)(c2 * s3), (float)(c2 * c3), (float)-s2, 0,
        (float)(c1 * s2 * s3 - c3 * s1), (float)(c1 * c3 * s2 + s1 * s3), (float)(c1 * c2), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhTaitBryanZYX(this Geometry.EulerAngles source)
    {
      var (s3, c3) = System.Math.SinCos(source.Yaw);
      var (s2, c2) = System.Math.SinCos(source.Pitch);
      var (s1, c1) = System.Math.SinCos(source.Roll);

      return new(
        (float)(c1 * c2), (float)(c1 * s2 * s3 - c3 * s1), (float)(s1 * s3 + c1 * c3 * s2), 0,
        (float)(c2 * s1), (float)(c1 * c3 + s1 * s2 * s3), (float)(c3 * s1 * s2 - c1 * s3), 0,
        (float)-s2, (float)(c2 * s3), (float)(c2 * c3), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhProperEulerZXZ(this Geometry.EulerAngles source)
    {
      var (s1, c1) = System.Math.SinCos(source.Yaw);
      var (s2, c2) = System.Math.SinCos(source.Pitch);
      var (s3, c3) = System.Math.SinCos(source.Roll);

      return new(
        (float)(c1 * c3 - c2 * s1 * s3), (float)(-c1 * s3 - c2 * c3 * s1), (float)(s1 * s2), 0,
        (float)(c3 * s1 + c1 * c2 * s3), (float)(c1 * c2 * c3 - s1 * s3), (float)(-c1 * s2), 0,
        (float)(s2 * s3), (float)(c3 * s2), (float)c2, 0,
        0, 0, 0, 1
      );
    }

    public static (Units.Angle yaw, Units.Angle pitch, Units.Angle roll) ToQuantities(this Geometry.EulerAngles source)
      => (
        new Units.Angle(source.Yaw),
        new Units.Angle(source.Pitch),
        new Units.Angle(source.Roll)
      );

    public static System.Numerics.Quaternion ToQuaternion(this Geometry.EulerAngles source)
    {
      var halfYaw = source.Yaw / 2;
      var halfPitch = source.Pitch / 2;
      var halfRoll = source.Roll / 2;

      var (sy, cy) = System.Math.SinCos(halfYaw);
      var (sp, cp) = System.Math.SinCos(halfPitch);
      var (sr, cr) = System.Math.SinCos(halfRoll);

      return new(
        (float)(sr * cp * cy - cr * sp * sy),
        (float)(cr * sp * cy + sr * cp * sy),
        (float)(cr * cp * sy - sr * sp * cy),
        (float)(cr * cp * cy + sr * sp * sy)
      );
    }
  }

  #endregion ExtensionMethods

  namespace Geometry
  {
    /// <summary>Euler-angles 3D orientation.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Euler_angles"/>
    /// <remarks>The Tait-Bryan sequence z-y'-x" (intrinsic rotations) or x-y-z (extrinsic rotations) represents the intrinsic rotations are known as: yaw, pitch and roll. All angles in radians.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct EulerAngles
    {
      private readonly double m_yaw;
      private readonly double m_pitch;
      private readonly double m_roll;

      public EulerAngles(double yaw, double pitch, double roll)
      {
        m_yaw = yaw;
        m_pitch = pitch;
        m_roll = roll;
      }

      public void Deconstruct(out double yaw, out double pitch, out double roll) { yaw = m_yaw; pitch = m_pitch; roll = m_roll; }

      /// <summary>The horizontal directional (left/right) angle, in radians. A.k.a. Azimuth, Bearing or Heading.</summary>
      public double Yaw { get => m_yaw; init => m_yaw = value; }
      /// <summary>The vertical directional (up/down) angle, in radians. A.k.a. Attitude, Elevation or Inclination.</summary>
      public double Pitch { get => m_pitch; init => m_pitch = value; }
      /// <summary>The horizontal lean (left/right) angle, in radians. A.k.a. Bank or Tilt.</summary>
      public double Roll { get => m_roll; init => m_roll = value; }
    }
  }
}
