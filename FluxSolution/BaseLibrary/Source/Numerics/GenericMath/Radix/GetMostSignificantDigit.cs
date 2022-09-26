#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Drop the leading digit of the number.</summary>
    public static TSelf GetMostSignificantDigit<TSelf>(this TSelf source, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(source) ? TSelf.Zero : source / GenericMath.IntegerPow(radix, DigitCount(source, radix) - TSelf.One); // Radix is already asserted elsewhere.
  }
}
#endif
