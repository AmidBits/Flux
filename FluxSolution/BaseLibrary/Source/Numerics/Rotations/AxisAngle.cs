namespace Flux
{
  #region ExtensionMethods
  public static partial class Em
  {
    public static Rotations.EulerAngles ToEulerAngles(this Rotations.AxisAngle source)
    {
      var (s, c) = System.Math.SinCos(source.Angle);

      var t = 1 - c;

      var test = source.X * source.Y * t + source.Z * s;

      if (test > 0.998) // North pole singularity detected.
      {
        var (sa, ca) = System.Math.SinCos(source.Angle / 2);

        return new(
          System.Math.Atan2(source.X * sa, ca) * 2,
          System.Math.PI / 2,
          0
        );
      }
      else if (test < -0.998) // South pole singularity detected.
      {
        var (sa, ca) = System.Math.SinCos(source.Angle / 2);

        return new(
          System.Math.Atan2(source.X * sa, ca) * -2,
          -System.Math.PI / 2,
          0
        );
      }
      else
        return new(
          System.Math.Atan2(source.Y * s - source.X * source.Z * t, 1 - (source.Y * source.Y + source.Z * source.Z) * t),
          System.Math.Asin(source.X * source.Y * t + source.Z * s),
          System.Math.Atan2(source.X * s - source.Y * source.Z * t, 1 - (source.X * source.X + source.Z * source.Z) * t)
        );
    }

    public static (System.Numerics.Vector3 axis, Quantities.Angle angle) ToQuantities(this Rotations.AxisAngle source)
      => (
        new System.Numerics.Vector3((float)source.X, (float)source.Y, (float)source.Z),
        new Quantities.Angle(source.Angle)
      );

    public static System.Numerics.Quaternion ToQuaternion(this Rotations.AxisAngle source)
    {
      var (s, w) = System.Math.SinCos(source.Angle / 2);

      var x = source.X * s;
      var y = source.Y * s;
      var z = source.Z * s;

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
      private readonly double m_x;
      private readonly double m_y;
      private readonly double m_z;
      private readonly double m_angle;

      public AxisAngle(double x, double y, double z, double angle)
      {
        var magnitude = System.Math.Sqrt(x * x + y * y + z * z);

        if (magnitude == 0)
          throw new ArithmeticException("Invalid axis (magnitude = 0).");

        m_x = x /= magnitude;
        m_y = y /= magnitude;
        m_z = z /= magnitude;

        m_angle = angle;
      }

      public void Deconstruct(out double x, out double y, out double z, out double angle) { x = m_x; y = m_y; z = m_z; angle = m_angle; }

      /// <summary>The x-axis vector component of the rotation.</summary>
      public double X => m_x;
      /// <summary>The y-axis vector component of the rotation.</summary>
      public double Y => m_y;
      /// <summary>The z-axis vector component of the rotation.</summary>
      public double Z => m_z;
      /// <summary>The angle component of the rotation.</summary>
      public double Angle => m_angle;
    }
  }
}
