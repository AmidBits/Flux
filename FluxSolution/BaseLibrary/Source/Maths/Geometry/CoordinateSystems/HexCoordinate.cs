#if NET7_0_OR_GREATER
namespace Flux.Geometry
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

    #region Overloaded operators

    public static HexCoordinate<TSelf> operator +(HexCoordinate<TSelf> a, IHexCoordinate<TSelf> b) => new(a.m_q + b.Q, a.m_r + b.R, a.m_s + b.S);
    public static HexCoordinate<TSelf> operator *(HexCoordinate<TSelf> h, TSelf scalar) => new(h.m_q * scalar, h.m_r * scalar, h.m_s * scalar);
    public static HexCoordinate<TSelf> operator /(HexCoordinate<TSelf> h, TSelf scalar) => TSelf.IsZero(scalar) ? throw new System.DivideByZeroException() : new(h.m_q / scalar, h.m_r / scalar, h.m_s / scalar);
    public static HexCoordinate<TSelf> operator -(HexCoordinate<TSelf> a, IHexCoordinate<TSelf> b) => new(a.m_q - b.Q, a.m_r - b.R, a.m_s - b.S);

    #endregion Overloaded operators

    public string ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().GetNameEx()} {{ Q = {string.Format(provider, $"{{0:{format ?? "N6"}}}", Q)}, R = {string.Format(provider, $"{{0:{format ?? "N6"}}}", R)}, S = {string.Format(provider, $"{{0:{format ?? "N6"}}}", S)} }}";

    public override string ToString() => ToString(null, null);
  }
}
#endif
