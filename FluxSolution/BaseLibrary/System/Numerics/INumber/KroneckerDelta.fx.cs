namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Usually used with integers, but can really be applied to any equatable object.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Kronecker_delta"/>
    public static TNumber KroneckerDelta<TNumber>(this TNumber a, TNumber b)
      where TNumber : System.Numerics.INumber<TNumber>
      => a == b ? TNumber.One : TNumber.Zero;
  }
}
