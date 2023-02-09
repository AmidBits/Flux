namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    public static void AssertCubeCoordinate<TSelf>(this Numerics.IHexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (!IsCubeCoordinate(source)) throw new ArgumentException($"Contraint violation of cube coordinate (Q + R + S = 0) : ({source.Q} + {source.R} + {source.S} = {(source.Q + source.R + source.S)}).");
    }

    /// <summary>Returns the length of the coordinate.</summary>
    public static TSelf CubeLength<TSelf>(this Numerics.IHexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => (TSelf.Abs(source.Q) + TSelf.Abs(source.R) + TSelf.Abs(source.S)).Divide(2);

    /// <summary>Returns the diagonal neighbor two cells over on-the-line and in-between two adjacent cells.</summary>
    /// <param name="direction">The hexagon direction [-5, 5] (either direction).</param>
    public static Numerics.HexCoordinate<TSelf> DiagonalNeighbor<TSelf>(this Numerics.IHexCoordinate<TSelf> source, int direction)
      where TSelf : System.Numerics.INumber<TSelf>
      => Numerics.IHexCoordinate<TSelf>.Diagonal(direction) + source;

    /// <summary>The distance between two hex locations is computer like a vector is computed, i.e. the length of the difference.</summary>
    public static TSelf Distance<TSelf>(this Numerics.HexCoordinate<TSelf> source, Numerics.HexCoordinate<TSelf> target)
      where TSelf : System.Numerics.INumber<TSelf>
      => (source - target).Length();

    /// <summary>Creates a new sequence of the surrounding neighbors of the specified center hex (excluded in the sequence).</summary>
    /// <param name="center">The center reference hex.</param>
    public static System.Collections.Generic.IEnumerable<Numerics.HexCoordinate<TSelf>> GetNeighbors<TSelf>(this Numerics.IHexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => Numerics.IHexCoordinate<TSelf>.Directions.Select(d => d + source);

    /// <summary>Creates a new sequence of all (including the specified center) hex cubes within the specified radius (inclusive).</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">The radius from the center reference hex.</param>
    public static System.Collections.Generic.IEnumerable<Numerics.HexCoordinate<TSelf>> GetRange<TSelf>(this Numerics.IHexCoordinate<TSelf> source, TSelf radius)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var q = -radius; q <= radius; q++)
        for (TSelf r = TSelf.Max(-radius, -q - radius), rei = TSelf.Min(radius, -q + radius); r <= rei; r++)
          yield return new Numerics.HexCoordinate<TSelf>(source.Q + q, source.R + r);
    }

    /// <summary>Create a new sequence of the hex cubes making up the ring at the radius from the center hex, starting at the specified (directional) cornerIndex.</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">[0,]</param>
    /// <param name="startDirection">In the range [0, 5]. The default is 0.</param>
    /// <param name="isCounterClockWise">Determines whether to enumerate counter-clockwise or not. The default is clockwise.</param>
    public static System.Collections.Generic.IEnumerable<Numerics.IHexCoordinate<TSelf>> GetRing<TSelf>(this Numerics.IHexCoordinate<TSelf> source, TSelf radius, int startDirection = 0, bool isCounterClockWise = false)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (startDirection < 0 || startDirection > 5) throw new System.ArgumentOutOfRangeException(nameof(startDirection));

      if (radius < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(radius));
      else if (radius > TSelf.Zero)
      {
        var deltaMultiplier = isCounterClockWise ? -1 : 1; // Determines the sign of the delta direction as a multiplier.

        var corner = Numerics.IHexCoordinate<TSelf>.Direction(startDirection) * radius + source; // Find the first corner hex, relative center in direction of choice (plus the length of the radius).
        var deltaDirection = (startDirection + 2 * deltaMultiplier) % 6; // Set initial delta direction.

        for (var index = 0; index < 6; index++)
        {
          yield return corner;

          for (var deltaIndex = TSelf.One; deltaIndex < radius; deltaIndex++) // Enumerate the 'side of the current corner hex'.
            yield return Numerics.IHexCoordinate<TSelf>.Direction(deltaDirection) * deltaIndex + corner; // Compute the direction and offset of the side.

          corner = isCounterClockWise ? corner.NextCornerCcw() : corner.NextCornerCw(); // Locate the next corner hex.
          deltaDirection = (deltaDirection + 1 * deltaMultiplier) % 6; // Set next delta direction (i.e. rotate clockwise one 'turn').
        }
      }
      else yield return source;
    }

    /// <summary>Returns whether the coordinate make up a valid cube hex, i.e. it satisfies the required cube constraint.</summary>
    public static bool IsCubeCoordinate<TSelf>(this Numerics.IHexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.IsZero(source.Q + source.R + source.S);

    public static TSelf Length<TSelf>(this Numerics.IHexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => (TSelf.Abs(source.Q) + TSelf.Abs(source.R) + TSelf.Abs(source.S)).Divide(2);

    public static Numerics.HexCoordinate<TSelf> Lerp<TSelf>(this Numerics.IHexCoordinate<TSelf> source, Numerics.IHexCoordinate<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => new(
        source.Q * (TSelf.One - mu) + target.Q * mu,
        source.R * (TSelf.One - mu) + target.R * mu,
        source.S * (TSelf.One - mu) + target.S * mu
      );

    /// <summary>Returns the neighbor of the specified hex and direction.</summary>
    /// <param name="direction">The hexagon direction [0, 5].</param>
    /// <returns>The neighbor of the reference hex.</returns>
    public static Numerics.HexCoordinate<TSelf> Neighbor<TSelf>(this Numerics.IHexCoordinate<TSelf> source, int direction)
      where TSelf : System.Numerics.INumber<TSelf>
      => Numerics.IHexCoordinate<TSelf>.Direction(direction) + source;

    /// <summary>Yields the next corner hex in a clockwise direction on the same ring as the specified 'corner' hex. This can also be use for other any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public static Numerics.HexCoordinate<TSelf> NextCornerCw<TSelf>(this Numerics.IHexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(-source.S, -source.Q, -source.R);

    /// <summary>Yields the next corner hex in a counter-clockwise direction on the same ring as the specified 'corner' hex. This can also be use for any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public static Numerics.HexCoordinate<TSelf> NextCornerCcw<TSelf>(this Numerics.IHexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(-source.R, -source.S, -source.Q);

    public static Numerics.HexCoordinate<TResult> Round<TSelf, TResult>(this Numerics.IHexCoordinate<TSelf> source, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.INumber<TResult>
    {
      var rQ = Rounding<TSelf>.Round(source.Q, mode);
      var rR = Rounding<TSelf>.Round(source.R, mode);
      var rS = Rounding<TSelf>.Round(source.S, mode);

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

    public static Numerics.CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this Numerics.IHexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(source.Q, source.R, source.S);

    public static Numerics.IHexCoordinate<TResult> ToHexCoordinate<TSelf, TResult>(this Numerics.IHexCoordinate<TSelf> source, out Numerics.HexCoordinate<TResult> result)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.INumber<TResult>
      => result = new Numerics.HexCoordinate<TResult>(
        TResult.CreateChecked(source.Q),
        TResult.CreateChecked(source.R),
        TResult.CreateChecked(source.S)
      );

    public static Numerics.IHexCoordinate<TResult> ToHexCoordinate<TSelf, TResult>(this Numerics.IHexCoordinate<TSelf> source, RoundingMode mode, out Numerics.HexCoordinate<TResult> result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.INumber<TResult>
      => result = Round<TSelf, TResult>(source, mode);

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<Numerics.IHexCoordinate<TSelf>>> TraverseSpiral<TSelf>(this Numerics.IHexCoordinate<TSelf> source, TSelf radius)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var k = TSelf.Zero; k < radius; k++)
        yield return GetRing(source, k);
    }
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
    /// <summary>A hex cube/axial coordinate system.</summary>
    /// <see href="https://www.redblobgames.com/grids/hexagons/"/>
    public interface IHexCoordinate<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
    {
      /// <summary>The first component or coordinate.</summary>
      TSelf Q { get; }
      /// <summary>The second component or coordinate.</summary>
      TSelf R { get; }
      /// <summary>The third component or coordinate, that can be calculated from Q and R with the formula (-Q - R).</summary>
      TSelf S { get; }

      /// <summary>Computes the count of hexes in the range of, i.e. any hex that is on or inside, the specified radius.</summary>
      public static int ComputeRangeCount(int radius)
        => new Flux.Loops.RangeLoop<int>(0, radius + 1, 6).AsParallel().Sum() + 1;

      /// <summary>Computes the count of hexes in the ring of the specified radius.</summary>
      public static int ComputeRingCount(int radius)
        => radius < 0
        ? throw new System.ArgumentOutOfRangeException(nameof(radius))
        : radius == 0
        ? 1
        : radius * 6;

      /// <summary>Diagonal in counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry), specified in the range [0, 5].</summary>
      public static Numerics.HexCoordinate<TSelf> Diagonal(int index)
        => index < 0 && index > 5
        ? throw new System.ArgumentOutOfRangeException(nameof(index))
        : Diagonals[index];

      /// <summary>Diagonals in counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry), in the range [0, 5].</summary>
      public static Numerics.HexCoordinate<TSelf>[] Diagonals
        => new Numerics.HexCoordinate<TSelf>[] {
        new(TSelf.CreateChecked(2), -TSelf.One, -TSelf.One),
        new(TSelf.One, -TSelf.CreateChecked(2), TSelf.One),
        new(-TSelf.One, -TSelf.One, TSelf.CreateChecked(2)),
        new(-TSelf.CreateChecked(2), TSelf.One, TSelf.One),
        new(-TSelf.One, TSelf.CreateChecked(2), -TSelf.One),
        new(TSelf.One, TSelf.One, -TSelf.CreateChecked(2))
        };

      /// <summary>Directions in counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry), specified in the range [0, 5].</summary>
      /// <summary>Returns the unit hex of the specified direction range [0, 5].</summary>
      /// <param name="index">The </param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static Numerics.HexCoordinate<TSelf> Direction(int index)
        => index < 0 || index > 5
        ? throw new System.ArgumentOutOfRangeException(nameof(index))
        : Directions[index];

      /// <summary>Directions in counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry), in the range [0, 5].</summary>
      public static Numerics.HexCoordinate<TSelf>[] Directions
        => new Numerics.HexCoordinate<TSelf>[] {
        new(TSelf.One, TSelf.Zero, -TSelf.One),
        new(TSelf.One, -TSelf.One, TSelf.Zero),
        new(TSelf.Zero, -TSelf.One, TSelf.One),
        new(-TSelf.One, TSelf.Zero, TSelf.One),
        new(-TSelf.One, TSelf.One, TSelf.Zero),
        new(TSelf.Zero, TSelf.One, -TSelf.One),
        };

      ///// <summary>Returns whether the coordinate make up a valid cube hex, i.e. it satisfies the required cube constraint.</summary>
      //public static bool IsCubeCoordinate(TSelf q, TSelf r, TSelf s) => TSelf.IsZero(q + r + s);

      ///// <summary>Returns the length of the coordinate.</summary>
      //public static TSelf CubeLength(TSelf q, TSelf r, TSelf s) => (TSelf.Abs(q) + TSelf.Abs(r) + TSelf.Abs(s)).Divide(2);
    }
  }
}
