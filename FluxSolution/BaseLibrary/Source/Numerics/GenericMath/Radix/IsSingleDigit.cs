namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Indicates whether the <paramref name="number"/> is single digit using the base <paramref name="radix"/>, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
    public static bool IsSingleDigit<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TSelf.CreateChecked(AssertRadix(radix)) is var rdx && (TSelf.IsZero(number) || (TSelf.IsPositive(number) && number < rdx) || (TSelf.IsNegative(number) && number > -rdx));
  }
}
