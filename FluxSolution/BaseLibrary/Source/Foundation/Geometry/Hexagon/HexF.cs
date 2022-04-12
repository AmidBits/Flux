namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct HexF
    : System.IEquatable<HexF>
  {
    public static readonly HexF Zero;

    public readonly double m_q;
    public readonly double m_r;
    public readonly double m_s;

    public HexF(double q, double r, double s)
    {
      if (!IsCubeCoordinate(q, r, s)) throw new System.ArgumentException($"Contraint violation of cube coordinate (Q + R + S = 0) = ({q} + {r} + {s} = {System.Math.Round(q + r + s)}).");

      m_q = q;
      m_r = r;
      m_s = s;
    }
    public HexF(double q, double r)
    : this(q, r, -q - r)
    { }

    [System.Diagnostics.Contracts.Pure] public double Q => m_q;
    [System.Diagnostics.Contracts.Pure] public double R => m_r;
    [System.Diagnostics.Contracts.Pure] public double S => m_s;

    public Hex ToRoundedHex()
      => RoundToNearest(m_q, m_r, m_s);

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
    public static bool IsCubeCoordinate(double q, double r, double s)
      => System.Math.Round(q + r + s) == 0;
    public static HexF Lerp(HexF source, HexF target, double mu)
      => new(source.m_q * (1 - mu) + target.m_q * mu, source.m_r * (1 - mu) + target.m_r * mu, source.m_s * (1 - mu) + target.m_s * mu);
    public static Hex RoundToNearest(double q, double r, double s)
    {
      var rq = (int)System.Math.Round(q);
      var rr = (int)System.Math.Round(r);
      var rs = (int)System.Math.Round(s);

      double q_diff = System.Math.Abs(rq - q);
      double r_diff = System.Math.Abs(rr - r);
      double s_diff = System.Math.Abs(rs - s);

      if (q_diff > r_diff && q_diff > s_diff)
        rq = -rr - rs;
      else if (r_diff > s_diff)
        rr = -rq - rs;
      else
        rs = -rq - rr;

      return new(rq, rr, rs);
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(HexF p1, HexF p2)
     => p1.Equals(p2);
    public static bool operator !=(HexF p1, HexF p2)
      => !p1.Equals(p2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(HexF other)
      => m_q == other.m_q && m_r == other.m_r && m_s == other.m_s;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is HexF o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_q, m_r, m_s);
    public override string ToString()
      => $"{GetType().Name} {{ Q = {m_q}, R = {m_r}, S = {m_s} }}";
    #endregion Object overrides
  }
}
