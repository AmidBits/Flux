namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Creates a new string formatted as a ratio string, optionally reducing the ratio, if possible.</summary>
    public static string ToRatioString<TSelf>(TSelf source, TSelf target, bool reduce)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (reduce ? source.GreatestCommonDivisor(target) : TSelf.One) is var gcd ? $"{source / gcd}\u2236{target / gcd}" : throw new System.Exception();
  }
}
