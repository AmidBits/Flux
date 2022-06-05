namespace Flux
{
  /// <summary>Euler-angles 3D rotation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Euler_angles"/>
  /// <remarks>The Tait-Bryan sequence z-y'-x" (intrinsic rotations) or x-y-z (extrinsic rotations) represents the intrinsic rotations are known as: yaw, pitch and roll.</remarks>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct EulerAngles
    : System.IEquatable<EulerAngles>
  {
    private readonly double m_radYaw; // Yaw.
    private readonly double m_radPitch; // Pitch.
    private readonly double m_radRoll; // Roll.

    public EulerAngles(double yaw, double pitch, double roll)
    {
      m_radYaw = yaw;
      m_radPitch = pitch;
      m_radRoll = roll;
    }

    /// <summary>The horizontal directional (left/right) angle. A.k.a. Azimuth, Bearing and Heading.</summary>
    public Angle Yaw => new(m_radYaw);
    /// <summary>The vertical directional (up/down) angle. A.k.a. Attitude, Elevation and Inclination.</summary>
    public Angle Pitch => new(m_radPitch);
    /// <summary>The horizontal lean (left/right) angle. A.k.a. Bank and Tilt.</summary>
    public Angle Roll => new(m_radRoll);

    public AxisAngle ToAxisAngle()
    {
      var c1 = System.Math.Cos(m_radYaw / 2);
      var s1 = System.Math.Sin(m_radYaw / 2);
      var c2 = System.Math.Cos(m_radPitch / 2);
      var s2 = System.Math.Sin(m_radPitch / 2);
      var c3 = System.Math.Cos(m_radRoll / 2);
      var s3 = System.Math.Sin(m_radRoll / 2);

      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;

      var w = c1c2 * c3 - s1s2 * s3;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;

      var angle = 2 * System.Math.Acos(w);

      var square = x * x + y * y + z * z;

      if (square < 0.001) // If all euler angles are zero angles, i.e. = 0, so we can set the axis to anything to avoid divide by zero.
        return new(1, 0, 0, angle);

      square = System.Math.Sqrt(square);

      x /= square;
      y /= square;
      z /= square;

      return new(x, y, z, angle);
    }
    [System.Diagnostics.Contracts.Pure]
    public Matrix4 ToMatrixTaitBryanXYZ()
    {
      var c1 = System.Math.Cos(m_radYaw);
      var s1 = System.Math.Sin(m_radYaw);
      var c2 = System.Math.Cos(m_radPitch);
      var s2 = System.Math.Sin(m_radPitch);
      var c3 = System.Math.Cos(m_radRoll);
      var s3 = System.Math.Sin(m_radRoll);

      return new Matrix4(
        c2 * c3, -s2, c2 * s3, 0,
        s1 * s3 + c1 * c3 * s2, c1 * s2, c1 * s2 * s3 - c3 * s1, 0,
        c3 * s1 * s2 - c1 * s3, c2 * s1, c1 * c3 + s1 * s2 * s3, 0,
        0, 0, 0, 1
      );
    }
    [System.Diagnostics.Contracts.Pure]
    public Matrix4 ToMatrixLhTaitBryanYXZ()
    {
      var c1 = System.Math.Cos(m_radYaw);
      var s1 = System.Math.Sin(m_radYaw);
      var c2 = System.Math.Cos(m_radPitch);
      var s2 = System.Math.Sin(m_radPitch);
      var c3 = System.Math.Cos(m_radRoll);
      var s3 = System.Math.Sin(m_radRoll);

      return new Matrix4(
        c1 * c3 + s1 * s2 * s3, c3 * s1 * s2 - c1 * s3, c2 * s1, 0,
        c2 * s3, c2 * c3, -s2, 0,
        c1 * s2 * s3 - c3 * s1, c1 * c3 * s2 + s1 * s3, c1 * c2, 0,
        0, 0, 0, 1
      );
    }
    [System.Diagnostics.Contracts.Pure]
    public Matrix4 ToMatrixLhTaitBryanZYX()
    {
      var c3 = System.Math.Cos(m_radYaw);
      var s3 = System.Math.Sin(m_radYaw);
      var c2 = System.Math.Cos(m_radPitch);
      var s2 = System.Math.Sin(m_radPitch);
      var c1 = System.Math.Cos(m_radRoll);
      var s1 = System.Math.Sin(m_radRoll);

      return new Matrix4(
        c1 * c2, c1 * s2 * s3 - c3 * s1, s1 * s3 + c1 * c3 * s2, 0,
        c2 * s1, c1 * c3 + s1 * s2 * s3, c3 * s1 * s2 - c1 * s3, 0,
        -s2, c2 * s3, c2 * c3, 0,
        0, 0, 0, 1
      );
    }
    [System.Diagnostics.Contracts.Pure]
    public Matrix4 ToMatrixLhProperEulerZXZ()
    {
      var c1 = System.Math.Cos(m_radYaw);
      var s1 = System.Math.Sin(m_radYaw);
      var c2 = System.Math.Cos(m_radPitch);
      var s2 = System.Math.Sin(m_radPitch);
      var c3 = System.Math.Cos(m_radRoll);
      var s3 = System.Math.Sin(m_radRoll);

      return new Matrix4(
        c1 * c3 - c2 * s1 * s3, -c1 * s3 - c2 * c3 * s1, s1 * s2, 0,
        c3 * s1 + c1 * c2 * s3, c1 * c2 * c3 - s1 * s3, -c1 * s2, 0,
        s2 * s3, c3 * s2, c2, 0,
        0, 0, 0, 1
      );
    }
    [System.Diagnostics.Contracts.Pure]
    public Quaternion ToQuaternion()
    {
      var cy = System.Math.Cos(m_radYaw * 0.5);
      var sy = System.Math.Sin(m_radYaw * 0.5);
      var cp = System.Math.Cos(m_radPitch * 0.5);
      var sp = System.Math.Sin(m_radPitch * 0.5);
      var cr = System.Math.Cos(m_radRoll * 0.5);
      var sr = System.Math.Sin(m_radRoll * 0.5);

      var w = cr * cp * cy + sr * sp * sy;
      var x = sr * cp * cy - cr * sp * sy;
      var y = cr * sp * cy + sr * cp * sy;
      var z = cr * cp * sy - sr * sp * cy;

      return new(x, y, z, w);
    }

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public (double x, double y, double z, double w) ConvertToQuaternion()
    {
      var c1 = System.Math.Cos(m_radYaw / 2);
      var s1 = System.Math.Sin(m_radYaw / 2);
      var c2 = System.Math.Cos(m_radPitch / 2);
      var s2 = System.Math.Sin(m_radPitch / 2);
      var c3 = System.Math.Cos(m_radRoll / 2);
      var s3 = System.Math.Sin(m_radRoll / 2);
      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;
      var w = c1c2 * c3 - s1s2 * s3;
      return (x, y, z, w);
    }

    [System.Diagnostics.Contracts.Pure]
    public static EulerAngles ConvertQuaternionToEulerAngles(double x, double y, double z, double w)
    {
      var sqw = w * w;
      var sqx = x * x;
      var sqy = y * y;
      var sqz = z * z;

      var unit = sqx + sqy + sqz + sqw; // If normalised is one, otherwise is correction factor.
      var test = x * y + z * w;

      if (test > 0.499 * unit) // singularity at north pole.
        return new(2 * System.Math.Atan2(x, w), System.Math.PI / 2, 0);

      if (test < -0.499 * unit) // // singularity at south pole
        return new(-2 * System.Math.Atan2(x, w), -System.Math.PI / 2, 0);

      var h = System.Math.Atan2(2 * y * w - 2 * x * z, sqx - sqy - sqz + sqw);
      var a = System.Math.Asin(2 * test / unit);
      var b = System.Math.Atan2(2 * x * w - 2 * y * z, -sqx + sqy - sqz + sqw);

      return new(h, a, b);
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(EulerAngles a, EulerAngles b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(EulerAngles a, EulerAngles b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(EulerAngles other) => m_radYaw == other.m_radYaw && m_radPitch == other.m_radPitch && m_radRoll == other.m_radRoll;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is EulerAngles o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radYaw, m_radPitch, m_radRoll);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Heading = {m_radYaw}, Attitude = {m_radPitch}, Bank = {m_radRoll} }}";
    #endregion Object overrides
  }
}
