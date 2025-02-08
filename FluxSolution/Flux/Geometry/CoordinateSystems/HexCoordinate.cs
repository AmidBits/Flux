namespace Flux.Geometry.Coordinates
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
