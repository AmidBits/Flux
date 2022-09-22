#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Number
  {
    /// <summary>PREVIEW! Min routine for 2 values of T (where T : System.IComparable<T>).</summary>
    public static TSelf Min<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Min(a, b);

    /// <summary>PREVIEW! Min routine for 3 values of T (where T : System.IComparable<T>).</summary>
    public static TSelf Min<TSelf>(this TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Min(TSelf.Min(a, b), c); // a <= b ? (a <= c ? a : c) : (b <= c ? b : c);

    /// <summary>PREVIEW! Min routine for 4 values of T (where T : System.IComparable<T>).</summary>
    public static TSelf Min<TSelf>(this TSelf a, TSelf b, TSelf c, TSelf d)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Min(TSelf.Min(TSelf.Min(a, b), c), d); // a <= b ? (a <= c ? (a <= d ? a : d) : (c <= d ? c : d)) : (b <= c ? (b <= d ? b : d) : (c <= d ? c : d));
  }
}
#endif
