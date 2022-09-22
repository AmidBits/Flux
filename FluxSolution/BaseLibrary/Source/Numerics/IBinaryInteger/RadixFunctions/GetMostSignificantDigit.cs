#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>PREVIEW! Drop the leading digit of the number.</summary>
    public static TSelf GetMostSignificantDigit<TSelf>(this TSelf source, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source == TSelf.Zero ? TSelf.Zero : source / IntegerPow(radix, DigitCount(source, radix));
  }
}
#endif
