namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    public static TSelf Log2F<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>
      => TSelf.IsNegative(value)
      ? -Log2F(TSelf.Abs(value))
      : TSelf.Log2(value);

#else

    public static double Log2F(this double value) => System.Math.Log2(value);

    public static float Log2F(this float value) => (float)System.Math.Log2(value);

#endif
  }
}
