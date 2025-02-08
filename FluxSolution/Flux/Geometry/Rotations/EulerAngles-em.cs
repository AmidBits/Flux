namespace Flux
{
  public static partial class Em
  {
    public static Geometry.Rotations.AxisAngle ToAxisAngle(this Geometry.Rotations.EulerAngles source)
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
        return new(1, Units.LengthUnit.Meter, 0, Units.LengthUnit.Meter, 0, Units.LengthUnit.Meter, angle, Units.AngleUnit.Radian);

      square = System.Math.Sqrt(square);

      x /= square;
      y /= square;
      z /= square;

      return new(x, Units.LengthUnit.Meter, y, Units.LengthUnit.Meter, z, Units.LengthUnit.Meter, angle, Units.AngleUnit.Radian);
    }

    public static System.Numerics.Matrix4x4 ToMatrixTaitBryanXYZ(this Geometry.Rotations.EulerAngles source)
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

    public static System.Numerics.Matrix4x4 ToMatrixLhTaitBryanYXZ(this Geometry.Rotations.EulerAngles source)
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

    public static System.Numerics.Matrix4x4 ToMatrixLhTaitBryanZYX(this Geometry.Rotations.EulerAngles source)
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

    public static System.Numerics.Matrix4x4 ToMatrixLhProperEulerZXZ(this Geometry.Rotations.EulerAngles source)
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

    public static (Units.Angle yaw, Units.Angle pitch, Units.Angle roll) ToQuantities(this Geometry.Rotations.EulerAngles source)
      => (
        source.Yaw,
        source.Pitch,
        source.Roll
      );

    public static System.Numerics.Quaternion ToQuaternion(this Geometry.Rotations.EulerAngles source)
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
}
