namespace Flux
{
  #region ExtensionMethods
  public static partial class GeometryExtensionMethods
  {
    public static Geometry.EulerAngles ToEulerAngles(this Geometry.AxisAngle source)
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

#if NET7_0_OR_GREATER

    public static (Geometry.CartesianCoordinate3<double> axis, Units.Angle angle) ToQuantities(this Geometry.AxisAngle source)
      => (
        new Geometry.CartesianCoordinate3<double>(source.X, source.Y, source.Z),
        new Units.Angle(double.CreateChecked(source.Angle))
      );

    public static System.Numerics.Quaternion ToQuaternion(this Geometry.AxisAngle source)
    {
      var h = source.Angle.Divide(2);

      var (s, w) = System.Math.SinCos(h);

      var x = source.X * s;
      var y = source.Y * s;
      var z = source.Z * s;

      return new((float)x, (float)y, (float)z, (float)w);
    }
#endif

  }

  #endregion ExtensionMethods

  namespace Geometry
  {
    /// <summary>Axis-angle 3D rotation.</summary>
    /// <remarks>All angles in radians.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
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

      /// <summary>Create a <see cref="Quantities.Angle"/> from the angle component of the rotation.</summary>
      /// <returns>The angle component as an <see cref="Quantities.Angle"/>.</returns>
      public Units.Angle ToAngle()
        => new(m_angle);

#if NET7_0_OR_GREATER
      /// <summary>Create an <see cref="CartesianCoordinate3{double}"/> from the axis vector components of the rotation.</summary>
      /// <returns>The axis vector as a <see cref="CartesianCoordinate3{double}"/>.</returns>
      public CartesianCoordinate3<double> ToAxis()
        => new(m_x, m_y, m_z);
#endif
    }
  }
}
