#if NET7_0_OR_GREATER
namespace Flux
{
  #region ExtensionMethods

  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.HexCoordinate{TSelf}"/> from a <see cref="System.Drawing.Point"/>.</summary>
    public static Geometry.HexCoordinate<int> ToHexCoordinate(this System.Drawing.Point source)
      => new(
        source.X,
        source.Y
      );

    /// <summary>Creates a new <see cref="Geometry.HexCoordinate{TSelf}"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.HexCoordinate<float> ToHexCoordinate(this System.Numerics.Vector3 source)
      => new(
        source.X,
        source.Y,
        source.Z
      );
  }

  #endregion

  namespace Geometry
  {
    /// <summary>A cube hex coordinate system.</summary>
    /// <see href="https://www.redblobgames.com/grids/hexagons/"/>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct HexCoordinate<TSelf>
      : System.IFormattable, IHexCoordinate<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
    {
      public static readonly HexCoordinate<TSelf> Zero;

      private readonly TSelf m_q;
      private readonly TSelf m_r;
      private readonly TSelf m_s;

      public HexCoordinate(TSelf q, TSelf r, TSelf s)
      {
        m_q = q;
        m_r = r;
        m_s = s;

        this.AssertCubeCoordinate();
      }
      public HexCoordinate(TSelf q, TSelf r)
        : this(q, r, -q - r)
      { }

      public void Deconstruct(out TSelf q, out TSelf r, out TSelf s) { q = m_q; r = m_r; s = m_s; }

      public TSelf Q { get => m_q; init => m_q = value; }
      public TSelf R { get => m_r; init => m_r = value; }
      public TSelf S { get => m_s; init => m_s = value; }

      public System.Drawing.Point ToPoint()
        => new(int.CreateChecked(m_q), int.CreateChecked(m_r));

      public System.Numerics.Vector3 ToVector3()
        => new(float.CreateChecked(m_q), float.CreateChecked(m_r), float.CreateChecked(m_s));

      #region Static methods

      /// <summary>Computes the count of hexes in the range of, i.e. any hex that is on or inside, the specified radius.</summary>
      public static int ComputeRangeCount(int radius)
        => Iteration.Range(0, radius + 1, 6).AsParallel().Sum() + 1;

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

      #endregion // Static methods

      #region Overloaded operators

      public static HexCoordinate<TSelf> operator +(HexCoordinate<TSelf> a, IHexCoordinate<TSelf> b) => new(a.m_q + b.Q, a.m_r + b.R, a.m_s + b.S);
      public static HexCoordinate<TSelf> operator *(HexCoordinate<TSelf> h, TSelf scalar) => new(h.m_q * scalar, h.m_r * scalar, h.m_s * scalar);
      public static HexCoordinate<TSelf> operator /(HexCoordinate<TSelf> h, TSelf scalar) => TSelf.IsZero(scalar) ? throw new System.DivideByZeroException() : new(h.m_q / scalar, h.m_r / scalar, h.m_s / scalar);
      public static HexCoordinate<TSelf> operator -(HexCoordinate<TSelf> a, IHexCoordinate<TSelf> b) => new(a.m_q - b.Q, a.m_r - b.R, a.m_s - b.S);

      #endregion Overloaded operators

      #region Implemented interfaces

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"{GetType().GetNameEx()} {{ Q = {string.Format(provider, $"{{0:{format ?? "N6"}}}", Q)}, R = {string.Format(provider, $"{{0:{format ?? "N6"}}}", R)}, S = {string.Format(provider, $"{{0:{format ?? "N6"}}}", S)} }}";

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
#endif
