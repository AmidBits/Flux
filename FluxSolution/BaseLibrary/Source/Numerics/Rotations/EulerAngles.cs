namespace Flux
{
  #region ExtensionMethods

  public static partial class Fx
  {
    public static Rotations.AxisAngle ToAxisAngle(this Rotations.EulerAngles source)
    {
      var halfYaw = source.Yaw / 2;
      var halfPitch = source.Pitch / 2;
      var halfRoll = source.Roll / 2;

      var (s1, c1) = System.Math.SinCos(halfYaw.Value);
      var (s2, c2) = System.Math.SinCos(halfPitch.Value);
      var (s3, c3) = System.Math.SinCos(halfRoll.Value);

      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;

      var w = c1c2 * c3 - s1s2 * s3;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;

      var angle = System.Math.Acos(w) * 2;

      var square = x * x + y * y + z * z;

      if (square < 0.001) // If all euler angles are zero angles, i.e. = 0, so we can set the axis to anything to avoid divide by zero.
        return new(1, Quantities.LengthUnit.Meter, 0, Quantities.LengthUnit.Meter, 0, Quantities.LengthUnit.Meter, angle, Quantities.AngleUnit.Radian);

      square = System.Math.Sqrt(square);

      x /= square;
      y /= square;
      z /= square;

      return new(x, Quantities.LengthUnit.Meter, y, Quantities.LengthUnit.Meter, z, Quantities.LengthUnit.Meter, angle, Quantities.AngleUnit.Radian);
    }

    public static System.Numerics.Matrix4x4 ToMatrixTaitBryanXYZ(this Rotations.EulerAngles source)
    {
      var (s1, c1) = System.Math.SinCos(source.Yaw.Value);
      var (s2, c2) = System.Math.SinCos(source.Pitch.Value);
      var (s3, c3) = System.Math.SinCos(source.Roll.Value);

      return new(
        (float)(c2 * c3), (float)-s2, (float)(c2 * s3), 0,
        (float)(s1 * s3 + c1 * c3 * s2), (float)(c1 * s2), (float)(c1 * s2 * s3 - c3 * s1), 0,
        (float)(c3 * s1 * s2 - c1 * s3), (float)(c2 * s1), (float)(c1 * c3 + s1 * s2 * s3), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhTaitBryanYXZ(this Rotations.EulerAngles source)
    {
      var (s1, c1) = System.Math.SinCos(source.Yaw.Value);
      var (s2, c2) = System.Math.SinCos(source.Pitch.Value);
      var (s3, c3) = System.Math.SinCos(source.Roll.Value);

      return new(
        (float)(c1 * c3 + s1 * s2 * s3), (float)(c3 * s1 * s2 - c1 * s3), (float)(c2 * s1), 0,
        (float)(c2 * s3), (float)(c2 * c3), (float)-s2, 0,
        (float)(c1 * s2 * s3 - c3 * s1), (float)(c1 * c3 * s2 + s1 * s3), (float)(c1 * c2), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhTaitBryanZYX(this Rotations.EulerAngles source)
    {
      var (s3, c3) = System.Math.SinCos(source.Yaw.Value);
      var (s2, c2) = System.Math.SinCos(source.Pitch.Value);
      var (s1, c1) = System.Math.SinCos(source.Roll.Value);

      return new(
        (float)(c1 * c2), (float)(c1 * s2 * s3 - c3 * s1), (float)(s1 * s3 + c1 * c3 * s2), 0,
        (float)(c2 * s1), (float)(c1 * c3 + s1 * s2 * s3), (float)(c3 * s1 * s2 - c1 * s3), 0,
        (float)-s2, (float)(c2 * s3), (float)(c2 * c3), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhProperEulerZXZ(this Rotations.EulerAngles source)
    {
      var (s1, c1) = System.Math.SinCos(source.Yaw.Value);
      var (s2, c2) = System.Math.SinCos(source.Pitch.Value);
      var (s3, c3) = System.Math.SinCos(source.Roll.Value);

      return new(
        (float)(c1 * c3 - c2 * s1 * s3), (float)(-c1 * s3 - c2 * c3 * s1), (float)(s1 * s2), 0,
        (float)(c3 * s1 + c1 * c2 * s3), (float)(c1 * c2 * c3 - s1 * s3), (float)(-c1 * s2), 0,
        (float)(s2 * s3), (float)(c3 * s2), (float)c2, 0,
        0, 0, 0, 1
      );
    }

    public static (Quantities.Angle yaw, Quantities.Angle pitch, Quantities.Angle roll) ToQuantities(this Rotations.EulerAngles source)
      => (
        source.Yaw,
        source.Pitch,
        source.Roll
      );

    public static System.Numerics.Quaternion ToQuaternion(this Rotations.EulerAngles source)
    {
      var halfYaw = source.Yaw / 2;
      var halfPitch = source.Pitch / 2;
      var halfRoll = source.Roll / 2;

      var (sy, cy) = System.Math.SinCos(halfYaw.Value);
      var (sp, cp) = System.Math.SinCos(halfPitch.Value);
      var (sr, cr) = System.Math.SinCos(halfRoll.Value);

      return new(
        (float)(sr * cp * cy - cr * sp * sy),
        (float)(cr * sp * cy + sr * cp * sy),
        (float)(cr * cp * sy - sr * sp * cy),
        (float)(cr * cp * cy + sr * sp * sy)
      );
    }
  }

  #endregion ExtensionMethods

  namespace Rotations
  {
    /// <summary>Euler-angles 3D orientation.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Euler_angles"/>
    /// <remarks>The Tait-Bryan sequence z-y'-x" (intrinsic rotations) or x-y-z (extrinsic rotations) represents the intrinsic rotations are known as: yaw, pitch and roll. All angles in radians.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct EulerAngles
    {
      private readonly Quantities.Angle m_yaw;
      private readonly Quantities.Angle m_pitch;
      private readonly Quantities.Angle m_roll;

      public EulerAngles(Quantities.Angle yaw, Quantities.Angle pitch, Quantities.Angle roll)
      {
        m_yaw = yaw;
        m_pitch = pitch;
        m_roll = roll;
      }

      public EulerAngles(double yawValue, Quantities.AngleUnit yawUnit, double pitchValue, Quantities.AngleUnit pitchUnit, double rollValue, Quantities.AngleUnit rollUnit)
        : this(new(yawValue, yawUnit), new(pitchValue, pitchUnit), new(rollValue, rollUnit)) { }

      public void Deconstruct(out Quantities.Angle yaw, out Quantities.Angle pitch, out Quantities.Angle roll) { yaw = m_yaw; pitch = m_pitch; roll = m_roll; }

      public void Deconstruct(out double yaw, out double pitch, out double roll) { yaw = m_yaw.Value; pitch = m_pitch.Value; roll = m_roll.Value; }

      /// <summary>The horizontal directional (left/right) angle, in radians. A.k.a. Azimuth, Bearing or Heading.</summary>
      public Quantities.Angle Yaw => m_yaw;

      /// <summary>The vertical directional (up/down) angle, in radians. A.k.a. Attitude, Elevation or Inclination.</summary>
      public Quantities.Angle Pitch => m_pitch;

      /// <summary>The horizontal lean (left/right) angle, in radians. A.k.a. Bank or Tilt.</summary>
      public Quantities.Angle Roll => m_roll;
    }
  }
}
