using System.Linq;

namespace Flux.Media.Geometry
{
  public enum HexagonOrientation
  {
    FlatTopped,
    PointyTopped
  }

  struct HexagonLayout
  {
    public static double[] PointyCoordinates = new double[] { System.Math.Sqrt(3.0), System.Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, System.Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5 };
    public static double[] FlatTopCoordinates = new double[] { 3.0 / 2.0, 0.0, System.Math.Sqrt(3.0) / 2.0, System.Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, System.Math.Sqrt(3.0) / 3.0, 0.0 };

    public readonly HexagonOrientation Orientation;
    public readonly Size2 Size;
    public readonly Point2 Origin;

    public HexagonLayout(HexagonOrientation orientation, Size2 size, Point2 origin)
    {
      Orientation = orientation;
      Size = size;
      Origin = origin;
    }

    public readonly double[] OrientationCoordinates
      => Orientation switch
      {
        HexagonOrientation.FlatTopped => FlatTopCoordinates,
        HexagonOrientation.PointyTopped => PointyCoordinates,
        _ => throw new System.Exception()
      };
    //static public Orientation pointy = new Orientation(Math.Sqrt(3.0), Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
    //static public Orientation flat = new Orientation(3.0 / 2.0, 0.0, Math.Sqrt(3.0) / 2.0, Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, Math.Sqrt(3.0) / 3.0, 0.0);

    //public Flux.Maui.Graphics.Point HexToPixel(Hex h)
    //{
    //  Orientation M = orientation;
    //  double x = (M.f0 * h.q + M.f1 * h.r) * size.x;
    //  double y = (M.f2 * h.q + M.f3 * h.r) * size.y;
    //  return new Point(x + origin.x, y + origin.y);
    //}


    //public FractionalHex PixelToHex(Point p)
    //{
    //  Orientation M = orientation;
    //  Point pt = new Point((p.x - origin.x) / size.x, (p.y - origin.y) / size.y);
    //  double q = M.b0 * pt.x + M.b1 * pt.y;
    //  double r = M.b2 * pt.x + M.b3 * pt.y;
    //  return new FractionalHex(q, r, -q - r);
    //}


    //public Point HexCornerOffset(int corner)
    //{
    //  Orientation M = orientation;
    //  double angle = 2.0 * Math.PI * (M.start_angle - corner) / 6.0;
    //  return new Point(size.x * Math.Cos(angle), size.y * Math.Sin(angle));
    //}


    //public List<Point> PolygonCorners(Hex h)
    //{
    //  List<Point> corners = new List<Point> { };
    //  Point center = HexToPixel(h);
    //  for (int i = 0; i < 6; i++)
    //  {
    //    Point offset = HexCornerOffset(i);
    //    corners.Add(new Point(center.x + offset.x, center.y + offset.y));
    //  }
    //  return corners;
    //}

  }

  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hex
    : System.IEquatable<Hex>
  {
    private static Hex[] Directions = {
      new Hex(1, 0, -1),
      new Hex(1, -1, 0),
      new Hex(0, -1, 1),
      new Hex(-1, 0, 1),
      new Hex(-1, 1, 0),
      new Hex(0, 1, -1),
    };

    public static readonly Hex Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private int m_q;
    [System.Runtime.InteropServices.FieldOffset(4)] private int m_r;
    [System.Runtime.InteropServices.FieldOffset(8)] private int m_s;

    public Hex(int q, int r, int s)
    {
      m_q = q;
      m_r = r;
      m_s = s;
    }
    public Hex(int q, int r)
      : this(q, r, -q - r)
    { }

    public int Q { get => m_q; set => m_q = value; }
    public int R { get => m_r; set => m_r = value; }
    public int S { get => m_s; set => m_s = value; }

    #region Statics
    public static Hex Add(Hex a, Hex b)
      => new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);
    public static Hex Direction(int direction /* 0 to 5 */)
      => 0 <= direction && direction < 6 ? Directions[direction] : throw new System.ArgumentOutOfRangeException(nameof(direction));
    public static Hex Neighbor(Hex hex, int direction)
      => Add(hex, Direction(direction));
    public static int Distance(Hex a, Hex b)
      => Length(Subtract(a, b));
    public static Hex Subtract(Hex a, Hex b)
      => new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);
    public static int Length(Hex hex)
      => (System.Math.Abs(hex.Q) + System.Math.Abs(hex.R) + System.Math.Abs(hex.S)) / 2;
    public static Hex Multiply(Hex a, int k)
      => new Hex(a.Q * k, a.R * k, a.S * k);
    #endregion Statics

    // IEquatable
    public bool Equals(Hex other)
      => Q == other.Q && R == other.R && S == other.S;

    // Overrides
    public override bool Equals(object? obj)
      => obj is Hex o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Q, R, S);
    public override string ToString()
      => $"<{GetType().Name} {Q}, {R}, {S}>";
  }
  // https://en.wikipedia.org/wiki/Centered_hexagonal_number
  // https://hexnet.org/content/hex-numbers
  // https://www.redblobgames.com/grids/hexagons/
  public class Hexagon
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

    public Hexagon(HexagonOrientation orientation, double outerDiameter = 1.0)
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
