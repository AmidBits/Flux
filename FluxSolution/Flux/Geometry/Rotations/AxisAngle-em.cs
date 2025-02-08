namespace Flux
{
  public static partial class Em
  {
    public static Geometry.Rotations.EulerAngles ToEulerAngles(this Geometry.Rotations.AxisAngle source)
    {
      var (x, y, z, angle) = source;

      var (s, c) = System.Math.SinCos(angle);

      var t = 1 - c;

      var test = x * y * t + z * s;

      if (test > 0.998) // North pole singularity detected.
      {
        var (sa, ca) = System.Math.SinCos(angle / 2);

        return new(
          System.Math.Atan2(x * sa, ca) * 2, Units.AngleUnit.Radian,
          System.Math.PI / 2, Units.AngleUnit.Radian,
          0, Units.AngleUnit.Radian
        );
      }
      else if (test < -0.998) // South pole singularity detected.
      {
        var (sa, ca) = System.Math.SinCos(angle / 2);

        return new(
          System.Math.Atan2(x * sa, ca) * -2, Units.AngleUnit.Radian,
          -System.Math.PI / 2, Units.AngleUnit.Radian,
          0, Units.AngleUnit.Radian
        );
      }
      else
        return new(
          System.Math.Atan2(y * s - x * z * t, 1 - (y * y + z * z) * t), Units.AngleUnit.Radian,
          System.Math.Asin(x * y * t + z * s), Units.AngleUnit.Radian,
          System.Math.Atan2(x * s - y * z * t, 1 - (x * x + z * z) * t), Units.AngleUnit.Radian
        );
    }

    public static (System.Numerics.Vector3 axis, Units.Angle angle) ToQuantities(this Geometry.Rotations.AxisAngle source)
      => (
        new System.Numerics.Vector3((float)source.X.Value, (float)source.Y.Value, (float)source.Z.Value),
        source.Angle
      );

    public static System.Numerics.Quaternion ToQuaternion(this Geometry.Rotations.AxisAngle source)
    {
      var (s, w) = System.Math.SinCos(source.Angle.Value / 2);

      var x = source.X.Value * s;
      var y = source.Y.Value * s;
      var z = source.Z.Value * s;

      return new((float)x, (float)y, (float)z, (float)w);
    }
  }
}
