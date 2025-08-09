namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Gets the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static (TNumber DigitCount, TNumber DigitSum, bool IsJumbled, bool IsPowOf, TNumber NumberReversed, System.Collections.Generic.List<TNumber> PlaceValues, System.Collections.Generic.List<TNumber> ReverseDigits) MagicalDigits<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(radix));

      var count = TNumber.Zero;
      var isJumbled = true;
      var numberReversed = TNumber.Zero;
      var placeValues = new System.Collections.Generic.List<TNumber>();
      var reverseDigits = new System.Collections.Generic.List<TNumber>();
      var sum = TNumber.Zero;

      var power = TNumber.One;

      while (!TNumber.IsZero(value))
      {
        var rem = value % rdx;

        count++;
        numberReversed = (numberReversed * rdx) + rem;
        placeValues.Add(rem * power);
        reverseDigits.Add(rem);
        sum += rem;

        power *= rdx;

        value /= rdx;

        if (isJumbled && (TNumber.Abs((value % rdx) - rem) > TNumber.One))
          isJumbled = false;
      }

      if (TNumber.IsZero(count))
      {
        placeValues.Add(count);
        reverseDigits.Add(count);
      }

      return (count, sum, isJumbled, sum == TNumber.One, numberReversed, placeValues, reverseDigits);
    }
  }
}
