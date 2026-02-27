namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Cartesian coordinate.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/></para>
  /// </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CartesianCoordinate
    : System.IFormattable, Units.IValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>>
  {
    public static CartesianCoordinate UnitX { get; } = new(1d, 0d, 0d, 0d);
    public static CartesianCoordinate UnitY { get; } = new(0d, 1d, 0d, 0d);
    public static CartesianCoordinate UnitZ { get; } = new(0d, 0d, 1d, 0d);
    public static CartesianCoordinate UnitW { get; } = new(0d, 0d, 0d, 1d);

    private readonly System.Runtime.Intrinsics.Vector256<double> m_v;

    public CartesianCoordinate(System.Runtime.Intrinsics.Vector128<double> xy) => m_v = System.Runtime.Intrinsics.Vector256.Create(xy[0], xy[1], 0, 0);

    public CartesianCoordinate(System.Runtime.Intrinsics.Vector256<double> xyzw) => m_v = xyzw;

    public CartesianCoordinate(System.Runtime.Intrinsics.Vector256<double> xyz, double w = 0) => m_v = System.Runtime.Intrinsics.Vector256.Create(xyz[0], xyz[1], xyz[2], w);

    public CartesianCoordinate(System.Runtime.Intrinsics.Vector256<double> xy, double z = 0, double w = 0) => m_v = System.Runtime.Intrinsics.Vector256.Create(xy[0], xy[1], z, w);

    public CartesianCoordinate(double x, double y, double z = 0, double w = 0) => m_v = System.Runtime.Intrinsics.Vector256.Create(x, y, z, w);

    public CartesianCoordinate(Units.Length x, Units.Length y = default, Units.Length z = default, Units.Length w = default)
      : this(x.Value, y.Value, z.Value, w.Value) { }

    /// <summary>
    /// <para>Initialize as a full 4D cartesian coordinate.</para>
    /// </summary>
    /// <param name="xValue"></param>
    /// <param name="xUnit"></param>
    /// <param name="yValue"></param>
    /// <param name="yUnit"></param>
    /// <param name="zValue"></param>
    /// <param name="zUnit"></param>
    /// <param name="wValue"></param>
    /// <param name="wUnit"></param>
    public CartesianCoordinate(double xValue, Units.LengthUnit xUnit, double yValue = 0, Units.LengthUnit yUnit = Units.LengthUnit.Meter, double zValue = 0, Units.LengthUnit zUnit = Units.LengthUnit.Meter, double wValue = 0, Units.LengthUnit wUnit = Units.LengthUnit.Meter)
      : this(new Units.Length(xValue, xUnit), new Units.Length(yValue, yUnit), new Units.Length(zValue, zUnit), new Units.Length(wValue, wUnit)) { }

    public void Deconstruct(out double x, out double y)
    {
      x = m_v[0];
      y = m_v[1];
    }

    public void Deconstruct(out double x, out double y, out double z)
    {
      x = m_v[0];
      y = m_v[1];
      z = m_v[2];
    }

    public void Deconstruct(out double x, out double y, out double z, out double w)
    {
      x = m_v[0];
      y = m_v[1];
      z = m_v[2];
      w = m_v[3];
    }

    /// <summary>
    /// <para>X component.</para>
    /// </summary>
    public Units.Length X => new(m_v[0]);

    /// <summary>
    /// <para>Y component.</para>
    /// </summary>
    public Units.Length Y => new(m_v[1]);

    /// <summary>
    /// <para>Z component.</para>
    /// </summary>
    public Units.Length Z => new(m_v[2]);

    /// <summary>
    /// <para>W component.</para>
    /// </summary>
    public Units.Length W => new(m_v[3]);

    /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
    public CylindricalCoordinate ToCylindricalCoordinate()
    {
      var (x, y, z) = this;

      var (radius, azimuth, height) = double.CartesianToCylindrical(x, y, z);

      return new(radius, azimuth, height);
    }

    /// <summary>Creates a new <see cref="Geometry.HexCoordinate{TSelf}"/> from a <see cref="Geometry.CoordinateSystems.CartesianCoordinate"/>.</summary>
    public static HexCoordinate<TResult> ToHexCoordinate<TResult>(CartesianCoordinate source, HalfRounding mode, out TResult q, out TResult r, out TResult s)
      where TResult : System.Numerics.INumber<TResult>
    {
      var (x, y, z) = source;

      HexCoordinate.Round(x, y, z, mode, out q, out r, out s);

      return new(
        q,
        r,
        s
      );
    }

    public System.Drawing.Point ToPoint(HalfRounding mode) => new(System.Convert.ToInt32(double.RoundHalf(m_v[0], mode)), System.Convert.ToInt32(double.RoundHalf(m_v[1], mode)));

    public System.Drawing.PointF ToPointF() => new((float)m_v[0], (float)m_v[1]);

    /// <summary>Creates a new <see cref="PolarCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y) components.</summary>
    public PolarCoordinate ToPolarCoordinate()
    {
      var (x, y) = this;

      var (radius, angle) = double.CartesianToPolar(x, y, true);

      return new(radius, angle);
    }

    /// <summary>Creates a new <see cref="PolarCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y) components.</summary>
    public PolarCoordinate ToPolarCoordinateEx()
    {
      var (x, y) = this;

      var (radius, angle) = double.CartesianToPolar(x, y, false);

      return new(radius, angle);
    }

    /// <summary>Creates a new <see cref="SphericalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
    public SphericalCoordinate ToSphericalCoordinate()
    {
      var (x, y, z) = this;

      var (radius, inclination, azimuth) = double.CartesianToSpherical(x, y, z);

      return new(radius, inclination, azimuth);
    }

    /// <summary>Creates a new <see cref="System.Numerics.Vector2"/> from a <see cref="CoordinateSystems.CartesianCoordinate"/>.</summary>
    public System.Numerics.Vector2 ToVector2()
    {
      var (x, y) = this;

      return new(
        (float)x,
        (float)y
      );
    }

    /// <summary>Creates a new <see cref="System.Numerics.Vector2"/> from a <see cref="CoordinateSystems.CartesianCoordinate"/>.</summary>
    public System.Numerics.Vector3 ToVector3()
    {
      var (x, y, z) = this;

      return new(
        (float)x,
        (float)y,
        (float)z
      );
    }

    /// <summary>Creates a new <see cref="System.Numerics.Vector2"/> from a <see cref="CoordinateSystems.CartesianCoordinate"/>.</summary>
    public System.Numerics.Vector4 ToVector4()
    {
      var (x, y, z, w) = this;

      return new(
        (float)x,
        (float)y,
        (float)z,
        (float)w
      );
    }

    /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="CoordinateSystems.CartesianCoordinate"/> (with 1 (x) + 3 optional components).</summary>
    public System.Runtime.Intrinsics.Vector256<double> ToVector128D2(double z = 0, double w = 0) => System.Runtime.Intrinsics.Vector256.Create(X.Value, Y.Value, z, w);

    /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="CoordinateSystems.CartesianCoordinate"/> (with 2 (x, y) + 2 optional components).</summary>
    public System.Runtime.Intrinsics.Vector256<double> ToVector256D2(double z = 0, double w = 0) => System.Runtime.Intrinsics.Vector256.Create(X.Value, Y.Value, z, w);

    /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="CoordinateSystems.CartesianCoordinate"/> (with 3 (x, y, z) + 1 optional component).</summary>
    public System.Runtime.Intrinsics.Vector256<double> ToVector256D3(double w = 0) => System.Runtime.Intrinsics.Vector256.Create(X.Value, Y.Value, Z.Value, w);

    #region Implemented interfaces

    #region IValueQuantifiable<>

    public System.Runtime.Intrinsics.Vector256<double> Value => m_v;

    #endregion // IValueQuantifiable<>

    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{X.Value.ToString(format, formatProvider)}, {Y.Value.ToString(format, formatProvider)}, {Z.Value.ToString(format, formatProvider)}, {W.Value.ToString(format, formatProvider)}>";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
