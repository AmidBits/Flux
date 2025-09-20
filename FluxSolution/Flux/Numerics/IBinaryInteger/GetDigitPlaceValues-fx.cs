namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Returns the digit place value components of <paramref name="value"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
    public static System.Collections.Generic.List<TInteger> GetDigitPlaceValues<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var list = value.GetDigitsReversed(radix); // Already asserts radix.

      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      var power = TInteger.One;

      for (int index = 0; index < list.Count; index++)
      {
        list[index] *= power;

        power *= rdx;
      }

      return list;
    }
  }
}
