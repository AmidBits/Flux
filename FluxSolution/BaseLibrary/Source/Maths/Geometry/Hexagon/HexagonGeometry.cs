using System.Linq;

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
    /// <summary>This is the ratio relashionship of maxima to minima.</summary>
    private static readonly double Ratio = System.Math.Sqrt(3) / 2;

    private readonly double m_sideLength;

    public HexagonGeometry(double sideLength) => m_sideLength = sideLength;

    public double SideLength => m_sideLength;

    public double MaximalRadius => m_sideLength;

    public double MinimalRadius => Ratio * m_sideLength;

    /// <summary>Calculates the surface area for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public double SurfaceArea => 3 * Ratio * m_sideLength * m_sideLength;

    /// <summary>Calculates the surface perimeter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public double SurfacePerimeter => m_sideLength * 6;

    #region Static methods

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
      => centeredHexagonalNumber > 0 ? (3 + (int)(System.Math.Sqrt(12 * centeredHexagonalNumber - 3))) / 6 : throw new System.ArgumentOutOfRangeException(nameof(centeredHexagonalNumber));

    /// <summary>
    /// <para>The number of hexagons in the "<paramref name="ring"/>".</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    /// <returns>The number of hexagons in the "<paramref name="ring"/>".</returns>
    /// <remarks>Ring is simply a 1-based index.</remarks>
    public static int GetHexagonCountOfRing(int ring)
      => ring == 1 ? 1 : ring > 1 ? ((ring - 1) * 6) : throw new System.ArgumentOutOfRangeException(nameof(ring));

    #endregion // Static methods
  }
}
