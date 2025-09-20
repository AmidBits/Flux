namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Gets the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static (TInteger DigitCount, TInteger DigitSum, bool IsJumbled, bool IsPowOf, TInteger NumberReversed, System.Collections.Generic.List<TInteger> PlaceValues, System.Collections.Generic.List<TInteger> ReverseDigits) MagicalDigits<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      var count = TInteger.Zero;
      var isJumbled = true;
      var numberReversed = TInteger.Zero;
      var placeValues = new System.Collections.Generic.List<TInteger>();
      var reverseDigits = new System.Collections.Generic.List<TInteger>();
      var sum = TInteger.Zero;

      var power = TInteger.One;

      while (!TInteger.IsZero(value))
      {
        var rem = value % rdx;

        count++;
        numberReversed = (numberReversed * rdx) + rem;
        placeValues.Add(rem * power);
        reverseDigits.Add(rem);
        sum += rem;

        power *= rdx;

        value /= rdx;

        if (isJumbled && (TInteger.Abs((value % rdx) - rem) > TInteger.One))
          isJumbled = false;
      }

      if (TInteger.IsZero(count))
      {
        placeValues.Add(count);
        reverseDigits.Add(count);
      }

      return (count, sum, isJumbled, sum == TInteger.One, numberReversed, placeValues, reverseDigits);
    }
  }
}
