namespace Flux.Geometry.Hexagon
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Hex
    : System.IEquatable<Hex>
  {
    private static readonly Hex[] Directions = {
      new Hex(1, 0, -1),
      new Hex(1, -1, 0),
      new Hex(0, -1, 1),
      new Hex(-1, 0, 1),
      new Hex(-1, 1, 0),
      new Hex(0, 1, -1),
    };

    public static readonly Hex Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] public readonly int Q;
    [System.Runtime.InteropServices.FieldOffset(4)] public readonly int R;
    [System.Runtime.InteropServices.FieldOffset(8)] public readonly int S;

    public Hex(int q, int r, int s)
    {
      Q = q;
      R = r;
      S = s;
    }
    public Hex(int q, int r)
      : this(q, r, -q - r)
    { }

    #region Statics
    public static Hex Add(Hex a, Hex b)
      => new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);
    public static Hex Direction(int direction /* 0 to 5 */)
      => 0 <= direction && direction < 6 ? Directions[direction] : throw new System.ArgumentOutOfRangeException(nameof(direction));
    public static Hex Neighbor(Hex hex, int direction)
      => Add(hex, Direction(direction));
    public static int Distance(Hex a, Hex b)
      => Length(Subtract(a, b));
    public static Hex Subtract(Hex a, Hex b)
      => new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);
    public static int Length(Hex hex)
      => (System.Math.Abs(hex.Q) + System.Math.Abs(hex.R) + System.Math.Abs(hex.S)) / 2;
    public static Hex Multiply(Hex a, int k)
      => new Hex(a.Q * k, a.R * k, a.S * k);
    #endregion Statics

    #region Overloaded operators
    public static bool operator ==(Hex p1, Hex p2)
      => p1.Equals(p2);
    public static bool operator !=(Hex p1, Hex p2)
      => !p1.Equals(p2);
    #endregion Overloaded operators

    // IEquatable
    public bool Equals(Hex other)
      => Q == other.Q && R == other.R && S == other.S;

    // Overrides
    public override bool Equals(object? obj)
      => obj is Hex o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Q, R, S);
    public override string ToString()
      => $"<{GetType().Name} {Q}, {R}, {S}>";
  }
}
