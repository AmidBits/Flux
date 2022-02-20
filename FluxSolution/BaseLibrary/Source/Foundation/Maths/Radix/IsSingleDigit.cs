namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this System.Numerics.BigInteger value)
      => value >= -9 && value <= 9;
    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this int value)
      => value >= -9 && value <= 9;
    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this long value)
      => value >= -9 && value <= 9;
  }
}
