namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Usually used with integers, but can really be applied to any equatable object.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Kronecker_delta"/></para>
    /// </summary>
    public static TNumber KroneckerDelta<TNumber>(this TNumber a, TNumber b)
      where TNumber : System.Numerics.INumber<TNumber>
      => a == b ? TNumber.One : TNumber.Zero;
  }
}
