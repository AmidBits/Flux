#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Drop the leading digit of the number.</summary>
    public static TSelf GetMostSignificantDigit<TSelf>(this TSelf source, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source == TSelf.Zero ? TSelf.Zero : source / GenericMath.IntegerPow(radix, DigitCount(source, radix));
  }
}
#endif
