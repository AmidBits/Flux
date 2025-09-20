namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>The Kronecker delta is a function of two variables, usually just non-negative integers. The function is 1 if the variables are equal, and 0 otherwise.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Kronecker_delta"/></para>
    /// </summary>
    public static TInteger KroneckerDelta<TInteger>(this TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => a == b ? TInteger.One : TInteger.Zero;
  }
}
