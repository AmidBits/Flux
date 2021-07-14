namespace Flux.Geometry.Hexagon
{
  /// <summary>The Hex coordinate system used is the Cube coordinate, and can be specified using </summary>
  public struct Hex
    : System.IEquatable<Hex>
  {
    public static Hex[] Directions
      => new Hex[] {
        new Hex(1, 0, -1),
        new Hex(1, -1, 0),
        new Hex(0, -1, 1),
        new Hex(-1, 0, 1),
        new Hex(-1, 1, 0),
        new Hex(0, 1, -1),
      };

    public static readonly Hex Empty;

    public int Q { get; }
    public int R { get; }
    public int S { get; }

    public Hex(int q, int r, int s)
    {
      if (!IsCubeCoordinate(q, r, s)) throw new System.InvalidOperationException($"Contraint violation of cube coordinate (Q + R + S = 0) = ({q} + {r} + {s} = {(q + r + s)}).");

      Q = q;
      R = r;
      S = s;
    }
    public Hex(int q, int r)
      : this(q, r, -q - r)
    { }

    public bool IsEmpty
      => Equals(Empty);

    #region Static methods
    /// <summary>Returns a new hex representing the sum of the two specified hex vectors.</summary>
    public static Hex Add(Hex a, Hex b)
      => new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);
    /// <summary>Returns the unit hex of the specified direction range [0, 5].</summary>
    public static Hex Direction(int direction /* [-5, 5] */)
      => (direction >= -5 && direction < 0)
      ? Directions[direction + 6]
      : (direction >= 0 && direction <= 5)
      ? Directions[direction]
      : throw new System.ArgumentOutOfRangeException(nameof(direction));
    /// <summary>The distance between two hex locations is computer like a vector is computed, i.e. the length of the difference.</summary>
    public static int DistanceBetween(Hex a, Hex b)
      => Length(Subtract(a, b));
    /// <summary>Determines wheter the specified coordinate components make up a valid cube hex.</summary>
    public static bool IsCubeCoordinate(int q, int r, int s)
      => q + r + s == 0;
    /// <summary>The length of a hex vector is half of a hex grid Manhattan distance.</summary>
    public static int Length(Hex hex)
      => (System.Math.Abs(hex.Q) + System.Math.Abs(hex.R) + System.Math.Abs(hex.S)) / 2;
    /// <summary>Returns a new hex representing the product of the specified hex vector and the scalar value.</summary>
    public static Hex Multiply(Hex h, int scalar)
      => new Hex(h.Q * scalar, h.R * scalar, h.S * scalar);
    /// <summary>Returns the neighbor of the specified hex and direction.</summary>
    /// <param name="hex">The reference hex.</param>
    /// <param name="direction">The hexagon direction [0, 5].</param>
    /// <returns>The neighbor of the reference hex.</returns>
    public static Hex Neighbor(Hex hex, int direction)
      => Add(hex, Direction(direction));
    /// <summary>Returns the next corner hex in a clockwise direction on the same ring as the specified hex.</summary>
    public static Hex NextCornerCw(Hex hex)
      => new Hex(-hex.S, -hex.Q, -hex.R);
    /// <summary>Returns the next corner hex in a counter-clockwise direction on the same ring as the specified hex.</summary>
    public static Hex NextCornerCcw(Hex hex)
      => new Hex(-hex.R, -hex.S, -hex.Q);
    /// <summary>Returns a new hex representing the difference of the two specified hex vectors.</summary>
    public static Hex Subtract(Hex a, Hex b)
      => new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);

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
          yield return new Hex(center.Q + q, center.R + r);
    }
    // Alternative to range, with many more iterations.
    //public static System.Collections.Generic.IEnumerable<Hex> GetNewRange(Hex center, int radius)
    //{
    //  for (var n = 0; n < radius; n++)
    //    for (var q = -n; q <= n; q++)
    //      for (var r = -n; r <= n; r++)
    //        for (var s = -n; s <= n; s++)
    //          if (System.Math.Abs(q) + System.Math.Abs(r) + System.Math.Abs(s) == n * 2 && q + r + s == 0)
    //            yield return new Hex(q, r, s);
    //}
    /// <summary>Create a new sequence of the hex cubes making up the ring at the radius from the center hex, starting at the specified (directional) cornerIndex.</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">[0,]</param>
    /// <param name="cornerIndex">In the range [0, 6], with a default of 0.</param>
    /// <returns></returns>
    //public static System.Collections.Generic.IEnumerable<Hex> GetRing(Hex center, int radius, int cornerIndex = 0)
    //{
    //  if (radius < 0) throw new System.ArgumentOutOfRangeException(nameof(radius));
    //  else if (cornerIndex < 0 || cornerIndex >= 6) throw new System.ArgumentOutOfRangeException(nameof(cornerIndex));
    //  else if (radius > 0)
    //  {
    //    var cube = center + Direction(cornerIndex) * radius; // Find the first corner.

    //    var startingDirection = (cornerIndex + 2) % 6;

    //    for (var directionOffset = 0; directionOffset < 6; directionOffset++)
    //    {
    //      for (var indexAlongTheSide = 0; indexAlongTheSide < radius; indexAlongTheSide++)
    //      {
    //        yield return cube;

    //        cube = Neighbor(cube, (startingDirection + directionOffset) % 6);
    //      }
    //    }
    //  }
    //  else yield return center;
    //}
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
      => Q == other.Q && R == other.R && S == other.S;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Hex o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Q, R, S);
    public override string ToString()
      => $"<{GetType().Name}: {Q}, {R}, {S}>";
    #endregion Object overrides
  }
}
