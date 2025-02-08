namespace Flux.Geometry.Coordinates
{
  /// <summary>
  /// <para>Cartesian coordinate.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/></para>
  /// </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CartesianCoordinate
    : System.IFormattable, IValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>>
  {
    public static CartesianCoordinate UnitX { get; } = new(1d, 0d, 0d, 0d);
    public static CartesianCoordinate UnitY { get; } = new(0d, 1d, 0d, 0d);
    public static CartesianCoordinate UnitZ { get; } = new(0d, 0d, 1d, 0d);
    public static CartesianCoordinate UnitW { get; } = new(0d, 0d, 0d, 1d);

    private readonly System.Runtime.Intrinsics.Vector256<double> m_v;

    public CartesianCoordinate(System.Runtime.Intrinsics.Vector128<double> xy) => m_v = System.Runtime.Intrinsics.Vector256.Create(xy[0], xy[1], 0, 0);

    public CartesianCoordinate(System.Runtime.Intrinsics.Vector256<double> xyzw) => m_v = xyzw;

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

      return new(
            double.Sqrt(x * x + y * y), Units.LengthUnit.Meter,
            double.Atan2(y, x) % double.Pi, Units.AngleUnit.Radian,
            z, Units.LengthUnit.Meter
          );
    }

    public System.Drawing.Point ToPoint(UniversalRounding mode) => new(System.Convert.ToInt32(m_v[0].RoundUniversal(mode)), System.Convert.ToInt32(m_v[1].RoundUniversal(mode)));

    public System.Drawing.PointF ToPointF() => new((float)m_v[0], (float)m_v[1]);

    /// <summary>Creates a new <see cref="PolarCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y) components.</summary>
    public PolarCoordinate ToPolarCoordinate()
    {
      var (x, y) = this;

      return new(
            double.Sqrt(x * x + y * y), Units.LengthUnit.Meter,
            double.Atan2(y, x), Units.AngleUnit.Radian
          );
    }

    /// <summary>Creates a new <see cref="SphericalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
    public SphericalCoordinate ToSphericalCoordinate()
    {
      var (x, y, z) = this;

      var x2y2 = x * x + y * y;

      return new(
        double.Sqrt(x2y2 + z * z), Units.LengthUnit.Meter,
        double.Atan2(double.Sqrt(x2y2), z), Units.AngleUnit.Radian,
        double.Atan2(y, x), Units.AngleUnit.Radian
      );
    }

    /// <summary>Creates a new <see cref="System.Numerics.Vector2"/> from a <see cref="Coordinates.CartesianCoordinate"/>.</summary>
    public System.Numerics.Vector2 ToVector2()
    {
      var (x, y) = this;

      return new(
        (float)x,
        (float)y
      );
    }

    /// <summary>Creates a new <see cref="System.Numerics.Vector2"/> from a <see cref="Coordinates.CartesianCoordinate"/>.</summary>
    public System.Numerics.Vector3 ToVector3()
    {
      var (x, y, z) = this;

      return new(
        (float)x,
        (float)y,
        (float)z
      );
    }

    /// <summary>Creates a new <see cref="System.Numerics.Vector2"/> from a <see cref="Coordinates.CartesianCoordinate"/>.</summary>
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

    /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="Coordinates.CartesianCoordinate"/> (with 1 (x) + 3 optional components).</summary>
    public System.Runtime.Intrinsics.Vector256<double> ToVector128D2(double z = 0, double w = 0) => System.Runtime.Intrinsics.Vector256.Create(X.Value, Y.Value, z, w);

    /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="Coordinates.CartesianCoordinate"/> (with 2 (x, y) + 2 optional components).</summary>
    public System.Runtime.Intrinsics.Vector256<double> ToVector256D2(double z = 0, double w = 0) => System.Runtime.Intrinsics.Vector256.Create(X.Value, Y.Value, z, w);

    /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="Coordinates.CartesianCoordinate"/> (with 3 (x, y, z) + 1 optional component).</summary>
    public System.Runtime.Intrinsics.Vector256<double> ToVector256D3(double w = 0) => System.Runtime.Intrinsics.Vector256.Create(X.Value, Y.Value, Z.Value, w);

    #region Static methods

    ///// <summary>
    ///// <para>Compute the Chebyshev length (using the specified edgeLength) of the cartesian coordinates.</para>
    ///// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    ///// </summary>
    //public static TSelf ChebyshevLength<TSelf>(TSelf edgeLength, params TSelf[] cartesianCoordinates)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  var max = TSelf.Zero;

    //  for (var i = cartesianCoordinates.Length - 1; i >= 0; i--)
    //    if (TSelf.Abs(cartesianCoordinates[i]) is var current && current > max)
    //      max = current;

    //  return max / edgeLength;
    //}

    /// <summary>
    /// <para>Converts cartesian 2D (<paramref name="x"/>, <paramref name="y"/>) coordinates to a linear index of a grid with the <paramref name="width"/> (the length of the x-axis).</para>
    /// </summary>
    public static TSelf ConvertCartesian2ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width);

    /// <summary>
    /// <para>Converts cartesian 3D (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) coordinates to a linear index of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis).</para>
    /// </summary>
    public static TSelf ConvertCartesian3ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf z, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width) + (z * width * height);

    /// <summary>
    /// <para>Converts a <paramref name="linearIndex"/> of a grid with the <paramref name="width"/> (the length of the x-axis) to cartesian 2D (x, y) coordinates.</para>
    /// </summary>
    public static (TSelf x, TSelf y) ConvertLinearIndexToCartesian2<TSelf>(TSelf linearIndex, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
       => (
        linearIndex % width,
        linearIndex / width
      );

    /// <summary>
    /// <para>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis), to cartesian 3D (x, y, z) coordinates.</para>
    /// </summary>
    public static (TSelf x, TSelf y, TSelf z) ConvertLinearIndexToCartesian3<TSelf>(TSelf linearIndex, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var xy = width * height;
      var irxy = linearIndex % xy;

      return (
        irxy % width,
        irxy / width,
        linearIndex / xy
      );
    }

    ///// <summary>
    ///// <para>Compute the Manhattan length (using the specified edgeLength) of the cartesian coordinates.</para>
    ///// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    ///// </summary>
    //public static TSelf ManhattanLength<TSelf>(TSelf edgeLength, params TSelf[] cartesianCoordinates)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  var sum = TSelf.Zero;

    //  for (var i = cartesianCoordinates.Length - 1; i >= 0; i--)
    //    sum += TSelf.Abs(cartesianCoordinates[i]);

    //  return sum / edgeLength;
    //}

    #endregion // Static methods

    #region Implemented interfaces

    #region IValueQuantifiable<>

    public System.Runtime.Intrinsics.Vector256<double> Value => m_v;

    #endregion // IValueQuantifiable<>

    public string ToString(string? format, System.IFormatProvider? provider)
      => $"<{X.Value.ToString(format, provider)}, {Y.Value.ToString(format, provider)}, {Z.Value.ToString(format, provider)}, {W.Value.ToString(format, provider)}>";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
