namespace Flux
{
  public static partial class Em
  {
    public static EuclideanRotations.AxisAngle ToAxisAngle(this EuclideanRotations.EulerAngles source)
    {
      var halfYaw = source.Yaw / 2;
      var halfPitch = source.Pitch / 2;
      var halfRoll = source.Roll / 2;

      var (s1, c1) = double.SinCos(halfYaw.Value);
      var (s2, c2) = double.SinCos(halfPitch.Value);
      var (s3, c3) = double.SinCos(halfRoll.Value);

      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;

      var w = c1c2 * c3 - s1s2 * s3;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;

      var angle = double.Acos(w) * 2;

      var square = x * x + y * y + z * z;

      if (square < 0.001) // If all euler angles are zero angles, i.e. = 0, so we can set the axis to anything to avoid divide by zero.
        return new(angle, Units.AngleUnit.Radian, 1, Units.LengthUnit.Meter, 0, Units.LengthUnit.Meter, 0, Units.LengthUnit.Meter);

      square = double.Sqrt(square);

      x /= square;
      y /= square;
      z /= square;

      return new(angle, Units.AngleUnit.Radian, x, Units.LengthUnit.Meter, y, Units.LengthUnit.Meter, z, Units.LengthUnit.Meter);
    }

    public static System.Numerics.Matrix4x4 ToMatrixTaitBryanXYZ(this EuclideanRotations.EulerAngles source)
    {
      var (s1, c1) = double.SinCos(source.Yaw.Value);
      var (s2, c2) = double.SinCos(source.Pitch.Value);
      var (s3, c3) = double.SinCos(source.Roll.Value);

      return new(
        (float)(c2 * c3), (float)-s2, (float)(c2 * s3), 0,
        (float)(s1 * s3 + c1 * c3 * s2), (float)(c1 * s2), (float)(c1 * s2 * s3 - c3 * s1), 0,
        (float)(c3 * s1 * s2 - c1 * s3), (float)(c2 * s1), (float)(c1 * c3 + s1 * s2 * s3), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhTaitBryanYXZ(this EuclideanRotations.EulerAngles source)
    {
      var (s1, c1) = double.SinCos(source.Yaw.Value);
      var (s2, c2) = double.SinCos(source.Pitch.Value);
      var (s3, c3) = double.SinCos(source.Roll.Value);

      return new(
        (float)(c1 * c3 + s1 * s2 * s3), (float)(c3 * s1 * s2 - c1 * s3), (float)(c2 * s1), 0,
        (float)(c2 * s3), (float)(c2 * c3), (float)-s2, 0,
        (float)(c1 * s2 * s3 - c3 * s1), (float)(c1 * c3 * s2 + s1 * s3), (float)(c1 * c2), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhTaitBryanZYX(this EuclideanRotations.EulerAngles source)
    {
      var (s3, c3) = double.SinCos(source.Yaw.Value);
      var (s2, c2) = double.SinCos(source.Pitch.Value);
      var (s1, c1) = double.SinCos(source.Roll.Value);

      return new(
        (float)(c1 * c2), (float)(c1 * s2 * s3 - c3 * s1), (float)(s1 * s3 + c1 * c3 * s2), 0,
        (float)(c2 * s1), (float)(c1 * c3 + s1 * s2 * s3), (float)(c3 * s1 * s2 - c1 * s3), 0,
        (float)-s2, (float)(c2 * s3), (float)(c2 * c3), 0,
        0, 0, 0, 1
      );
    }

    public static System.Numerics.Matrix4x4 ToMatrixLhProperEulerZXZ(this EuclideanRotations.EulerAngles source)
    {
      var (s1, c1) = double.SinCos(source.Yaw.Value);
      var (s2, c2) = double.SinCos(source.Pitch.Value);
      var (s3, c3) = double.SinCos(source.Roll.Value);

      return new(
        (float)(c1 * c3 - c2 * s1 * s3), (float)(-c1 * s3 - c2 * c3 * s1), (float)(s1 * s2), 0,
        (float)(c3 * s1 + c1 * c2 * s3), (float)(c1 * c2 * c3 - s1 * s3), (float)(-c1 * s2), 0,
        (float)(s2 * s3), (float)(c3 * s2), (float)c2, 0,
        0, 0, 0, 1
      );
    }

    public static (Units.Angle yaw, Units.Angle pitch, Units.Angle roll) ToQuantities(this EuclideanRotations.EulerAngles source)
      => (
        source.Yaw,
        source.Pitch,
        source.Roll
      );

    public static System.Numerics.Quaternion ToQuaternion(this EuclideanRotations.EulerAngles source)
    {
      var (yaw, pitch, roll) = source;

      var (x, y, z, w) = EuclideanRotations.EulerAngles.ConvertEulerAnglesToQuaternion(yaw, pitch, roll);

      return new((float)x, (float)y, (float)z, (float)w);
    }
  }
}
