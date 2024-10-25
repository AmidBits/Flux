namespace Flux
{
  #region ExtensionMethods
  public static partial class Fx
  {
    public static Rotations.EulerAngles ToEulerAngles(this Rotations.AxisAngle source)
    {
      var (x, y, z, angle) = source;

      var (s, c) = System.Math.SinCos(angle);

      var t = 1 - c;

      var test = x * y * t + z * s;

      if (test > 0.998) // North pole singularity detected.
      {
        var (sa, ca) = System.Math.SinCos(angle / 2);

        return new(
          System.Math.Atan2(x * sa, ca) * 2, Quantities.AngleUnit.Radian,
          System.Math.PI / 2, Quantities.AngleUnit.Radian,
          0, Quantities.AngleUnit.Radian
        );
      }
      else if (test < -0.998) // South pole singularity detected.
      {
        var (sa, ca) = System.Math.SinCos(angle / 2);

        return new(
          System.Math.Atan2(x * sa, ca) * -2, Quantities.AngleUnit.Radian,
          -System.Math.PI / 2, Quantities.AngleUnit.Radian,
          0, Quantities.AngleUnit.Radian
        );
      }
      else
        return new(
          System.Math.Atan2(y * s - x * z * t, 1 - (y * y + z * z) * t), Quantities.AngleUnit.Radian,
          System.Math.Asin(x * y * t + z * s), Quantities.AngleUnit.Radian,
          System.Math.Atan2(x * s - y * z * t, 1 - (x * x + z * z) * t), Quantities.AngleUnit.Radian
        );
    }

    public static (System.Numerics.Vector3 axis, Quantities.Angle angle) ToQuantities(this Rotations.AxisAngle source)
      => (
        new System.Numerics.Vector3((float)source.X.Value, (float)source.Y.Value, (float)source.Z.Value),
        source.Angle
      );

    public static System.Numerics.Quaternion ToQuaternion(this Rotations.AxisAngle source)
    {
      var (s, w) = System.Math.SinCos(source.Angle.Value / 2);

      var x = source.X.Value * s;
      var y = source.Y.Value * s;
      var z = source.Z.Value * s;

      return new((float)x, (float)y, (float)z, (float)w);
    }
  }

  #endregion ExtensionMethods

  namespace Rotations
  {
    /// <summary>Axis-angle 3D rotation.</summary>
    /// <remarks>All angles in radians.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct AxisAngle
    {
      private readonly Quantities.Length m_x;
      private readonly Quantities.Length m_y;
      private readonly Quantities.Length m_z;
      private readonly Quantities.Angle m_angle;

      public AxisAngle(Quantities.Length x, Quantities.Length y, Quantities.Length z, Quantities.Angle angle)
      {
        var xd = x.Value;
        var yd = y.Value;
        var zd = z.Value;

        if (System.Math.Sqrt(xd * xd + yd * yd + zd * zd) is var magnitude && magnitude == 0)
          throw new ArithmeticException("Invalid axis (magnitude = 0).");

        m_x = x /= magnitude;
        m_y = y /= magnitude;
        m_z = z /= magnitude;

        m_angle = angle;
      }

      public AxisAngle(double xValue, Quantities.LengthUnit xUnit, double yValue, Quantities.LengthUnit yUnit, double zValue, Quantities.LengthUnit zUnit, double angleValue, Quantities.AngleUnit angleUnit)
        : this(new Quantities.Length(xValue, xUnit), new Quantities.Length(yValue, yUnit), new Quantities.Length(zValue, zUnit), new Quantities.Angle(angleValue, angleUnit)) { }

      public void Deconstruct(out double x, out double y, out double z, out double angle) { x = m_x.Value; y = m_y.Value; z = m_z.Value; angle = m_angle.Value; }

      /// <summary>The x-axis vector component of the rotation.</summary>
      public Quantities.Length X => m_x;

      /// <summary>The y-axis vector component of the rotation.</summary>
      public Quantities.Length Y => m_y;

      /// <summary>The z-axis vector component of the rotation.</summary>
      public Quantities.Length Z => m_z;

      /// <summary>The angle component of the rotation.</summary>
      public Quantities.Angle Angle => m_angle;
    }
  }
}
