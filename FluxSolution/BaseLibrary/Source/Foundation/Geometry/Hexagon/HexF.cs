namespace Flux.Geometry.Hexagon
{
  public struct HexF
    : System.IEquatable<HexF>
  {
    public static readonly HexF Zero;

    public readonly double Q;
    public readonly double R;
    public readonly double S;

    public HexF(double q, double r, double s)
    {
      if (!IsCubeCoordinate(q, r, s)) throw new System.ArgumentException($"Contraint violation of cube coordinate (Q + R + S = 0) = ({q} + {r} + {s} = {System.Math.Round(q + r + s)}).");

      Q = q;
      R = r;
      S = s;
    }
    public HexF(double q, double r)
    : this(q, r, -q - r)
    { }

    public Hex ToRoundedHex()
      => RoundToNearest(Q, R, S);

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
      => new(source.Q * (1 - mu) + target.Q * mu, source.R * (1 - mu) + target.R * mu, source.S * (1 - mu) + target.S * mu);
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
      => Q == other.Q && R == other.R && S == other.S;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is HexF o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Q, R, S);
    public override string ToString()
      => $"<{GetType().Name} {Q}, {R}, {S}>";
    #endregion Object overrides
  }
}
