namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the digit place value components of <paramref name="value"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
    public static System.Collections.Generic.List<TValue> GetDigitPlaceValues<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      var list = value.GetDigitsReversed(radix); // Already asserts radix.

      var power = TValue.One;

      for (int index = 0; index < list.Count; index++)
      {
        list[index] *= power;

        power *= radix;
      }

      return list;
    }
  }
}
