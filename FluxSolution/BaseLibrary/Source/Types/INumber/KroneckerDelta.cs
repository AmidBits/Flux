#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Usually used with integers, but can really be applied to any equatable object.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Kronecker_delta"/>
    public static TSelf KroneckerDelta<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryNumber<TSelf>
      => a.Equals(b) ? TSelf.One : TSelf.Zero;
  }
}
#endif
