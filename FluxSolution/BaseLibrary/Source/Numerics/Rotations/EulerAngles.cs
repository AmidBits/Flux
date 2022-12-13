namespace Flux
{
  public static partial class ExtensionMethods
  {

  }

  /// <summary>Euler-angles 3D orientation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Euler_angles"/>
  /// <remarks>The Tait-Bryan sequence z-y'-x" (intrinsic rotations) or x-y-z (extrinsic rotations) represents the intrinsic rotations are known as: yaw, pitch and roll.</remarks>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct EulerAngles<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    private readonly TSelf m_radYaw; // Yaw.
    private readonly TSelf m_radPitch; // Pitch.
    private readonly TSelf m_radRoll; // Roll.

    public EulerAngles(TSelf radYaw, TSelf radPitch, TSelf radRoll)
    {
      m_radYaw = radYaw;
      m_radPitch = radPitch;
      m_radRoll = radRoll;
    }

    /// <summary>The horizontal directional (left/right) angle. A.k.a. Azimuth, Bearing and Heading.</summary>
    public TSelf Yaw { get => m_radYaw; init => m_radYaw = value; }
    /// <summary>The vertical directional (up/down) angle. A.k.a. Attitude, Elevation and Inclination.</summary>
    public TSelf Pitch { get => m_radPitch; init => m_radPitch = value; }
    /// <summary>The horizontal lean (left/right) angle. A.k.a. Bank and Tilt.</summary>
    public TSelf Roll { get => m_radRoll; init => m_radRoll = value; }

    public AxisAngle<TSelf> ToAxisAngle()
    {
      var c1 = TSelf.Cos(m_radYaw.Divide(2));
      var s1 = TSelf.Sin(m_radYaw.Divide(2));
      var c2 = TSelf.Cos(m_radPitch.Divide(2));
      var s2 = TSelf.Sin(m_radPitch.Divide(2));
      var c3 = TSelf.Cos(m_radRoll.Divide(2));
      var s3 = TSelf.Sin(m_radRoll.Divide(2));

      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;

      var w = c1c2 * c3 - s1s2 * s3;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;

      var angle = TSelf.Acos(w).Multiply(2);

      var square = x * x + y * y + z * z;

      if (square < TSelf.CreateChecked(0.001)) // If all euler angles are zero angles, i.e. = 0, so we can set the axis to anything to avoid divide by zero.
        return new(TSelf.One, TSelf.Zero, TSelf.Zero, angle);

      square = TSelf.Sqrt(square);

      x /= square;
      y /= square;
      z /= square;

      return new(x, y, z, angle);
    }

    public Matrix4<TSelf> ToMatrixTaitBryanXYZ()
    {
      var c1 = TSelf.Cos(m_radYaw);
      var s1 = TSelf.Sin(m_radYaw);
      var c2 = TSelf.Cos(m_radPitch);
      var s2 = TSelf.Sin(m_radPitch);
      var c3 = TSelf.Cos(m_radRoll);
      var s3 = TSelf.Sin(m_radRoll);

      return new(
        c2 * c3, -s2, c2 * s3, TSelf.Zero,
        s1 * s3 + c1 * c3 * s2, c1 * s2, c1 * s2 * s3 - c3 * s1, TSelf.Zero,
        c3 * s1 * s2 - c1 * s3, c2 * s1, c1 * c3 + s1 * s2 * s3, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public Matrix4<TSelf> ToMatrixLhTaitBryanYXZ()
    {
      var c1 = TSelf.Cos(m_radYaw);
      var s1 = TSelf.Sin(m_radYaw);
      var c2 = TSelf.Cos(m_radPitch);
      var s2 = TSelf.Sin(m_radPitch);
      var c3 = TSelf.Cos(m_radRoll);
      var s3 = TSelf.Sin(m_radRoll);

      return new(
        c1 * c3 + s1 * s2 * s3, c3 * s1 * s2 - c1 * s3, c2 * s1, TSelf.Zero,
        c2 * s3, c2 * c3, -s2, TSelf.Zero,
        c1 * s2 * s3 - c3 * s1, c1 * c3 * s2 + s1 * s3, c1 * c2, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public Matrix4<TSelf> ToMatrixLhTaitBryanZYX()
    {
      var c3 = TSelf.Cos(m_radYaw);
      var s3 = TSelf.Sin(m_radYaw);
      var c2 = TSelf.Cos(m_radPitch);
      var s2 = TSelf.Sin(m_radPitch);
      var c1 = TSelf.Cos(m_radRoll);
      var s1 = TSelf.Sin(m_radRoll);

      return new(
        c1 * c2, c1 * s2 * s3 - c3 * s1, s1 * s3 + c1 * c3 * s2, TSelf.Zero,
        c2 * s1, c1 * c3 + s1 * s2 * s3, c3 * s1 * s2 - c1 * s3, TSelf.Zero,
        -s2, c2 * s3, c2 * c3, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public Matrix4<TSelf> ToMatrixLhProperEulerZXZ()
    {
      var c1 = TSelf.Cos(m_radYaw);
      var s1 = TSelf.Sin(m_radYaw);
      var c2 = TSelf.Cos(m_radPitch);
      var s2 = TSelf.Sin(m_radPitch);
      var c3 = TSelf.Cos(m_radRoll);
      var s3 = TSelf.Sin(m_radRoll);

      return new(
        c1 * c3 - c2 * s1 * s3, -c1 * s3 - c2 * c3 * s1, s1 * s2, TSelf.Zero,
        c3 * s1 + c1 * c2 * s3, c1 * c2 * c3 - s1 * s3, -c1 * s2, TSelf.Zero,
        s2 * s3, c3 * s2, c2, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public Quaternion<TSelf> ToQuaternion()
    {
      var cy = TSelf.Cos(m_radYaw.Divide(2));
      var sy = TSelf.Sin(m_radYaw.Divide(2));
      var cp = TSelf.Cos(m_radPitch.Divide(2));
      var sp = TSelf.Sin(m_radPitch.Divide(2));
      var cr = TSelf.Cos(m_radRoll.Divide(2));
      var sr = TSelf.Sin(m_radRoll.Divide(2));

      var x = sr * cp * cy - cr * sp * sy;
      var y = cr * sp * cy + sr * cp * sy;
      var z = cr * cp * sy - sr * sp * cy;
      var w = cr * cp * cy + sr * sp * sy;

      return new(x, y, z, w);
    }

    #region Static methods

    public (TSelf x, TSelf y, TSelf z, TSelf w) ConvertToQuaternion()
    {
      var c1 = TSelf.Cos(m_radYaw.Divide(2));
      var s1 = TSelf.Sin(m_radYaw.Divide(2));
      var c2 = TSelf.Cos(m_radPitch.Divide(2));
      var s2 = TSelf.Sin(m_radPitch.Divide(2));
      var c3 = TSelf.Cos(m_radRoll.Divide(2));
      var s3 = TSelf.Sin(m_radRoll.Divide(2));
      var c1c2 = c1 * c2;
      var s1s2 = s1 * s2;
      var x = c1c2 * s3 + s1s2 * c3;
      var y = s1 * c2 * c3 + c1 * s2 * s3;
      var z = c1 * s2 * c3 - s1 * c2 * s3;
      var w = c1c2 * c3 - s1s2 * s3;
      return (x, y, z, w);
    }

    public static EulerAngles<TSelf> ConvertQuaternionToEulerAngles(TSelf x, TSelf y, TSelf z, TSelf w)
    {
      var sqw = w * w;
      var sqx = x * x;
      var sqy = y * y;
      var sqz = z * z;

      var unit = sqx + sqy + sqz + sqw; // If normalised is one, otherwise is correction factor.
      var test = x * y + z * w;

      if (test > TSelf.CreateChecked(0.499) * unit) // singularity at north pole.
        return new(TSelf.Atan2(x, w).Multiply(2), TSelf.Pi.Divide(2), TSelf.Zero);

      if (test < -TSelf.CreateChecked(0.499) * unit) // // singularity at south pole
        return new(TSelf.Atan2(x, w).Multiply(-2), -TSelf.Pi.Divide(2), TSelf.Zero);

      var h = TSelf.Atan2(y.Multiply(2) * w - x.Multiply(2) * z, sqx - sqy - sqz + sqw);
      var a = TSelf.Asin(test.Multiply(2) / unit);
      var b = TSelf.Atan2(x.Multiply(2) * w - y.Multiply(2) * z, -sqx + sqy - sqz + sqw);

      return new(h, a, b);
    }
    #endregion Static methods
  }
}
