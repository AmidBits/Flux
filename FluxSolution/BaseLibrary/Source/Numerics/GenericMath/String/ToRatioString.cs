#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Creates a new string representing the ratio.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Ratio"/>
    public static System.ReadOnlySpan<char> ToRatioString<TSelf>(this TSelf source, TSelf target, bool reduceWhenPossible)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => reduceWhenPossible && GreatestCommonDivisor(source, target) is var gcd
      ? $"{source / gcd}\u2236{target / gcd}"
      : $"{source}\u2236{target}";
  }
}
#endif
