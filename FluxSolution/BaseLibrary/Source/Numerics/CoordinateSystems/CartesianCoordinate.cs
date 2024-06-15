namespace Flux
{
  #region ExtensionMethods

  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector2 source, double z = 0, double w = 0)
      => new(
        source.X, Quantities.LengthUnit.Metre,
        source.Y, Quantities.LengthUnit.Metre,
        z, Quantities.LengthUnit.Metre,
        w, Quantities.LengthUnit.Metre
      );

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector3 source, double w = 0)
      => new(
        source.X, Quantities.LengthUnit.Metre,
        source.Y, Quantities.LengthUnit.Metre,
        source.Z, Quantities.LengthUnit.Metre,
        w, Quantities.LengthUnit.Metre
      );

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector4"/>.</summary>
    public static Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector4 source)
      => new(
        source.X, Quantities.LengthUnit.Metre,
        source.Y, Quantities.LengthUnit.Metre,
        source.Z, Quantities.LengthUnit.Metre,
        source.W, Quantities.LengthUnit.Metre
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
      : System.IFormattable
    {
      public static readonly CartesianCoordinate Zero;

      public static readonly CartesianCoordinate UnitX = new(1d, 0d, 0d, 0d);
      public static readonly CartesianCoordinate UnitY = new(0d, 1d, 0d, 0d);
      public static readonly CartesianCoordinate UnitZ = new(0d, 0d, 1d, 0d);
      public static readonly CartesianCoordinate UnitW = new(0d, 0d, 0d, 1d);

      private readonly Quantities.Length m_x;
      private readonly Quantities.Length m_y;
      private readonly Quantities.Length m_z;
      private readonly Quantities.Length m_w;

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
      public CartesianCoordinate(double xValue, Quantities.LengthUnit xUnit, double yValue, Quantities.LengthUnit yUnit, double zValue, Quantities.LengthUnit zUnit, double wValue, Quantities.LengthUnit wUnit)
      {
        m_x = new(xValue, xUnit);
        m_y = new(yValue, yUnit);
        m_z = new(zValue, zUnit);
        m_w = new(wValue, wUnit);
      }

      /// <summary>
      /// <para>Initialize as a 3D cartesian coordinate with the W component = 0.</para>
      /// </summary>
      /// <param name="xValue"></param>
      /// <param name="xUnit"></param>
      /// <param name="yValue"></param>
      /// <param name="yUnit"></param>
      /// <param name="zValue"></param>
      /// <param name="zUnit"></param>
      public CartesianCoordinate(double xValue, Quantities.LengthUnit xUnit, double yValue, Quantities.LengthUnit yUnit, double zValue, Quantities.LengthUnit zUnit)
        : this(xValue, xUnit, yValue, yUnit, zValue, zUnit, 0, Quantities.LengthUnit.Metre) { }

      /// <summary>
      /// <para>Initialize as a 2D cartesian coordinate with the Z and W components = 0.</para>
      /// </summary>
      /// <param name="xValue"></param>
      /// <param name="xUnit"></param>
      /// <param name="yValue"></param>
      /// <param name="yUnit"></param>
      public CartesianCoordinate(double xValue, Quantities.LengthUnit xUnit, double yValue, Quantities.LengthUnit yUnit)
        : this(xValue, xUnit, yValue, yUnit, 0, Quantities.LengthUnit.Metre, 0, Quantities.LengthUnit.Metre) { }

      /// <summary>
      /// <para>Initialize as a 1D cartesian coordinate with the Y, Z and W components = 0.</para>
      /// </summary>
      /// <param name="xValue"></param>
      /// <param name="xUnit"></param>
      /// <param name="yValue"></param>
      /// <param name="yUnit"></param>
      public CartesianCoordinate(double xValue, Quantities.LengthUnit xUnit)
        : this(xValue, xUnit, 0, Quantities.LengthUnit.Metre, 0, Quantities.LengthUnit.Metre, 0, Quantities.LengthUnit.Metre) { }

      public CartesianCoordinate(double x, double y, double z, double w) : this(x, Quantities.LengthUnit.Metre, y, Quantities.LengthUnit.Metre, z, Quantities.LengthUnit.Metre, w, Quantities.LengthUnit.Metre) { }

      public CartesianCoordinate(double x, double y, double z) : this(x, Quantities.LengthUnit.Metre, y, Quantities.LengthUnit.Metre, z, Quantities.LengthUnit.Metre) { }

      public CartesianCoordinate(double x, double y) : this(x, Quantities.LengthUnit.Metre, y, Quantities.LengthUnit.Metre) { }

      public CartesianCoordinate(double x) : this(x, Quantities.LengthUnit.Metre) { }

      public void Deconstruct(out double x, out double y)
      {
        x = m_x.Value;
        y = m_y.Value;
      }

      public void Deconstruct(out double x, out double y, out double z)
      {
        x = m_x.Value;
        y = m_y.Value;
        z = m_z.Value;
      }

      public void Deconstruct(out double x, out double y, out double z, out double w)
      {
        x = m_x.Value;
        y = m_y.Value;
        z = m_z.Value;
        w = m_w.Value;
      }

      /// <summary>
      /// <para>X component.</para>
      /// </summary>
      public Quantities.Length X { get => m_x; init => m_x = value; }

      /// <summary>
      /// <para>Y component.</para>
      /// </summary>
      public Quantities.Length Y { get => m_y; init => m_y = value; }

      /// <summary>
      /// <para>Z component.</para>
      /// </summary>
      public Quantities.Length Z { get => m_z; init => m_z = value; }

      /// <summary>
      /// <para>W component.</para>
      /// </summary>
      public Quantities.Length W { get => m_w; init => m_w = value; }

      /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="Coordinates.CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      public Coordinates.CylindricalCoordinate ToCylindricalCoordinate()
      {
        var (x, y, z) = this;

        return new(
              System.Math.Sqrt(x * x + y * y), Quantities.LengthUnit.Metre,
              (System.Math.Atan2(y, x) + System.Math.Tau) % System.Math.Tau, Quantities.AngleUnit.Radian,
              z, Quantities.LengthUnit.Metre
            );
      }

      /// <summary>Creates a new <see cref="Coordinates.PolarCoordinate"/> from a <see cref="Coordinates.CartesianCoordinate"/> and its (X, Y) components.</summary>
      public Coordinates.PolarCoordinate ToPolarCoordinate()
      {
        var (x, y) = this;

        return new(
              System.Math.Sqrt(x * x + y * y), Quantities.LengthUnit.Metre,
              System.Math.Atan2(y, x), Quantities.AngleUnit.Radian
            );
      }

      /// <summary>Creates a new <see cref="Coordinates.SphericalCoordinate"/> from a <see cref="Coordinates.CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      public Coordinates.SphericalCoordinate ToSphericalCoordinate()
      {
        var (x, y, z) = this;

        var x2y2 = x * x + y * y;

        return new(
          System.Math.Sqrt(x2y2 + z * z), Quantities.LengthUnit.Metre,
          System.Math.Atan2(System.Math.Sqrt(x2y2), z) /*+ System.Math.PI*/, Quantities.AngleUnit.Radian,
          System.Math.Atan2(y, x) /*+ System.Math.PI*/, Quantities.AngleUnit.Radian
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

      /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="Coordinates.CartesianCoordinate"/> (with all 4 components).</summary>
      public System.Runtime.Intrinsics.Vector256<double> ToVector256() => System.Runtime.Intrinsics.Vector256.Create(m_x.Value, m_y.Value, m_z.Value, m_w.Value);

      /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="Coordinates.CartesianCoordinate"/> (with 1 (x) + 3 optional components).</summary>
      public System.Runtime.Intrinsics.Vector256<double> ToVector256WithX(double y = 0, double z = 0, double w = 0) => System.Runtime.Intrinsics.Vector256.Create(m_x.Value, y, z, w);

      /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="Coordinates.CartesianCoordinate"/> (with 2 (x, y) + 2 optional components).</summary>
      public System.Runtime.Intrinsics.Vector256<double> ToVector256WithXY(double z = 0, double w = 0) => System.Runtime.Intrinsics.Vector256.Create(m_x.Value, m_y.Value, z, w);

      /// <summary>Creates a new <see cref="System.Runtime.Intrinsics.Vector256{T}"/> from a <see cref="Coordinates.CartesianCoordinate"/> (with 3 (x, y, z) + 1 optional component).</summary>
      public System.Runtime.Intrinsics.Vector256<double> ToVector256WithXYZ(double w = 0) => System.Runtime.Intrinsics.Vector256.Create(m_x.Value, m_y.Value, m_z.Value, w);

      #region Static methods

      /// <summary>
      /// <para>Compute the Chebyshev length (using the specified edgeLength) of the cartesian coordinates.</para>
      /// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
      /// </summary>
      public static TSelf ChebyshevLength<TSelf>(TSelf edgeLength, params TSelf[] cartesianCoordinates)
        where TSelf : System.Numerics.INumber<TSelf>
      {
        var max = TSelf.Zero;

        for (var i = cartesianCoordinates.Length - 1; i >= 0; i--)
          if (TSelf.Abs(cartesianCoordinates[i]) is var current && current > max)
            max = current;

        return max / edgeLength;
      }

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

      /// <summary>
      /// <para>Compute the Manhattan length (using the specified edgeLength) of the cartesian coordinates.</para>
      /// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
      /// </summary>
      public static TSelf ManhattanLength<TSelf>(TSelf edgeLength, params TSelf[] cartesianCoordinates)
        where TSelf : System.Numerics.INumber<TSelf>
      {
        var sum = TSelf.Zero;

        for (var i = cartesianCoordinates.Length - 1; i >= 0; i--)
          sum += TSelf.Abs(cartesianCoordinates[i]);

        return sum / edgeLength;
      }

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

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"<{m_x.Value.ToString(format, provider)}, {m_y.Value.ToString(format, provider)}, {m_z.Value.ToString(format, provider)}, {m_w.Value.ToString(format, provider)}>";

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
