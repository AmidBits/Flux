namespace Flux
{
  /// <summary>The HECS coordinate system.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Hexagonal_Efficient_Coordinate_System"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct HecsCoordinate
  {
    public static readonly HecsCoordinate Zero;

    public readonly int m_a;
    public readonly int m_r;
    public readonly int m_c;

    public HecsCoordinate(int a, int r, int c)
    {
      m_a = a;
      m_r = r;
      m_c = c;
    }

    [System.Diagnostics.Contracts.Pure] public int A { get => m_a; init => m_a = value; }
    [System.Diagnostics.Contracts.Pure] public int R { get => m_r; init => m_r = value; }
    [System.Diagnostics.Contracts.Pure] public int C { get => m_c; init => m_c = value; }

    [System.Diagnostics.Contracts.Pure]
    public HecsCoordinate[] NearestNeighborsCcw()
    {
      var cPa = m_c + m_a;
      var rPa = m_r + m_a;
      var uMa = 1 - m_a;
      var cMu = m_c - uMa;
      var rMu = m_r - uMa;

      return new HecsCoordinate[]
      {
        new HecsCoordinate(m_a, m_r, m_c + 1),
        new HecsCoordinate(uMa, rMu, cPa),
        new HecsCoordinate(uMa, rMu, cMu),
        new HecsCoordinate(m_a, m_r, m_c - 1),
        new HecsCoordinate(uMa, rPa, cMu),
        new HecsCoordinate(uMa, rPa, cPa),
      };
    }
    [System.Diagnostics.Contracts.Pure]
    public HecsCoordinate[] NearestNeighborsCw()
    {
      var cPa = m_c + m_a;
      var rPa = m_r + m_a;
      var uMa = 1 - m_a;
      var cMu = m_c - uMa;
      var rMu = m_r - uMa;

      return new HecsCoordinate[]
      {
        new HecsCoordinate(uMa, rMu, cPa),
        new HecsCoordinate(m_a, m_r, m_c + 1),
        new HecsCoordinate(uMa, rPa, cPa),
        new HecsCoordinate(uMa, rPa, cMu),
        new HecsCoordinate(m_a, m_r, m_c - 1),
        new HecsCoordinate(uMa, rMu, cMu),
      };
    }

    //#region Static methods
    ///// <summary>Returns a new hex representing the sum of the two specified hex vectors.</summary>
    //public static HecsCoordinate Add(HecsCoordinate a, HecsCoordinate b)
    //  => new(a.m_a + b.m_a, a.m_r + b.m_r, a.m_c + b.m_c);
    ///// <summary>Returns the count of hexes in the range of, i.e. any hex that is on or inside, the specified radius.</summary>
    //public static int ComputeRangeCount(int radius)
    //  => Enumerable.Loop(0, radius + 1, 6).AsParallel().Sum() + 1;
    ///// <summary>Returns the count of hexes in the ring of the specified radius.</summary>
    //public static int ComputeRingCount(int radius)
    //  => radius * 6;
    ///// <summary>Returns the unit hex of the specified direction range [0, 5].</summary>
    //public static HecsCoordinate Direction(int direction /* [-5, 5] */)
    //  => (direction >= -5 && direction < 0)
    //  ? Directions[direction + 6]
    //  : (direction >= 0 && direction <= 5)
    //  ? Directions[direction]
    //  : throw new System.ArgumentOutOfRangeException(nameof(direction));
    ///// <summary>The distance between two hex locations is computer like a vector is computed, i.e. the length of the difference.</summary>
    //public static int Distance(HecsCoordinate a, HecsCoordinate b)
    //  => Magnitude(Subtract(a, b));
    ///// <summary>Creates a new sequence of the surrounding neighbors of the specified center hex (excluded in the sequence).</summary>
    ///// <param name="center">The center reference hex.</param>
    //public static System.Collections.Generic.IEnumerable<HecsCoordinate> GetNeighbors(HecsCoordinate center)
    //{
    //  foreach (var hex in Directions)
    //    yield return center + hex;
    //}
    ///// <summary>Creates a new sequence of all (including the specified center) hex cubes within the specified radius (inclusive).</summary>
    ///// <param name="center">The center reference hex.</param>
    ///// <param name="radius">The radius from the center reference hex.</param>
    //public static System.Collections.Generic.IEnumerable<HecsCoordinate> GetRange(HecsCoordinate center, int radius)
    //{
    //  for (var q = -radius; q <= radius; q++)
    //    for (int r = System.Math.Max(-radius, -q - radius), rei = System.Math.Min(radius, -q + radius); r <= rei; r++)
    //      yield return new Hecs(center.m_a + q, center.m_r + r);
    //}
    ///// <summary>Create a new sequence of the hex cubes making up the ring at the radius from the center hex, starting at the specified (directional) cornerIndex.</summary>
    ///// <param name="center">The center reference hex.</param>
    ///// <param name="radius">[0,]</param>
    ///// <param name="startDirection">In the range [0, 6]. The default is 0.</param>
    ///// <param name="isCounterClockWise">Determines whether to enumerate counter-clockwise or not. The default is clockwise.</param>
    //public static System.Collections.Generic.IEnumerable<HecsCoordinate> GetRing(HecsCoordinate center, int radius, int startDirection = 0, bool isCounterClockWise = false)
    //{
    //  if (radius < 0) throw new System.ArgumentOutOfRangeException(nameof(radius));
    //  else if (startDirection < 0 || startDirection >= 6) throw new System.ArgumentOutOfRangeException(nameof(startDirection));
    //  else if (radius > 0)
    //  {
    //    var deltaMultiplier = isCounterClockWise ? -1 : 1; // Determines the sign of the delta direction as a multiplier.

    //    var corner = center + Direction(startDirection) * radius; // Find the first corner hex, relative center in direction of choice (plus the length of the radius).
    //    var deltaDirection = (startDirection + 2 * deltaMultiplier) % 6; // Set initial delta direction.

    //    for (var index = 0; index < 6; index++)
    //    {
    //      yield return corner;

    //      for (var deltaIndex = 1; deltaIndex < radius; deltaIndex++) // Enumerate the 'side of the current corner hex'.
    //        yield return corner + Direction(deltaDirection) * deltaIndex; // Compute the direction and offset of the side.

    //      corner = isCounterClockWise ? NextCornerCcw(corner) : NextCornerCw(corner); // Locate the next corner hex.
    //      deltaDirection = (deltaDirection + 1 * deltaMultiplier) % 6; // Set next delta direction (i.e. rotate clockwise one 'turn').
    //    }
    //  }
    //  else yield return center;
    //}
    ///// <summary>Determines wheter the specified coordinate components make up a valid cube hex.</summary>
    //public static bool IsCubeCoordinate(int q, int r, int s)
    //  => q + r + s == 0;
    ///// <summary>The magnitude (length) of a hex vector is half of a hex grid Manhattan distance.</summary>
    //public static int Magnitude(HecsCoordinate hex)
    //  => (System.Math.Abs(hex.m_a) + System.Math.Abs(hex.m_r) + System.Math.Abs(hex.m_c)) / 2;
    ///// <summary>Returns a new hex representing the product of the specified hex vector and the scalar value.</summary>
    //public static HecsCoordinate Multiply(HecsCoordinate h, int scalar)
    //  => new(h.m_a * scalar, h.m_r * scalar, h.m_c * scalar);
    ///// <summary>Returns the neighbor of the specified hex and direction.</summary>
    ///// <param name="hex">The reference hex.</param>
    ///// <param name="direction">The hexagon direction [0, 5].</param>
    ///// <returns>The neighbor of the reference hex.</returns>
    //public static HecsCoordinate Neighbor(HecsCoordinate hex, int direction)
    //  => Add(hex, Direction(direction));
    ///// <summary>Returns the next corner hex in a clockwise direction on the same ring as the specified 'corner' hex. This can also be use for other any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    //public static HecsCoordinate NextCornerCw(HecsCoordinate hex)
    //  => new(-hex.m_c, -hex.m_a, -hex.m_r);
    ///// <summary>Returns the next corner hex in a counter-clockwise direction on the same ring as the specified 'corner' hex. This can also be use for any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    //public static HecsCoordinate NextCornerCcw(HecsCoordinate hex)
    //  => new(-hex.m_r, -hex.m_c, -hex.m_a);
    ///// <summary>Returns a new hex representing the difference of the two specified hex vectors.</summary>
    //public static HecsCoordinate Subtract(HecsCoordinate a, HecsCoordinate b)
    //  => new(a.m_a - b.m_a, a.m_r - b.m_r, a.m_c - b.m_c);
    //#endregion Static methods

    #region Overloaded operators
    //public static Hecs operator +(Hecs h1, Hecs h2)
    //  => Add(h1, h2);
    //public static Hecs operator *(Hecs h, int scalar)
    //  => Multiply(h, scalar);
    //public static Hecs operator -(Hecs h1, Hecs h2)
    //  => Subtract(h1, h2);
    #endregion Overloaded operators
  }
}
