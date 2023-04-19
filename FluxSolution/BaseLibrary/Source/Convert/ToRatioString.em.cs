namespace Flux
{
  public static partial class Convert
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates a new string formatted as a ratio string, optionally reducing the ratio, if possible.</summary>
    public static string ToRatioString<TSelf>(TSelf numerator, TSelf denominator, bool reduceIfPossible)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (reduceIfPossible ? numerator.GreatestCommonDivisor(denominator) : TSelf.One) is var gcd ? $"{numerator / gcd}\u2236{denominator / gcd}" : throw new System.Exception();

#else

    /// <summary>Creates a new string formatted as a ratio string, optionally reducing the ratio, if possible.</summary>
    public static string ToRatioString(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, bool reduceIfPossible)
      => (reduceIfPossible ? numerator.GreatestCommonDivisor(denominator) : System.Numerics.BigInteger.One) is var gcd ? $"{numerator / gcd}\u2236{denominator / gcd}" : throw new System.Exception();

#endif
  }
}
