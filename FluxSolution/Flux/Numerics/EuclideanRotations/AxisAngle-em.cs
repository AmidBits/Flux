namespace Flux
{
  public static partial class Em
  {
    public static Numerics.EuclideanRotations.EulerAngles ToEulerAngles(this Numerics.EuclideanRotations.AxisAngle source)
    {
      var (x, y, z, angle) = source;

      var (s, c) = double.SinCos(angle);

      var t = 1 - c;

      var test = x * y * t + z * s;

      if (test > 0.998) // North pole singularity detected.
      {
        var (sa, ca) = double.SinCos(angle / 2);

        return new(
          double.Atan2(x * sa, ca) * 2, Units.AngleUnit.Radian,
          double.Pi / 2, Units.AngleUnit.Radian,
          0, Units.AngleUnit.Radian
        );
      }
      else if (test < -0.998) // South pole singularity detected.
      {
        var (sa, ca) = double.SinCos(angle / 2);

        return new(
          double.Atan2(x * sa, ca) * -2, Units.AngleUnit.Radian,
          -double.Pi / 2, Units.AngleUnit.Radian,
          0, Units.AngleUnit.Radian
        );
      }
      else
        return new(
          double.Atan2(y * s - x * z * t, 1 - (y * y + z * z) * t), Units.AngleUnit.Radian,
          double.Asin(x * y * t + z * s), Units.AngleUnit.Radian,
          double.Atan2(x * s - y * z * t, 1 - (x * x + z * z) * t), Units.AngleUnit.Radian
        );
    }

    public static (CoordinateSystems.CartesianCoordinate axis, Units.Angle angle) ToQuantities(this Numerics.EuclideanRotations.AxisAngle source)
      => (
        new((float)source.X.Value, (float)source.Y.Value, (float)source.Z.Value),
        source.Angle
      );

    public static System.Numerics.Quaternion ToQuaternion(this Numerics.EuclideanRotations.AxisAngle source)
    {
      var (angle, x, y, z) = source;

      (x, y, z, var w) = Numerics.EuclideanRotations.AxisAngle.ConvertAxisAngleToQuaternion(angle, x, y, z);

      return new((float)x, (float)y, (float)z, (float)w);
    }
  }
}
