namespace Flux.Numerics.EuclideanRotations
{
  /// <summary>Axis-angle 3D rotation.</summary>
  /// <remarks>All angles in radians.</remarks>
  /// <see href="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct AxisAngle
    : System.IFormattable
  {
    private readonly double m_angle;
    private readonly double m_x;
    private readonly double m_y;
    private readonly double m_z;

    public AxisAngle(double angleRadian, double xMeter, double yMeter, double zMeter)
    {
      if (double.Sqrt(xMeter * xMeter + yMeter * yMeter + zMeter * zMeter) is var magnitude && magnitude == 0)
        throw new ArithmeticException("Invalid axis (magnitude = 0).");

      m_angle = angleRadian;
      m_x = xMeter / magnitude;
      m_y = yMeter / magnitude;
      m_z = zMeter / magnitude;
    }

    public AxisAngle(Units.Angle angle, Units.Length x, Units.Length y, Units.Length z)
      : this(angle.Value, x.Value, y.Value, z.Value)
    { }

    public AxisAngle(double angleValue, Units.AngleUnit angleUnit, double xValue, Units.LengthUnit xUnit, double yValue, Units.LengthUnit yUnit, double zValue, Units.LengthUnit zUnit)
      : this(Units.Angle.ConvertFromUnit(angleUnit, angleValue), Units.Length.ConvertFromUnit(xUnit, xValue), Units.Length.ConvertFromUnit(yUnit, yValue), Units.Length.ConvertFromUnit(zUnit, zValue))
    { }

    public void Deconstruct(out double angleRadian, out double x, out double y, out double z) { angleRadian = m_angle; x = m_x; y = m_y; z = m_z; }

    /// <summary>The angle component of the rotation.</summary>
    public Units.Angle Angle { get => new(m_angle); init => m_angle = value.Value; }

    /// <summary>The x-axis vector component of the rotation.</summary>
    public Units.Length X { get => new(m_x); init => m_x = value.Value; }

    /// <summary>The y-axis vector component of the rotation.</summary>
    public Units.Length Y { get => new(m_y); init => m_y = value.Value; }

    /// <summary>The z-axis vector component of the rotation.</summary>
    public Units.Length Z { get => new(m_z); init => m_z = value.Value; }

    #region Static methods

    #region Conversions

    public static (double x, double y, double z, double w) ConvertAxisAngleToQuaternion(double angle, double x, double y, double z)
    {
      var (s, w) = double.SinCos(angle / 2);

      return (x * s, y * s, z * s, w);
    }

    public static (double angle, double x, double y, double z) ConvertQuaternionToAxisAngle(double x, double y, double z, double w)
    {
      if (w > 1)
        (x, y, z, w) = NormalizeQuaternion(x, y, z, w); // if w>1 acos and sqrt will produce errors, this cant happen if quaternion is normalised.

      var angle = 2 * double.Acos(w);

      var s = double.Sqrt(1 - w * w); // Assuming quaternion normalised then w is less than 1, so term always positive.

      // Test to avoid divide by zero, s is always positive due to sqrt. If s close to zero then direction of axis not important. If it is important that axis is normalised then replace with x=1; y=z=0;

      return (s < 0.001) ? (angle, 1, 0, 0) : (angle, x / s, y / s, z / s);
    }

    #endregion // Conversions

    public static AxisAngle FromQuaternion(double x, double y, double z, double w)
    {
      (var angle, x, y, z) = ConvertQuaternionToAxisAngle(x, y, z, w);

      return new(angle, Units.AngleUnit.Radian, x, Units.LengthUnit.Meter, y, Units.LengthUnit.Meter, z, Units.LengthUnit.Meter);
    }

    public static (double x, double y, double z, double w) NormalizeQuaternion(double x, double y, double z, double w)
    {
      var n = double.Sqrt(x * x + y * y + z * z + w * w);

      return (x / n, y / n, z / n, w / n);
    }

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{Angle.ToUnitString(Units.AngleUnit.Degree, format ?? 6.GetFormatWithCountDecimals(), formatProvider)}, {X.ToString(format ?? 3.GetFormatWithCountDecimals(), formatProvider)}, {Y.ToString(format ?? 3.GetFormatWithCountDecimals(), formatProvider)}, {Z.ToString(format ?? 3.GetFormatWithCountDecimals(), formatProvider)}>";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
