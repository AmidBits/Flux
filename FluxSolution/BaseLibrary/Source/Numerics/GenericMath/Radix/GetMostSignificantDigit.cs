#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Drop the leading digit of x using base b.</summary>
    public static TSelf GetMostSignificantDigit<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(x) ? TSelf.Zero : x / IntegerPow(b, DigitCount(x, b) - TSelf.One); // Radix is already asserted elsewhere.
  }
}
#endif
