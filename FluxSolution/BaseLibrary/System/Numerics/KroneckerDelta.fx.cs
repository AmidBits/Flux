namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Usually used with integers, but can really be applied to any equatable object.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Kronecker_delta"/>
    public static TSelf KroneckerDelta<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => a == b ? TSelf.One : TSelf.Zero;
  }
}