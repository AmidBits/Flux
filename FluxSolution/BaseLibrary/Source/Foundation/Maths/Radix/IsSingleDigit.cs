namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this System.Numerics.BigInteger value, int radix)
      => AssertRadix(radix) == radix && (radix < 0 && value > radix) && value < radix;
    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this int value, int radix)
      => AssertRadix(radix) == radix && (radix < 0 && value > radix) && value < radix;
    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this long value, int radix)
      => AssertRadix(radix) == radix && (radix < 0 && value > radix) && value < radix;
  }
}
