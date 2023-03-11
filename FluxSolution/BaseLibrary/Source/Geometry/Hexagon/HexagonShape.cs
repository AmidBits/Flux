using System.Linq;

namespace Flux.Geometry
{
  // https://en.wikipedia.org/wiki/Centered_hexagonal_number
  // https://hexnet.org/content/hex-numbers
  // https://www.redblobgames.com/grids/hexagons/
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record class HexagonShape
  {
    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    public static System.Collections.Generic.IEnumerable<int> GetCenteredNumbers(int count)
    {
      for (var nth = 1; nth <= count; nth++)
        yield return 3 * nth * (nth - 1) + 1;
    }

    /// <summary></summary>
    public static System.Collections.Generic.IEnumerable<(int centeredRing, int startCenteredNumber, int endCenteredNumber, int count)> GetCenteredRings(int count)
      => new (int, int, int, int)[] { (0, 1, 1, 1) }.Concat(GetCenteredNumbers(int.MaxValue).Take(count).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Select((n, i) => (i + 1, n.leading + 1, n.trailing, n.trailing - n.leading)));

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_hexagonal_number#Testing_/_finding_the_root"/>
    public static int GetCenteredRoot(int centeredNumber)
      => centeredNumber >= 1 ? (3 + (int)System.Math.Sqrt(12 * centeredNumber - 3)) / 6 : centeredNumber == 0 ? 0 : throw new System.ArgumentOutOfRangeException(nameof(centeredNumber));

    /// <summary></summary>
    public static int GetRingOf(int number)
      => (number > 0) ? GetCenteredRoot(number - 1) : throw new System.ArgumentOutOfRangeException(nameof(number));

    public static double RatioOfOuterToInnerDiameter => GenericMath.TheodorusConstant / 2.0;

    public const double SixtyDegreesInRadians = GenericMath.PiOver180 * 60.0;

    private readonly System.Numerics.Vector2[] m_points = new System.Numerics.Vector2[6];

    public HexagonShape(HexagonOrientation orientation, double outerDiameter = 1.0)
      => m_points = (orientation == HexagonOrientation.FlatTopped) ? new EllipseGeometry(outerDiameter, outerDiameter).CreateCircularArcPoints(6, (x, y) => new System.Numerics.Vector2((float)x, (float)y), Quantities.Angle.ConvertDegreeToRadian(90)).ToArray() : new EllipseGeometry(outerDiameter, outerDiameter).CreateCircularArcPoints(6, (x, y) => new System.Numerics.Vector2((float)x, (float)y)).ToArray();

    /// <summary>The six hexagon points.</summary>
    public System.Collections.Generic.IReadOnlyList<System.Numerics.Vector2> Points { get => m_points; }

    /// <summary>Creates an array with the vertices for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static System.Numerics.Vector2[] GetPoints(double length, double angleOffset)
      => new EllipseGeometry(length, length).CreateCircularArcPoints(6, (x, y) => new System.Numerics.Vector2((float)x, (float)y), angleOffset).ToArray();

    /// <summary>Calculates the surface area for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfaceArea(double length)
      => 3 * length * length * GenericMath.TheodorusConstant / 2.0;
    /// <summary>Calculates the surface inner diameter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfaceInnerDiameter(double length)
      => length * GenericMath.TheodorusConstant;
    /// <summary>Calculates the surface inner radius for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfaceInnerRadius(double length)
      => length * GenericMath.TheodorusConstant / 2.0;
    /// <summary>Calculates the surface outer diameter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfaceOuterDiameter(double length)
      => length * 2;
    /// <summary>Calculates the surface perimeter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfacePerimeter(double length)
      => length * 6;
  }
}
