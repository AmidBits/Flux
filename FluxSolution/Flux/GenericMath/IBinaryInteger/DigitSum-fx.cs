namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TNumber DigitSum<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertWithin(radix));

      var sum = TNumber.Zero;

      while (!TNumber.IsZero(value))
      {
        sum += value % rdx;

        value /= rdx;
      }

      return sum;
    }
  }
}
