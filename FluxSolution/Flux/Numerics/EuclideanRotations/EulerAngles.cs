namespace Flux.Numerics.EuclideanRotations
{
  /// <summary>Euler-angles 3D orientation.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Euler_angles"/>
  /// <remarks>The Tait-Bryan sequence z-y'-x" (intrinsic rotations) or x-y-z (extrinsic rotations) represents the intrinsic rotations are known as: yaw, pitch and roll. All angles in radians.</remarks>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct EulerAngles
    : System.IFormattable
  {
    private readonly double m_yaw;
    private readonly double m_pitch;
    private readonly double m_roll;

    public EulerAngles(double yawRadian, double pitchRadian, double rollRadian)
    {
      m_yaw = yawRadian;
      m_pitch = pitchRadian;
      m_roll = rollRadian;
    }

    public EulerAngles(Units.Angle yaw, Units.Angle pitch, Units.Angle roll)
      : this(yaw.Value, pitch.Value, roll.Value)
    { }

    public EulerAngles(double yawValue, Units.AngleUnit yawUnit, double pitchValue, Units.AngleUnit pitchUnit, double rollValue, Units.AngleUnit rollUnit)
      : this(Units.Angle.ConvertFromUnit(yawUnit, yawValue), Units.Angle.ConvertFromUnit(pitchUnit, pitchValue), Units.Angle.ConvertFromUnit(rollUnit, rollValue))
    { }

    public void Deconstruct(out double yaw, out double pitch, out double roll) { yaw = m_yaw; pitch = m_pitch; roll = m_roll; }

    /// <summary>The horizontal directional (left/right) angle, in radians. A.k.a. Azimuth, Bearing or Heading.</summary>
    public Units.Angle Yaw { get => new(m_yaw); init => m_yaw = value.Value; }

    /// <summary>The vertical directional (up/down) angle, in radians. A.k.a. Attitude, Elevation or Inclination.</summary>
    public Units.Angle Pitch { get => new(m_pitch); init => m_pitch = value.Value; }

    /// <summary>The horizontal lean (left/right) angle, in radians. A.k.a. Bank or Tilt.</summary>
    public Units.Angle Roll { get => new(m_roll); init => m_roll = value.Value; }

    #region Static methods

    #region Conversions

    public static (double x, double y, double z, double w) ConvertEulerAnglesToQuaternion(double yaw, double pitch, double roll)
    {
      var (sy, cy) = double.SinCos(yaw / 2);
      var (sp, cp) = double.SinCos(pitch / 2);
      var (sr, cr) = double.SinCos(roll / 2);

      return (
        sr * cp * cy - cr * sp * sy,
        cr * sp * cy + sr * cp * sy,
        cr * cp * sy - sr * sp * cy,
        cr * cp * cy + sr * sp * sy
      );
    }

    public static (double yaw, double pitch, double roll) ConvertQuaternionToEulerAngles(double x, double y, double z, double w)
    {
      double sqw = w * w;
      double sqx = x * x;
      double sqy = y * y;
      double sqz = z * z;
      double unit = sqx + sqy + sqz + sqw; // If normalised is one, otherwise is correction factor.
      double test = x * y + z * w;

      if (test > 0.499 * unit) // Singularity at north pole.
        return (2 * double.Atan2(x, w), double.Pi / 2, 0);

      if (test < -0.499 * unit) // Singularity at south pole.
        return (-2 * double.Atan2(x, w), -double.Pi / 2, 0);

      return (
        double.Atan2(2 * y * w - 2 * x * z, sqx - sqy - sqz + sqw),
        double.Asin(2 * test / unit),
        double.Atan2(2 * x * w - 2 * y * z, -sqx + sqy - sqz + sqw)
      );
    }

    #endregion // Conversions

    public static EulerAngles FromQuaternion(double x, double y, double z, double w)
    {
      var (yaw, pitch, roll) = ConvertQuaternionToEulerAngles(x, y, z, w);

      return new(yaw, Units.AngleUnit.Radian, pitch, Units.AngleUnit.Radian, roll, Units.AngleUnit.Radian);
    }

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{Yaw.ToUnitString(Units.AngleUnit.Degree, format ?? IBinaryInteger.GetFormatStringWithCountDecimals(6), formatProvider)}, {Pitch.ToUnitString(Units.AngleUnit.Degree, format ?? IBinaryInteger.GetFormatStringWithCountDecimals(6), formatProvider)}, {Roll.ToUnitString(Units.AngleUnit.Degree, format ?? IBinaryInteger.GetFormatStringWithCountDecimals(6), formatProvider)}>";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
