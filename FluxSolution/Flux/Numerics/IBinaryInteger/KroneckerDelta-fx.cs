namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The Kronecker delta is a function of two variables, usually just non-negative integers. The function is 1 if the variables are equal, and 0 otherwise.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Kronecker_delta"/></para>
    /// </summary>
    public static TNumber KroneckerDelta<TNumber>(this TNumber a, TNumber b)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => a == b ? TNumber.One : TNumber.Zero;
  }
}
