namespace Flux
{
  /// <summary>Euler-angles 3D rotation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Euler_angles"/>
  public struct EulerAngles
    : System.IEquatable<EulerAngles>
  {
    private readonly double m_h;
    private readonly double m_a;
    private readonly double m_b;

    public EulerAngles(double heading, double attitude, double bank)
    {
      m_h = heading;
      m_a = attitude;
      m_b = bank;
    }

    /// <summary></summary>
    public double Heading
      => m_h;
    /// <summary></summary>
    public double Attitude
      => m_a;
    /// <summary></summary>
    public double Bank
      => m_b;

    /// <summary></summary>
    public double Azimuth
      => m_h;
    /// <summary></summary>
    public double Elevation
      => m_a;
    /// <summary></summary>
    public double Tilt
      => m_b;

    /// <summary>Angular Velocity: Yaw</summary>
    public double Yaw
      => m_h;
    /// <summary>Angular Velocity: Pitch</summary>
    public double Pitch
      => m_a;
    /// <summary>Angular Velocity: Roll</summary>
    public double Roll
      => m_b;

    public AxisAngle ToAxisAngle()
    {
      var c1 = System.Math.Cos(m_h / 2);
      var s1 = System.Math.Sin(m_h / 2);
      var c2 = System.Math.Cos(m_a / 2);
      var s2 = System.Math.Sin(m_a / 2);
      var c3 = System.Math.Cos(m_b / 2);
      var s3 = System.Math.Sin(m_b / 2);

      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;

      var w = c1c2 * c3 - s1s2 * s3;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;

      var angle = 2 * System.Math.Acos(w);

      var norm = x * x + y * y + z * z;

      if (norm < 0.001) // When all euler angles are zero angle = 0, so we can set axis to anything to avoid divide by zero.
        return new(1, 0, 0, angle);

      norm = System.Math.Sqrt(norm);

      x /= norm;
      y /= norm;
      z /= norm;

      return new(x, y, z, angle);
    }
    public Quaternion ToQuaternion()
    {
      var c1 = System.Math.Cos(m_h / 2);
      var s1 = System.Math.Sin(m_h / 2);
      var c2 = System.Math.Cos(m_a / 2);
      var s2 = System.Math.Sin(m_a / 2);
      var c3 = System.Math.Cos(m_b / 2);
      var s3 = System.Math.Sin(m_b / 2);
      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;
      var w = c1c2 * c3 - s1s2 * s3;
      return new(x, y, z, w);
    }

    #region Static methods
    public (double x, double y, double z, double w) ConvertToQuaternion()
    {
      var c1 = System.Math.Cos(m_h / 2);
      var s1 = System.Math.Sin(m_h / 2);
      var c2 = System.Math.Cos(m_a / 2);
      var s2 = System.Math.Sin(m_a / 2);
      var c3 = System.Math.Cos(m_b / 2);
      var s3 = System.Math.Sin(m_b / 2);
      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;
      var w = c1c2 * c3 - s1s2 * s3;
      return (x, y, z, w);
    }

    public static EulerAngles ConvertQuaternionToEulerAngles(double x, double y, double z, double w)
    {
      var sqw = w * w;
      var sqx = x * x;
      var sqy = y * y;
      var sqz = z * z;

      var unit = sqx + sqy + sqz + sqw; // If normalised is one, otherwise is correction factor.
      var test = x * y + z * w;

      if (test > 0.499 * unit) // singularity at north pole.
        return new(2 * System.Math.Atan2(x, w), Math.PI / 2, 0);

      if (test < -0.499 * unit) // // singularity at south pole
        return new(-2 * System.Math.Atan2(x, w), -Math.PI / 2, 0);

      var h = System.Math.Atan2(2 * y * w - 2 * x * z, sqx - sqy - sqz + sqw);
      var a = System.Math.Asin(2 * test / unit);
      var b = System.Math.Atan2(2 * x * w - 2 * y * z, -sqx + sqy - sqz + sqw);

      return new(h, a, b);
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(EulerAngles a, EulerAngles b)
      => a.Equals(b);
    public static bool operator !=(EulerAngles a, EulerAngles b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(EulerAngles other)
      => m_h == other.m_h && m_a == other.m_a && m_b == other.m_b;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is EulerAngles o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_h, m_a, m_b);
    public override string ToString()
      => $"{GetType().Name} {{ Heading = {m_h}, Attitude = {m_a}, Bank = {m_b} }}";
    #endregion Object overrides
  }
}
