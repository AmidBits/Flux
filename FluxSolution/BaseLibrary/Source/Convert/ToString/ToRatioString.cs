namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    public static string ToRatioString(System.Numerics.BigInteger source, System.Numerics.BigInteger target) => System.Numerics.BigInteger.GreatestCommonDivisor(source, target) is var gcd ? $"{source / gcd}:{target / gcd}" : throw new System.Exception();

    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    public static string ToRatioString(int source, int target) => Maths.GreatestCommonDivisor(source, target) is var gcd ? $"{source / gcd}:{target / gcd}" : throw new System.Exception();
    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    public static string ToRatioString(long source, long target) => Maths.GreatestCommonDivisor(source, target) is var gcd ? $"{source / gcd}:{target / gcd}" : throw new System.Exception();
  }
}
