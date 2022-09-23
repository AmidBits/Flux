#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Usually used with integers, but can really be applied to any equatable object.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Kronecker_delta"/>
    public static TSelf KroneckerDelta<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.INumber<TSelf>
      => a == b ? TSelf.One : TSelf.Zero;
  }
}
#endif
