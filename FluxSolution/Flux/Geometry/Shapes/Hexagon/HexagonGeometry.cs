namespace Flux.Geometry.Shapes.Hexagon
{
  /// <summary>
  /// <para>Hexagon geometry.</para>
  /// <see href="https://en.wikipedia.org/wiki/Hexagon"/>
  /// </summary>
  /// <remarks>The hexagon is essentially a specialized 6-point circle.</remarks>
  public readonly record struct HexagonGeometry
    : System.IFormattable
  {
    public const double CircleFillRatio = 0.8269933431326881;
    public static HexagonGeometry Unit { get; } = new(1);

    private readonly double m_sideLength;

    public HexagonGeometry(double sideLength) => m_sideLength = sideLength;

    /// <summary>
    /// <para>The circumradius of the hexagon.</para>
    /// </summary>
    public double Circumradius => m_sideLength;

    /// <summary>
    /// <para>The inradius of the hexagon.</para>
    /// </summary>
    public double Inradius => ConvertCircumradiusToInradius(m_sideLength);

    /// <summary>
    /// <para>The perimeter of the hexagon.</para>
    /// </summary>
    public double Perimeter => Units.Length.PerimeterOfHexagon(m_sideLength);

    /// <summary>
    /// <para>The side-length of the hexagon, which is equal to the circumradius.</para>
    /// </summary>
    public double SideLength => m_sideLength;

    /// <summary>
    /// <para>The surface area of the hexagon.</para>
    /// </summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public double SurfaceArea => Units.Area.OfHexagon(m_sideLength);

    /// <summary>
    /// <para>Verify whether the point (<paramref name="x"/>, <paramref name="y"/>) is inside the hexagon, assuming (0, 0) is the center of the hexagon.</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Contains(double x, double y) => PointInHexagon(m_sideLength, x, y);

    public Polygon.RegularPolygon ToPolygonFlatTopped() => Polygon.RegularPolygon.Create(6, m_sideLength, double.Pi / 2);

    public Polygon.RegularPolygon ToPolygonPointyTopped() => Polygon.RegularPolygon.Create(6, m_sideLength);

    #region Static methods

    #region Conversion methods

    /// <summary>
    /// <para>Compute the hexagon circumradius from its <paramref name="inradius"/>.</para>
    /// </summary>
    /// <param name="inradius"></param>
    /// <returns></returns>
    /// <remarks>The inradius is also known as minimal radius.</remarks>
    public static double ConvertInradiusToCircumradius(double inradius) => inradius * 2 / double.Sqrt(3);

    /// <summary>
    /// <para>Compute the hexagon inradius from its <paramref name="circumradius"/>.</para>
    /// </summary>
    /// <param name="circumradius"></param>
    /// <returns></returns>
    /// <remarks>The circumradius, or maximal radius, is equal to the side-length of a hexagon.</remarks>
    public static double ConvertCircumradiusToInradius(double circumradius) => circumradius * double.Sqrt(3) / 2;

    #endregion // Conversion methods

    public static System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> CreatePointsOfHexagon(double circumradius = 1, double arcOffset = 0, double translateX = 0, double translateY = 0)
      => Ellipse.EllipseGeometry.CreatePointsOnEllipse(6, circumradius, circumradius, arcOffset, translateX, translateY);

    /// <summary>
    /// <para>Find the centered hexagonal number by index. This is the number of hexagons in the "ring" represented by <paramref name="index"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    /// <returns>The centered hexagonal number corresponding to the <paramref name="index"/>.</returns>
    /// <remarks>Indexing of the centered hexagonal number is 1-based. Index is also referred to as "ring".</remarks>
    public static int CenteredHexagonalNumber(int index)
      => index > 0 ? 3 * index * (index - 1) + 1 : throw new System.ArgumentOutOfRangeException(nameof(index));

    /// <summary>
    /// <para>The number of hexagons in the "<paramref name="ring"/>".</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    /// <returns>The number of hexagons in the "<paramref name="ring"/>".</returns>
    /// <remarks>Ring is simply a 1-based index.</remarks>
    public static int GetHexagonCountOfRing(int ring)
      => ring == 1 ? 1 : ring > 1 ? ((ring - 1) * 6) : throw new System.ArgumentOutOfRangeException(nameof(ring));

    /// <summary>
    /// <para>The inverse of the centered hexagonal number, i.e. find the index of the centered hexagonal number.</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    /// <remarks>Indexing of the centered hexagonal number is 1-based. Index is also referred to as "ring".</remarks>
    public static int IndexOfCenteredHexagonalNumber(int centeredHexagonalNumber)
      => centeredHexagonalNumber > 0 ? (3 + (int)double.Sqrt(12 * centeredHexagonalNumber - 3)) / 6 : throw new System.ArgumentOutOfRangeException(nameof(centeredHexagonalNumber));

    /// <summary>
    /// <para>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the hexagon with the specified <paramref name="sideLength"/>.</para>
    /// <see href="http://www.playchilla.com/how-to-check-if-a-point-is-inside-a-hexagon#:~:text=The%20intuitive%20way%20to%20check%20if%20a%20point,corners%20the%20point%20is%20indeed%20within%20the%20hexagon."/>
    /// </summary>
    /// <param name="sideLength"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool PointInHexagon(double sideLength, double x, double y)
    {
      var q2x = double.Abs(x);         // transform the test point locally and to quadrant 2
      var q2y = double.Abs(y);         // transform the test point locally and to quadrant 2

      var v = sideLength;
      var h = 2 * sideLength * double.Cos(30);

      if (q2x > h || q2y > v * 2) return false;           // bounding test (since q2 is in quadrant 2 only 2 tests are needed)

      return (2 * v * h) - (v * q2x) - (h * q2y) >= 0;   // finally the dot product can be reduced to this due to the hexagon symmetry
    }

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N3";

      return $"{GetType().Name} {{ SideLength = {SideLength.ToString(format, formatProvider)}, Circumradius = {Circumradius.ToString(format, formatProvider)}, Inradius = {Inradius.ToString(format, formatProvider)}, Perimeter = {Perimeter.ToString(format, formatProvider)}, SurfaceArea = {SurfaceArea.ToString(format, formatProvider)} }}";
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
