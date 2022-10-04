#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Drop the leading digit of x using base b.</summary>
    public static TSelf DropLeadingDigit<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => DigitCount(x, b) is var dc && dc <= TSelf.One ? TSelf.Zero : x % IntegerPow(b, dc - TSelf.One);
  }
}
#endif
