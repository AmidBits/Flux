namespace Flux.Geometry.Hexagon
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct HexF
    : System.IEquatable<HexF>
  {
    public static readonly HexF Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] public readonly double Q;
    [System.Runtime.InteropServices.FieldOffset(8)] public readonly double R;
    [System.Runtime.InteropServices.FieldOffset(16)] public readonly double S;

    public HexF(double q, double r, double s)
    {
      if (System.Math.Round(q + r + s) != 0) throw new System.ArgumentException("q + r + s must be 0");

      Q = q;
      R = r;
      S = s;
    }
    public HexF(double q, double r)
    : this(q, r, -q - r)
    { }

    public Hex ToHex()
    {
      var qi = (int)System.Math.Round(Q);
      var ri = (int)System.Math.Round(R);
      var si = (int)System.Math.Round(S);

      double q_diff = System.Math.Abs(qi - Q);
      double r_diff = System.Math.Abs(ri - R);
      double s_diff = System.Math.Abs(si - S);

      if (q_diff > r_diff && q_diff > s_diff)
      {
        qi = -ri - si;
      }
      else if (r_diff > s_diff)
      {
        ri = -qi - si;
      }
      else
      {
        si = -qi - ri;
      }

      return new Hex(qi, ri, si);
    }

    #region Static methods
    public static HexF Lerp(HexF source, HexF target, double mu)
      => new HexF(source.Q * (1.0 - mu) + target.Q * mu, source.R * (1.0 - mu) + target.R * mu, source.S * (1.0 - mu) + target.S * mu);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(HexF p1, HexF p2)
     => p1.Equals(p2);
    public static bool operator !=(HexF p1, HexF p2)
      => !p1.Equals(p2);
    #endregion Overloaded operators

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

    // IEquatable
    public bool Equals(HexF other)
      => Q == other.Q && R == other.R && S == other.S;

    // Overrides
    public override bool Equals(object? obj)
      => obj is HexF o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Q, R, S);
    public override string ToString()
      => $"<{GetType().Name} {Q}, {R}, {S}>";
  }
}
