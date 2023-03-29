namespace Flux
{
  #region ExtensionMethods
  public static partial class NumericsExtensionMethods
  {
    public static Numerics.AxisAngle<TSelf> ToAxisAngle<TSelf>(this Numerics.EulerAngles<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var halfYaw = source.Yaw.Divide(2);
      var halfPitch = source.Pitch.Divide(2);
      var halfRoll = source.Roll.Divide(2);

      var (s1, c1) = TSelf.SinCos(halfYaw);
      var (s2, c2) = TSelf.SinCos(halfPitch);
      var (s3, c3) = TSelf.SinCos(halfRoll);

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

    public static Numerics.Matrix4<TSelf> ToMatrixTaitBryanXYZ<TSelf>(this Numerics.EulerAngles<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var (s1, c1) = TSelf.SinCos(source.Yaw);
      var (s2, c2) = TSelf.SinCos(source.Pitch);
      var (s3, c3) = TSelf.SinCos(source.Roll);

      return new(
        c2 * c3, -s2, c2 * s3, TSelf.Zero,
        s1 * s3 + c1 * c3 * s2, c1 * s2, c1 * s2 * s3 - c3 * s1, TSelf.Zero,
        c3 * s1 * s2 - c1 * s3, c2 * s1, c1 * c3 + s1 * s2 * s3, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public static Numerics.Matrix4<TSelf> ToMatrixLhTaitBryanYXZ<TSelf>(this Numerics.EulerAngles<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var (s1, c1) = TSelf.SinCos(source.Yaw);
      var (s2, c2) = TSelf.SinCos(source.Pitch);
      var (s3, c3) = TSelf.SinCos(source.Roll);

      return new(
        c1 * c3 + s1 * s2 * s3, c3 * s1 * s2 - c1 * s3, c2 * s1, TSelf.Zero,
        c2 * s3, c2 * c3, -s2, TSelf.Zero,
        c1 * s2 * s3 - c3 * s1, c1 * c3 * s2 + s1 * s3, c1 * c2, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public static Numerics.Matrix4<TSelf> ToMatrixLhTaitBryanZYX<TSelf>(this Numerics.EulerAngles<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var (s3, c3) = TSelf.SinCos(source.Yaw);
      var (s2, c2) = TSelf.SinCos(source.Pitch);
      var (s1, c1) = TSelf.SinCos(source.Roll);

      return new(
        c1 * c2, c1 * s2 * s3 - c3 * s1, s1 * s3 + c1 * c3 * s2, TSelf.Zero,
        c2 * s1, c1 * c3 + s1 * s2 * s3, c3 * s1 * s2 - c1 * s3, TSelf.Zero,
        -s2, c2 * s3, c2 * c3, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public static Numerics.Matrix4<TSelf> ToMatrixLhProperEulerZXZ<TSelf>(this Numerics.EulerAngles<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var (s1, c1) = TSelf.SinCos(source.Yaw);
      var (s2, c2) = TSelf.SinCos(source.Pitch);
      var (s3, c3) = TSelf.SinCos(source.Roll);

      return new(
        c1 * c3 - c2 * s1 * s3, -c1 * s3 - c2 * c3 * s1, s1 * s2, TSelf.Zero,
        c3 * s1 + c1 * c2 * s3, c1 * c2 * c3 - s1 * s3, -c1 * s2, TSelf.Zero,
        s2 * s3, c3 * s2, c2, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    public static (Quantities.Angle yaw, Quantities.Angle pitch, Quantities.Angle roll) ToQuantities<TSelf>(this Numerics.EulerAngles<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        new Quantities.Angle(double.CreateChecked(source.Yaw)),
        new Quantities.Angle(double.CreateChecked(source.Pitch)),
        new Quantities.Angle(double.CreateChecked(source.Roll))
      );

    public static Numerics.Quaternion<TSelf> ToQuaternion<TSelf>(this Numerics.EulerAngles<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var halfYaw = source.Yaw.Divide(2);
      var halfPitch = source.Pitch.Divide(2);
      var halfRoll = source.Roll.Divide(2);

      var (sy, cy) = TSelf.SinCos(halfYaw);
      var (sp, cp) = TSelf.SinCos(halfPitch);
      var (sr, cr) = TSelf.SinCos(halfRoll);

      return new(
        sr * cp * cy - cr * sp * sy,
        cr * sp * cy + sr * cp * sy,
        cr * cp * sy - sr * sp * cy,
        cr * cp * cy + sr * sp * sy
      );
    }
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
    /// <summary>Euler-angles 3D orientation.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Euler_angles"/>
    /// <remarks>The Tait-Bryan sequence z-y'-x" (intrinsic rotations) or x-y-z (extrinsic rotations) represents the intrinsic rotations are known as: yaw, pitch and roll. All angles in radians.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct EulerAngles<TSelf>
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      private readonly TSelf m_yaw;
      private readonly TSelf m_pitch;
      private readonly TSelf m_roll;

      public EulerAngles(TSelf yaw, TSelf pitch, TSelf roll)
      {
        m_yaw = yaw;
        m_pitch = pitch;
        m_roll = roll;
      }

      public void Deconstruct(out TSelf yaw, out TSelf pitch, out TSelf roll) { yaw = m_yaw; pitch = m_pitch; roll = m_roll; }

      /// <summary>The horizontal directional (left/right) angle, in radians. A.k.a. Azimuth, Bearing or Heading.</summary>
      public TSelf Yaw { get => m_yaw; init => m_yaw = value; }
      /// <summary>The vertical directional (up/down) angle, in radians. A.k.a. Attitude, Elevation or Inclination.</summary>
      public TSelf Pitch { get => m_pitch; init => m_pitch = value; }
      /// <summary>The horizontal lean (left/right) angle, in radians. A.k.a. Bank or Tilt.</summary>
      public TSelf Roll { get => m_roll; init => m_roll = value; }
    }
  }
}
