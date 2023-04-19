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

    /// <summary>
    /// <para>Creates a new sequence of 6 <typeparamref name="TResult"/> in the <paramref name="orientation"/> specified with an optional <paramref name="radOffset"/>, <paramref name="maxRandomness"/> and <paramref name="rng"/>.</para>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="orientation">The orientation of the hexagon.</param>
    /// <param name="resultSelector">The selector that determines the result (<typeparamref name="TResult"/>) for each vector.</param>
    /// <param name="radOffset">The offset in radians to apply to each vector.</param>
    /// <param name="maxRandomness">The maximum randomness to allow for each vector. Must be in the range [0, 0.5].</param>
    /// <param name="rng">The random number generator to use, or default if null.</param>
    /// <returns>A new sequence of <typeparamref name="TResult"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public System.Collections.Generic.IEnumerable<TResult> CreateVectors<TResult>(HexagonOrientation orientation, System.Func<double, double, TResult> resultSelector, double radOffset = 0, double maxRandomness = 0, System.Random? rng = null)
    {
      var radOrientationOffset = orientation switch
      {
        HexagonOrientation.PointyTopped => 0,
        HexagonOrientation.FlatTopped => Units.Angle.ConvertDegreeToRadian(90),
        _ => throw new System.ArgumentOutOfRangeException(nameof(orientation))
      };

      return new CircleGeometry(m_sideLength).CreateVectors(6, (x, y) => resultSelector(x, y), radOrientationOffset + radOffset, maxRandomness, rng);
    }

    public CircleGeometry ToCircleGeometry() => new(m_sideLength);

    public EllipseGeometry ToEllipseGeometry() => new(m_sideLength, m_sideLength);

    #region Static methods

    /// <summary>Create a hexagon geometry by specifying a <paramref name="circumradius"/>, a.k.a. the maximal radius, or the side length of the hexagon.</summary>
    /// <param name="circumradius">The circumradius, or maximal radius, from which to create the hexagon geometry.</param>
    /// <returns></returns>
    public static HexagonGeometry FromCircumradius(double circumradius) => new(circumradius);

    /// <summary>Create a hexagon geometry by specifying a <paramref name="inradius"/>, a.k.a. the minimal radius.</summary>
    /// <param name="inradius">The inradius, or minimal radius, from which to create the hexagon geometry.</param>
    /// <returns></returns>
    public static HexagonGeometry FromInradius(double inradius) => new(inradius * 1 / Ratio);

    /// <summary>
    /// <para>Returns how many hexagons are with in <paramref name="nth"/> hex "rings".</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    /// <param name="nth"></param>
    /// <returns></returns>
    public static int GetCenteredNumber(int nth)
      => 3 * nth * (nth - 1) + 1;

    /// <summary>
    /// <para>Creates a new sequence with the centered hexagonal numbers for each "ring", up to <paramref name="count"/> rings, starting with Nth = 1.</para>
    /// <see href="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    /// </summary>
    public static System.Collections.Generic.IEnumerable<int> GetCenteredNumbers(int count)
    {
      for (var n = 1; n <= count; n++)
        yield return GetCenteredNumber(n);
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    public static System.Collections.Generic.IEnumerable<(int centeredRing, int startCenteredNumber, int endCenteredNumber, int count)> GetCenteredRings(int count)
      => new (int, int, int, int)[] { (0, 1, 1, 1) }.Concat(GetCenteredNumbers(int.MaxValue).Take(count).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Select((n, i) => (i + 1, n.leading + 1, n.trailing, n.trailing - n.leading)));

    /// <summary>
    /// <para>The inverse of getting the centered hexagonal number, i.e. find the root, or index, of the centered hexagonal number.</para>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_hexagonal_number#Testing_/_finding_the_root"/>
    /// </summary>
    public static int GetCenteredRoot(int centeredHexagonalNumber)
      => centeredHexagonalNumber >= 1 ? (3 + System.Convert.ToInt32(System.Math.Sqrt(12 * centeredHexagonalNumber - 3))) / 6 : centeredHexagonalNumber == 0 ? 0 : throw new System.ArgumentOutOfRangeException(nameof(centeredHexagonalNumber));

    /// <summary></summary>
    public static int GetRingOf(int centeredHexagonalNumber)
      => (centeredHexagonalNumber > 0) ? GetCenteredRoot(centeredHexagonalNumber - 1) : throw new System.ArgumentOutOfRangeException(nameof(centeredHexagonalNumber));

    #endregion // Static methods
  }
}
