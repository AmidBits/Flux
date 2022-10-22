namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct HexCoordinateR
    : IEquatable<HexCoordinateR>
#if NET7_0_OR_GREATER
    , IHexCoordinate<double>
#endif
  {
    public static readonly HexCoordinateR Zero;

    public readonly double m_q;
    public readonly double m_r;
    public readonly double m_s;

    public HexCoordinateR(double q, double r, double s)
    {
      AssertCubeCoordinate(q, r, s);

      m_q = q;
      m_r = r;
      m_s = s;
    }
    public HexCoordinateR(double q, double r)
    : this(q, r, -q - r)
    { }

    [System.Diagnostics.Contracts.Pure] public double Q { get => m_q; init => m_q = value; }
    [System.Diagnostics.Contracts.Pure] public double R { get => m_r; init => m_r = value; }
    [System.Diagnostics.Contracts.Pure] public double S { get => m_s; init => m_s = value; }

    //static public List<Hex> HexLinedraw(Hex a, Hex b)
    //{
    //  int N = a.Distance(b);
    //  FractionalHex a_nudge = new FractionalHex(a.q + 1e-06, a.r + 1e-06, a.s - 2e-06);
    //  FractionalHex b_nudge = new FractionalHex(b.q + 1e-06, b.r + 1e-06, b.s - 2e-06);
    //  List<Hex> results = new List<Hex> { };
    //  double step = 1.0 / Math.Max(N, 1);
    //  for (int i = 0; i <= N; i++)
    //  {
    //    results.Add(a_nudge.HexLerp(b_nudge, step * i).HexRound());
    //  }
    //  return results;
    //}

    #region Static methods
    public static void AssertCubeCoordinate(double q, double r, double s)
    {
      if (!IsCubeCoordinate(q, r, s))
        throw new ArgumentException($"Contraint violation of cube coordinate (Q + R + S = 0) : ({q} + {r} + {s} = {Math.Round(q + r + s)}).");
    }

    public static bool IsCubeCoordinate(double q, double r, double s)
      => System.Math.Round(q + r + s) == 0;

    public static HexCoordinateR Lerp(HexCoordinateR source, HexCoordinateR target, double mu)
      => new(
        source.m_q * (1 - mu) + target.m_q * mu,
        source.m_r * (1 - mu) + target.m_r * mu,
        source.m_s * (1 - mu) + target.m_s * mu
      );

    public static HexCoordinateI Round(HexCoordinateR hex)
    {
      var rQ = System.Math.Round(hex.m_q);
      var rR = System.Math.Round(hex.m_r);
      var rS = System.Math.Round(hex.m_s);

      var aQ = System.Math.Abs(rQ - hex.m_q);
      var aR = System.Math.Abs(rR - hex.m_r);
      var aS = System.Math.Abs(rS - hex.m_s);

      if (aQ > aR && aQ > aS)
        rQ = -rR - rS;
      else if (aR > aS)
        rR = -rQ - rS;
      else
        rS = -rQ - rR;

      return new(
        System.Convert.ToInt32(rQ),
        System.Convert.ToInt32(rR),
        System.Convert.ToInt32(rS)
      );
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(HexCoordinateR p1, HexCoordinateR p2)
     => p1.Equals(p2);
    public static bool operator !=(HexCoordinateR p1, HexCoordinateR p2)
      => !p1.Equals(p2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(HexCoordinateR other)
      => m_q == other.m_q && m_r == other.m_r && m_s == other.m_s;

#if NET7_0_OR_GREATER
    // IHexCoordinate<>
    public IHexCoordinate<double> Create(double q, double r, double s)
      => new HexCoordinateR(q, r, s);
#endif
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is HexCoordinateR o && Equals(o);
    public override int GetHashCode()
      => HashCode.Combine(m_q, m_r, m_s);
    public override string ToString()
      => $"{GetType().Name} {{ Q = {m_q}, R = {m_r}, S = {m_s} }}";
    #endregion Object overrides
  }
}
