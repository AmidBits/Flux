namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Gets the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static (TValue count, bool isPowOf, TValue numberReversed, System.Collections.Generic.List<TValue> placeValues, System.Collections.Generic.List<TValue> reverseDigits, TValue sum) MagicalDigits<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.INumber<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      var count = TValue.Zero;
      var isPowOf = (value == TValue.One);
      var numberReversed = TValue.Zero;
      var placeValues = new System.Collections.Generic.List<TValue>();
      var reverseDigits = new System.Collections.Generic.List<TValue>();
      var sum = TValue.Zero;

      var power = TValue.One;

      while (!TValue.IsZero(value))
      {
        var rem = value % rdx;

        count++;
        numberReversed = (numberReversed * rdx) + rem;
        placeValues.Add(rem * power);
        reverseDigits.Add(rem);
        sum += rem;

        power *= rdx;

        value /= rdx;

        if (TValue.IsZero(rem)) isPowOf = (value == TValue.One);
      }

      if (reverseDigits.Count == 0)
      {
        placeValues.Add(TValue.Zero);
        reverseDigits.Add(TValue.Zero);
      }

      return (count, isPowOf, numberReversed, placeValues, reverseDigits, sum);
    }
  }
}
