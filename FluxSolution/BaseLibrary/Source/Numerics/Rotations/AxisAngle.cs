namespace Flux
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

    public AxisAngle(TSelf x, TSelf y, TSelf z, TSelf radAngle)
    {
      var magnitude = TSelf.Sqrt(x * x + y * y + z * z);

      if (magnitude == TSelf.Zero)
        throw new ArithmeticException("Invalid axis (magnitude = 0).");

      m_x = x /= magnitude;
      m_y = y /= magnitude;
      m_z = z /= magnitude;

      m_angle = radAngle;
    }

    public TSelf X => m_x;
    public TSelf Y => m_y;
    public TSelf Z => m_z;
    public TSelf Angle => m_angle;

    public Quantities.Angle ToAngle()
      => new(double.CreateChecked(m_angle));

    public CoordinateSystems.CartesianCoordinate3<TSelf> ToAxis()
      => new(m_x, m_y, m_z);

    public EulerAngles<TSelf> ToEulerAngles()
    {
      var s = TSelf.Sin(m_angle);
      var t = TSelf.One - TSelf.Cos(m_angle);

      var test = m_x * m_y * t + m_z * s;

      if (test > TSelf.CreateChecked(0.998)) // North pole singularity detected.
        return new(
          TSelf.Atan2(m_x * TSelf.Sin(m_angle.Divide(2)), TSelf.Cos(m_angle.Divide(2))).Multiply(2),
          TSelf.Pi.Divide(2),
          TSelf.Zero
        );
      else if (test < -TSelf.CreateChecked(0.998)) // South pole singularity detected.
        return new(
          TSelf.Atan2(m_x * TSelf.Sin(m_angle.Divide(2)), TSelf.Cos(m_angle.Divide(2))).Multiply(-2),
          -TSelf.Pi.Divide(2),
          TSelf.Zero
        );
      else
        return new(
          TSelf.Atan2(m_y * s - m_x * m_z * t, TSelf.One - (m_y * m_y + m_z * m_z) * t),
          TSelf.Asin(m_x * m_y * t + m_z * s),
          TSelf.Atan2(m_x * s - m_y * m_z * t, TSelf.One - (m_x * m_x + m_z * m_z) * t)
        );
    }

    public Quaternion<TSelf> ToQuaternion()
    {
      var h = m_angle.Divide(2);
      var s = TSelf.Sin(h);

      var x = m_x * s;
      var y = m_y * s;
      var z = m_z * s;
      var w = TSelf.Cos(h);

      return new(x, y, z, w);
    }
  }
}
