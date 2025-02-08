namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns the diagonal neighbor two cells over on-the-line and in-between two adjacent cells.</summary>
    /// <param name = "direction" > The hexagon direction [-5, 5] (either direction).</param>
    public static Geometry.Coordinates.HexCoordinate<TSelf> DiagonalNeighbor<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source, int direction)
      where TSelf : System.Numerics.INumber<TSelf>
      => Geometry.Coordinates.HexCoordinate<TSelf>.Diagonal(direction) + source;

    /// <summary>Creates a new sequence of the surrounding neighbors of the specified center hex(excluded in the sequence).</summary>
    /// <param name = "center" > The center reference hex.</param>
    public static System.Collections.Generic.IEnumerable<Geometry.Coordinates.HexCoordinate<TSelf>> GetNeighbors<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => Geometry.Coordinates.HexCoordinate<TSelf>.Directions.Select(d => d + source);

    /// <summary>Creates a new sequence of all (including the specified center) hex cubes within the specified radius (inclusive).</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">The radius from the center reference hex.</param>
    public static System.Collections.Generic.IEnumerable<Geometry.Coordinates.HexCoordinate<TSelf>> GetRange<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source, TSelf radius)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var q = -radius; q <= radius; q++)
        for (TSelf r = TSelf.Max(-radius, -q - radius), rei = TSelf.Min(radius, -q + radius); r <= rei; r++)
          yield return new Geometry.Coordinates.HexCoordinate<TSelf>(source.Q + q, source.R + r);
    }

    /// <summary>Create a new sequence of the hex cubes making up the ring at the radius from the center hex, starting at the specified (directional) cornerIndex.</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">[0,]</param>
    /// <param name="startDirection">In the range [0, 5]. The default is 0.</param>
    /// <param name="isCounterClockWise">Determines whether to enumerate counter-clockwise or not. The default is clockwise.</param>
    public static System.Collections.Generic.IEnumerable<Geometry.Coordinates.HexCoordinate<TSelf>> GetRing<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source, TSelf radius, int startDirection = 0, bool isCounterClockWise = false)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (startDirection < 0 || startDirection > 5) throw new System.ArgumentOutOfRangeException(nameof(startDirection));

      if (radius < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(radius));
      else if (radius > TSelf.Zero)
      {
        var deltaMultiplier = isCounterClockWise ? -1 : 1; // Determines the sign of the delta direction as a multiplier.

        var corner = Geometry.Coordinates.HexCoordinate<TSelf>.Direction(startDirection) * radius + source; // Find the first corner hex, relative center in direction of choice (plus the length of the radius).
        var deltaDirection = (startDirection + 2 * deltaMultiplier) % 6; // Set initial delta direction.

        for (var index = 0; index < 6; index++)
        {
          yield return corner;

          for (var deltaIndex = TSelf.One; deltaIndex < radius; deltaIndex++) // Enumerate the 'side of the current corner hex'.
            yield return Geometry.Coordinates.HexCoordinate<TSelf>.Direction(deltaDirection) * deltaIndex + corner; // Compute the direction and offset of the side.

          corner = isCounterClockWise ? corner.NextCornerCcw() : corner.NextCornerCw(); // Locate the next corner hex.
          deltaDirection = (deltaDirection + 1 * deltaMultiplier) % 6; // Set next delta direction (i.e. rotate clockwise one 'turn').
        }
      }
      else yield return source;
    }

    public static Geometry.Coordinates.HexCoordinate<TSelf> Lerp<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source, Geometry.Coordinates.HexCoordinate<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => new(
        source.Q * (TSelf.One - mu) + target.Q * mu,
        source.R * (TSelf.One - mu) + target.R * mu,
        source.S * (TSelf.One - mu) + target.S * mu
      );

    /// <summary>Returns the neighbor of the specified hex and direction.</summary>
    /// <param name="direction">The hexagon direction [0, 5].</param>
    /// <returns>The neighbor of the reference hex.</returns>
    public static Geometry.Coordinates.HexCoordinate<TSelf> Neighbor<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source, int direction)
      where TSelf : System.Numerics.INumber<TSelf>
      => source + Geometry.Coordinates.HexCoordinate<TSelf>.Direction(direction);

    /// <summary>Yields the next corner hex in a clockwise direction on the same ring as the specified 'corner' hex. This can also be use for other any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public static Geometry.Coordinates.HexCoordinate<TSelf> NextCornerCw<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(-source.S, -source.Q, -source.R);

    /// <summary>Yields the next corner hex in a counter-clockwise direction on the same ring as the specified 'corner' hex. This can also be use for any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public static Geometry.Coordinates.HexCoordinate<TSelf> NextCornerCcw<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(-source.R, -source.S, -source.Q);

    public static Geometry.Coordinates.HexCoordinate<TResult> Round<TSelf, TResult>(this Geometry.Coordinates.HexCoordinate<TSelf> source, UniversalRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.INumber<TResult>
    {
      var rQ = source.Q.RoundUniversal(mode);
      var rR = source.R.RoundUniversal(mode);
      var rS = source.S.RoundUniversal(mode);

      var aQ = TSelf.Abs(rQ - source.Q);
      var aR = TSelf.Abs(rR - source.R);
      var aS = TSelf.Abs(rS - source.S);

      if (aQ > aR && aQ > aS)
        rQ = -rR - rS;
      else if (aR > aS)
        rR = -rQ - rS;
      else
        rS = -rQ - rR;

      return new(
        TResult.CreateChecked(rQ),
        TResult.CreateChecked(rR),
        TResult.CreateChecked(rS)
      );
    }

    public static (double x, double y, double z) ToCartesianCoordinate3<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(double.CreateChecked(source.Q), double.CreateChecked(source.R), double.CreateChecked(source.S));

    public static Geometry.Coordinates.HexCoordinate<TResult> ToHexCoordinate<TSelf, TResult>(this Geometry.Coordinates.HexCoordinate<TSelf> source, out Geometry.Coordinates.HexCoordinate<TResult> result)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.INumber<TResult>
      => result = new(
        TResult.CreateChecked(source.Q),
        TResult.CreateChecked(source.R),
        TResult.CreateChecked(source.S)
      );

    public static Geometry.Coordinates.HexCoordinate<TResult> ToHexCoordinate<TSelf, TResult>(this Geometry.Coordinates.HexCoordinate<TSelf> source, UniversalRounding mode, out Geometry.Coordinates.HexCoordinate<TResult> result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.INumber<TResult>
      => result = Round<TSelf, TResult>(source, mode);

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<Geometry.Coordinates.HexCoordinate<TSelf>>> TraverseSpiral<TSelf>(this Geometry.Coordinates.HexCoordinate<TSelf> source, TSelf radius)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var k = TSelf.Zero; k < radius; k++)
        yield return GetRing(source, k);
    }

    /// <summary>Creates a new <see cref="Geometry.HexCoordinate{TSelf}"/> from a <see cref="System.Drawing.Point"/>.</summary>
    public static Geometry.Coordinates.HexCoordinate<int> ToHexCoordinate(this System.Drawing.Point source)
      => new(
        source.X,
        source.Y
      );

    /// <summary>Creates a new <see cref="Geometry.HexCoordinate{TSelf}"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.Coordinates.HexCoordinate<float> ToHexCoordinate(this System.Numerics.Vector3 source)
      => new(
        source.X,
        source.Y,
        source.Z
      );
  }
}
