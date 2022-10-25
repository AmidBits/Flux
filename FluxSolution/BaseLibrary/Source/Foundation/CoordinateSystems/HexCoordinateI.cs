namespace Flux
{
  /// <summary>The Hex coordinate system used is the Cube coordinate, and can be specified using </summary>
  /// <see href="https://www.redblobgames.com/grids/hexagons/"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record struct HexCoordinateI
    : IHexCoordinate<int>
  {
    public static readonly HexCoordinateI Zero;

    /// <summary>In counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry).</summary>
    public static HexCoordinateI[] Diagonals
      => new HexCoordinateI[] {
        new(2, -1, -1),
        new(1, -2, 1),
        new(-1, -1, 2),
        new(-2, 1, 1),
        new(-1, 2, -1),
        new(1, 1, -2)
      };

    /// <summary>In counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry).</summary>
    public static HexCoordinateI[] Directions
      => new HexCoordinateI[] {
        new(1, 0, -1),
        new(1, -1, 0),
        new(0, -1, 1),
        new(-1, 0, 1),
        new(-1, 1, 0),
        new(0, 1, -1),
      };

    private readonly int m_q;
    private readonly int m_r;
    private readonly int m_s;

    public HexCoordinateI(int q, int r, int s)
    {
      AssertCubeCoordinate(q, r, s);

      m_q = q;
      m_r = r;
      m_s = s;
    }
    public HexCoordinateI(int q, int r)
      : this(q, r, -q - r)
    { }

    [System.Diagnostics.Contracts.Pure] public int Q { get => m_q; init => m_q = value; }
    [System.Diagnostics.Contracts.Pure] public int R { get => m_r; init => m_r = value; }
    [System.Diagnostics.Contracts.Pure] public int S { get => m_s; init => m_s = value; }

    /// <summary>Returns the diagonal neighbor two cells over on-the-line and in-between two adjacent cells.</summary>
    /// <param name="direction">The hexagon direction [-5, 5] (either direction).</param>
    public HexCoordinateI DiagonalNeighbor(int direction)
      => this + Diagonal(direction);

    /// <summary>Creates a new sequence of the surrounding neighbors of the specified center hex (excluded in the sequence).</summary>
    /// <param name="center">The center reference hex.</param>
    public System.Collections.Generic.IEnumerable<HexCoordinateI> GetNeighbors()
    {
      foreach (var hex in Directions)
        yield return this + hex;
    }

    /// <summary>Creates a new sequence of all (including the specified center) hex cubes within the specified radius (inclusive).</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">The radius from the center reference hex.</param>
    public System.Collections.Generic.IEnumerable<HexCoordinateI> GetRange(int radius)
    {
      for (var q = -radius; q <= radius; q++)
        for (int r = System.Math.Max(-radius, -q - radius), rei = System.Math.Min(radius, -q + radius); r <= rei; r++)
          yield return new HexCoordinateI(m_q + q, m_r + r);
    }

    /// <summary>Create a new sequence of the hex cubes making up the ring at the radius from the center hex, starting at the specified (directional) cornerIndex.</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">[0,]</param>
    /// <param name="startDirection">In the range [0, 6]. The default is 0.</param>
    /// <param name="isCounterClockWise">Determines whether to enumerate counter-clockwise or not. The default is clockwise.</param>
    public System.Collections.Generic.IEnumerable<HexCoordinateI> GetRing(int radius, int startDirection = 0, bool isCounterClockWise = false)
    {
      if (startDirection < 0 || startDirection >= 6) throw new System.ArgumentOutOfRangeException(nameof(startDirection));

      if (radius < 0) throw new System.ArgumentOutOfRangeException(nameof(radius));
      else if (radius > 0)
      {
        var deltaMultiplier = isCounterClockWise ? -1 : 1; // Determines the sign of the delta direction as a multiplier.

        var corner = this + Direction(startDirection) * radius; // Find the first corner hex, relative center in direction of choice (plus the length of the radius).
        var deltaDirection = (startDirection + 2 * deltaMultiplier) % 6; // Set initial delta direction.

        for (var index = 0; index < 6; index++)
        {
          yield return corner;

          for (var deltaIndex = 1; deltaIndex < radius; deltaIndex++) // Enumerate the 'side of the current corner hex'.
            yield return corner + Direction(deltaDirection) * deltaIndex; // Compute the direction and offset of the side.

          corner = isCounterClockWise ? corner.NextCornerCcw() : corner.NextCornerCw(); // Locate the next corner hex.
          deltaDirection = (deltaDirection + 1 * deltaMultiplier) % 6; // Set next delta direction (i.e. rotate clockwise one 'turn').
        }
      }
      else yield return this;
    }

    public int Length()
      => (System.Math.Abs(m_q) + System.Math.Abs(m_r) + System.Math.Abs(m_s)) / 2;

    /// <summary>Returns the neighbor of the specified hex and direction.</summary>
    /// <param name="direction">The hexagon direction [-5, 5] (either direction).</param>
    /// <returns>The neighbor of the reference hex.</returns>
    public HexCoordinateI Neighbor(int direction)
      => this + Direction(direction);

    /// <summary>Returns the next corner hex in a clockwise direction on the same ring as the specified 'corner' hex. This can also be use for other any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public HexCoordinateI NextCornerCw()
      => new(-m_s, -m_q, -m_r);

    /// <summary>Returns the next corner hex in a counter-clockwise direction on the same ring as the specified 'corner' hex. This can also be use for any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public HexCoordinateI NextCornerCcw()
      => new(-m_r, -m_s, -m_q);

    public HexCoordinateR ToHexCoordinateR()
      => new(m_q, m_r, m_s);

    public System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<HexCoordinateI>> TraverseSpiral(int radius)
    {
      for (var k = 0; k < radius; k++)
        yield return GetRing(k);
    }

    #region Static methods
    public static void AssertCubeCoordinate(int q, int r, int s)
    {
      if (!IsCubeCoordinate(q, r, s))
        throw new System.ArgumentException($"Contraint violation of cube coordinate (Q + R + S = 0) : ({q} + {r} + {s} = {(q + r + s)}).");
    }

    /// <summary>Returns the count of hexes in the range of, i.e. any hex that is on or inside, the specified radius.</summary>
    public static int ComputeRangeCount(int radius)
      => Flux.Enumerable.Loop(0, radius + 1, 6).AsParallel().Sum() + 1;

    /// <summary>Returns the count of hexes in the ring of the specified radius.</summary>
    public static int ComputeRingCount(int radius)
      => radius < 0
      ? throw new System.ArgumentOutOfRangeException(nameof(radius))
      : radius == 0
      ? 1
      : radius * 6;

    public static HexCoordinateI Diagonal(int direction)
      => (direction >= -5 && direction < 0)
      ? Diagonals[direction + 6]
      : (direction >= 0 && direction <= 5)
      ? Diagonals[direction]
      : throw new System.ArgumentOutOfRangeException(nameof(direction));

    /// <summary>Returns the unit hex of the specified direction range [0, 5].</summary>
    public static HexCoordinateI Direction(int direction /* [-5, 5] */)
      => (direction >= -5 && direction < 0)
      ? Directions[direction + 6]
      : (direction >= 0 && direction <= 5)
      ? Directions[direction]
      : throw new System.ArgumentOutOfRangeException(nameof(direction));

    /// <summary>The distance between two hex locations is computer like a vector is computed, i.e. the length of the difference.</summary>
    public static int Distance(HexCoordinateI source, HexCoordinateI target)
      => (source - target).Length();

    /// <summary>Determines wheter the specified coordinate components make up a valid cube hex.</summary>
    public static bool IsCubeCoordinate(int q, int r, int s)
      => q + r + s == 0;

    public static System.Collections.Generic.IEnumerable<HexCoordinateI> HexLinedraw(HexCoordinateI a, HexCoordinateI b)
    {
      var distance = Distance(a, b);

      var a_nudge = new HexCoordinateR(a.Q + 1e-06, a.R + 1e-06, a.S - 2e-06);
      var b_nudge = new HexCoordinateR(b.Q + 1e-06, b.R + 1e-06, b.S - 2e-06);

      var step = 1.0 / System.Math.Max(distance, 1);

      for (var i = 0; i <= distance; i++)
        yield return HexCoordinateR.Round(HexCoordinateR.Lerp(a_nudge, b_nudge, step * i));
    }
    #endregion Static methods

    #region Overloaded operators
    public static HexCoordinateI operator +(HexCoordinateI a, HexCoordinateI b)
      => new(a.m_q + b.m_q, a.m_r + b.m_r, a.m_s + b.m_s);
    public static HexCoordinateI operator *(HexCoordinateI h, int scalar)
      => new(h.m_q * scalar, h.m_r * scalar, h.m_s * scalar);
    public static HexCoordinateI operator /(HexCoordinateI h, int scalar)
      => scalar == 0
      ? throw new System.DivideByZeroException()
      : new(h.m_q / scalar, h.m_r / scalar, h.m_s / scalar);
    public static HexCoordinateI operator -(HexCoordinateI a, HexCoordinateI b)
      => new(a.m_q - b.m_q, a.m_r - b.m_r, a.m_s - b.m_s);
    #endregion Overloaded operators
  }
}
