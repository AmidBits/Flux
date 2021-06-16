using System.Linq;

namespace Flux.Geometry
{
  public enum HexagonOrientation
  {
    FlatTopped,
    PointyTopped
  }

  // https://en.wikipedia.org/wiki/Centered_hexagonal_number
  // https://hexnet.org/content/hex-numbers
  // https://www.redblobgames.com/grids/hexagons/
  public class HexagonShape
  {
    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_hexagonal_number"/>
    public static System.Collections.Generic.IEnumerable<int> GetCenteredNumbers(int count)
    {
      for (var nth = 1; nth <= count; nth++)
      {
        yield return 3 * nth * (nth - 1) + 1;
      }
    }

    /// <summary></summary>
    public static System.Collections.Generic.IEnumerable<(int centeredRing, int startCenteredNumber, int endCenteredNumber, int count)> GetCenteredRings(int count)
      => GetCenteredNumbers(int.MaxValue).Take(count).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Select((n, i) => (i + 1, n.leading + 1, n.trailing, n.trailing - n.leading)).Prepend((0, 1, 1, 1));

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_hexagonal_number#Testing_/_finding_the_root"/>
    public static int GetCenteredRoot(int centeredNumber)
      => centeredNumber >= 1 ? (3 + (int)System.Math.Sqrt(12 * centeredNumber - 3)) / 6 : centeredNumber == 0 ? 0 : throw new System.ArgumentOutOfRangeException(nameof(centeredNumber));

    /// <summary></summary>
    public static int GetRingOf(int number)
      => (number > 0) ? GetCenteredRoot(number - 1) : throw new System.ArgumentOutOfRangeException(nameof(number));

    public const double RatioOfOuterToInnerDiameter = Maths.SquareRootOf3 / 2.0;

    public const double SixtyDegreesInRadians = System.Math.PI / 180 * 60.0;

    private readonly System.Numerics.Vector2[] m_points = new System.Numerics.Vector2[6];
    /// <summary>The six hexagon points.</summary>
    public System.Collections.Generic.IReadOnlyList<System.Numerics.Vector2> Points { get => m_points; }

    public HexagonShape(HexagonOrientation orientation, double outerDiameter = 1.0)
    {
      m_points = (orientation == HexagonOrientation.FlatTopped) ? Ellipse.CreateHexagon(outerDiameter, outerDiameter).ToArray() : Ellipse.CreateHexagon(outerDiameter, outerDiameter, 0.0).ToArray();
    }

    /// <summary>Creates an array with the vertices for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static System.Numerics.Vector2[] GetPoints(double length, double angleOffset) => Ellipse.CreateHexagon(length, length, angleOffset).ToArray();

    /// <summary>Calculates the surface area for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfaceArea(double length) => 3 * length * length * Maths.SquareRootOf3 / 2.0;
    /// <summary>Calculates the surface inner diameter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfaceInnerDiameter(double length) => length * Maths.SquareRootOf3;
    /// <summary>Calculates the surface inner radius for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfaceInnerRadius(double length) => length * Maths.SquareRootOf3 / 2.0;
    /// <summary>Calculates the surface outer diameter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfaceOuterDiameter(double length) => length * 2;
    /// <summary>Calculates the surface perimeter for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double SurfacePerimeter(double length) => length * 6;
  }
}
