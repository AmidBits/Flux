namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Creates a new string representing the ratio between <paramref name="a"/> and <paramref name="b"/> with the option to reduce (when possible) beforehand.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Ratio"/>
    public static string ToRatioString<TSelf>(this TSelf a, TSelf b, bool reduceWhenPossible)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => reduceWhenPossible && GreatestCommonDivisor(a, b) is var gcd && gcd > TSelf.One
      ? $"{a / gcd}\u2236{b / gcd}"
      : $"{a}\u2236{b}";
  }
}
