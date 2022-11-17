namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the maximum of three values.</summary>
    public static TSelf Max<TSelf>(this TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Max(TSelf.Max(a, b), c); // a >= b ? (a >= c ? a : c) : (b >= c ? b : c);

    /// <summary>Returns the maximum of four values.</summary>
    public static TSelf Max<TSelf>(this TSelf a, TSelf b, TSelf c, TSelf d)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Max(TSelf.Max(TSelf.Max(a, b), c), d); // a >= b ? (a >= c ? (a >= d ? a : d) : (c >= d ? c : d)) : (b >= c ? (b >= d ? b : d) : (c >= d ? c : d));
  }
}
