#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Max routine for 2 values of T (where T : System.IComparable<T>).</summary>
    public static TSelf Max<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.INumber<TSelf>
      => a >= b ? a : b;

    /// <summary>PREVIEW! Max routine for 3 values of T (where T : System.IComparable<T>).</summary>
    public static TSelf Max<TSelf>(this TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.INumber<TSelf>
      => a >= b ? (a >= c ? a : c) : (b >= c ? b : c);

    /// <summary>PREVIEW! Max routine for 4 values of T (where T : System.IComparable<T>).</summary>
    public static TSelf Max<TSelf>(this TSelf a, TSelf b, TSelf c, TSelf d)
      where TSelf : System.Numerics.INumber<TSelf>
      => a >= b ? (a >= c ? (a >= d ? a : d) : (c >= d ? c : d)) : (b >= c ? (b >= d ? b : d) : (c >= d ? c : d));
  }
}
#endif