namespace Flux
{
  #region ExtensionMethods

  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector2 source, double z = 0, double w = 0)
      => new(
        source.X, Quantities.LengthUnit.Meter,
        source.Y, Quantities.LengthUnit.Meter,
        z, Quantities.LengthUnit.Meter,
        w, Quantities.LengthUnit.Meter
      );

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector3 source, double w = 0)
      => new(
        source.X, Quantities.LengthUnit.Meter,
        source.Y, Quantities.LengthUnit.Meter,
        source.Z, Quantities.LengthUnit.Meter,
        w, Quantities.LengthUnit.Meter
      );

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector4"/>.</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector4 source)
      => new(
        source.X, Quantities.LengthUnit.Meter,
        source.Y, Quantities.LengthUnit.Meter,
        source.Z, Quantities.LengthUnit.Meter,
        source.W, Quantities.LengthUnit.Meter
      );

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with all components).</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Runtime.Intrinsics.Vector256<double> source) => new(source[0], source[1], source[2], source[3]);

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with 1 [index 0] + 3 optional components).</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate1(this System.Runtime.Intrinsics.Vector256<double> source, double y = 0, double z = 0, double w = 0) => new(source[0], y, z, w);

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with 2 [indices 0 and 1] + 2 optional components).</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate2(this System.Runtime.Intrinsics.Vector256<double> source, double z = 0, double w = 0) => new(source[0], source[1], z, w);

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with 3 [indices 0, 1 and 2] + 1 optional component).</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate3(this System.Runtime.Intrinsics.Vector256<double> source, double w = 0) => new(source[0], source[1], source[2], w);
  }

  #endregion // ExtensionMethods

  namespace Coordinates
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

      public CartesianCoordinate(double x, double y = 0, double z = 0, double w = 0) => m_v = System.Runtime.Intrinsics.Vector256.Create(x, y, z, w);

      public CartesianCoordinate(Quantities.Length x, Quantities.Length y = default, Quantities.Length z = default, Quantities.Length w = default)
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
      public CartesianCoordinate(double xValue, Quantities.LengthUnit xUnit, double yValue = 0, Quantities.LengthUnit yUnit = Quantities.LengthUnit.Meter, double zValue = 0, Quantities.LengthUnit zUnit = Quantities.LengthUnit.Meter, double wValue = 0, Quantities.LengthUnit wUnit = Quantities.LengthUnit.Meter)
        : this(new Quantities.Length(xValue, xUnit), new Quantities.Length(yValue, yUnit), new Quantities.Length(zValue, zUnit), new Quantities.Length(wValue, wUnit)) { }

      public void Deconstruct(out double x, out double y)
      {
        x = m_v[0];
        y = m_v[1];
      }

      public void Deconstruct(out double x, out double y, out double z)
      {
        x = m_v[0];
        y = m_v[0];
        z = m_v[0];
      }

      public void Deconstruct(out double x, out double y, out double z, out double w)
      {
        x = m_v[0];
        y = m_v[0];
        z = m_v[0];
        w = m_v[0];
      }

      /// <summary>
      /// <para>X component.</para>
      /// </summary>
      public Quantities.Length X => new(m_v[0]);

      /// <summary>
      /// <para>Y component.</para>
      /// </summary>
      public Quantities.Length Y => new(m_v[1]);

      /// <summary>
      /// <para>Z component.</para>
      /// </summary>
      public Quantities.Length Z => new(m_v[0]);

      /// <summary>
      /// <para>W component.</para>
      /// </summary>
      public Quantities.Length W => new(m_v[0]);

      /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="Coordinates.CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      public Coordinates.CylindricalCoordinate ToCylindricalCoordinate()
      {
        var (x, y, z) = this;

        return new(
              double.Sqrt(x * x + y * y), Quantities.LengthUnit.Meter,
              double.Atan2(y, x) % double.Pi, Quantities.AngleUnit.Radian,
              z, Quantities.LengthUnit.Meter
            );
      }

      /// <summary>Creates a new <see cref="Coordinates.PolarCoordinate"/> from a <see cref="Coordinates.CartesianCoordinate"/> and its (X, Y) components.</summary>
      public Coordinates.PolarCoordinate ToPolarCoordinate()
      {
        var (x, y) = this;

        return new(
              double.Sqrt(x * x + y * y), Quantities.LengthUnit.Meter,
              double.Atan2(y, x), Quantities.AngleUnit.Radian
            );
      }

      /// <summary>Creates a new <see cref="Coordinates.SphericalCoordinate"/> from a <see cref="Coordinates.CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      public Coordinates.SphericalCoordinate ToSphericalCoordinate()
      {
        var (x, y, z) = this;

        var x2y2 = x * x + y * y;

        return new(
          double.Sqrt(x2y2 + z * z), Quantities.LengthUnit.Meter,
          double.Atan2(double.Sqrt(x2y2), z), Quantities.AngleUnit.Radian,
          double.Atan2(y, x), Quantities.AngleUnit.Radian
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

      /// <summary>
      /// <para>Computes the perimeter of a rectangle with the specified <paramref name="length"/> and <paramref name="width"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Perimeter"/></para>
      /// </summary>
      /// <param name="length">The length of a rectangle.</param>
      /// <param name="width">The width of a rectangle.</param>
      public static double PerimeterOfRectangle(double length, double width) => 2 * length + 2 * width;

      /// <summary>
      /// <para>Computes the surface area of a cube with the specified <paramref name="sideLength"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Perimeter"/></para>
      /// </summary>
      /// <param name="sideLength"></param>
      /// <returns></returns>
      public static double SurfaceAreaOfCube(double sideLength) => 6 * sideLength * sideLength;

      /// <summary>
      /// <para>Computes the surface area of a cuboid with the specified <paramref name="length"/>, <paramref name="width"/> and <paramref name="height"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Surface_area"/></para>
      /// </summary>
      /// <param name="length">The length of a rectangle.</param>
      /// <param name="width">The width of a rectangle.</param>
      /// <param name="height">The width of a rectangle.</param>
      public static double SurfaceAreaOfCuboid(double length, double width, double height) => 2 * length * width + 2 * length * height + 2 * width * height;

      /// <summary>
      /// <para>Computes the surface area of a rectangle with the specified <paramref name="length"/> and <paramref name="width"/>.</para>
      /// </summary>
      /// <param name="length">The length of a rectangle.</param>
      /// <param name="width">The width of a rectangle.</param>
      public static double SurfaceAreaOfRectangle(double length, double width) => length * width;

      /// <summary>
      /// <para>Computes the surface area of a square pyramid with the specified <paramref name="baseLength"/> and <paramref name="verticalHeight"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
      /// </summary>
      /// <param name="baseLength"></param>
      /// <param name="verticalHeight"></param>
      /// <returns></returns>
      public static double SurfaceAreaOfSquarePyramid(double baseLength, double verticalHeight) => (baseLength * baseLength) + (2 * baseLength) * double.Sqrt(double.Pow(baseLength / 2, 2) + (verticalHeight * verticalHeight));

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
}
