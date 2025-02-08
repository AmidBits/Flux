namespace Flux.Geometry.Rotations
{
  /// <summary>Euler-angles 3D orientation.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Euler_angles"/>
  /// <remarks>The Tait-Bryan sequence z-y'-x" (intrinsic rotations) or x-y-z (extrinsic rotations) represents the intrinsic rotations are known as: yaw, pitch and roll. All angles in radians.</remarks>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct EulerAngles
  {
    private readonly Units.Angle m_yaw;
    private readonly Units.Angle m_pitch;
    private readonly Units.Angle m_roll;

    public EulerAngles(Units.Angle yaw, Units.Angle pitch, Units.Angle roll)
    {
      m_yaw = yaw;
      m_pitch = pitch;
      m_roll = roll;
    }

    public EulerAngles(double yawValue, Units.AngleUnit yawUnit, double pitchValue, Units.AngleUnit pitchUnit, double rollValue, Units.AngleUnit rollUnit)
      : this(new(yawValue, yawUnit), new(pitchValue, pitchUnit), new(rollValue, rollUnit)) { }

    public void Deconstruct(out Units.Angle yaw, out Units.Angle pitch, out Units.Angle roll) { yaw = m_yaw; pitch = m_pitch; roll = m_roll; }

    public void Deconstruct(out double yaw, out double pitch, out double roll) { yaw = m_yaw.Value; pitch = m_pitch.Value; roll = m_roll.Value; }

    /// <summary>The horizontal directional (left/right) angle, in radians. A.k.a. Azimuth, Bearing or Heading.</summary>
    public Units.Angle Yaw => m_yaw;

    /// <summary>The vertical directional (up/down) angle, in radians. A.k.a. Attitude, Elevation or Inclination.</summary>
    public Units.Angle Pitch => m_pitch;

    /// <summary>The horizontal lean (left/right) angle, in radians. A.k.a. Bank or Tilt.</summary>
    public Units.Angle Roll => m_roll;
  }
}
