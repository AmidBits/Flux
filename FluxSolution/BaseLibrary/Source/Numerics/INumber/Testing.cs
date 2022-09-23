#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Number
  {
    /// <summary>PREVIEW! Usually used with integers, but can really be applied to any equatable object.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Kronecker_delta"/>
    public static string Testing<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      return string.Join('|', value.GetType().GetTypeChain());

      return "Unknown";
    }
  }
}
#endif
