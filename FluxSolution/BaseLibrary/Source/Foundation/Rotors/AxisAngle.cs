namespace Flux
{
  /// <summary>Axis-angle 3D rotation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
  public struct AxisAngle
    : System.IEquatable<AxisAngle>
  {
    private readonly double m_x;
    private readonly double m_y;
    private readonly double m_z;
    private readonly double m_angle;

    public AxisAngle(double x, double y, double z, double a)
    {
      var magnitude = System.Math.Sqrt(x * x + y * y + z * z);

      if (magnitude == 0)
        throw new System.ArithmeticException("Invalid axis (magnitude = 0).");

      m_x = x /= magnitude;
      m_y = y /= magnitude;
      m_z = z /= magnitude;

      m_angle = a;
    }

    public double X
      => m_x;
    public double Y
      => m_y;
    public double Z
      => m_z;
    public double Angle
      => m_angle;

    public Angle ToAngle()
      => new(m_angle);
    public CartesianCoordinate3 ToAxis()
      => new(m_x, m_y, m_z);
    public EulerAngles ToEulerAngles()
    {
      var s = System.Math.Sin(m_angle);
      var t = 1 - System.Math.Cos(m_angle);

      var test = m_x * m_y * t + m_z * s;

      if (test > 0.998) // North pole singularity detected.
        return new(2 * System.Math.Atan2(m_x * System.Math.Sin(m_angle / 2), System.Math.Cos(m_angle / 2)), System.Math.PI / 2, 0);
      if (test < -0.998) // South pole singularity detected.
        return new(-2 * System.Math.Atan2(m_x * System.Math.Sin(m_angle / 2), System.Math.Cos(m_angle / 2)), -System.Math.PI / 2, 0);

      var heading = System.Math.Atan2(m_y * s - m_x * m_z * t, 1 - (m_y * m_y + m_z * m_z) * t);
      var attitude = System.Math.Asin(m_x * m_y * t + m_z * s);
      var bank = System.Math.Atan2(m_x * s - m_y * m_z * t, 1 - (m_x * m_x + m_z * m_z) * t);

      return new(heading, attitude, bank);
    }
    public Quaternion ToQuaternion()
    {
      var h = m_angle / 2;
      var s = System.Math.Sin(h);
      var x = m_x * s;
      var y = m_y * s;
      var z = m_z * s;
      var w = System.Math.Cos(h);
      return new(x, y, z, w);
    }

    #region Overloaded operators
    public static bool operator ==(AxisAngle a, AxisAngle b)
      => a.Equals(b);
    public static bool operator !=(AxisAngle a, AxisAngle b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(AxisAngle other)
      => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z && m_angle == other.m_angle;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AxisAngle o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y, m_z, m_angle);
    public override string ToString()
      => $"{GetType().Name} {{ X = {m_x}, Y = {m_x}, Z = {m_x}, Angle = {new Angle(m_angle).ToUnitString(AngleUnit.Radian)} ({new Angle(m_angle).ToUnitString(AngleUnit.Degree, "N1")}) }}";
    #endregion Object overrides
  }
}
