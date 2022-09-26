#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Drop the leading digit of the number.</summary>
    public static TSelf DropLeadingDigit<TSelf>(this TSelf source, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => DigitCount(source, radix) is var dc && dc <= TSelf.One ? TSelf.Zero : source % GenericMath.IntegerPow(radix, dc - TSelf.One);
  }
}
#endif
