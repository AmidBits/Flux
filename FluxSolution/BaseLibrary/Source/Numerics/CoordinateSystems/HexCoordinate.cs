namespace Flux
{
  /// <summary>Cube hex coordinate system using integer.</summary>
  /// <see href="https://www.redblobgames.com/grids/hexagons/"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct HexCoordinate<TSelf>
    : IHexCoordinate<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
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

    public TSelf Q { get => m_q; init => m_q = value; }
    public TSelf R { get => m_r; init => m_r = value; }
    public TSelf S { get => m_s; init => m_s = value; }

    #region Overloaded operators
    public static HexCoordinate<TSelf> operator +(HexCoordinate<TSelf> a, IHexCoordinate<TSelf> b)
      => new(a.m_q + b.Q, a.m_r + b.R, a.m_s + b.S);
    public static HexCoordinate<TSelf> operator *(HexCoordinate<TSelf> h, TSelf scalar)
      => new(h.m_q * scalar, h.m_r * scalar, h.m_s * scalar);
    public static HexCoordinate<TSelf> operator /(HexCoordinate<TSelf> h, TSelf scalar)
      => TSelf.IsZero(scalar)
      ? throw new System.DivideByZeroException()
      : new(h.m_q / scalar, h.m_r / scalar, h.m_s / scalar);
    public static HexCoordinate<TSelf> operator -(HexCoordinate<TSelf> a, IHexCoordinate<TSelf> b)
      => new(a.m_q - b.Q, a.m_r - b.R, a.m_s - b.S);
    #endregion Overloaded operators
  }
}
