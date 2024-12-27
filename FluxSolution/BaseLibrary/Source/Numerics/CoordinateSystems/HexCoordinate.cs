namespace Flux
{
  #region ExtensionMethods

  public static partial class Fx
  {
    /// <summary>Returns the diagonal neighbor two cells over on-the-line and in-between two adjacent cells.</summary>
    /// <param name = "direction" > The hexagon direction [-5, 5] (either direction).</param>
    public static Coordinates.HexCoordinate<TSelf> DiagonalNeighbor<TSelf>(this Coordinates.HexCoordinate<TSelf> source, int direction)
      where TSelf : System.Numerics.INumber<TSelf>
      => Coordinates.HexCoordinate<TSelf>.Diagonal(direction) + source;

    /// <summary>Creates a new sequence of the surrounding neighbors of the specified center hex(excluded in the sequence).</summary>
    /// <param name = "center" > The center reference hex.</param>
    public static System.Collections.Generic.IEnumerable<Coordinates.HexCoordinate<TSelf>> GetNeighbors<TSelf>(this Coordinates.HexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => Coordinates.HexCoordinate<TSelf>.Directions.Select(d => d + source);

    /// <summary>Creates a new sequence of all (including the specified center) hex cubes within the specified radius (inclusive).</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">The radius from the center reference hex.</param>
    public static System.Collections.Generic.IEnumerable<Coordinates.HexCoordinate<TSelf>> GetRange<TSelf>(this Coordinates.HexCoordinate<TSelf> source, TSelf radius)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var q = -radius; q <= radius; q++)
        for (TSelf r = TSelf.Max(-radius, -q - radius), rei = TSelf.Min(radius, -q + radius); r <= rei; r++)
          yield return new Coordinates.HexCoordinate<TSelf>(source.Q + q, source.R + r);
    }

    /// <summary>Create a new sequence of the hex cubes making up the ring at the radius from the center hex, starting at the specified (directional) cornerIndex.</summary>
    /// <param name="center">The center reference hex.</param>
    /// <param name="radius">[0,]</param>
    /// <param name="startDirection">In the range [0, 5]. The default is 0.</param>
    /// <param name="isCounterClockWise">Determines whether to enumerate counter-clockwise or not. The default is clockwise.</param>
    public static System.Collections.Generic.IEnumerable<Coordinates.HexCoordinate<TSelf>> GetRing<TSelf>(this Coordinates.HexCoordinate<TSelf> source, TSelf radius, int startDirection = 0, bool isCounterClockWise = false)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (startDirection < 0 || startDirection > 5) throw new System.ArgumentOutOfRangeException(nameof(startDirection));

      if (radius < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(radius));
      else if (radius > TSelf.Zero)
      {
        var deltaMultiplier = isCounterClockWise ? -1 : 1; // Determines the sign of the delta direction as a multiplier.

        var corner = Coordinates.HexCoordinate<TSelf>.Direction(startDirection) * radius + source; // Find the first corner hex, relative center in direction of choice (plus the length of the radius).
        var deltaDirection = (startDirection + 2 * deltaMultiplier) % 6; // Set initial delta direction.

        for (var index = 0; index < 6; index++)
        {
          yield return corner;

          for (var deltaIndex = TSelf.One; deltaIndex < radius; deltaIndex++) // Enumerate the 'side of the current corner hex'.
            yield return Coordinates.HexCoordinate<TSelf>.Direction(deltaDirection) * deltaIndex + corner; // Compute the direction and offset of the side.

          corner = isCounterClockWise ? corner.NextCornerCcw() : corner.NextCornerCw(); // Locate the next corner hex.
          deltaDirection = (deltaDirection + 1 * deltaMultiplier) % 6; // Set next delta direction (i.e. rotate clockwise one 'turn').
        }
      }
      else yield return source;
    }

    public static Coordinates.HexCoordinate<TSelf> Lerp<TSelf>(this Coordinates.HexCoordinate<TSelf> source, Coordinates.HexCoordinate<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => new(
        source.Q * (TSelf.One - mu) + target.Q * mu,
        source.R * (TSelf.One - mu) + target.R * mu,
        source.S * (TSelf.One - mu) + target.S * mu
      );

    /// <summary>Returns the neighbor of the specified hex and direction.</summary>
    /// <param name="direction">The hexagon direction [0, 5].</param>
    /// <returns>The neighbor of the reference hex.</returns>
    public static Coordinates.HexCoordinate<TSelf> Neighbor<TSelf>(this Coordinates.HexCoordinate<TSelf> source, int direction)
      where TSelf : System.Numerics.INumber<TSelf>
      => source + Coordinates.HexCoordinate<TSelf>.Direction(direction);

    /// <summary>Yields the next corner hex in a clockwise direction on the same ring as the specified 'corner' hex. This can also be use for other any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public static Coordinates.HexCoordinate<TSelf> NextCornerCw<TSelf>(this Coordinates.HexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(-source.S, -source.Q, -source.R);

    /// <summary>Yields the next corner hex in a counter-clockwise direction on the same ring as the specified 'corner' hex. This can also be use for any 'non-corner' hex for various 'circular' (symmetrical) pattern traverals.</summary>
    public static Coordinates.HexCoordinate<TSelf> NextCornerCcw<TSelf>(this Coordinates.HexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(-source.R, -source.S, -source.Q);

    public static Coordinates.HexCoordinate<TResult> Round<TSelf, TResult>(this Coordinates.HexCoordinate<TSelf> source, UniversalRounding mode)
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

    public static (double x, double y, double z) ToCartesianCoordinate3<TSelf>(this Coordinates.HexCoordinate<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(double.CreateChecked(source.Q), double.CreateChecked(source.R), double.CreateChecked(source.S));

    public static Coordinates.HexCoordinate<TResult> ToHexCoordinate<TSelf, TResult>(this Coordinates.HexCoordinate<TSelf> source, out Coordinates.HexCoordinate<TResult> result)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.INumber<TResult>
      => result = new Coordinates.HexCoordinate<TResult>(
        TResult.CreateChecked(source.Q),
        TResult.CreateChecked(source.R),
        TResult.CreateChecked(source.S)
      );

    public static Coordinates.HexCoordinate<TResult> ToHexCoordinate<TSelf, TResult>(this Coordinates.HexCoordinate<TSelf> source, UniversalRounding mode, out Coordinates.HexCoordinate<TResult> result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.INumber<TResult>
      => result = Round<TSelf, TResult>(source, mode);

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<Coordinates.HexCoordinate<TSelf>>> TraverseSpiral<TSelf>(this Coordinates.HexCoordinate<TSelf> source, TSelf radius)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var k = TSelf.Zero; k < radius; k++)
        yield return GetRing(source, k);
    }

    /// <summary>Creates a new <see cref="Geometry.HexCoordinate{TSelf}"/> from a <see cref="System.Drawing.Point"/>.</summary>
    public static Coordinates.HexCoordinate<int> ToHexCoordinate(this System.Drawing.Point source)
      => new(
        source.X,
        source.Y
      );

    /// <summary>Creates a new <see cref="Geometry.HexCoordinate{TSelf}"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Coordinates.HexCoordinate<float> ToHexCoordinate(this System.Numerics.Vector3 source)
      => new(
        source.X,
        source.Y,
        source.Z
      );
  }

  #endregion

  namespace Coordinates
  {
    /// <summary>A cube hex coordinate system.</summary>
    /// <see href="https://www.redblobgames.com/grids/hexagons/"/>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct HexCoordinate<TSelf>
      : System.IFormattable
      where TSelf : System.Numerics.INumber<TSelf>
    {
      public static HexCoordinate<TSelf> Zero { get; }

      private readonly TSelf m_q;
      private readonly TSelf m_r;
      private readonly TSelf m_s;

      public HexCoordinate(TSelf q, TSelf r, TSelf s)
      {
        AssertCubeCoordinate(q, r, s);

        m_q = q;
        m_r = r;
        m_s = s;
      }
      public HexCoordinate(TSelf q, TSelf r) : this(q, r, -q - r) { }

      public void Deconstruct(out TSelf q, out TSelf r, out TSelf s) { q = m_q; r = m_r; s = m_s; }

      public readonly TSelf Q { get => m_q; init => m_q = value; }
      public readonly TSelf R { get => m_r; init => m_r = value; }
      public readonly TSelf S { get => m_s; init => m_s = value; }

      public readonly TSelf Length() => (TSelf.Abs(m_q) + TSelf.Abs(m_r) + TSelf.Abs(m_s)) / TSelf.CreateChecked(2);

      public CartesianCoordinate ToCartesianCoordinate(double w = 0) => new(double.CreateChecked(m_q), double.CreateChecked(m_r), double.CreateChecked(m_s), w);

      public (double x, double y) ToCartesianCoordinate2() => (double.CreateChecked(m_q), double.CreateChecked(m_r));

      public (double x, double y, double z) ToCartesianCoordinate3() => (double.CreateChecked(m_q), double.CreateChecked(m_r), double.CreateChecked(m_s));

      public readonly System.Drawing.Point ToPoint() => new(int.CreateChecked(m_q), int.CreateChecked(m_r));

      public readonly System.Numerics.Vector3 ToVector3() => new(float.CreateChecked(m_q), float.CreateChecked(m_r), float.CreateChecked(m_s));

      #region Static methods

      public static void AssertCubeCoordinate(TSelf q, TSelf r, TSelf s)
      {
        if (!IsCubeCoordinate(q, r, s)) throw new ArgumentException($"Contraint violation of cube coordinate (Q + R + S = 0) : ({q} + {r} + {s} = {(q + r + s)}).");
      }

      /// <summary>Computes the count of hexes in the range of, i.e. any hex that is on or inside, the specified radius.</summary>
      public static int ComputeRangeCount(int radius)
        => 0.LoopRange(6, radius + 1).AsParallel().Sum() + 1;

      /// <summary>Computes the count of hexes in the ring of the specified radius.</summary>
      public static int ComputeRingCount(int radius)
        => radius < 0
        ? throw new System.ArgumentOutOfRangeException(nameof(radius))
        : radius == 0
        ? 1
        : radius * 6;

      /// <summary>Diagonal in counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry), specified in the range [0, 5].</summary>
      public static HexCoordinate<TSelf> Diagonal(int index)
        => index < 0 && index > 5
        ? throw new System.ArgumentOutOfRangeException(nameof(index))
        : Diagonals[index];

      /// <summary>Diagonals in counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry), in the range [0, 5].</summary>
      public static HexCoordinate<TSelf>[] Diagonals
        => new HexCoordinate<TSelf>[] {
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
      public static HexCoordinate<TSelf> Direction(int index)
        => index < 0 || index > 5
        ? throw new System.ArgumentOutOfRangeException(nameof(index))
        : Directions[index];

      /// <summary>Directions in counter-clockwise order, starting at 3 o'clock (the same as Euclidean trigonometry), in the range [0, 5].</summary>
      public static HexCoordinate<TSelf>[] Directions
        => new HexCoordinate<TSelf>[] {
          new(TSelf.One, TSelf.Zero, -TSelf.One),
          new(TSelf.One, -TSelf.One, TSelf.Zero),
          new(TSelf.Zero, -TSelf.One, TSelf.One),
          new(-TSelf.One, TSelf.Zero, TSelf.One),
          new(-TSelf.One, TSelf.One, TSelf.Zero),
          new(TSelf.Zero, TSelf.One, -TSelf.One),
        };

      /// <summary>Returns whether the coordinate make up a valid cube hex, i.e. it satisfies the required cube constraint.</summary>
      public static bool IsCubeCoordinate(TSelf q, TSelf r, TSelf s) => TSelf.IsZero(q + r + s);

      public static Coordinates.HexCoordinate<TSelf> Lerp(Coordinates.HexCoordinate<TSelf> source, Coordinates.HexCoordinate<TSelf> target, TSelf mu)
        => new(
          source.Q * (TSelf.One - mu) + target.Q * mu,
          source.R * (TSelf.One - mu) + target.R * mu,
          source.S * (TSelf.One - mu) + target.S * mu
        );

      #endregion // Static methods

      #region Overloaded operators

      public static HexCoordinate<TSelf> operator +(HexCoordinate<TSelf> a, HexCoordinate<TSelf> b) => new(a.m_q + b.Q, a.m_r + b.R, a.m_s + b.S);
      public static HexCoordinate<TSelf> operator *(HexCoordinate<TSelf> h, TSelf scalar) => new(h.m_q * scalar, h.m_r * scalar, h.m_s * scalar);
      public static HexCoordinate<TSelf> operator /(HexCoordinate<TSelf> h, TSelf scalar) => TSelf.IsZero(scalar) ? throw new System.DivideByZeroException() : new(h.m_q / scalar, h.m_r / scalar, h.m_s / scalar);
      public static HexCoordinate<TSelf> operator -(HexCoordinate<TSelf> a, HexCoordinate<TSelf> b) => new(a.m_q - b.Q, a.m_r - b.R, a.m_s - b.S);

      #endregion Overloaded operators

      #region Implemented interfaces

      public readonly string ToString(string? format, System.IFormatProvider? provider)
      {
        format ??= "N6";

        return $"<{m_q.ToString(format, provider)}, {m_r.ToString(format, provider)}, {m_s.ToString(format, provider)}>";
      }

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
