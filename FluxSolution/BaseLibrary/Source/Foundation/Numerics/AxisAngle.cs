namespace Flux
{
  /// <summary>Axis-angle 3D rotation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct AxisAngle
    : System.IEquatable<AxisAngle>
  {
    private readonly double m_x;
    private readonly double m_y;
    private readonly double m_z;
    private readonly double m_radAngle;

    public AxisAngle(double x, double y, double z, double radAngle)
    {
      var magnitude = System.Math.Sqrt(x * x + y * y + z * z);

      if (magnitude == 0)
        throw new System.ArithmeticException("Invalid axis (magnitude = 0).");

      m_x = x /= magnitude;
      m_y = y /= magnitude;
      m_z = z /= magnitude;

      m_radAngle = radAngle;
    }

    [System.Diagnostics.Contracts.Pure] public double X => m_x;
    [System.Diagnostics.Contracts.Pure] public double Y => m_y;
    [System.Diagnostics.Contracts.Pure] public double Z => m_z;
    [System.Diagnostics.Contracts.Pure] public Angle Angle => new(m_radAngle);
    [System.Diagnostics.Contracts.Pure] public CartesianCoordinate3R Axis => new(m_x, m_y, m_z);

    [System.Diagnostics.Contracts.Pure]
    public EulerAngles ToEulerAngles()
    {
      var s = System.Math.Sin(m_radAngle);
      var t = 1 - System.Math.Cos(m_radAngle);

      var test = m_x * m_y * t + m_z * s;

      if (test > 0.998) // North pole singularity detected.
        return new(2 * System.Math.Atan2(m_x * System.Math.Sin(m_radAngle / 2), System.Math.Cos(m_radAngle / 2)), System.Math.PI / 2, 0);
      if (test < -0.998) // South pole singularity detected.
        return new(-2 * System.Math.Atan2(m_x * System.Math.Sin(m_radAngle / 2), System.Math.Cos(m_radAngle / 2)), -System.Math.PI / 2, 0);

      var heading = System.Math.Atan2(m_y * s - m_x * m_z * t, 1 - (m_y * m_y + m_z * m_z) * t);
      var attitude = System.Math.Asin(m_x * m_y * t + m_z * s);
      var bank = System.Math.Atan2(m_x * s - m_y * m_z * t, 1 - (m_x * m_x + m_z * m_z) * t);

      return new(heading, attitude, bank);
    }
    [System.Diagnostics.Contracts.Pure]
    public Quaternion ToQuaternion()
    {
      var h = m_radAngle / 2;
      var s = System.Math.Sin(h);
      var x = m_x * s;
      var y = m_y * s;
      var z = m_z * s;
      var w = System.Math.Cos(h);
      return new(x, y, z, w);
    }

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure]
    public static bool operator ==(AxisAngle a, AxisAngle b)
      => a.Equals(b);
    [System.Diagnostics.Contracts.Pure]
    public static bool operator !=(AxisAngle a, AxisAngle b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(AxisAngle other)
      => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z && m_radAngle == other.m_radAngle;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is AxisAngle o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y, m_z, m_radAngle);
    [System.Diagnostics.Contracts.Pure]
    public override string ToString()
      => $"{GetType().Name} {{ X = {m_x}, Y = {m_x}, Z = {m_x}, Angle = {new Angle(m_radAngle).ToUnitString(AngleUnit.Radian)} ({new Angle(m_radAngle).ToUnitString(AngleUnit.Degree, "N1")}) }}";
    #endregion Object overrides
  }
}
