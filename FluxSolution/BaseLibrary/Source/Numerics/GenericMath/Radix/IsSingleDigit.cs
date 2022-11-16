namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Indicates whether the <paramref name="number"/> is single digit using the base <paramref name="radix"/>, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
    public static bool IsSingleDigit<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(number) || (TSelf.Abs(radix) is var absRadix && ((TSelf.IsNegative(number) && number > -absRadix) || (TSelf.IsPositive(number) && number < absRadix)));
  }
}
