namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Numerics.EulerAngles<TSelf> ToEulerAngles<TSelf>(this Numerics.AxisAngle<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var s = TSelf.Sin(source.Angle);
      var t = TSelf.One - TSelf.Cos(source.Angle);

      var test = source.X * source.Y * t + source.Z * s;

      if (test > TSelf.CreateChecked(0.998)) // North pole singularity detected.
        return new(
          TSelf.Atan2(source.X * TSelf.Sin(source.Angle.Divide(2)), TSelf.Cos(source.Angle.Divide(2))).Multiply(2),
          TSelf.Pi.Divide(2),
          TSelf.Zero
        );
      else if (test < -TSelf.CreateChecked(0.998)) // South pole singularity detected.
        return new(
          TSelf.Atan2(source.X * TSelf.Sin(source.Angle.Divide(2)), TSelf.Cos(source.Angle.Divide(2))).Multiply(-2),
          -TSelf.Pi.Divide(2),
          TSelf.Zero
        );
      else
        return new(
          TSelf.Atan2(source.Y * s - source.X * source.Z * t, TSelf.One - (source.Y * source.Y + source.Z * source.Z) * t),
          TSelf.Asin(source.X * source.Y * t + source.Z * s),
          TSelf.Atan2(source.X * s - source.Y * source.Z * t, TSelf.One - (source.X * source.X + source.Z * source.Z) * t)
        );
    }

    public static Numerics.Quaternion<TSelf> ToQuaternion<TSelf>(this Numerics.AxisAngle<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var h = source.Angle.Divide(2);

      var s = TSelf.Sin(h);

      var x = source.X * s;
      var y = source.Y * s;
      var z = source.Z * s;

      var w = TSelf.Cos(h);

      return new(x, y, z, w);
    }
  }

  namespace Numerics
  {
    /// <summary>Axis-angle 3D rotation.</summary>
    /// <remarks>All angles in radians.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct AxisAngle<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      private readonly TSelf m_x;
      private readonly TSelf m_y;
      private readonly TSelf m_z;
      private readonly TSelf m_angle;

      public AxisAngle(TSelf x, TSelf y, TSelf z, TSelf angle)
      {
        var magnitude = TSelf.Sqrt(x * x + y * y + z * z);

        if (magnitude == TSelf.Zero)
          throw new ArithmeticException("Invalid axis (magnitude = 0).");

        m_x = x /= magnitude;
        m_y = y /= magnitude;
        m_z = z /= magnitude;

        m_angle = angle;
      }

      public TSelf X => m_x;
      public TSelf Y => m_y;
      public TSelf Z => m_z;
      public TSelf Angle => m_angle;

      public Quantities.Angle ToAngle()
        => new(double.CreateChecked(m_angle));

      public CartesianCoordinate3<TSelf> ToAxis()
        => new(m_x, m_y, m_z);
    }
  }
}
