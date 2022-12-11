namespace Flux
{
  /// <summary>Axis-angle 3D rotation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct AxisAngle
  {
    private readonly double m_x;
    private readonly double m_y;
    private readonly double m_z;
    private readonly double m_radAngle;

    public AxisAngle(double x, double y, double z, double radAngle)
    {
      var magnitude = Math.Sqrt(x * x + y * y + z * z);

      if (magnitude == 0)
        throw new ArithmeticException("Invalid axis (magnitude = 0).");

      m_x = x /= magnitude;
      m_y = y /= magnitude;
      m_z = z /= magnitude;

      m_radAngle = radAngle;
    }

     public double X => m_x;
     public double Y => m_y;
     public double Z => m_z;

     public Quantities.Angle Angle => new(m_radAngle);

    
    public CoordinateSystems.CartesianCoordinate3<double> ToCartesianCoordinate()
      => new(m_x, m_y, m_z);

    
    public EulerAngles ToEulerAngles()
    {
      var s = Math.Sin(m_radAngle);
      var t = 1 - Math.Cos(m_radAngle);

      var test = m_x * m_y * t + m_z * s;

      if (test > 0.998) // North pole singularity detected.
        return new(2 * Math.Atan2(m_x * Math.Sin(m_radAngle / 2), Math.Cos(m_radAngle / 2)), Math.PI / 2, 0);
      if (test < -0.998) // South pole singularity detected.
        return new(-2 * Math.Atan2(m_x * Math.Sin(m_radAngle / 2), Math.Cos(m_radAngle / 2)), -Math.PI / 2, 0);

      var heading = Math.Atan2(m_y * s - m_x * m_z * t, 1 - (m_y * m_y + m_z * m_z) * t);
      var attitude = Math.Asin(m_x * m_y * t + m_z * s);
      var bank = Math.Atan2(m_x * s - m_y * m_z * t, 1 - (m_x * m_x + m_z * m_z) * t);

      return new(heading, attitude, bank);
    }

    
    public Quaternion ToQuaternion()
    {
      var h = m_radAngle / 2;
      var s = Math.Sin(h);
      var x = m_x * s;
      var y = m_y * s;
      var z = m_z * s;
      var w = Math.Cos(h);
      return new(x, y, z, w);
    }
  }
}
