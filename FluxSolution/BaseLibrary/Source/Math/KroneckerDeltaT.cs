
namespace Flux
{
  public static partial class Math
  {
    /// <summary>Usually used with integers, but can really be applied to any equatable object.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Kronecker_delta"/>
    public static int KroneckerDelta<T>(T i, T j)
      where T : System.IEquatable<T>
      => i.Equals(j) ? 1 : 0;
  }
}
