using System.Linq;

namespace Flux.Geometry
{
  /// <summary>The Hex coordinate system used is the Cube coordinate, and can be specified using </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct Hex
    : System.IEquatable<Hex>
  {
    public static Hex[] Directions
      => new Hex[] {
        new Hex(1, 0, -1),
        new(1, -1, 0),
        new Hex(0, -1, 1),
        new Hex(-1, 0, 1),
        new Hex(-1, 1, 0),
        new Hex(0, 1, -1),
      };

    public static readonly Hex Zero;

    public readonly int m_q;
    public readonly int m_r;
    public readonly int m_s;

    public Hex(int q, int r, int s)
    {
      if (!IsCubeCoordinate(q, r, s)) throw new System.ArgumentException($"Contraint violation of cube coordinate (Q + R + S = 0) = ({q} + {r} + {s} = {(q + r + s)}).");

      m_q = q;
      m_r = r;
      m_s = s;
    }
    public Hex(int q, int r)
      : this(q, r, -q - r)
    { }

    public int Q => m_q;
    public int R => m_r;
    public int S => m_s;

    #region Static methods
    /// <summary>Returns a new hex representing the sum of the two specified hex vectors.</summary>
    public static Hex Add(Hex a, Hex b)
      => new(a.m_q + b.m_q, a.m_r + b.m_r, a.m_s + b.m_s);
    /// <summary>Returns the count of hexes in the range of, i.e. any hex that is on or inside, the specified radius.</summary>
    public static int ComputeRangeCount(int radius)
      => Liq.Range(0, radius + 1, 6).AsParallel().Sum() + 1;
    /// <summary>Returns the count of hexes in the ring of the specified radius.</summary>
    public static int ComputeRingCount(int radius)
      => radius * 6;
    /// <summary>Returns the unit hex of the specified direction range [0, 5].</summary>
    public static Hex Direction(int direction /* [-5, 5] */)
      => (direction >= -5 && direction < 0)
      ? Directions[direction + 6]
      : (direction >= 0 && direction <= 5)
      ? Directions[direction]
      : throw new System.ArgumentOutOfRangeException(nameof(direction));
    /// <summary>The distance between two hex locations is computer like a vector is computed, i.e. the length of the difference.</summary>
    public static int Distance(Hex a, Hex b)
      => Magnitude(Subtract(a, b));
    /// <summary>Creates a new sequence of the surrounding neighbors of the specified center hex (excluded in the sequence).</summary>
    /// <param name="center">The center reference hex.</param>
    public static System.Collections.Generic.IEnumerable<Hex> GetNeighbors(Hex center)
    {
      foreach (var hex in Directions)
        yield return center + hex;
    }
    /// <summary>Creates a new sequence of all (including the specified center) hex cubes within the specified radius (inclusive).</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">The radius from the center reference hex.</param>
    public static System.Collections.Generic.IEnumerable<Hex> GetRange(Hex center, int radius)
    {
      for (var q = -radius; q <= radius; q++)
        for (int r = System.Math.Max(-radius, -q - radius), rei = System.Math.Min(radius, -q + radius); r <= rei; r++)
          yield return new Hex(center.m_q + q, center.m_r + r);
    }
    /// <summary>Create a new sequence of the hex cubes making up the ring at the radius from the center hex, starting at the specified (directional) cornerIndex.</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">[0,]</param>
    /// <param name="startDirection">In the range [0, 6]. The default is 0.</param>
    /// <param name="isCounterClockWise">Determines whether to enumerate counter-clockwise or not. The default is clockwise.</param>
    public static System.Collections.Generic.IEnumerable<Hex> GetRing(Hex center, int radius, int startDirection = 0, bool isCounterClockWise = false)
    {
      if (radius < 0) throw new System.ArgumentOutOfRangeException(nameof(radius));
      else if (startDirection < 0 || startDirection >= 6) throw new System.ArgumentOutOfRangeException(nameof(startDirection));
      else if (radius > 0)
      {
        var deltaMultiplier = isCounterClockWise ? -1 : 1; // Determines the sign of the delta direction as a multiplier.

        var corner = center + Direction(startDirection) * radius; // Find the first corner hex, relative center in direction of choice (plus the length of the radius).
        var deltaDirection = (startDirection + 2 * deltaMultiplier) % 6; // Set initial delta direction.

        for (var index = 0; index < 6; index++)
        {
          yield return corner;

          for (var deltaIndex = 1; deltaIndex < radius; deltaIndex++) // Enumerate the 'side of the current corner hex'.
            yield return corner + Direction(deltaDirection) * deltaIndex; // Compute the direction and offset of the side.

          corner = isCounterClockWise ? NextCornerCcw(corner) : NextCornerCw(corner); // Locate the next corner hex.
          deltaDirection = (deltaDirection + 1 * deltaMultiplier) % 6; // Set next delta direction (i.e. rotate clockwise one 'turn').
        }
      }
      else yield return center;
    }
    /// <summary>Determines wheter the specified coordinate components make up a valid cube hex.</summary>
    public static bool IsCubeCoordinate(int q, int r, int s)
      => q + r + s == 0;
    /// <summary>The magnitude (length) of a hex vector is half of a hex grid Manhattan distance.</summary>
    public static int Magnitude(Hex hex)
      => (System.Math.Abs(hex.m_q) + System.Math.Abs(hex.m_r) + System.Math.Abs(hex.m_s)) / 2;
    /// <summary>Returns a new hex representing the product of the specified hex vector and the scalar value.</summary>
    public static Hex Multiply(Hex h, int scalar)
      => new(h.m_q * scalar, h.m_r * scalar, h.m_s * scalar);
    /// <summary>Returns the neighbor of the specified hex and direction.</summary>
    /// <param name="hex">The reference hex.</param>
    /// <param name="direction">The hexagon direction [0, 5].</param>
    /// <returns>The neighbor of the reference hex.</returns>
    public static Hex Neighbor(Hex hex, int direction)
      => Add(hex, Direction(direction));
    /// <summary>Returns the next corner hex in a clockwise direction on the same ring as the specified 'corner' hex. This can also be use for other any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public static Hex NextCornerCw(Hex hex)
      => new(-hex.m_s, -hex.m_q, -hex.m_r);
    /// <summary>Returns the next corner hex in a counter-clockwise direction on the same ring as the specified 'corner' hex. This can also be use for any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public static Hex NextCornerCcw(Hex hex)
      => new(-hex.m_r, -hex.m_s, -hex.m_q);
    /// <summary>Returns a new hex representing the difference of the two specified hex vectors.</summary>
    public static Hex Subtract(Hex a, Hex b)
      => new(a.m_q - b.m_q, a.m_r - b.m_r, a.m_s - b.m_s);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Hex h1, Hex h2)
      => h1.Equals(h2);
    public static bool operator !=(Hex h1, Hex h2)
      => !h1.Equals(h2);

    public static Hex operator +(Hex h1, Hex h2)
      => Add(h1, h2);
    public static Hex operator *(Hex h, int scalar)
      => Multiply(h, scalar);
    public static Hex operator -(Hex h1, Hex h2)
      => Subtract(h1, h2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Hex other)
      => m_q == other.m_q && m_r == other.m_r && m_s == other.m_s;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Hex o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_q, m_r, m_s);
    public override string ToString()
      => $"{GetType().Name} {{ Q = {m_q}, R = {m_r}, S = {m_s} }}";
    #endregion Object overrides
  }
}
