namespace Flux.Geometry
{
  /// <summary>
  /// <para>Hexagon geometry.</para>
  /// <see href="https://en.wikipedia.org/wiki/Hexagon"/>
  /// </summary>
  /// <remarks>The hexagon is essentially a specialized 6-point circle.</remarks>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record class HexagonGeometry
  {
    private readonly double m_sideLength;

    public HexagonGeometry(double sideLength) => m_sideLength = sideLength;

    public double SideLength => m_sideLength;

    public double Circumradius => m_sideLength;

    public double Inradius => m_sideLength * System.Math.Sqrt(3) / 2;

    /// <summary>Calculates the surface area for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public double SurfaceArea => AreaOfHexagon(m_sideLength);

    /// <summary>Calculates the surface perimeter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public double SurfacePerimeter => PerimeterOfHexagon(m_sideLength);

    public System.Numerics.Vector2[] FlatTopped => CircleGeometry.CreateVectors(6, (x, y) => new System.Numerics.Vector2((float)x, (float)y), m_sideLength, double.DegreesToRadians(90)).ToArray();

    public System.Numerics.Vector2[] PointyTopped => CircleGeometry.CreateVectors(6, (x, y) => new System.Numerics.Vector2((float)x, (float)y), m_sideLength).ToArray();

    #region Static methods

    /// <summary>Calculates the surface area for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double AreaOfHexagon(double sideLength) => 3 * (System.Math.Sqrt(3) / 2) * sideLength * sideLength;

    /// <summary>
    /// <para>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the hexagon with the specified <paramref name="sideLength"/>.</para>
    /// <see href="http://www.playchilla.com/how-to-check-if-a-point-is-inside-a-hexagon#:~:text=The%20intuitive%20way%20to%20check%20if%20a%20point,corners%20the%20point%20is%20indeed%20within%20the%20hexagon."/>
    /// </summary>
    /// <param name="sideLength"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool ContainsPoint(double sideLength, double x, double y)
    {
      var q2x = System.Math.Abs(x);         // transform the test point locally and to quadrant 2
      var q2y = System.Math.Abs(y);         // transform the test point locally and to quadrant 2

      var v = sideLength;
      var h = 2 * sideLength * System.Math.Cos(30);

      if (q2x > h || q2y > v * 2) return false;           // bounding test (since q2 is in quadrant 2 only 2 tests are needed)

      return (2 * v * h) - (v * q2x) - (h * q2y) >= 0;   // finally the dot product can be reduced to this due to the hexagon symmetry
    }

    /// <summary>
    /// <para>Find the centered hexagonal number by index. This is the number of hexagons in the "ring" represented by <paramref name="index"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    /// <returns>The centered hexagonal number corresponding to the <paramref name="index"/>.</returns>
    /// <remarks>Indexing of the centered hexagonal number is 1-based. Index is also referred to as "ring".</remarks>
    public static int CenteredHexagonalNumber(int index)
      => index > 0 ? 3 * index * (index - 1) + 1 : throw new System.ArgumentOutOfRangeException(nameof(index));

    /// <summary>
    /// <para>Compute the hexagon circumradius from the <paramref name="inradius"/>.</para>
    /// </summary>
    /// <param name="inradius"></param>
    /// <returns></returns>
    /// <remarks>The inradius is also known as minimal radius.</remarks>
    public static double ComputeCircumradius(double inradius)
      => inradius * 2 / System.Math.Sqrt(3);

    /// <summary>
    /// <para>Compute the hexagon inradius from the <paramref name="circumradius"/>.</para>
    /// </summary>
    /// <param name="circumradius"></param>
    /// <returns></returns>
    /// <remarks>The circumradius, or maximal radius, is equal to the side-length of a hexagon.</remarks>
    public static double ComputeInradius(double circumradius)
      => circumradius * System.Math.Sqrt(3) / 2;

    /// <summary>
    /// <para>The inverse of the centered hexagonal number, i.e. find the index of the centered hexagonal number.</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    /// <remarks>Indexing of the centered hexagonal number is 1-based. Index is also referred to as "ring".</remarks>
    public static int IndexOfCenteredHexagonalNumber(int centeredHexagonalNumber)
        => centeredHexagonalNumber > 0 ? (3 + (int)System.Math.Sqrt(12 * centeredHexagonalNumber - 3)) / 6 : throw new System.ArgumentOutOfRangeException(nameof(centeredHexagonalNumber));

    /// <summary>
    /// <para>The number of hexagons in the "<paramref name="ring"/>".</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    /// <returns>The number of hexagons in the "<paramref name="ring"/>".</returns>
    /// <remarks>Ring is simply a 1-based index.</remarks>
    public static int GetHexagonCountOfRing(int ring)
      => ring == 1 ? 1 : ring > 1 ? ((ring - 1) * 6) : throw new System.ArgumentOutOfRangeException(nameof(ring));

    /// <summary>Calculates the surface perimeter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double PerimeterOfHexagon(double sideLength) => sideLength * 6;

    #endregion // Static methods
  }
}
