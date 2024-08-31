namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TValue DigitSum<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      Quantities.Radix.AssertMember(radix);

      var sum = TValue.Zero;

      while (!TValue.IsZero(value))
      {
        sum += value % radix;

        value /= radix;
      }

      return sum;
    }
  }
}
