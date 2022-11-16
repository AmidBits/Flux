namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the minimum of the three values.</summary>
    public static TSelf Min<TSelf>(this TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Min(TSelf.Min(a, b), c); // a <= b ? (a <= c ? a : c) : (b <= c ? b : c);

    /// <summary>Returns the minimum of the four values.</summary>
    public static TSelf Min<TSelf>(this TSelf a, TSelf b, TSelf c, TSelf d)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Min(TSelf.Min(a, b), TSelf.Min(c, d)); // a <= b ? (a <= c ? (a <= d ? a : d) : (c <= d ? c : d)) : (b <= c ? (b <= d ? b : d) : (c <= d ? c : d));
  }
}
