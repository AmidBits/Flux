namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TValue DigitSum<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      var sum = TValue.Zero;

      while (!TValue.IsZero(value))
      {
        sum += value % rdx;

        value /= rdx;
      }

      return sum;
    }
  }
}
