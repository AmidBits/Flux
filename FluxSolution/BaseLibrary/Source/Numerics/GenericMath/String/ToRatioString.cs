#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Creates a new string representing the ratio between <paramref name="a"/> and <paramref name="b"/> with the option to reduce (when possible) beforehand.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Ratio"/>
    public static System.ReadOnlySpan<char> ToRatioString<TSelf>(this TSelf a, TSelf b, bool reduceWhenPossible)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => reduceWhenPossible && GreatestCommonDivisor(a, b) is var gcd
      ? $"{a / gcd}\u2236{b / gcd}"
      : $"{a}\u2236{b}";
  }
}
#endif
