namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Usually used with integers, but can really be applied to any equatable object.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Kronecker_delta"/>
    public static TSelf KroneckerDelta<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.INumber<TSelf>
      => a == b ? TSelf.One : TSelf.Zero;

#else

    /// <summary>Usually used with integers, but can really be applied to any equatable object.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Kronecker_delta"/>
    public static int KroneckerDelta<T>(T i, T j)
      where T : System.IEquatable<T>
      => i.Equals(j) ? 1 : 0;

#endif
  }
}
