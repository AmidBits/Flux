namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Indicates whether the <paramref name="number"/> is single digit using the base <paramref name="radix"/>, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
    public static bool IsSingleDigit<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TSelf.IsZero(number) || (TSelf.IsPositive(number) && number < AssertRadix(radix, out TSelf ptradix)) || (TSelf.IsNegative(number) && number > -AssertRadix(radix, out TSelf ntradix));
  }
}
