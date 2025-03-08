namespace Flux.Geometry.Rotations
{
  /// <summary>Axis-angle 3D rotation.</summary>
  /// <remarks>All angles in radians.</remarks>
  /// <see href="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct AxisAngle
  {
    private readonly Units.Length m_x;
    private readonly Units.Length m_y;
    private readonly Units.Length m_z;
    private readonly Units.Angle m_angle;

    public AxisAngle(Units.Length x, Units.Length y, Units.Length z, Units.Angle angle)
    {
      var xd = x.Value;
      var yd = y.Value;
      var zd = z.Value;

      if (double.Sqrt(xd * xd + yd * yd + zd * zd) is var magnitude && magnitude == 0)
        throw new ArithmeticException("Invalid axis (magnitude = 0).");

      m_x = x /= magnitude;
      m_y = y /= magnitude;
      m_z = z /= magnitude;

      m_angle = angle;
    }

    public AxisAngle(double xValue, Units.LengthUnit xUnit, double yValue, Units.LengthUnit yUnit, double zValue, Units.LengthUnit zUnit, double angleValue, Units.AngleUnit angleUnit)
      : this(new(xValue, xUnit), new(yValue, yUnit), new(zValue, zUnit), new(angleValue, angleUnit)) { }

    public void Deconstruct(out double x, out double y, out double z, out double angle) { x = m_x.Value; y = m_y.Value; z = m_z.Value; angle = m_angle.Value; }

    /// <summary>The x-axis vector component of the rotation.</summary>
    public Units.Length X => m_x;

    /// <summary>The y-axis vector component of the rotation.</summary>
    public Units.Length Y => m_y;

    /// <summary>The z-axis vector component of the rotation.</summary>
    public Units.Length Z => m_z;

    /// <summary>The angle component of the rotation.</summary>
    public Units.Angle Angle => m_angle;
  }
}
